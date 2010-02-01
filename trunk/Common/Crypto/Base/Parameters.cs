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
  public class Parameters : Serializable
  {
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

    /// <summary>
    /// Number of adversaries that can be tolerated.
    /// </summary>
    public int Thereshold { get; private set; }

    /// <summary>
    /// Number of authorities.
    /// </summary>
    public int AuthorityCount { get; private set; }

    /// <summary>
    /// Number of vota each voter may cast.
    /// </summary>
    public int MaxVota { get; private set; }

    /// <summary>
    /// Number of voting options or candidates.
    /// </summary>
    public int OptionCount { get; private set; }

    /// <summary>
    /// Number of proves required to proof a single fact.
    /// </summary>
    public int ProofCount { get; private set; }

    public Parameters()
    {
    }
    
    /// <summary>
    /// Initializes the crypto part of the parameters.
    /// </summary>
    /// <remarks>
    /// Prime must be lower than safePrime.
    /// </remarks>
    /// <param name="prime">Prime number p for group Zp*.</param>
    /// <param name="safePrime">Safe prime for q.</param>
    /// <param name="thereshold">Maximal tolerable number of compromised authorities.</param>
    /// <param name="authorityCount">Number of authorities.</param>
    /// <param name="optionCount">Number of options.</param>
    /// <param name="maxVota">Maximum number of votes castable by a voter.</param>
    /// <param name="proofCount">Number of proof required to proof each fact.</param>
    public void InitilizeCrypto(
      BigInt prime, 
      BigInt safePrime, 
      int thereshold, 
      int authorityCount, 
      int optionCount, 
      int maxVota, 
      int proofCount)
    {
      if (prime == null)
        throw new ArgumentNullException("prime");
      if (safePrime == null)
        throw new ArgumentNullException("safePrime");
      if (prime >= safePrime)
        throw new ArgumentException("Prime must be smaller than safePrime.");
      if (!authorityCount.InRange(3, 23))
        throw new ArgumentException("Authority count must be in range from 3 to 23.");
      if (!thereshold.InRange(1, authorityCount - 1))
        throw new ArgumentException("Thereshold must be in range from 1 to authorityCount - 1.");
      if (optionCount < 2)
        throw new ArgumentException("Option count must be at least 2.");
      if (!maxVota.InRange(1, optionCount))
        throw new ArgumentException("Maximum vota number must be in range from 1 to optionCount.");

      Q = prime;
      P = safePrime;

      BigInt r0 = Random();
      BigInt r1 = Random();

      G = r0.PowerMod(Q, P);
      F = G.PowerMod(r1, P);

      Thereshold = thereshold;
      AuthorityCount = authorityCount;
      OptionCount = optionCount;
      MaxVota = maxVota;
      ProofCount = proofCount;
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

    public Parameters(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(P);
      context.Write(Q);
      context.Write(G);
      context.Write(F);
      context.Write(Thereshold);
      context.Write(AuthorityCount);
      context.Write(MaxVota);
      context.Write(OptionCount);
      context.Write(ProofCount);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      P = context.ReadBigInt();
      Q = context.ReadBigInt();
      G = context.ReadBigInt();
      F = context.ReadBigInt();
      Thereshold = context.ReadInt32();
      AuthorityCount = context.ReadInt32();
      MaxVota = context.ReadInt32();
      OptionCount = context.ReadInt32();
      ProofCount = context.ReadInt32();
    }
  }
}
