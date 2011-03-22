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
      /// IP address and port of the server.
      /// </summary>
      private IPEndPoint serverEndPoint;

      /// <summary>
      /// Proxy IP address and port.
      /// </summary>
      private IPEndPoint proxyEndPoint;

      /// <summary>
      /// Callback upon completion.
      /// </summary>
      private ConnectCallBack callBack;

      /// <summary>
      /// Create a new connect operation.
      /// </summary>
      /// <param name="serverEndPoint">IP address and port of server.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public ConnectOperation(IPEndPoint serverEndPoint, ConnectCallBack callBack)
      {
        this.serverEndPoint = serverEndPoint;
        this.proxyEndPoint = null;
        this.callBack = callBack;
      }

      /// <summary>
      /// Create a new connect operation.
      /// </summary>
      /// <param name="serverEndPoint">IP address and port of server.</param>
      /// <param name="proxyEndPoint">Proxy IP address and port.</param>
      /// <param name="callBack">Callback upon completion.</param>
      public ConnectOperation(IPEndPoint serverEndPoint, IPEndPoint proxyEndPoint, ConnectCallBack callBack)
      {
        this.serverEndPoint = serverEndPoint;
        this.proxyEndPoint = proxyEndPoint;
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

          if (!client.client.Connected)
          {
            if (proxyEndPoint == null)
            {
              client.Connect(this.serverEndPoint);
            }
            else
            {
              client.Connect(this.serverEndPoint, this.proxyEndPoint);
            }
          }

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
