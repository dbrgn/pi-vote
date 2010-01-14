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

      List<PartialDecipher> partialDeciphers = new List<PartialDecipher>();
      auths.Foreach(authority => partialDeciphers.AddRange(authority.PartialDeciphers(sum)));

      IEnumerable<BigInt> partialDeciphers0 = partialDeciphers
        .Where(partialDecipher => partialDecipher.Index == 1)
        .Select(partialDecipher => partialDecipher.Value);
      int v0 = sum.Decrypt(partialDeciphers0, parameters);

      IEnumerable<BigInt> partialDeciphers1 = partialDeciphers
        .Where(partialDecipher => partialDecipher.Index == 1)
        .Select(partialDecipher => partialDecipher.Value);
      int v1 = sum.Decrypt(partialDeciphers1, parameters);

      IEnumerable<BigInt> partialDeciphers2 = partialDeciphers
        .Where(partialDecipher => partialDecipher.Index == 1)
        .Select(partialDecipher => partialDecipher.Value);
      int v2 = sum.Decrypt(partialDeciphers2, parameters);

      IEnumerable<BigInt> partialDeciphers3 = partialDeciphers
        .Where(partialDecipher => partialDecipher.Index == 1)
        .Select(partialDecipher => partialDecipher.Value);
      int v3 = sum.Decrypt(partialDeciphers3, parameters);

      IEnumerable<BigInt> partialDeciphers4 = partialDeciphers
        .Where(partialDecipher => partialDecipher.Index == 1)
        .Select(partialDecipher => partialDecipher.Value);
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
