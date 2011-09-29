using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirate.PiVote.DocumentationGenerator
{
  public class Request
  {
    public string Name { get; private set; }

    public string Text { get; private set; }

    public string Input { get; private set; }

    public string Output { get; private set; }

    public Request(string name, string text, string input, string output)
    {
      Name = name;
      Text = text;
      Input = input;
      Output = output;
    }
  }
}
