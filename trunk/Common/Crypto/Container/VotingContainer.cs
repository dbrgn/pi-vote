
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
  /// Full voting.
  /// </summary>
  /// <remarks>
  /// Contains all data needed to tally and verify the voting procedure.
  /// </remarks>
  public class VotingContainer : Serializable
  {
    /// <summary>
    /// Id of the voting procedure.
    /// </summary>
    public int VotingId { get { return Material.VotingId; } }

    /// <summary>
    /// Voting material issued to voters.
    /// </summary>
    public VotingMaterial Material { get; private set; }

    /// <summary>
    /// Voting parameters.
    /// </summary>
    public VotingParameters Parameters { get { return Material.Parameters; } }

    /// <summary>
    /// Envelopes from all voters.
    /// </summary>
    public List<Signed<Envelope>> Emvelopes { get; private set; }

    /// <summary>
    /// Partial deciphers from all authorities.
    /// </summary>
    public List<Signed<PartialDecipherList>> PartialDeciphers { get; private set; }

    public VotingContainer(
      VotingMaterial material, 
      IEnumerable<Signed<Envelope>> envelopes,
      IEnumerable<Signed<PartialDecipherList>> partialDeciphers)
    {
      Material = material;
      Emvelopes = new List<Signed<Envelope>>(envelopes);
      PartialDeciphers = new List<Signed<PartialDecipherList>>(partialDeciphers);
    }

    public VotingContainer(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Material);
      context.WriteList(Emvelopes);
      context.WriteList(PartialDeciphers);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Material = context.ReadObject<VotingMaterial>();
      Emvelopes = context.ReadObjectList<Signed<Envelope>>();
      PartialDeciphers = context.ReadObjectList<Signed<PartialDecipherList>>();
    }
  }
}
