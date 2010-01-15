
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

namespace Pirate.PiVote.Crypto
{
  public class AllSharesContainer : Serializable
  {
    public int VotingId { get; private set; }

    public List<SignedContainer<ShareContainer>> Shares { get; private set; }

    public AllSharesContainer(int votingId, IEnumerable<SignedContainer<ShareContainer>> shares)
    {
      VotingId = votingId;
      Shares = new List<SignedContainer<ShareContainer>>(shares);
    }

    public AllSharesContainer(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingId);
      context.WriteList(Shares);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadInt32();
      Shares = context.ReadObjectList<SignedContainer<ShareContainer>>();
    }
  }
}
