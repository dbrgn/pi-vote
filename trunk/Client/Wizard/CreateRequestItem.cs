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
    public enum SendStatus
    { 
      Send,
      SendDone,
      SendFailed
    }

    private SendStatus status;

    public CreateRequestItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      return null;
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
      get { return true; }
    }

    public override void Begin()
    {
      this.idTextBox.Text = Status.Certificate.Id.ToString();

      if (Status.Certificate is VoterCertificate)
      {
        this.typeTextBox.Text = "Voter";
      }
      else if (Status.Certificate is AuthorityCertificate)
      {
        this.typeTextBox.Text = "Voting Authority";
      }
      else if (Status.Certificate is AdminCertificate)
      {
        this.typeTextBox.Text = "Administrator";
      }
      else
      {
        this.typeTextBox.Text = "Unkown";
      }
    }

    private void sendButton_Click(object sender, EventArgs e)
    {
      this.firstNameTextBox.Enabled = false;
      this.familyNameTextBox.Enabled = false;
      this.emailAddressTextBox.Enabled = false;
      this.sendButton.Enabled = false;

      SignatureRequest signatureRequest 
        = new SignatureRequest(
          this.firstNameTextBox.Text, 
          this.familyNameTextBox.Text, 
          this.emailAddressTextBox.Text);
      Signed<SignatureRequest> signedSignatureRequest = 
        new Signed<SignatureRequest>(signatureRequest, Status.Certificate);

      this.status = SendStatus.Send;

      Status.VotingClient.SetSignatureRequest(signedSignatureRequest, SetSignatureRequestComplete);

      while (this.status == SendStatus.Send)
      {
        Application.DoEvents();
        Thread.Sleep(1);
      }

      switch (this.status)
      {
        case SendStatus.SendFailed:
          this.messageLabel.Text = "Your request could not be submitted.";
          break;
        case SendStatus.SendDone:
          this.messageLabel.Text = "Your request has been submitted to the server. You must now wait for the certificat authority to process it.";
          break;
      }
    }

    private void SetSignatureRequestComplete(Exception exception)
    {
      if (exception == null)
      {
        this.status = SendStatus.SendDone;
      }
      else
      {
        MessageBox.Show(exception.ToString());
        this.status = SendStatus.SendFailed;
      }
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
