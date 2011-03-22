/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pirate.PiVote;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.UnitTest
{
  [TestClass]
  public class SecureTest
  {
    private CertificateStorage storage;
    private CACertificate root;
    private CACertificate intermediate;
    private AdminCertificate admin;
    private AdminCertificate eve;

    public SecureTest()
    {
    }

    [TestInitialize()]
    public void MyTestInitialize()
    {
      this.storage = new CertificateStorage();

      this.root = new CACertificate(null, "Root");
      this.root.CreateSelfSignature();
      this.storage.AddRoot(this.root.OnlyPublicPart);

      var rootCrl = new RevocationList(this.root.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedRootCrl = new Signed<RevocationList>(rootCrl, this.root);
      this.storage.AddRevocationList(signedRootCrl);

      this.intermediate = new CACertificate(null, "Intermediate");
      this.intermediate.CreateSelfSignature();
      this.intermediate.AddSignature(this.root, DateTime.Now.AddDays(1));
      this.storage.Add(intermediate.OnlyPublicPart);

      var intermediateCrl = new RevocationList(this.intermediate.Id, DateTime.Now, DateTime.Now.AddDays(1), new Guid[] { });
      var signedIntermediateCrl = new Signed<RevocationList>(intermediateCrl, this.intermediate);
      this.storage.AddRevocationList(signedIntermediateCrl);

      this.admin = new AdminCertificate(Language.English, null, "Test");
      this.admin.CreateSelfSignature();
      this.admin.AddSignature(this.intermediate, DateTime.Now.AddDays(1));

      this.eve = new AdminCertificate(Language.English, null, "Eve");
      this.eve.CreateSelfSignature();
      this.eve.AddSignature(this.intermediate, DateTime.Now.AddDays(1));
    }

    [TestCleanup()]
    public void MyTestCleanup()
    {
    }

    [TestMethod]
    public void SecureDecryptionTest()
    {
      Option option = new Option(new MultiLanguageString("Test"), new MultiLanguageString(string.Empty), new MultiLanguageString(string.Empty));

      Secure<Option> encryptedOption = new Secure<Option>(option, this.admin.OnlyPublicPart, this.admin);

      Assert.IsTrue(encryptedOption.Verify(this.storage));

      Option other = encryptedOption.Value.Decrypt(this.admin);

      Assert.IsTrue(other.ToBinary().Equal(option.ToBinary()));
    }

    [TestMethod()]
    [ExpectedException(typeof(ArgumentException))]
    public void SecureNonDecryptionTest()
    {
      Option option = new Option(new MultiLanguageString("Test"), new MultiLanguageString(string.Empty), new MultiLanguageString(string.Empty));

      Secure<Option> encryptedOption = new Secure<Option>(option, this.admin.OnlyPublicPart, this.admin);

      Option other = encryptedOption.Value.Decrypt(this.eve);
    }
  }
}
