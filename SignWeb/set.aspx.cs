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
  public partial class set : System.Web.UI.Page
  {
    private bool valid;
    private string message;
    private string error; 
    private TcpRpcClient client;
    private VotingRpcProxy proxy;
    private Guid notaryId;
    private byte[] code;
    private Signed<SignCheckCookie> cookie;
    private const string CookieName = "PiVoteSignCheckCookie";

    private void ConnectPiVote()
    {
      if (this.valid)
      {
        try
        {
          this.client = new TcpRpcClient();
          this.client.Connect(new IPEndPoint(IPAddress.Loopback, 4242));
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

    private void ParseId()
    {
      if (this.valid)
      {
        try
        {
          string idString = Request.Params["id"];
          this.notaryId = new Guid(idString);
        }
        catch
        {
          this.message = "Certificate id invalid.";
          this.valid = false;
        }
      }
    }

    private void ParseCode()
    {
      if (this.valid)
      {
        string codeString = Request.Params["c"]
          .Replace(" ", string.Empty)
          .Replace("-", string.Empty)
          .ToLowerInvariant();

        if (codeString.Length % 2 == 0)
        {
          List<byte> codeBytes = new List<byte>();

          for (int i = 0; i < codeString.Length; i += 2)
          {
            codeBytes.Add((byte)Convert.ToUInt32(codeString.Substring(i, 2), 16));
          }

          this.code = codeBytes.ToArray();
        }
        else
        {
          this.message = "Cannot parse code.";
          this.valid = false;
        }
      }
    }

    private void GetCookie()
    {
      if (this.valid)
      {
        try
        {
          this.cookie = this.proxy.FetchSignCheckCookie(this.notaryId, this.code);
        }
        catch (Exception exception)
        {
          this.error = exception.Message;
          this.message = "Cannot get cookie.";
          this.valid = false;
        }
      }
    }

    private void SetCookie()
    {
      if (this.valid)
      {
        string cookieString = Convert.ToBase64String(this.cookie.ToBinary());
        HttpCookie httpCookie = new HttpCookie(CookieName, cookieString);
        httpCookie.Expires = DateTime.Now.AddYears(1);
        Response.Cookies.Set(httpCookie);
      }
    }

    private void BuildTable()
    {
      Table table = new Table();

      table.AddHeaderRow(2, "Notary's sign check cookie");

      if (this.cookie != null)
      {
        table.AddRow("You are:", this.cookie.Certificate.FullName);
        table.AddRow(string.Empty, "Sign check cookie set.");
      }
      else
      {
        table.AddRow("You are:", "Unknown");
      }

      table.AddSpaceRow(2, 32);

      if (!this.valid)
      {
        table.AddRow(string.Empty, this.message);

        if (!string.IsNullOrEmpty(this.error))
        {
          table.AddRow(string.Empty, this.error);
        }
      }

      mainPanel.Controls.Add(table);
    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
      this.valid = true;
      this.message = string.Empty;
      this.error = null;

      ParseId();
      ParseCode();
      ConnectPiVote();
      GetCookie();
      SetCookie();
      BuildTable();
    }
  }
}