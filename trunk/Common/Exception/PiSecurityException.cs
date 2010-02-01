
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
  public class PiSecurityException : PiException
  {
    public PiSecurityException(ExceptionCode code, string message)
      : base(code, message)
    { }

    public PiSecurityException(Exception exception)
      : base(exception)
    { }
  }
}
