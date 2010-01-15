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

    public Parameters(BigInt prime, BigInt safePrime, int thereshold, int authorityCount, int optionCount, int maxVota, int proofCount)
    {
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

    public BigInt Random()
    {
      byte[] data = new byte[P.BitLength / 8 + 1];
      RandomNumberGenerator.Create().GetBytes(data);
      return new BigInt(data).Mod(P);
    }

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
