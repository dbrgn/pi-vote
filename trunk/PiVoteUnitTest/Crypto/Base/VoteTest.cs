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
      this.parameters = VotingParameters.CreateTestParameters();
    }

    [TestCleanup()]
    public void MyTestCleanup()
    {
    }

    [TestMethod]
    public void VoteVerifyTest()
    {
      for (int i = 0; i < 100; i++)
      {
        this.publicKey = parameters.Random();

        Vote vote = new Vote(0, this.parameters.Random(), this.parameters, this.publicKey, new Progress(null));

        Assert.IsTrue(vote.Verify(this.publicKey, this.parameters));
      }
    }
  }
}
