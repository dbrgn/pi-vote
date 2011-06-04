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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// RPC server.
  /// </summary>
  public abstract class RpcServer
  {
    public abstract ILogger Logger { get; }

    public abstract IServerConfig ServerConfig { get; }

    /// <summary>
    /// Lets the server do something when it's idle.
    /// </summary>
    public abstract void Idle();

    /// <summary>
    /// Lets the server do something once ine a while.
    /// </summary>
    public abstract void Process();

    /// <summary>
    /// Excecutes a RPC request.
    /// </summary>
    /// <param name="connection">Connection that made the request.</param>
    /// <param name="requestData">Serialized request data.</param>
    /// <returns>Serialized response data</returns>
    public abstract byte[] Execute(TcpRpcConnection connection, byte[] requestData);
  }
}
