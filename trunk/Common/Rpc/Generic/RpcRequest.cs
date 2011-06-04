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
  /// Request message to the RPC server.
  /// </summary>
  /// <typeparam name="TRpcServer">Type of RPC server.</typeparam>
  [SerializeObject("Request message to the RPC server.")]
  [GenericArgument(0, "TRpcServer", "Type of the server.")]
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
    public RpcRequest(DeserializeContext context, byte version)
      : base(context, version)
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
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
    }

    /// <summary>
    /// Tries to execute a RPC request.
    /// </summary>
    /// <remarks>
    /// May return a request containing an exception rather than a result.
    /// </remarks>
    /// <param name="connection">Conenction that made the request.</param>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    public abstract RpcResponse TryExecute(IRpcConnection connection, TRpcServer server);
  }

  /// <summary>
  /// Request message to the RPC server.
  /// </summary>
  /// <typeparam name="TRpcServer">Type of RPC server.</typeparam>
  /// <typeparam name="TResponse">Type of RPC response.</typeparam>
  [SerializeObject("Request message to the RPC server.")]
  [GenericArgument(0, "TRpcServer", "Type of the server.")]
  [GenericArgument(1, "TResponse", "Type of the response.")]
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
    public RpcRequest(DeserializeContext context, byte version)
      : base(context, version)
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
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
    }

    /// <summary>
    /// Tries to execute a RPC request.
    /// </summary>
    /// <remarks>
    /// May return a request containing an exception rather than a result.
    /// </remarks>
    /// <param name="connection">Connection that made the request.</param>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    public override RpcResponse TryExecute(IRpcConnection connection, TRpcServer server)
    {
      try
      {
        server.Logger.Log(LogLevel.Debug, "Connection {0}: Executing request of type {1}.", connection.Id, GetType().FullName); 
        
        RpcResponse response = Execute(connection, server);

        server.Logger.Log(LogLevel.Debug, "Connection {0}: Request of type {1} executed sucessfully.", connection.Id, GetType().FullName); 

        return response;
      }
      catch (PiException exception)
      {
        server.Logger.Log(LogLevel.Info, "Connection {0}: Executing request of type {1} resulted in PiException with message {2}.", connection.Id, GetType().FullName, exception.Message);
        
        return (TResponse)Activator.CreateInstance(typeof(TResponse), new object[] { RequestId, exception });
      }
      catch (Exception exception)
      {
        server.Logger.Log(LogLevel.Error, "Connection {0}: Executing request of type {1} resulted in Exception with message {2} and trace {3}.", connection.Id, GetType().FullName, exception.Message, exception.StackTrace);

        return (TResponse)Activator.CreateInstance(typeof(TResponse), new object[] { RequestId, new PiException(exception) });
      }
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="connection">Connection that made the request.</param>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected abstract TResponse Execute(IRpcConnection connection, TRpcServer server);
  }
}
