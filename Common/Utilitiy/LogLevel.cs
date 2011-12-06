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

namespace Pirate.PiVote
{
  /// <summary>
  /// Levels of serverity for log entries.
  /// </summary>
  public enum LogLevel
  {
    /// <summary>
    /// The server cannot continue to run.
    /// </summary>
    Emergency = 1,

    /// <summary>
    /// The action requested by the user failed.
    /// </summary>
    Error = 2,

    /// <summary>
    /// There might be a problem.
    /// </summary>
    Warning = 3,

    /// <summary>
    /// Just some information.
    /// </summary>
    Info = 4,

    /// <summary>
    /// Output only for debug.
    /// </summary>
    Debug = 5
  }

  /// <summary>
  /// Extension method for the LogLevel
  /// </summary>
  public static class LogLevelExtensions
  {
    public static EventLogEntryType ToLogEntryType(this LogLevel logLevel)
    {
      switch (logLevel)
      {
        case LogLevel.Debug:
          return EventLogEntryType.Information;
        case LogLevel.Info:
          return EventLogEntryType.Information;
        case LogLevel.Warning:
          return EventLogEntryType.Warning;
        case LogLevel.Error:
          return EventLogEntryType.Error;
        case  LogLevel.Emergency:
          return EventLogEntryType.Error;
        default:
          return EventLogEntryType.Warning;
      }
    }
  }
}
