
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
  /// RPC request creates a new voting.
  /// </summary>
  public class CreateVotingRequest : RpcRequest<VotingRpcServer, CreateVotingResponse>
  {
    /// <summary>
    /// Parameters for the new voting.
    /// </summary>
    private Signed<VotingParameters> votingParameters;

    /// <summary>
    /// List of authorities to oversee the voting.
    /// </summary>
    private List<AuthorityCertificate> authorities;

    /// <summary>
    /// Creates a new voting creation request.
    /// </summary>
    /// <param name="requestId">Id of this request.</param>
    /// <param name="votingParameters">Parameters for the new voting.</param>
    /// <param name="authorities">List of authorities to oversee the voting.</param>
    public CreateVotingRequest(
      Guid requestId, 
      Signed<VotingParameters> votingParameters,
      IEnumerable<AuthorityCertificate> authorities)
      : base(requestId)
    {
      this.votingParameters = votingParameters;
      this.authorities = new List<AuthorityCertificate>(authorities);
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public CreateVotingRequest(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.votingParameters);
      context.WriteList(authorities);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.votingParameters = context.ReadObject<Signed<VotingParameters>>();
      this.authorities = context.ReadObjectList<AuthorityCertificate>();
    }

    /// <summary>
    /// Executes a RPC request.
    /// </summary>
    /// <param name="server">Server to execute the request on.</param>
    /// <returns>Response to the request.</returns>
    protected override CreateVotingResponse Execute(VotingRpcServer server)
    {
      server.CreateVoting(this.votingParameters, this.authorities);

      return new CreateVotingResponse(RequestId);
    }
  }
}
