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
using Emil.GMP;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// List of partial deciphers from an authority.
  /// </summary>
  public class PartialDecipherList : Serializable
  {
    /// <summary>
    /// Id of voting procedure.
    /// </summary>
    public int VotingId { get; set; }

    /// <summary>
    /// Index of issuing authority.
    /// </summary>
    public int AuthorityIndex { get; set; }

    /// <summary>
    /// Partial deciphers from authority.
    /// </summary>
    public List<PartialDecipher> PartialDeciphers { get; set; }

    /// <summary>
    /// Create new list of partial deciphers from an authority.
    /// </summary>
    /// <param name="votingId">Id of voting procedure.</param>
    /// <param name="authorityIndex">Index of issuing authority.</param>
    public PartialDecipherList(int votingId, int authorityIndex)
    {
      VotingId = votingId;
      AuthorityIndex = authorityIndex;
      PartialDeciphers = new List<PartialDecipher>();
    }

    public PartialDecipherList(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingId);
      context.Write(AuthorityIndex);
      context.WriteList(PartialDeciphers);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadInt32();
      AuthorityIndex = context.ReadInt32();
      PartialDeciphers = context.ReadObjectList<PartialDecipher>();
    }
  }
}
