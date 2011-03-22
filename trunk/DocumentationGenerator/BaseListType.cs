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
  public class BaseListType : FieldType
  {
    public string Comment { get; private set; }

    public BaseListType(string name, string comment)
      : base(name)
    {
      Comment = comment;
    }
  }
}
