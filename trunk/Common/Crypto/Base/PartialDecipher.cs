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
  /// A partial decipher of a vote from an authority.
  /// </summary>
  [SerializeObject("A partial decipher of a vote from an authority.")]
  public class PartialDecipher : Serializable
  {
    /// <summary>
    /// Index of the question in question.
    /// </summary>
    [SerializeField(0, "Index of the question in question.")]
    public int QuestionIndex { get;private set; }

    /// <summary>
    /// Index of the option in question.
    /// </summary>
    [SerializeField(1, "Index of the option in question.")]
    public int OptionIndex { get; private set; }

    /// <summary>
    /// Index of the deciphering authority.
    /// </summary>
    [SerializeField(2, "Index of the deciphering authority.")]
    public int AuthorityIndex { get; private set; }

    /// <summary>
    /// Index of the partial decipher group.
    /// </summary>
    /// <remarks>
    /// Used to put the right partial decipher together to a full decipher.
    /// Equals index of the authority NOT in this group..
    /// </remarks>
    [SerializeField(3, "Index of the partial decipher group.")]
    public int GroupIndex { get; private set; }

    /// <summary>
    /// Value of the partial decipher.
    /// </summary>
    [SerializeField(4, "Value of the partial decipher.")]
    public BigInt Value { get; private set; }

    /// <summary>
    /// Creates a new partial decipher.
    /// </summary>
    /// <param name="groupIndex">Index of the partial decipher group. Equals index of authority not in this group.</param>
    /// <param name="questionIndex">Index of question in the voting.</param>
    /// <param name="optionIndex">Index of the option in the question.</param>
    /// <param name="value">Value of the partial decipher</param>
    public PartialDecipher(int authorityIndex, int groupIndex, int questionIndex, int optionIndex, BigInt value)
    {
      if (value == null)
        throw new ArgumentNullException("value");

      AuthorityIndex = authorityIndex;
      QuestionIndex = questionIndex;
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
      context.Write(QuestionIndex);
      context.Write(OptionIndex);
      context.Write(AuthorityIndex);
      context.Write(GroupIndex);
      context.Write(Value);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      QuestionIndex = context.ReadInt32();
      OptionIndex = context.ReadInt32();
      AuthorityIndex = context.ReadInt32();
      GroupIndex = context.ReadInt32();
      Value = context.ReadBigInt();
    }
  }
}
