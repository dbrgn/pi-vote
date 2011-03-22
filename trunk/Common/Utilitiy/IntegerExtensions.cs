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

namespace System
{
  public static class IntegerExtensions
  {
    public static bool InRange(this int value, int min, int max)
    {
      return value >= min && value <= max;
    }
  }
}
