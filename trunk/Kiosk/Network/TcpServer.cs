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

    private PeriodicTask listenTask;

    private PeriodicTask workerTask;

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

      this.listenTask = new PeriodicTask(Listen);
      this.listenTask.Start();
      this.workerTask = new PeriodicTask(Work);
      this.workerTask.Start();
    }

    public void Stop()
    {
      this.listenTask.Stop();
      this.workerTask.Stop();
      this.listener.Stop();
    }

    private void Listen()
    {
      if (this.listener.Pending())
      {
        var client = this.listener.AcceptTcpClient();
        var connection = new Connection(client, this.kiosk);

        lock (this.connetions)
        {
          this.connetions.Enqueue(connection);
        }
      }

      Thread.Sleep(10);
    }

    private void Work()
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

            lock (this.connetions)
            {
              this.connetions.Enqueue(connection);
            }
          }
        }
      }
    }
  }
}
