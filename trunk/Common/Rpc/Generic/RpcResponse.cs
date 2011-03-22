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

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// RPC response message.
  /// </summary>
  [SerializeObject("RPC response message.")]
  public class RpcResponse : RpcMessage
  {
    /// <summary>
    /// Exception throw by the RPC call if any.
    /// </summary>
    [SerializeField(0, "Exception throw by the RPC call if any.")]
    public PiException Exception { get; protected set; }

    /// <summary>
    /// Creates a new RPC response.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    public RpcResponse(Guid requestId)
      : base(requestId)
    {
      Exception = null;
    }

    /// <summary>
    /// Creates a new RPC error response from an ecxception.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception to respond with.</param>
    public RpcResponse(Guid requestId, PiException exception)
      : base(requestId)
    {
      Exception = exception;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public RpcResponse(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Exception == null);
      if (Exception != null)
        context.Write(Exception.ToBinary());
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Exception = context.ReadBoolean() ? null : PiException.FromBinary(context.ReadBytes());
    }
  }
}
