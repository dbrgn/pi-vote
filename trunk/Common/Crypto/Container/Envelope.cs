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
  [SerializeObject("Container for a ballot.")]
  public class Envelope : Serializable
  {
    /// <summary>
    /// Id of the voting procedure.
    /// </summary>
    [SerializeField(0, "Id of the voting procedure.")]
    public Guid VotingId { get; private set; }

    /// <summary>
    /// Id of the voter.
    /// </summary>
    [SerializeField(1, "Id of the voter.")]
    public Guid VoterId { get; private set; }

    /// <summary>
    /// Casted ballot.
    /// </summary>
    [SerializeField(2, "Casted ballot.")]
    public List<Ballot> Ballots { get; private set; }

    /// <summary>
    /// Date this envelope was formed.
    /// </summary>
    [SerializeField(3, "Date this envelope was formed.")]
    public DateTime Date { get; private set; }

    /// <summary>
    /// Create a new envelope for a ballot.
    /// </summary>
    /// <param name="votingId">Id of the voting procedure.</param>
    /// <param name="voterId">Id of the voter.</param>
    /// <param name="ballots">Casted ballot.</param>
    public Envelope(Guid votingId, Guid voterId, List<Ballot> ballots)
    {
      if (ballots == null)
        throw new ArgumentNullException("ballots");

      VotingId = votingId;
      VoterId = voterId;
      Ballots = ballots;
      Date = DateTime.Now;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public Envelope(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingId);
      context.Write(VoterId);
      context.WriteList(Ballots);
      context.Write(Date);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadGuid();
      VoterId = context.ReadGuid();
      Ballots = context.ReadObjectList<Ballot>();
      Date = context.ReadDateTime();
    }
  }
}
