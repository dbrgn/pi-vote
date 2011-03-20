using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;

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
      this.commands.Add(new ListVotingsCommend(this.status));
      this.commands.Add(new ListCertificatesCommand(this.status));
      this.commands.Add(new CreateCertificateCommand(this.status));
      this.commands.Add(new DeleteCertificateCommand(this.status));
      this.commands.Add(new ChangePassphraseCommand(this.status));
      this.commands.Add(new PrintCommand(this.status));
      this.commands.Add(new ListPrintersCommand(this.status));
      this.commands.Add(new UploadCommand(this.status));
      this.commands.Add(new CheckCommand(this.status));
      this.commands.Add(new AutoCACommand(this.status));
    }

    public void Execute(string[] commandLine)
    {
      new ConnectCommand(this.status).Execute(string.Empty);

      string fileName = Assembly.GetEntryAssembly().GetFiles().Single().Name;
      int fileNameIndex = Environment.CommandLine.IndexOf(fileName);
      string commandText = Environment.CommandLine.Substring(fileNameIndex + fileName.Length + 1).Trim();
      Console.WriteLine(commandText);

      ExecuteCommand(commandText);

      this.status.Disconnect();
    }

    public void Loop()
    {
      new ConnectCommand(this.status).Execute(string.Empty);

      while (this.status.Continue)
      {
        ReadCommand();
      }

      this.status.Disconnect();
    }

    private void ReadCommand()
    {
      Console.Write("PiVote: ");
      string commandText = Console.ReadLine();

      ExecuteCommand(commandText);
    }

    private void ExecuteCommand(string commandText)
    {
      string commandAlias = commandText.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries)[0];
      var commands = this.commands.Where(cmd => cmd.Aliases.Contains(commandAlias));

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