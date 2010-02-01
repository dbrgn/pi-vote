
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

namespace Pirate.PiVote.Rpc
{
  public class CreateVotingResponse : RpcResponse
  {
    public int VotingId { get; private set; }

    public CreateVotingResponse(Guid requestId, int votingId)
      : base(requestId)
    {
      VotingId = votingId;
    }

    public CreateVotingResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    public CreateVotingResponse(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingId);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadInt32();
    }
  }
}
