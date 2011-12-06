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
    /// Callback for the certificate storage get operation.
    /// </summary>
    /// <param name="remoteConfig">Remote config or null in case of failure.</param>
    /// <param name="groups">Voting groups or null in case of failure.</param>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void GetConfigCallBack(IRemoteConfig remoteConfig, IEnumerable<Group> groups, Exception exception);

    /// <summary>
    /// Certificate storage get operation.
    /// </summary>
    private class GetConfigOperation : Operation
    {
      /// <summary>
      /// Name of the client.
      /// </summary>
      private string clientName;

      /// <summary>
      /// Version of the client.
      /// </summary>
      private string clientVersion;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private GetConfigCallBack callBack;

      /// <summary>
      /// Create a new voting list get operation.
      /// </summary>
      /// <param name="clientName">Name of the client.</param>
      /// <param name="clientVersion">Version of the client.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public GetConfigOperation(string clientName, string clientVersion, GetConfigCallBack callBack)
      {
        this.clientName = clientName;
        this.clientVersion = clientVersion;
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
          Text = LibraryResources.ClientGetCertificateStorage;
          Progress = 0d;
          SubText = string.Empty;
          SubProgress = 0d;

          var config = client.proxy.FetchConfig(this.clientName, this.clientVersion);
          
          this.callBack(config.First, config.Second, null);
        }
        catch (Exception exception)
        {
          this.callBack(null, null, exception);
        }
      }
    }
  }
}
