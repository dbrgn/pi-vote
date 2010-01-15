
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
  public class AllBallotsContainer : Serializable
  {
    public int VotingId { get; private set; }

    public List<SignedContainer<BallotContainer>> Ballots { get; private set; }

    public VotingMaterial VotingMaterial { get; private set; }

    public AllBallotsContainer(int votingId, IEnumerable<SignedContainer<BallotContainer>> ballots, VotingMaterial votingMaterial)
    {
      VotingId = votingId;
      Ballots = new List<SignedContainer<BallotContainer>>(ballots);
      VotingMaterial = votingMaterial;
    }

    public AllBallotsContainer(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingId);
      context.WriteList(Ballots);
      context.Write(VotingMaterial);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadInt32();
      Ballots = context.ReadObjectList<SignedContainer<BallotContainer>>();
      VotingMaterial = context.ReadObject<VotingMaterial>();
    }
  }
}
