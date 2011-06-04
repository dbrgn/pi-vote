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
  public class PushSignCheckRequest : RpcRequest<VotingRpcServer, PushSignCheckResponse>
  {
    private Signed<SignatureRequestSignCheck> signedSignCheck;

    public PushSignCheckRequest(
      Guid requestId,
      Signed<SignatureRequestSignCheck> signedSignCheck)
      : base(requestId)
    {
      this.signedSignCheck = signedSignCheck;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public PushSignCheckRequest(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.signedSignCheck);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      this.signedSignCheck = context.ReadObject<Signed<SignatureRequestSignCheck>>();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="connection">Connection that made the request.</param>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override PushSignCheckResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      server.AddSignatureRequestSignCheck(connection, this.signedSignCheck);

      return new PushSignCheckResponse(RequestId);
    }
  }
}
