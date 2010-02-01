

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
  public class GetVotingMaterialResponse : RpcResponse
  {
    public VotingMaterial VotingMaterial { get; private set; }

    public GetVotingMaterialResponse(Guid requestId, VotingMaterial votingMaterial)
      : base(requestId)
    {
      VotingMaterial = votingMaterial;
    }

    public GetVotingMaterialResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    public GetVotingMaterialResponse(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingMaterial);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingMaterial = context.ReadObject<VotingMaterial>();
    }
  }
}
