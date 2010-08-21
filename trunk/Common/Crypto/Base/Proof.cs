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
using System.IO;
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
    /// Creates a new sum proof.
    /// </summary>
    /// <param name="r">The randomness used to encrypt the vote.</param>
    /// <param name="voteSum">The sum of all votes for which to generate a proof.</param>
    /// <param name="publicKey">The public key with which the vote was encrypted.</param>
    /// <param name="parameters">Cryptographic Parameters.</param>
    public Proof(BigInt r, Vote voteSum, BigInt publicKey, BaseParameters parameters)
    {
      if (r == null)
        throw new ArgumentNullException("r");
      if (voteSum == null)
        throw new ArgumentNullException("vote");
      if (publicKey == null)
        throw new ArgumentNullException("publicKey");
      if (parameters == null)
        throw new ArgumentNullException("parameters");

      BigInt r0 = parameters.Random();

      T0 = publicKey.PowerMod(r0, parameters.P);

      MemoryStream serializeStream = new MemoryStream();
      SerializeContext serializer = new SerializeContext(serializeStream);
      serializer.Write(voteSum.Ciphertext);
      serializer.Write(voteSum.HalfKey);
      serializer.Write(publicKey);
      serializer.Write(T0);
      serializer.Close();
      serializeStream.Close();

      SHA512Managed sha512 = new SHA512Managed();
      byte[] hash = sha512.ComputeHash(serializeStream.ToArray());
      C0 = hash[0] | (hash[1] << 8);

      S0 = r0 + r * C0;
    }

    /// <summary>
    /// Creates a new sum proof.
    /// </summary>
    /// <param name="r">The randomness used to encrypt the vote.</param>
    /// <param name="voteSum">The sum of all votes for which to generate a proof.</param>
    /// <param name="publicKey">The public key with which the vote was encrypted.</param>
    /// <param name="parameters">Cryptographic Parameters.</param>
    /// /// <param name="fakeType">What fake to create?</param>
    public Proof(BigInt r, Vote voteSum, BigInt publicKey, BaseParameters parameters, FakeType fakeType)
    {
      if (r == null)
        throw new ArgumentNullException("r");
      if (voteSum == null)
        throw new ArgumentNullException("vote");
      if (publicKey == null)
        throw new ArgumentNullException("publicKey");
      if (parameters == null)
        throw new ArgumentNullException("parameters");

      BigInt r0 = parameters.Random();
      MemoryStream serializeStream = new MemoryStream();
      SerializeContext serializer = new SerializeContext(serializeStream);
      SHA512Managed sha512 = new SHA512Managed();

      switch (fakeType)
      {
        case FakeType.BadFiatShamir:

          T0 = publicKey.PowerMod(r0, parameters.P);

          serializer.Write(voteSum.Ciphertext);
          serializer.Write(voteSum.HalfKey);
          serializer.Write(publicKey);
          serializer.Write(T0);
          serializer.Close();
          serializeStream.Close();

          byte[] hash0 = sha512.ComputeHash(serializeStream.ToArray());
          C0 = 0;

          S0 = r0 + r * C0;

          break;

        case FakeType.BadPowerMod:

          T0 = publicKey.PowerMod(0, parameters.P);

          serializer.Write(voteSum.Ciphertext);
          serializer.Write(voteSum.HalfKey);
          serializer.Write(publicKey);
          serializer.Write(T0);
          serializer.Close();
          serializeStream.Close();

          byte[] hash1 = sha512.ComputeHash(serializeStream.ToArray());
          C0 = hash1[0] | (hash1[1] << 8);

          S0 = 1;

          break;

        default:
          throw new NotSupportedException("Cannot generate that type of fake.");
      }
    }

    /// <summary>
    /// Verifies a range proof.
    /// </summary>
    /// <param name="voteSum">The vote sum for which to verify the proof.</param>
    /// <param name="publicKey">Public key against which to verify the proof.</param>
    /// <param name="parameters">Cryptographic Parameters.</param>
    /// <returns>Wether the proof is valid.</returns>
    public bool Verify(Vote voteSum, BigInt publicKey, BaseParameters parameters, Question questionParameters)
    {
      if (voteSum == null)
        throw new ArgumentNullException("vote");
      if (publicKey == null)
        throw new ArgumentNullException("publicKey");
      if (parameters == null)
        throw new ArgumentNullException("parameters");

      if (!C0.InRange(0, 0xffff))
        return false;

      MemoryStream serializeStream = new MemoryStream();
      SerializeContext serializer = new SerializeContext(serializeStream);
      serializer.Write(voteSum.Ciphertext);
      serializer.Write(voteSum.HalfKey);
      serializer.Write(publicKey);
      serializer.Write(T0);
      serializer.Close();
      serializeStream.Close();

      SHA512Managed sha512 = new SHA512Managed();
      byte[] hash = sha512.ComputeHash(serializeStream.ToArray());
      if (C0 != (hash[0] | (hash[1] << 8)))
        return false;

      if (publicKey.PowerMod(S0, parameters.P) !=
        (T0 * voteSum.Ciphertext.DivideMod(parameters.F.PowerMod(questionParameters.MaxVota, parameters.P), parameters.P).PowerMod(C0, parameters.P)).Mod(parameters.P))
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
