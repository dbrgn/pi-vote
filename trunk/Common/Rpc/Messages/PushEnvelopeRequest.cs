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
  /// RPC request to push an envelope.
  /// </summary>
  [SerializeObject("RPC request to push an envelope.")]
  public class PushEnvelopeRequest : RpcRequest<VotingRpcServer, PushEnvelopeResponse>
  {
    /// <summary>
    /// Id of the voting.
    /// </summary>
    [SerializeField(0, "Id of the voting.")]
    private Guid votingId;

    /// <summary>
    /// Signed envelope.
    /// </summary>
    [SerializeField(1, "Signed envelope.")]
    private Signed<Envelope> signedEnvelope;

    public PushEnvelopeRequest(
      Guid requestId,
      Guid votingId,
      Signed<Envelope> signedEnvelope)
      : base(requestId)
    {
      this.votingId = votingId;
      this.signedEnvelope = signedEnvelope;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public PushEnvelopeRequest(DeserializeContext context, byte version)
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
      context.Write(this.signedEnvelope);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      this.votingId = context.ReadGuid();
      this.signedEnvelope = context.ReadObject<Signed<Envelope>>();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override PushEnvelopeResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      var voting = server.GetVoting(this.votingId);
      var voteReceipt = voting.Vote(connection, this.signedEnvelope);

      return new PushEnvelopeResponse(RequestId, voteReceipt);
    }
  }
}
