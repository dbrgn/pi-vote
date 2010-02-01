
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
  public class SetPartialDecipherRequest : RpcRequest<VotingRpcServer, SetPartialDecipherResponse>
  {
    private int votingId;

    private Signed<PartialDecipherList> signedPartialDecipherList;

    public SetPartialDecipherRequest(
      Guid requestId,
      int votingId,
      Signed<PartialDecipherList> signedPartialDecipherList)
      : base(requestId)
    {
      this.votingId = votingId;
      this.signedPartialDecipherList = signedPartialDecipherList;
    }

    public SetPartialDecipherRequest(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.votingId);
      context.Write(this.signedPartialDecipherList);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.votingId = context.ReadInt32();
      this.signedPartialDecipherList = context.ReadObject<Signed<PartialDecipherList>>();
    }

    protected override SetPartialDecipherResponse Execute(VotingRpcServer server)
    {
      var voting = server.GetVoting(this.votingId);
      voting.DepositPartialDecipher(this.signedPartialDecipherList);

      return new SetPartialDecipherResponse(RequestId);
    }
  }
}
