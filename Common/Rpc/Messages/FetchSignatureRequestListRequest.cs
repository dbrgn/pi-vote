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
  /// RPC request to fetch list signature requests.
  /// </summary>
  [SerializeObject("RPC request to fetch list signature requests.")]
  [RpcRequest("Downloads a list of ids of the pending signature requests. This is done by an administrator when downloading signature requests.")]
  [RpcOutput("List of guid of the pending signature requests.")]
  public class FetchSignatureRequestListRequest : RpcRequest<VotingRpcServer, FetchSignatureRequestListResponse>
  {
    public FetchSignatureRequestListRequest(
      Guid requestId)
      : base(requestId)
    {
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchSignatureRequestListRequest(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override FetchSignatureRequestListResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      var signatureRequestList = server.GetSignatureRequestList();

      return new FetchSignatureRequestListResponse(RequestId, signatureRequestList);
    }
  }
}
