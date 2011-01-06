
/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
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
    public RpcMessage(DeserializeContext context)
      : base(context)
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
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      RequestId = context.ReadGuid();
    }
  }
}
