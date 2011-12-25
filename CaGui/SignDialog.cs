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
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui.Printing;

namespace Pirate.PiVote.CaGui
{
  public partial class SignDialog : Form
  {
    private Language language;
    private SignatureRequest request;
    private Certificate certificate;
  
    public SignDialog()
    {
      InitializeComponent();
    }

    private void CaNameDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
      Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }

    public void Display(ListEntry listEntry, CertificateStorage storage, Certificate caCertificate, IEnumerable<ListEntry> allListEntries)
    {
      this.certificate = listEntry.Certificate;
      this.request = listEntry.Request;

      this.idTextBox.Text = this.certificate.Id.ToString();
      this.typeTextBox.Text = this.certificate.TypeText;
      this.nameTextBox.Text = this.request.FullName;
      this.emailAddressTextBox.Text = this.request.EmailAddress;
      this.cantonTextBox.Text = this.certificate is VoterCertificate ? GroupList.GetGroupName(((VoterCertificate)this.certificate).GroupId) : "N/A";
      this.fingerprintTextBox.Text = this.certificate.Fingerprint;
      this.language = this.certificate.Language;
      this.validFromPicker.MinDate = DateTime.Now;
      this.validFromPicker.MaxDate = DateTime.Now.AddMonths(6);
      this.validFromPicker.Value = DateTime.Now;

      bool requestValid = true;

      if (this.request is SignatureRequest2)
      {
        SignatureRequest2 request2 = (SignatureRequest2)this.request;
        ListEntry signingListEntry = allListEntries.Where(le => le.Certificate.IsIdentic(request2.SigningCertificate)).FirstOrDefault();
        requestValid &= signingListEntry != null;

        this.signedByIdTextBox.Text = request2.SigningCertificate.Id.ToString();
        this.signedByTypeTextBox.Text = request2.SigningCertificate.TypeText;
        this.signedByCantonTextBox.Text = request2.SigningCertificate is VoterCertificate ? GroupList.GetGroupName(((VoterCertificate)request2.SigningCertificate).GroupId) : "N/A";
        this.signedByFingerprintTextBox.Text = request2.SigningCertificate.Fingerprint;

        if (signingListEntry != null)
        {
          requestValid &= signingListEntry.Certificate.Fingerprint == request2.SigningCertificate.Fingerprint;

          this.signedByNameTextBox.Text = signingListEntry.Request.FullName;
          this.signedByEmailAddressTextBox.Text = signingListEntry.Request.EmailAddress;
          this.validUntilPicker.Value = request2.SigningCertificate.ExpectedValidUntil(storage, DateTime.Now);
          this.validUntilPicker.MinDate = DateTime.Now;
          this.validUntilPicker.MaxDate = this.validUntilPicker.Value;
          this.printButton.Enabled = true;
        }
        else
        {
          this.signedByNameTextBox.Text = "N/A";
          this.signedByEmailAddressTextBox.Text = "N/A";
          this.validUntilPicker.MinDate = DateTime.Now;
          this.validUntilPicker.MaxDate = DateTime.Now.AddYears(3).AddMonths(6);
          this.printButton.Enabled = false;
        }

        var result = request2.SigningCertificate.Validate(storage);
        requestValid &= result == CertificateValidationResult.Valid;
        this.signedByStatusTextBox.Text = result.ToString();
        this.signedByStatusTextBox.BackColor = result == CertificateValidationResult.Valid ? Color.Green : Color.Red;

        bool signatureValid = request2.IsSignatureValid();
        requestValid &= signatureValid;
        this.signedBySignatureTextBox.Text = signatureValid ? "Valid" : "Invalid";
        this.signedBySignatureTextBox.BackColor = signatureValid ? Color.Green : Color.Red;
      }
      else
      {
        this.signedByIdTextBox.Text = "N/A";
        this.signedByFingerprintTextBox.Text = "N/A";
        this.signedByNameTextBox.Text = "N/A";
        this.signedByEmailAddressTextBox.Text = "N/A";
        this.signedByStatusTextBox.Text = "N/A";
        this.signedBySignatureTextBox.Text = "N/A";
        this.signedByCantonLabel.Text = "N/A";
        this.signedByTypeLabel.Text = "N/A";
        this.printButton.Enabled = false;
        this.validUntilPicker.Value = DateTime.Now.AddYears(3);
      }

      if (requestValid && listEntry.VerifyRequestSimple())
      {
        LibraryResources.Culture = Language.English.ToCulture();
        this.reasonComboBox.Items.Add(LibraryResources.RefusedFingerprintNoMatch);
        this.reasonComboBox.Items.Add(LibraryResources.RefusedPersonHasAlready);
        this.reasonComboBox.Items.Add(LibraryResources.RefusedRequestForgotten);
        this.reasonComboBox.Items.Add(LibraryResources.RefusedRequestLost);
        this.reasonComboBox.Items.Add(LibraryResources.RefusedRequestNotValid);

        if (certificate is VoterCertificate)
        {
          this.reasonComboBox.Items.Add(LibraryResources.RefusedPersonNoPirate);
        }
        else
        {
          this.reasonComboBox.Items.Add(LibraryResources.RefusedPersonNotInOffice);
        }
      }
      else
      {
        this.refuseRadioButton.Checked = true;
        this.acceptSignRadioButton.Enabled = false;
      }

      CheckValid();
    }

