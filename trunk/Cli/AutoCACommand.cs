using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;
using System.IO;

namespace Pirate.PiVote.Cli
{
  public class AutoCACommand : Command
  {
    private string dataPath;
    private string donePath;
    private string passPhrase;
    private CACertificate caCertificate;

    public AutoCACommand(Status status)
      : base(status)
    { }

    protected override void Execute()
    {
      if (ArgCount < 3)
      {
        Console.WriteLine("autoca $path $passphrase");
        return;
      }

      this.dataPath = ArgGetString(1);
      this.passPhrase = ArgGetString(2);
      this.donePath = Path.Combine(this.dataPath, "Done");

      string caCertificateFileName = Path.Combine(this.dataPath, Files.RootCertificateFileName);

      try
      {
        if (!Directory.Exists(this.donePath))
        {
          Directory.CreateDirectory(this.donePath);
        }

        this.caCertificate = Serializable.Load<CACertificate>(caCertificateFileName);

        if (!this.caCertificate.FullName.ToUpperInvariant().Contains("TEST"))
        {
          throw new InvalidOperationException("Thou shalt not use Auto CA with thy operational CA.");
        }

        if (this.caCertificate.PrivateKeyStatus == PrivateKeyStatus.Encrypted)
        {
          this.caCertificate.Unlock(this.passPhrase);
        }

        Loop();
      }
      catch (Exception exception)
      {
        Console.WriteLine(exception.Message);
      }
    }

    private void Loop()
    {
      DateTime lastWork = DateTime.Now;

      while (true)
      {
        if (DateTime.Now.Subtract(lastWork).TotalSeconds > 5d)
        {
          Approve();
          lastWork = DateTime.Now;
        }
      }
    }

    private void Approve()
    {
      Begin("Get signature requests ... ");
      Status.Client.GetSignatureRequests(this.dataPath, OnSimpleOperationCompleted);
      WaitForCompletion();

      DirectoryInfo dataDirectory = new DirectoryInfo(this.dataPath);
      List<string> signatureReponseFilenames = new List<string>();

      foreach (var file in dataDirectory.GetFiles(Files.SignatureRequestPattern))
      {
        var secureRequest = Serializable.Load<Secure<SignatureRequest>>(file.FullName);
        var request = secureRequest.Value.Decrypt(this.caCertificate);

        Console.Write("Signature request from {0} ... ", request.FullName);

        if (secureRequest.VerifySimple())
        {
          if (request.FirstName.EndsWith("$"))
          {
            var response = new SignatureResponse(secureRequest.Certificate.Id, "This sucks.");
            var signedResponse = new Signed<SignatureResponse>(response, this.caCertificate);
            var responseFilename = Path.Combine(this.dataPath, secureRequest.Certificate.Id.ToString() + Files.SignatureResponseExtension);
            signedResponse.Save(responseFilename);
            signatureReponseFilenames.Add(responseFilename);
            File.Move(file.FullName, Path.Combine(this.donePath, file.Name));
            Console.WriteLine("Declined.");
          }
          else
          {
            var signature = new Signature(this.caCertificate, secureRequest.Certificate.GetSignatureContent(), DateTime.Now.AddDays(10));
            var response = new SignatureResponse(secureRequest.Certificate.Id, signature);
            var signedResponse = new Signed<SignatureResponse>(response, this.caCertificate);
            var responseFilename = Path.Combine(this.dataPath, secureRequest.Certificate.Id.ToString() + Files.SignatureResponseExtension);
            signedResponse.Save(responseFilename);
            signatureReponseFilenames.Add(responseFilename);
            File.Move(file.FullName, Path.Combine(this.donePath, file.Name));
            Console.WriteLine("Granted.");
          }
        }
        else
        {
          Console.WriteLine("Invalid.");
        }
      }

      if (signatureReponseFilenames.Count > 0)
      {
        Begin("Set signature responses ... ");
        Status.Client.SetSignatureResponses(signatureReponseFilenames, OnSimpleOperationCompleted);
        WaitForCompletion();
      }
      else
      {
        Console.WriteLine("No signature responses.");
      }

      foreach (var file in dataDirectory.GetFiles(Files.SignatureResponsePattern))
      {
        File.Move(file.FullName, Path.Combine(this.donePath, file.Name));
      }
    }

    public void OnSimpleOperationCompleted(Exception exception)
    {
      CompleteAndReady(exception);
    }

    public override IEnumerable<string> Aliases
    {
      get
      {
        yield return "autoca";
      }
    }

    public override string HelpText
    {
      get { return "Automated CA."; }
    }
  }
}
