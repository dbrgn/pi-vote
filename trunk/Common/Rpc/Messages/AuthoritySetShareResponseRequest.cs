
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
  public class AuthoritySetShareResponseRequest : RpcRequest<VotingRpcServer, AuthoritySetShareResponseResponse>
  {
    private int votingId;

    private Signed<ShareResponse> signedShareResponse;

    public AuthoritySetShareResponseRequest(
      Guid requestId,
      int votingId,
      Signed<ShareResponse> signedShareResponse)
      : base(requestId)
    {
      this.votingId = votingId;
      this.signedShareResponse = signedShareResponse;
    }

    public AuthoritySetShareResponseRequest(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.votingId);
      context.Write(this.signedShareResponse);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.votingId = context.ReadInt32();
      this.signedShareResponse = context.ReadObject<Signed<ShareResponse>>();
    }

    protected override AuthoritySetShareResponseResponse Execute(VotingRpcServer server)
    {
      var voting = server.GetVoting(this.votingId);
      voting.DepositShareResponse(this.signedShareResponse);

      return new AuthoritySetShareResponseResponse(RequestId);
    }
  }
}
