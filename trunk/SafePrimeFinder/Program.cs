/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using Emil.GMP;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.SafePrimeFinder
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("PiVote Safe Prime Finder");

      AssemblyName programName = Assembly.GetExecutingAssembly().GetName();
      AssemblyName libraryName = typeof(Prime).Assembly.GetName();

      Console.WriteLine("Program version {0}", programName.Version.ToString());
      Console.WriteLine("Library version {0}", libraryName.Version.ToString());
      Console.WriteLine();

      string portableDataPath = Path.Combine(Application.StartupPath, Files.PortableDataFolder);
      string dataPath = null;

      if (Directory.Exists(portableDataPath))
      {
        dataPath = portableDataPath;
      }
      else
      {
        dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Files.DataFolder);

        if (!Directory.Exists(dataPath))
        {
          Directory.CreateDirectory(dataPath);
        }
      }

      Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal;

      while (true)
      {
        Console.Write("Searching safe prime...");

        Prime.GenerateAndStoreSafePrime(dataPath);

        Console.WriteLine("Found");
      }
    }
  }
}
