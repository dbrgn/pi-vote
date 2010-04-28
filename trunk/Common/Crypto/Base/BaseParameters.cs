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
  public abstract class BaseParameters : Serializable
  {
    public CryptoParameters Crypto { get; private set; }

    public abstract QuestionBaseParameters QB { get; }

    public abstract VotingBaseParameters QV { get; }

    public BaseParameters(CryptoParameters crypto)
    {
      Crypto = crypto;
    }

    public BaseParameters(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Crypto);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Crypto = context.ReadObject<CryptoParameters>();
    }
  }

  public class BaseParameters<TQuestionBaseParameters, TVotingBaseParameters> 
    : BaseParameters
    where TQuestionBaseParameters : QuestionBaseParameters
    where TVotingBaseParameters : VotingBaseParameters
  {
    public TQuestionBaseParameters Quest { get; private set; }

    public TVotingBaseParameters Voting { get; private set; }

    public override QuestionBaseParameters QB
    {
      get { return Quest; }
    }

    public override VotingBaseParameters QV
    {
      get { return Voting; }
    }

    public BaseParameters(
      CryptoParameters crypto,
      TQuestionBaseParameters quest,
      TVotingBaseParameters voting)
      : base(crypto)
    {
      Quest = quest;
      Voting = voting;
    }

    public BaseParameters(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Quest);
      context.Write(Voting);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Quest = context.ReadObject<TQuestionBaseParameters>();
      Voting = context.ReadObject<TVotingBaseParameters>();
    }
  }

  public class VotingBaseParameters : Serializable
  {
    public const int StandardAuthorityCount = 5;
    public const int StandardThereshold = 3;
    public const int StandardProofCount = 40;

    /// <summary>
    /// Number of adversaries that can be tolerated.
    /// </summary>
    public int Thereshold { get; private set; }

    /// <summary>
    /// Number of authorities.
    /// </summary>
    public int AuthorityCount { get; private set; }

    /// <summary>
    /// Number of proves required to proof a single fact.
    /// </summary>
    public int ProofCount { get; private set; }

    public VotingBaseParameters(
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

    public VotingBaseParameters(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Thereshold);
      context.Write(AuthorityCount);
      context.Write(ProofCount);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Thereshold = context.ReadInt32();
      AuthorityCount = context.ReadInt32();
      ProofCount = context.ReadInt32();
    }
  }

  public abstract class QuestionBaseParameters : Serializable
  {
    /// <summary>
    /// Number of vota each voter may cast.
    /// </summary>
    public int MaxVota { get; private set; }

    /// <summary>
    /// Number of voting options or candidates.
    /// </summary>
    public abstract int OptionCount { get; }

    public QuestionBaseParameters(
      int maxVota)
    {
      MaxVota = maxVota;
    }

    public QuestionBaseParameters(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(MaxVota);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      MaxVota = context.ReadInt32();
    }
  }

  public class CryptoParameters : Serializable
  {
    public const int PrimeBits = 1024;

    /// <summary>
    /// Prime
    /// </summary>
    public BigInt Q { get; private set; }

    /// <summary>
    /// Safe Prime
    /// P = 2 * Q + 1
    /// </summary>
    public BigInt P { get; private set; }

    /// <summary>
    /// Order Q element of Zp*
    /// G = a ^ Q mod P where a random
    /// </summary>
    public BigInt G { get; private set; }

    /// <summary>
    /// element of <G>
    /// F = G ^ b mod P where b random
    /// </summary>
    public BigInt F { get; private set; }

    public CryptoParameters(
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

    public CryptoParameters(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(P);
      context.Write(Q);
      context.Write(G);
      context.Write(F);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      P = context.ReadBigInt();
      Q = context.ReadBigInt();
      G = context.ReadBigInt();
      F = context.ReadBigInt();
    }

    public static CryptoParameters Generate(int primeBits)
    {
      BigInt prime = null;
      BigInt safePrime = null;

      DateTime start = DateTime.Now;
      Prime.FindPrimeAndSafePrimeThreaded(primeBits, out prime, out safePrime);
      System.Diagnostics.Debug.WriteLine("Found safe prime after " + DateTime.Now.Subtract(start).ToString());
      //Prime.FindPrimeAndSafePrime(PrimeBits, out prime, out safePrime);

      return new CryptoParameters(prime, safePrime);
    }
  }
}
