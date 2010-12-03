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
      CheckCertificate,
      CheckCertificateAccepted,
      CheckCertificateDeclined,
      CheckCertificatePending,
      CheckCertificateNeeded,
      CheckCertificateFailed
    }

    private CheckStatus status;
    private string message;
    private Exception exception;
    public WizardItem PreviousItem { get; set; }

    public CheckCertificateItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      switch (this.status)
      {
        case CheckStatus.CheckCertificateNeeded:
          return new SimpleCreateCertificateItem();
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
        case CheckStatus.CheckCertificateDeclined:
          return new ChooseCertificateItem();
        default:
          return null;
      }
    }

    public override WizardItem Previous()
    {
      return PreviousItem;
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
          case CheckStatus.CheckCertificateAccepted:
          case CheckStatus.CheckCertificateDeclined:
          case CheckStatus.CheckCertificatePending:
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
          this.status == CheckStatus.CheckCertificateNeeded;
      }
    }

    public override bool CanPrevious
    {
      get { return PreviousItem != null; }
    }

    public override bool CancelIsDone
    {
      get
      {
        switch (this.status)
        {
          case CheckStatus.CheckCertificateDeclined:
          case CheckStatus.CheckCertificatePending:
          case CheckStatus.CheckCertificateFailed:
            return true;
          default:
            return false;
        }
      }
    }

    public override void Begin()
    {
      this.status = CheckStatus.CheckCertificate;
      Status.UpdateProgress();

      CertificateValidationResult result = Status.Certificate.Validate(Status.CertificateStorage);

      switch (result)
      {
        case CertificateValidationResult.Valid:
          this.status = CheckStatus.CheckCertificateAccepted;
          Status.SetMessage(Resources.CheckCertificateReady, MessageType.Info);
          break;
        case CertificateValidationResult.Outdated:
        case CertificateValidationResult.NotYetValid:
        case CertificateValidationResult.Revoked:
        case CertificateValidationResult.SelfsignatureInvalid:
        case CertificateValidationResult.SignatureDataInvalid:
        case CertificateValidationResult.SignerInvalid:
        case CertificateValidationResult.UnknownSigner:
          string msessage = string.Empty;

          switch (result)
          {
            case CertificateValidationResult.Outdated:
              message = Resources.CheckCertificateOutdated;
              break;
            case CertificateValidationResult.NotYetValid:
              message = string.Format(Resources.CheckCertificateNotYetValid, Status.Certificate.ExpectedValidFrom(Status.CertificateStorage));
              break;
            case CertificateValidationResult.Revoked:
              message = Resources.CheckCertificateRevoked;
              break;
            case CertificateValidationResult.SelfsignatureInvalid:
              message = Resources.CheckCertificateSelfsignatureInvalid;
              break;
            case CertificateValidationResult.SignatureDataInvalid:
              message = Resources.CheckCertificateSignatureDataInvalid;
              break;
            case CertificateValidationResult.SignerInvalid:
              message = Resources.CheckCertificateSignerInvalid;
              break;
            case CertificateValidationResult.UnknownSigner:
              message = Resources.CheckCertificateUnknownSigner;
              break;
          }

          message += " " + Resources.CheckCertificateRemove;

          this.status = CheckStatus.CheckCertificateDeclined;
          Status.SetMessage(message, MessageType.Error);
          break;
        case CertificateValidationResult.NoSignature:
          Status.VotingClient.GetSignatureResponse(Status.Certificate.Id, GetSignatureResponseComplete);

          while (this.status == CheckStatus.CheckCertificate)
          {
            Status.UpdateProgress();
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
            case CheckStatus.CheckCertificatePending:
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
          break;
        default:
          throw new InvalidOperationException("Unknown certificate validation result");
      }

      if (this.status == CheckStatus.CheckCertificateDeclined)
      {
        File.Move(Status.CertificateFileName, Status.CertificateFileName + Files.BakExtension);
        Status.CertificateFileName = null;
        Status.Certificate = null;
      }

      OnUpdateWizard();
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
            this.status = CheckStatus.CheckCertificatePending;
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

    public override void UpdateLanguage()
    {
      this.pictureBox.Image = Resources.CheckCertificateItem;
    }
  }
}
