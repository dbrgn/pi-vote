using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Pirate.PiVote.Kiosk
{
  public class Connection
  {
    private TcpClient client;

    private DateTime created;

    private bool haveHeader;
    MessageType requestType;
    MessageFunction requestFunction;
    MessageStatus requestStatus;
    byte requestTag;
    int requestLength;

    private BinaryReader reader;

    private BinaryWriter writer;

    private KioskServer kiosk;

    public Connection(TcpClient client, KioskServer kiosk)
    {
      this.client = client;
      this.kiosk = kiosk;
      this.reader = new BinaryReader(this.client.GetStream());
      this.writer = new BinaryWriter(this.client.GetStream());
      this.created = DateTime.Now;
      this.haveHeader = false;
    }

    public bool Overdue
    {
      get
      {
        return DateTime.Now.Subtract(this.created).TotalSeconds > 5;
      }
    }

    public void Process()
    {
      try
      {
        if (!this.haveHeader)
        {
          if (this.client.Available >= 8)
          {
            this.requestType = (MessageType)this.reader.ReadByte();
            this.requestFunction = (MessageFunction)this.reader.ReadByte();
            this.requestStatus = (MessageStatus)this.reader.ReadByte();
            this.requestTag = this.reader.ReadByte();
            this.requestLength = this.reader.ReadInt32();

            if (this.requestLength > 0)
            {
              this.haveHeader = true;
            }
            else
            {
              Handle(new byte[] { });
            }
          }
        }
        else
        {
          if (this.client.Available >= this.requestLength)
          {
            byte[] requestData = this.reader.ReadBytes(this.requestLength);

            Handle(requestData);
            
            this.haveHeader = false;
          }
        }
      }
      catch { }
    }

    private void Handle(byte[] requestData)
    {
      try
      {
        switch (this.requestFunction)
        {
          case MessageFunction.FetchUserData:
            Reply(MessageStatus.Success, this.kiosk.FetchUserData());
            break;
          case MessageFunction.PushSignatureRequest:
            this.kiosk.PushSignatureRequest(requestData);
            Reply(MessageStatus.Success);
            break;
          case MessageFunction.FetchCertificateStorage:
            Reply(MessageStatus.Success, this.kiosk.FetchCertificateStorage());
            break;
          case MessageFunction.FetchServerCertificate:
            Reply(MessageStatus.Success, this.kiosk.FetchServerCertificate());
            break;
          default:
            Reply(MessageStatus.FailBadRequest);
            break;
        }
      }
      catch (Exception exception)
      {
        Reply(MessageStatus.FailServerError, Encoding.UTF8.GetBytes(exception.Message));
      }
    }

    private void Reply(MessageStatus replyStatus)
    {
      Reply(replyStatus, new byte[] { });
    }

    private void Reply(MessageStatus replyStatus, byte[] replyData)
    {
      this.writer.Write((byte)MessageType.Reply);
      this.writer.Write((byte)this.requestFunction);
      this.writer.Write((byte)replyStatus);
      this.writer.Write((byte)this.requestTag);
      this.writer.Write(replyData.Length);
      this.writer.Flush();

      this.writer.Write(replyData);
      this.writer.Flush();
    }

    public void Close()
    {
      this.client.Close();
    }
  }
}
