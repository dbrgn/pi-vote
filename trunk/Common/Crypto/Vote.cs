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
      BigInt r = parameters.Random();
      HalfKey = parameters.G.PowerMod(r, parameters.P);
      Ciphertext = publicKey.PowerMod(r, parameters.P) * parameters.F.PowerMod(new BigInt(vote), parameters.P);
    }

    /// <summary>
    /// Add two votes to a new one.
    /// </summary>
    /// <param name="a">A vote.</param>
    /// <param name="b">Another vote.</param>
    public Vote(Vote a, Vote b)
    {
      HalfKey = (a.HalfKey * b.HalfKey);
      Ciphertext = a.Ciphertext * b.Ciphertext;
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

    /// <summary>
    /// Decrypt a vote from a full public key.
    /// </summary>
    /// <remarks>
    /// Not to be used in release.
    /// </remarks>
    /// <param name="parameters">Cryptographic parameters.</param>
    /// <param name="privateKey">Private key of the voting authorities.</param>
    /// <returns>Sum of votes.</returns>
    public int Decrypt(Parameters parameters, BigInt privateKey)
    {
      BigInt voteEncryptionKey = HalfKey.PowerMod(privateKey, parameters.P);
      BigInt disguisedSumOfVotes = Ciphertext.DivideMod(voteEncryptionKey, parameters.P);

      int sumOfVotes = 0;
      while (parameters.F.PowerMod(new BigInt(sumOfVotes), parameters.P) != disguisedSumOfVotes)
      {
        sumOfVotes++;
      }

      return sumOfVotes;
    }
  }
}
