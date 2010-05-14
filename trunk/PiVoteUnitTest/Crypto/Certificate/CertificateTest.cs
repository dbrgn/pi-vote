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
      Assert.IsFalse(root.Valid(storage));

      storage.AddRoot(root.OnlyPublicPart);
      Assert.IsTrue(root.Valid(storage));

      var rootCrl = new RevocationList(root.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[]{});
      var signedRootCrl = new Signed<RevocationList>(rootCrl, root);
      storage.AddRevocationList(signedRootCrl);

      CACertificate intermediate = new CACertificate("Intermediate");
      intermediate.CreateSelfSignature();
      Assert.IsFalse(intermediate.Valid(storage));

      intermediate.AddSignature(root, DateTime.Now.AddDays(1));
      storage.Add(intermediate.OnlyPublicPart); 
      Assert.IsTrue(intermediate.Valid(storage));

      var intermediateCrl = new RevocationList(intermediate.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedIntermediateCrl = new Signed<RevocationList>(intermediateCrl, intermediate);
      storage.AddRevocationList(signedIntermediateCrl);

      AdminCertificate test = new AdminCertificate("Test");
      test.CreateSelfSignature();
      Assert.IsFalse(test.Valid(storage));

      test.AddSignature(intermediate, DateTime.Now.AddDays(1));
      Assert.IsTrue(test.Valid(storage));
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

      AdminCertificate test = new AdminCertificate("Test");
      test.CreateSelfSignature();
      test.AddSignature(intermediate, DateTime.Now.AddDays(1));

      byte[] data = test.ToBinary();
      data[data.Length - 3]++;
      AdminCertificate other = Serializable.FromBinary<AdminCertificate>(data);
      Assert.IsFalse(other.Valid(storage));
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

      AdminCertificate test = new AdminCertificate("Test");
      test.CreateSelfSignature();
      test.AddSignature(intermediate, DateTime.Now.AddDays(1));

      Assert.IsTrue(test.Valid(storage, DateTime.Now));
      Assert.IsTrue(test.Valid(storage, DateTime.Now.AddMinutes(1)));
      Assert.IsTrue(test.Valid(storage, DateTime.Now.AddHours(1)));
      Assert.IsTrue(test.Valid(storage, DateTime.Now.AddDays(1)));

      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddDays(2)));
      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddDays(5)));
      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddDays(30)));
      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddYears(1)));
      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddYears(5)));

      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddDays(-1)));
      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddDays(-5)));
      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddDays(-30)));
      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddYears(-1)));
      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddYears(-5)));
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

      AdminCertificate test = new AdminCertificate("Test");
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
      
      Assert.IsTrue(test.Valid(storage, DateTime.Now));
      Assert.IsTrue(test.Valid(storage, DateTime.Now.AddDays(1)));
      Assert.IsTrue(test.Valid(storage, DateTime.Now.AddDays(2)));
      Assert.IsTrue(test.Valid(storage, DateTime.Now.AddDays(3)));
      Assert.IsTrue(test.Valid(storage, DateTime.Now.AddDays(4)));
      Assert.IsTrue(test.Valid(storage, DateTime.Now.AddDays(5)));

      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddDays(6)));
      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddDays(7)));
      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddDays(8)));
      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddDays(9)));
      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddDays(10)));
      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddDays(11)));
      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddDays(12)));
      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddDays(30)));
      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddYears(1)));
      Assert.IsFalse(test.Valid(storage, DateTime.Now.AddYears(5)));
    }
  }
}
