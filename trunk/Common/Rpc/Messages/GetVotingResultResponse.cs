

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
  public class GetVotingResultResponse : RpcResponse
  {
    public VotingContainer VotingContainer { get; private set; }

    public GetVotingResultResponse(Guid requestId, VotingContainer votingContainer)
      : base(requestId)
    {
      VotingContainer = votingContainer;
    }

    public GetVotingResultResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    public GetVotingResultResponse(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingContainer);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingContainer = context.ReadObject<VotingContainer>();
    }
  }
}
