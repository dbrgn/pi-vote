
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
    /// Callback for the vote operation.
    /// </summary>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void VoteCallBack(Exception exception);

    /// <summary>
    /// Vote cast operation.
    /// </summary>
    private class VoteOperation : Operation
    {
      /// <summary>
      /// Id of the voting.
      /// </summary>
      private Guid votingId;

      /// <summary>
      /// Selected options.
      /// </summary>
      private IEnumerable<bool> vota;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private VoteCallBack callBack;

      /// <summary>
      /// Create a new vote cast opeation.
      /// </summary>
      /// <param name="votingId">Id of the voting.</param>
      /// <param name="optionIndex">Selected options.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public VoteOperation(Guid votingId, IEnumerable<bool> vota, VoteCallBack callBack)
      {
        this.votingId = votingId;
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
          Text = "Casting vote";
          Progress = 0d;
          SubText = "Fetching voting material";
          SubProgress = 0d;

          var material = client.proxy.FetchVotingMaterial(this.votingId);
          if (!material.Valid(client.voterEntity.CertificateStorage))
            throw new InvalidOperationException("Voting material not valid.");
          var parameters = material.Parameters.Value;

          int[] vota = this.vota.Select(votum => votum ? 1 : 0).ToArray();

          Progress = 0.3d;
          SubText = "Calculating vote";
          SubProgress = 0d;

          var envelope = client.voterEntity.Vote(material, vota);

          Progress = 0.7d;
          SubText = "Pushing vote";
          SubProgress = 0d;

          client.proxy.PushEnvelope(this.votingId, envelope);

          Progress = 1d;

          this.callBack(null);
        }
        catch (Exception exception)
        {
          this.callBack(exception);
        }
      }
    }
  }
}
