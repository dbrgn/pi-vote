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
  /// RPC request to fetch the config.
  /// </summary>
  [SerializeObject("RPC request to fetch the config.")]
  [RpcRequest("Downloads the config and the voting groups from the server. Also tells the server what kind of client is connected. This is the first message as it also contains information about updates.")]
  [RpcInput("Name of the client program, version of the client program.")]
  [RpcOutput("Remote configuration for the client, list of voting groups.")]
  public class FetchConfigRequest : RpcRequest<VotingRpcServer, FetchConfigResponse>
  {
    /// <summary>
    /// Name of the client software.
    /// </summary>
    [SerializeField(0, "Name of the client software.", 1)]
    public string ClientName { get; private set; }

    /// <summary>
    /// Version of the client software.
    /// </summary>
    [SerializeField(1, "Version of the client software.", 1)]
    public string ClientVersion { get; private set; }

    public FetchConfigRequest(
      Guid requestId,
      string clientName,
      string clientVersion)
      : base(requestId)
    {
      ClientName = clientName;
      ClientVersion = clientVersion;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchConfigRequest(DeserializeContext context, byte version)
      : base(context, version)
    { }

    public override byte Version
    {
      get { return 1; }
    }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(ClientName);
      context.Write(ClientVersion);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);

      if (version >= 1)
      {
        ClientName = context.ReadString();
        ClientVersion = context.ReadString();
      }
      else
      {
        ClientName = "Old";
        ClientVersion = "0";
      }
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <param name="signer">Signer of the RPC request.</param>
    /// <returns>Response to the request.</returns>
    protected override FetchConfigResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      server.Logger.Log(LogLevel.Info, "Connection {0}: Client {1} version {2} got config.", connection.Id, ClientName, ClientVersion);

      return new FetchConfigResponse(RequestId, server.RemoteConfig, server.GetGroups());
    }
  }
}
