
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
using System.IO;
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
      /// Path for offline checking files.
      /// </summary>
      private string offlinePath;

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
      /// Indivual progress by thread id.
      /// </summary>
      private Dictionary<int, double> threadProgress;

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
      /// Create a new result get operation.
      /// </summary>
      /// <param name="offlinePath">Path for offline checking files.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public GetResultOperation(string offlinePath, IEnumerable<Signed<VoteReceipt>> signedVoteReceipts, GetResultCallBack callBack)
      {
        this.offlinePath = offlinePath;
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

          VotingMaterial material = null;

          if (this.offlinePath.IsNullOrEmpty())
          {
            material = client.proxy.FetchVotingMaterial(votingId);
          }
          else
          {
            material = Serializable.Load<VotingMaterial>(Path.Combine(this.offlinePath, Files.VotingMaterialFileName));
          }

          if (!material.Valid(client.voterEntity.CertificateStorage))
            throw new InvalidOperationException(LibraryResources.ClientGetResultMaterialInvalid);
          var parameters = material.Parameters.Value;

          Progress = 0.1d;
          Text = LibraryResources.ClientGetResultFetchEnvelopeCount;

          if (this.offlinePath.IsNullOrEmpty())
          {
            this.envelopeCount = client.proxy.FetchEnvelopeCount(votingId);
          }
          else
          {
            var directory = new DirectoryInfo(this.offlinePath);
            this.envelopeCount = directory.GetFiles(Files.EnvelopeFilePattern).Count();
          }

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
          this.threadProgress = new Dictionary<int, double>();
          workers.ForEach(worker => this.threadProgress.Add(worker.ManagedThreadId, 0d));
          workers.ForEach(worker => worker.Start());

          while (this.verifiedEnvelopes < this.envelopeCount)
          {
            lock (this.threadProgress)
            {
              SubText = string.Format(LibraryResources.ClientGetResultFetchEnvelopesOf, this.verifiedEnvelopes, this.envelopeCount);
              SubProgress = 0.2d / (double)this.envelopeCount * (double)this.fetchedEnvelopes +
                            0.8d / (double)this.envelopeCount * ((double)this.verifiedEnvelopes + this.threadProgress.Values.Sum());
            }

            Thread.Sleep(100);
          }

          fetcher.Join();
          this.workerRun = false;
          workers.ForEach(worker => worker.Join());

          Progress = 0.8d;
          Text = LibraryResources.ClientGetResultFetchPartialDeciphers;
          SubText = string.Format(LibraryResources.ClientGetResultFetchPartialDeciphersOf, 0, parameters.AuthorityCount);
          SubProgress = 0d;

          fetcher.Join();
          this.workerRun = false;
          workers.ForEach(worker => worker.Join());

          for (int authorityIndex = 1; authorityIndex < parameters.AuthorityCount + 1; authorityIndex++)
          {
            Signed<PartialDecipherList> partialDecipher = null;

            if (this.offlinePath.IsNullOrEmpty())
            {
              partialDecipher = client.proxy.FetchPartialDecipher(votingId, authorityIndex);
            }
            else
            {
              string partialDecipherFileName = Path.Combine(this.offlinePath, string.Format(Files.PartialDecipherFileString, authorityIndex));

              if (File.Exists(partialDecipherFileName))
              {
                partialDecipher = Serializable.Load<Signed<PartialDecipherList>>(partialDecipherFileName);
              }
            }

            if (partialDecipher != null)
            {
              client.voterEntity.TallyAddPartialDecipher(partialDecipher);
            }

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

          Signed<Envelope> envelope = null;

          if (this.offlinePath.IsNullOrEmpty())
          {
            envelope = this.client.proxy.FetchEnvelope(votingId, envelopeIndex);
          }
          else
          {
            string fileName = Path.Combine(this.offlinePath, string.Format(Files.EnvelopeFileString, envelopeIndex));
            envelope = Serializable.Load<Signed<Envelope>>(fileName);
          }

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
            this.client.voterEntity.TallyAdd(indexedSignedEnvelope.First, indexedSignedEnvelope.Second, new Progress(TallyProgressHandler));

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

            lock (this.threadProgress)
            {
              this.threadProgress[Thread.CurrentThread.ManagedThreadId] = 0d;
              Interlocked.Increment(ref this.verifiedEnvelopes);
            }
          }
        }
      }

      /// <summary>
      /// Callback that reports on the progress of the tallying.
      /// </summary>
      /// <param name="value">Amount of work done.</param>
      private void TallyProgressHandler(double value)
      {
        lock (this.threadProgress)
        {
          this.threadProgress[Thread.CurrentThread.ManagedThreadId] = value;
        }
      }
    }
  }
}
