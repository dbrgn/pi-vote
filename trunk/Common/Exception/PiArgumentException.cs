
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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote
{
  /// <summary>
  /// Exception from bad arguments to RPC calls.
  /// </summary>
  public class PiArgumentException : PiException
  {
    /// <summary>
    /// Creates an exception from a code and message.
    /// </summary>
    /// <param name="code">Identifiing code of exception.</param>
    /// <param name="message">English debugging message.</param>
    public PiArgumentException(ExceptionCode code, string message)
      : base(code, message)
    { }

    /// <summary>
    /// Creates an exception containing a standard exception.
    /// </summary>
    /// <param name="exception">Exception to be incorporated.</param>
    public PiArgumentException(Exception exception)
      : base(exception)
    { }
  }
}
