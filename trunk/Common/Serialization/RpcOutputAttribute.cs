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

namespace Pirate.PiVote.Serialization
{
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
  public class RpcOutputAttribute : Attribute
  {
    public string Comment { get; private set; }

    public RpcOutputAttribute(string comment)
    {
      Comment = comment;
    }
  }
}
