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
      var root = new CACertificate(null, "Root");
      root.CreateSelfSignature();
      var rootCrl = new RevocationList(root.Id, DateTime.Now, validUntil, new List<Guid>());
      var sigRootCrl = new Signed<RevocationList>(rootCrl, root);

      var intermediate = new CACertificate(null, "Intermediate");
      intermediate.CreateSelfSignature();
      intermediate.AddSignature(root, validUntil);
      var intCrl = new RevocationList(intermediate.Id, DateTime.Now, validUntil, new List<Guid>());
      var sigIntCrl = new Signed<RevocationList>(intCrl, intermediate);

      var admin = new AdminCertificate(Language.English, null, "Admin");
      admin.CreateSelfSignature();
      admin.AddSignature(intermediate, DateTime.Now.AddDays(1));

      var serverCert = new ServerCertificate("Server");
      serverCert.CreateSelfSignature();
      serverCert.AddSignature(intermediate, DateTime.Now.AddDays(1));

      VotingParameters parameters = 
        new VotingParameters(
          new MultiLanguageString("Zufrieden"), 
          new MultiLanguageString("Tada"), 
          DateTime.Now, 
          DateTime.Now.AddDays(1),
          Canton.None);

      Question question = new Question(new MultiLanguageString("Zufrieden?"), new MultiLanguageString(string.Empty), 1);
      question.AddOption(new Option(new MultiLanguageString("Nein"), new MultiLanguageString("Dagegen")));
      question.AddOption(new Option(new MultiLanguageString("Ja"), new MultiLanguageString("Dafür")));
      parameters.AddQuestion(question);

      Signed<VotingParameters> signedParameters = new Signed<VotingParameters>(parameters, admin);

      DateTime start = DateTime.Now;
      Console.WriteLine();
      Console.Write("Voting begins...");

      CertificateStorage serverCertStorage = new CertificateStorage();
      serverCertStorage.AddRoot(root);
      serverCertStorage.Add(intermediate);
      serverCertStorage.AddRevocationList(sigRootCrl);
      serverCertStorage.AddRevocationList(sigIntCrl);

      VotingServerEntity vs = new VotingServerEntity(null, signedParameters, serverCertStorage, serverCert);

      var a1c = new AuthorityCertificate(Language.English, "Authority 1", null);
      a1c.CreateSelfSignature();
      a1c.AddSignature(intermediate, validUntil);
      var a2c = new AuthorityCertificate(Language.English, "Authority 2", null);
      a2c.CreateSelfSignature();
      a2c.AddSignature(intermediate, validUntil);
      var a3c = new AuthorityCertificate(Language.English, "Authority 3", null);
      a3c.CreateSelfSignature();
      a3c.AddSignature(intermediate, validUntil);
      var a4c = new AuthorityCertificate(Language.English, "Authority 4", null);
      a4c.CreateSelfSignature();
      a4c.AddSignature(intermediate, validUntil);
      var a5c = new AuthorityCertificate(Language.English, "Authority 5", null);
      a5c.CreateSelfSignature();
      a5c.AddSignature(intermediate, validUntil);

      var a1 = new AuthorityEntity(serverCertStorage, a1c);
      var a2 = new AuthorityEntity(serverCertStorage, a2c);
      var a3 = new AuthorityEntity(serverCertStorage, a3c);
      var a4 = new AuthorityEntity(serverCertStorage, a4c);
      var a5 = new AuthorityEntity(serverCertStorage, a5c);

      vs.AddAuthority(a1.Certificate);
      vs.AddAuthority(a2.Certificate);
      vs.AddAuthority(a3.Certificate);
      vs.AddAuthority(a4.Certificate);
      vs.AddAuthority(a5.Certificate);

      a1.Prepare(1, vs.SignedParameters);
      a2.Prepare(2, vs.SignedParameters);
      a3.Prepare(3, vs.SignedParameters);
      a4.Prepare(4, vs.SignedParameters);
      a5.Prepare(5, vs.SignedParameters);

      a1.SetAuthorities(vs.AuthorityList);
      a2.SetAuthorities(vs.AuthorityList);
      a3.SetAuthorities(vs.AuthorityList);
      a4.SetAuthorities(vs.AuthorityList);
      a5.SetAuthorities(vs.AuthorityList);

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

      var v1c = new VoterCertificate(Language.English, null, Canton.None);
      v1c.CreateSelfSignature();
      v1c.AddSignature(intermediate, validUntil);

      var cs = new CertificateStorage();
      cs.AddRoot(root);
      var v1 = new VoterEntity(cs);

      IEnumerable<int> questionVota = new int[] { 0, 1 };

      var vote1 = v1.Vote(vs.GetVotingMaterial(), v1c, new IEnumerable<int>[] { questionVota }, null);

      vs.Vote(vote1);

      int voters = 10;

      for (int i = 1000; i < 1000 + voters; i++)
      {
        var vc = new VoterCertificate(Language.English, null, Canton.None);
        vc.CreateSelfSignature();
        vc.AddSignature(intermediate, validUntil);

        var vx = new VoterEntity(cs);

        IEnumerable<int> questionVota2 = new int[] { 0, 1 };
        var votex = vx.Vote(vs.GetVotingMaterial(), vc, new IEnumerable<int>[] { questionVota2 }, null);

        vs.Vote(votex);
      }

      for (int i = 2000; i < 2000 + voters; i++)
      {
        var vc = new VoterCertificate(Language.English, null, Canton.None);
        vc.CreateSelfSignature();
        vc.AddSignature(intermediate, validUntil);

        var vx = new VoterEntity(cs);

        IEnumerable<int> questionVota3 = new int[] { 1, 0 };
        var votex = vx.Vote(vs.GetVotingMaterial(), vc, new IEnumerable<int>[] { questionVota3 }, null);

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
        a1.TallyAdd(envelopeIndex, vs.GetEnvelope(envelopeIndex), new Progress(null));
        a2.TallyAdd(envelopeIndex, vs.GetEnvelope(envelopeIndex), new Progress(null));
        a3.TallyAdd(envelopeIndex, vs.GetEnvelope(envelopeIndex), new Progress(null));
        a4.TallyAdd(envelopeIndex, vs.GetEnvelope(envelopeIndex), new Progress(null));
        a5.TallyAdd(envelopeIndex, vs.GetEnvelope(envelopeIndex), new Progress(null));
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
        v1.TallyAdd(envelopeIndex, vs.GetEnvelope(envelopeIndex), new Progress(null));
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

      BaseParameters parameters = new BaseParameters();

      Question question = new Question(new MultiLanguageString(string.Empty), new MultiLanguageString(string.Empty), 1);
      question.AddOption(new Option(new MultiLanguageString("Ja"), new MultiLanguageString(string.Empty)));
      question.AddOption(new Option(new MultiLanguageString("Nein"), new MultiLanguageString(string.Empty)));
      parameters.AddQuestion(question);

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
      ballots.Add(new Ballot(new int[] { 0, 1 }, parameters, question, publicKey, null));
      ballots.Add(new Ballot(new int[] { 0, 1 }, parameters, question, publicKey, null));
      ballots.Add(new Ballot(new int[] { 1, 0 }, parameters, question, publicKey, null));
      ballots.Add(new Ballot(new int[] { 0, 1 }, parameters, question, publicKey, null));
      ballots.Add(new Ballot(new int[] { 1, 0 }, parameters, question, publicKey, null));
      ballots.Add(new Ballot(new int[] { 0, 1 }, parameters, question, publicKey, null));

      if (!ballots.All(ballot => ballot.Verify(publicKey, parameters, question, new Progress(null))))
        throw new Exception("Bad proof.");

      for (int optionIndex = 0; optionIndex < question.Options.Count(); optionIndex++)
      {
        IEnumerable<Vote> votes = ballots.Select(ballot => ballot.Votes[optionIndex]);

        Vote sum = null;
        votes.Foreach(vote => sum = sum == null ? vote : sum + vote);

        List<PartialDecipher> partialDeciphers = new List<PartialDecipher>();
        auths.Foreach(authority => partialDeciphers.AddRange(authority.PartialDeciphers(sum, 0, optionIndex)));

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
