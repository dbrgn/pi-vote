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
    }

    private void sendButton_Click(object sender, EventArgs e)
    {
      this.firstNameTextBox.Enabled = false;
      this.familyNameTextBox.Enabled = false;
      this.emailAddressTextBox.Enabled = false;
      this.sendButton.Enabled = false;

      this.run = true;
      OnUpdateWizard();

      SignatureRequest signatureRequest 
        = new SignatureRequest(
          this.firstNameTextBox.Text, 
          this.familyNameTextBox.Text, 
          this.emailAddressTextBox.Text);
      Signed<SignatureRequest> signedSignatureRequest = 
        new Signed<SignatureRequest>(signatureRequest, Status.Certificate);

      Status.VotingClient.SetSignatureRequest(signedSignatureRequest, SetSignatureRequestComplete);

      while (this.run)
      {
        Status.UpdateProgress();
        Application.DoEvents();
        Thread.Sleep(1);
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

      OnUpdateWizard();
    }

    private void SetSignatureRequestComplete(Exception exception)
    {
      this.run = false;
      this.exception = exception;
    }

    private void firstNameTextBox_TextChanged(object sender, EventArgs e)
    {
      this.sendButton.Enabled =
        !this.firstNameTextBox.Text.IsNullOrEmpty() &&
        !this.familyNameTextBox.Text.IsNullOrEmpty() &&
        !this.emailAddressTextBox.Text.IsNullOrEmpty();
    }

    private void familyNameTextBox_TextChanged(object sender, EventArgs e)
    {
      this.sendButton.Enabled =
        !this.firstNameTextBox.Text.IsNullOrEmpty() &&
        !this.familyNameTextBox.Text.IsNullOrEmpty() &&
        !this.emailAddressTextBox.Text.IsNullOrEmpty();
    }

    private void functionTextBox_TextChanged(object sender, EventArgs e)
    {
      this.sendButton.Enabled =
        !this.firstNameTextBox.Text.IsNullOrEmpty() &&
        !this.familyNameTextBox.Text.IsNullOrEmpty() &&
        !this.emailAddressTextBox.Text.IsNullOrEmpty();
    }
  }
}
