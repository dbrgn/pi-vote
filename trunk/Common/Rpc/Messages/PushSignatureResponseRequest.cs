﻿
/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  public class PushSignatureResponseRequest : RpcRequest<VotingRpcServer, PushSignatureResponseResponse>
  {
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
    public PushSignatureResponseRequest(DeserializeContext context)
      : base(context)
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
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.signatureResponse = context.ReadObject<Signed<SignatureResponse>>();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override PushSignatureResponseResponse Execute(VotingRpcServer server)
    {
      server.SetSignatureResponse(this.signatureResponse);

      return new PushSignatureResponseResponse(RequestId);
    }
  }
}