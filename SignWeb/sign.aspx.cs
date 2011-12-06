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
    private Table table;
    private Guid certificateId;
    private byte[] signatureRequestKey;
    private TcpRpcClient client;
    private VotingRpcProxy proxy;
    private SignatureResponseStatus status;
    private Certificate certificate;
    private IEnumerable<Signed<SignatureRequestSignCheck>> signChecks;
    private ServerCertificate serverCertificate;
    private Signed<SignCheckCookie> cookie;
    private CertificateStorage certificateStorage;
    private const string CookieName = "PiVoteSignCheckCookie";
    private SignatureRequest request;
    private bool maySign;

    private bool ReadSignCheckCookie()
    {
      table.AddHeaderRow(2, "Notary's sign check cookie");

      try
      {
        byte[] cookieData = Convert.FromBase64String(Request.Cookies.Get(CookieName).Value);
        this.cookie = Serializable.FromBinary<Signed<SignCheckCookie>>(cookieData);
        table.AddRow("You are:", this.cookie.Certificate.FullName);
      }
      catch
      {
        table.AddRow("You are:", "Unknown");
        table.AddRow(string.Empty, "Cannot decode sign check cookie.");
        table.AddSpaceRow(2, 32);
        return false;
      }

      if (this.cookie.Verify(this.certificateStorage))
      {
        table.AddRow(string.Empty, "Your sign check cookie is valid.");
        table.AddSpaceRow(2, 32);
        return true;
      }
      else
      {
        table.AddRow(string.Empty, "Your sign check cookie is invalid.");

        if (!this.cookie.VerifySimple())
        {
          table.AddRow(string.Empty, "The signature on the sign check cookie is invalid.");
        }
        else
        {
          table.AddRow(string.Empty, this.cookie.Certificate.Validate(this.certificateStorage).Text());
        }

        table.AddSpaceRow(2, 32);
        return false;
      }
    }

    private bool ReadRequestData()
    {
      table.AddHeaderRow(2, "Signature request");

      try
      {
        string idString = Request.Params["id"];
        this.certificateId = new Guid(idString);
      }
      catch
      {
        table.AddRow(string.Empty, "Certificate id invalid.");
        table.AddSpaceRow(2, 32);
        return false;
      }

      try
      {
        if (!Request.Params.AllKeys.Contains("k"))
        {
          table.AddRow(string.Empty, "Request key missing.");
          table.AddSpaceRow(2, 32);
          return false;
        }

        string data = Request.Params["k"]
          .Replace(" ", string.Empty)
          .Replace("-", string.Empty)
          .ToLowerInvariant();
        this.signatureRequestKey = HexToBytes(data);

        if (this.signatureRequestKey.Length != 32)
        {
          table.AddRow(string.Empty, "Request key length invalid.");
          table.AddSpaceRow(2, 32);
          return false;
        }
      }
      catch
      {
        table.AddRow(string.Empty, "Cannot parse request key.");
        table.AddSpaceRow(2, 32);
        return false;
      }

      try
      {
        Signed<SignatureResponse> signedResponse = null;
        this.status = proxy.FetchSignatureResponse(this.certificateId, out signedResponse);

        switch (this.status)
        {
          case SignatureResponseStatus.Pending:
            break;
          case SignatureResponseStatus.Declined:
            table.AddRow(string.Empty, "Signature request already declined.");
            table.AddSpaceRow(2, 32);
            return false;
          case SignatureResponseStatus.Accepted:
            table.AddRow(string.Empty, "Signature request already accepted.");
            table.AddSpaceRow(2, 32);
            return false;
          case SignatureResponseStatus.Unknown:
          default:
            table.AddRow(string.Empty, "Cannot find signature request.");
            table.AddSpaceRow(2, 32);
            return false;
        }
      }
      catch
      {
        table.AddRow(string.Empty, "Cannot determine response status.");
        table.AddSpaceRow(2, 32);
        return false;
      }

      try
      {
        var request = proxy.FetchSignatureRequest(this.certificateId);
        this.certificate = request.Certificate;

        if (!this.certificate.Id.Equals(this.certificateId))
        {
          table.AddRow(string.Empty, "Certificate id does not match.");
          table.AddSpaceRow(2, 32);
          return false;
        }
      }
      catch
      {
        table.AddRow(string.Empty, "Cannot retrieve signature request.");
        table.AddSpaceRow(2, 32);
        return false;
      }

      byte[] encryptedSignatureRequestData;

      try
      {
        var result = proxy.FetchSignCheckList(this.certificateId); ;
        this.signChecks = result.First;

        if (result.Second.Length <= 32)
        {
          table.AddRow(string.Empty, "Encrypted signature request data invalid.");
          table.AddSpaceRow(2, 32);
          return false;
        }
        else
        {
          encryptedSignatureRequestData = result.Second;
        }
      }
      catch
      {
        table.AddRow(string.Empty, "Cannot download sign checks.");
        table.AddSpaceRow(2, 32);
        return false;
      } 

      try
      {
        this.request = SignatureRequest.Decrypt(encryptedSignatureRequestData, this.signatureRequestKey);
      }
      catch
      {
        table.AddRow(string.Empty, "Cannot decrypt signature request data.");
        table.AddSpaceRow(2, 32);
        return false;
      }

      return true;
    }

    private bool DisplayRequest()
    {
      if (this.request != null)
      {
        table.AddRow("Firstname:", this.request.FirstName);
        table.AddRow("Surname:", this.request.FamilyName);
        table.AddRow("Email:", this.request.EmailAddress);
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
          table.AddRow("Signature from:", signCheck.Cookie.Certificate.FullName + " at " + signCheck.SignDateTime.ToString());

          if (signCheck.Cookie.Certificate.IsIdentic(this.cookie.Certificate))
          {
            alreadySigned = true;
          }
        }

        table.AddSpaceRow(2, 32);

        return !alreadySigned;
      }
      else
      {
        return false;
      }
    }

    private byte[] HexToBytes(string hexString)
    {
      if (hexString.Length % 2 != 0)
        throw new ArgumentException("Hex string has odd length.");

      List<byte> bytes = new List<byte>();

      for (int i = 0; i < hexString.Length; i += 2)
      {
        bytes.Add(Convert.ToByte(hexString.Substring(i, 2), 16));
      }

      return bytes.ToArray();
    }

    private bool ConnectToServer()
    {
      table.AddHeaderRow(2, "Pi-Vote Server");

      string fileName = Path.Combine(Request.PhysicalApplicationPath, "server.pi-cert");

      if (File.Exists(fileName))
      {
        this.serverCertificate = Serializable.Load<ServerCertificate>(fileName);
      }
      else
      {
        table.AddRow("Connection:", "N/A"); 
        table.AddRow(string.Empty, "Server certifcate not found.");
        table.AddSpaceRow(2, 32);
        return false;
      }

      try
      {
        this.client = new TcpRpcClient();
        this.client.Connect(new IPEndPoint(IPAddress.Loopback, 4242));
        this.proxy = new VotingRpcProxy(client);
      }
      catch
      {
        table.AddRow("Connection:", "Failed"); 
        table.AddRow(string.Empty, "Pi-Vote server connection failed.");
        table.AddSpaceRow(2, 32);
        return false;
      }

      try
      {
        var result = proxy.FetchCertificateStorage();
        this.certificateStorage = new CertificateStorage();
        this.certificateStorage.TryLoadRoot(Request.PhysicalApplicationPath);
        this.certificateStorage.Add(result.First);
      }
      catch
      {
        table.AddRow("Connection:", "Failed"); 
        table.AddRow(string.Empty, "Cannot download certificate storage.");
        table.AddSpaceRow(2, 32);
        return false;
      }

      table.AddRow("Connection:", "Ok");
      table.AddSpaceRow(2, 32);
      return true;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
      this.table = new Table();
      this.maySign = false;

      if (ConnectToServer())
      {
        bool isNotary = ReadSignCheckCookie();
        bool requestValid = ReadRequestData();
        bool notAlreadySigned = DisplayRequest();

        if (isNotary)
        {
          if (requestValid)
          {
            if (notAlreadySigned)
            {
              table.AddRow(string.Empty, "Please verify the name and certificate id matches the paper form before signing.");

              this.maySign = true;
              Button signButton = new Button();
              signButton.ID = "signButton";
              signButton.Text = "Sign";
              signButton.Click += new EventHandler(signButton_Click);
              table.AddRow(null, signButton);
            }
            else
            {
              table.AddRow(string.Empty, "You have already signed.");
            }
          }
          else
          {
            table.AddRow(string.Empty, "You cannot sign because the request is not valid.");
          }
        }
        else
        {
          table.AddRow(string.Empty, "You are not authorized to sign.");
        }
      }

      mainPanel.Controls.Add(table);
    }

    private void signButton_Click(object sender, EventArgs e)
    {
      if (this.maySign)
      {
        try
        {
          var cert = new X509Certificate2(Request.ClientCertificate.Certificate);
          SignatureRequestSignCheck signCheck = new SignatureRequestSignCheck(this.certificate, this.cookie);
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