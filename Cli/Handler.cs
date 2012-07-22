using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirate.PiVote.Cli
{
  public class Handler
  {
    private List<Command> _commands;

    public Handler()
    {
      _commands = new List<Command>();
      _commands.Add(new ListCertificateCommand());
    }

    public void Execute(string verb, string subject, IEnumerable<string> parameters, IEnumerable<string> options)
    {
      var command = _commands.Where(c => c.Verb == verb && c.Subject == subject).SingleOrDefault();

      if (command == null)
      {
        Console.WriteLine("Unknown command.");
      }
      else
      {
        command.Execute(parameters, options);
      }
    }
  }
}
