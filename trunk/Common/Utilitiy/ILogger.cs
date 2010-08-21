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
  /// An abstract logger.
  /// </summary>
  public interface ILogger
  {
    /// <summary>
    /// Closes the log.
    /// </summary>
    void Close();

    /// <summary>
    /// Log a message.
    /// </summary>
    /// <param name="logLevel">Level of the message.</param>
    /// <param name="message">Message string.</param>
    /// <param name="values">Values to be inserted into the string.</param>
    void Log(LogLevel logLevel, string message, params object[] values);
  }
}
