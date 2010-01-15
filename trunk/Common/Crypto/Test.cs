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
      BigInt prime = Prime.Find(80);
      BigInt safePrime = Prime.FindSafe(88);

      int good = 0;
      int bad = 0;
      int fail = 0;

      for (int i = 0; i < 10000; i++)
      {
        IEnumerable<int> result =SingleTest(prime, safePrime);

        if (result.Count() == 2 &&
            result.ElementAt(0) == 2 &&
            result.ElementAt(1) == 4)
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

    private IEnumerable<int> SingleTest(BigInt prime, BigInt safePrime)
    {
      Parameters parameters = new Parameters(prime, safePrime, 3, 5, 2, 1, 100);
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

      List<Ballot> ballots = new List<Ballot>();
      ballots.Add(new Ballot(new int[] { 0, 1 }, parameters, publicKey));
      ballots.Add(new Ballot(new int[] { 0, 1 }, parameters, publicKey));
      ballots.Add(new Ballot(new int[] { 1, 0 }, parameters, publicKey));
      ballots.Add(new Ballot(new int[] { 0, 1 }, parameters, publicKey));
      ballots.Add(new Ballot(new int[] { 1, 0 }, parameters, publicKey));
      ballots.Add(new Ballot(new int[] { 0, 1 }, parameters, publicKey));

      if (!ballots.All(ballot => ballot.Verify(publicKey, parameters)))
        throw new Exception("Bad proof.");

      for (int optionIndex = 0; optionIndex < parameters.OptionCount; optionIndex++)
      {
        IEnumerable<Vote> votes = ballots.Select(ballot => ballot.Votes[optionIndex]);

        Vote sum = null;
        votes.Foreach(vote => sum = sum == null ? vote : sum + vote);

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
          yield return v0;
        }
        else
        {
          throw new Exception("Bad vote.");
        }
      }
    }
  }
}
