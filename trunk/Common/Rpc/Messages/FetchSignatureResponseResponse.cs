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
  /// RPC response delivering a signature response.
  /// </summary>
  [SerializeObject("RPC response delivering a signature response.")]
  public class FetchSignatureResponseResponse : RpcResponse
  {
    /// <summary>
    /// Status of the signature response.
    /// </summary>
    [SerializeField(0, "Status of the signature response.")]
    public SignatureResponseStatus Status { get; private set; }

    /// <summary>
    /// Signed signature response.
    /// </summary>
    [SerializeField(1, "Signed signature response.")]
    public Signed<SignatureResponse> SignatureResponse { get; private set; }

    public FetchSignatureResponseResponse(Guid requestId, SignatureResponseStatus status, Signed<SignatureResponse> signatureResponse)
      : base(requestId)
    {
      Status = status;
      SignatureResponse = signatureResponse;
    }

    /// <summary>
    /// Create a failure response to request.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="exception">Exception that occured when executing the request.</param>
    public FetchSignatureResponseResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchSignatureResponseResponse(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);

      context.Write((int)Status);
      context.Write(SignatureResponse);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);

      Status = (SignatureResponseStatus)context.ReadInt32();
      SignatureResponse = context.ReadObject<Signed<SignatureResponse>>();
    }
  }
}
