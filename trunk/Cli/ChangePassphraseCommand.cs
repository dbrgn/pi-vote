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
  public class ChangePassphraseCommand : Command
  {
    public ChangePassphraseCommand(Status status)
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

          switch (certificate.PrivateKeyStatus)
          {
            case PrivateKeyStatus.Unavailable:
              Console.WriteLine("Private key is not available.");
              break;
            case PrivateKeyStatus.Unencrypted:
              string newPassphrase = ReadPasswordRepeated("Enter new passphrase: ", "Repeat passphrase: ");

              if (newPassphrase == null)
              {
                Console.WriteLine("Passphrases do not match.");
                return;
              }
              else if (newPassphrase == string.Empty)
              {
                Console.WriteLine("Nothing changed.");
                return;
              }

              try
              {
                certificate.EncryptPrivateKey(newPassphrase);
                SaveCertificate(certificate);
                Console.WriteLine("Passphrase set.");
              }
              catch (Exception exception)
              {
                Console.WriteLine(exception.Message);
              }

              break;
            case PrivateKeyStatus.Encrypted:
            case PrivateKeyStatus.Decrypted:
              string oldPassphrase = ReadPassword("Enter old passphrase: ");
              string newPassphrase2 = ReadPasswordRepeated("Enter new passphrase: ", "Repeat passphrase: ");

              if (newPassphrase2 == null)
              {
                Console.WriteLine("Passphrases do not match.");
                return;
              }
              else if (newPassphrase2 == string.Empty)
              {
                try
                {
                  certificate.DecryptPrivateKey(oldPassphrase);
                  SaveCertificate(certificate);
                  Console.WriteLine("Passphrase removed.");
                }
                catch (Exception exception)
                {
                  Console.WriteLine(exception.Message);
                }
              }
              else
              {
                try
                {
                  certificate.ChangePassphrase(oldPassphrase, newPassphrase2);
                  SaveCertificate(certificate);
                  Console.WriteLine("Passphrase changed.");
                }
                catch (Exception exception)
                {
                  Console.WriteLine(exception.Message);
                }
              }

              break;
          }
        }
      }
      else
      {
        Console.WriteLine("Usage: passphrase $id");
      }
    }

    public override IEnumerable<string> Aliases
    {
      get
      {
        yield return "passphrase";
      }
    }

    public override string HelpText
    {
      get { return "Changes the passphrase on a certificate."; }
    }
  }
}
