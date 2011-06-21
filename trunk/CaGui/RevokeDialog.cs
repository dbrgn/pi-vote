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

namespace Pirate.PiVote.CaGui
{
  public partial class RevokeDialog : Form
  {
    private Language language;
    private Certificate certificate;

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
      this.cantonTextBox.Text = certificate is VoterCertificate ? GroupList.GetGroupName(((VoterCertificate)certificate).GroupId) : "N/A";
      this.fingerprintTextBox.Text = certificate.Fingerprint;
      this.language = certificate.Language;
      this.certificate = entry.Certificate;

      LibraryResources.Culture = Language.English.ToCulture();
      this.reasonComboBox.Items.Add(LibraryResources.RevokedMoved);
      this.reasonComboBox.Items.Add(LibraryResources.RevokedStolen);
      this.reasonComboBox.Items.Add(LibraryResources.RevokedLost);
      this.reasonComboBox.Items.Add(LibraryResources.RevokedForgotten);
      this.reasonComboBox.Items.Add(LibraryResources.RevokedError);

      if (entry.Certificate is VoterCertificate)
      {
        this.reasonComboBox.Items.Add(LibraryResources.RevokedNoLonger);
      }
      else
      {
        this.reasonComboBox.Items.Add(LibraryResources.RevokedNoMoreFx);
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

        switch (this.reasonComboBox.SelectedIndex)
        {
          case 0:
            return LibraryResources.RevokedMoved;
          case 1:
            return LibraryResources.RevokedStolen;
          case 2:
            return LibraryResources.RevokedLost;
          case 3:
            return LibraryResources.RevokedForgotten;
          case 4:
            return LibraryResources.RevokedError;
          case 5:
            if (this.certificate is VoterCertificate)
            {
              return LibraryResources.RevokedNoLonger;
            }
            else
            {
              return LibraryResources.RevokedNoMoreFx;
            }
          default:
            throw new InvalidOperationException("No valid reason.");
        }
      }
    }
  }
}
