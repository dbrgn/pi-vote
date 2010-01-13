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
      BigInt prime = new BigInt("5393");
      Parameters parameters = new Parameters(prime);
      Authority[] auths = new Authority[5];

      for (int aI = 0; aI < 5; aI++)
      {
        auths[aI] = new Authority(aI + 1, parameters);
      }

      foreach (Authority a in auths)
      {
        a.CreatePolynomial(2);
      }

      foreach (Authority a in auths)
      {
        List<BigInt> shares = new List<BigInt>();
        List<List<BigInt>> As = new List<List<BigInt>>();

        foreach (Authority b in auths)
        {
          shares.Add(b.S(a.Index));
          As.Add(new List<BigInt>());

          for (int l = 0; l <= 2; l++)
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

      int votum = sum.Decrypt(parameters, privateKey);
      if (votum != 10)
        throw new Exception("bad tally");
    }
  }
}
