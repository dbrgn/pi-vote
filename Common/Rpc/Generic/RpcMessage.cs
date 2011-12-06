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
  /// Message to or from RPC server.
  /// </summary>
  [SerializeObject("Message to or from RPC server.")]
  public abstract class RpcMessage : Serializable
  {
    /// <summary>
    /// Id of the request.
    /// </summary>
    [SerializeField(0, "Id of the request.")]
    public Guid RequestId { get; private set; }

    /// <summary>
    /// Creates a new RPC message.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    public RpcMessage(Guid requestId)
    {
      RequestId = requestId;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public RpcMessage(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(RequestId);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      RequestId = context.ReadGuid();
    }
  }
}
