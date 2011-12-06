﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Cli
{
  public class ConnectCommand : Command
  {
    public ConnectCommand(Status status)
      : base(status)
    { }

    protected override void Execute()
    {
      try
      {
        Status.Connect();

        Console.WriteLine("Connection established");

        Begin("Fetching certificate storage...");
        Status.Client.GetCertificateStorage(Status.CertificateStorage, GetCertificateStorageCompleted);
        WaitForCompletion();

        var assembly = Assembly.GetExecutingAssembly();
        var clientName = assembly.GetName().Name;
        var clientVersion = assembly.GetName().Version.ToString();

        Begin("Fetching configuration...");
        Status.Client.GetConfig(clientName, clientVersion, GetConfigComplete);
        WaitForCompletion();
      }
      catch (Exception exception)
      {
        Console.WriteLine("Connection failed: " + exception.Message);
        Status.Continue = false;
      }
    }

    private void GetCertificateStorageCompleted(Certificate serverCertificate, Exception exception)
    {
      Status.ServerCertificate = serverCertificate;
      CompleteAndReady(exception);
    }

    private void GetConfigComplete(IRemoteConfig config, IEnumerable<Group> groups, Exception exception)
    {
      Status.RemoteConfig = config;
      Status.Groups = groups;
      CompleteAndReady(exception);
    }

    public override IEnumerable<string> Aliases
    {
      get
      {
        yield return "connect";
      }
    }

    public override string HelpText
    {
      get { return "Connects to the Pi-Vote server."; }
    }
  }
}