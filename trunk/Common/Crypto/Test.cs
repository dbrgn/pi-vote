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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  public class Test
  {
    public void Do2()
    {
      VotingParameters pc = new VotingParameters(27, "Zufrieden");
      pc.AddOption(new Option("Nein", "Dagegen"));
      pc.AddOption(new Option("Ja", "Dafür"));
      pc.Initialize(1);

      VotingServerEntity vs = new VotingServerEntity(pc);

      AuthorityEntity a1 = new AuthorityEntity();
      AuthorityEntity a2 = new AuthorityEntity();
      AuthorityEntity a3 = new AuthorityEntity();
      AuthorityEntity a4 = new AuthorityEntity();
      AuthorityEntity a5 = new AuthorityEntity();

      vs.AddAuthority(a1.Certificate);
      vs.AddAuthority(a2.Certificate);
      vs.AddAuthority(a3.Certificate);
      vs.AddAuthority(a4.Certificate);
      vs.AddAuthority(a5.Certificate);

      a1.Prepare(1, vs.Parameters);
      a2.Prepare(2, vs.Parameters);
      a3.Prepare(3, vs.Parameters);
      a4.Prepare(4, vs.Parameters);
      a5.Prepare(5, vs.Parameters);

      a1.SetAuthorities(vs.Authorities);
      a2.SetAuthorities(vs.Authorities);
      a3.SetAuthorities(vs.Authorities);
      a4.SetAuthorities(vs.Authorities);
      a5.SetAuthorities(vs.Authorities);

      vs.DepositShares(a1.GetShares());
      vs.DepositShares(a2.GetShares());
      vs.DepositShares(a3.GetShares());
      vs.DepositShares(a4.GetShares());
      vs.DepositShares(a5.GetShares());

      var r1 = a1.VerifyShares(vs.GetAllShares());
      var r2 = a2.VerifyShares(vs.GetAllShares());
      var r3 = a3.VerifyShares(vs.GetAllShares());
      var r4 = a4.VerifyShares(vs.GetAllShares());
      var r5 = a5.VerifyShares(vs.GetAllShares());

      vs.DepositShareResponse(r1);
      vs.DepositShareResponse(r2);
      vs.DepositShareResponse(r3);
      vs.DepositShareResponse(r4);
      vs.DepositShareResponse(r5);

      VoterEntity v1 = new VoterEntity(11, "Hans");
      VoterEntity v2 = new VoterEntity(22, "Urs");
      VoterEntity v3 = new VoterEntity(33, "Käti");
      VoterEntity v4 = new VoterEntity(44, "Lisi");
      VoterEntity v5 = new VoterEntity(55, "Dani");

      var vote1 = v1.Vote(vs.GetVotingMaterial(), new int[] { 0, 1 });
      var vote2 = v2.Vote(vs.GetVotingMaterial(), new int[] { 0, 1 });
      var vote3 = v3.Vote(vs.GetVotingMaterial(), new int[] { 0, 1 });
      var vote4 = v4.Vote(vs.GetVotingMaterial(), new int[] { 0, 1 });
      var vote5 = v5.Vote(vs.GetVotingMaterial(), new int[] { 1, 0 });

      vs.Vote(vote1);
      vs.Vote(vote2);
      vs.Vote(vote3);
      vs.Vote(vote4);
      vs.Vote(vote5);

      vs.EndVote();

      var pd1 = a1.PartiallyDecipher(vs.GetAllBallots());
      var pd2 = a2.PartiallyDecipher(vs.GetAllBallots());
      var pd3 = a3.PartiallyDecipher(vs.GetAllBallots());
      var pd4 = a4.PartiallyDecipher(vs.GetAllBallots());
      var pd5 = a5.PartiallyDecipher(vs.GetAllBallots());

      vs.DepositPartialDecipher(pd1);
      vs.DepositPartialDecipher(pd2);
      vs.DepositPartialDecipher(pd3);
      vs.DepositPartialDecipher(pd4);
      vs.DepositPartialDecipher(pd5);

      var res1 = v1.Result(vs.GetVotingResult());
      var res2 = v2.Result(vs.GetVotingResult());
      var res3 = v3.Result(vs.GetVotingResult());
      var res4 = v4.Result(vs.GetVotingResult());
      var res5 = v5.Result(vs.GetVotingResult());
    }

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
      Parameters parameters = new Parameters();
      parameters.InitilizeCrypto(prime, safePrime, 3, 5, 2, 1, 100);
      Authority[] auths = new Authority[5];

      for (int aI = 0; aI < parameters.AuthorityCount; aI++)
      {
        auths[aI] = new Authority(aI + 1, parameters);
      }

      foreach (Authority a in auths)
      {
        a.CreatePolynomial();
      }

      foreach (Authority a in auths)
      {
        List<Share> shares = new List<Share>();
        List<List<VerificationValue>> As = new List<List<VerificationValue>>();

        foreach (Authority b in auths)
        {
          shares.Add(b.Share(a.Index));
          As.Add(new List<VerificationValue>());

          for (int l = 0; l <= parameters.Thereshold; l++)
          {
            As[As.Count - 1].Add(b.VerificationValue(l));
          }
        }

        a.VerifySharing(shares, As);
      }

      BigInt publicKey = new BigInt(1);
      foreach (Authority a in auths)
      {
        publicKey = (publicKey * a.PublicKeyPart).Mod(parameters.P);
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
        auths.Foreach(authority => partialDeciphers.AddRange(authority.PartialDeciphers(sum, optionIndex)));

        IEnumerable<BigInt> partialDeciphers0 = partialDeciphers
          .Where(partialDecipher => partialDecipher.GroupIndex == 1)
          .Select(partialDecipher => partialDecipher.Value);
        int v0 = sum.Decrypt(partialDeciphers0, parameters);

        IEnumerable<BigInt> partialDeciphers1 = partialDeciphers
          .Where(partialDecipher => partialDecipher.GroupIndex == 1)
          .Select(partialDecipher => partialDecipher.Value);
        int v1 = sum.Decrypt(partialDeciphers1, parameters);

        IEnumerable<BigInt> partialDeciphers2 = partialDeciphers
          .Where(partialDecipher => partialDecipher.GroupIndex == 1)
          .Select(partialDecipher => partialDecipher.Value);
        int v2 = sum.Decrypt(partialDeciphers2, parameters);

        IEnumerable<BigInt> partialDeciphers3 = partialDeciphers
          .Where(partialDecipher => partialDecipher.GroupIndex == 1)
          .Select(partialDecipher => partialDecipher.Value);
        int v3 = sum.Decrypt(partialDeciphers3, parameters);

        IEnumerable<BigInt> partialDeciphers4 = partialDeciphers
          .Where(partialDecipher => partialDecipher.GroupIndex == 1)
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
