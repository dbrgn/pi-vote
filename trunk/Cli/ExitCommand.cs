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
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Cli
{
  public class ExitCommand : Command
  {
    public ExitCommand(Status status)
      : base(status)
    { }

    protected override void Execute()
    {
      if (Status.Client != null && Status.Client.Connected)
      {
        Status.Client.Close();
      }

      Status.Continue = false;
    }

    public override IEnumerable<string> Aliases
    {
      get
      {
        yield return "exit";
      }
    }

    public override string HelpText
    {
      get { return "Exits the program."; }
    }
  }
}
