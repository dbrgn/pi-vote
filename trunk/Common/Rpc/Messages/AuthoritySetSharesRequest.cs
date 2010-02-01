
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
  public class AuthoritySetSharesRequest : RpcRequest<VotingRpcServer, AuthoritySetSharesResponse>
  {
    private int votingId;

    private Signed<SharePart> signedSharePart;

    public AuthoritySetSharesRequest(
      Guid requestId,
      int votingId,
      Signed<SharePart> signedSharePart)
      : base(requestId)
    {
      this.votingId = votingId;
      this.signedSharePart = signedSharePart;
    }

    public AuthoritySetSharesRequest(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.votingId);
      context.Write(this.signedSharePart);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.votingId = context.ReadInt32();
      this.signedSharePart = context.ReadObject<Signed<SharePart>>();
    }

    protected override AuthoritySetSharesResponse Execute(VotingRpcServer server)
    {
      var voting = server.GetVoting(this.votingId);
      voting.DepositShares(this.signedSharePart);

      return new AuthoritySetSharesResponse(RequestId);
    }
  }
}
