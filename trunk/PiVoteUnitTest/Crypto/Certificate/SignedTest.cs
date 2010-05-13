using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pirate.PiVote;
using Pirate.PiVote.Crypto;

namespace PiVoteUnitTest
{
  [TestClass]
  public class SignedTest
  {
    private CertificateStorage storage;
    private CACertificate root;
    private CACertificate intermediate;
    private AdminCertificate admin;

    public SignedTest()
    {
    }

    [TestInitialize()]
    public void MyTestInitialize()
    {
      this.storage = new CertificateStorage();

      this.root = new CACertificate("Root");
      this.root.CreateSelfSignature();
      this.storage.AddRoot(this.root.OnlyPublicPart);

      var rootCrl = new RevocationList(this.root.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedRootCrl = new Signed<RevocationList>(rootCrl, this.root);
      this.storage.AddRevocationList(signedRootCrl);

      this.intermediate = new CACertificate("Intermediate");
      this.intermediate.CreateSelfSignature();
      this.intermediate.AddSignature(this.root, DateTime.Now.AddDays(1));
      this.storage.Add(intermediate.OnlyPublicPart);

      var intermediateCrl = new RevocationList(this.intermediate.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedIntermediateCrl = new Signed<RevocationList>(intermediateCrl, this.intermediate);
      this.storage.AddRevocationList(signedIntermediateCrl);

      this.admin = new AdminCertificate("Test");
      this.admin.CreateSelfSignature();
      this.admin.AddSignature(this.intermediate, DateTime.Now.AddDays(1));
    }

    [TestCleanup()]
    public void MyTestCleanup()
    {
    }

    [TestMethod]
    public void SignedVerifyTest()
    {
      Option option = new Option(new MultiLanguageString("Test"), new MultiLanguageString(string.Empty));

      Signed<Option> signed = new Signed<Option>(option, this.root);

      Assert.IsTrue(signed.VerifySimple());
      Assert.IsTrue(signed.Verify(this.storage));

      Assert.IsTrue(signed.Value.ToBinary().Equal(option.ToBinary()));
    }
  }
}
