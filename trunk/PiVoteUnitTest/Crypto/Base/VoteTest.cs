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
using Emil.GMP;

namespace Pirate.PiVote.UnitTest
{
  [TestClass]
  public class VoteTest
  {
    private VotingParameters parameters;
    private BigInt publicKey;

    public VoteTest()
    {
    }

    [TestInitialize()]
    public void MyTestInitialize()
    {
      this.parameters = VotingParameters.CreateTestParameters(Files.TestDataPath);
    }

    [TestCleanup()]
    public void MyTestCleanup()
    {
    }

    [DeploymentItem("libgmp-3.dll"), TestMethod]
    public void VoteVerifyTest()
    {
      for (int i = 0; i < 100; i++)
      {
        this.publicKey = parameters.Random();

        Vote vote = new Vote(0, this.parameters.Random(), this.parameters, this.publicKey, new Progress(null));

        Assert.IsTrue(vote.Verify(this.publicKey, this.parameters));
      }
    }

    [DeploymentItem("libgmp-3.dll"), TestMethod]
    public void VoteInvalidVerifyTest()
    {
      for (int i = 0; i < 100; i++)
      {
        this.publicKey = parameters.Random();

        Vote vote0 = new Vote(2, this.parameters.Random(), this.parameters, this.publicKey, FakeType.BadDisjunction);
        Assert.IsFalse(vote0.Verify(this.publicKey, this.parameters));

        Vote vote1 = new Vote(2, this.parameters.Random(), this.parameters, this.publicKey, FakeType.BadFiatShamir);
        Assert.IsFalse(vote1.Verify(this.publicKey, this.parameters));

        Vote vote2 = new Vote(2, this.parameters.Random(), this.parameters, this.publicKey, FakeType.BadPowerMod);
        Assert.IsFalse(vote2.Verify(this.publicKey, this.parameters));
      }
    }
  }
}
