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
  public class BaseVotingTest
  {
    private VotingParameters parameters;

    public BaseVotingTest()
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
    public void VotingTest()
    {
      List<Authority> auths = new List<Authority>();

      for (int authIndex = 1; authIndex <= 5; authIndex++)
      {
        auths.Add(new Authority(authIndex, this.parameters));
      }

      auths.ForEach(a => a.CreatePolynomial());

      var verVals = new List<List<VerificationValue>>();

      for (int authIndex = 1; authIndex <= 5; authIndex++)
      {
        verVals.Add(new List<VerificationValue>(auths.Select(a => a.VerificationValue(authIndex))));
      }

      for (int authIndex = 1; authIndex <= 5; authIndex++)
      {
        Authority auth = auths[authIndex];
        var shares = new List<Share>(auths.Select(a => a.Share(authIndex)));
        Assert.IsTrue(auth.VerifySharing(shares, verVals));
      }
    }
  }
}
