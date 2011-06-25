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
  /// RPC request to fetch voting parameters.
  /// </summary>
  [SerializeObject("RPC request to fetch voting parameters.")]
  [RpcRequest("Gets the parameters for a voting together with the index of an authority in a voting. This is used by the authorities.")]
  public class FetchParametersRequest : RpcRequest<VotingRpcServer, FetchParametersResponse>
  {
    /// <summary>
    /// Id of the voting.
    /// </summary>
    [SerializeField(0, "Id of the voting.")]
    private Guid votingId;

    /// <summary>
    /// Certificate of the authorities.
    /// </summary>
    [SerializeField(1, "Certificate of the authorities.")]
    private AuthorityCertificate certificate;

    /// <summary>
    /// Creates a new request to fetch voting parameters.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="certificate">Certificate of the authority.</param>
    public FetchParametersRequest(
      Guid requestId,
      Guid votingId,
      AuthorityCertificate certificate)
      : base(requestId)
    {
      this.votingId = votingId;
      this.certificate = certificate;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public FetchParametersRequest(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.votingId);
      context.Write(this.certificate);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      this.votingId = context.ReadGuid();
      this.certificate = context.ReadObject<AuthorityCertificate>();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override FetchParametersResponse Execute(IRpcConnection connection, VotingRpcServer server)
    {
      VotingServerEntity voting = server.GetVoting(this.votingId);

      return new FetchParametersResponse(RequestId, voting.GetAuthorityIndex(this.certificate), voting.SignedParameters);
    }
  }
}
