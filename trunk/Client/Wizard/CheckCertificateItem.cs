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
    private const string ServerIpFile = "serverip.txt";

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
    private Exception exception;

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
            return new ListVotingsItem();
          }
          else if (Status.Certificate is AuthorityCertificate)
          {
            return new AuthorityListVotingsItem();
          }
          else
          {
            return new ChooseCertificateItem();
          }
        default:
          return new ChooseCertificateItem();
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
        return
          this.status == CheckStatus.CheckCertificateAccepted ||
          this.status == CheckStatus.CheckCertificateNeeded ||
          this.status == CheckStatus.CheckCertificateDeclined ||
          this.status == CheckStatus.CheckCertificateFailed;
      }
    }

    public override void Begin()
    {
      IPAddress serverIpAddress = IPAddress.Parse(File.ReadAllText(ServerIpFile).Trim());

      this.status = CheckStatus.Connect;
      Status.VotingClient.Connect(serverIpAddress, ConnectComplete);
      Status.UpdateProgress();

      while (this.status == CheckStatus.Connect)
      {
        Status.UpdateProgress();
        Application.DoEvents();
        Thread.Sleep(1);
      }

      if (this.status == CheckStatus.ConnectFailed)
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
        OnUpdateWizard();
        return;
      }

      this.status = CheckStatus.GetCertificates;
      Status.UpdateProgress();
      Status.VotingClient.GetCertificateStorage(GetCertificateStorageComplete);

      while (this.status == CheckStatus.GetCertificates)
      {
        Status.UpdateProgress();
        Application.DoEvents();
        Thread.Sleep(1);
      }

      if (this.status == CheckStatus.GetCertificatesFailed)
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
        OnUpdateWizard();
        return;
      }

      this.status = CheckStatus.CheckCertificate;
      Status.UpdateProgress();

      if (Status.Certificate.Valid(Status.CertificateStorage))
      {
        this.status = CheckStatus.CheckCertificateAccepted;
        Status.SetMessage(Resources.CheckCertificateReady, MessageType.Info);
        OnUpdateWizard();
      }
      else
      {
        Status.VotingClient.GetSignatureResponse(Status.Certificate.Id, GetSignatureResponseComplete);

        while (this.status == CheckStatus.CheckCertificate)
        {
          Status.UpdateProgress();
          Application.DoEvents();
          Thread.Sleep(1);
        }

        if (this.status == CheckStatus.CheckCertificateFailed)
        {
          Status.SetMessage(this.exception.Message, MessageType.Error);
          OnUpdateWizard();
          return;
        }

        MessageType type = MessageType.Info;
        switch (this.status)
        {
          case CheckStatus.CheckCertificateAccepted:
            type = MessageType.Success;
            break;
          case CheckStatus.CheckCertificateDeclined:
            type = MessageType.Info;
            break;
          case CheckStatus.CheckCertificateFailed:
            type = MessageType.Error;
            break;
          case CheckStatus.CheckCertificateNeeded:
            type = MessageType.Info;
            break;
        }

        Status.SetMessage(this.message, type);

        OnUpdateWizard();
      }
    }

    private void ConnectComplete(Exception exception)
    {
      this.exception = exception;

      if (exception == null)
      {
        this.status = CheckStatus.ConnectDone;
      }
      else
      {
        this.status = CheckStatus.ConnectFailed;
      }
    }

    private void GetCertificateStorageComplete(CertificateStorage certificateStorage, Exception exception)
    {
      this.exception = exception;

      if (exception == null)
      {
        Status.CertificateStorage.Add(certificateStorage);
        this.status = CheckStatus.GetCertificatesDone;
      }
      else
      {
        this.status = CheckStatus.GetCertificatesFailed;
      }
    }

    private void GetSignatureResponseComplete(SignatureResponseStatus status, Signed<SignatureResponse> response, Exception exception)
    {
      this.exception = exception;

      if (exception == null)
      {
        switch (status)
        {
          case SignatureResponseStatus.Accepted:
            if (response.Verify(Status.CertificateStorage))
            {
              Status.Certificate.AddSignature(response.Value.Signature);
              Status.Certificate.Save(Status.CertificateFileName);

              this.message = Resources.CheckCertificateResponseAccept;
              this.status = CheckStatus.CheckCertificateAccepted;
            }
            else
            {
              this.message = Resources.CheckCertificateResponseInvalid;
              this.status = CheckStatus.CheckCertificateDeclined;
            }
            break;
          case SignatureResponseStatus.Declined:
            if (response.Verify(Status.CertificateStorage))
            {
              this.message = Resources.CheckCertificateResponseDeclined + response.Value.Reason;
              this.status = CheckStatus.CheckCertificateDeclined;
            }
            else
            {
              this.message = Resources.CheckCertificateResponseInvalid;
              this.status = CheckStatus.CheckCertificateDeclined;
            }
            break;
          case SignatureResponseStatus.Pending:
            this.message = Resources.CheckCertificatePending;
            this.status = CheckStatus.CheckCertificateDeclined;
            break;
          default:
            this.message = Resources.CheckCertificateNeedRequest;
            this.status = CheckStatus.CheckCertificateNeeded;
            break;
        }
      }
      else
      {
        this.status = CheckStatus.CheckCertificateFailed;
      }
    }

    private void StartWizardItem_Load(object sender, EventArgs e)
    {
      
    }
  }
}
