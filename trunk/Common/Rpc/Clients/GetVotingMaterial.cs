
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
    /// Callback for the vote operation.
    /// </summary>
    /// <param name="voteReceipt">Vote receipt signed by the server.</param>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void GetVotingMaterialCallBack(VotingMaterial votingMaterial, Exception exception);

    /// <summary>
    /// Vote cast operation.
    /// </summary>
    private class GetVotingMaterialOperation : Operation
    {
      /// <summary>
      /// Id of the voting.
      /// </summary>
      private Guid votingId;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private GetVotingMaterialCallBack callBack;

      /// <summary>
      /// Client for update
      /// </summary>
      private VotingClient client;

      /// <summary>
      /// Create a new vote cast opeation.
      /// </summary>
      /// <param name="votingId">Id of the voting.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public GetVotingMaterialOperation(Guid votingId, GetVotingMaterialCallBack callBack)
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
          this.client = client;

          Text = LibraryResources.ClientVoteFetchMaterial;
          Progress = 0d;
          SubText = string.Empty;
          SubProgress = 0d;

          var material = client.proxy.FetchVotingMaterial(this.votingId);

          Progress = 1d;
          SubProgress = 1d;

          this.callBack(material, null);
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
