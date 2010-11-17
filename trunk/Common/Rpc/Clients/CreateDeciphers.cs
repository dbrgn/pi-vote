
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
    /// Callback for the create deciphers operation.
    /// </summary>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void CreateDeciphersCallBack(VotingDescriptor votingDescriptor, Exception exception);

    /// <summary>
    /// Callback to ask for permission to partially decipher.
    /// </summary>
    /// <param name="validEnvelopeCount">Number of valid envelopes.</param>
    /// <returns>Continue with partially deciphering?</returns>
    public delegate bool AskForPartiallyDecipherCallBack(int validEnvelopeCount);

    /// <summary>
    /// Create decipherss parts.
    /// </summary>
    private class CreateDeciphersOperation : Operation
    {
      /// <summary>
      /// Id of the voting.
      /// </summary>
      private Guid votingId;

      /// <summary>
      /// Filename to save authority data.
      /// </summary>
      private string authorityFileName;

      /// <summary>
      /// Authority's certificate.
      /// </summary>
      private AuthorityCertificate authorityCertificate;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private CreateDeciphersCallBack callBack;

      /// <summary>
      /// Callback to ask for permission to partially decipher.
      /// </summary>
      private AskForPartiallyDecipherCallBack askCallBack;

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
      /// Queue with enveloped to verify and add.
      /// </summary>
      private Queue<Tuple<int, Signed<Envelope>>> envelopeQueue;

      /// <summary>
      /// Continue to run worker threads?
      /// </summary>
      private bool workerRun;

      /// <summary>
      /// Voter client to work with.
      /// </summary>
      private VotingClient client;

      /// <summary>
      /// Create a new vote cast opeation.
      /// </summary>
      /// <param name="votingId">Id of the voting.</param>
      /// <param name="authorityFileName">Filename to save authority data.</param>
      /// <param name="authorityCertificate">Authority's certificate.</param>
      /// <param name="askCallBack">Callback to ask for permission to partially decipher.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public CreateDeciphersOperation(Guid votingId, AuthorityCertificate authorityCertificate, string authorityFileName, AskForPartiallyDecipherCallBack askCallBack, CreateDeciphersCallBack callBack)
      {
        this.votingId = votingId;
        this.authorityFileName = authorityFileName;
        this.authorityCertificate = authorityCertificate;
        this.callBack = callBack;
        this.askCallBack = askCallBack;
      }

      /// <summary>
      /// Execute the operation.
      /// </summary>
      /// <param name="client">Voting client to execute against.</param>
      public override void Execute(VotingClient client)
      {
        try
        {
          Text = LibraryResources.ClientCreateDeciphersLoadAuthority;
          Progress = 0d;
          SubText = string.Empty;
          SubProgress = 0d;

          client.LoadAuthority(this.authorityFileName, this.authorityCertificate);

          Text = LibraryResources.ClientCreateDeciphersFetchMaterial;
          Progress = 0.1d;

          var material = client.proxy.FetchVotingMaterial(this.votingId);
          client.authorityEntity.TallyBegin(material);

          Text = LibraryResources.ClientCreateDeciphersFetchEnvelopeCount;
          Progress = 0.2d;
          this.envelopeCount = client.proxy.FetchEnvelopeCount(votingId);

          Text = LibraryResources.ClientCreateDeciphersFetchEnvelope;
          Progress = 0.2d;

          this.client = client;
          this.envelopeQueue = new Queue<Tuple<int, Signed<Envelope>>>();
          this.workerRun = true;
          Thread fetcher = new Thread(FetchWorker);
          fetcher.Priority = ThreadPriority.Lowest;
          fetcher.Start();
          List<Thread> workers = new List<Thread>();
          Environment.ProcessorCount.Times(() => workers.Add(new Thread(TallyAddWorker)));
          this.threadProgress = new Dictionary<int, double>();
          workers.ForEach(worker => worker.Priority = ThreadPriority.Lowest);
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

          this.workerRun = false;
          workers.ForEach(worker => worker.Join());

          Text = LibraryResources.ClientCreateDeciphersCreatePartialDecipher;
          Progress = 0.7d;

          if (this.askCallBack == null || this.askCallBack(client.authorityEntity.TallyValidEnvelopeCount))
          {
            var decipherList = client.authorityEntity.PartiallyDecipher();

            Text = LibraryResources.ClientCreateDeciphersPushPartialDecipher;
            Progress = 0.8d;

            client.proxy.PushPartailDecipher(this.votingId, decipherList);

            Text = LibraryResources.ClientCreateDeciphersFetchVotingStatus;
            Progress = 0.9d;

            material = client.proxy.FetchVotingMaterial(this.votingId);
            List<Guid> authoritieDone;
            VotingStatus status = client.proxy.FetchVotingStatus(this.votingId, out authoritieDone);
            var votingDescriptor = new VotingDescriptor(material.Parameters.Value, status, authoritieDone, material.CastEnvelopeCount);

            Progress = 1d;

            this.callBack(votingDescriptor, null);
          }
          else
          {
            this.callBack(null, new PiException(ExceptionCode.CanceledByUser, "Operation was canceled by user."));
          }
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

          var envelope = this.client.proxy.FetchEnvelope(votingId, envelopeIndex);

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
            this.client.authorityEntity.TallyAdd(indexedSignedEnvelope.First, indexedSignedEnvelope.Second, new Progress(TallyProgressHandler));

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
