
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
  /// <summary>
  /// TCP RPC server.
  /// </summary>
  public class TcpRpcServer
  {
    /// <summary>
    /// Server TCP port.
    /// </summary>
    private const int Port = 2323;

    /// <summary>
    /// TCP listener for new connection.
    /// </summary>
    private TcpListener listener;

    /// <summary>
    /// Threads that listens for new connections.
    /// </summary>
    private Thread listenerThread;

    /// <summary>
    /// Threads that work requests.
    /// </summary>
    private List<Thread> workerThreads;

    /// <summary>
    /// Continue to run the server.
    /// </summary>
    private bool run;

    /// <summary>
    /// Connection from RPC clients.
    /// </summary>
    private Queue<TcpRpcConnection> connections;

    /// <summary>
    /// The voting RPC server.
    /// </summary>
    private VotingRpcServer rpcServer;

    /// <summary>
    /// Create a new TCP RPC server.
    /// </summary>
    /// <param name="rpcServer">Voting RPC server.</param>
    public TcpRpcServer(VotingRpcServer rpcServer)
    {
      this.listener = new TcpListener(new IPEndPoint(IPAddress.Any, Port));
      this.connections = new Queue<TcpRpcConnection>();
      this.rpcServer = rpcServer;
    }

    /// <summary>
    /// Start the server and its threads.
    /// </summary>
    public void Start()
    {
      this.run = true;

      this.listenerThread = new Thread(Listen);
      this.listenerThread.Start();

      this.workerThreads = new List<Thread>();

      for (int threadIndex = 0; threadIndex < 4; threadIndex++)
      {
        Thread workerThread = new Thread(Worker);
        workerThread.Start();
        this.workerThreads.Add(workerThread);
      }
    }

    /// <summary>
    /// Stop the server and its threads.
    /// </summary>
    public void Stop()
    {
      this.run = false;
      this.listenerThread.Join();
      this.workerThreads.ForEach(workerThread => workerThread.Join());
    }

    /// <summary>
    /// Works RPC requests.
    /// </summary>
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
            {
              lock (this.connections)
              {
                this.connections.Enqueue(connection);
              }
            }
            else
            {
              int t = 0;
            }
          }
          catch
          {
            connection.Close();
          }
        }

        Thread.Sleep(1);
      }
    }

    /// <summary>
    /// Listens for connection.
    /// </summary>
    private void Listen()
    {
      this.listener.Start();

      while (this.run)
      {
        if (this.listener.Pending())
        {
          TcpClient client = this.listener.AcceptTcpClient();
          TcpRpcConnection connection = new TcpRpcConnection(client, rpcServer);

          lock (this.connections)
          {
            this.connections.Enqueue(connection);
          }
        }

        Thread.Sleep(1);
      }

      this.listener.Stop();
    }
  }
}
