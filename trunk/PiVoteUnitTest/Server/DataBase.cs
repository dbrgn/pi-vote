using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;


namespace Pirate.PiVote.UnitTest
{
  [TestClass]
  public class Database
  {
    public Database()
    {
    }

    [TestInitialize()]
    public void MyTestInitialize()
    {
      CACertificate rootCertificate = new CACertificate(null, "Root CA");
      rootCertificate.CreateSelfSignature();
      rootCertificate.Save("root.pi-cert");

      ServerCertificate serverCertificate = new ServerCertificate("Server");
      serverCertificate.CreateSelfSignature();
      serverCertificate.AddSignature(rootCertificate, DateTime.Now.AddDays(1));
      serverCertificate.Save("server.pi-cert");
    }

    [TestCleanup()]
    public void MyTestCleanup()
    {
    }

    [DeploymentItem("libgmp-3.dll"), DeploymentItem("Server\\pi-vote-server.cfg"), TestMethod]
    public void DatabaseTest()
    {
      VotingRpcServer votingServer = new VotingRpcServer();

      votingServer.Idle();

      var ids = votingServer.FetchVotingIds();

      votingServer.Logger.Close();
    }
  }
}
