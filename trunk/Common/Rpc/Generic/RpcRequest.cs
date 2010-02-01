
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
  public abstract class RpcRequest<TRpcServer> : RpcMessage
    where TRpcServer : RpcServer
  {
    public RpcRequest(Guid requestId)
      : base(requestId)
    { }

    public RpcRequest(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
    }

    public abstract RpcResponse TryExecute(TRpcServer server);
  }

  public abstract class RpcRequest<TRpcServer, TResponse> : RpcRequest<TRpcServer>
    where TRpcServer : RpcServer
    where TResponse : RpcResponse
  {
    public RpcRequest(Guid requestId)
      : base(requestId)
    { }

    public RpcRequest(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
    }

    public override RpcResponse TryExecute(TRpcServer server)
    {
      try
      {
        return Execute(server);
      }
      catch (PiException exception)
      {
        return (TResponse)Activator.CreateInstance(typeof(TResponse), new object[] { RequestId, exception });
      }
      catch (Exception exception)
      {
        return (TResponse)Activator.CreateInstance(typeof(TResponse), new object[] { RequestId, new PiException(exception) });
      }
    }

    protected abstract TResponse Execute(TRpcServer server);
  }
}
