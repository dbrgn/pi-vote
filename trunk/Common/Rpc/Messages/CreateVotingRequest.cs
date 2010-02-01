
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
  public class CreateVotingRequest : RpcRequest<VotingRpcServer, CreateVotingResponse>
  {
    private VotingParameters votingParameters;

    private List<AuthorityCertificate> authorities;

    public CreateVotingRequest(
      Guid requestId, 
      VotingParameters votingParameters,
      IEnumerable<AuthorityCertificate> authorities)
      : base(requestId)
    {
      this.votingParameters = votingParameters;
      this.authorities = new List<AuthorityCertificate>(authorities);
    }

    public CreateVotingRequest(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.votingParameters);
      context.WriteList(authorities);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.votingParameters = context.ReadObject<VotingParameters>();
      this.authorities = context.ReadObjectList<AuthorityCertificate>();
    }

    protected override CreateVotingResponse Execute(VotingRpcServer server)
    {
      int votingId = server.AddVoting(this.votingParameters, this.authorities);

      return new CreateVotingResponse(RequestId, votingId);
    }
  }
}
