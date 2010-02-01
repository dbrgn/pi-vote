
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
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Rpc
{
  public class VotingStatusResponse : RpcResponse
  {
    public VotingStatus VotingStatus { get; private set; }

    public VotingStatusResponse(Guid requestId, VotingStatus votingStatus)
      : base(requestId)
    {
      VotingStatus = votingStatus;
    }

    public VotingStatusResponse(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write((int)VotingStatus);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingStatus = (VotingStatus)context.ReadInt32();
    }
  }
}
