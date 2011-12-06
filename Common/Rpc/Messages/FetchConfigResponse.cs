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
  /// RPC response delivering the config.
  /// </summary>
  [SerializeObject("RPC response delivering the config.")]
  public class FetchConfigResponse : RpcResponse
  {
    /// <summary>
    /// Configuration for the client.
    /// </summary>
    [SerializeField(0, "Configuration for the client.")]
    public RemoteConfig Config { get; private set; }

    /// <summary>
    /// List of voting groups on the server.
    /// </summary>
    [SerializeField(1, "List of voting groups on the server.")]
    public List<Group> Groups { get; private set; }

    public FetchConfigResponse(Guid requestId, IRemoteConfig config, List<Group> groups)
      : base(requestId)
    {
      Config = new RemoteConfig(config);
      Groups = groups;
    }

    /// <summary>
    /// Create a failure response to request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception that occured when executing the request.</param>
    public FetchConfigResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchConfigResponse(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Config);
      context.WriteList(Groups);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      Config = context.ReadObject<RemoteConfig>();
      Groups = context.ReadObjectList<Group>();
    }
  }
}
