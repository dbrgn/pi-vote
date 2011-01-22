using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pirate.PiVote;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.UnitTest
{
  [TestClass]
  public class CertificateTest
  {
    public CertificateTest()
    {
    }

    [TestInitialize()]
    public void MyTestInitialize()
    {
    }

    [TestCleanup()]
    public void MyTestCleanup()
    {
    }

    [TestMethod]
    public void ChainTest()
    {
      CertificateStorage storage = new CertificateStorage();

      CACertificate root = new CACertificate(null, "Root");
      root.CreateSelfSignature();
      Assert.AreEqual(CertificateValidationResult.NoSignature, root.Validate(storage));

      storage.AddRoot(root.OnlyPublicPart);
      Assert.AreEqual(CertificateValidationResult.Valid, root.Validate(storage));

      var rootCrl = new RevocationList(root.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[]{});
      var signedRootCrl = new Signed<RevocationList>(rootCrl, root);
      storage.AddRevocationList(signedRootCrl);

      CACertificate intermediate = new CACertificate(null, "Intermediate");
      intermediate.CreateSelfSignature();
      Assert.AreEqual(CertificateValidationResult.NoSignature, intermediate.Validate(storage));

      intermediate.AddSignature(root, DateTime.Now.AddDays(1));
      storage.Add(intermediate.OnlyPublicPart);
      Assert.AreEqual(CertificateValidationResult.Valid, intermediate.Validate(storage));

      var intermediateCrl = new RevocationList(intermediate.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedIntermediateCrl = new Signed<RevocationList>(intermediateCrl, intermediate);
      storage.AddRevocationList(signedIntermediateCrl);

      AdminCertificate test = new AdminCertificate(Language.English, null, "Test");
      test.CreateSelfSignature();
      Assert.AreEqual(CertificateValidationResult.NoSignature, test.Validate(storage));

      test.AddSignature(intermediate, DateTime.Now.AddDays(1));
      Assert.AreEqual(CertificateValidationResult.Valid, test.Validate(storage));
    }

    [TestMethod]
    public void DataTest()
    {
      CertificateStorage storage = new CertificateStorage();

      CACertificate root = new CACertificate(null, "Root");
      root.CreateSelfSignature();
      storage.AddRoot(root.OnlyPublicPart);

      var rootCrl = new RevocationList(root.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedRootCrl = new Signed<RevocationList>(rootCrl, root);
      storage.AddRevocationList(signedRootCrl);

      CACertificate intermediate = new CACertificate(null, "Intermediate");
      intermediate.CreateSelfSignature();
      intermediate.AddSignature(root, DateTime.Now.AddDays(1));
      storage.Add(intermediate.OnlyPublicPart);

      var intermediateCrl = new RevocationList(intermediate.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedIntermediateCrl = new Signed<RevocationList>(intermediateCrl, intermediate);
      storage.AddRevocationList(signedIntermediateCrl);

      AdminCertificate test = new AdminCertificate(Language.English, null, "Test");
      test.CreateSelfSignature();
      test.AddSignature(intermediate, DateTime.Now.AddDays(1));

      byte[] data = test.ToBinary();
      data[data.Length - 3]++;
      AdminCertificate other = Serializable.FromBinary<AdminCertificate>(data);
      Assert.AreEqual(CertificateValidationResult.SelfsignatureInvalid, other.Validate(storage));
    }

    [TestMethod]
    public void TimeTest()
    {
      CertificateStorage storage = new CertificateStorage();

      CACertificate root = new CACertificate(null, "Root");
      root.CreateSelfSignature();
      storage.AddRoot(root.OnlyPublicPart);

      var rootCrl = new RevocationList(root.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedRootCrl = new Signed<RevocationList>(rootCrl, root);
      storage.AddRevocationList(signedRootCrl);

      CACertificate intermediate = new CACertificate(null, "Intermediate");
      intermediate.CreateSelfSignature();
      intermediate.AddSignature(root, DateTime.Now.AddDays(1));
      storage.Add(intermediate.OnlyPublicPart);

      var intermediateCrl = new RevocationList(intermediate.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedIntermediateCrl = new Signed<RevocationList>(intermediateCrl, intermediate);
      storage.AddRevocationList(signedIntermediateCrl);

      AdminCertificate test = new AdminCertificate(Language.English, null, "Test");
      test.CreateSelfSignature();
      test.AddSignature(intermediate, DateTime.Now.AddDays(1));

      Assert.AreEqual(CertificateValidationResult.Valid, test.Validate(storage, DateTime.Now));
      Assert.AreEqual(CertificateValidationResult.Valid, test.Validate(storage, DateTime.Now.AddMinutes(1)));
      Assert.AreEqual(CertificateValidationResult.Valid, test.Validate(storage, DateTime.Now.AddHours(1)));
      Assert.AreEqual(CertificateValidationResult.Valid, test.Validate(storage, DateTime.Now.AddDays(1)));

      Assert.AreEqual(CertificateValidationResult.Outdated, test.Validate(storage, DateTime.Now.AddDays(2)));
      Assert.AreEqual(CertificateValidationResult.Outdated, test.Validate(storage, DateTime.Now.AddDays(5)));
      Assert.AreEqual(CertificateValidationResult.Outdated, test.Validate(storage, DateTime.Now.AddDays(30)));
      Assert.AreEqual(CertificateValidationResult.Outdated, test.Validate(storage, DateTime.Now.AddYears(1)));
      Assert.AreEqual(CertificateValidationResult.Outdated, test.Validate(storage, DateTime.Now.AddYears(5)));

      Assert.AreEqual(CertificateValidationResult.NotYetValid, test.Validate(storage, DateTime.Now.AddDays(-1)));
      Assert.AreEqual(CertificateValidationResult.NotYetValid, test.Validate(storage, DateTime.Now.AddDays(-5)));
      Assert.AreEqual(CertificateValidationResult.NotYetValid, test.Validate(storage, DateTime.Now.AddDays(-30)));
      Assert.AreEqual(CertificateValidationResult.NotYetValid, test.Validate(storage, DateTime.Now.AddYears(-1)));
      Assert.AreEqual(CertificateValidationResult.NotYetValid, test.Validate(storage, DateTime.Now.AddYears(-5)));
    }

    [TestMethod]
    public void RevocationTest()
    {
      CertificateStorage storage = new CertificateStorage();

      CACertificate root = new CACertificate(null, "Root");
      root.CreateSelfSignature();
      storage.AddRoot(root.OnlyPublicPart);

      var rootCrl = new RevocationList(root.Id, DateTime.Now, DateTime.Now.AddYears(10), new Guid[] { });
      var signedRootCrl = new Signed<RevocationList>(rootCrl, root);
      storage.AddRevocationList(signedRootCrl);

      CACertificate intermediate = new CACertificate(null, "Intermediate");
      intermediate.CreateSelfSignature();
      intermediate.AddSignature(root, DateTime.Now.AddYears(10));
      storage.Add(intermediate.OnlyPublicPart);

      AdminCertificate test = new AdminCertificate(Language.English, null, "Test");
      test.CreateSelfSignature();
      test.AddSignature(intermediate, DateTime.Now.AddYears(10));

      for (int startDay = 0; startDay < 10; startDay += 2)
      {
        DateTime validFrom = DateTime.Now.AddDays(startDay);
        DateTime validUntil = validFrom.AddDays(1);
        IEnumerable<Guid> revoked = startDay > 5 ? new Guid[] { test.Id } : new Guid[] { };
        var intermediateCrl = new RevocationList(intermediate.Id, validFrom, validUntil, revoked);
        var signedIntermediateCrl = new Signed<RevocationList>(intermediateCrl, intermediate);
        storage.AddRevocationList(signedIntermediateCrl);
      }

      Assert.AreEqual(CertificateValidationResult.Valid, test.Validate(storage, DateTime.Now));
      Assert.AreEqual(CertificateValidationResult.Valid, test.Validate(storage, DateTime.Now.AddDays(1)));
      Assert.AreEqual(CertificateValidationResult.Valid, test.Validate(storage, DateTime.Now.AddDays(2)));
      Assert.AreEqual(CertificateValidationResult.Valid, test.Validate(storage, DateTime.Now.AddDays(3)));
      Assert.AreEqual(CertificateValidationResult.Valid, test.Validate(storage, DateTime.Now.AddDays(4)));
      Assert.AreEqual(CertificateValidationResult.Valid, test.Validate(storage, DateTime.Now.AddDays(5)));

      Assert.AreEqual(CertificateValidationResult.Revoked, test.Validate(storage, DateTime.Now.AddDays(6)));
      Assert.AreEqual(CertificateValidationResult.Revoked, test.Validate(storage, DateTime.Now.AddDays(7)));
      Assert.AreEqual(CertificateValidationResult.Revoked, test.Validate(storage, DateTime.Now.AddDays(8)));
      Assert.AreEqual(CertificateValidationResult.Revoked, test.Validate(storage, DateTime.Now.AddDays(9)));
      Assert.AreEqual(CertificateValidationResult.Revoked, test.Validate(storage, DateTime.Now.AddDays(10)));
      Assert.AreEqual(CertificateValidationResult.Revoked, test.Validate(storage, DateTime.Now.AddDays(11)));
      Assert.AreEqual(CertificateValidationResult.Revoked, test.Validate(storage, DateTime.Now.AddDays(12)));
      Assert.AreEqual(CertificateValidationResult.Revoked, test.Validate(storage, DateTime.Now.AddDays(30)));
      Assert.AreEqual(CertificateValidationResult.Revoked, test.Validate(storage, DateTime.Now.AddYears(1)));
      Assert.AreEqual(CertificateValidationResult.Revoked, test.Validate(storage, DateTime.Now.AddYears(5)));
    }

    [TestMethod]
    public void SignerUnknownTest()
    {
      CertificateStorage storage = new CertificateStorage();

      CACertificate root = new CACertificate(null, "Root");
      root.CreateSelfSignature();
      storage.AddRoot(root.OnlyPublicPart);

      var rootCrl = new RevocationList(root.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedRootCrl = new Signed<RevocationList>(rootCrl, root);
      storage.AddRevocationList(signedRootCrl);

      CACertificate intermediate = new CACertificate(null, "Intermediate");
      intermediate.CreateSelfSignature();
      intermediate.AddSignature(root, DateTime.Now.AddDays(1));

      var intermediateCrl = new RevocationList(intermediate.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedIntermediateCrl = new Signed<RevocationList>(intermediateCrl, intermediate);
      storage.AddRevocationList(signedIntermediateCrl);

      AdminCertificate test = new AdminCertificate(Language.English, null, "Test");
      test.CreateSelfSignature();
      test.AddSignature(intermediate, DateTime.Now.AddDays(1));

      Assert.AreEqual(CertificateValidationResult.UnknownSigner, test.Validate(storage));
    }

    [TestMethod]
    public void SignerInvalidTest()
    {
      CertificateStorage storage = new CertificateStorage();

      CACertificate root = new CACertificate(null, "Root");
      root.CreateSelfSignature();
      storage.AddRoot(root.OnlyPublicPart);

      var rootCrl0 = new RevocationList(root.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedRootCrl0 = new Signed<RevocationList>(rootCrl0, root);
      storage.AddRevocationList(signedRootCrl0);

      CACertificate intermediate = new CACertificate(null, "Intermediate");
      intermediate.CreateSelfSignature();
      intermediate.AddSignature(root, DateTime.Now.AddDays(4));
      storage.Add(intermediate.OnlyPublicPart);

      var rootCrl1 = new RevocationList(root.Id, DateTime.Now.AddDays(2), DateTime.Now.AddDays(3), new Guid[] { });
      rootCrl1.RevokedCertificates.Add(intermediate.Id);
      var signedRootCrl1 = new Signed<RevocationList>(rootCrl1, root);
      storage.AddRevocationList(signedRootCrl1);

      var intermediateCrl = new RevocationList(intermediate.Id, DateTime.Now, DateTime.Now.AddDays(4), new Guid[] { });
      var signedIntermediateCrl = new Signed<RevocationList>(intermediateCrl, intermediate);
      storage.AddRevocationList(signedIntermediateCrl);

      AdminCertificate test = new AdminCertificate(Language.English, null, "Test");
      test.CreateSelfSignature();
      test.AddSignature(intermediate, DateTime.Now.AddDays(4));

      Assert.AreEqual(CertificateValidationResult.Valid, test.Validate(storage, DateTime.Now.AddDays(1)));

      Assert.AreEqual(CertificateValidationResult.SignerInvalid, test.Validate(storage, DateTime.Now.AddDays(2)));
    }

    [TestMethod]
    public void PassphraseTest()
    {
      var root = new CACertificate("test", "Root");
      root.CreateSelfSignature();
      root.Lock();

      root.Unlock("test");

      root.Sign(Encoding.UTF8.GetBytes("hello"));

      root.Lock();
    }

    [TestMethod]
    [ExpectedException(typeof(System.Security.Cryptography.CryptographicException))]
    public void BadPassphraseTest()
    {
      var root = new CACertificate("test", "Root");
      root.CreateSelfSignature();
      root.Lock();

      root.Unlock("other");
    }

    [TestMethod]
    [ExpectedException(typeof(InvalidOperationException))]
    public void NoPassphraseTest()
    {
      var root = new CACertificate("test", "Root");
      root.CreateSelfSignature();
      root.Lock();

      root.Sign(Encoding.UTF8.GetBytes("hello"));
    }
  }
}
