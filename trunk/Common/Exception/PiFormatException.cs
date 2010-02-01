
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
  public class PiFormatException : PiException
  {
    public PiFormatException(ExceptionCode code, string message)
      : base(code, message)
    { }

    public PiFormatException(Exception exception)
      : base(exception)
    { }
  }
}
