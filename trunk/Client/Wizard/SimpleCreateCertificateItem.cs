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
using Pirate.PiVote.Gui.Printing;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Client
{
  public partial class SimpleCreateCertificateItem : WizardItem
  {
    private byte[] signatureRequestKey;
    private SignatureRequest signatureRequest;
    private SignatureRequestInfo signatureRequestInfo;
    private Secure<SignatureRequest> secureSignatureRequest;
    private Secure<SignatureRequestInfo> secureSignatureRequestInfo;
    private bool run = false;
    private Exception exception;
    private bool done = false;

    public SimpleCreateCertificateItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      return new CheckCertificateItem();
    }

    public override WizardItem Previous()
    {
      return new ChooseCertificateItem();
    }

    public override WizardItem Cancel()
    {
      return null;
    }

    public override bool CanNext
    {
      get { return false; }
    }

    public override bool CancelIsDone
    {
      get { return !this.run && this.done; }
    }

    public override bool CanPrevious
    {
      get { return !this.run && !this.done; }
    }

    public override bool CanCancel
    {
      get { return !this.run; }
    }

    private void StartWizardItem_Load(object sender, EventArgs e)
    {
    }

    public override void Begin()
    {
      if (Status.Certificate == null)
      {
        SetEnable(true);
        this.printButton.Enabled = false;
        this.uploadButton.Enabled = false;
        this.done = false;
      }
      else if (Status.Certificate.Validate(Status.CertificateStorage) == CertificateValidationResult.Valid)
      {
        SetEnable(false);
        this.printButton.Enabled = false;
        this.uploadButton.Enabled = false;
        this.done = true;
        Status.SetMessage(Resources.CheckCertificateReady, MessageType.Info);
      }
      else
      {
        string signatureRequestDataFileName = Path.Combine(Status.DataPath, Status.Certificate.Id.ToString() + Files.SignatureRequestDataExtension);

        if (File.Exists(signatureRequestDataFileName))
        {
          if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(Status.Certificate, GuiResources.UnlockActionSignRequest))
          {
            this.signatureRequest = Serializable.Load<SignatureRequest>(signatureRequestDataFileName);
            this.signatureRequestInfo = new SignatureRequestInfo(
              this.signatureRequest.EmailAddress,
              this.signatureRequest.Encrypt());
            this.secureSignatureRequest = new Secure<SignatureRequest>(this.signatureRequest, Status.CaCertificate, Status.Certificate);
            this.secureSignatureRequestInfo = new Secure<SignatureRequestInfo>(this.signatureRequestInfo, Status.ServerCertificate, Status.Certificate);

            if (Status.Certificate is VoterCertificate)
            {
              this.typeComboBox.SelectedIndex = 0;
            }
            else if (Status.Certificate is AuthorityCertificate)
            {
              this.typeComboBox.SelectedIndex = 1;
            }
            else if (Status.Certificate is AdminCertificate)
            {
              this.typeComboBox.SelectedIndex = 2;
            }

            this.firstNameTextBox.Text = this.signatureRequest.FirstName;
            this.familyNameTextBox.Text = this.signatureRequest.FamilyName;
            this.emailAddressTextBox.Text = this.signatureRequest.EmailAddress;

            if (Status.Certificate is VoterCertificate)
            {
              this.groupComboBox.Value = Status.Groups.Where(group => group.Id == ((VoterCertificate)Status.Certificate).GroupId).Single();
            }

            SetEnable(false);
            this.printButton.Enabled = true;
            this.uploadButton.Enabled = true;
            this.done = false;
          }
          else
          {
            Status.CertificateFileName = null;
            Status.Certificate = null;

            SetEnable(true);
            this.printButton.Enabled = false;
            this.uploadButton.Enabled = false;
            this.done = false;
            Status.SetMessage(Resources.SimpleCreateCertificateSigningCanceled, MessageType.Info);
          }
        }
        else
        {
          File.Move(Status.CertificateFileName, Status.CertificateFileName + Files.BakExtension);
          Status.CertificateFileName = null;
          Status.Certificate = null;

          SetEnable(true);
          this.printButton.Enabled = false;
          this.uploadButton.Enabled = false;
          this.done = false;
          Status.SetMessage(Resources.SimpleCreateCertificateFileMissing, MessageType.Error);
        }
      }

      OnUpdateWizard();
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

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
      this.typeComboBox.Items.Add(Resources.CreateCertificateTypeNotary);

      this.groupComboBox.Clear();
      this.groupComboBox.Add(Status.Groups);
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
      if (Status.Certificate != null &&
        Status.Certificate is VoterCertificate)
      {
        DirectoryInfo directory = new DirectoryInfo(Status.DataPath);
        List<Certificate> candidates = new List<Certificate>();

        foreach (FileInfo file in directory.GetFiles(Files.CertificatePattern))
        {
          try
          {
            Certificate certificate = Serializable.Load<Certificate>(file.FullName);

            if (certificate is VoterCertificate &&
              ((VoterCertificate)certificate).GroupId != ((VoterCertificate)Status.Certificate).GroupId &&
              certificate.Validate(Status.CertificateStorage) == CertificateValidationResult.Valid)
            {
              candidates.Add(certificate);
            }
          }
          catch
          {
          }
        }

        return candidates
          .OrderByDescending(candidate => candidate.ExpectedValidUntil(Status.CertificateStorage, DateTime.Now))
          .FirstOrDefault();
      }
      else
      {
        return null;
      }
    }

    private void createButton_Click(object sender, EventArgs e)
    {
      this.run = true;
      OnUpdateWizard();
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
            Status.Certificate = new VoterCertificate(Resources.Culture.ToLanguage(), passphrase, this.groupComboBox.Value.Id);
            break;
          case 1:
            Status.Certificate = new AuthorityCertificate(Resources.Culture.ToLanguage(), passphrase, fullName);
            break;
          case 2:
            Status.Certificate = new NotaryCertificate(Resources.Culture.ToLanguage(), passphrase, fullName);
            break;
          default:
            throw new InvalidOperationException("Bad type selection.");
        }

        Status.Certificate.CreateSelfSignature();
        Status.CertificateFileName = Path.Combine(Status.DataPath, Status.Certificate.Id.ToString() + Files.CertificateExtension);
        Status.Certificate.Save(Status.CertificateFileName);

        Certificate parentCertificate = TryFindValidParentCertificate();

        if (parentCertificate != null)
        {
          DateTime parentValidUntil = parentCertificate.ExpectedValidUntil(Status.CertificateStorage, DateTime.Now);
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

        this.signatureRequestInfo = new SignatureRequestInfo(
          this.emailNotificationCheckBox.Checked ? this.emailAddressTextBox.Text : string.Empty,
          this.signatureRequest.Encrypt());
        this.secureSignatureRequest = new Secure<SignatureRequest>(this.signatureRequest, Status.CaCertificate, Status.Certificate);
        this.secureSignatureRequestInfo = new Secure<SignatureRequestInfo>(this.signatureRequestInfo, Status.ServerCertificate, Status.Certificate);

        string signatureRequestDataFileName = Path.Combine(Status.DataPath, Status.Certificate.Id.ToString() + Files.SignatureRequestDataExtension);
        this.signatureRequest.Save(signatureRequestDataFileName);

        this.run = false;
        OnUpdateWizard();

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
        this.run = false;
        OnUpdateWizard();
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
      this.run = true;
      OnUpdateWizard();
      this.printButton.Enabled = false;

      SignatureRequestDocument document = new SignatureRequestDocument(
        this.signatureRequest,
        Status.Certificate, 
        Status.GetGroupName);
      PrintDialog printDialog = new PrintDialog();
      printDialog.Document = document;

      if (printDialog.ShowDialog() == DialogResult.OK &&
        printDialog.PrinterSettings.IsValid)
      {
        document.Print();
        this.uploadButton.Enabled = true;
      }

      this.printButton.Enabled = true;
      this.run = false;
      OnUpdateWizard();
    }

    private void uploadButton_Click(object sender, EventArgs e)
    {
      this.run = true;
      OnUpdateWizard();
      this.uploadButton.Enabled = false;

      Status.VotingClient.SetSignatureRequest(this.secureSignatureRequest, this.secureSignatureRequestInfo, SetSignatureRequestComplete);

      while (this.run)
      {
        Status.UpdateProgress();
        Thread.Sleep(10);
      }

      Status.UpdateProgress();

      if (this.exception == null)
      {
        Status.SetMessage(Resources.CreateCertificateDone, MessageType.Success);
        this.done = true;
      }
      else
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
        this.uploadButton.Enabled = true;
      }

      OnUpdateWizard();
    }

    private void SetSignatureRequestComplete(Exception exception)
    {
      this.run = false;
      this.exception = exception;
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
  }
}
