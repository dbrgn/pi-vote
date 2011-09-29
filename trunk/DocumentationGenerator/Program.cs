/*
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

namespace Pirate.PiVote.DocumentationGenerator
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      Console.WriteLine("PiVote Documentation Generator");

      AssemblyName programName = Assembly.GetExecutingAssembly().GetName();
      AssemblyName libraryName = typeof(Pirate.PiVote.Serialization.Serializable).Assembly.GetName();

      Console.WriteLine("Program version {0}", programName.Version.ToString());
      Console.WriteLine("Library version {0}", libraryName.Version.ToString());
      Console.WriteLine();
      
      Analyzer analyzer = new Analyzer();
      analyzer.Analyze();
      Generator generator = new Generator();
      generator.Generate(analyzer.Types, analyzer.Requests);
      System.IO.File.WriteAllText("doc.tex", generator.Text);

      Console.WriteLine("Latex document completed.");
      Console.ReadLine();
    }
  }
}
