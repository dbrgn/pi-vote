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
  public class Test
  {
    /// <summary>
    /// Test function. Only for development.
    /// </summary>
    public void Do()
    {
      //BigInt prime = FindSafePrime();
      //BigInt prime = FindPrime();
      BigInt prime = new BigInt("500000000000000000037883");
      BigInt safePrime = new BigInt("1000000000000000000059533");

      int good = 0;
      int bad = 0;
      int fail = 0;

      for (int i = 0; i < 10000; i++)
      {
        if (SingleTest(prime, safePrime) == 10)
        {
          good++;
        }
        else
        {
          bad++;
        }
      }

      System.Diagnostics.Debug.WriteLine(good.ToString() + " good, " + bad.ToString() + " bad, " + fail.ToString() + " fail");
    }

    private BigInt FindPrime()
    {
      BigInt i = new BigInt("500000000000000000000000");

      while (true)
      {
        while (!i.IsProbablyPrimeRabinMiller(1000))
        {
          i++;
        }

        System.Diagnostics.Debug.WriteLine(i);
        i++;
      }

      return i;
    }

    private BigInt FindSafePrime()
    {
      BigInt i = new BigInt("1000000000000000000000000");

      while (true)
      {
        while (!i.IsProbablyPrimeRabinMiller(1000) ||
               !(2 * i + 1).IsProbablyPrimeRabinMiller(1000))
        {
          i++;
        }

        System.Diagnostics.Debug.WriteLine(i);
        i++;
      }

      return i;
    }

    private int SingleTest(BigInt prime, BigInt safePrime)
    {
      Parameters parameters = new Parameters(prime, safePrime, 3, 5);
      Authority[] auths = new Authority[5];

      for (int aI = 0; aI < parameters.AuthorityCount; aI++)
      {
        auths[aI] = new Authority(aI + 1, parameters);
      }

      foreach (Authority a in auths)
      {
        a.CreatePolynomial(parameters.Thereshold);
      }

      foreach (Authority a in auths)
      {
        List<BigInt> shares = new List<BigInt>();
        List<List<BigInt>> As = new List<List<BigInt>>();

        foreach (Authority b in auths)
        {
          shares.Add(b.S(a.Index));
          As.Add(new List<BigInt>());

          for (int l = 0; l <= parameters.Thereshold; l++)
          {
            As[As.Count - 1].Add(b.A(l));
          }
        }

        a.VerifySharing(shares, As);
      }

      BigInt publicKey = new BigInt(1);
      foreach (Authority a in auths)
      {
        publicKey = (publicKey * a.Y).Mod(parameters.P);
      }

      List<Vote> votes = new List<Vote>();
      votes.Add(new Vote(0, parameters, publicKey));
      votes.Add(new Vote(1, parameters, publicKey));
      votes.Add(new Vote(2, parameters, publicKey));
      votes.Add(new Vote(1, parameters, publicKey));
      votes.Add(new Vote(0, parameters, publicKey));
      votes.Add(new Vote(1, parameters, publicKey));
      votes.Add(new Vote(2, parameters, publicKey));
      votes.Add(new Vote(1, parameters, publicKey));
      votes.Add(new Vote(2, parameters, publicKey));
      votes.Add(new Vote(0, parameters, publicKey));
      votes.Add(new Vote(0, parameters, publicKey));

      Vote sum = new Vote(0, parameters, publicKey);
      foreach (Vote v in votes)
      {
        sum += v;
      }

      BigInt privateKey = new BigInt(0);
      foreach (Authority a in auths)
      {
        privateKey = (privateKey + a.Z);
      }

      Authority a0 = auths[0];
      Authority a1 = auths[1];
      Authority a2 = auths[2];
      Authority a3 = auths[3];
      Authority a4 = auths[4];

      List<BigInt> partialDeciphers0 = new List<BigInt>();
      partialDeciphers0.Add(a0.PartialDecipher(sum, 4, 1));
      partialDeciphers0.Add(a1.PartialDecipher(sum, -6, 1));
      partialDeciphers0.Add(a2.PartialDecipher(sum, 4, 1));
      partialDeciphers0.Add(a3.PartialDecipher(sum, -1, 1));
      int v0 = sum.Decrypt(partialDeciphers0, parameters);

      List<BigInt> partialDeciphers1 = new List<BigInt>();
      partialDeciphers1.Add(a0.PartialDecipher(sum, 15, 4));
      partialDeciphers1.Add(a1.PartialDecipher(sum, -5, 1));
      partialDeciphers1.Add(a2.PartialDecipher(sum, 5, 2));
      partialDeciphers1.Add(a4.PartialDecipher(sum, -1, 4));
      int v1 = sum.Decrypt(partialDeciphers1, parameters);

      List<BigInt> partialDeciphers2 = new List<BigInt>();
      partialDeciphers2.Add(a0.PartialDecipher(sum, 10, 3));
      partialDeciphers2.Add(a1.PartialDecipher(sum, -10, 3));
      partialDeciphers2.Add(a3.PartialDecipher(sum, 5, 3));
      partialDeciphers2.Add(a4.PartialDecipher(sum, -2, 3));
      int v2 = sum.Decrypt(partialDeciphers2, parameters);

      List<BigInt> partialDeciphers3 = new List<BigInt>();
      partialDeciphers3.Add(a0.PartialDecipher(sum, 5, 2));
      partialDeciphers3.Add(a2.PartialDecipher(sum, -5, 1));
      partialDeciphers3.Add(a3.PartialDecipher(sum, 5, 1));
      partialDeciphers3.Add(a4.PartialDecipher(sum, -3, 2));
      int v3 = sum.Decrypt(partialDeciphers3, parameters);

      List<BigInt> partialDeciphers4 = new List<BigInt>();
      partialDeciphers4.Add(a1.PartialDecipher(sum, 10, 1));
      partialDeciphers4.Add(a2.PartialDecipher(sum, -20, 1));
      partialDeciphers4.Add(a3.PartialDecipher(sum, 15, 1));
      partialDeciphers4.Add(a4.PartialDecipher(sum, -4, 1));
      int v4 = sum.Decrypt(partialDeciphers4, parameters);

      if (v0 == v1 &&
          v0 == v2 &&
          v0 == v3 &&
          v0 == v4)
      {
        return v0;
      }
      else
      {
        return -1;
      }
    }
  }
}
