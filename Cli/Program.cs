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
      var parameters = new List<string>();
      var options = new List<string>();
      var handler = new Handler();

      if (args.Length >= 2)
      {
        var verb = args[0];
        var subject = args[1];

        for (var i = 2; i < args.Length; i++)
        {
          if (args[i].StartsWith("--"))
          {
            options.Add(args[i].Substring(2));
          }
          else if (args[i].StartsWith("\"") && args[i].EndsWith("\""))
          {
            parameters.Add(args[i].Substring(1, args[i].Length - 2));
          }
          else
          {
            parameters.Add(args[i]);
          }
        }

        handler.Execute(verb, subject, parameters, options);
      }
      else
      {
        Console.WriteLine("Verb or subject missing.");
      }
    }
  }
}
