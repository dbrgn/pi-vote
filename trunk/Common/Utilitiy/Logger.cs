/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Pirate.PiVote
{
  /// <summary>
  /// Logs interesting events of the voting server.
  /// </summary>
  public class Logger : ILogger
  {
    public const string ServerLogFileName = "pi-vote-server.log";

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
    /// Create a new logger.
    /// </summary>
    /// <param name="logFileName">Name and path of the log file</param>
    /// <param name="maxLogLevel">Maximum log level to log.</param>
    public Logger(string logFileName, LogLevel maxLogLevel)
    {
      this.maxLogLevel = maxLogLevel;
      this.logFileStream = new FileStream(logFileName, FileMode.Append, FileAccess.Write);
      this.logWriter = new StreamWriter(this.logFileStream);
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
        }
      }
    }

    /// <summary>
    /// Closes the log file.
    /// </summary>
    public void Close()
    {
      this.logWriter.Close();
      this.logFileStream.Close();
    }
  }
}
