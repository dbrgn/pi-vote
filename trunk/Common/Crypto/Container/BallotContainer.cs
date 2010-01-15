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
  public class BallotContainer : Serializable
  {
    public int VotingId { get; private set; }

    public int VoterId { get; private set; }

    public string VoterName { get; private set; }

    public Ballot Ballot { get; private set; }

    public BallotContainer(int votingId, int voterId, string voterName, Ballot ballot)
    {
      VotingId = votingId;
      VoterId = voterId;
      VoterName = voterName;
      Ballot = ballot;
    }

    public BallotContainer(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingId);
      context.Write(VoterId);
      context.Write(VoterName);
      context.Write(Ballot);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadInt32();
      VoterId = context.ReadInt32();
      VoterName = context.ReadString();
      Ballot = context.ReadObject<Ballot>();
    }
  }
}
