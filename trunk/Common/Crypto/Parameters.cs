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
using System.Security.Cryptography;
using Emil.GMP;

namespace Pirate.PiVote.Crypto
{
  public class Parameters
  {
    /// <summary>
    /// Prime
    /// </summary>
    public BigInt Q { get; set; }

    /// <summary>
    /// Safe Prime
    /// P = 2 * Q + 1
    /// </summary>
    public BigInt P { get; set; }

    /// <summary>
    /// Order Q element of Zp*
    /// G = a ^ Q mod P where a random
    /// </summary>
    public BigInt G { get; set; }

    /// <summary>
    /// element of <G>
    /// F = G ^ b mod P where b random
    /// </summary>
    public BigInt F { get; set; }

    public int Thereshold { get; set; }

    public int AuthorityCount { get; set; }

    public Parameters(BigInt prime, BigInt safePrime, int thereshold, int authorityCount)
    {
      Q = prime;
      P = safePrime;

      BigInt r0 = Random();
      BigInt r1 = Random();

      G = r0.PowerMod(Q, P);
      F = G.PowerMod(r1, P);

      Thereshold = thereshold;
      AuthorityCount = authorityCount;
    }

    public BigInt Random()
    {
      byte[] data = new byte[P.BitLength / 8 + 1];
      RandomNumberGenerator.Create().GetBytes(data);
      return new BigInt(data).Mod(P);
    }
  }
}
