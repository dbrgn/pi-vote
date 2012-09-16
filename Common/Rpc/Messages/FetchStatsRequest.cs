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
  /// RPC request to fetch all shares of a voting.
  /// </summary>
  [SerializeObject("RPC request to fetch statistics data.")]
  [RpcRequest("Downloads statistics data.")]
  [RpcInput("Type of statistics data.")]
  [RpcOutput("All share parts in a single container.")]
  public class FetchStatsRequest : RpcRequest<VotingRpcServer, FetchStatsResponse>
  {
    [SerializeField(0, "Type of statistics data requested.")]
    private StatisticsDataType type;

    public FetchStatsRequest(
      Guid requestId,
      StatisticsDataType type)
      : base(requestId)
    {
      this.type = type;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchStatsRequest(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write((int)this.type);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      this.type = (StatisticsDataType)context.ReadInt32();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override FetchStatsResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      return new FetchStatsResponse(RequestId, server.GetStatistics(this.type));
    }
  }
}
