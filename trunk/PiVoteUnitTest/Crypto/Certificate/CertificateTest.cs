using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pirate.PiVote;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace PiVoteUnitTest
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

      CACertificate root = new CACertificate("Root");
      root.CreateSelfSignature();
      Assert.IsFalse(root.Validate(storage) == CertificateValidationResult.Valid);

      storage.AddRoot(root.OnlyPublicPart);
      Assert.IsTrue(root.Validate(storage) == CertificateValidationResult.Valid);

      var rootCrl = new RevocationList(root.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[]{});
      var signedRootCrl = new Signed<RevocationList>(rootCrl, root);
      storage.AddRevocationList(signedRootCrl);

      CACertificate intermediate = new CACertificate("Intermediate");
      intermediate.CreateSelfSignature();
      Assert.IsFalse(intermediate.Validate(storage) == CertificateValidationResult.Valid);

      intermediate.AddSignature(root, DateTime.Now.AddDays(1));
      storage.Add(intermediate.OnlyPublicPart);
      Assert.IsTrue(intermediate.Validate(storage) == CertificateValidationResult.Valid);

      var intermediateCrl = new RevocationList(intermediate.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedIntermediateCrl = new Signed<RevocationList>(intermediateCrl, intermediate);
      storage.AddRevocationList(signedIntermediateCrl);

      AdminCertificate test = new AdminCertificate(Language.English, "Test");
      test.CreateSelfSignature();
      Assert.IsFalse(test.Validate(storage) == CertificateValidationResult.Valid);

      test.AddSignature(intermediate, DateTime.Now.AddDays(1));
      Assert.IsTrue(test.Validate(storage) == CertificateValidationResult.Valid);
    }

    [TestMethod]
    public void DataTest()
    {
      CertificateStorage storage = new CertificateStorage();

      CACertificate root = new CACertificate("Root");
      root.CreateSelfSignature();
      storage.AddRoot(root.OnlyPublicPart);

      var rootCrl = new RevocationList(root.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedRootCrl = new Signed<RevocationList>(rootCrl, root);
      storage.AddRevocationList(signedRootCrl);

      CACertificate intermediate = new CACertificate("Intermediate");
      intermediate.CreateSelfSignature();
      intermediate.AddSignature(root, DateTime.Now.AddDays(1));
      storage.Add(intermediate.OnlyPublicPart);

      var intermediateCrl = new RevocationList(intermediate.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedIntermediateCrl = new Signed<RevocationList>(intermediateCrl, intermediate);
      storage.AddRevocationList(signedIntermediateCrl);

      AdminCertificate test = new AdminCertificate(Language.English, "Test");
      test.CreateSelfSignature();
      test.AddSignature(intermediate, DateTime.Now.AddDays(1));

      byte[] data = test.ToBinary();
      data[data.Length - 3]++;
      AdminCertificate other = Serializable.FromBinary<AdminCertificate>(data);
      Assert.IsFalse(other.Validate(storage) == CertificateValidationResult.Valid);
    }

    [TestMethod]
    public void TimeTest()
    {
      CertificateStorage storage = new CertificateStorage();

      CACertificate root = new CACertificate("Root");
      root.CreateSelfSignature();
      storage.AddRoot(root.OnlyPublicPart);

      var rootCrl = new RevocationList(root.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedRootCrl = new Signed<RevocationList>(rootCrl, root);
      storage.AddRevocationList(signedRootCrl);

      CACertificate intermediate = new CACertificate("Intermediate");
      intermediate.CreateSelfSignature();
      intermediate.AddSignature(root, DateTime.Now.AddDays(1));
      storage.Add(intermediate.OnlyPublicPart);

      var intermediateCrl = new RevocationList(intermediate.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedIntermediateCrl = new Signed<RevocationList>(intermediateCrl, intermediate);
      storage.AddRevocationList(signedIntermediateCrl);

      AdminCertificate test = new AdminCertificate(Language.English, "Test");
      test.CreateSelfSignature();
      test.AddSignature(intermediate, DateTime.Now.AddDays(1));

      Assert.IsTrue(test.Validate(storage, DateTime.Now) == CertificateValidationResult.Valid);
      Assert.IsTrue(test.Validate(storage, DateTime.Now.AddMinutes(1)) == CertificateValidationResult.Valid);
      Assert.IsTrue(test.Validate(storage, DateTime.Now.AddHours(1)) == CertificateValidationResult.Valid);
      Assert.IsTrue(test.Validate(storage, DateTime.Now.AddDays(1)) == CertificateValidationResult.Valid);

      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddDays(2)) == CertificateValidationResult.Valid);
      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddDays(5)) == CertificateValidationResult.Valid);
      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddDays(30)) == CertificateValidationResult.Valid);
      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddYears(1)) == CertificateValidationResult.Valid);
      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddYears(5)) == CertificateValidationResult.Valid);

      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddDays(-1)) == CertificateValidationResult.Valid);
      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddDays(-5)) == CertificateValidationResult.Valid);
      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddDays(-30)) == CertificateValidationResult.Valid);
      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddYears(-1)) == CertificateValidationResult.Valid);
      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddYears(-5)) == CertificateValidationResult.Valid);
    }

    [TestMethod]
    public void RevocationTest()
    {
      CertificateStorage storage = new CertificateStorage();

      CACertificate root = new CACertificate("Root");
      root.CreateSelfSignature();
      storage.AddRoot(root.OnlyPublicPart);

      var rootCrl = new RevocationList(root.Id, DateTime.Now, DateTime.Now.AddDays(10), new Guid[] { });
      var signedRootCrl = new Signed<RevocationList>(rootCrl, root);
      storage.AddRevocationList(signedRootCrl);

      CACertificate intermediate = new CACertificate("Intermediate");
      intermediate.CreateSelfSignature();
      intermediate.AddSignature(root, DateTime.Now.AddDays(10));
      storage.Add(intermediate.OnlyPublicPart);

      AdminCertificate test = new AdminCertificate(Language.English, "Test");
      test.CreateSelfSignature();
      test.AddSignature(intermediate, DateTime.Now.AddDays(10));

      for (int startDay = 0; startDay < 10; startDay += 2)
      {
        DateTime validFrom = DateTime.Now.AddDays(startDay);
        DateTime validUntil = validFrom.AddDays(1);
        IEnumerable<Guid> revoked = startDay > 5 ? new Guid[] { test.Id } : new Guid[] { };
        var intermediateCrl = new RevocationList(intermediate.Id, validFrom, validUntil, revoked);
        var signedIntermediateCrl = new Signed<RevocationList>(intermediateCrl, intermediate);
        storage.AddRevocationList(signedIntermediateCrl);
      }

      Assert.IsTrue(test.Validate(storage, DateTime.Now) == CertificateValidationResult.Valid);
      Assert.IsTrue(test.Validate(storage, DateTime.Now.AddDays(1)) == CertificateValidationResult.Valid);
      Assert.IsTrue(test.Validate(storage, DateTime.Now.AddDays(2)) == CertificateValidationResult.Valid);
      Assert.IsTrue(test.Validate(storage, DateTime.Now.AddDays(3)) == CertificateValidationResult.Valid);
      Assert.IsTrue(test.Validate(storage, DateTime.Now.AddDays(4)) == CertificateValidationResult.Valid);
      Assert.IsTrue(test.Validate(storage, DateTime.Now.AddDays(5)) == CertificateValidationResult.Valid);

      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddDays(6)) == CertificateValidationResult.Valid);
      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddDays(7)) == CertificateValidationResult.Valid);
      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddDays(8)) == CertificateValidationResult.Valid);
      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddDays(9)) == CertificateValidationResult.Valid);
      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddDays(10)) == CertificateValidationResult.Valid);
      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddDays(11)) == CertificateValidationResult.Valid);
      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddDays(12)) == CertificateValidationResult.Valid);
      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddDays(30)) == CertificateValidationResult.Valid);
      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddYears(1)) == CertificateValidationResult.Valid);
      Assert.IsFalse(test.Validate(storage, DateTime.Now.AddYears(5)) == CertificateValidationResult.Valid);
    }
  }
}
