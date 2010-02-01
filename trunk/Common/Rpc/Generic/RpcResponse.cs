
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
  public class RpcResponse : RpcMessage
  {
    public PiException Exception { get; protected set; }

    public RpcResponse(Guid requestId)
      : base(requestId)
    {
      Exception = null;
    }

    public RpcResponse(Guid requestId, PiException exception)
      : base(requestId)
    {
      Exception = exception;
    }

    public RpcResponse(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Exception == null);
      if (Exception != null)
        context.Write(Exception.ToBinary());
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Exception = context.ReadBoolean() ? null : PiException.FromBinary(context.ReadBytes());
    }
  }
}
