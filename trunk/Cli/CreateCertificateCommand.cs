﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Cli
{
  public class CreateCertificateCommand : Command
  {
    public CreateCertificateCommand(Status status)
      : base(status)
    { }

    protected override void Execute()
    {
      if (ArgCount >= 2)
      {
        string type = ArgGetString(1);

        if (type == "voter")
        {
          CreateVoter();
        }
        else if (type == "authority")
        {
          CreateAuthority();
        }
        else
        {
          Console.WriteLine("Unknown certificate type.");
          Usage();
        }
      }
      else
      {
        Console.WriteLine("Wrong argument count.");
        Usage();
      }
    }

    private void CreateAuthority()
    {
      if (ArgCount == 6)
      {
        string firstName = ArgGetString(2);
        string lastName = ArgGetString(3);
        string emailAddress = ArgGetString(4);
        string functionName = ArgGetString(5);

        if (!Mailer.IsEmailAddressValid(emailAddress))
        {
          Console.WriteLine("Invalid email address.");
          Usage();
          return;
        }

        string fullName = string.Format("{0} {1}, {2}",
          firstName,
          lastName,
          functionName);
        Status.Certificate = new AuthorityCertificate(Language.English, null, fullName);
        Status.Certificate.CreateSelfSignature();
        Status.Certificate.Save(Status.CertificateFileName);

        var request = new SignatureRequest(firstName, lastName, emailAddress);

        request.Save(Status.SignatureRequestDataFileName);

        Console.WriteLine("Certificate with id {0} created.", Status.Certificate.Id.ToString());
      }
      else
      {
        Console.WriteLine("Wrong argument count.");
        Usage();
      }
    }

    private void Usage()
    {
      Console.WriteLine("Usage: create voter $firstname $lastname $email $group");
      Console.WriteLine("       create authority $firstname $lastname $email $function");
    }

    private void CreateVoter()
    {
      if (ArgCount == 6)
      {
        string firstName = ArgGetString(2);
        string lastName = ArgGetString(3);
        string emailAddress = ArgGetString(4);

        if (!ArgIsInt32(5))
        {
          Console.WriteLine("Invalid group id.");
          Usage();
          return;
        }

        int groupId = ArgGetInt32(5);

        if (!Status.Groups.Any(group => group.Id == groupId))
        {
          Console.WriteLine("Invalid group id.");
          Usage();
          return;
        }

        if (!Mailer.IsEmailAddressValid(emailAddress))
        {
          Console.WriteLine("Invalid email address.");
          Usage();
          return;
        }

        Status.Certificate = new VoterCertificate(Language.English, null, groupId);
        Status.Certificate.CreateSelfSignature(); 
        Status.Certificate.Save(Status.CertificateFileName);

        var request = new SignatureRequest(firstName, lastName, emailAddress);

        request.Save(Status.SignatureRequestDataFileName);

        Console.WriteLine("Certificate with id {0} created.", Status.Certificate.Id.ToString());
      }
      else
      {
        Console.WriteLine("Wrong argument count.");
        Usage();
      }
    }

    public override IEnumerable<string> Aliases
    {
      get
      {
        yield return "create";
      }
    }

    public override string HelpText
    {
      get { return "Lists all votings."; }
    }
  }
}