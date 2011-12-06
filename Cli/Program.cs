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

namespace Pirate.PiVote.Cli
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      Controller controller = new Controller();

      if (args.Length >= 1)
      {
        controller.Execute(args);
      }
      else
      {
        controller.Loop();
      }
    }
  }
}
