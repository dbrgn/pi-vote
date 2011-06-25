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
  /// RPC request setting the sign check cookie for a notary/authority.
  /// </summary>
  [SerializeObject("RPC request setting the sign check cookie for a notary/authority.")]
  public class PushSignCheckCookieRequest : RpcRequest<VotingRpcServer, PushSignCheckCookieResponse>
  {
    /// <summary>
    /// Signed sign check cookie.
    /// </summary>
    [SerializeField(0, "Signed sign check cookie.")]
    private Signed<SignCheckCookie> cookie;

    public PushSignCheckCookieRequest(
      Guid requestId,
      Signed<SignCheckCookie> cookie)
      : base(requestId)
    {
      this.cookie = cookie;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public PushSignCheckCookieRequest(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.cookie);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      this.cookie = context.ReadObject<Signed<SignCheckCookie>>();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <param name="signer">Signer of the RPC request.</param>
    /// <returns>Response to the request.</returns>
    protected override PushSignCheckCookieResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      return new PushSignCheckCookieResponse(RequestId, server.SetSignCheckCookie(this.cookie));
    }
  }
}
