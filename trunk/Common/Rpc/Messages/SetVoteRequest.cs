
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
  public class SetVoteRequest : RpcRequest<VotingRpcServer, SetVoteResponse>
  {
    private int votingId;

    private Signed<Envelope> signedEnvelope;

    public SetVoteRequest(
      Guid requestId,
      int votingId,
      Signed<Envelope> signedEnvelope)
      : base(requestId)
    {
      this.votingId = votingId;
      this.signedEnvelope = signedEnvelope;
    }

    public SetVoteRequest(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.votingId);
      context.Write(this.signedEnvelope);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.votingId = context.ReadInt32();
      this.signedEnvelope = context.ReadObject<Signed<Envelope>>();
    }

    protected override SetVoteResponse Execute(VotingRpcServer server)
    {
      var voting = server.GetVoting(this.votingId);
      voting.Vote(this.signedEnvelope);

      return new SetVoteResponse(RequestId);
    }
  }
}
