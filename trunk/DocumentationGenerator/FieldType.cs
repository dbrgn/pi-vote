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
  public class FieldType
  {
    public string Name { get; private set; }

    public string ShortName
    {
      get
      {
        return Name
          .Replace("Pirate.PiVote.", string.Empty)
          .Replace("System.", string.Empty);
      }
    }

    public FieldType(string name)
    {
      Name = name.Substring(0, 1).ToUpper() + name.Substring(1);
    }
  }
}
