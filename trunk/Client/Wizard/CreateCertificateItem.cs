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
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Client
{
  public partial class CreateCertificateItem : WizardItem
  {
    private const int VoterType = 0;
    private const int AuthorityType = 1;
    private const int AdminType = 2;

    private Certificate certificate;

    public CreateCertificateItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      CheckCertificateItem item = new CheckCertificateItem();
      Status.Certificate = this.certificate;
      return item;
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
      get { return this.certificate != null; }
    }

    public override bool CanPrevious
    {
      get { return true; }
    }

    public override bool CanCancel
    {
      get { return true; }
    }

    private void StartWizardItem_Load(object sender, EventArgs e)
    {

    }

    private void CheckSaveEnabled()
    {
      switch (typeComboBox.SelectedIndex)
      {
        case VoterType:
          this.saveButton.Enabled = true;
          break;
        case AuthorityType:
          this.saveButton.Enabled =
            !this.firstNameTextBox.Text.IsNullOrEmpty() &&
            !this.familyNameTextBox.Text.IsNullOrEmpty() &&
            !this.functionNameTextBox.Text.IsNullOrEmpty();
          break;
        case AdminType:
          this.saveButton.Enabled =
            !this.firstNameTextBox.Text.IsNullOrEmpty() &&
            !this.familyNameTextBox.Text.IsNullOrEmpty() &&
            !this.functionNameTextBox.Text.IsNullOrEmpty();
          break;
        default:
          this.saveButton.Enabled = false;
          break;
      }
    }

    private void typeComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      switch (typeComboBox.SelectedIndex)
      {
        case VoterType:
          this.firstNameTextBox.Enabled = false;
          this.familyNameTextBox.Enabled = false;
          this.functionNameTextBox.Enabled = false;
          break;
        case AuthorityType:
          this.firstNameTextBox.Enabled = true;
          this.familyNameTextBox.Enabled = true;
          this.functionNameTextBox.Enabled = true;
          break;
        case AdminType:
          this.firstNameTextBox.Enabled = true;
          this.familyNameTextBox.Enabled = true;
          this.functionNameTextBox.Enabled = true;
          break;
      }

      CheckSaveEnabled();
    }

    private void firstNameTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckSaveEnabled();
    }

    private void familyNameTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckSaveEnabled();
    }

    private void functionNameTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckSaveEnabled();
    }

    private void saveButton_Click(object sender, EventArgs e)
    {
      switch (typeComboBox.SelectedIndex)
      {
        case VoterType:
          this.certificate = new VoterCertificate();
          break;
        case AuthorityType:
          string authorityFullName =
            string.Format("{0} {1}, {2}",
            this.firstNameTextBox.Text,
            this.familyNameTextBox.Text,
            this.functionNameTextBox.Text);
          this.certificate = new AuthorityCertificate(authorityFullName);
          break;
        case AdminType:
          string adminFullName =
            string.Format("{0} {1}, {2}",
            this.firstNameTextBox.Text,
            this.familyNameTextBox.Text,
            this.functionNameTextBox.Text);
          this.certificate = new AdminCertificate(adminFullName);
          break;
      }

      certificate.CreateSelfSignature();

      string fileName = Path.Combine(Status.DataPath, certificate.Id.ToString() + ".pi-cert");
      certificate.Save(fileName);

      OnUpdateWizard();
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.typeLabel.Text = Resources.CreateCertificateType;
      this.firstNameLabel.Text = Resources.CreateCertificateFirstname;
      this.familyNameLabel.Text = Resources.CreateCertificateSurname;
      this.functionNameLabel.Text = Resources.CreateCertificateFunction;
      this.saveButton.Text = Resources.CreateCertificateSaveButton;

      this.typeComboBox.Items.Clear();
      this.typeComboBox.Items.Add(Resources.CreateCertificateTypeVoter);
      this.typeComboBox.Items.Add(Resources.CreateCertificateTypeAuthority);
      this.typeComboBox.Items.Add(Resources.CreateCertificateTypeAdmin);
    }
  }
}
