using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;
using System.Diagnostics;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Kiosk
{
  public class Client
  {
    private TcpClient client;

    private BinaryReader reader;

    private BinaryWriter writer;

    private IPAddress controlIpAddress;

    public Client()
    {
      this.controlIpAddress = IPAddress.Loopback;
      this.client = new TcpClient();
    }

    private void Connect()
    {
      this.client = new TcpClient();
      this.client.SendTimeout = 2000;
      this.client.ReceiveTimeout = 2000;
      this.client.Connect(new IPEndPoint(this.controlIpAddress, 4242));

      this.reader = new BinaryReader(this.client.GetStream());
      this.writer = new BinaryWriter(this.client.GetStream());
    }

    public byte[] Execute(MessageFunction function, byte[] data)
    {
      DateTime timeout = DateTime.Now.AddSeconds(60);
      byte tag = 0;

      this.writer.Write((byte)MessageType.Request);
      this.writer.Write((byte)function);
      this.writer.Write((byte)MessageStatus.Unknown);
      this.writer.Write(tag);
      this.writer.Write(data.Length);
      this.writer.Flush();

      this.writer.Write(data);
      this.writer.Flush();

      while (this.client.Available < 8)
      {
        if (DateTime.Now > timeout)
        {
          this.client.Close();
          throw new Exception("Netowrk timeout");
        }

        Thread.Sleep(10);
      }

      MessageType responseType = (MessageType)this.reader.ReadByte();
      MessageFunction responseFunction = (MessageFunction)this.reader.ReadByte();
      MessageStatus responseStatus = (MessageStatus)this.reader.ReadByte();
      byte responseTag = this.reader.ReadByte();
      int length = this.reader.ReadInt32();

      while (length > 0 && this.client.Available < length)
      {
        if (DateTime.Now > timeout)
        {
          this.client.Close();
          throw new Exception("Netowrk timeout");
        }

        Thread.Sleep(10);
      }

      if (length < 0 || length > 16777216)
      {
        this.writer.Close();
        this.reader.Close();
        this.client.Close();
        throw new ClientException("Bad length");
      }

      byte[] replyData = length > 0 ? this.reader.ReadBytes(length) : new byte[] { };

      if (responseType != MessageType.Reply)
      {
        throw new ClientException("Bad response message type.");
      }
      else if (responseFunction != function)
      {
        throw new ClientException("Bad response message function.");
      }
      else if (responseTag != tag)
      {
        throw new ClientException("Bad response message tag.");
      }
      else
      {
        switch (responseStatus)
        {
          case MessageStatus.Success:
            return replyData;
          case MessageStatus.FailServerError:
            throw new ClientException("Server reports error: " + Encoding.UTF8.GetString(replyData));
          case MessageStatus.FailBadRequest:
            throw new ClientException("Server reports bad request.");
          default:
            throw new ClientException("Bad response message status.");
        }
      }
    }

    private byte[] TryExecute(MessageFunction function, byte[] data)
    {
      byte[] replyData = null;

      try
      {
        Debug.WriteLine("TryExecute " + function.ToString());

        if (!this.client.Connected)
        {
          Debug.WriteLine("Failed. Reconnect");
          Connect();
        }

        if (this.client.Connected)
        {
          replyData = Execute(function, data);
        }
        else
        {
          Debug.WriteLine("Reconnect failed");
        }
      }
      catch (Exception ex)
      {
        Debug.WriteLine("TryExecute failed: " + ex.ToString());
      }

      return replyData;
    }

    public CertificateStorage FetchCertificateStroage()
    {
      byte[] replyData = TryExecute(MessageFunction.FetchCertificateStorage, new byte[] { });

      if (replyData == null)
      {
        return null;
      }
      else
      {
        return Serializable.FromBinary<CertificateStorage>(replyData);
      }
    }

    public Certificate FetchServerCertificate()
    {
      byte[] replyData = TryExecute(MessageFunction.FetchServerCertificate, new byte[] { });

      if (replyData == null)
      {
        return null;
      }
      else
      {
        return Serializable.FromBinary<Certificate>(replyData);
      }
    }

    public SignatureRequest FetchUserData()
    {
      System.Diagnostics.Debug.WriteLine("FetchUserData");

      byte[] replyData = TryExecute(MessageFunction.FetchUserData, new byte[] { });

      if (replyData == null)
      {
        System.Diagnostics.Debug.WriteLine("Reply is null");

        return null;
      }
      else
      {
        System.Diagnostics.Debug.WriteLine("Reply has length " + replyData.Length);

        return Serializable.FromBinary<SignatureRequest>(replyData);
      }
    }

    public bool PushSignaturRequest(RequestContainer request)
    {
      byte[] replyData = TryExecute(MessageFunction.PushSignatureRequest, request.ToBinary());

      return replyData != null;
    }
  }
}
