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
  public partial class SimpleCreateCertificateItem : WizardItem
  {
    private Certificate certificate;
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
      this.typeComboBox.Enabled = true;
      this.firstNameTextBox.Enabled = false;
      this.familyNameTextBox.Enabled = false;
      this.functionNameTextBox.Enabled = false;
      this.emailAddressTextBox.Enabled = false;
      this.cantonComboBox.Enabled = false;

      CheckValid();

      this.printButton.Enabled = false;
      this.uploadButton.Enabled = false;

      this.done = false;
      OnUpdateWizard();
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.typeLabel.Text = Resources.CreateCertificateType;
      this.firstNameLabel.Text = Resources.CreateCertificateFirstname;
      this.familyNameLabel.Text = Resources.CreateCertificateSurname;
      this.emailAddressLabel.Text = Resources.CreateCertificateEmailAddress;
      this.emailAddressTextBox.Text = Resources.CreateCertificateEmailNotification;
      this.functionNameLabel.Text = Resources.CreateCertificateFunction;
      this.cantonLabel.Text = Resources.CreateCertificateCanton;

      this.createButton.Text = Resources.SimpleChooseCertificateCreateButton;
      this.printButton.Text = Resources.SimpleChooseCertificatePrintButton;
      this.uploadButton.Text = Resources.SimpleChooseCertificateUploadButton;

      this.explainCreateLabel.Text = Resources.SimpleChooseCertificateCreateExplain;

      this.typeComboBox.Items.Clear();
      this.typeComboBox.Items.Add(Resources.CreateCertificateTypeVoter);
      this.typeComboBox.Items.Add(Resources.CreateCertificateTypeAuthority);
      this.typeComboBox.Items.Add(Resources.CreateCertificateTypeAdmin);

      this.cantonComboBox.Items.Clear();
      foreach (Canton canton in Enum.GetValues(typeof(Canton)))
      {
        this.cantonComboBox.Items.Add(canton.Text());
      }
    }

    private void createButton_Click(object sender, EventArgs e)
    {
      this.run = true;
      OnUpdateWizard();

      this.typeComboBox.Enabled = false;
      this.firstNameTextBox.Enabled = false;
      this.familyNameTextBox.Enabled = false;
      this.functionNameTextBox.Enabled = false;
      this.emailAddressTextBox.Enabled = false;
      this.cantonComboBox.Enabled = false;
      this.createButton.Enabled = false;

      string fullName = string.Format("{0} {1}, {2}",
        this.firstNameTextBox.Text,
        this.familyNameTextBox.Text,
        this.functionNameTextBox.Text);

      switch (this.typeComboBox.SelectedIndex)
      {
        case 0:
          this.certificate = new VoterCertificate(Resources.Culture.ToLanguage(), (Canton)this.cantonComboBox.SelectedIndex);
          break;
        case 1:
          this.certificate = new AuthorityCertificate(Resources.Culture.ToLanguage(), fullName);
          break;
        case 2:
          this.certificate = new AdminCertificate(Resources.Culture.ToLanguage(), fullName);
          break;
        default:
          throw new InvalidOperationException("Bad type selection.");
      }

      this.certificate.CreateSelfSignature();

      this.signatureRequest = new SignatureRequest(this.firstNameTextBox.Text, this.familyNameTextBox.Text, this.emailAddressTextBox.Text);
      this.signatureRequestInfo = new SignatureRequestInfo(this.emailNotificationCheckBox.Checked ? this.emailAddressTextBox.Text : string.Empty);
      this.secureSignatureRequest = new Secure<SignatureRequest>(this.signatureRequest, this.certificate, Status.CaCertificate);
      this.secureSignatureRequestInfo = new Secure<SignatureRequestInfo>(this.signatureRequestInfo, this.certificate, Status.ServerCertificate);

      this.run = false;
      OnUpdateWizard();
      this.printButton.Enabled = true;
    }

    private void CheckValid()
    {
      this.createButton.Enabled =
        this.typeComboBox.SelectedIndex >= 0 &&
        !this.firstNameTextBox.Text.IsNullOrEmpty() &&
        !this.familyNameTextBox.Text.IsNullOrEmpty() &&
        (!this.functionNameTextBox.Text.IsNullOrEmpty() || this.typeComboBox.SelectedIndex == 0) &&
        Mailer.IsEmailAddressValid(this.emailAddressTextBox.Text) &&
        (this.typeComboBox.SelectedIndex != 0 || this.cantonComboBox.SelectedIndex >= 0);
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

      SignatureRequestDocument document = new SignatureRequestDocument(this.signatureRequest, this.certificate);
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

    private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.firstNameTextBox.Enabled = this.typeComboBox.SelectedIndex >= 0;
      this.familyNameTextBox.Enabled = this.typeComboBox.SelectedIndex >= 0;
      this.emailAddressTextBox.Enabled = this.typeComboBox.SelectedIndex >= 0;
      this.functionNameTextBox.Enabled = 
        this.typeComboBox.SelectedIndex == 1 || 
        this.typeComboBox.SelectedIndex == 2;
      this.cantonComboBox.Enabled = this.typeComboBox.SelectedIndex == 0;
      CheckValid();
    }

    private void functionNameTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckValid();
    }

    private void cantonComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckValid();
    }
  }
}
