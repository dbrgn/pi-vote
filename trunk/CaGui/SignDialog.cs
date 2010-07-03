/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
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

namespace Pirate.PiVote.CaGui
{
  public partial class SignDialog : Form
  {
    private Language language;
    private int refusedFingerprintNoMatchIndex;
    private int refusedPersonHasAlreadyIndex;
    private int refusedPersonNoPirateIndex;
    private int refusedPersonNotInOfficeIndex;
    private int refusedRequestNotValidIndex;
  
    public SignDialog()
    {
      InitializeComponent();
    }

    private void CaNameDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();

      this.validUntilPicker.Value = DateTime.Now.AddYears(2);
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

    public void Display(CertificateAuthorityEntry entry, CertificateStorage storage)
    {
      Certificate certificate = entry.Certificate;
      SignatureRequest request = entry.Request.Value;

      this.idTextBox.Text = certificate.Id.ToString();
      this.typeTextBox.Text = certificate.TypeText;
      this.nameTextBox.Text = certificate.FullName;
      this.emailAddressTextBox.Text = request.EmailAddress;
      this.cantonTextBox.Text = certificate is VoterCertificate ? ((VoterCertificate)certificate).Canton.Text() : "N/A";
      this.fingerprintTextBox.Text = certificate.Fingerprint;
      this.language = certificate.Language;

      if (entry.Request.Verify(storage))
      {
        LibraryResources.Culture = Language.English.ToCulture();
        this.refusedFingerprintNoMatchIndex = this.reasonComboBox.Items.Add(LibraryResources.RefusedFingerprintNoMatch);
        this.refusedPersonHasAlreadyIndex = this.reasonComboBox.Items.Add(LibraryResources.RefusedPersonHasAlready);
        this.refusedPersonNoPirateIndex = -1;
        this.refusedPersonNotInOfficeIndex = -1;
        this.refusedRequestNotValidIndex = -1;

        if (certificate is VoterCertificate)
        {
          this.refusedPersonNoPirateIndex = this.reasonComboBox.Items.Add(LibraryResources.RefusedPersonNoPirate);
        }
        else
        {
          this.refusedPersonNotInOfficeIndex = this.reasonComboBox.Items.Add(LibraryResources.RefusedPersonNotInOffice);
        }
      }
      else
      {
        this.refusedRequestNotValidIndex = this.reasonComboBox.Items.Add(LibraryResources.RefusedRequestNotValid);
      }
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
      this.okButton.Enabled = (this.acceptSignRadioButton.Checked && this.validUntilPicker.Value > DateTime.Now) ||
                              (this.reasonComboBox.Enabled && this.reasonComboBox.SelectedIndex >= 0);
    }

    private void acceptSignRadioButton_CheckedChanged(object sender, EventArgs e)
    {
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

        if (this.reasonComboBox.SelectedIndex == this.refusedFingerprintNoMatchIndex)
        {
          return LibraryResources.RefusedFingerprintNoMatch;
        }
        else if (this.reasonComboBox.SelectedIndex == this.refusedPersonHasAlreadyIndex)
        {
          return LibraryResources.RefusedPersonHasAlready;
        }
        else if (this.reasonComboBox.SelectedIndex == this.refusedPersonNoPirateIndex)
        {
          return LibraryResources.RefusedPersonNoPirate;
        }
        else if (this.reasonComboBox.SelectedIndex == this.refusedPersonNotInOfficeIndex)
        {
          return LibraryResources.RefusedPersonNotInOffice;
        }
        else if (this.reasonComboBox.SelectedIndex == this.refusedRequestNotValidIndex)
        {
          return LibraryResources.RefusedRequestNotValid;
        }
        else
        {
          throw new InvalidOperationException("No valid reason.");
        }
      }
    }

    public DateTime ValidUntil
    {
      get { return this.validUntilPicker.Value; }
    }
  }
}
