
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
  public class VotingStatusRequest : RpcRequest<VotingRpcServer, VotingStatusResponse>
  {
    private int votingId;

    public VotingStatusRequest(
      Guid requestId, 
      int votingId)
      : base(requestId)
    {
      this.votingId = votingId;
    }

    public VotingStatusRequest(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.votingId);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.votingId = context.ReadInt32();
    }

    protected override VotingStatusResponse Execute(VotingRpcServer server)
    {
      VotingServerEntity voting = server.GetVoting(this.votingId);

      return new VotingStatusResponse(RequestId, voting.Status);
    }
  }
}
