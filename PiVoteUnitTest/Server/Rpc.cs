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
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.UnitTest
{
  [TestClass]
  public class Rpc
  {
    public Rpc()
    {
    }

    [TestInitialize()]
    public void MyTestInitialize()
    {
    }

    [TestCleanup()]
    public void MyTestCleanup()
    {
    }

    [TestMethod]
    public void RpcTest()
    {
      TcpRpcServer server = new TcpRpcServer(new EchoServer());
      server.Start();

      CertificateStorage storage = new CertificateStorage();
      TcpRpcClient client = new TcpRpcClient();

      client.Connect(new IPEndPoint(IPAddress.Loopback, 4242));

      Assert.IsTrue(client.Connected);

      var request = new EchoRequest(Guid.NewGuid(), "hello");

      var responseData = client.Execute(request.ToBinary());

      var response = Serializable.FromBinary<EchoResponse>(responseData);

      Assert.AreEqual(request.RequestId, response.RequestId);
      Assert.AreEqual("hello", response.Message);

      client.Disconnect();

      Assert.IsFalse(client.Connected);

      server.Stop();
    }
  }
}
