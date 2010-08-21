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
}
