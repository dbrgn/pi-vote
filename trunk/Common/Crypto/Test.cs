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
using Pirate.PiVote.Serialization;
using System.Security.Cryptography;

namespace Pirate.PiVote.Crypto
{
  public class Test
  {
    /// <summary>
    /// Voting entity test.
    /// </summary>
    /// <remarks>
    /// Used only during development.
    /// </remarks>
    public void EntityTest()
    {
      DateTime validUntil = DateTime.Now.AddDays(1);
      var root = new CACertificate("Root");
      root.CreateSelfSignature();
      var rootCrl = new RevocationList(root.Id, DateTime.Now, validUntil, new List<Guid>());
      var sigRootCrl = new Signed<RevocationList>(rootCrl, root);

      var intermediate = new CACertificate("Intermediate");
      intermediate.CreateSelfSignature();
      intermediate.AddSignature(root, validUntil);
      var intCrl = new RevocationList(intermediate.Id, DateTime.Now, validUntil, new List<Guid>());
      var sigIntCrl = new Signed<RevocationList>(intCrl, intermediate);

      VotingParameters pc = new VotingParameters("Zufrieden");
      pc.SetId(27);
      pc.AddOption(new Option("Nein", "Dagegen"));
      pc.AddOption(new Option("Ja", "Dafür"));
      pc.Initialize(1);

      DateTime start = DateTime.Now;
      Console.WriteLine();
      Console.Write("Voting begins...");

      CertificateStorage serverCertStorage = new CertificateStorage();
      serverCertStorage.AddRoot(root);
      serverCertStorage.Add(intermediate);
      serverCertStorage.SetRevocationList(sigRootCrl);
      serverCertStorage.SetRevocationList(sigIntCrl);
      VotingServerEntity vs = new VotingServerEntity(pc, serverCertStorage);

      var a1c = new AuthorityCertificate("Authority 1");
      a1c.CreateSelfSignature();
      a1c.AddSignature(intermediate, validUntil);
      var a2c = new AuthorityCertificate("Authority 2");
      a2c.CreateSelfSignature();
      a2c.AddSignature(intermediate, validUntil);
      var a3c = new AuthorityCertificate("Authority 3");
      a3c.CreateSelfSignature();
      a3c.AddSignature(intermediate, validUntil);
      var a4c = new AuthorityCertificate("Authority 4");
      a4c.CreateSelfSignature();
      a4c.AddSignature(intermediate, validUntil);
      var a5c = new AuthorityCertificate("Authority 5");
      a5c.CreateSelfSignature();
      a5c.AddSignature(intermediate, validUntil);

      var a1 = new AuthorityEntity(root, a1c);
      var a2 = new AuthorityEntity(root, a2c);
      var a3 = new AuthorityEntity(root, a3c);
      var a4 = new AuthorityEntity(root, a4c);
      var a5 = new AuthorityEntity(root, a5c);

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

      var v1c = new VoterCertificate();
      v1c.CreateSelfSignature();
      v1c.AddSignature(intermediate, validUntil);

      var v1 = new VoterEntity(root, v1c);

      var vote1 = v1.Vote(vs.GetVotingMaterial(), new int[] { 0, 1 });

      vs.Vote(vote1);

      int voters = 10;

      for (int i = 1000; i < 1000 + voters; i++)
      {
        var vc = new VoterCertificate();
        vc.CreateSelfSignature();
        vc.AddSignature(intermediate, validUntil);

        var vx = new VoterEntity(root, vc);

        var votex = vx.Vote(vs.GetVotingMaterial(), new int[] { 0, 1 });

        vs.Vote(votex);
      }

      for (int i = 2000; i < 2000 + voters; i++)
      {
        var vc = new VoterCertificate();
        vc.CreateSelfSignature();
        vc.AddSignature(intermediate, validUntil);

        var vx = new VoterEntity(root, vc);

        var votex = vx.Vote(vs.GetVotingMaterial(), new int[] { 1, 0 });

        vs.Vote(votex);
      }

      vs.EndVote();

      a1.TallyBegin(vs.GetVotingMaterial());
      a2.TallyBegin(vs.GetVotingMaterial());
      a3.TallyBegin(vs.GetVotingMaterial());
      a4.TallyBegin(vs.GetVotingMaterial());
      a5.TallyBegin(vs.GetVotingMaterial());

      for (int envelopeIndex = 0; envelopeIndex < vs.GetEnvelopeCount(); envelopeIndex++)
      {
        a1.TallyAdd(vs.GetEnvelope(envelopeIndex));
        a2.TallyAdd(vs.GetEnvelope(envelopeIndex));
        a3.TallyAdd(vs.GetEnvelope(envelopeIndex));
        a4.TallyAdd(vs.GetEnvelope(envelopeIndex));
        a5.TallyAdd(vs.GetEnvelope(envelopeIndex));
      }

      var pd1 = a1.PartiallyDecipher();
      var pd2 = a2.PartiallyDecipher();
      var pd3 = a3.PartiallyDecipher();
      var pd4 = a4.PartiallyDecipher();
      var pd5 = a5.PartiallyDecipher();

      vs.DepositPartialDecipher(pd1);
      vs.DepositPartialDecipher(pd2);
      vs.DepositPartialDecipher(pd3);
      vs.DepositPartialDecipher(pd4);
      vs.DepositPartialDecipher(pd5);

      v1.TallyBegin(vs.GetVotingMaterial());

      for (int envelopeIndex = 0; envelopeIndex < vs.GetEnvelopeCount(); envelopeIndex++)
      {
        v1.TallyAdd(vs.GetEnvelope(envelopeIndex));
      }

      for (int authorityIndex = 1; authorityIndex < vs.Parameters.AuthorityCount + 1; authorityIndex++)
      {
        v1.TallyAddPartialDecipher(vs.GetPartialDecipher(authorityIndex));
      }

      var res1 = v1.TallyResult;

      TimeSpan duration = DateTime.Now.Subtract(start);
      Console.WriteLine("Succeded {0}", duration.ToString());
    }

    /// <summary>
    /// Crypto base test.
    /// </summary>
    /// <remarks>
    /// Only used during development.
    /// </remarks>
    public void BaseTest()
    {
      BigInt prime = Prime.Find(80);
      BigInt safePrime = Prime.FindSafe(88);

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
          throw new Exception("Everything ok.");
        }
        else
        {
          throw new Exception("Bad vote.");
        }
      }
    }
  }
}
