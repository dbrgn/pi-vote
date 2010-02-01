
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
  public class ListVotingIdsRequest : RpcRequest<VotingRpcServer, ListVotingIdsResponse>
  {
    public ListVotingIdsRequest(
      Guid requestId)
      : base(requestId)
    { }

    public ListVotingIdsRequest(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
    }

    protected override ListVotingIdsResponse Execute(VotingRpcServer server)
    {
      return new ListVotingIdsResponse(RequestId, server.GetVotingIds());
    }
  }
}
