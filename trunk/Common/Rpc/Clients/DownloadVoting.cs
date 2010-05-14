
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
  /// Asynchronous client.
  /// </summary>
  public partial class VotingClient
  {
    /// <summary>
    /// Callback for the result get operation.
    /// </summary>
    /// <param name="result">Voting result or null in case of failure.</param>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void DownloadVotingCallBack(Exception exception);

    /// <summary>
    /// Result get operation.
    /// </summary>
    private class DownloadVotingOperation : Operation
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
      private DownloadVotingCallBack callBack;

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
      /// Number of envelopes in voting.
      /// </summary>
      private int envelopeCount;

      /// <summary>
      /// Create a new result get operation.
      /// </summary>
      /// <param name="votingId">Id of the voting.</param>
      /// <param name="votingId">Path where program data is stored.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public DownloadVotingOperation(Guid votingId, string dataPath, DownloadVotingCallBack callBack)
      {
        this.votingId = votingId;
        this.callBack = callBack;

        this.offlinePath = Path.Combine(dataPath, votingId.ToString());
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

          if (!Directory.Exists(this.offlinePath))
            Directory.CreateDirectory(this.offlinePath);

          var material = client.proxy.FetchVotingMaterial(votingId);
          string materialFileName = Path.Combine(this.offlinePath, Files.VotingMaterialFileName);
          material.Save(materialFileName);

          var parameters = material.Parameters.Value;

          Progress = 0.1d;
          Text = LibraryResources.ClientGetResultFetchEnvelopeCount;

          this.envelopeCount = client.proxy.FetchEnvelopeCount(votingId);

          Progress = 0.2d;
          Text = LibraryResources.ClientGetResultFetchEnvelopes;
          SubText = string.Format(LibraryResources.ClientGetResultFetchEnvelopesOf, 0, this.envelopeCount);
          SubProgress = 0d;

          this.client = client;
          this.workerRun = true;
          Thread fetcher = new Thread(FetchWorker);
          fetcher.Start();

          while (this.fetchedEnvelopes < this.envelopeCount)
          {
            SubText = string.Format(LibraryResources.ClientDownloadVotingFetchEnvelopesOf, this.fetchedEnvelopes, this.envelopeCount);
            SubProgress = 1d / (double)this.envelopeCount * (double)this.fetchedEnvelopes;

            Thread.Sleep(100);
          }

          fetcher.Join();

          Progress = 0.8d;
          Text = LibraryResources.ClientGetResultFetchPartialDeciphers;
          SubText = string.Format(LibraryResources.ClientGetResultFetchPartialDeciphersOf, 0, parameters.AuthorityCount);
          SubProgress = 0d;

          for (int authorityIndex = 1; authorityIndex < parameters.AuthorityCount + 1; authorityIndex++)
          {
            var partialDecipher = client.proxy.FetchPartialDecipher(votingId, authorityIndex);

            if (partialDecipher != null)
            {
              string partialDecipherFileName = Path.Combine(this.offlinePath, string.Format(Files.PartialDecipherFileString, authorityIndex));
              partialDecipher.Save(partialDecipherFileName);
            }

            SubText = string.Format(LibraryResources.ClientGetResultFetchPartialDeciphersOf, authorityIndex, parameters.AuthorityCount);
            SubProgress = 1d / (double)parameters.AuthorityCount * (double)(authorityIndex);
          }

          Progress = 1d;
          SubProgress = 1d;

          this.callBack(null);
        }
        catch (Exception exception)
        {
          this.callBack(exception);
        }
      }

      /// <summary>
      /// Thread that fetches envelopes from server.
      /// </summary>
      private void FetchWorker()
      {
        for (int envelopeIndex = 0; envelopeIndex < this.envelopeCount; envelopeIndex++)
        {
          var envelope = this.client.proxy.FetchEnvelope(votingId, envelopeIndex);
          string envelopeFileName = Path.Combine(this.offlinePath, string.Format(Files.EnvelopeFileString, envelopeIndex));
          envelope.Save(envelopeFileName);

          Interlocked.Increment(ref this.fetchedEnvelopes);
        }
      }
    }
  }
}
