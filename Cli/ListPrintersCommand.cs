/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Text;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui.Printing;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Cli
{
  public class ListPrintersCommand : Command
  {
    public ListPrintersCommand(Status status)
      : base(status)
    { }

    protected override void Execute()
    {
      StringTable table = new StringTable();

      table.AddColumn("Printer Name");

      foreach (string printerName in PrinterSettings.InstalledPrinters)
      {
        table.AddRow(printerName);
      }

      Console.WriteLine(table.Render());
    }

    public override IEnumerable<string> Aliases
    {
      get
      {
        yield return "printers";
      }
    }

    public override string HelpText
    {
      get { return "List all installed printers."; }
    }
  }
}
