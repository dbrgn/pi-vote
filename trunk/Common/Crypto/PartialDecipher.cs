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

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// A partial decipher of a vote from an authority.
  /// </summary>
  public class PartialDecipher
  {
    /// <summary>
    /// Index of the partial decipher group.
    /// </summary>
    /// <remarks>
    /// Used to put the right partial decipher together to a full decipher.
    /// Equals index of the authority NOT in this group..
    /// </remarks>
    public int Index { get; private set; }

    /// <summary>
    /// Value of the partial decipher.
    /// </summary>
    public BigInt Value { get; private set; }

    /// <summary>
    /// Creates a new partial decipher.
    /// </summary>
    /// <param name="index">Index of the partial decipher group. Equals index of authority not in this group.</param>
    /// <param name="value"></param>
    public PartialDecipher(int index, BigInt value)
    {
      Index = index;
      Value = value;
    }
  }
}
