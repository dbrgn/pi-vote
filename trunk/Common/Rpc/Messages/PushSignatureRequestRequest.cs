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
  /// RPC request to push of signature request.
  /// </summary>
  [SerializeObject("RPC request to push of signature request.")]
  public class PushSignatureRequestRequest : RpcRequest<VotingRpcServer, PushSignatureRequestResponse>
  {
    /// <summary>
    /// Signature request signed and encrypted for the CA.
    /// </summary>
    [SerializeField(0, "Signature request signed and encrypted for the CA.")]
    private Secure<SignatureRequest> signatureRequest;

    /// <summary>
    /// Signature request signed and encrypted for the server.
    /// </summary>
    [SerializeField(1, "Signature request signed and encrypted for the server.")]
    private Secure<SignatureRequestInfo> signatureRequestInfo;

    /// <summary>
    /// Creates a new signature request request.
    /// </summary>
    /// <param name="requestId">Unique Id of the request.</param>
    /// <param name="signatureRequest">Signature request signed and encrypted for the CA.</param>
    /// <param name="signatureRequestInfo">Signature request Info signed and encrypted for the server.</param>
    public PushSignatureRequestRequest(
      Guid requestId,
      Secure<SignatureRequest> signatureRequest,
      Secure<SignatureRequestInfo> signatureRequestInfo)
      : base(requestId)
    {
      this.signatureRequest = signatureRequest;
      this.signatureRequestInfo = signatureRequestInfo;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public PushSignatureRequestRequest(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.signatureRequest);
      context.Write(this.signatureRequestInfo);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.signatureRequest = context.ReadObject<Secure<SignatureRequest>>();
      this.signatureRequestInfo = context.ReadObject<Secure<SignatureRequestInfo>>();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override PushSignatureRequestResponse Execute(VotingRpcServer server)
    {
      server.SetSignatureRequest(this.signatureRequest, this.signatureRequestInfo);

      return new PushSignatureRequestResponse(RequestId);
    }
  }
}
