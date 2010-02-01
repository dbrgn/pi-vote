

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
  public class ListVotingIdsResponse : RpcResponse
  {
    public List<int> VotingIds { get; private set; }

    public ListVotingIdsResponse(Guid requestId, IEnumerable<int> votingIds)
      : base(requestId)
    {
      VotingIds = new List<int>(votingIds);
    }

    public ListVotingIdsResponse(Guid requestId, PiException exception)
      : base(requestId, exception)
    { }

    public ListVotingIdsResponse(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingIds.Count);
      foreach (int votingId in VotingIds)
      {
        context.Write(votingId);
      }
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      int count = context.ReadInt32();
      VotingIds = new List<int>();
      for (int index = 0; index < count; index++)
      {
        VotingIds.Add(context.ReadInt32());
      }
    }
  }
}
