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

namespace Pirate.PiVote.Client
{
  public partial class SimpleChooseCertificateItem : WizardItem
  {
    private Certificate certificate;
    private Signed<SignatureRequest> signedRequest;
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

      this.advancedRadioButton.Text = Resources.SimpleChooseCertificateAdvancedOption;
      this.createRadioButton.Text = Resources.SimpleChooseCertificateCreateOption;
      this.importRadioButton.Text = Resources.SimpleChooseCertificateImportOption;

      this.importButton.Text = Resources.SimpleChooseCertificateImportButton;
      this.createButton.Text = Resources.SimpleChooseCertificateCreateButton;
      this.printButton.Text = Resources.SimpleChooseCertificatePrintButton;
      this.uploadButton.Text = Resources.SimpleChooseCertificateUploadButton;

      this.headerLabel.Text = Resources.SimpleChooseCertificateHeader;
      this.explainCreateLabel.Text = Resources.SimpleChooseCertificateCreateExplain;
    }

    private void createRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      if (this.createRadioButton.Checked)
      {
        this.firstNameTextBox.Enabled = true;
        this.familyNameTextBox.Enabled = true;
        this.emailAddressTextBox.Enabled = true;
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
        this.createRadioButton.Enabled = false;
        this.printButton.Enabled = false;
        this.uploadButton.Enabled = false;
        this.importButton.Enabled = false;
        this.done = false;
        this.canNext = true;
        OnUpdateWizard();
      }
    }

    private void createButton_Click(object sender, EventArgs e)
    {
      this.run = true;
      OnUpdateWizard();

      this.advancedRadioButton.Enabled = false;
      this.createRadioButton.Enabled = false;
      this.importRadioButton.Enabled = false;

      this.firstNameTextBox.Enabled = false;
      this.familyNameTextBox.Enabled = false;
      this.emailAddressTextBox.Enabled = false;
      this.createButton.Enabled = false;

      this.certificate = new VoterCertificate();
      this.certificate.CreateSelfSignature();

      var request = new SignatureRequest(this.firstNameTextBox.Text, this.familyNameTextBox.Text, this.emailAddressTextBox.Text);
      this.signedRequest = new Signed<SignatureRequest>(request, this.certificate);

      this.run = false;
      OnUpdateWizard();
      this.printButton.Enabled = true;
    }

    private void CheckValid()
    {
      this.createButton.Enabled =
        !this.firstNameTextBox.Text.IsNullOrEmpty() &&
        !this.familyNameLabel.Text.IsNullOrEmpty() &&
        !this.emailAddressLabel.Text.IsNullOrEmpty();
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

      SignatureRequestDocument document = new SignatureRequestDocument(this.signedRequest.Value, this.certificate);
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

      Status.VotingClient.SetSignatureRequest(this.signedRequest, SetSignatureRequestComplete);

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

        Status.Certificate = this.certificate;
        Status.CertificateFileName = Path.Combine(Status.DataPath, certificate.Id.ToString() + ".pi-cert");
        Status.Certificate.Save(Status.CertificateFileName);
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
        this.certificate = Serializable.Load<Certificate>(dialog.FileName);

        string newFileName = Path.Combine(Status.DataPath, certificate.Id.ToString() + ".pi-cert");
        File.Copy(dialog.FileName, newFileName);
      }

      this.canNext = true;
      this.done = false;
      OnUpdateWizard();
    }
  }
}
