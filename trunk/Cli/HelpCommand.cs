using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Cli
{
  public class HelpCommand : Command
  {
    public HelpCommand(Status status)
      : base(status)
    { }

    protected override void Execute()
    {
      StringTable table = new StringTable();

      table.AddColumn("Command");
      table.AddColumn("Description");

      foreach (var command in Status.Controller.Commands)
      {
        table.AddRow(command.Aliases.First(), command.HelpText);
      }

      Console.WriteLine(table.Render());
    }

    public override IEnumerable<string> Aliases
    {
      get
      {
        yield return "help";
      }
    }

    public override string HelpText
    {
      get { return "Show help text for all commands."; }
    }
  }
}
