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
    /// Callback for the get votings operation.
    /// </summary>
    /// <param name="votings">Voting containers for all the votings.</param>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void GetVotingsCallBack(IEnumerable<VotingContainer> votings, Exception exception);

    /// <summary>
    /// Get votings operation.
    /// </summary>
    private class GetVotingsOperation : Operation
    {
      /// <summary>
      /// Ids of the voting or null for all votings.
      /// </summary>
      private IEnumerable<Guid> votingIds;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private GetVotingsCallBack callBack;

      /// <summary>
      /// Client for update
      /// </summary>
      private VotingClient client;

      /// <summary>
      /// Create a new vote cast opeation.
      /// </summary>
      /// <param name="votingId">Ids of the votings or null for all votings.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public GetVotingsOperation(List<Guid> votingIds, GetVotingsCallBack callBack)
      {
        this.votingIds = votingIds;
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

          var containers = client.proxy.FetchVotings(this.votingIds);

          Progress = 1d;
          SubProgress = 1d;

          this.callBack(containers, null);
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
