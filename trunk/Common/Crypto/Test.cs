﻿/*
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
        if (More(prime, safePrime) == 10)
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

    private int More(BigInt prime, BigInt safePrime)
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

      BigInt x0 = auths[0].x;
      BigInt x1 = auths[1].x;
      BigInt x2 = auths[2].x;
      BigInt x3 = auths[3].x;
      BigInt x4 = auths[4].x;


      //full private key is a linear combo of authority private keys
      //BigInt privateKey2 = (x0 * 3 - x1 * 3 + x2);
      //sum.Decrypt(privateKey2);

      Vote s0 = sum.Clone();
      s0.Decrypt(x0 * 4);
      s0.Decrypt(x1 * -6);
      s0.Decrypt(x2 * 4);
      s0.Decrypt(x3 * -1);
      int v0 = s0.Result(parameters);

      Vote s1 = sum.Clone();
      s1.Decrypt(x0 * 15 / 4);
      s1.Decrypt(x1 * -5);
      s1.Decrypt(x2 * 5 / 2);
      s1.Decrypt(x4 * -1 / 4);
      int v1 = s1.Result(parameters) ;

      Vote s2 = sum.Clone();
      s2.Decrypt(x0 * 10 / 3);
      s2.Decrypt(x1 * -10 / 3);
      s2.Decrypt(x3 * 5 / 3);
      s2.Decrypt(x4 * -2 / 3);
      int v2 = s2.Result(parameters);

      BigInt r0 = (x0 * 10).Remainder(3);
      BigInt r1 = (x1 * -10).Remainder(3);
      BigInt r3 = (x3 * 5).Remainder(3);
      BigInt r4 = (x4 * -2).Remainder(3);
      BigInt rx = r0 + r1 + r3 + r4;
      
      return v0;
    }
  }
}
