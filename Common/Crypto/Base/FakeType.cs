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
  /// What kind of a fake (invalid) proof is to be created?
  /// </summary>
  public enum FakeType
  {
    /// <summary>
    /// A twisted form of Fiat-Shamir heuristic.
    /// </summary>
    BadFiatShamir,

    /// <summary>
    /// The disjunction to be proofed is not really a disjunction.
    /// </summary>
    BadDisjunction,

    /// <summary>
    /// The power mod p isn't really a such.
    /// </summary>
    BadPowerMod
  }
}
