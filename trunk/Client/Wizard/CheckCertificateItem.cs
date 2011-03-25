/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
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
    public enum ContinueTo
    {
      Undetermined,
      NoWhere,
      NextScreen,
      ChooseCertificate,
      CreateCertificate,
    }

    private bool run;
    private ContinueTo continueTo;
    private Signed<SignatureResponse> response;
    private SignatureResponseStatus responseStatus;
    private string message;
    private Exception exception;
    public WizardItem PreviousItem { get; set; }

    public CheckCertificateItem()
    {
      InitializeComponent();

      this.continueTo = ContinueTo.Undetermined;
    }

    public override WizardItem Next()
    {
      switch (this.continueTo)
      {
        case ContinueTo.NextScreen:
          if (Status.Certificate is AuthorityCertificate)
          {
            return new AuthorityListVotingsItem();
          }
          else if (Status.Certificate is AdminCertificate)
          {
            return new AdminChooseItem();
          }
          else if (Status.Certificate is VoterCertificate)
          {
            return new ListVotingsItem();
          }
          else
          {
            return null;
          }
        case ContinueTo.ChooseCertificate:
          return new ChooseCertificateItem();
        case ContinueTo.CreateCertificate:
          return new SimpleCreateCertificateItem();
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
        switch (this.continueTo)
        {
          case ContinueTo.Undetermined:
            return false;
          default:
            return true;
        }
      }
    }

    public override bool CanNext
    {
      get
      {
        switch (this.continueTo)
        {
          case ContinueTo.Undetermined:
          case ContinueTo.NoWhere:
            return false;
          default:
            return true;
        }
      }
    }

    public override bool CanPrevious
    {
      get
      {
        switch (this.continueTo)
        {
          case ContinueTo.Undetermined:
            return false;
          default:
            return PreviousItem != null;
        }
      }
    }

    public override bool CancelIsDone
    {
      get { return this.continueTo == ContinueTo.NoWhere; }
    }

    public override void Begin()
    {
      Status.UpdateProgress();

      if (Status.Certificate is VoterCertificate)
      {
        CheckValidityOfVoter();
      }
      else
      {
        CheckValidityOfOther();
      }

      OnUpdateWizard();
    }

    private void HandleSignatureResponseUpdate()
    {
      this.run = true;
      Status.VotingClient.GetSignatureResponse(Status.Certificate.Id, GetSignatureResponseComplete);

      while (this.run)
      {
        Status.UpdateProgress();
        Thread.Sleep(1);
      }

      if (this.exception == null)
      {
        switch (this.responseStatus)
        {
          case SignatureResponseStatus.Accepted:
            if (this.response.Verify(Status.CertificateStorage) &&
                this.response.Value.Status == this.responseStatus)
            {
              Status.Certificate.AddSignature(response.Value.Signature);
              Status.Certificate.Save(Status.CertificateFileName);
              Status.SetMessage(Resources.CheckCertificateResponseAccept, MessageType.Success);
              this.continueTo = ContinueTo.NextScreen;
            }
            else
            {
              Status.SetMessage(Resources.CheckCertificateResponseInvalid, MessageType.Error);
              this.continueTo = ContinueTo.NoWhere;
            }
            break;
          case SignatureResponseStatus.Declined:
            if (this.response.Verify(Status.CertificateStorage) &&
                this.response.Value.Status == this.responseStatus)
            {
              Status.SetMessage(Resources.CheckCertificateResponseDeclined + this.response.Value.Reason, MessageType.Info);
              RemoveCertificate();
              this.continueTo = ContinueTo.NoWhere;
            }
            else
            {
              Status.SetMessage(Resources.CheckCertificateResponseInvalid, MessageType.Error);
              this.continueTo = ContinueTo.NoWhere;
            }
            break;
          case SignatureResponseStatus.Pending:
            Status.SetMessage(Resources.CheckCertificatePending, MessageType.Info);
            this.continueTo = ContinueTo.NoWhere;
            break;
          default:
            Status.SetMessage(Resources.CheckCertificateNeedRequest, MessageType.Info);
            this.continueTo = ContinueTo.CreateCertificate;
            break;
        }
      }
      else
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
        this.continueTo = ContinueTo.NoWhere;
      }

      OnUpdateWizard();
    }

    private void GetSignatureResponseComplete(SignatureResponseStatus status, Signed<SignatureResponse> response, Exception exception)
    {
      this.exception = exception;
      this.response = response;
      this.responseStatus = status;
    }

    private void CheckValidityOfOther()
    {
      CertificateValidationResult result = Status.Certificate.Validate(Status.CertificateStorage);

      switch (result)
      {
        case CertificateValidationResult.Valid:
          Status.SetMessage(Resources.CheckCertificateReady, MessageType.Info);
          this.continueTo = ContinueTo.NextScreen;
          break;
        case CertificateValidationResult.Outdated:
          Status.SetMessage(Resources.CheckCertificateOutdated, MessageType.Error);
          this.continueTo = ContinueTo.NextScreen;
          break;
        case CertificateValidationResult.NotYetValid:
          Status.SetMessage(string.Format(Resources.CheckCertificateNotYetValid, Status.Certificate.ExpectedValidFrom(Status.CertificateStorage)), MessageType.Error);
          this.continueTo = ContinueTo.NextScreen;
          break;
        case CertificateValidationResult.Revoked:
          Status.SetMessage(Resources.CheckCertificateRevoked, MessageType.Error);
          this.continueTo = ContinueTo.NextScreen;
          break;
        case CertificateValidationResult.CrlMissing:
          Status.SetMessage(Resources.CheckCertificateCrlMissing, MessageType.Error);
          this.continueTo = ContinueTo.NextScreen;
          break;
        case CertificateValidationResult.SignerInvalid:
          Status.SetMessage(Resources.CheckCertificateSignerInvalid, MessageType.Error);
          this.continueTo = ContinueTo.NextScreen;
          break;
        case CertificateValidationResult.UnknownSigner:
          Status.SetMessage(Resources.CheckCertificateUnknownSigner, MessageType.Error);
          this.continueTo = ContinueTo.NextScreen;
          break;
        case CertificateValidationResult.SelfsignatureInvalid:
          Status.SetMessage(Resources.CheckCertificateSelfsignatureInvalid + " " + Resources.CheckCertificateRemove, MessageType.Error);
          RemoveCertificate();
          this.continueTo = ContinueTo.NoWhere;
          break;
        case CertificateValidationResult.SignatureDataInvalid:
          Status.SetMessage(Resources.CheckCertificateSignatureDataInvalid + " " + Resources.CheckCertificateRemove, MessageType.Error);
          RemoveCertificate();
          this.continueTo = ContinueTo.NoWhere;
          break;
        case CertificateValidationResult.NoSignature:
          HandleSignatureResponseUpdate();
          break;
        default:
          throw new InvalidOperationException("Unknown certificate validation result");
      }
    }

    private void CheckValidityOfVoter()
    {
      CertificateValidationResult result = Status.Certificate.Validate(Status.CertificateStorage);

      switch (result)
      {
        case CertificateValidationResult.Valid:
          Status.SetMessage(Resources.CheckCertificateReady, MessageType.Info);
          this.continueTo = ContinueTo.NextScreen;
          break;
        case CertificateValidationResult.Outdated:
          Status.SetMessage(Resources.CheckCertificateOutdated + " " + Resources.CheckCertificateRemove, MessageType.Error);
          RemoveCertificate();
          this.continueTo = ContinueTo.CreateCertificate;
          break;
        case CertificateValidationResult.NotYetValid:
          Status.SetMessage(string.Format(Resources.CheckCertificateNotYetValid, Status.Certificate.ExpectedValidFrom(Status.CertificateStorage)), MessageType.Error);
          this.continueTo = ContinueTo.NoWhere;
          break;
        case CertificateValidationResult.Revoked:
          Status.SetMessage(Resources.CheckCertificateRevoked, MessageType.Error);
          RemoveCertificate();
          this.continueTo = ContinueTo.CreateCertificate;
          break;
        case CertificateValidationResult.CrlMissing:
          Status.SetMessage(Resources.CheckCertificateCrlMissing, MessageType.Error);
          this.continueTo = ContinueTo.NoWhere;
          break;
        case CertificateValidationResult.SignerInvalid:
          Status.SetMessage(Resources.CheckCertificateSignerInvalid, MessageType.Error);
          this.continueTo = ContinueTo.NoWhere;
          break;
        case CertificateValidationResult.UnknownSigner:
          Status.SetMessage(Resources.CheckCertificateUnknownSigner, MessageType.Error);
          this.continueTo = ContinueTo.NoWhere;
          break;
        case CertificateValidationResult.SelfsignatureInvalid:
          Status.SetMessage(Resources.CheckCertificateSelfsignatureInvalid + " " + Resources.CheckCertificateRemove, MessageType.Error);
          RemoveCertificate();
          this.continueTo = ContinueTo.NoWhere;
          break;
        case CertificateValidationResult.SignatureDataInvalid:
          Status.SetMessage(Resources.CheckCertificateSignatureDataInvalid + " " + Resources.CheckCertificateRemove, MessageType.Error);
          RemoveCertificate();
          this.continueTo = ContinueTo.NoWhere;
          break;
        case CertificateValidationResult.NoSignature:
          HandleSignatureResponseUpdate();
          break;
        default:
          throw new InvalidOperationException("Unknown certificate validation result");
      }
    }

    private void RemoveCertificate()
    {
      File.Move(Status.CertificateFileName, Status.CertificateFileName + Files.BakExtension);
      Status.CertificateFileName = null;
      Status.Certificate = null;
    }

    public override void UpdateLanguage()
    {
      this.pictureBox.Image = Resources.CheckCertificateItem;
    }
  }
}
