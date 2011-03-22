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
using Pirate.PiVote.Printing;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Cli
{
  public class UploadCommand : Command
  {
    public UploadCommand(Status status)
      : base(status)
    { }

    protected override void Execute()
    {
      if (ArgCount >= 2)
      {
        string idText = ArgGetString(1);
        bool notify = true;

        if (ArgCount >= 3)
        {
          if (ArgIsBoolean(2))
          {
            notify = ArgGetBoolean(2);
          }
          else
          {
            Console.WriteLine("Notify argument is not boolean.");
            return;
          }
        }

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

          if (HasSignatureRequestDataFile(certificate))
          {
            var caCertificate = Status.CertificateStorage
              .Certificates.Where(c => c is CACertificate &&
                c.Validate(Status.CertificateStorage) == CertificateValidationResult.Valid)
              .FirstOrDefault();

            if (caCertificate == null)
            {
              Console.WriteLine("Cannot find any valid Certififcate Authority.");
            }

            if (UnlockCertificate(certificate))
            {
              var request = Serializable.Load<SignatureRequest>(SignatureRequestDataFileName(certificate));
              var requestInfo = new SignatureRequestInfo(request.EmailAddress);
              var secureRequest = new Secure<SignatureRequest>(request, caCertificate, certificate);
              var secureRequestInfo = new Secure<SignatureRequestInfo>(requestInfo, Status.ServerCertificate, certificate);
              certificate.Lock();

              Begin("Uploading signature request...");
              Status.Client.SetSignatureRequest(secureRequest, secureRequestInfo, SetSignatureRequestCompleted);
              WaitForCompletion();
            }
          }
          else
          {
            Console.WriteLine("Cannot find signature request file.");
          }
        }
      }
      else
      {
        Console.WriteLine("Usage: upload $id [$notify]");
      }
    }

    private void SetSignatureRequestCompleted(Exception exception)
    {
      CompleteAndReady(exception);
    }

    public override IEnumerable<string> Aliases
    {
      get
      {
        yield return "upload";
      }
    }

    public override string HelpText
    {
      get { return "Upload a signature request to the server."; }
    }
  }
}
