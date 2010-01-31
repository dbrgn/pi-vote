
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
  /// Envelope list for the authorities.
  /// </summary>
  /// <remarks>
  /// Used by authorities to generate partial deciphers.
  /// </remarks>
  public class AuthorityEnvelopeList : Serializable
  {
    /// <summary>
    /// Id of the voting procedure.
    /// </summary>
    public int VotingId { get; private set; }

    /// <summary>
    /// List of all cast envelopes.
    /// </summary>
    /// <remarks>
    /// Each envelopes contain one ballot.
    /// </remarks>
    public List<Signed<Envelope>> Envelopes { get; private set; }

    /// <summary>
    /// The voting material that was handed out to the voters.
    /// </summary>
    /// <remarks>
    /// Needed to compute the public key.
    /// </remarks>
    public VotingMaterial VotingMaterial { get; private set; }

    /// <summary>
    /// Create a new envelope list for authorities.
    /// </summary>
    /// <param name="votingId">Id of the voting procedure.</param>
    /// <param name="envelopes">List of all cast envelopes.</param>
    /// <param name="votingMaterial">The voting material.</param>
    public AuthorityEnvelopeList(int votingId, IEnumerable<Signed<Envelope>> envelopes, VotingMaterial votingMaterial)
    {
      if (envelopes == null)
        throw new ArgumentNullException("shareParts");
      if (votingMaterial == null)
        throw new ArgumentNullException("votingMaterial");

      VotingId = votingId;
      Envelopes = new List<Signed<Envelope>>(envelopes);
      VotingMaterial = votingMaterial;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public AuthorityEnvelopeList(DeserializeContext context)
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
      context.WriteList(Envelopes);
      context.Write(VotingMaterial);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadInt32();
      Envelopes = context.ReadObjectList<Signed<Envelope>>();
      VotingMaterial = context.ReadObject<VotingMaterial>();
    }
  }
}
