/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Thomas Bruderer <apophis@apophis.ch> 
 *  <BSD Like license>
 */
 
using System;
using System.Threading;
using System.Collections.Generic;
using System.Reflection;
using System.Diagnostics;
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

      AssemblyName programName = Assembly.GetExecutingAssembly().GetName();
      AssemblyName libraryName = typeof(VotingRpcServer).Assembly.GetName();

      Console.WriteLine("Program version {0}", programName.Version.ToString());
      Console.WriteLine("Library version {0}", libraryName.Version.ToString());
      Console.WriteLine();

      if (args.Length >= 1 && args[0] == "wait")
      {
        Console.Write("Waiting to start...");
        Thread.Sleep(10000);
        Console.WriteLine("Go");
      }

      RpcServer = new VotingRpcServer();
      TcpServer = new TcpRpcServer(RpcServer);

      try
      {
        TcpServer.Start();
      }
      catch (Exception exception)
      {
        TcpServer.Logger.Log(LogLevel.Error, "Start failed with exception {0}", exception.Message);

        Console.WriteLine();
        Console.WriteLine(exception.ToString());
      }

      try
      {
        while (true)
        {
          Thread.Sleep(1000);
        }
      }
      catch (Exception exception)
      {
        TcpServer.Logger.Log(LogLevel.Error, "Server failed with exception {0}. Trying to restart it.", exception.Message);

        Process.Start(Environment.GetCommandLineArgs()[0], "wait");
        Environment.FailFast("Server failed");
      }
    }
  }
}