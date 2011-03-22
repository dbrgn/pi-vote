/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;
using System.IO;

namespace Pirate.PiVote.Kiosk
{
  public class KioskServer
  {
    private TcpServer tcpServer;

    private byte[] certificateStorageBinary;

    private byte[] serverCertificateBinary;

    public SignatureRequest UserData { get; set; }

    public Queue<RequestContainer> Requests { get; private set; }

    public KioskServer(CertificateStorage certificateStorage, Certificate serverCertificate)
    {
      Requests = new Queue<RequestContainer>();
      this.certificateStorageBinary = certificateStorage.ToBinary();
      this.serverCertificateBinary = serverCertificate.ToBinary();
      this.tcpServer = new TcpServer(this);
    }

    public void Start()
    {
      this.tcpServer.Start();
    }

    public void Stop()
    {
      this.tcpServer.Stop();
    }

    public byte[] FetchUserData()
    {
      if (UserData == null)
      {
        throw new InvalidOperationException("User data not available.");
      }
      else
      {
        return UserData.ToBinary();
      }
    }

    public byte[] FetchCertificateStorage()
    {
      if (this.certificateStorageBinary == null)
      {
        throw new InvalidOperationException("Certificate storage not available.");
      }
      else
      {
        return this.certificateStorageBinary;
      }
    }

    public byte[] FetchServerCertificate()
    {
      if (this.serverCertificateBinary == null)
      {
        throw new InvalidOperationException("Server certificate not available.");
      }
      else
      {
        return this.serverCertificateBinary;
      }
    }

    public void PushSignatureRequest(byte[] data)
    {
      var request = Serializable.FromBinary<RequestContainer>(data);

      lock (Requests)
      {
        Requests.Enqueue(request);
      }
    }
  }
}
