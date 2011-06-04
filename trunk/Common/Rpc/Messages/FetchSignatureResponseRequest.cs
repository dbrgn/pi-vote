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
  /// RPC request to fetch a signature response.
  /// </summary>
  [SerializeObject("RPC request to fetch a signature response.")]
  public class FetchSignatureResponseRequest : RpcRequest<VotingRpcServer, FetchSignatureResponseResponse>
  {
    /// <summary>
    /// Id of the certificate.
    /// </summary>
    [SerializeField(0, "Id of the certificate.")]
    private Guid certificateId;

    public FetchSignatureResponseRequest(
      Guid requestId,
      Guid certificateId)
      : base(requestId)
    {
      this.certificateId = certificateId;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchSignatureResponseRequest(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.certificateId);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      this.certificateId = context.ReadGuid();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override FetchSignatureResponseResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      Signed<SignatureResponse> signatureResponse = null;
      var status = server.GetSignatureResponseStatus(this.certificateId, out signatureResponse);

      return new FetchSignatureResponseResponse(RequestId, status, signatureResponse);
    }
  }
}
