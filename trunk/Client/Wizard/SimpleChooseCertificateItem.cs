/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Printing;

namespace Pirate.PiVote.Client
{
  public partial class SimpleChooseCertificateItem : WizardItem
  {
    private SignatureRequest signatureRequest;
    private SignatureRequestInfo signatureRequestInfo;
    private Secure<SignatureRequest> secureSignatureRequest;
    private Secure<SignatureRequestInfo> secureSignatureRequestInfo;
    private bool run = false;
    private Exception exception;
    private bool done = false;
    private bool canNext = false;

    public SimpleChooseCertificateItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      if (this.advancedRadioButton.Checked)
      {
        return new ChooseCertificateItem();
      }
      else
      {
        return new CheckCertificateItem();
      }
    }

    public override WizardItem Previous()
    {
      return new StartItem();
    }

    public override WizardItem Cancel()
    {
      return null;
    }

    public override bool CanNext
    {
      get { return !this.run && this.canNext; }
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
      this.advancedRadioButton.Enabled = true;
      this.createRadioButton.Enabled = true;
      this.importRadioButton.Enabled = true;
      this.createRadioButton.Checked = true;
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.firstNameLabel.Text = Resources.CreateCertificateFirstname;
      this.familyNameLabel.Text = Resources.CreateCertificateSurname;
      this.emailAddressLabel.Text = Resources.CreateCertificateEmailAddress;
      this.emailNotificationCheckBox.Text = Resources.CreateCertificateEmailNotification;
      this.groupLabel.Text = Resources.CreateCertificateGroup;

      this.advancedRadioButton.Text = Resources.SimpleChooseCertificateAdvancedOption;
      this.createRadioButton.Text = Resources.SimpleChooseCertificateCreateOption;
      this.importRadioButton.Text = Resources.SimpleChooseCertificateImportOption;

      this.importButton.Text = Resources.SimpleChooseCertificateImportButton;
      this.createButton.Text = Resources.SimpleChooseCertificateCreateButton;
      this.printButton.Text = Resources.SimpleChooseCertificatePrintButton;
      this.uploadButton.Text = Resources.SimpleChooseCertificateUploadButton;

      this.headerLabel.Text = Resources.SimpleChooseCertificateHeader;
      this.explainCreateLabel.Text = Resources.SimpleChooseCertificateCreateExplain;

      this.groupComboBox.Clear();
      this.groupComboBox.Add(Status.Groups);
    }

    private void createRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      if (this.createRadioButton.Checked)
      {
        this.firstNameTextBox.Enabled = true;
        this.familyNameTextBox.Enabled = true;
        this.emailAddressTextBox.Enabled = true;
        this.groupComboBox.Enabled = true;
        CheckValid();
        this.printButton.Enabled = false;
        this.uploadButton.Enabled = false;
        this.importButton.Enabled = false;
        this.done = false;
        this.canNext = false;
        OnUpdateWizard();
      }
    }

    private void importReadioButton_CheckedChanged(object sender, EventArgs e)
    {
      if (this.importRadioButton.Checked)
      {
        this.firstNameTextBox.Enabled = false;
        this.familyNameTextBox.Enabled = false;
        this.emailAddressTextBox.Enabled = false;
        this.groupComboBox.Enabled = false;
        this.createButton.Enabled = false;
        this.printButton.Enabled = false;
        this.uploadButton.Enabled = false;
        this.importButton.Enabled = true;
        this.done = false;
        this.canNext = false;
        OnUpdateWizard();
      }
    }

    private void advancedRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      if (this.advancedRadioButton.Checked)
      {
        this.firstNameTextBox.Enabled = false;
        this.familyNameTextBox.Enabled = false;
        this.emailAddressTextBox.Enabled = false;
        this.groupComboBox.Enabled = false;
        this.createRadioButton.Enabled = false;
        this.printButton.Enabled = false;
        this.uploadButton.Enabled = false;
        this.importButton.Enabled = false;
        this.done = false;
        this.canNext = true;
        OnUpdateWizard();
      }
    }

    private void SetEnable(bool enable)
    {
      this.advancedRadioButton.Enabled = enable;
      this.createRadioButton.Enabled = enable;
      this.importRadioButton.Enabled = enable;
      this.firstNameTextBox.Enabled = enable;
      this.familyNameTextBox.Enabled = enable;
      this.emailAddressTextBox.Enabled = enable;
      this.emailNotificationCheckBox.Enabled = enable;
      this.groupComboBox.Enabled = enable;
      this.createButton.Enabled = enable;
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
      
      var encryptResult = EncryptPrivateKeyDialog.ShowSetPassphrase();

      if (encryptResult.First == DialogResult.OK)
      {
        string passphrase = encryptResult.Second;

        Status.Certificate = new VoterCertificate(Resources.Culture.ToLanguage(), passphrase, this.groupComboBox.Value.Id);
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
              Resources.MessageBoxTitle,
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

    private void CheckValid()
    {
      this.createButton.Enabled =
        !this.firstNameTextBox.Text.IsNullOrEmpty() &&
        !this.familyNameTextBox.Text.IsNullOrEmpty() &&
        Mailer.IsEmailAddressValid(this.emailAddressTextBox.Text) &&
        this.groupComboBox.SelectedIndex >= 0;
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

    private void printButton_Click(object sender, EventArgs e)
    {
      this.run = true;
      OnUpdateWizard();
      this.printButton.Enabled = false;

      SignatureRequestDocument document = new SignatureRequestDocument(this.signatureRequest, Status.Certificate, Status.GetGroupName);
      PrintDialog printDialog = new PrintDialog();
      printDialog.Document = document;

      if (printDialog.ShowDialog() == DialogResult.OK &&
        printDialog.PrinterSettings.IsValid)
      {
        document.Print();
        this.uploadButton.Enabled = true;
      }
      else
      {
        this.printButton.Enabled = true;
      }

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
      }

      OnUpdateWizard();
    }

    private void SetSignatureRequestComplete(Exception exception)
    {
      this.run = false;
      this.exception = exception;
    }

    private void importButton_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Title = Resources.ChooseCertificateLoadDialog;
      dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      dialog.CheckPathExists = true;
      dialog.CheckFileExists = true;
      dialog.Filter = Files.CertificateFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        Status.Certificate = Serializable.Load<Certificate>(dialog.FileName);

        string newFileName = Path.Combine(Status.DataPath, Status.Certificate.Id.ToString() + ".pi-cert");
        File.Copy(dialog.FileName, newFileName);

        this.importButton.Enabled = false;
        this.createRadioButton.Enabled = false;
        this.importRadioButton.Enabled = false;
        this.advancedRadioButton.Enabled = false;

        Status.SetMessage(Resources.SimpleChooseCertificateImportDone, MessageType.Success);
      }

      this.canNext = true;
      this.done = false;
      OnUpdateWizard();
    }

    private void cantonComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckValid();
    }
  }
}
