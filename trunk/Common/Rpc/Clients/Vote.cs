/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
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
    /// Callback for the vote operation.
    /// </summary>
    /// <param name="voteReceipt">Vote receipt signed by the server.</param>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void VoteCallBack(Signed<VoteReceipt> voteReceipt, Exception exception);

    /// <summary>
    /// Vote cast operation.
    /// </summary>
    private class VoteOperation : Operation
    {
      /// <summary>
      /// Material to vote.
      /// </summary>
      private VotingMaterial votingMaterial;

      /// <summary>
      /// Certificate of the voter.
      /// </summary>
      private Certificate voterCertificate;

      /// <summary>
      /// Selected options.
      /// </summary>
      private IEnumerable<IEnumerable<bool>> vota;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private VoteCallBack callBack;

      /// <summary>
      /// Client for update
      /// </summary>
      private VotingClient client;

      /// <summary>
      /// Create a new vote cast opeation.
      /// </summary>
      /// <param name="votingMaterial">Material to vote.</param>
      /// <param name="voterCertificate">Certificate of the voter.</param>
      /// <param name="vota">Selected options.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public VoteOperation(VotingMaterial votingMaterial, Certificate voterCertificate, IEnumerable<IEnumerable<bool>> vota, VoteCallBack callBack)
      {
        this.votingMaterial = votingMaterial;
        this.voterCertificate = voterCertificate;
        this.vota = vota;
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
          this.client = client;

          Text = LibraryResources.ClientVote;
          Progress = 0d;
          SubText = LibraryResources.ClientVoteFetchMaterial;
          SubProgress = 0d;

          if (!votingMaterial.Valid(client.voterEntity.CertificateStorage))
            throw new InvalidOperationException(LibraryResources.ClientVoteMaterialInvalid);
          var parameters = votingMaterial.Parameters.Value;

          IEnumerable<IEnumerable<int>> vota = this.vota.Select(questionVota => questionVota.Select(votum => votum ? 1 : 0));

          Progress = 0.3d;
          SubText = LibraryResources.ClientVoteCalcVote;
          SubProgress = 0d;

          var envelope = client.voterEntity.Vote(this.votingMaterial, this.voterCertificate, vota, VoteProgressHandler);

          Progress = 0.7d;
          SubText = LibraryResources.ClientVotePushVote;
          SubProgress = 0d;

          var voteReceipt = client.proxy.PushEnvelope(parameters.VotingId, envelope);

          if (voteReceipt == null ||
              !voteReceipt.Verify(client.voterEntity.CertificateStorage) ||
              !(voteReceipt.Certificate is ServerCertificate) ||
              voteReceipt.Value.VoterId != envelope.Certificate.Id ||
              voteReceipt.Value.VotingId != parameters.VotingId ||
              !voteReceipt.Value.Verify(envelope))
          {
            throw new PiSecurityException(ExceptionCode.InvalidVoteReceipt, "Invalid vote receipt");
          }

          Progress = 1d;

          this.callBack(voteReceipt, null);
        }
        catch (Exception exception)
        {
          this.callBack(null, exception);
        }
      }

      private void VoteProgressHandler(double value)
      {
        SubProgress = value;
      }
    }
  }
}
