﻿/*
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
  /// RPC request to get the list of sign checks on a certificate.
  /// </summary>
  [SerializeObject("RPC request to get the list of sign checks on a certificate.")]
  [RpcRequest("Download the list of sign checks for a certificate.")]
  [RpcInput("Id of the certificate.")]
  [RpcOutput("List of signature request sign checks signed by the web server.")]
  public class FetchSignCheckListRequest : RpcRequest<VotingRpcServer, FetchSignCheckListResponse>
  {
    /// <summary>
    /// Id of the certificate.
    /// </summary>
    [SerializeField(0, "Id of the certificate.")]
    private Guid certificateId;

    public FetchSignCheckListRequest(
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
    public FetchSignCheckListRequest(DeserializeContext context, byte version)
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
    protected override FetchSignCheckListResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      return new FetchSignCheckListResponse(
        RequestId, 
        server.GetSignatureRequestSignChecks(this.certificateId),
        server.GetEncryptedSignatureRequest(this.certificateId));
    }
  }
}
