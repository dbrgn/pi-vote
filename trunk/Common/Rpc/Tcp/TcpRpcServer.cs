
/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  public class TcpRpcServer
  {
    private const int Port = 2323;

    private TcpListener listener;
    private Thread listenerThread;
    private Thread workerThread;
    private bool run;
    private Queue<TcpRpcConnection> connections;
    private VotingRpcServer rpcServer;

    public TcpRpcServer(VotingRpcServer rpcServer)
    {
      this.listener = new TcpListener(new IPEndPoint(IPAddress.Any, Port));
      this.connections = new Queue<TcpRpcConnection>();
      this.rpcServer = rpcServer;
    }

    public void Start()
    {
      this.run = true;

      this.listenerThread = new Thread(Listen);
      this.listenerThread.Start();

      this.workerThread = new Thread(Worker);
      this.workerThread.Start();
    }

    public void Stop()
    {
      this.run = false;
      this.listenerThread.Join();
      this.workerThread.Join();
    }

    private void Worker()
    {
      while (this.run)
      {
        TcpRpcConnection connection = null;

        lock (this.connections)
        {
          if (this.connections.Count > 0)
          {
            connection = this.connections.Dequeue();
          }
        }

        if (connection != null)
        {
          try
          {
            connection.Process();

            if (!connection.Overdue)
              this.connections.Enqueue(connection);
          }
          catch
          {
            connection.Close();
          }
        }

        Thread.Sleep(1);
      }
    }

    private void Listen()
    {
      this.listener.Start();

      while (this.run)
      {
        if (this.listener.Pending())
        {
          lock (this.connections)
          {
            TcpClient client = this.listener.AcceptTcpClient();
            TcpRpcConnection connection = new TcpRpcConnection(client, rpcServer);
            this.connections.Enqueue(connection);
          }
        }

        Thread.Sleep(1);
      }

      this.listener.Stop();
    }
  }
}
