
/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// Status of a vote receipt to check.
  /// </summary>
  public enum VoteReceiptStatus
  {
    NotFound,
    FoundBad,
    FoundOk
  }

  /// <summary>
  /// Asynchronous client.
  /// </summary>
  public partial class VotingClient
  {
    /// <summary>
    /// Callback for the result get operation.
    /// </summary>
    /// <param name="result">Voting result or null in case of failure.</param>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void GetResultCallBack(VotingResult result, IDictionary<Guid, VoteReceiptStatus> voteReceiptsStatus, Exception exception);

    /// <summary>
    /// Result get operation.
    /// </summary>
    private class GetResultOperation : Operation
    {
      /// <summary>
      /// Id of the voting.
      /// </summary>
      private Guid votingId;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private GetResultCallBack callBack;

      /// <summary>
      /// Queue with enveloped to verify and add.
      /// </summary>
      private Queue<Tuple<int, Signed<Envelope>>> envelopeQueue;

      /// <summary>
      /// Receipts the server issued for this vote by voter id.
      /// </summary>
      private Dictionary<Guid, Signed<VoteReceipt>> voteReceipts;

      /// <summary>
      /// Status of the vote receipts to be checked.
      /// </summary>
      private Dictionary<Guid, VoteReceiptStatus> voteReceiptsStatus;

      /// <summary>
      /// Continue to run worker threads?
      /// </summary>
      private bool workerRun;

      /// <summary>
      /// Voter client to work with.
      /// </summary>
      private VotingClient client;

      /// <summary>
      /// Number of envelopes fetched from the server.
      /// </summary>
      private int fetchedEnvelopes;

      /// <summary>
      /// Number of envelopes verified.
      /// </summary>
      private int verifiedEnvelopes;

      /// <summary>
      /// Number of envelopes in voting.
      /// </summary>
      private int envelopeCount;

      /// <summary>
      /// Create a new result get operation.
      /// </summary>
      /// <param name="votingId">Id of the voting.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public GetResultOperation(Guid votingId, IEnumerable<Signed<VoteReceipt>> signedVoteReceipts, GetResultCallBack callBack)
      {
        this.votingId = votingId;
        this.callBack = callBack;

        this.voteReceipts = new Dictionary<Guid, Signed<VoteReceipt>>();
        signedVoteReceipts.Foreach(signedVoteReceipt => this.voteReceipts.Add(signedVoteReceipt.Value.VoterId, signedVoteReceipt));

        this.voteReceiptsStatus = new Dictionary<Guid, VoteReceiptStatus>();
        signedVoteReceipts.Foreach(signedVoteReceipt => this.voteReceiptsStatus.Add(signedVoteReceipt.Value.VoterId, VoteReceiptStatus.NotFound));
      }

      /// <summary>
      /// Execute the operation.
      /// </summary>
      /// <param name="client">Voter client to execute against.</param>
      public override void Execute(VotingClient client)
      {
        try
        {
          Text = LibraryResources.ClientGetResultFetchMaterial;
          Progress = 0d;
          SubText = string.Empty;
          SubProgress = 0d;

          var material = client.proxy.FetchVotingMaterial(votingId);
          if (!material.Valid(client.voterEntity.CertificateStorage))
            throw new InvalidOperationException(LibraryResources.ClientGetResultMaterialInvalid);
          var parameters = material.Parameters.Value;

          Progress = 0.1d;
          Text = LibraryResources.ClientGetResultFetchEnvelopeCount;

          this.envelopeCount = client.proxy.FetchEnvelopeCount(votingId);

          Progress = 0.2d;
          Text = LibraryResources.ClientGetResultFetchEnvelopes;
          SubText = string.Format(LibraryResources.ClientGetResultFetchEnvelopesOf, 0, this.envelopeCount);
          SubProgress = 0d;

          client.voterEntity.TallyBegin(material);

          this.client = client;
          this.envelopeQueue = new Queue<Tuple<int, Signed<Envelope>>>();
          this.workerRun = true;
          Thread fetcher = new Thread(FetchWorker);
          fetcher.Start();
          List<Thread> workers = new List<Thread>();
          Environment.ProcessorCount.Times(() => workers.Add(new Thread(TallyAddWorker)));
          workers.ForEach(worker => worker.Start());

          while (this.verifiedEnvelopes < this.envelopeCount)
          {
            SubText = string.Format(LibraryResources.ClientGetResultFetchEnvelopesOf, this.verifiedEnvelopes, this.envelopeCount);
            SubProgress = 0.2d / (double)this.envelopeCount * (double)this.fetchedEnvelopes +
                          0.8d / (double)this.envelopeCount * (double)this.verifiedEnvelopes;

            Thread.Sleep(10);
          }

          Progress = 0.8d;
          Text = LibraryResources.ClientGetResultFetchPartialDeciphers;
          SubText = string.Format(LibraryResources.ClientGetResultFetchPartialDeciphersOf, 0, parameters.AuthorityCount);
          SubProgress = 0d;

          fetcher.Join();
          this.workerRun = false;
          workers.ForEach(worker => worker.Join());

          for (int authorityIndex = 1; authorityIndex < parameters.AuthorityCount + 1; authorityIndex++)
          {
            client.voterEntity.TallyAddPartialDecipher(client.proxy.FetchPartialDecipher(votingId, authorityIndex));

            SubText = string.Format(LibraryResources.ClientGetResultFetchPartialDeciphersOf, authorityIndex, parameters.AuthorityCount);
            SubProgress = 1d / (double)parameters.AuthorityCount * (double)(authorityIndex);
          }

          Progress = 0.9d;
          Text = LibraryResources.ClientGetResultDecipherResult;
          SubProgress = 0d;

          var result = client.voterEntity.TallyResult;

          SubProgress = 1d;
          Progress = 1d;

          this.callBack(result, this.voteReceiptsStatus, null);
        }
        catch (Exception exception)
        {
          this.callBack(null, null, exception);
        }
      }

      /// <summary>
      /// Thread that fetches envelopes from server.
      /// </summary>
      private void FetchWorker()
      {
        for (int envelopeIndex = 0; envelopeIndex < this.envelopeCount; envelopeIndex++)
        {
          while (this.envelopeQueue.Count > Environment.ProcessorCount * 2)
          {
            Thread.Sleep(1);
          }

          var envelope = client.proxy.FetchEnvelope(votingId, envelopeIndex);

          lock (this.envelopeQueue)
          {
            this.envelopeQueue.Enqueue(new Tuple<int, Signed<Envelope>>(envelopeIndex, envelope));
          }

          Interlocked.Increment(ref this.fetchedEnvelopes);
        }
      }

      /// <summary>
      /// Thread that adds envelopes to tally.
      /// </summary>
      private void TallyAddWorker()
      {
        while (this.workerRun)
        {
          Tuple<int, Signed<Envelope>> indexedSignedEnvelope = null;

          lock (this.envelopeQueue)
          {
            if (this.envelopeQueue.Count > 0)
            {
              indexedSignedEnvelope = this.envelopeQueue.Dequeue();
            }
          }

          if (indexedSignedEnvelope == null)
          {
            Thread.Sleep(1);
          }
          else
          {
            this.client.voterEntity.TallyAdd(indexedSignedEnvelope.First, indexedSignedEnvelope.Second);

            Envelope envelope = indexedSignedEnvelope.Second.Value;
            if (this.voteReceipts.ContainsKey(envelope.VoterId))
            {
              Signed<VoteReceipt> signedVoteReceipt = null;

              lock (this.voteReceipts)
              {
                signedVoteReceipt = this.voteReceipts[envelope.VoterId];
              }

              VoteReceipt voteReceipt = signedVoteReceipt.Value;

              if (voteReceipt.Verify(indexedSignedEnvelope.Second))
              {
                lock (this.voteReceiptsStatus)
                {
                  this.voteReceiptsStatus[voteReceipt.VoterId] = VoteReceiptStatus.FoundOk;
                }
              }
              else
              {
                lock (this.voteReceiptsStatus)
                {
                  this.voteReceiptsStatus[voteReceipt.VoterId] = VoteReceiptStatus.FoundBad;
                }
              }
            }

            Interlocked.Increment(ref this.verifiedEnvelopes);
          }
        }
      }
    }
  }
}
