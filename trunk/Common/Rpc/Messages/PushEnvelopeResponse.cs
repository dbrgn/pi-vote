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
  /// RPC response to push of envelope.
  /// </summary>
  [SerializeObject("RPC response to push of envelope.")]
  public class PushEnvelopeResponse : RpcResponse
  {
    /// <summary>
    /// Receipt of the cast vote or null in case of exception.
    /// </summary>
    [SerializeField(0, "Receipt of the cast vote or null in case of exception.")]
    public Signed<VoteReceipt> VoteReceipt { get; private set; }

    public PushEnvelopeResponse(Guid requestId, Signed<VoteReceipt> voteReceipt)
      : base(requestId)
    {
      VoteReceipt = voteReceipt;
    }

    /// <summary>
    /// Create a failure response to request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception that occured when executing the request.</param>
    public PushEnvelopeResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public PushEnvelopeResponse(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VoteReceipt);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VoteReceipt = context.ReadObject<Signed<VoteReceipt>>();
    }
  }
}
