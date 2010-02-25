/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Client
{
  public partial class CheckCertificateItem : WizardItem
  {
    public enum CheckStatus
    {
      Unknown,
      Connect,
      ConnectDone,
      ConnectFailed,
      GetCertificates,
      GetCertificatesDone,
      GetCertificatesFailed,
      CheckCertificate,
      CheckCertificateAccepted,
      CheckCertificateDeclined,
      CheckCertificateNeeded,
      CheckCertificateFailed
    }

    private CheckStatus status;
    private string message;

    public CheckCertificateItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      switch (this.status)
      {
        case CheckStatus.CheckCertificateNeeded:
          return new CreateRequestItem();
        case CheckStatus.CheckCertificateAccepted:
          if (Status.Certificate is AdminCertificate)
          {
            return new AdminChooseItem();
          }
          else if (Status.Certificate is VoterCertificate)
          {
            return null;
          }
          else if (Status.Certificate is AuthorityCertificate)
          {
            return null;
          }
          else
          {
            return null;
          }
        default:
          return null;
      }
    }

    public override WizardItem Previous()
    {
      return null;
    }

    public override WizardItem Cancel()
    {
      return null;
    }

    public override bool CanCancel
    {
      get
      {
        switch (this.status)
        {
          case CheckStatus.ConnectFailed:
          case CheckStatus.GetCertificatesFailed:
          case CheckStatus.CheckCertificateAccepted:
          case CheckStatus.CheckCertificateDeclined:
          case CheckStatus.CheckCertificateFailed:
          case CheckStatus.CheckCertificateNeeded:
            return true;
          default:
            return false;
        }
      }
    }

    public override bool CanNext
    {
      get
      {
        return this.status == CheckStatus.CheckCertificateAccepted ||
          this.status == CheckStatus.CheckCertificateNeeded;
      }
    }

    public override void Begin()
    {
      this.status = CheckStatus.Connect;
      this.connectBox.BackColor = Color.Yellow;
      Status.VotingClient.Connect(IPAddress.Loopback, ConnectComplete);

      while (this.status == CheckStatus.Connect)
      {
        Application.DoEvents();
        Thread.Sleep(1);
      }

      if (this.status == CheckStatus.ConnectFailed)
        return;

      this.status = CheckStatus.GetCertificates;
      this.getCertificatesBox.BackColor = Color.Yellow;
      Status.VotingClient.GetCertificateStorage(GetCertificateStorageComplete);

      while (this.status == CheckStatus.GetCertificates)
      {
        Application.DoEvents();
        Thread.Sleep(1);
      }

      if (this.status == CheckStatus.GetCertificatesFailed)
        return;

      this.status = CheckStatus.CheckCertificate;
      this.checkStatusBox.BackColor = Color.Yellow;

      if (Status.Certificate.Valid(Status.CertificateStorage))
      {
        this.status = CheckStatus.CheckCertificateAccepted;
        this.checkStatusBox.BackColor = Color.Green;
        this.messageLabel.Text = "Your certificate is valid and ready for use.";
        OnUpdateWizard();
      }
      else
      {
        Status.VotingClient.GetSignatureResponse(Status.Certificate.Id, GetSignatureResponseComplete);

        while (this.status == CheckStatus.CheckCertificate)
        {
          Application.DoEvents();
          Thread.Sleep(1);
        }

        if (this.status == CheckStatus.CheckCertificateFailed)
          return;

        this.messageLabel.Text = this.message;

        switch (this.status)
        {
          case CheckStatus.CheckCertificateAccepted:
            this.checkStatusBox.BackColor = Color.Green;
            break;
          case CheckStatus.CheckCertificateDeclined:
            this.checkStatusBox.BackColor = Color.Red;
            break;
          case CheckStatus.CheckCertificateFailed:
            this.checkStatusBox.BackColor = Color.Red;
            break;
          case CheckStatus.CheckCertificateNeeded:
            this.checkStatusBox.BackColor = Color.Green;
            break;
        }

        OnUpdateWizard();
      }
    }

    private void ConnectComplete(Exception exception)
    {
      if (exception == null)
      {
        this.connectBox.BackColor = Color.Green;
        this.status = CheckStatus.ConnectDone;
      }
      else
      {
        MessageBox.Show(exception.ToString());
        this.connectBox.BackColor = Color.Red;
        this.status = CheckStatus.ConnectFailed;
      }
    }

    private void GetCertificateStorageComplete(CertificateStorage certificateStorage, Exception exception)
    {
      if (exception == null)
      {
        Status.CertificateStorage.Add(certificateStorage);
        this.getCertificatesBox.BackColor = Color.Green;
        this.status = CheckStatus.GetCertificatesDone;
      }
      else
      {
        MessageBox.Show(exception.ToString());
        this.getCertificatesBox.BackColor = Color.Red;
        this.status = CheckStatus.GetCertificatesFailed;
      }
    }

    private void GetSignatureResponseComplete(SignatureResponseStatus status, Signed<SignatureResponse> response, Exception exception)
    {
      if (exception == null)
      {
        switch (status)
        {
          case SignatureResponseStatus.Accepted:
            if (response.Verify(Status.CertificateStorage))
            {
              Status.Certificate.AddSignature(response.Value.Signature);
              Status.Certificate.Save(Status.CertificateFileName);

              this.message = "The server sent a response to your certificate signing request and the signature was affixed to your certificate.";
              this.status = CheckStatus.CheckCertificateAccepted;
            }
            else
            {
              this.message = "The server sent a response to your certificate signing request, but it is not valid. Please context your voting administrator.";
              this.status = CheckStatus.CheckCertificateDeclined;
            }
            break;
          case SignatureResponseStatus.Declined:
            if (response.Verify(Status.CertificateStorage))
            {
              this.message = "The server sent a response to your certificate signing request, but the certificate authority declined your request for the following reason: " + response.Value.Reason;
              this.status = CheckStatus.CheckCertificateDeclined;
            }
            else
            {
              this.message = "The server sent a response to your certificate signing request, but it is not valid. Please context your voting administrator.";
              this.status = CheckStatus.CheckCertificateDeclined;
            }
            break;
          case SignatureResponseStatus.Pending:
            this.message = "Your certificate signature request ist still pending on the server.";
            this.status = CheckStatus.CheckCertificateDeclined;
            break;
          default:
            this.message = "You need to create a certificate signature request and submit it to the server.";
            this.status = CheckStatus.CheckCertificateNeeded;
            break;
        }
      }
      else
      {
        MessageBox.Show(exception.ToString());
        this.status = CheckStatus.CheckCertificateFailed;
      }
    }

    private void StartWizardItem_Load(object sender, EventArgs e)
    {
      
    }
  }
}
