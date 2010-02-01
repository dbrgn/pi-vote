
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
  public class AuthorityParametersRequest : RpcRequest<VotingRpcServer, AuthorityParametersResponse>
  {
    private int votingId;

    private AuthorityCertificate certificate;

    public AuthorityParametersRequest(
      Guid requestId, 
      int votingId,
      AuthorityCertificate certificate)
      : base(requestId)
    {
      this.votingId = votingId;
      this.certificate = certificate;
    }

    public AuthorityParametersRequest(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.votingId);
      context.Write(this.certificate);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.votingId = context.ReadInt32();
      this.certificate = context.ReadObject<AuthorityCertificate>();
    }

    protected override AuthorityParametersResponse Execute(VotingRpcServer server)
    {
      VotingServerEntity voting = server.GetVoting(this.votingId);

      return new AuthorityParametersResponse(RequestId, voting.GetAuthorityIndex(this.certificate), voting.Parameters);
    }
  }
}
