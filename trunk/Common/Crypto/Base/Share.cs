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
  /// Share from one authority given to another.
  /// </summary>
  public class Share : Serializable
  {
    /// <summary>
    /// Value of share.
    /// </summary>
    public BigInt Value { get; private set; }

    /// <summary>
    /// Index of issuing authority.
    /// </summary>
    public int SourceAuthorityIndex { get; private set; }

    /// <summary>
    /// Index of receiving authority.
    /// </summary>
    public int DestinationAuthorityIndex { get; private set; }

    public Share(BigInt value, int sourceAuthorityIndex, int destinationAuthorityIndex)
    {
      Value = value;
      SourceAuthorityIndex = sourceAuthorityIndex;
      DestinationAuthorityIndex = destinationAuthorityIndex;
    }

    public Share(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(SourceAuthorityIndex);
      context.Write(DestinationAuthorityIndex);
      context.Write(Value);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      SourceAuthorityIndex = context.ReadInt32();
      DestinationAuthorityIndex = context.ReadInt32();
      Value = context.ReadBigInt();
    }
  }
}
