﻿/*
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
  /// RPC request to list voting ids.
  /// </summary>
  [SerializeObject("RPC request to list voting ids.")]
  [RpcRequest("Fetches the ids of all votings.")]
  [RpcOutput("List of ids of votings.")]
  public class ListVotingIdsRequest : RpcRequest<VotingRpcServer, ListVotingIdsResponse>
  {
    public ListVotingIdsRequest(
      Guid requestId)
      : base(requestId)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public ListVotingIdsRequest(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override ListVotingIdsResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      return new ListVotingIdsResponse(RequestId, server.FetchVotingIds());
    }
  }
}
