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
  /// RPC request to get voting status.
  /// </summary>
  [SerializeObject("RPC request to get voting status.")]
  [RpcRequest("Gets the status of a voting.")]
  [RpcInput("Id of the voting.")]
  [RpcOutput("Status of the voting, list of the authorities which have completed the current phase.")]
  public class VotingStatusRequest : RpcRequest<VotingRpcServer, VotingStatusResponse>
  {
    /// <summary>
    /// Id of the voting.
    /// </summary>
    [SerializeField(0, "Id of the voting.")]
    private Guid votingId;

    public VotingStatusRequest(
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
    public VotingStatusRequest(DeserializeContext context, byte version)
      : base(context, version)
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
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      this.votingId = context.ReadGuid();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="connection">Connection that made the request.</param>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override VotingStatusResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      VotingServerEntity voting = server.GetVoting(this.votingId);

      return new VotingStatusResponse(RequestId, voting.Status, voting.AuthoritiesDone);
    }
  }
}
