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
  [SerializeObject("RPC request to fetch all shares of a voting.")]
  public class FetchAllSharesRequest : RpcRequest<VotingRpcServer, FetchAllSharesResponse>
  {
    [SerializeField(0, "Id of the voting.")]
    private Guid votingId;

    public FetchAllSharesRequest(
      Guid requestId,
      Guid votingId)
      : base(requestId)
    {
      this.votingId = votingId;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchAllSharesRequest(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.votingId);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.votingId = context.ReadGuid();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override FetchAllSharesResponse Execute(VotingRpcServer server)
    {
      var voting = server.GetVoting(this.votingId); 

      return new FetchAllSharesResponse(RequestId, voting.GetAllShares());
    }
  }
}
