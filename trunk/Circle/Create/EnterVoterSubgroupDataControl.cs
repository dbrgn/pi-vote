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
  public partial class EnterVoterSubgroupDataControl : CreateCertificateControl
  {
    private List<VoterCertificate> baseCertificates;

    public EnterVoterSubgroupDataControl()
    {
      InitializeComponent();

      this.baseCertificateLabel.Text = Resources.CreateCertificateDataBaseCertificate;
      this.baseValidUntilLabel.Text = Resources.CreateCertificateDataValidUntil;
      this.firstNameLabel.Text = Resources.CreateCertificateDataFirstName;
      this.familyNameLabel.Text = Resources.CreateCertificateDataFamilyName;
      this.emailAddressLabel.Text = Resources.CreateCertificateDataEmailAddress;
      this.emailNotificationCheckBox.Text = Resources.CreateCertificateDataNotify;
      this.groupLabel.Text = Resources.CreateCertificateDataGroup;
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

      VoterCertificate baseCertificate = this.baseCertificates[this.baseCertificateComboBox.SelectedIndex];

      var encryptResult = EncryptPrivateKeyDialog.ShowSetPassphrase();

      if (encryptResult.First == DialogResult.OK)
      {
        string passphrase = encryptResult.Second;

        Status.Certificate = new VoterCertificate(Resources.Culture.ToLanguage(), passphrase, this.groupComboBox.Value.Id);

        Status.Certificate.CreateSelfSignature();

        if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(baseCertificate, GuiResources.UnlockActionSignRequest))
        {
          try
          {
            Status.Controller.AddAndSaveCertificate(Status.Certificate);
            Status.SignatureRequest =
              new SignatureRequest2(
                this.firstNameTextBox.Text,
                this.familyNameTextBox.Text,
                this.emailAddressTextBox.Text,
                baseCertificate);
            Status.SignatureRequestInfo = new SignatureRequestInfo(this.emailAddressTextBox.Text);

            Status.SignatureRequestFileName = Path.Combine(Status.Controller.Status.DataPath, Status.Certificate.Id.ToString() + Files.SignatureRequestDataExtension);
            Status.SignatureRequest.Save(Status.SignatureRequestFileName);

            Status.SignatureRequestInfoFileName = Path.Combine(Status.Controller.Status.DataPath, Status.Certificate.Id.ToString() + Files.SignatureRequestInfoExtension);
            Status.SignatureRequestInfo.Save(Status.SignatureRequestInfoFileName);
          }
          finally
          {
            baseCertificate.Lock();
          }

          var nextControl = new PrintAndUploadCertificateControl();
          nextControl.Status = Status;
          OnShowNextControl(nextControl);
        }
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
        Mailer.IsEmailAddressValid(this.emailAddressTextBox.Text) &&
        this.baseCertificateComboBox.SelectedIndex >= 0 &&
        this.groupComboBox.Value != null &&
        this.groupComboBox.Value.Id != this.baseCertificates[this.baseCertificateComboBox.SelectedIndex].GroupId;
    }

    private void EnterVoterCertificateDataControl_Load(object sender, EventArgs e)
    {
      this.nextButton.Enabled = false;

      this.groupComboBox.Clear();
      this.groupComboBox.Add(Status.Controller.Status.Groups);

      if (this.groupComboBox.Items.Count > 0)
      {
        this.groupComboBox.SelectedIndex = 0;
      }
    }

    private void groupComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckValid();
    }

    public override void Prepare()
    {
      var certificates = Status.Controller.GetValidVoterCertificates();
      this.baseCertificates = new List<VoterCertificate>(
        certificates.Where(certificate => certificate.GroupId == 0));
      this.baseCertificates.ForEach(certificate => this.baseCertificateComboBox.Items.Add(certificate.Id.ToString()));

      if (this.baseCertificateComboBox.Items.Count > 0)
      {
        this.baseCertificateComboBox.SelectedIndex = 0;
      }
    }

    private void baseCertificateComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.baseValidUnitlTextBox.Text = 
        this.baseCertificates[this.baseCertificateComboBox.SelectedIndex]
        .ExpectedValidUntil(Status.Controller.Status.CertificateStorage, DateTime.Now).ToShortDateString();
      CheckValid();
    }
  }
}
