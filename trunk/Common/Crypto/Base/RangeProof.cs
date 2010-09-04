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
  /// Non-interactive zero knowledge proof that a vote is in range 0-1.
  /// </summary>
  public class RangeProof : Serializable
  {
    /// <summary>
    /// Witness for votum equals 0.
    /// </summary>
    public BigInt T0 { get; private set; }

    /// <summary>
    /// Witness for votum equals 1.
    /// </summary>
    public BigInt T1 { get; private set; }

    /// <summary>
    /// Or-Challenge
    /// </summary>
    public int C { get; private set; }

    /// <summary>
    /// Challenge for vote equals 0.
    /// </summary>
    public int C0 { get; private set; }

    /// <summary>
    /// Challenge for vote equals 1.
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

      BigInt r0 = parameters.Random();
      BigInt r1 = parameters.Random();

      switch (votum)
      {
        case 0:
          //Create the fake proof.
          C1 = (int)Prime.RandomNumber(16);
          S1 = parameters.Random();
          T1 = (vote.Ciphertext.DivideMod(parameters.F.PowerMod(1, parameters.P), parameters.P).PowerMod(C1, parameters.P).InvertMod(parameters.P) * publicKey.PowerMod(S1, parameters.P)).Mod(parameters.P);

          //First part of the real proof
          T0 = publicKey.PowerMod(r0, parameters.P);
          break;
        case 1:
          //Create the fake proof.
          C0 = (int)Prime.RandomNumber(16);
          S0 = parameters.Random();
          T0 = (vote.Ciphertext.DivideMod(parameters.F.PowerMod(0, parameters.P), parameters.P).PowerMod(C0, parameters.P).InvertMod(parameters.P) * publicKey.PowerMod(S0, parameters.P)).Mod(parameters.P);

          //First part of the real proof
          T1 = publicKey.PowerMod(r1, parameters.P);
          break;
        default:
          throw new ArgumentException("Bad votum.");
      }

      //Put togeather the data to be hashed.
      MemoryStream serializeStream = new MemoryStream();
      SerializeContext serializer = new SerializeContext(serializeStream);
      serializer.Write(vote.Ciphertext);
      serializer.Write(vote.HalfKey);
      serializer.Write(publicKey);
      serializer.Write(T0);
      serializer.Write(T1);
      serializer.Close();
      serializeStream.Close();

      //Hash the proof data.
      SHA512Managed sha512 = new SHA512Managed();
      byte[] hash = sha512.ComputeHash(serializeStream.ToArray());
      //Take the first 16 bits.
      C = hash[0] | (hash[1] << 8);

      switch (votum)
      {
        case 0:
          //Second part of the real proof
          C0 = (int)((BigInt)(C - C1)).Mod(0xffff);
          S0 = r0 + r * C0;
          break;
        case 1:
          //Second part of the real proof
          C1 = (int)((BigInt)(C - C0)).Mod(0xffff);
          S1 = r1 + r * C1;
          break;
        default:
          throw new ArgumentException("Bad votum.");
      } 
    }

    /// <summary>
    /// Creates a new invalid range proof.
    /// </summary>
    /// <remarks>
    /// Used to test proofing mechanism.
    /// </remarks>
    /// <param name="votum">The votum. Must NOT be 0 or 1.</param>
    /// <param name="r">The randomness used to encrypt the vote.</param>
    /// <param name="vote">The vote for which to generate a proof.</param>
    /// <param name="publicKey">The public key with which the vote was encrypted.</param>
    /// <param name="parameters">Cryptographic Parameters.</param>
    /// <param name="fakeType">What is to be wrong?</param>
    public RangeProof(int votum, BigInt r, Vote vote, BigInt publicKey, BaseParameters parameters, FakeType fakeType)
    {
      if (r == null)
        throw new ArgumentNullException("r");
      if (vote == null)
        throw new ArgumentNullException("vote");
      if (publicKey == null)
        throw new ArgumentNullException("publicKey");
      if (parameters == null)
        throw new ArgumentNullException("parameters");

      BigInt r0 = parameters.Random();
      BigInt r1 = parameters.Random();
      MemoryStream serializeStream = new MemoryStream();
      SerializeContext serializer = new SerializeContext(serializeStream);
      SHA512Managed sha512 = new SHA512Managed();

      switch (fakeType)
      {
        case FakeType.BadDisjunction:
          //The C value will not be correct as it is not considered.

          //Create the fake proof.
          C0 = (int)Prime.RandomNumber(16);
          S0 = parameters.Random();
          T0 = (vote.Ciphertext.DivideMod(parameters.F.PowerMod(0, parameters.P), parameters.P).PowerMod(C0, parameters.P).InvertMod(parameters.P) * publicKey.PowerMod(S0, parameters.P)).Mod(parameters.P);

          //Create the fake proof.
          C1 = (int)Prime.RandomNumber(16);
          S1 = parameters.Random();
          T1 = (vote.Ciphertext.DivideMod(parameters.F.PowerMod(1, parameters.P), parameters.P).PowerMod(C1, parameters.P).InvertMod(parameters.P) * publicKey.PowerMod(S1, parameters.P)).Mod(parameters.P);

          //Put togeather the data to be hashed.
          serializer.Write(vote.Ciphertext);
          serializer.Write(vote.HalfKey);
          serializer.Write(publicKey);
          serializer.Write(T0);
          serializer.Write(T1);
          serializer.Close();
          serializeStream.Close();

          //Hash the proof data.
          byte[] hash0 = sha512.ComputeHash(serializeStream.ToArray());
          //Take the first 16 bits.
          C = hash0[0] | (hash0[1] << 8);

          break;

        case FakeType.BadFiatShamir:
          //The C value will not correspond to the hash.

          //Create the fake proof.
          C0 = (int)Prime.RandomNumber(16);
          S0 = parameters.Random();
          T0 = (vote.Ciphertext.DivideMod(parameters.F.PowerMod(0, parameters.P), parameters.P).PowerMod(C0, parameters.P).InvertMod(parameters.P) * publicKey.PowerMod(S0, parameters.P)).Mod(parameters.P);

          //Create the fake proof.
          C1 = (int)Prime.RandomNumber(16);
          S1 = parameters.Random();
          T1 = (vote.Ciphertext.DivideMod(parameters.F.PowerMod(1, parameters.P), parameters.P).PowerMod(C1, parameters.P).InvertMod(parameters.P) * publicKey.PowerMod(S1, parameters.P)).Mod(parameters.P);

          //Put togeather the data to be hashed.
          serializer.Write(vote.Ciphertext);
          serializer.Write(vote.HalfKey);
          serializer.Write(publicKey);
          serializer.Write(T0);
          serializer.Write(T1);
          serializer.Close();
          serializeStream.Close();

          //Hash the proof data.
          byte[] hash1 = sha512.ComputeHash(serializeStream.ToArray());
          //Take the first 16 bits.
          C = C0 + C1;

          break;

        case FakeType.BadPowerMod:

          //Create the fake proof.
          C1 = (int)Prime.RandomNumber(16);
          S1 = parameters.Random();
          T1 = (vote.Ciphertext.DivideMod(parameters.F.PowerMod(1, parameters.P), parameters.P).PowerMod(C1, parameters.P).InvertMod(parameters.P) * publicKey.PowerMod(S1, parameters.P)).Mod(parameters.P);

          //First part of the real proof
          T0 = publicKey.PowerMod(r0, parameters.P);

          //Put togeather the data to be hashed.
          serializer.Write(vote.Ciphertext);
          serializer.Write(vote.HalfKey);
          serializer.Write(publicKey);
          serializer.Write(T0);
          serializer.Write(T1);
          serializer.Close();
          serializeStream.Close();

          //Hash the proof data.
          byte[] hash2 = sha512.ComputeHash(serializeStream.ToArray());
          //Take the first 16 bits.
          C = hash2[0] | (hash2[1] << 8);

          //Second part of the real proof
          C0 = (int)((BigInt)(C - C1)).Mod(0xffff);
          S0 = r0 + r * C0;

          break;

        default:
          throw new NotSupportedException("Cannot fake in that way.");
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

      if (!C.InRange(0, 0xffff))
        return false;

      MemoryStream serializeStream = new MemoryStream();
      SerializeContext serializer = new SerializeContext(serializeStream);
      serializer.Write(vote.Ciphertext);
      serializer.Write(vote.HalfKey);
      serializer.Write(publicKey);
      serializer.Write(T0);
      serializer.Write(T1);
      serializer.Close();
      serializeStream.Close();

      SHA512Managed sha512 = new SHA512Managed();
      byte[] hash = sha512.ComputeHash(serializeStream.ToArray());
      if (C != (hash[0] | (hash[1] << 8)))
        return false;

      if (((C0 + C1) % 0xffff) != C)
        return false;

      if (publicKey.PowerMod(S0, parameters.P) !=
        (T0 * vote.Ciphertext.DivideMod(parameters.F.PowerMod(0, parameters.P), parameters.P).PowerMod(C0, parameters.P)).Mod(parameters.P))
        return false;

      if (publicKey.PowerMod(S1, parameters.P) !=
        (T1 * vote.Ciphertext.DivideMod(parameters.F.PowerMod(1, parameters.P), parameters.P).PowerMod(C1, parameters.P)).Mod(parameters.P))
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
