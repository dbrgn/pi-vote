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
  /// RPC request to fetch an envelope.
  /// </summary>
  [SerializeObject("RPC request to fetch an envelope.")]
  [RpcRequest("Downloads an envelope from the server by stating the voting and index of the envelope.")]
  [RpcInput("Id of the voting, index of an envelope.")]
  [RpcOutput("Envelope signed by the voter.")]
  public class FetchEnvelopeRequest : RpcRequest<VotingRpcServer, FetchEnvelopeResponse>
  {
    /// <summary>
    /// Id of the voting.
    /// </summary>
    [SerializeField(0, "Id of the voting.")]
    private Guid votingId;

    /// <summary>
    /// Index of the envelope.
    /// </summary>
    [SerializeField(1, "Index of the envelope.")]
    private int envelopeIndex;

    public FetchEnvelopeRequest(
      Guid requestId,
      Guid votingId,
      int envelopeIndex)
      : base(requestId)
    {
      this.votingId = votingId;
      this.envelopeIndex = envelopeIndex;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchEnvelopeRequest(DeserializeContext context, byte version)
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
      context.Write(this.envelopeIndex);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      this.votingId = context.ReadGuid();
      this.envelopeIndex = context.ReadInt32();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override FetchEnvelopeResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      var voting = server.GetVoting(this.votingId);
      var envelope = voting.GetEnvelope(this.envelopeIndex);

      return new FetchEnvelopeResponse(RequestId, envelope);
    }
  }
}
