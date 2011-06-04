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
  /// RPC request to push share response.
  /// </summary>
  [SerializeObject("RPC request to push share response.")]
  public class PushShareResponseRequest : RpcRequest<VotingRpcServer, PushShareResponseResponse>
  {
    /// <summary>
    /// Id of the voting.
    /// </summary>
    [SerializeField(0, "Id of the voting.")]
    private Guid votingId;

    /// <summary>
    /// Signed share response.
    /// </summary>
    [SerializeField(1, "Signed share response.")]
    private Signed<ShareResponse> signedShareResponse;

    public PushShareResponseRequest(
      Guid requestId,
      Guid votingId,
      Signed<ShareResponse> signedShareResponse)
      : base(requestId)
    {
      this.votingId = votingId;
      this.signedShareResponse = signedShareResponse;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public PushShareResponseRequest(DeserializeContext context, byte version)
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
      context.Write(this.signedShareResponse);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      this.votingId = context.ReadGuid();
      this.signedShareResponse = context.ReadObject<Signed<ShareResponse>>();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override PushShareResponseResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      var voting = server.GetVoting(this.votingId);
      voting.DepositShareResponse(connection, this.signedShareResponse);

      return new PushShareResponseResponse(RequestId);
    }
  }
}
