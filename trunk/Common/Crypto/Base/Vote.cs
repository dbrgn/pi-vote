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
  /// Elgamal encrypted vote.
  /// </summary>
  [SerializeObject("Elgamal encrypted vote.")]
  public class Vote : Serializable
  {
    /// <summary>
    /// Diffie-Hellman halfkey.
    /// </summary>
    [SerializeField(0, "Diffie-Hellman halfkey.")]
    public BigInt HalfKey { get; private set; }

    /// <summary>
    /// Ciphertext.
    /// </summary>
    [SerializeField(1, "Ciphertext.")]
    public BigInt Ciphertext { get; private set; }

    /// <summary>
    /// Prime number defining the modular arithmetic.
    /// </summary>
    [SerializeField(2, "Prime number defining the modular arithmetic.")]
    public BigInt P { get; private set; }

    /// <summary>
    /// All range proves for this vote.
    /// </summary>
    [SerializeField(3, "All range proves for this vote.")]
    public List<RangeProof> RangeProves { get; private set; }

    /// <summary>
    /// Creates a new encrypted vote.
    /// </summary>
    /// <remarks>
    /// Includes zero-knowledge proof of vote being 0 or 1.
    /// Still need non-interactive zero-knowledge proof of sum of votes.
    /// </remarks>
    /// <param name="votum">Actual vote.</param>
    /// <param name="parameters">Cryptographic parameters.</param>
    /// <param name="publicKey">Public key of the authorities.</param>
    /// <param name="progress">Report the progress up.</param>
    public Vote(int votum, BigInt nonce, BaseParameters parameters, BigInt publicKey, Progress progress)
    {
      if (!votum.InRange(0, 1))
        throw new ArgumentException("Bad votum.");
      if (nonce == null)
        throw new ArgumentNullException("nonce");
      if (parameters == null)
        throw new ArgumentNullException("parameters");
      if (publicKey == null)
        throw new ArgumentNullException("publicKey");

      P = parameters.P;
      HalfKey = parameters.G.PowerMod(nonce, P);

      //The 12 magic number is inserted to avoid division remainders when
      //dividing partial deciphers for linear combinations by 2, 3 and 4.
      Ciphertext = (publicKey.PowerMod(nonce * 12, P) * parameters.F.PowerMod(votum, P)).Mod(P);

      RangeProves = new List<RangeProof>();
      for (int proofIndex = 0; proofIndex < parameters.ProofCount; proofIndex++)
      {
        RangeProves.Add(new RangeProof(votum, nonce * 12, this, publicKey, parameters));
        progress.Add(1d / (double)parameters.ProofCount);
      }
    }

    /// <summary>
    /// Creates a new encrypted vote with an invalid proof.
    /// </summary>
    /// <remarks>
    /// Includes invalid zero-knowledge proves of vote being 0 or 1 when it is not.
    /// </remarks>
    /// <param name="votum">Actual vote.</param>
    /// <param name="parameters">Cryptographic parameters.</param>
    /// <param name="publicKey">Public key of the authorities.</param>
    /// <param name="fakeType">What kind of a fake?</param>
    public Vote(int votum, BigInt nonce, BaseParameters parameters, BigInt publicKey, FakeType fakeType)
    {
      if (votum.InRange(0, 1))
        throw new ArgumentException("Cannot create a invalid proof of a valid votum.");
      if (nonce == null)
        throw new ArgumentNullException("nonce");
      if (parameters == null)
        throw new ArgumentNullException("parameters");
      if (publicKey == null)
        throw new ArgumentNullException("publicKey");

      P = parameters.P;
      HalfKey = parameters.G.PowerMod(nonce, P);

      //The 12 magic number is inserted to avoid division remainders when
      //dividing partial deciphers for linear combinations by 2, 3 and 4.
      Ciphertext = (publicKey.PowerMod(nonce * 12, P) * parameters.F.PowerMod(votum, P)).Mod(P);

      RangeProves = new List<RangeProof>();
      for (int proofIndex = 0; proofIndex < parameters.ProofCount; proofIndex++)
      {
        RangeProves.Add(new RangeProof(votum, nonce * 12, this, publicKey, parameters, fakeType));
      }
    }

    /// <summary>
    /// Add two votes to a new one.
    /// </summary>
    /// <param name="a">A vote.</param>
    /// <param name="b">Another vote.</param>
    public Vote(Vote a, Vote b)
    {
      if (a == null)
        throw new ArgumentNullException("a");
      if (b == null)
        throw new ArgumentNullException("b");

      P = a.P;
      HalfKey = (a.HalfKey * b.HalfKey).Mod(P);
      Ciphertext = (a.Ciphertext * b.Ciphertext).Mod(P);
      RangeProves = new List<RangeProof>();
    }

    /// <summary>
    /// Create a vote without proofs.
    /// </summary>
    /// <remarks>
    /// Used to clone.
    /// </remarks>
    /// <param name="halfKey">Halfkey ot the vote.</param>
    /// <param name="ciphertext">Ciphertext of the vote.</param>
    /// <param name="p">Prime number defining modular arithmetic.</param>
    private Vote(BigInt halfKey, BigInt ciphertext, BigInt p)
    {
      if (halfKey == null)
        throw new ArgumentNullException("halfKey");
      if (ciphertext == null)
        throw new ArgumentNullException("ciphertext");
      if (p == null)
        throw new ArgumentNullException("p");

      HalfKey = halfKey;
      Ciphertext = ciphertext;
      P = p;
      RangeProves = new List<RangeProof>();
    }

    /// <summary>
    /// Add two votes to a new one.
    /// </summary>
    /// <param name="a">A vote.</param>
    /// <param name="b">Another vote.</param>
    /// <returns>Summary vote.</returns>
    public static Vote operator +(Vote a, Vote b)
    {
      if (a == null)
        throw new ArgumentNullException("a");
      if (b == null)
        throw new ArgumentNullException("b"); 
      
      return new Vote(a, b);
    }

    /// <summary>
    /// Decrypts a vote from partial deciphers.
    /// </summary>
    /// <param name="partialDeciphers">Partial deciphers from t+1 authorities.</param>
    /// <param name="parameters">Cryptographic parameters.</param>
    /// <returns>Result of the vote.</returns>
    public int Decrypt(IEnumerable<BigInt> partialDeciphers, BaseParameters parameters)
    {
      if (partialDeciphers == null)
        throw new ArgumentNullException("partialDeciphers");
      if (parameters == null)
        throw new ArgumentNullException("parameters");
      if (partialDeciphers.Count() != parameters.Thereshold + 1)
        throw new ArgumentException("Wrong number of partial deciphers.");

      BigInt votePower = Ciphertext;
      partialDeciphers.Foreach(partialDecipher => votePower = votePower.DivideMod(partialDecipher, P));

      return Result(votePower, parameters);
    }

    /// <summary>
    /// Calculates the result of the vote.
    /// </summary>
    /// <param name="votePower">Vote value equal to F^vote.</param>
    /// <param name="parameters">Cryptographic parameters.</param>
    /// <returns>Result of the vote.</returns>
    private static int Result(BigInt votePower, BaseParameters parameters)
    {
      if (votePower == null)
        throw new ArgumentNullException("votePower");
      if (parameters == null)
        throw new ArgumentNullException("parameters");
      
      int sumOfVotes = 0;
      while (parameters.F.PowerMod(new BigInt(sumOfVotes), parameters.P) != votePower)
      {
        sumOfVotes++;
        if (sumOfVotes > 10000)
          return -1;
      }

      return sumOfVotes;
    }

    /// <summary>
    /// Clones a vote.
    /// </summary>
    /// <returns>Close of this vote.</returns>
    public Vote Clone()
    {
      return new Vote(HalfKey.Clone(), Ciphertext.Clone(), P);
    }

    /// <summary>
    /// Verifies all range proves.
    /// </summary>
    /// <param name="publicKey">Public key to verify against.</param>
    /// <param name="parameters">Cryptographic Parameters.</param>
    /// <returns>All proves are valid.</returns>
    public bool Verify(BigInt publicKey, BaseParameters parameters)
    {
      if (publicKey == null)
        throw new ArgumentNullException("publicKey");
      if (parameters == null)
        throw new ArgumentNullException("parameters");

      bool verifies = true;

      verifies &= RangeProves.Count == parameters.ProofCount;
      verifies &= RangeProves.All(proof => proof.Verify(this, publicKey, parameters));

      return verifies;
    }

    public Vote(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(HalfKey);
      context.Write(Ciphertext);
      context.Write(P);
      context.WriteList(RangeProves);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      HalfKey = context.ReadBigInt();
      Ciphertext = context.ReadBigInt();
      P = context.ReadBigInt();
      RangeProves = context.ReadObjectList<RangeProof>();
    }
  }
}
