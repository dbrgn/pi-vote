﻿/*
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
  /// <summary>
  /// Non-interactive zero knowledge proof that a vote is in range 0-1.
  /// </summary>
  public class RangeProof
  {
    /// <summary>
    /// Whitness for votum equals 0.
    /// </summary>
    public BigInt T0 { get; private set; }

    /// <summary>
    /// Whitness for votum equals 1.
    /// </summary>
    public BigInt T1 { get; private set; }

    /// <summary>
    /// Or-Challange
    /// </summary>
    public int C { get; private set; }

    /// <summary>
    /// Challange for vote equals 0.
    /// </summary>
    public int C0 { get; private set; }

    /// <summary>
    /// Challange for vote equals 1.
    /// </summary>
    public int C1 { get; private set; }

    /// <summary>
    /// Response for vote equals 0.
    /// </summary>
    public BigInt S0 { get; private set; }

    /// <summary>
    /// Response for vote equals 1.
    /// </summary>
    public BigInt S1 { get; private set; }

    /// <summary>
    /// Creates a new range proof.
    /// </summary>
    /// <param name="votum">The votum. Must be 0 or 1.</param>
    /// <param name="r">The randomness used to encrypt the vote.</param>
    /// <param name="vote">The vote for which to generate a proof.</param>
    /// <param name="publicKey">The public key with which the vote was encrypted.</param>
    /// <param name="parameters">Cryptographic Parameters.</param>
    public RangeProof(int votum, BigInt r, Vote vote, BigInt publicKey, Parameters parameters)
    {
      BigInt r0 = parameters.Random();
      BigInt r1 = parameters.Random();

      T0 = publicKey.PowerMod(r0, parameters.P);
      T1 = publicKey.PowerMod(r1, parameters.P);

      SHA512Managed sha512 = new SHA512Managed();
      byte[] hash = sha512.ComputeHash(T0.ToByteArray().Concat(T1.ToByteArray()));
      C = ((hash[0] & 1) == 1) ? 1 : 0;

      switch (votum)
      {
        case 0:
          C1 = 0;
          S1 = r1;
          C0 = C - C1;
          S0 = r0 + r * C0;
          break;
        case 1:
          C0 = 0;
          S0 = r0;
          C1 = C - C0;
          S1 = r1 + r * C1;
          break;
        default:
          throw new ArgumentException("Bad votum.");
      }
    }

    /// <summary>
    /// Verifies a range proof.
    /// </summary>
    /// <param name="vote">The vote for which to verify the proof.</param>
    /// <param name="publicKey">Public key against which to verify the proof.</param>
    /// <param name="parameters">Cryptographic Parameters.</param>
    /// <returns>Wether the proof is valid.</returns>
    public bool Verify(Vote vote, BigInt publicKey, Parameters parameters)
    {
      SHA512Managed sha512 = new SHA512Managed();
      byte[] hash = sha512.ComputeHash(T0.ToByteArray().Concat(T1.ToByteArray()));
      if (C != (((hash[0] & 1) == 1) ? 1 : 0))
        return false;

      if (C0 + C1 != C)
        return false;

      if (publicKey.PowerMod(S0, parameters.P) !=
        (T0 * vote.Ciphertext.DivideMod(parameters.F.PowerMod(0, parameters.P), parameters.P).PowerMod(C0, parameters.P)).Mod(parameters.P))
        return false;

      if (publicKey.PowerMod(S1, parameters.P) !=
        (T1 * vote.Ciphertext.DivideMod(parameters.F.PowerMod(1, parameters.P), parameters.P).PowerMod(C1, parameters.P)).Mod(parameters.P))
        return false;

      return true;
    }
  }
}
