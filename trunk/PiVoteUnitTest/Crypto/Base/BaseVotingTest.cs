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
  public class BaseVotingTest
  {
    private VotingParameters parameters;

    public BaseVotingTest()
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
    public void VotingTest()
    {
      Dictionary<int, Authority> auths = new Dictionary<int, Authority>();

      for (int authIndex = 1; authIndex <= 5; authIndex++)
      {
        auths.Add(authIndex, new Authority(authIndex, this.parameters));
      }

      auths.Values.Foreach(a => a.CreatePolynomial());

      var verVals = new List<List<VerificationValue>>();

      for (int authIndex = 1; authIndex <= 5; authIndex++)
      {
        Authority auth = auths[authIndex];
        var authVerVals = new List<VerificationValue>();

        for (int valueIndex = 0; valueIndex <= parameters.Thereshold; valueIndex++)
        {
          authVerVals.Add(auth.VerificationValue(valueIndex));
        }

        verVals.Add(authVerVals);
      }

      for (int authIndex = 1; authIndex <= 5; authIndex++)
      {
        Authority auth = auths[authIndex];
        var shares = new List<Share>(auths.Values.Select(a => a.Share(authIndex)));
        Assert.IsTrue(auth.VerifySharing(shares, verVals));
      }
    }
  }
}
