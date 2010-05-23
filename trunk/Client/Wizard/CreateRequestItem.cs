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
  public partial class CreateRequestItem : WizardItem
  {
    private bool run;
    private Exception exception;

    public CreateRequestItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      return new ChooseCertificateItem();
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
      get { return !this.run; }
    }

    public override bool CanNext
    {
      get { return !this.run; }
    }

    public override void Begin()
    {
      this.idTextBox.Text = Status.Certificate.Id.ToString();
      this.typeTextBox.Text = Status.Certificate.TypeText;

      if (!Status.FirstName.IsNullOrEmpty())
      {
        this.firstNameTextBox.Text = Status.FirstName;
        this.firstNameTextBox.ReadOnly = true;
      }

      if (!Status.FamilyName.IsNullOrEmpty())
      {
        this.familyNameTextBox.Text = Status.FamilyName;
        this.familyNameTextBox.ReadOnly = true;
      }
    }

    private void SetEnable(bool enable)
    {
      this.firstNameTextBox.Enabled = enable;
      this.familyNameTextBox.Enabled = enable;
      this.emailAddressTextBox.Enabled = enable;
      this.sendButton.Enabled = enable;
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.idLabel.Text = Resources.CreateCertificateId;
      this.typeLabel.Text = Resources.CreateCertificateType;
      this.firstNameLabel.Text = Resources.CreateCertificateFirstname;
      this.familyNameLabel.Text = Resources.CreateCertificateSurname;
      this.emailAddressLabel.Text = Resources.CreateCertificateEmailAddress;

      this.sendButton.Text = Resources.CreateCertificatePrintAndSend;
    }

    private void sendButton_Click(object sender, EventArgs e)
    {
      SetEnable(false);
      this.run = true;
      OnUpdateWizard();

      SignatureRequest signatureRequest 
        = new SignatureRequest(
          this.firstNameTextBox.Text, 
          this.familyNameTextBox.Text, 
          this.emailAddressTextBox.Text);

      SignatureRequestDocument document = new SignatureRequestDocument(signatureRequest, Status.Certificate);
      PrintDialog printDialog = new PrintDialog();
      printDialog.Document = document;

      if (printDialog.ShowDialog() == DialogResult.OK)
      {
        document.Print();

        Signed<SignatureRequest> signedSignatureRequest =
          new Signed<SignatureRequest>(signatureRequest, Status.Certificate);

        Status.VotingClient.SetSignatureRequest(signedSignatureRequest, SetSignatureRequestComplete);

        while (this.run)
        {
          Status.UpdateProgress();
          Thread.Sleep(10);
        }

        Status.UpdateProgress();

        if (this.exception == null)
        {
          Status.SetMessage(Resources.CreateCertificateDone, MessageType.Success);
        }
        else
        {
          Status.SetMessage(this.exception.Message, MessageType.Error);
        }
      }
      else
      {
        SetEnable(true);
        this.run = false;
      }

      OnUpdateWizard();
    }

    private void SetSignatureRequestComplete(Exception exception)
    {
      this.run = false;
      this.exception = exception;
    }

    private void CheckValid()
    {
      this.sendButton.Enabled =
        !this.firstNameTextBox.Text.IsNullOrEmpty() &&
        !this.familyNameTextBox.Text.IsNullOrEmpty() &&
        Mailer.IsEmailAddressValid(this.emailAddressTextBox.Text);
    }

    private void firstNameTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckValid();
    }

    private void familyNameTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckValid();
    }

    private void functionTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckValid();
    }
  }
}
