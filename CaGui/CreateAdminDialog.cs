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

namespace Pirate.PiVote.CaGui
{
  public partial class CreateAdminDialog : Form
  {
    public CreateAdminDialog()
    {
      InitializeComponent();
    }

    private void CaNameDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();
      this.validUntilPicker.Value = DateTime.Now.AddYears(1);
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

    private void CheckOkEnabled()
    {
      this.okButton.Enabled =
        !this.firstNameTextBox.Text.IsNullOrEmpty() &&
        !this.familyNameTextBox.Text.IsNullOrEmpty() &&
        !this.functionTextBox.Text.IsNullOrEmpty() &&
        !this.emailAddressTextBox.Text.IsNullOrEmpty() &&
        this.passphraseTextBox.Text.Length >= 12 &&
        this.passphraseTextBox.Text == this.repeatTextBox.Text;
    }

    private void firstNameTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckOkEnabled();
    }

    private void familyNameTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckOkEnabled();
    }

    private void functionTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckOkEnabled();
    }

    private void emailAddressTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckOkEnabled();
    }

    public string FirstName
    {
      get { return this.firstNameTextBox.Text; }
    }

    public string FamilyName
    {
      get { return this.familyNameTextBox.Text; }
    }

    public string Function
    {
      get { return this.functionTextBox.Text; }
    }

    public string EmailAddress
    {
      get { return this.emailAddressTextBox.Text; }
    }

    public DateTime ValidUntil
    {
      get { return this.validUntilPicker.Value; }
    }

    public string Passphrase
    {
      get { return this.passphraseTextBox.Text; }
    }

    private void passphraseTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckOkEnabled();
    }

    private void repeatTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckOkEnabled();
    }
  }
}
