
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
  public class AuthorityParametersResponse : RpcResponse
  {
    public int AuthorityIndex { get; private set; }

    public VotingParameters VotingParameters { get; private set; }

    public AuthorityParametersResponse(Guid requestId, int authorityIndex, VotingParameters votingParameters)
      : base(requestId)
    {
      AuthorityIndex = authorityIndex;
      VotingParameters = votingParameters;
    }

    public AuthorityParametersResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    public AuthorityParametersResponse(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(AuthorityIndex);
      context.Write(VotingParameters);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      AuthorityIndex = context.ReadInt32();
      VotingParameters = context.ReadObject<VotingParameters>();
    }
  }
}
