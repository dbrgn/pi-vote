
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
  public class VotingContainer : Serializable
  {
    public int VotingId { get; private set; }

    public List<SignedContainer<BallotContainer>> Ballots { get; private set; }

    public List<SignedContainer<PartialDeciphersContainer>> PartialDeciphers { get; private set; }

    public VotingContainer(int votingId, 
      IEnumerable<SignedContainer<BallotContainer>> ballots,
      IEnumerable<SignedContainer<PartialDeciphersContainer>> partialDeciphers)
    {
      VotingId = votingId;
      Ballots = new List<SignedContainer<BallotContainer>>(ballots);
      PartialDeciphers = new List<SignedContainer<PartialDeciphersContainer>>(partialDeciphers);
    }

    public VotingContainer(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingId);
      context.WriteList(Ballots);
      context.WriteList(PartialDeciphers);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadInt32();
      Ballots = context.ReadObjectList<SignedContainer<BallotContainer>>();
      PartialDeciphers = context.ReadObjectList<SignedContainer<PartialDeciphersContainer>>();
    }
  }
}
