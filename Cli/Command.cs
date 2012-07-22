using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirate.PiVote.Cli
{
  public abstract class Command
  {
    public abstract string Verb { get; }

    public abstract string Subject { get; }

    public abstract void Execute(IEnumerable<string> parameters, IEnumerable<string> options);
  }
}
