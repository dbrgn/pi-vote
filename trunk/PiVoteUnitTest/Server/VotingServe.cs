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
  public class VotingServer
  {
    public VotingServer()
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
    public void VotingServerTest()
    {
      TcpRpcServer server = new TcpRpcServer(new VotingRpcServer());
      server.Start();

      CertificateStorage storage = new CertificateStorage();
      TcpRpcClient client = new TcpRpcClient();

      client.Connect(IPAddress.Loopback);
      Assert.IsTrue(client.Connected);

      VotingRpcProxy proxy = new VotingRpcProxy(client);
      proxy.Start();

      var ids = proxy.FetchVotingIds();

      proxy.Stop();

      client.Disconnect();
      Assert.IsFalse(client.Connected);

      server.Stop();
    }
  }
}
