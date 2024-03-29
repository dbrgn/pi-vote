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
  /// RPC request to add a certificate storage to the server's data.
  /// </summary>
  /// <remarks>
  /// Used to add new CRLs.
  /// </remarks>
  [SerializeObject("RPC request to add a certificate storage to the server's data.")]
  [RpcRequest("Uploads a certificate storage which will be merge into the server database.")]
  [RpcInput("A certificate storage including CAs and CRLs.")]
  public class PushCertificateStorageRequest : RpcRequest<VotingRpcServer, PushCertificateStorageResponse>
  {
    /// <summary>
    /// Certificate storage to add.
    /// </summary>
    [SerializeField(0, "Certificate storage to add.")]
    private CertificateStorage certificateStorage;

    public PushCertificateStorageRequest(
      Guid requestId,
      CertificateStorage certificateStorage)
      : base(requestId)
    {
      this.certificateStorage = certificateStorage;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public PushCertificateStorageRequest(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.certificateStorage);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      this.certificateStorage = context.ReadObject<CertificateStorage>();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override PushCertificateStorageResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      server.AddCertificateStorage(connection, this.certificateStorage);

      return new PushCertificateStorageResponse(RequestId);
    }
  }
}
