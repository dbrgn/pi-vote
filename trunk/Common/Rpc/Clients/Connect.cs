
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
    /// Callback for the connection operation.
    /// </summary>
    /// <param name="exception">Exception or null in case of success.</param>
    public delegate void ConnectCallBack(Exception exception);

    /// <summary>
    /// Connect operation.
    /// </summary>
    private class ConnectOperation : Operation
    {
      /// <summary>
      /// IP address of the server.
      /// </summary>
      private IPAddress serverIpAddress;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private ConnectCallBack callBack;

      /// <summary>
      /// Create a new connect operation.
      /// </summary>
      /// <param name="serverIpAddress">IP address of server.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public ConnectOperation(IPAddress serverIpAddress, ConnectCallBack callBack)
      {
        this.serverIpAddress = serverIpAddress;
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
          Text = LibraryResources.ClientConnect;
          Progress = 0d;
          SubText = string.Empty;
          SubProgress = 0d;

          client.client.Connect(this.serverIpAddress);
          client.proxy = new VotingRpcProxy(client.client);

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
