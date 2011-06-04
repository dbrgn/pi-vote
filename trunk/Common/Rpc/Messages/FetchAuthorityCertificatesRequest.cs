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
  /// RPC request to fetch all valid authority certificates.
  /// </summary>
  [SerializeObject("RPC request to fetch all valid authority certificates.")]
  public class FetchAuthorityCertificatesRequest : RpcRequest<VotingRpcServer, FetchAuthorityCertificatesResponse>
  {
    /// <summary>
    /// Creates a new request to fetch all valid authority certificates.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    public FetchAuthorityCertificatesRequest(
      Guid requestId)
      : base(requestId)
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchAuthorityCertificatesRequest(DeserializeContext context, byte version)
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
    protected override FetchAuthorityCertificatesResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      return new FetchAuthorityCertificatesResponse(RequestId, server.GetValidAuthorityCertificates());
    }
  }
}
