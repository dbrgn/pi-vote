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
using System.IO;
using System.Diagnostics;
using System.Reflection;
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

      Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.BelowNormal;

      while (true)
      {
        Console.Write("Searching safe prime...");

        Prime.GenerateAndStoreSafePrime();

        Console.WriteLine("Found");
      }
    }
  }
}
