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
  [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
  public class GenericArgumentAttribute : Attribute
  {
    public int Index { get; private set; }

    public string Name { get; private set; }

    public string Comment { get; private set; }

    public GenericArgumentAttribute(int index, string name, string comment)
    {
      Index = index;
      Name = name;
      Comment = comment;
    }
  }
}
