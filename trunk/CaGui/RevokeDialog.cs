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
  public partial class RevokeDialog : Form
  {
    private Language language;
    private int revokedLostIndex;
    private int revokedMovedIndex;
    private int revokedNoLongerIndex;
    private int revokedNoMoreFxIndex;
    private int revokedStolenIndex;

    public RevokeDialog()
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

    public void Display(CertificateAuthorityEntry entry, CertificateStorage storage, Certificate caCertificate)
    {
      Certificate certificate = entry.Certificate;
      SignatureRequest request = entry.RequestValue(caCertificate);

      this.idTextBox.Text = certificate.Id.ToString();
      this.typeTextBox.Text = certificate.TypeText;
      this.nameTextBox.Text = certificate.FullName;
      this.emailAddressTextBox.Text = request.EmailAddress;
      this.cantonTextBox.Text = certificate is VoterCertificate ? ((VoterCertificate)certificate).Canton.Text() : "N/A";
      this.fingerprintTextBox.Text = certificate.Fingerprint;
      this.language = certificate.Language;

      LibraryResources.Culture = Language.English.ToCulture();
      this.revokedLostIndex = this.reasonComboBox.Items.Add(LibraryResources.RefusedFingerprintNoMatch);
      this.revokedMovedIndex = this.reasonComboBox.Items.Add(LibraryResources.RefusedPersonHasAlready);
      this.revokedNoLongerIndex = this.reasonComboBox.Items.Add(LibraryResources.RefusedPersonNoPirate);
      this.revokedNoMoreFxIndex = this.reasonComboBox.Items.Add(LibraryResources.RefusedPersonNotInOffice);
      this.revokedStolenIndex = this.reasonComboBox.Items.Add(LibraryResources.RefusedRequestNotValid);
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
      this.okButton.Enabled = this.reasonComboBox.SelectedIndex >= 0;
    }

    private void reasonComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckValid();
    }

    public string Reason
    {
      get
      {
        LibraryResources.Culture = this.language.ToCulture();

        if (this.reasonComboBox.SelectedIndex == this.revokedLostIndex)
        {
          return LibraryResources.RevokedLost;
        }
        else if (this.reasonComboBox.SelectedIndex == this.revokedMovedIndex)
        {
          return LibraryResources.RevokedMoved;
        }
        else if (this.reasonComboBox.SelectedIndex == this.revokedNoLongerIndex)
        {
          return LibraryResources.RevokedNoLonger;
        }
        else if (this.reasonComboBox.SelectedIndex == this.revokedNoMoreFxIndex)
        {
          return LibraryResources.RevokedNoMoreFx;
        }
        else if (this.reasonComboBox.SelectedIndex == this.revokedStolenIndex)
        {
          return LibraryResources.RevokedStolen;
        }
        else
        {
          throw new InvalidOperationException("No valid reason.");
        }
      }
    }
  }
}
