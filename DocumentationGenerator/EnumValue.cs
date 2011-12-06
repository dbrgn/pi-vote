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

namespace Pirate.PiVote.DocumentationGenerator
{
  public class EnumValue
  {
    public int Value { get; private set; }

    public string Name { get; private set; }

    public EnumValue(int value, string name)
    {
      Value = value;
      Name = name;
    }
  }
}
