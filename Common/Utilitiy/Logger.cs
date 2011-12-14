/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Pirate.PiVote.Utilitiy;

namespace Pirate.PiVote
{
  /// <summary>
  /// Logs interesting events of the voting server.
  /// </summary>
  public class Logger : ILogger
  {
    /// <summary>
    /// File name for the log file.
    /// </summary>
    public const string ServerLogFileName = "pi-vote-server.log";

    /// <summary>
    /// Source name in the event log.
    /// </summary>
    public const string EventLogSource = "Pi-Vote";

    /// <summary>
    /// Log name of the event log.
    /// </summary>
    public const string EventLogName = "Application";

    /// <summary>
    /// File stream of the log file.
    /// </summary>
    private FileStream logFileStream;

    /// <summary>
    /// Log entry writer.
    /// </summary>
    private TextWriter logWriter;

    /// <summary>
    /// Maximum log level to log.
    /// </summary>
    private LogLevel maxLogLevel;

    /// <summary>
    /// Unix syslog
    /// </summary>
    private Syslog syslog;

    /// <summary>
    /// Create a new logger.
    /// </summary>
    /// <param name="logFileName">Name and path of the log file</param>
    /// <param name="maxLogLevel">Maximum log level to log.</param>
    public Logger(string logFileName, LogLevel maxLogLevel)
    {
      this.maxLogLevel = maxLogLevel;
      this.logFileStream = new FileStream(logFileName, FileMode.Append, FileAccess.Write);
      this.logWriter = new StreamWriter(this.logFileStream);

      if (!EventLog.SourceExists(EventLogSource))
      {
        EventLog.CreateEventSource(EventLogSource, EventLogName);
      }

      if (Environment.OSVersion.Platform == PlatformID.Unix)
      {
        this.syslog = new Syslog();
      }
    }

    /// <summary>
    /// Log a message to file.
    /// </summary>
    /// <param name="logLevel">Level of the message.</param>
    /// <param name="message">Message string.</param>
    /// <param name="values">Values to be inserted into the string.</param>
    public void Log(LogLevel logLevel, string message, params object[] values)
    {
      if (logLevel <= this.maxLogLevel)
      {
        lock (this.logWriter)
        {
          Console.WriteLine(DateTime.Now.ToString("s") + " \t" + logLevel.ToString() + " \t" + message, values);
          this.logWriter.WriteLine(DateTime.Now.ToString("s") + " \t" + logLevel.ToString() + " \t" + message, values);
          this.logWriter.Flush();

          if (Environment.OSVersion.Platform == PlatformID.Unix)
          {
            this.syslog.Log(LogLevelToUnix(logLevel), string.Format(message, values));
          }
        }
      }
    }

    private Pirate.PiVote.Utilitiy.Syslog.SyslogSeverity LogLevelToUnix(LogLevel logLevel)
    {
      switch (logLevel)
      {
        case LogLevel.Emergency:
          return Utilitiy.Syslog.SyslogSeverity.Alert;
        case LogLevel.Error:
          return Utilitiy.Syslog.SyslogSeverity.Error;
        case LogLevel.Warning:
          return Utilitiy.Syslog.SyslogSeverity.Warning;
        default:
          return Utilitiy.Syslog.SyslogSeverity.Informational;
      }
    }

    /// <summary>
    /// Closes the log file.
    /// </summary>
    public void Close()
    {
      this.logWriter.Close();
      this.logFileStream.Close();
      this.syslog.Dispose();
    }
  }
}
