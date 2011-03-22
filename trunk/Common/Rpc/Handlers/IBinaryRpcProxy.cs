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
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// Binary RPC proxy.
  /// </summary>
  /// <remarks>
  /// Implemented by web service client.
  /// </remarks>
  public interface IBinaryRpcProxy
  {
    /// <summary>
    /// Execute a request on an RPC server over some channel.
    /// </summary>
    /// <param name="request">Request as serialized binary data.</param>
    /// <returns>Response as serialized binary data.</returns>
    byte[] Execute(byte[] request);
  }
}
