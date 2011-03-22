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

namespace Pirate.PiVote.DocumentationGenerator
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      Analyzer analyzer = new Analyzer();
      analyzer.Analyze();
      Generator generator = new Generator();
      generator.Generate(analyzer.Types);
      System.IO.File.WriteAllText("doc.tex", generator.Text);
    }
  }
}
