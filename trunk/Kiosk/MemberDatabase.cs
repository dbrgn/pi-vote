using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Pirate.PiVote.Kiosk
{
  public class MemberDatabase
  {
    public class Entry
    {
      public string Surname { get; private set; }

      public string Givenname { get; private set; }

      public string EmailAddress { get; private set; }

      public string Name { get { return Givenname + " " + Surname; } }

      public Entry(string surname, string givenname, string emailAddress)
      {
        Surname = surname;
        Givenname = givenname;
        EmailAddress = emailAddress;
      }

      public static Entry TryParse(string text)
      {
        if (text.StartsWith("\"") && text.EndsWith("\""))
        {
          string[] parts = text.Substring(1, text.Length - 2).Split(new string[] { "\",\"" }, StringSplitOptions.None);

          if (parts.Length == 4)
          {
            string givenname = parts[1];
            string emailAddress = parts[2];
            string surname = parts[3];

            return new Entry(surname, givenname, emailAddress);
          }
          else
          {
            return null;
          }
        }
        else
        {
          return null;
        }
      }
    }

    private List<Entry> entries;

    public IEnumerable<Entry> Entries { get { return this.entries; } }

    public MemberDatabase()
    {
      this.entries = new List<Entry>();
    }

    public void ImportCsv(string fileName)
    {
      string[] lines = File.ReadAllLines(fileName);

      for (int lineIndex = 1; lineIndex < lines.Length; lineIndex++)
      {
        var entry = Entry.TryParse(lines[lineIndex]);

        if (entry != null)
        {
          this.entries.Add(entry);
        }
      }
    }
  }
}
