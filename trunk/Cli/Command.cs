using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

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

    protected bool ArgGetBoolean(int index)
    {
      string text = ArgGetString(index);

      switch (text.ToLower())
      {
        case "0":
        case "f":
        case "false":
          return false;
        case "1":
        case "t":
        case "true":
          return true;
        default:
          throw new ArgumentOutOfRangeException("Argument is not boolean.");
      }
    }

    protected bool ArgIsBoolean(int index)
    {
      string text = ArgGetString(index);

      switch (text.ToLower())
      {
        case "0":
        case "f":
        case "false":
        case "1":
        case "t":
        case "true":
          return true;
        default:
          return false;
      }
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

    protected string ReadPasswordRepeated(string message, string repeatMessage)
    {
      string password1 = ReadPassword(message);
      string password2 = ReadPassword(repeatMessage);

      if (password1 == password2)
      {
        return password1;
      }
      else
      {
        return null;
      }
    }

    protected string ReadPassword(string message)
    {
      string password = string.Empty;

      Console.Write(message);

      while (true)
      {
        var key = Console.ReadKey(true);

        switch (key.Key)
        {
          case ConsoleKey.Enter:
            Console.WriteLine();
            return password;
          case ConsoleKey.Backspace:
            if (password.Length > 0)
            {
              password = password.Substring(0, password.Length - 1);
              Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
              Console.Write(" ");
              Console.SetCursorPosition(Console.CursorLeft - 1, Console.CursorTop);
            }

            break;
          default:
            password += key.KeyChar;
            Console.Write("*");
            break;
        }
      }
    }

    protected IEnumerable<Certificate> Certificates
    {
      get
      {
        DirectoryInfo directory = new DirectoryInfo(Status.DataPath);

        foreach (var file in directory.GetFiles(Files.CertificatePattern))
        {
          var certificate = Serializable.Load<Certificate>(file.FullName);

          yield return certificate;
        }
      }
    }

    protected string GetCertificateFileName(Certificate certificate)
    {
      return Path.Combine(Status.DataPath, certificate.Id.ToString() + Files.CertificateExtension);
    }

    protected void SaveCertificate(Certificate certificate)
    {
      var fileName = GetCertificateFileName(certificate);
      certificate.Save(fileName);
    }

    protected bool HasSignatureRequestDataFile(Certificate certificate)
    {
      return File.Exists(SignatureRequestDataFileName(certificate));
    }

    protected string SignatureRequestDataFileName(Certificate certificate)
    {
      return Path.Combine(Status.DataPath, certificate.Id.ToString() + Files.SignatureRequestDataExtension);
    }

    protected bool UnlockCertificate(Certificate certificate)
    { 
      string passphrase = ReadPassword("Enter passphrase: ");

      try
      {
        certificate.Unlock(passphrase);
        return true;
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception.Message);
        return false;
      }
    }
  }
}
