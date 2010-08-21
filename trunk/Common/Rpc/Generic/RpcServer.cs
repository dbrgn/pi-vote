
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
    /// Excecutes a RPC request.
    /// </summary>
    /// <param name="requestData">Serialized request data.</param>
    /// <returns>Serialized response data</returns>
    public abstract byte[] Execute(byte[] requestData);
  }
}
