
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
    private int port;

    /// <summary>
    /// Number of worker threads.
    /// </summary>
    private int workerCount;

    /// <summary>
    /// Short worker sleep period in milliseconds;
    /// </summary>
    private int workerShortWait;

    /// <summary>
    /// Long worker sleep period in milliseconds;
    /// </summary>
    private int workerLongWait;

    /// <summary>
    /// Time until idle clients are disconnected in seconds.
    /// </summary>
    private double clientTimeOut;

    /// <summary>
    /// Time between SQL keep alive queries in seconds.
    /// </summary>
    private double sqlKeepAliveTime;

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
    /// Logs messages to file.
    /// </summary>
    private Logger logger;

    /// <summary>
    /// Create a new TCP RPC server.
    /// </summary>
    /// <param name="rpcServer">Voting RPC server.</param>
    public TcpRpcServer(VotingRpcServer rpcServer)
    {
      this.logger = rpcServer.Logger;

      this.port = rpcServer.ServerConfig.Port;
      this.workerCount = rpcServer.ServerConfig.WorkerCount;
      this.workerShortWait = rpcServer.ServerConfig.WorkerShortWait;
      this.workerLongWait = rpcServer.ServerConfig.WorkerLongWait;
      this.sqlKeepAliveTime = rpcServer.ServerConfig.SqlKeepAliveTime;
      this.clientTimeOut = rpcServer.ServerConfig.ClientTimeOut;

      this.listener = new TcpListener(new IPEndPoint(IPAddress.Any, this.port));
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

      for (int threadIndex = 0; threadIndex < this.workerCount; threadIndex++)
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
      DateTime lastWorkDone = DateTime.Now;

      while (this.run)
      {
        bool doneSomeWork = false;

        for (int processIndex = 0; processIndex < this.connections.Count; processIndex++)
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
              doneSomeWork |= connection.Process();

              if (connection.Overdue)
              {
                this.logger.Log(LogLevel.Info, "Connection from {0} was overdue and therefore dropped.", connection.RemoteEndPointText);
                connection.Close();
              }
              else
              {
                lock (this.connections)
                {
                  this.connections.Enqueue(connection);
                }
              }
            }
            catch (Exception exception)
            {
              this.logger.Log(LogLevel.Warning, "Connection from {0} faild and was therefore dropped. Exception: {1}", connection.RemoteEndPointText, exception.ToString());
              connection.Close();
            }
          }
        }

        if (doneSomeWork)
        {
          lastWorkDone = DateTime.Now;
          Thread.Sleep(this.workerShortWait);
        }
        else
        {
          if (DateTime.Now.Subtract(lastWorkDone).TotalSeconds > this.sqlKeepAliveTime)
          {
            lock (this.rpcServer)
            {
              this.rpcServer.Idle();
            }

            lastWorkDone = DateTime.Now;
          }

          Thread.Sleep(this.workerLongWait);
        }
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
        while (this.listener.Pending())
        {
          TcpClient client = this.listener.AcceptTcpClient();
          TcpRpcConnection connection = new TcpRpcConnection(client, rpcServer, this.clientTimeOut);

          lock (this.connections)
          {
            this.connections.Enqueue(connection);
          }

          this.logger.Log(LogLevel.Info, "New connection from {0}.", connection.RemoteEndPointText);

          Thread.Sleep(1);
        }

        Thread.Sleep(100);
      }

      this.listener.Stop();
    }
  }
}
