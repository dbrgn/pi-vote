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
  /// Verification value from an authority used to check shares.
  /// </summary>
  public class VerificationValue : Serializable
  {
    /// <summary>
    /// Value used to verify shares.
    /// </summary>
    public BigInt Value { get; private set; }

    public int SourceAuthorityIndex { get; private set; }

    public VerificationValue(BigInt value, int sourceAuthorityIndex)
    {
      Value = value;
      SourceAuthorityIndex = sourceAuthorityIndex;
    }

    public VerificationValue(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(SourceAuthorityIndex);
      context.Write(Value);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      SourceAuthorityIndex = context.ReadInt32();
      Value = context.ReadBigInt();
    }
  }
}
