using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pirate.PiVote;
using Pirate.PiVote.Crypto;
using Emil.GMP;

namespace PiVoteUnitTest
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
      this.parameters = VotingParameters.CreateTestParameters();
    }

    [TestCleanup()]
    public void MyTestCleanup()
    {
    }

    [TestMethod]
    public void BallotVerifyTest()
    {
      for (int i = 0; i < 100; i++)
      {
        this.publicKey = this.parameters.Random();

        Ballot ballot = new Ballot(new int[] { 0, 1, 0, 1 }, this.parameters, this.parameters.Questions.ElementAt(0), this.publicKey, new Progress(null));

        Assert.IsTrue(ballot.Verify(this.publicKey, this.parameters, this.parameters.Questions.ElementAt(0), new Progress(null)));
      }
    }
  }
}
