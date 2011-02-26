using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Pirate.PiVote.Cli
{
  public abstract class Command
  {
    private enum Phase
    {
      Running,
      Report,
      Wait,
      Ready
    }

    private Phase phase;

    private Exception exception;

    private IEnumerable<string> arguments;

    public abstract IEnumerable<string> Aliases { get; }

    public void Execute(string text)
    {
      this.arguments = Arguments(text);
      Execute();
    }

    protected abstract void Execute();

    public abstract string HelpText { get; }

    protected Status Status { get; private set; }

    public Command(Status status)
    {
      Status = status;
    }

    protected void Begin(string message)
    {
      Console.Write(message);
      this.phase = Phase.Running;
    }

    protected void WaitForCompletion()
    {
      while (this.phase == Phase.Running)
      {
        Thread.Sleep(1);
      }

      if (this.exception == null)
      {
        Console.WriteLine("Success");
      }
      else
      {
        Console.WriteLine("Failed: " + this.exception.Message);
      }

      this.phase = Phase.Wait;

      while (this.phase == Phase.Wait)
      {
        Thread.Sleep(1);
      }
    }

    protected void Complete(Exception exception)
    {
      this.exception = exception;
      this.phase = Phase.Report;

      while (this.phase == Phase.Report)
      {
        Thread.Sleep(1);
      }
    }

    protected void Ready()
    {
      this.phase = Phase.Ready;
    }

    protected void CompleteAndReady(Exception exception)
    {
      Complete(exception);
      Ready();
    }

    protected string ArgGetString(int index)
    {
      return this.arguments.ElementAt(index);
    }

    protected int ArgGetInt32(int index)
    {
      return int.Parse(this.arguments.ElementAt(index));
    }

    protected bool ArgIsInt32(int index)
    {
      int dummy = 0;
      return int.TryParse(this.arguments.ElementAt(index), out dummy);
    }

    protected int ArgCount
    {
      get { return this.arguments.Count(); }
    }

    private IEnumerable<string> Arguments(string text)
    {
      bool quoted = false;
      string current = string.Empty;

      for (int index = 0; index < text.Length; index++)
      {
        string character = text.Substring(index, 1);

        if (quoted)
        {
          switch (character)
          {
            case "\"":
              quoted = false;
              break;
            default:
              current += character;
              break;
          }
        }
        else
        {
          switch (character)
          {
            case "\"":
              quoted = true;
              break;
            case " ":
              if (!string.IsNullOrEmpty(current))
              {
                yield return current;
              }

              current = string.Empty;
              break;
            default:
              current += character;
              break;
          }
        }
      }

      if (!string.IsNullOrEmpty(current))
      {
        yield return current;
      }
    }
  }
}
