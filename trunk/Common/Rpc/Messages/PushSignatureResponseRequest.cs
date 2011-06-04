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
  /// RPC request to push signature response.
  /// </summary>
  [SerializeObject("RPC request to push signature response.")]
  public class PushSignatureResponseRequest : RpcRequest<VotingRpcServer, PushSignatureResponseResponse>
  {
    /// <summary>
    /// Signed signature response.
    /// </summary>
    [SerializeField(0, "Signed signature response.")]
    private Signed<SignatureResponse> signatureResponse;

    public PushSignatureResponseRequest(
      Guid requestId,
      Signed<SignatureResponse> signatureResponse)
      : base(requestId)
    {
      this.signatureResponse = signatureResponse;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public PushSignatureResponseRequest(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.signatureResponse);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      this.signatureResponse = context.ReadObject<Signed<SignatureResponse>>();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="connection">Connection that made the request.</param>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override PushSignatureResponseResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      server.SetSignatureResponse(connection, this.signatureResponse);

      return new PushSignatureResponseResponse(RequestId);
    }
  }
}
