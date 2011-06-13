﻿/*
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
  public class FetchSignCheckCookieRequest : RpcRequest<VotingRpcServer, FetchSignCheckCookieResponse>
  {
    private Guid id;

    private byte[] code;

    public FetchSignCheckCookieRequest(
      Guid requestId,
      Guid id,
      byte[] code)
      : base(requestId)
    {
      this.id = id;
      this.code = code;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchSignCheckCookieRequest(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.id);
      context.Write(this.code);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      this.id = context.ReadGuid();
      this.code = context.ReadBytes();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <param name="signer">Signer of the RPC request.</param>
    /// <returns>Response to the request.</returns>
    protected override FetchSignCheckCookieResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      return new FetchSignCheckCookieResponse(RequestId, server.GetSignCheckCookie(this.id, this.code));
    }
  }
}