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
  public class BallotTest
  {
    private VotingParameters parameters;
    private BigInt publicKey;

    public BallotTest()
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
    public void BallotVerifyTest()
    {
      for (int i = 0; i < 100; i++)
      {
        this.publicKey = this.parameters.Random();

        Ballot ballot = new Ballot(new int[] { 0, 1, 0, 1 }, this.parameters, this.parameters.Questions.ElementAt(0), this.publicKey, new Progress(null));
        Assert.IsTrue(ballot.Verify(this.publicKey, this.parameters, this.parameters.Questions.ElementAt(0), new Progress(null)));
      }
    }

    [DeploymentItem("libgmp-3.dll"), TestMethod]
    public void BallotInvalidVerifyTest()
    {
      for (int i = 0; i < 100; i++)
      {
        this.publicKey = this.parameters.Random();

        Ballot ballot0 = new Ballot(new int[] { 0, 1, 0, 1 }, this.parameters, this.parameters.Questions.ElementAt(0), this.publicKey, FakeType.BadFiatShamir);
        Assert.IsFalse(ballot0.Verify(this.publicKey, this.parameters, this.parameters.Questions.ElementAt(0), new Progress(null)));

        Ballot ballot1 = new Ballot(new int[] { 0, 1, 0, 1 }, this.parameters, this.parameters.Questions.ElementAt(0), this.publicKey, FakeType.BadPowerMod);
        Assert.IsFalse(ballot1.Verify(this.publicKey, this.parameters, this.parameters.Questions.ElementAt(0), new Progress(null)));
      }
    }
  }
}
