using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirate.PiVote.Cli
{
  public class ListCertificateCommand : Command
  {
    public override string Verb
    {
      get { return "list"; }
    }

    public override string Subject
    {
      get { return "certificate"; }
    }

    public override void Execute(IEnumerable<string> parameters, IEnumerable<string> options)
    {
      
    }
  }
}
