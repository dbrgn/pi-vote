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
using System.Security.Cryptography;
using System.IO;
using Emil.GMP;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Base for the voting parameters.
  /// </summary>
  [SerializeObject("Base for the voting parameters.")]
  public class BaseParameters : Serializable
  {
    public const int PrimeBits = 4096;
    public const int StandardAuthorityCount = 5;
    public const int StandardThereshold = 3;
    public const int StandardProofCount = 8;

    /// <summary>
    /// Questions in the voting.
    /// </summary>
    [SerializeField(7, "Questions in the voting.")]
    private List<Question> questions;

    /// <summary>
    /// Questions in the voting.
    /// </summary>
    public IEnumerable<Question> Questions { get { return this.questions; } }

    /// <summary>
    /// Number of adversaries that can be tolerated.
    /// </summary>
    [SerializeField(4, "Number of adversaries that can be tolerated.")]
    public int Thereshold { get; private set; }

    /// <summary>
    /// Number of authorities.
    /// </summary>
    [SerializeField(5, "Number of authorities.")]
    public int AuthorityCount { get; private set; }

    /// <summary>
    /// Number of proves required to proof a single fact.
    /// </summary>
    [SerializeField(6, "Number of proves required to proof a single fact.")]
    public int ProofCount { get; private set; }

    /// <summary>
    /// Prime.
    /// </summary>
    [SerializeField(1, "Prime.")]
    public BigInt Q { get; private set; }

    /// <summary>
    /// Safe Prime.
    /// P = 2 * Q + 1
    /// </summary>
    [SerializeField(0, "Safe Prime.")]
    public BigInt P { get; private set; }

    /// <summary>
    /// Order Q element of Zp*.
    /// G = a ^ Q mod P where a random
    /// </summary>
    [SerializeField(2, "Order Q element of Zp*.")]
    public BigInt G { get; private set; }

    /// <summary>
    /// Element of <G>
    /// F = G ^ b mod P where b random
    /// </summary>
    [SerializeField(3, "Element of <G>.")]
    public BigInt F { get; private set; }

    /// <summary>
    /// Sets some parameters of the voting.
    /// </summary>
    /// <param name="thereshold">Thereshold of secret sharing.</param>
    /// <param name="authorityCount">Number of authorities.</param>
    /// <param name="proofCount">Number of proofs per obligation.</param>
    public void SetParameters(
      int thereshold,
      int authorityCount,
      int proofCount)
    {
      if (!authorityCount.InRange(3, 23))
        throw new ArgumentException("Authority count must be in range from 3 to 23.");
      if (!thereshold.InRange(1, authorityCount - 1))
        throw new ArgumentException("Thereshold must be in range from 1 to authorityCount - 1.");

      Thereshold = thereshold;
      AuthorityCount = authorityCount;
      ProofCount = proofCount;
    }

    /// <summary>
    /// Sets the numbers needed for cryptography.
    /// </summary>
    /// <remarks>
    /// This includes Q, P, G and F.
    /// </remarks>
    /// <param name="prime">A prime number.</param>
    /// <param name="safePrime">A safe prime.</param>
    private void SetNumbers(
      BigInt prime,
      BigInt safePrime)
    {
      if (prime == null)
        throw new ArgumentNullException("prime");
      if (safePrime == null)
        throw new ArgumentNullException("safePrime");

      Q = prime;
      P = safePrime;

      BigInt r0 = Random();
      BigInt r1 = Random();

      G = r0.PowerMod(Q, P);
      F = G.PowerMod(r1, P);
    }

    /// <summary>
    /// Are these parameters valid.
    /// </summary>
    public virtual bool Valid
    {
      get
      {
        return P.BitLength >= 128 &&
               Q.BitLength >= 128 &&
               G.BitLength >= 128 &&
               F.BitLength >= 128;
      }
    }

    /// <summary>
    /// Creates a random number in Zp*
    /// </summary>
    /// <returns>New random number.</returns>
    public BigInt Random()
    {
      BigInt value = 0;

      while (value < 1)
      {
        byte[] data = new byte[P.BitLength / 8 + 1];
        RandomNumberGenerator.Create().GetBytes(data);
        value = new BigInt(data).Mod(P);
      }

      return value;
    }

    /// <summary>
    /// Generates numbers for cryptography with default length.
    /// </summary>
    /// <remarks>
    /// This may take a very long time.
    /// May load safe primes from disk.
    /// </remarks>
    /// <param name="dataPath">Path where application data is stored.</param>
    public void GenerateNumbers(string dataPath)
    {
      GenerateNumbers(dataPath, PrimeBits);
    }

    /// <summary>
    /// Generates numbers for cryptography.
    /// </summary>
    /// <remarks>
    /// This may take a very long time.
    /// May load safe primes from disk.
    /// </remarks>
    /// <param name="dataPath">Path where application data is stored.</param>
    /// <param name="primeBits">Bit length of the numbers.</param>
    public void GenerateNumbers(string dataPath, int primeBits)
    {
      BigInt prime = null;
      BigInt safePrime = null;

      if (!Prime.TryLoadPregeneratedSafePrime(dataPath, primeBits, out prime, out safePrime))
      {
        Prime.FindPrimeAndSafePrimeThreaded(primeBits, out prime, out safePrime);
      }

      SetNumbers(prime, safePrime);
    }

    /// <summary>
    /// Creates new parameters using non-standard parameters.
    /// </summary>
    public BaseParameters()
    {
      this.questions = new List<Question>();

      SetParameters(StandardThereshold, StandardAuthorityCount, StandardProofCount);
      SetNumbers(4, 4);
    }

    /// <summary>
    /// Adds a question to the voting.
    /// </summary>
    /// <param name="question"></param>
    public void AddQuestion(Question question)
    {
      this.questions.Add(question);
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public BaseParameters(DeserializeContext context)
      : base(context)
    {
    }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(P);
      context.Write(Q);
      context.Write(G);
      context.Write(F);
      context.Write(Thereshold);
      context.Write(AuthorityCount);
      context.Write(ProofCount);
      context.WriteList(this.questions);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      P = context.ReadBigInt();
      Q = context.ReadBigInt();
      G = context.ReadBigInt();
      F = context.ReadBigInt();
      Thereshold = context.ReadInt32();
      AuthorityCount = context.ReadInt32();
      ProofCount = context.ReadInt32();
      this.questions = context.ReadObjectList<Question>();
    }
  }
}
