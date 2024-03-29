﻿/*
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
  [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
  public class SerializeFieldAttribute : Attribute
  {
    public int Index { get; private set; }

    public string Comment { get; private set; }

    public string Condition { get; private set; }

    public Type AlternateType { get; private set; }

    public int MinVersion { get; private set; }

    public SerializeFieldAttribute(int index, string comment)
      : this(index, comment, string.Empty, null, 0)
    { 
    }

    public SerializeFieldAttribute(int index, string comment, int minVersion)
      : this(index, comment, string.Empty, null, minVersion)
    {
    }

    public SerializeFieldAttribute(int index, string comment, Type alternateType)
      : this(index, comment, string.Empty, alternateType, 0)
    {
    }

    public SerializeFieldAttribute(int index, string comment, string condition, Type alternateType, int minVersion)
    {
      Index = index;
      Comment = comment;
      Condition = condition;
      AlternateType = alternateType;
      MinVersion = minVersion;
    }
  }
}
