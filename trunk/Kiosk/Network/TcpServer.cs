using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Pirate.PiVote.Kiosk
{
  public class TcpServer
  {
    private Queue<Connection> connetions;

    private TcpListener listener;

    private Thread listenThread;

    private Thread workerThread;

    private bool run;

    private KioskServer kiosk;

    public TcpServer(KioskServer kiosk)
    {
      this.kiosk = kiosk;
    }

    public void Start()
    {
      this.connetions = new Queue<Connection>();

      var localEndPoint = new IPEndPoint(IPAddress.Any, 4242);
      this.listener = new TcpListener(localEndPoint);
      this.listener.Start();

      this.run = true;
      this.listenThread = new Thread(Listen);
      this.listenThread.Start();
      this.workerThread = new Thread(Worker);
      this.workerThread.Start();
    }

    public void Stop()
    {
      this.run = false;
      this.listenThread.Join();
      this.workerThread.Join();

      this.listener.Stop();
    }

    private void Listen()
    {
      while (this.run)
      {
        if (this.listener.Pending())
        {
          var client = this.listener.AcceptTcpClient();
          var connection = new Connection(client, this.kiosk);
          this.connetions.Enqueue(connection);
        }

        Thread.Sleep(10);
      }
    }

    private void Worker()
    {
      while (this.run)
      {
        int workCount = this.connetions.Count;

        for (int workIndex = 0; workIndex < workCount; workIndex++)
        {
          Connection connection = null;

          lock (this.connetions)
          {
            if (this.connetions.Count > 0)
            {
              connection = this.connetions.Dequeue();
            }
          }

          if (connection != null)
          {
            if (connection.Overdue)
            {
              connection.Close();
            }
            else
            {
              connection.Process();

              this.connetions.Enqueue(connection);
            }
          }
        }

        Thread.Sleep(10);
      }
    }
  }
}
