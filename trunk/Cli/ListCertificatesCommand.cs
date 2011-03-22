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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Cli
{
  public class ListCertificatesCommand : Command
  {
    public ListCertificatesCommand(Status status)
      : base(status)
    { }

    protected override void Execute()
    {
      DirectoryInfo directory = new DirectoryInfo(Status.DataPath);
      StringTable table = new StringTable();
      table.AddColumn("Id");
      table.AddColumn("Type");
      table.AddColumn("Group/Name");
      table.AddColumn("Status");

      foreach (var file in directory.GetFiles(Files.CertificatePattern))
      {
        var certificate = Serializable.Load<Certificate>(file.FullName);
        string groupNameText = string.Empty;

        if (certificate is VoterCertificate)
        {
          groupNameText = Status.GetGroupName(((VoterCertificate)certificate).GroupId);
        }
        else if (certificate is AuthorityCertificate)
        {
          groupNameText = ((AuthorityCertificate)certificate).FullName;
        }
        else if (certificate is AdminCertificate)
        {
          groupNameText = ((AdminCertificate)certificate).FullName;
        }
        else if (certificate is ServerCertificate)
        {
          groupNameText = ((ServerCertificate)certificate).FullName;
        }
        else if (certificate is CACertificate)
        {
          groupNameText = ((CACertificate)certificate).FullName;
        }
        else
        {
          groupNameText = @"N/A";
        }

        table.AddRow(
          certificate.Id.ToString(),
          certificate.TypeText,
          groupNameText,
          certificate.Validate(Status.CertificateStorage).ToString());
      }

      Console.WriteLine(table.Render());
    }

    public override IEnumerable<string> Aliases
    {
      get
      {
        yield return "certs";
      }
    }

    public override string HelpText
    {
      get { return "Lists all certificates."; }
    }
  }
}
