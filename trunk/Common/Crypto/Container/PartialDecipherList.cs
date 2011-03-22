/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emil.GMP;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// List of partial deciphers from an authority.
  /// </summary>
  [SerializeObject("List of partial deciphers from an authority.")]
  public class PartialDecipherList : Serializable
  {
    /// <summary>
    /// Id of voting procedure.
    /// </summary>
    [SerializeField(0, "Id of voting procedure.")]
    public Guid VotingId { get; private set; }

    /// <summary>
    /// Index of issuing authority.
    /// </summary>
    [SerializeField(1, "Index of issuing authority.")]
    public int AuthorityIndex { get; private set; }

    /// <summary>
    /// Partial deciphers from authority.
    /// </summary>
    [SerializeField(2, "Partial deciphers from authority.")]
    public List<PartialDecipher> PartialDeciphers { get; private set; }

    /// <summary>
    /// Number of envelopes that where partially deciphered.
    /// </summary>
    [SerializeField(3, "Number of envelopes that where partially deciphered.")]
    public int EnvelopeCount { get; private set; }

    /// <summary>
    /// Hash over all envelopes that where partially deciphered.
    /// </summary>
    [SerializeField(4, "Hash over all envelopes that where partially deciphered.")]
    public byte[] EnvelopeHash { get; private set; }

    /// <summary>
    /// Date at which the partial decipher was created.
    /// </summary>
    [SerializeField(5, "Date at which the partial decipher was created.")]
    public DateTime Date { get; private set; }

    /// <summary>
    /// Create new list of partial deciphers from an authority.
    /// </summary>
    /// <param name="votingId">Id of voting procedure.</param>
    /// <param name="authorityIndex">Index of issuing authority.</param>
    /// <param name="envelopeCount">Total number of envelopes.</param>
    /// <param name="envelopeHash">Hash over all envelopes.</param>
    public PartialDecipherList(Guid votingId, int authorityIndex, int envelopeCount, byte[] envelopeHash)
    {
      VotingId = votingId;
      AuthorityIndex = authorityIndex;
      PartialDeciphers = new List<PartialDecipher>();
      EnvelopeCount = envelopeCount;
      EnvelopeHash = envelopeHash;
      Date = DateTime.Now;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public PartialDecipherList(DeserializeContext context)
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
      context.Write(AuthorityIndex);
      context.WriteList(PartialDeciphers);
      context.Write(EnvelopeCount);
      context.Write(EnvelopeHash);
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
      AuthorityIndex = context.ReadInt32();
      PartialDeciphers = context.ReadObjectList<PartialDecipher>();
      EnvelopeCount = context.ReadInt32();
      EnvelopeHash = context.ReadBytes();
      Date = context.ReadDateTime();
    }
  }
}
