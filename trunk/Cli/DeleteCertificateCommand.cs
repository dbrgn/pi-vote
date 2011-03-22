/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Cli
{
  public class DeleteCertificateCommand : Command
  {
    public DeleteCertificateCommand(Status status)
      : base(status)
    { }

    protected override void Execute()
    {
      if (ArgCount >= 2)
      {
        string idText = ArgGetString(1);

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
          File.Delete(GetCertificateFileName(certificate));
          Console.WriteLine("Certificate id {0} deleted.", certificate.Id.ToString());
        }
      }
      else
      {
        Console.WriteLine("Usage: delete $id");
      }
    }

    public override IEnumerable<string> Aliases
    {
      get
      {
        yield return "delete";
      }
    }

    public override string HelpText
    {
      get { return "Deletes a certificate."; }
    }
  }
}
