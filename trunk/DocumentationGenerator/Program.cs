using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentationGenerator
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
