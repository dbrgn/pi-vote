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
    private RpcServer rpcServer;

    /// <summary>
    /// Logs messages to file.
    /// </summary>
    public ILogger Logger { get; private set; }

    /// <summary>
    /// When was the last processing done?
    /// </summary>
    private DateTime lastProcessDone;

    /// <summary>
    /// Id for the next connection.
    /// </summary>
    private ulong nextConnectionId;

    /// <summary>
    /// Create a new TCP RPC server.
    /// </summary>
    /// <param name="rpcServer">Voting RPC server.</param>
    public TcpRpcServer(RpcServer rpcServer)
    {
      Logger = rpcServer.Logger;

      this.port = rpcServer.ServerConfig.Port;
      this.workerCount = rpcServer.ServerConfig.WorkerCount;
      this.workerShortWait = rpcServer.ServerConfig.WorkerShortWait;
      this.workerLongWait = rpcServer.ServerConfig.WorkerLongWait;
      this.sqlKeepAliveTime = rpcServer.ServerConfig.SqlKeepAliveTime;
      this.clientTimeOut = rpcServer.ServerConfig.ClientTimeOut;

      this.listener = new TcpListener(new IPEndPoint(IPAddress.Any, this.port));
      this.connections = new Queue<TcpRpcConnection>();
      this.rpcServer = rpcServer;
      this.nextConnectionId = 0;
    }

    /// <summary>
    /// Start the server and its threads.
    /// </summary>
    public void Start()
    {
      this.lastProcessDone = DateTime.Now;
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
      Logger.Close();
    }

    /// <summary>
    /// Works RPC requests.
    /// </summary>
    private void Worker()
    {
      int failCount = 0;
      DateTime lastWorkDone = DateTime.Now;

      while (this.run)
      {
        try
        {
          bool doneSomeWork = WorkConnections();

          WorkProcess();

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
        catch (Exception exception)
        {
          failCount++;
          Logger.Log(LogLevel.Error, "Worker thread {0} failed the {1} time with exception {2}", Thread.CurrentThread.ManagedThreadId, failCount, exception.ToString());

          if (failCount < 10)
          {
            Thread.Sleep(1000 * failCount);
          }
          else
          {
            Logger.Log(LogLevel.Error, "Worker thread {0} failed too many times.", Thread.CurrentThread.ManagedThreadId, exception.ToString());
            throw exception;
          }
        }
      }
    }

    /// <summary>
    /// Works the server processing when needed.
    /// </summary>
    private void WorkProcess()
    {
      if (DateTime.Now.Subtract(this.lastProcessDone).TotalSeconds > this.sqlKeepAliveTime)
      {
        lock (this.rpcServer)
        {
          this.rpcServer.Process();
        }

        this.lastProcessDone = DateTime.Now;
      }
    }

    /// <summary>
    /// Works all connections.
    /// </summary>
    /// <returns>Was some work done?</returns>
    private bool WorkConnections()
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
              Logger.Log(LogLevel.Debug, "Connection {0} was overdue and therefore dropped.", connection.Id);
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
            Logger.Log(LogLevel.Warning, "Connection {0} failed and was therefore dropped. Exception: {1}", connection.Id, exception.ToString());
            connection.Close();
          }
        }
      }
      return doneSomeWork;
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
          try
          {
            TcpClient client = this.listener.AcceptTcpClient();
            ulong connectionId = 0;

            lock (this.connections)
            {
              connectionId = this.nextConnectionId;
              this.nextConnectionId++;
            }

            TcpRpcConnection connection = new TcpRpcConnection(client, rpcServer, connectionId, this.clientTimeOut);

            lock (this.connections)
            {
              this.connections.Enqueue(connection);
            }

            Logger.Log(LogLevel.Info, "New connection {0} from {1}.", connection.Id, client.Client.RemoteEndPoint);
          }
          catch (SocketException socketException)
          {
            Logger.Log(LogLevel.Warning, "New connection failed: {0}", socketException.Message);
            this.listener = new TcpListener(new IPEndPoint(IPAddress.Any, this.port));
            this.listener.Start();
          }

          Thread.Sleep(1);
        }

        Thread.Sleep(100);
      }

      this.listener.Stop();
    }
  }
}
