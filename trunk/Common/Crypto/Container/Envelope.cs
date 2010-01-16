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
  /// <summary>
  /// Container for a ballot.
  /// </summary>
  public class Envelope : Serializable
  {
    /// <summary>
    /// Id of the voting procedure.
    /// </summary>
    public int VotingId { get; private set; }

    /// <summary>
    /// Id of the voter.
    /// </summary>
    public int VoterId { get; private set; }

    /// <summary>
    /// Full name of the voter.
    /// </summary>
    public string VoterName { get; private set; }

    /// <summary>
    /// Casted ballot.
    /// </summary>
    public Ballot Ballot { get; private set; }

    /// <summary>
    /// Create a new envelope for a ballot.
    /// </summary>
    /// <param name="votingId">Id of the voting procedure.</param>
    /// <param name="voterId">Id of the voter.</param>
    /// <param name="voterName">Full name of the voter.</param>
    /// <param name="ballot">Casted ballot.</param>
    public Envelope(int votingId, int voterId, string voterName, Ballot ballot)
    {
      VotingId = votingId;
      VoterId = voterId;
      VoterName = voterName;
      Ballot = ballot;
    }

    public Envelope(DeserializeContext context)
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
