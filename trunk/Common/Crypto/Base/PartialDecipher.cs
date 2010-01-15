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
  /// A partial decipher of a vote from an authority.
  /// </summary>
  public class PartialDecipher : Serializable
  {
    public int OptionIndex { get; private set; }

    public int AuthorityIndex { get; private set; }

    /// <summary>
    /// Index of the partial decipher group.
    /// </summary>
    /// <remarks>
    /// Used to put the right partial decipher together to a full decipher.
    /// Equals index of the authority NOT in this group..
    /// </remarks>
    public int GroupIndex { get; private set; }

    /// <summary>
    /// Value of the partial decipher.
    /// </summary>
    public BigInt Value { get; private set; }

    /// <summary>
    /// Creates a new partial decipher.
    /// </summary>
    /// <param name="index">Index of the partial decipher group. Equals index of authority not in this group.</param>
    /// <param name="value"></param>
    public PartialDecipher(int groupIndex, int optionIndex, BigInt value)
    {
      GroupIndex = groupIndex;
      OptionIndex = optionIndex;
      Value = value;
    }

    public PartialDecipher(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(OptionIndex);
      context.Write(AuthorityIndex);
      context.Write(GroupIndex);
      context.Write(Value);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      OptionIndex = context.ReadInt32();
      AuthorityIndex = context.ReadInt32();
      GroupIndex = context.ReadInt32();
      Value = context.ReadBigInt();
    }
  }
}
