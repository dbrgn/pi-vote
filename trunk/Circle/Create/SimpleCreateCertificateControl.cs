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
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;
using Pirate.PiVote.Printing;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Circle
{
  public partial class SimpleCreateCertificateControl : UserControl
  {
    private SignatureRequest signatureRequest;
    private SignatureRequestInfo signatureRequestInfo;
    private Secure<SignatureRequest> secureSignatureRequest;
    private Secure<SignatureRequestInfo> secureSignatureRequestInfo;
    private Certificate certificate;
    private string certificateFileName;
    private CircleController controller;

    public event EventHandler ReturnFromControl;

    public SimpleCreateCertificateControl()
    {
      InitializeComponent();
    }

    private void OnReturnFromControl()
    {
      if (ReturnFromControl != null)
      {
        ReturnFromControl(this, new EventArgs());
      }
    }

    private void StartWizardItem_Load(object sender, EventArgs e)
    {
    }

    public void Set(CircleController controller, VoterCertificate certificate, int groupId)
    {
      this.controller = controller;
      this.certificate = certificate;
      UpdateLanguage();

      this.typeComboBox.SelectedIndex = 0;

      if (this.certificate == null)
      {
        SetEnable(true);
        this.printButton.Enabled = false;
        this.doneButton.Enabled = false;
        this.cancelButton.Enabled = true;
        this.uploadButton.Enabled = false;
        this.groupComboBox.Value = this.controller.Status.Groups
          .Where(group => group.Id == groupId).Single();
      }
      else if (this.certificate.Validate(this.controller.Status.CertificateStorage) == CertificateValidationResult.Valid)
      {
        SetEnable(false);
        this.doneButton.Enabled = true;
        this.cancelButton.Enabled = false;
        this.printButton.Enabled = false;
        this.uploadButton.Enabled = false;
        this.groupComboBox.Value = this.controller.Status.Groups
          .Where(group => group.Id == groupId).Single();
      }
      else
      {
        string signatureRequestDataFileName = Path.Combine(this.controller.Status.DataPath, this.certificate.Id.ToString() + Files.SignatureRequestDataExtension);

        if (File.Exists(signatureRequestDataFileName))
        {
          if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(this.certificate, Resources.SignRequestUnlockAction))
          {
            this.signatureRequest = Serializable.Load<SignatureRequest>(signatureRequestDataFileName);
            this.signatureRequestInfo = new SignatureRequestInfo(this.signatureRequest.EmailAddress);
            this.secureSignatureRequest = new Secure<SignatureRequest>(this.signatureRequest, this.controller.Status.CaCertificate, this.certificate);
            this.secureSignatureRequestInfo = new Secure<SignatureRequestInfo>(this.signatureRequestInfo, this.controller.Status.ServerCertificate, this.certificate);

            if (this.certificate is VoterCertificate)
            {
              this.typeComboBox.SelectedIndex = 0;
            }
            else if (this.certificate is AuthorityCertificate)
            {
              this.typeComboBox.SelectedIndex = 1;
            }
            else if (this.certificate is AdminCertificate)
            {
              this.typeComboBox.SelectedIndex = 2;
            }

            this.firstNameTextBox.Text = this.signatureRequest.FirstName;
            this.familyNameTextBox.Text = this.signatureRequest.FamilyName;
            this.emailAddressTextBox.Text = this.signatureRequest.EmailAddress;

            if (this.certificate is VoterCertificate)
            {
              this.groupComboBox.Value = this.controller.Status.Groups.Where(group => group.Id == ((VoterCertificate)this.certificate).GroupId).Single();
            }

            SetEnable(false);
            this.printButton.Enabled = true;
            this.uploadButton.Enabled = true;
            this.doneButton.Enabled = false;
            this.cancelButton.Enabled = true;
          }
          else
          {
            this.certificateFileName = null;
            this.certificate = null;

            SetEnable(true);
            this.printButton.Enabled = false;
            this.uploadButton.Enabled = false;
            this.doneButton.Enabled = false;
            this.cancelButton.Enabled = true;
          }
        }
        else
        {
          File.Move(this.certificateFileName, this.certificateFileName + Files.BakExtension);
          this.certificateFileName = null;
          this.certificate = null;

          SetEnable(true);
          this.printButton.Enabled = false;
          this.uploadButton.Enabled = false;
          this.doneButton.Enabled = false;
          this.cancelButton.Enabled = true;
        }
      }
    }

    public void Set(CircleController controller, AuthorityCertificate certificate)
    {
      this.controller = controller;
      this.certificate = certificate;
      UpdateLanguage();

      this.typeComboBox.SelectedIndex = 1;

      if (this.certificate == null)
      {
        SetEnable(true);
        this.printButton.Enabled = false;
        this.uploadButton.Enabled = false;
        this.doneButton.Enabled = false;
        this.cancelButton.Enabled = true;
      }
      else if (this.certificate.Validate(this.controller.Status.CertificateStorage) == CertificateValidationResult.Valid)
      {
        SetEnable(false);
        this.printButton.Enabled = false;
        this.uploadButton.Enabled = false;
        this.doneButton.Enabled = true;
        this.cancelButton.Enabled = false;
      }
      else
      {
        string signatureRequestDataFileName = Path.Combine(this.controller.Status.DataPath, this.certificate.Id.ToString() + Files.SignatureRequestDataExtension);

        if (File.Exists(signatureRequestDataFileName))
        {
          if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(this.certificate, Resources.SignRequestUnlockAction))
          {
            this.signatureRequest = Serializable.Load<SignatureRequest>(signatureRequestDataFileName);
            this.signatureRequestInfo = new SignatureRequestInfo(this.signatureRequest.EmailAddress);
            this.secureSignatureRequest = new Secure<SignatureRequest>(this.signatureRequest, this.controller.Status.CaCertificate, this.certificate);
            this.secureSignatureRequestInfo = new Secure<SignatureRequestInfo>(this.signatureRequestInfo, this.controller.Status.ServerCertificate, this.certificate);

            if (this.certificate is VoterCertificate)
            {
              this.typeComboBox.SelectedIndex = 0;
            }
            else if (this.certificate is AuthorityCertificate)
            {
              this.typeComboBox.SelectedIndex = 1;
            }
            else if (this.certificate is AdminCertificate)
            {
              this.typeComboBox.SelectedIndex = 2;
            }

            this.firstNameTextBox.Text = this.signatureRequest.FirstName;
            this.familyNameTextBox.Text = this.signatureRequest.FamilyName;
            this.emailAddressTextBox.Text = this.signatureRequest.EmailAddress;

            if (this.certificate is VoterCertificate)
            {
              this.groupComboBox.Value = this.controller.Status.Groups.Where(group => group.Id == ((VoterCertificate)this.certificate).GroupId).Single();
            }

            SetEnable(false);
            this.printButton.Enabled = true;
            this.uploadButton.Enabled = true;
            this.doneButton.Enabled = true;
            this.cancelButton.Enabled = false;
          }
          else
          {
            this.certificateFileName = null;
            this.certificate = null;

            SetEnable(true);
            this.printButton.Enabled = false;
            this.uploadButton.Enabled = false;
            this.doneButton.Enabled = false;
            this.cancelButton.Enabled = true;
          }
        }
        else
        {
          File.Move(this.certificateFileName, this.certificateFileName + Files.BakExtension);
          this.certificateFileName = null;
          this.certificate = null;

          SetEnable(true);
          this.printButton.Enabled = false;
          this.uploadButton.Enabled = false;
          this.doneButton.Enabled = false;
          this.cancelButton.Enabled = true;
        }
      }
    }

    public void UpdateLanguage()
    {
      this.typeLabel.Text = Resources.CreateCertificateType;
      this.firstNameLabel.Text = Resources.CreateCertificateFirstname;
      this.familyNameLabel.Text = Resources.CreateCertificateSurname;
      this.emailAddressLabel.Text = Resources.CreateCertificateEmailAddress;
      this.emailNotificationCheckBox.Text = Resources.CreateCertificateEmailNotification;
      this.functionNameLabel.Text = Resources.CreateCertificateFunction;
      this.groupLabel.Text = Resources.CreateCertificateGroup;

      this.createButton.Text = Resources.SimpleChooseCertificateCreateButton;
      this.printButton.Text = Resources.SimpleChooseCertificatePrintButton;
      this.uploadButton.Text = Resources.SimpleChooseCertificateUploadButton;

      this.explainCreateLabel.Text = Resources.SimpleChooseCertificateCreateExplain;

      this.typeComboBox.Items.Clear();
      this.typeComboBox.Items.Add(Resources.CreateCertificateTypeVoter);
      this.typeComboBox.Items.Add(Resources.CreateCertificateTypeAuthority);
      this.typeComboBox.Items.Add(Resources.CreateCertificateTypeAdmin);

      this.groupComboBox.Clear();
      this.groupComboBox.Add(this.controller.Status.Groups);
    }

    private void SetEnable(bool enable)
    {
      this.typeComboBox.Enabled = enable;
      this.firstNameTextBox.Enabled = enable && this.typeComboBox.SelectedIndex >= 0;
      this.familyNameTextBox.Enabled = enable && this.typeComboBox.SelectedIndex >= 0;
      this.functionNameTextBox.Enabled = enable && this.typeComboBox.SelectedIndex >= 1;
      this.emailAddressTextBox.Enabled = enable && this.typeComboBox.SelectedIndex >= 0;
      this.emailNotificationCheckBox.Enabled = enable && this.typeComboBox.SelectedIndex == 0;
      this.emailNotificationCheckBox.Checked |= this.typeComboBox.SelectedIndex >= 1;
      this.groupComboBox.Enabled = enable && this.typeComboBox.SelectedIndex == 0;

      this.createButton.Enabled =
        enable &&
        this.typeComboBox.SelectedIndex >= 0 &&
        !this.firstNameTextBox.Text.IsNullOrEmpty() &&
        !this.familyNameTextBox.Text.IsNullOrEmpty() &&
        (!this.functionNameTextBox.Text.IsNullOrEmpty() || this.typeComboBox.SelectedIndex == 0) &&
        Mailer.IsEmailAddressValid(this.emailAddressTextBox.Text) &&
        (this.typeComboBox.SelectedIndex != 0 || this.groupComboBox.SelectedIndex >= 0);
    }

    private Certificate TryFindValidParentCertificate()
    {
      if (this.certificate != null &&
        this.certificate is VoterCertificate)
      {
        DirectoryInfo directory = new DirectoryInfo(this.controller.Status.DataPath);
        List<Certificate> candidates = new List<Certificate>();

        foreach (FileInfo file in directory.GetFiles(Files.CertificatePattern))
        {
          try
          {
            Certificate certificate = Serializable.Load<Certificate>(file.FullName);

            if (certificate is VoterCertificate &&
              ((VoterCertificate)certificate).GroupId != ((VoterCertificate)this.certificate).GroupId &&
              certificate.Validate(this.controller.Status.CertificateStorage) == CertificateValidationResult.Valid)
            {
              candidates.Add(certificate);
            }
          }
          catch
          {
          }
        }

        return candidates
          .OrderByDescending(candidate => candidate.ExpectedValidUntil(this.controller.Status.CertificateStorage, DateTime.Now))
          .FirstOrDefault();
      }
      else
      {
        return null;
      }
    }

    private void createButton_Click(object sender, EventArgs e)
    {
      SetEnable(false);

      string fullName = string.Format("{0} {1}, {2}",
        this.firstNameTextBox.Text,
        this.familyNameTextBox.Text,
        this.functionNameTextBox.Text);

      var encryptResult = EncryptPrivateKeyDialog.ShowSetPassphrase();

      if (encryptResult.First == DialogResult.OK)
      {
        string passphrase = encryptResult.Second;

        switch (this.typeComboBox.SelectedIndex)
        {
          case 0:
            this.certificate = new VoterCertificate(Resources.Culture.ToLanguage(), passphrase, this.groupComboBox.Value.Id);
            break;
          case 1:
            this.certificate = new AuthorityCertificate(Resources.Culture.ToLanguage(), passphrase, fullName);
            break;
          case 2:
            this.certificate = new AdminCertificate(Resources.Culture.ToLanguage(), passphrase, fullName);
            break;
          default:
            throw new InvalidOperationException("Bad type selection.");
        }

        this.certificate.CreateSelfSignature();
        this.certificateFileName = Path.Combine(this.controller.Status.DataPath, this.certificate.Id.ToString() + Files.CertificateExtension);
        this.certificate.Save(this.certificateFileName);

        Certificate parentCertificate = TryFindValidParentCertificate();

        if (parentCertificate != null)
        {
          DateTime parentValidUntil = parentCertificate.ExpectedValidUntil(this.controller.Status.CertificateStorage, DateTime.Now);
          DialogResult result = DialogResult.Yes;

          while (result == DialogResult.Yes)
          {
            result = MessageForm.Show(
              string.Format(Resources.AskToSignSignatureRequestWithParent, parentValidUntil),
              GuiResources.MessageBoxTitle,
              MessageBoxButtons.YesNo,
              MessageBoxIcon.Question,
              DialogResult.Yes);

            if (result == DialogResult.Yes &&
              DecryptPrivateKeyDialog.TryDecryptIfNessecary(parentCertificate, string.Empty))
            {
              this.signatureRequest = new SignatureRequest2(this.firstNameTextBox.Text, this.familyNameTextBox.Text, this.emailAddressTextBox.Text, parentCertificate);
              result = DialogResult.OK;
            }

            parentCertificate.Lock();
          }
        }

        if (this.signatureRequest == null)
        {
          this.signatureRequest = new SignatureRequest(this.firstNameTextBox.Text, this.familyNameTextBox.Text, this.emailAddressTextBox.Text);
        }

        this.signatureRequestInfo = new SignatureRequestInfo(this.emailNotificationCheckBox.Checked ? this.emailAddressTextBox.Text : string.Empty);
        this.secureSignatureRequest = new Secure<SignatureRequest>(this.signatureRequest, this.controller.Status.CaCertificate, this.certificate);
        this.secureSignatureRequestInfo = new Secure<SignatureRequestInfo>(this.signatureRequestInfo, this.controller.Status.ServerCertificate, this.certificate);

        string signatureRequestDataFileName = Path.Combine(this.controller.Status.DataPath, this.certificate.Id.ToString() + Files.SignatureRequestDataExtension);
        this.signatureRequest.Save(signatureRequestDataFileName);

        if (this.signatureRequest is SignatureRequest2)
        {
          this.uploadButton.Enabled = true;
        }
        else
        {
          this.printButton.Enabled = true;
        }
      }
      else
      {
        SetEnable(true);
      }
    }

    private void firstNameTextBox_TextChanged(object sender, EventArgs e)
    {
      SetEnable(true);
    }

    private void familyNameTextBox_TextChanged(object sender, EventArgs e)
    {
      SetEnable(true);
    }

    private void emailAddressTextBox_TextChanged(object sender, EventArgs e)
    {
      SetEnable(true);
    }

    private void printButton_Click(object sender, EventArgs e)
    {
      this.printButton.Enabled = false;

      SignatureRequestDocument document = new SignatureRequestDocument(this.signatureRequest, this.certificate, this.controller.Status.GetGroupName);
      PrintDialog printDialog = new PrintDialog();
      printDialog.Document = document;

      if (printDialog.ShowDialog() == DialogResult.OK &&
        printDialog.PrinterSettings.IsValid)
      {
        document.Print();
        this.uploadButton.Enabled = true;
      }

      this.printButton.Enabled = true;
    }

    private void uploadButton_Click(object sender, EventArgs e)
    {
      this.uploadButton.Enabled = false;

      if (this.controller.TrySetSignatureRequest(this.secureSignatureRequest, this.secureSignatureRequestInfo))
      {
        this.doneButton.Enabled = true;
        this.cancelButton.Enabled = false;
      }
      else
      {
        this.uploadButton.Enabled = true;
      }
    }

    private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      SetEnable(true);
    }

    private void functionNameTextBox_TextChanged(object sender, EventArgs e)
    {
      SetEnable(true);
    }

    private void cantonComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      SetEnable(true);
    }

    private void doneButton_Click(object sender, EventArgs e)
    {
      OnReturnFromControl();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      OnReturnFromControl();
    }
  }
}
