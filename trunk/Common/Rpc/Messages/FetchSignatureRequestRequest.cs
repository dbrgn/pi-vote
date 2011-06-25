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
  /// RPC request to fetch a signature request.
  /// </summary>
  [SerializeObject("RPC request to fetch a signature request.")]
  [RpcRequest("Downloads a list of ids of the pending signature requests. This is done by an administrator when downloading signature requests.")]
  public class FetchSignatureRequestRequest : RpcRequest<VotingRpcServer, FetchSignatureRequestResponse>
  {
    /// <summary>
    /// Id of the signature request.
    /// </summary>
    [SerializeField(0, "Id of the signature request.")]
    private Guid signatureRequestId;

    public FetchSignatureRequestRequest(
      Guid requestId,
      Guid signatureRequestId)
      : base(requestId)
    {
      this.signatureRequestId = signatureRequestId;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchSignatureRequestRequest(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(signatureRequestId);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      signatureRequestId = context.ReadGuid();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override FetchSignatureRequestResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      var signatureRequest = server.GetSignatureRequest(this.signatureRequestId);

      return new FetchSignatureRequestResponse(RequestId, signatureRequest);
    }
  }
}
