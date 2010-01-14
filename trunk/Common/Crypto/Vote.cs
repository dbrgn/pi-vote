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
using Emil.GMP;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Elgamal encrypted vote.
  /// </summary>
  public class Vote
  {
    /// <summary>
    /// Diffie-Hellman halfkey.
    /// </summary>
    public BigInt HalfKey { get; private set; }

    /// <summary>
    /// Ciphertext.
    /// </summary>
    public BigInt Ciphertext { get; private set; }

    public BigInt P { get; private set; }

    /// <summary>
    /// Creates a new encrypted vote.
    /// </summary>
    /// <remarks>
    /// Still need non-interactive zero-knowledge proof of vote being 0 or 1.
    /// Still need non-interactive zero-knowledge proof of sum of votes.
    /// </remarks>
    /// <param name="vote">Actual vote.</param>
    /// <param name="parameters">Cryptographic parameters.</param>
    /// <param name="publicKey">Public key of the authorities.</param>
    public Vote(int vote, Parameters parameters, BigInt publicKey)
    {
      P = parameters.P;
      BigInt r = parameters.Random();
      HalfKey = parameters.G.PowerMod(r, P);
      Ciphertext = (publicKey.PowerMod(r, P) * parameters.F.PowerMod(vote, P)).Mod(P);
    }

    /// <summary>
    /// Add two votes to a new one.
    /// </summary>
    /// <param name="a">A vote.</param>
    /// <param name="b">Another vote.</param>
    public Vote(Vote a, Vote b)
    {
      P = a.P;
      HalfKey = (a.HalfKey * b.HalfKey).Mod(P);
      Ciphertext = (a.Ciphertext * b.Ciphertext).Mod(P);
    }

    /// <summary>
    /// Add two votes to a new one.
    /// </summary>
    /// <param name="a">A vote.</param>
    /// <param name="b">Another vote.</param>
    /// <returns>Summary vote.</returns>
    public static Vote operator +(Vote a, Vote b)
    {
      return new Vote(a, b);
    }

    public void Decrypt(BigInt privateKey)
    {
      BigInt voteEncryptionKey = HalfKey.PowerMod(privateKey, P);
      Ciphertext = Ciphertext.DivideMod(voteEncryptionKey, P);
    }

    public int Result(Parameters parameters)
    {
      int sumOfVotes = 0;
      while (parameters.F.PowerMod(new BigInt(sumOfVotes), P) != Ciphertext)
      {
        sumOfVotes++;
      }

      return sumOfVotes;
    }
  }
}
