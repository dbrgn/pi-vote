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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote
{
  /// <summary>
  /// Bad serialized data format exception.
  /// </summary>
  public class PiFormatException : PiException
  {
    /// <summary>
    /// Creates an exception from a code and message.
    /// </summary>
    /// <param name="code">Identifiing code of exception.</param>
    /// <param name="message">English debugging message.</param>
    public PiFormatException(ExceptionCode code, string message)
      : base(code, message)
    { }

    /// <summary>
    /// Creates an exception containing a standard exception.
    /// </summary>
    /// <param name="exception">Exception to be incorporated.</param>
    public PiFormatException(Exception exception)
      : base(exception)
    { }
  }
}
