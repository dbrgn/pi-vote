/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Thomas Bruderer <apophis@apophis.ch> 
 *  <BSD Like license>
 */
 
using System;
using System.Threading;
using System.Collections.Generic;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Server
{
  /// <summary>
  /// Description of ServerMain.
  /// </summary>
  public class ServerMain
  {
    private static VotingRpcServer RpcServer;
    private static TcpRpcServer TcpServer;

    public static void Main(string[] args)
    {
      Console.WriteLine("PiVote TCP RPC Server");
      Console.WriteLine();
      Console.Write("Loading...");

      RpcServer = new VotingRpcServer();
      TcpServer = new TcpRpcServer(RpcServer);
      TcpServer.Start();

      Console.WriteLine("Done");

      while (true)
      {
        Thread.Sleep(1000);
      }

      //Console.Write("Stopping...");
      //TcpServer.Stop();
      //Console.WriteLine("Done");
    }
  }
}