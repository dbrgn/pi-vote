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

    private int length;

    private BinaryReader reader;

    private StreamWriter writer;

    private KioskServer kiosk;

    public Connection(TcpClient client, KioskServer kiosk)
    {
      this.client = client;
      this.kiosk = kiosk;
      this.reader = new BinaryReader(this.client.GetStream());
      this.writer = new StreamWriter(this.client.GetStream());
      this.created = DateTime.Now;
      this.length = -1;
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
        if (this.length < 0)
        {
          if (this.client.Available >= sizeof(int))
          {
            this.length = this.reader.ReadInt32();
          }
        }
        else
        {
          if (this.client.Available >= this.length)
          {
            if (this.length >= 4)
            {
              Message message = (Message)this.reader.ReadInt32();

              if (this.length > 4)
              {
                byte[] data = this.reader.ReadBytes(this.length - 4);
                Handle(message, data);
              }
              else
              {
                Handle(message, new byte[] { });
              }
            }
            else
            {
              this.reader.ReadBytes(this.length);
              Reply(Message.Error);
            }
          }
        }
      }
      catch { }
    }

    private void Handle(Message message, byte[] data)
    {
      try
      {
        switch (message)
        {
          case Message.FetchUserData:
            Reply(Message.Reply, this.kiosk.FetchUserData());
            break;
          case Message.PushSignatureRequest:
            this.kiosk.PushSignatureRequest(data);
            Reply(Message.Reply);
            break;
          case Message.FetchCertificateStorage:
            Reply(Message.Reply, this.kiosk.FetchCertificateStorage());
            break;
          case Message.FetchServerCertificate:
            Reply(Message.Reply, this.kiosk.FetchServerCertificate());
            break;
          default:
            Reply(Message.Error);
            break;
        }
      }
      catch
      {
        Reply(Message.Error);
      }
    }

    private void Reply(Message message)
    {
      Reply(message, new byte[] { });
    }

    private void Reply(Message message, byte[] data)
    {
      this.writer.Write(data.Length + sizeof(int));
      this.writer.Write((int)message);
      this.writer.Write(data);
      this.writer.Flush();
    }

    public void Close()
    {
      this.client.Close();
    }
  }
}
