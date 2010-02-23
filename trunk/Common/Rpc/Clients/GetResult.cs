
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
  /// Asynchronous client.
  /// </summary>
  public partial class VotingClient
  {
    /// <summary>
    /// Callback for the result get operation.
    /// </summary>
    /// <param name="result">Voting result or null in case of failure.</param>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void GetResultCallBack(VotingResult result, Exception exception);

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
      private Queue<Signed<Envelope>> envelopeQueue;

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
      public GetResultOperation(Guid votingId, GetResultCallBack callBack)
      {
        this.votingId = votingId;
        this.callBack = callBack;
      }

      /// <summary>
      /// Execute the operation.
      /// </summary>
      /// <param name="client">Voter client to execute against.</param>
      public override void Execute(VotingClient client)
      {
        try
        {
          Text = "Getting result";
          Progress = 0d;
          SubText = "Fetching material";
          SubProgress = 0d;

          var material = client.proxy.FetchVotingMaterial(votingId);
          if (!material.Valid(client.voterEntity.CertificateStorage))
            throw new InvalidOperationException("Voting material not valid.");
          var parameters = material.Parameters.Value;

          Progress = 0.1d;
          SubText = "Fetching envelope count";
          SubProgress = 0d;

          this.envelopeCount = client.proxy.FetchEnvelopeCount(votingId);

          Progress = 0.2d;
          SubText = "Fetching and verifying votes";
          SubProgress = 0d;

          client.voterEntity.TallyBegin(material);

          this.client = client;
          this.envelopeQueue = new Queue<Signed<Envelope>>();
          this.workerRun = true;
          Thread fetcher = new Thread(FetchWorker);
          fetcher.Start();
          List<Thread> workers = new List<Thread>();
          Environment.ProcessorCount.Times(() => workers.Add(new Thread(TallyAddWorker)));
          workers.ForEach(worker => worker.Start());

          while (this.verifiedEnvelopes < this.envelopeCount)
          {
            SubProgress = 0.2d / (double)this.envelopeCount * (double)this.fetchedEnvelopes +
                          0.8d / (double)this.envelopeCount * (double)this.verifiedEnvelopes;

            Thread.Sleep(10);
          }

          Progress = 0.8d;
          SubText = "Fetching partial deciphers";
          SubProgress = 0d;

          fetcher.Join();
          this.workerRun = false;
          workers.ForEach(worker => worker.Join());

          for (int authorityIndex = 1; authorityIndex < parameters.AuthorityCount + 1; authorityIndex++)
          {
            client.voterEntity.TallyAddPartialDecipher(client.proxy.FetchPartialDecipher(votingId, authorityIndex));

            SubProgress = 1d / (double)parameters.AuthorityCount * (double)(authorityIndex + 1);
          }

          Progress = 0.9d;
          SubText = "Deciphering result";
          SubProgress = 0d;

          var result = client.voterEntity.TallyResult;

          Progress = 1d;

          this.callBack(result, null);
        }
        catch (Exception exception)
        {
          this.callBack(null, exception);
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
            this.envelopeQueue.Enqueue(envelope);
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
          Signed<Envelope> envelope = null;

          lock (this.envelopeQueue)
          {
            if (this.envelopeQueue.Count > 0)
            {
              envelope = this.envelopeQueue.Dequeue();
            }
          }

          if (envelope == null)
          {
            Thread.Sleep(1);
          }
          else
          {
            this.client.voterEntity.TallyAdd(envelope);
            Interlocked.Increment(ref this.verifiedEnvelopes);
          }
        }
      }
    }
  }
}
