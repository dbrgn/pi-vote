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
  /// Non-interactive zero knowledge proof that a vote sum is MaxVota.
  /// </summary>
  public class Proof : Serializable
  {
    /// <summary>
    /// Whitness.
    /// </summary>
    public BigInt T0 { get; private set; }

    /// <summary>
    /// Challange.
    /// </summary>
    public int C0 { get; private set; }

    /// <summary>
    /// Response.
    /// </summary>
    public BigInt S0 { get; private set; }

    /// <summary>
    /// Creates a new range proof.
    /// </summary>
    /// <param name="votum">The votum. Must be 0 or 1.</param>
    /// <param name="r">The randomness used to encrypt the vote.</param>
    /// <param name="vote">The vote for which to generate a proof.</param>
    /// <param name="publicKey">The public key with which the vote was encrypted.</param>
    /// <param name="parameters">Cryptographic Parameters.</param>
    public Proof(BigInt r, Vote vote, BigInt publicKey, Parameters parameters)
    {
      BigInt r0 = parameters.Random();

      T0 = publicKey.PowerMod(r0, parameters.P);

      SHA512Managed sha512 = new SHA512Managed();
      byte[] hash = sha512.ComputeHash(T0.ToByteArray());
      C0 = ((hash[0] & 1) == 1) ? 1 : 0;

      S0 = r0 + r * C0;
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
      if (C0 != 0 && C0 != 1)
        return false;

      SHA512Managed sha512 = new SHA512Managed();
      byte[] hash = sha512.ComputeHash(T0.ToByteArray());
      if (C0 != (((hash[0] & 1) == 1) ? 1 : 0))
        return false;

      if (publicKey.PowerMod(S0, parameters.P) !=
        (T0 * vote.Ciphertext.DivideMod(parameters.F.PowerMod(parameters.MaxVota, parameters.P), parameters.P).PowerMod(C0, parameters.P)).Mod(parameters.P))
        return false;

      return true;
    }

    public Proof(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(T0);
      context.Write(C0);
      context.Write(S0);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      T0 = context.ReadBigInt();
      C0 = context.ReadInt32();
      S0 = context.ReadBigInt();
    }
  }
}
