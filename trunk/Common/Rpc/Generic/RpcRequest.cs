
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
  /// Request message to the RPC server.
  /// </summary>
  /// <typeparam name="TRpcServer">Type of RPC server.</typeparam>
  public abstract class RpcRequest<TRpcServer> : RpcMessage
    where TRpcServer : RpcServer
  {
    /// <summary>
    /// Creates a new request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    public RpcRequest(Guid requestId)
      : base(requestId)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public RpcRequest(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
    }

    public abstract RpcResponse TryExecute(TRpcServer server);
  }

  /// <summary>
  /// Request message to the RPC server.
  /// </summary>
  /// <typeparam name="TRpcServer">Type of RPC server.</typeparam>
  /// <typeparam name="TResponse">Type of RPC response.</typeparam>
  public abstract class RpcRequest<TRpcServer, TResponse> : RpcRequest<TRpcServer>
    where TRpcServer : RpcServer
    where TResponse : RpcResponse
  {
    /// <summary>
    /// Creates a new request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    public RpcRequest(Guid requestId)
      : base(requestId)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public RpcRequest(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
    }

    /// <summary>
    /// Tries to execute a RPC request.
    /// </summary>
    /// <remarks>
    /// May return a request containing an exception rather than a result.
    /// </remarks>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
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

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected abstract TResponse Execute(TRpcServer server);
  }
}
