/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;

namespace Pirate.PiVote.Circle.Create
{
  public partial class EnterVoterCertificateDataControl : CreateCertificateControl
  {
    public EnterVoterCertificateDataControl()
    {
      InitializeComponent();

      this.firstNameLabel.Text = Resources.CreateCertificateDataFirstName;
      this.familyNameLabel.Text = Resources.CreateCertificateDataFamilyName;
      this.emailAddressLabel.Text = Resources.CreateCertificateDataEmailAddress;
      this.emailNotificationCheckBox.Text = Resources.CreateCertificateDataNotify;
      this.nextButton.Text = GuiResources.ButtonNext;
      this.cancelButton.Text = GuiResources.ButtonCancel;

      CheckValid();
    }

    private void firstNameTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckValid();
    }

    private void familyNameTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckValid();
    }

    private void emailAddressTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckValid();
    }

    private void nextButton_Click(object sender, EventArgs e)
    {
      string fullName = string.Format("{0} {1}",
        this.firstNameTextBox.Text,
        this.familyNameTextBox.Text);

      var encryptResult = EncryptPrivateKeyDialog.ShowSetPassphrase();

      if (encryptResult.First == DialogResult.OK)
      {
        string passphrase = encryptResult.Second;

        Status.Certificate = new VoterCertificate(Resources.Culture.ToLanguage(), passphrase, 0);

        Status.Certificate.CreateSelfSignature();

        Status.Controller.AddAndSaveCertificate(Status.Certificate); 
        Status.SignatureRequest = new SignatureRequest(this.firstNameTextBox.Text, this.familyNameTextBox.Text, this.emailAddressTextBox.Text);
        Status.SignatureRequestInfo = new SignatureRequestInfo(this.emailAddressTextBox.Text);

        Status.SignatureRequestFileName = Path.Combine(Status.Controller.Status.DataPath, Status.Certificate.Id.ToString() + Files.SignatureRequestDataExtension);
        Status.SignatureRequest.Save(Status.SignatureRequestFileName);

        Status.SignatureRequestInfoFileName = Path.Combine(Status.Controller.Status.DataPath, Status.Certificate.Id.ToString() + Files.SignatureRequestInfoExtension);
        Status.SignatureRequestInfo.Save(Status.SignatureRequestInfoFileName);

        var nextControl = new PrintAndUploadCertificateControl();
        nextControl.Status = Status;
        OnShowNextControl(nextControl);
      }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      OnCloseCreateDialog();
    }

    private void CheckValid()
    {
      this.nextButton.Enabled =
        !this.firstNameTextBox.Text.IsNullOrEmpty() &&
        !this.familyNameTextBox.Text.IsNullOrEmpty() &&
        Mailer.IsEmailAddressValid(this.emailAddressTextBox.Text);
    }

    private void EnterVoterCertificateDataControl_Load(object sender, EventArgs e)
    {
      this.nextButton.Enabled = false;
    }

    private void groupComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckValid();
    }
  }
}
