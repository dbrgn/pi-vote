using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using Emil.GMP;

namespace Pirate.PiVote.Crypto
{
  public static class CryptoLog
  {
    private class Entry
    {
      private string text;

      private Dictionary<string, object> properties;

      private List<Entry> children;

      public Entry Parent { get; private set; }

      public Entry(Entry parent, string text)
      {
        this.text = text;
        this.properties = new Dictionary<string, object>();
        this.children = new List<Entry>();
        Parent = parent;
      }

      public Entry Add(string text)
      {
        var entry = new Entry(this, text);
        this.children.Add(entry);
        return entry;
      }

      public void Add(string text, object value)
      {
        this.properties.Add(text, value);
      }

      public string Output()
      {
        StringBuilder output = new StringBuilder();
        Output(string.Empty, output);
        return output.ToString();
      }

      private void Output(string indent, StringBuilder output)
      {
        output.AppendLine(indent + this.text);

        foreach (var property in this.properties)
        {
          output.AppendLine(indent + "  " + property.Key + ": " + Text(property.Value));
        }

        foreach (var entry in this.children)
        {
          entry.Output(indent + "    ", output);
        }
      }

      private string Text(object value)
      {
        if (value is BigInt)
        {
          return (value as BigInt).ToString(16);
        }
        else if (value is byte[])
        {
          return (value as byte[]).ToHexString();
        }
        else
        {
          return value.ToString();
        }
      }
    }

    public const string FileNameFormat = "pi-vote-crypto.{0}.log";

    public const string DateTimeFormat = "yyyyMMddHHmmss";

    public static CryptoLogLevel Level = CryptoLogLevel.Numeric;

    private static TextWriter logWriter;

    private static Dictionary<int, Entry> threadEntries;

    public static void Open(string dataPath)
    { 
      string fileName = Path.Combine(dataPath, string.Format(FileNameFormat, DateTime.Now.ToString(DateTimeFormat)));
      var file = File.OpenWrite(fileName);
      logWriter = new StreamWriter(file);
      threadEntries = new Dictionary<int, Entry>();
    }

    public static void Begin(CryptoLogLevel level, string text)
    {
      if (logWriter != null &&
          level <= Level)
      {
        int threadId = Thread.CurrentThread.ManagedThreadId;

        lock (threadEntries)
        {
          if (!threadEntries.ContainsKey(threadId))
          {
            threadEntries.Add(threadId, null);
          }
        }

        if (threadEntries[threadId] == null)
        {
          threadEntries[threadId] = new Entry(null, text);
        }
        else
        {
          threadEntries[threadId] = threadEntries[threadId].Add(text);
        }
      }
    }

    public static void Add(CryptoLogLevel level, string text, object value)
    {
      if (logWriter != null &&
          level <= Level)
      {
        int threadId = Thread.CurrentThread.ManagedThreadId;
        threadEntries[threadId].Add(text, value);
      }
    }

    public static void End(CryptoLogLevel level)
    {
      if (logWriter != null &&
          level <= Level)
      {
        int threadId = Thread.CurrentThread.ManagedThreadId;
        threadEntries[threadId] = threadEntries[threadId].Parent;
      }
    }

    public static void EndWrite()
    {
      if (logWriter != null)
      {
        int threadId = Thread.CurrentThread.ManagedThreadId;

        if (threadEntries.ContainsKey(threadId))
        {
          var entry = threadEntries[threadId];

          if (entry != null)
          {
            if (entry.Parent != null)
              throw new InvalidOperationException("Cannot commit non-root entry.");

            string output = entry.Output();

            lock (logWriter)
            {
              logWriter.Write(output);
              logWriter.Flush();
            }

            threadEntries[threadId] = null;
          }
        }
      }
    }
  }
}
