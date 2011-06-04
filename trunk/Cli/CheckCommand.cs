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
  public class CheckCommand : Command
  {
    public CheckCommand(Status status)
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
          Status.Certificate = certificate;

          Begin("Fetching signature response...");
          Status.Client.GetSignatureResponse(certificate.Id, GetSignatureResponseCompleted);
          WaitForCompletion();
        }
      }
      else
      {
        Console.WriteLine("Usage: check $id");
      }
    }

    private void GetSignatureResponseCompleted(SignatureResponseStatus status, Signed<SignatureResponse> response, Exception exception)
    {
      Complete(exception);

      if (exception == null)
      {
        switch (status)
        {
          case SignatureResponseStatus.Accepted:
            Console.WriteLine("Your signature request was accepted.");

            if (response.Verify(Status.CertificateStorage) && response.Value.Status == status)
            {
              var signature = response.Value.Signature;
              Status.Certificate.AddSignature(signature);
              SaveCertificate(Status.Certificate);
              Console.WriteLine("The signature has been added to your certificate.");
            }
            else
            {
              Console.WriteLine("The answer is not authentic or corrupt.");
            }

            break;
          case SignatureResponseStatus.Declined:
            Console.WriteLine("Your signature request was declined.");

            if (response.Verify(Status.CertificateStorage) && response.Value.Status == status)
            {
              Console.WriteLine("Stated reason: " + response.Value.Reason);
            }
            else
            {
              Console.WriteLine("The answer is not authentic or corrupt.");
            }

            break;
          case SignatureResponseStatus.Pending:
            Console.WriteLine("Your signature request is still pending on the server.");
            break;
          case SignatureResponseStatus.Unknown:
            Console.WriteLine("There is no information about your signature request.");
            break;
        }
      }

      Ready();
    }

    public override IEnumerable<string> Aliases
    {
      get
      {
        yield return "check";
      }
    }

    public override string HelpText
    {
      get { return "Check if there's an answer to your signature request."; }
    }
  }
}
