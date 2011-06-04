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
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Serialization;

namespace SignWeb
{
  public partial class sign : System.Web.UI.Page
  {
    private bool valid;
    private string message;
    private string error; 
    private Guid certificateId;
    private string fingerprint;
    private TcpRpcClient client;
    private VotingRpcProxy proxy;
    private SignatureResponseStatus status;
    private Certificate certificate;
    private IEnumerable<Signed<SignatureRequestSignCheck>> signChecks;
    private ServerCertificate serverCertificate;
    private bool authorized;

    private void ParseId()
    {
      if (this.valid)
      {
        try
        {
          string idString = Request.Params["id"];
          this.certificateId = new Guid(idString);
        }
        catch
        {
          this.message = "Certificate id invalid.";
          this.valid = false;
        }
      }
    }

    private void ParseFingerprint()
    {
      if (this.valid)
      {
        this.fingerprint = Request.Params["fp"]
          .Replace(" ", string.Empty)
          .Replace("-", string.Empty)
          .ToLowerInvariant();
      }
    }

    private void ConnectPiVote()
    {
      if (this.valid)
      {
        try
        {
          this.client = new TcpRpcClient();
          this.client.Connect(new IPEndPoint(IPAddress.Parse("78.46.176.243"), 4243));
          this.proxy = new VotingRpcProxy(client);
        }
        catch (Exception exception)
        {
          this.error = exception.Message;
          this.message = "Pi-Vote server connection failed.";
          this.valid = false;
        }
      }
    }

    private void CheckRequestStatus()
    {
      if (this.valid)
      {
        try
        {
          Signed<SignatureResponse> signedResponse = null;
          this.status = proxy.FetchSignatureResponse(this.certificateId, out signedResponse);

          switch (this.status)
          {
            case SignatureResponseStatus.Unknown:
              this.valid = false;
              this.message = "Cannot find signature request.";
              break;
            case SignatureResponseStatus.Declined:
              this.message = "Signature request already declined.";
              this.valid = false;
              break;
            case SignatureResponseStatus.Accepted:
              this.message = "Signature request already accepted.";
              this.valid = false;
              break;
          }
        }
        catch (Exception exception)
        {
          this.error = exception.Message;
          this.message = "Cannot determine response status.";
          this.valid = false;
        }
      }
    }

    private void CheckRequestFingerprint()
    {
      if (this.valid)
      {
        try
        {
          var request = proxy.FetchSignatureRequest(this.certificateId);
          this.certificate = request.Certificate;

          if (!this.certificate.Id.Equals(this.certificateId))
          {
            this.message = "Certificate id does not match.";
            this.valid = false;
          }
          else if (this.certificate.Fingerprint.ToLowerInvariant().Replace(" ", string.Empty) != this.fingerprint)
          {
            this.message = "Fingerprint does not match.";
            this.valid = false;
          }
        }
        catch (Exception exception)
        {
          this.error = exception.Message;
          this.message = "Cannot find signature request.";
          this.valid = false;
        }
      }
    }

    private void CheckSignChecks()
    {
      if (this.valid)
      {
        try
        {
          this.signChecks = proxy.FetchSignCheckList(this.certificateId);
        }
        catch (Exception exception)
        {
          this.error = exception.Message;
          this.message = "Cannot get sign checks.";
          this.valid = false;
        }
      }
    }

    private void CheckServerCertificate()
    {
      if (this.valid)
      {
        string fileName = Path.Combine(Request.PhysicalApplicationPath, "server.pi-cert");

        if (File.Exists(fileName))
        {
          this.serverCertificate = Serializable.Load<ServerCertificate>(fileName);
        }
        else
        {
          this.message = "Server certifcate not found.";
          this.valid = false;
        }
      }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.authorized = false;
      this.valid = true;
      this.message = string.Empty;
      this.error = null;

      CheckServerCertificate();
      ParseId();
      ParseFingerprint();
      ConnectPiVote();
      CheckRequestStatus();
      CheckRequestFingerprint();
      CheckSignChecks();
      BuildTable();
    }

