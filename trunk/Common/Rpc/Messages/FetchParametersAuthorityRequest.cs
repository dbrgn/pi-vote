
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
  /// <summary>
  /// RPC request to fetch voting parameters.
  /// </summary>
  public class FetchParametersAuthorityRequest : RpcRequest<VotingRpcServer, FetchParametersAuthorityResponse>
  {
    /// <summary>
    /// Id of the voting.
    /// </summary>
    private int votingId;

    /// <summary>
    /// Certificate of the authorities.
    /// </summary>
    private AuthorityCertificate certificate;

    /// <summary>
    /// Creates a new request to fetch voting parameters.
    /// </summary>
    /// <param name="requestId">Id of the request.</param>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="certificate">Certificate of the authority.</param>
    public FetchParametersAuthorityRequest(
      Guid requestId, 
      int votingId,
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
    public FetchParametersAuthorityRequest(DeserializeContext context)
      : base(context)
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
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.votingId = context.ReadInt32();
      this.certificate = context.ReadObject<AuthorityCertificate>();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override FetchParametersAuthorityResponse Execute(VotingRpcServer server)
    {
      VotingServerEntity voting = server.GetVoting(this.votingId);

      return new FetchParametersAuthorityResponse(RequestId, voting.GetAuthorityIndex(this.certificate), voting.Parameters);
    }
  }
}
