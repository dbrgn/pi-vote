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
  /// RPC response delivering the signature request.
  /// </summary>
  [SerializeObject("RPC response delivering the signature request.")]
  public class FetchSignatureRequestResponse : RpcResponse
  {
    /// <summary>
    /// Signature request signed and encrypted for the CA.
    /// </summary>
    [SerializeField(0, "Signature request signed and encrypted for the CA.")]
    public Secure<SignatureRequest> SecureSignatureRequest { get; private set; }

    public FetchSignatureRequestResponse(Guid requestId, Secure<SignatureRequest> signatureRequest)
      : base(requestId)
    {
      SecureSignatureRequest = signatureRequest;
    }

    /// <summary>
    /// Create a failure response to request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception that occured when executing the request.</param>
    public FetchSignatureRequestResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchSignatureRequestResponse(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(SecureSignatureRequest);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      SecureSignatureRequest = context.ReadObject<Secure<SignatureRequest>>();
    }
  }
}
