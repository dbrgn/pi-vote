using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirate.PiVote.Cli
{
  public class Controller
  {
    private Status status;

    private List<Command> commands;

    public IEnumerable<Command> Commands { get { return this.commands; } }

    public Controller()
    {
      this.status = new Status(this);
      this.commands = new List<Command>();
      this.commands.Add(new ExitCommand(this.status));
      this.commands.Add(new HelpCommand(this.status));
      this.commands.Add(new ConnectCommand(this.status));
      this.commands.Add(new ListVotingsCommend(this.status));
      this.commands.Add(new ListCertificatesCommand(this.status));
      this.commands.Add(new CreateCertificateCommand(this.status));
    }

    public void Loop()
    {
      while (this.status.Continue)
      {
        ReadCommand();
      }
    }

    private void ReadCommand()
    {
      Console.Write("PiVote: ");
      string commandText = Console.ReadLine();

      var commands = this.commands.Where(cmd => cmd.Aliases.Any(alias => commandText.StartsWith(alias)));

      if (commands.Count() == 1)
      {
        commands.Single().Execute(commandText);
      }
      else if (commands.Count() == 0)
      {
        Console.WriteLine("Command not known");
      }
      else
      {
        Console.WriteLine("Command ambigous");
      }
    }
  }
}