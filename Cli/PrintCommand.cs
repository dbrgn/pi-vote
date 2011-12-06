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
  public class PrintCommand : Command
  {
    public PrintCommand(Status status)
      : base(status)
    { }

    protected override void Execute()
    {
      if (ArgCount >= 3)
      {
        string idText = ArgGetString(1);
        string printer = ArgGetString(2);

        var candidates = Certificates
          .Where(certificate => certificate.Id.ToString().Contains(idText));

        if (candidates.Count() == 0)
        {
          Console.WriteLine("No certificate with such id found.");
        }
        else if (candidates.Count() > 1)
        {
          Console.WriteLine("Given id is ambigous.");
        }
        else
        {
          var certificate = candidates.Single();

          var printerCandidates = PrinterSettings.InstalledPrinters
            .Cast<string>()
            .Where(printerName => printerName.Contains(printer));

          if (printerCandidates.Count() == 0)
          {
            Console.WriteLine("No printer with such name found.");
          }
          else if (printerCandidates.Count() > 1)
          {
            Console.WriteLine("Given printer name is ambigous.");
          }
          else
          {
            string printerName = printerCandidates.Single();

            if (HasSignatureRequestDataFile(certificate))
            {
              var request = Serializable.Load<SignatureRequest>(SignatureRequestDataFileName(certificate));
              SignatureRequestDocument document = new SignatureRequestDocument(request, certificate, Status.GetGroupName);
              document.PrinterSettings.PrinterName = printerName;
              document.Print();
            }
            else
            {
              Console.WriteLine("Cannot find signature request file.");
            }
          }
        }
      }
      else
      {
        Console.WriteLine("Usage: print $id $printer");
      }
    }

    public override IEnumerable<string> Aliases
    {
      get
      {
        yield return "print";
      }
    }

    public override string HelpText
    {
      get { return "Print a signature request from."; }
    }
  }
}
