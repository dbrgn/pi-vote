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
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// RPC response delivering the number of envelopes in the voting.
  /// </summary>
  [SerializeObject("RPC response delivering the number of envelopes in the voting.")]
  public class FetchEnvelopeCountResponse : RpcResponse
  {
    /// <summary>
    /// Number of envelopes in the voting.
    /// </summary>
    [SerializeField(0, "Number of envelopes in the voting.")]
    public int EnvelopeCount { get; private set; }

    public FetchEnvelopeCountResponse(Guid requestId, int envelopeCount)
      : base(requestId)
    {
      EnvelopeCount = envelopeCount;
    }

    /// <summary>
    /// Create a failure response to request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception that occured when executing the request.</param>
    public FetchEnvelopeCountResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchEnvelopeCountResponse(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(EnvelopeCount);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      EnvelopeCount = context.ReadInt32();
    }
  }
}
