using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Kiosk
{
  public class Client
  {
    private TcpClient client;

    private BinaryReader reader;

    private BinaryWriter writer;

    public Client()
    {
    }

    public void Connect(IPAddress ipAddress)
    {
      this.client = new TcpClient();
      this.client.Connect(new IPEndPoint(ipAddress, 4242));

      this.reader = new BinaryReader(this.client.GetStream());
      this.writer = new BinaryWriter(this.client.GetStream());
    }

    public byte[] Execute(Message message, byte[] data)
    {
      DateTime timeout = DateTime.Now.AddSeconds(5);

      this.writer.Write(data.Length + sizeof(int));
      this.writer.Write((int)message);
      this.writer.Write(data);
      this.writer.Flush();

      while (this.client.Available < sizeof(int))
      {
        if (DateTime.Now > timeout)
        {
          throw new Exception("Netowrk timeout");
        }

        Thread.Sleep(10);
      }

      int length = this.reader.ReadInt32();

      while (this.client.Available < length)
      {
        if (DateTime.Now > timeout)
        {
          throw new Exception("Netowrk timeout");
        }

        Thread.Sleep(10);
      }

      if (length >= 4)
      {
        Message replyMessage = (Message)this.reader.ReadInt32();
        byte[] replyData = this.reader.ReadBytes(length - 4);

        switch (replyMessage)
        {
          case Message.Reply:
            return replyData;
          case Message.Error:
            throw new Exception("Server error");
          default:
            throw new Exception("Invalid message");
        }
      }
      else
      {
        throw new Exception("Invalid response");
      }
    }

    public CertificateStorage FetchCertificateStroage()
    {
      byte[] replyData = Execute(Message.FetchCertificateStorage, new byte[] { });

      return Serializable.FromBinary<CertificateStorage>(replyData);
    }

    public Certificate FetchServerCertificate()
    {
      byte[] replyData = Execute(Message.FetchServerCertificate, new byte[] { });

      return Serializable.FromBinary<Certificate>(replyData);
    }

    public SignatureRequest FetchFetchUserData()
    {
      byte[] replyData = Execute(Message.FetchUserData, new byte[] { });

      return Serializable.FromBinary<SignatureRequest>(replyData);
    }

    public void PushSignaturRequest(Secure<SignatureRequest> request, Secure<SignatureRequestInfo> requestInfo)
    {
      MemoryStream memoryStream = new MemoryStream();
      SerializeContext context = new SerializeContext(memoryStream);

      context.Write(request);
      context.Write(requestInfo);

      context.Close();
      memoryStream.Close();

      Execute(Message.PushSignatureRequest, memoryStream.ToArray());
    }
  }
}