    private void RefuseDialog_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          if (this.okButton.Enabled)
          {
            DialogResult = DialogResult.OK;
            Close();
          }
          break;
        case Keys.Escape:
          DialogResult = DialogResult.Cancel;
          Close();
          break;
      }
    }

    private void validUntilPicker_ValueChanged(object sender, EventArgs e)
    {
      CheckValid();
    }

    private void CheckValid()
    {
      this.okButton.Enabled = ((this.acceptSignRadioButton.Checked && this.validUntilPicker.Value > DateTime.Now) ||
                              (this.reasonComboBox.Enabled && this.reasonComboBox.SelectedIndex >= 0));
    }

    private void acceptSignRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      this.validFromPicker.Enabled = true;
      this.validUntilPicker.Enabled = this.acceptSignRadioButton.Checked;
      this.reasonComboBox.Enabled = this.refuseRadioButton.Checked;
      CheckValid();
    }

    private void refuseRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      this.validUntilPicker.Enabled = this.acceptSignRadioButton.Checked;
      this.reasonComboBox.Enabled = this.refuseRadioButton.Checked;
      CheckValid();
    }

    private void reasonComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckValid();
    }

    public bool Accept
    {
      get { return this.acceptSignRadioButton.Checked; }
    }

    public string Reason
    {
      get
      {
        LibraryResources.Culture = this.language.ToCulture();

        switch (this.reasonComboBox.SelectedIndex)
        {
          case 0:
            return LibraryResources.RefusedFingerprintNoMatch;
          case 1:
            return LibraryResources.RefusedPersonHasAlready;
          case 2:
            return LibraryResources.RefusedRequestForgotten;
          case 3:
            return LibraryResources.RefusedRequestLost;
          case 4:
            return LibraryResources.RefusedRequestNotValid;
          case 5:
            if (this.certificate is VoterCertificate)
            {
              return LibraryResources.RefusedPersonNoPirate;
            }
            else
            {
              return LibraryResources.RefusedPersonNotInOffice;
            }
          default:
            throw new InvalidOperationException("No valid reason.");
        }
      }
    }

    public DateTime ValidUntil
    {
      get { return this.validUntilPicker.Value; }
    }

    public DateTime ValidFrom
    {
      get { return this.validFromPicker.Value; }
    }

    private void printButton_Click(object sender, EventArgs e)
    {
      if (this.request == null)
        throw new InvalidOperationException("Request is null.");

      SignatureRequestDocument document = new SignatureRequestDocument(this.request, this.certificate, GroupList.GetGroupName);

      PrintDialog dialog = new PrintDialog();
      dialog.Document = document;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        document.Print();
        CheckValid();
      }
    }

    private void validFromPicker_ValueChanged(object sender, EventArgs e)
    {
      if (this.validFromPicker.Value.AddYears(3) > this.validUntilPicker.MaxDate)
      {
        this.validUntilPicker.Value = this.validUntilPicker.MaxDate;
      }
      else
      {
        this.validUntilPicker.Value = this.validFromPicker.Value.AddYears(3);
      }
    }
  }
}
