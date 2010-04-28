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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Non-interactive zero knowledge proof that a vote is in range 0-1.
  /// </summary>
  public class RangeProof : Serializable
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
    public RangeProof(int votum, BigInt r, Vote vote, BigInt publicKey, BaseParameters parameters)
    {
      if (r == null)
        throw new ArgumentNullException("r");
      if (vote == null)
        throw new ArgumentNullException("vote");
      if (publicKey == null)
        throw new ArgumentNullException("publicKey");
      if (parameters == null)
        throw new ArgumentNullException("parameters");

      BigInt r0 = parameters.Crypto.Random();
      BigInt r1 = parameters.Crypto.Random();

      T0 = publicKey.PowerMod(r0, parameters.Crypto.P);
      T1 = publicKey.PowerMod(r1, parameters.Crypto.P);

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
    public bool Verify(Vote vote, BigInt publicKey, BaseParameters parameters)
    {
      if (vote == null)
        throw new ArgumentNullException("vote");
      if (publicKey == null)
        throw new ArgumentNullException("publicKey");
      if (parameters == null)
        throw new ArgumentNullException("parameters");

      if (C != 0 && C != 1)
        return false;

      if (C0 != 0 && C0 != 1)
        return false;

      if (C1 != 0 && C1 != 1)
        return false;
      
      SHA512Managed sha512 = new SHA512Managed();
      byte[] hash = sha512.ComputeHash(T0.ToByteArray().Concat(T1.ToByteArray()));
      if (C != (((hash[0] & 1) == 1) ? 1 : 0))
        return false;

      if (C0 + C1 != C)
        return false;

      if (publicKey.PowerMod(S0, parameters.Crypto.P) !=
        (T0 * vote.Ciphertext.DivideMod(parameters.Crypto.F.PowerMod(0, parameters.Crypto.P), parameters.Crypto.P).PowerMod(C0, parameters.Crypto.P)).Mod(parameters.Crypto.P))
        return false;

      if (publicKey.PowerMod(S1, parameters.Crypto.P) !=
        (T1 * vote.Ciphertext.DivideMod(parameters.Crypto.F.PowerMod(1, parameters.Crypto.P), parameters.Crypto.P).PowerMod(C1, parameters.Crypto.P)).Mod(parameters.Crypto.P))
        return false;

      return true;
    }

    public RangeProof(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(T0);
      context.Write(T1);
      context.Write(C);
      context.Write(C0);
      context.Write(C1);
      context.Write(S0);
      context.Write(S1);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      T0 = context.ReadBigInt();
      T1 = context.ReadBigInt();
      C = context.ReadInt32();
      C0 = context.ReadInt32();
      C1 = context.ReadInt32();
      S0 = context.ReadBigInt();
      S1 = context.ReadBigInt();
    }
  }
}
