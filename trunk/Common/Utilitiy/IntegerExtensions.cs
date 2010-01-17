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
