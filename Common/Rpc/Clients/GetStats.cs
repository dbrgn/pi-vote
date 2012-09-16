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
    public delegate void GetStatsCallBack(string data, Exception exception);

    /// <summary>
    /// Voting list get operation.
    /// </summary>
    private class GetStatsOperation : Operation
    {
      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private GetStatsCallBack callBack;

      private StatisticsDataType type;

      public GetStatsOperation(StatisticsDataType type, GetStatsCallBack callBack)
      {
        this.type = type;
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
          Text = LibraryResources.ClientGetStats;
          Progress = 0d;
          SubText = string.Empty;
          SubProgress = 0d;

          List<VotingDescriptor> votingList = new List<VotingDescriptor>();

          var data = client.proxy.FetchStats(this.type);

          Progress = 1d;
          SubProgress = 0d;

          this.callBack(data, null);
        }
        catch (Exception exception)
        {
          this.callBack(null, exception);
        }
      }
    }
  }
}