    private void BuildTable()
    {
      Table table = new Table();

      table.AddHeaderRow(2, "Authority's SSL Certificate");
      X509Certificate2 sslCertificate = null;

      if (Request.ClientCertificate.IsPresent)
      {
        sslCertificate = new X509Certificate2(Request.ClientCertificate.Certificate);
        string name = ConvertString(ExtractCnFromDn(sslCertificate.Subject), Encoding.Unicode, Encoding.GetEncoding(850));
        table.AddRow("You are:", name);

        if (Request.ClientCertificate.IsValid)
        {
          if (Request.ClientCertificate.ValidFrom <= DateTime.Now &&
              Request.ClientCertificate.ValidUntil >= DateTime.Now)
          {
            this.authorized = true;
            table.AddRow(string.Empty, "Your certificate is valid.");
          }
          else
          {
            table.AddRow(string.Empty, "Your certificate is outdated.");
          }
        }
        else
        {
          table.AddRow(string.Empty, "Your certificate is invalid.");
        }
      }
      else
      {
        table.AddRow("You are:", "Unknown");
      }

      table.AddSpaceRow(2, 32);

      if (valid)
      {
        table.AddHeaderRow(2, "Pi-Vote Certificate");

        table.AddRow("Certificate Id:", this.certificate.Id.ToString());
        table.AddRow("Certificate Fingerprint:", this.certificate.Fingerprint);
        table.AddRow("Status:", this.status.ToString());
        table.AddRow(string.Empty, string.Empty);

        table.AddSpaceRow(2, 32);

        table.AddHeaderRow(2, "Signatures already present");
        bool alreadySigned = false;

        foreach (var signedSignCheck in this.signChecks)
        {
          var signCheck = signedSignCheck.Value;
          string name = ConvertString(ExtractCnFromDn(signCheck.Signer.Subject), Encoding.Unicode, Encoding.GetEncoding(850));
          table.AddRow("Signature from:", name + " at " + signCheck.SignDateTime.ToString());

          if (signCheck.Signer.Subject == sslCertificate.Subject)
          {
            alreadySigned = true;
          }
        }

        table.AddSpaceRow(2, 32);
        table.AddHeaderRow(2, "Sign this certifcate");

        if (this.authorized)
        {
          if (alreadySigned)
          {
            table.AddRow(string.Empty, "You havy already signed.");
          }
          else
          {
            Button signButton = new Button();
            signButton.ID = "signButton";
            signButton.Text = "Sign";
            signButton.Click += new EventHandler(signButton_Click);
            table.AddRow(null, signButton);
          }
        }
        else
        {
          table.AddRow(string.Empty, "Your are not authorized to sign.");
        }
      }
      else
      {
        table.AddRow(string.Empty, this.message);

        if (!string.IsNullOrEmpty(this.error))
        {
          table.AddRow(string.Empty, this.error);
        }
      }

      mainPanel.Controls.Add(table);
    }

    private string ConvertString(string input, Encoding from, Encoding to)
    {
      return to.GetString(from.GetBytes(input));
    }

    private string ExtractCnFromDn(string dn)
    {
      string[] names = dn.Split(new string[] { ", " }, StringSplitOptions.None);

      foreach (var name in names)
      {
        if (name.StartsWith("CN="))
        {
          return name.Substring(3);
        }
      }

      return dn;
    }

    private void signButton_Click(object sender, EventArgs e)
    {
      if (this.valid &&
          this.authorized)
      {
        try
        {
          var cert = new X509Certificate2(Request.ClientCertificate.Certificate);
          SignatureRequestSignCheck signCheck = new SignatureRequestSignCheck(cert, this.certificate);
          Signed<SignatureRequestSignCheck> signedSignCheck = new Signed<SignatureRequestSignCheck>(signCheck, this.serverCertificate);

          this.proxy.PushSignCheck(signedSignCheck);
        }
        catch
        {
        }

        Response.Redirect(Request.Url.AbsoluteUri, true);
      }
    }
  }
}