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
  public partial class CreateCaDialog : Form
  {
    public CreateCaDialog()
    {
      InitializeComponent();
    }

    private void CaNameDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();
      CheckOkEnabled();
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

    private void CheckOkEnabled()
    {
      this.okButton.Enabled =
        this.caNameTextBox.Text.Length > 0 &&
        this.passphraseTextBox.Text.Length >= 12 &&
        this.passphraseTextBox.Text == this.repeatTextBox.Text;
    }

    private void CreateCaDialog_KeyDown(object sender, KeyEventArgs e)
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

    private void passphraseTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckOkEnabled();
    }

    private void repeatTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckOkEnabled();
    }

    public string CaName
    {
      get { return this.caNameTextBox.Text; }
    }

    public bool RootCa
    {
      get { return this.rootCaCheckBox.Checked; }
    }

    public string Passphrase
    {
      get { return this.passphraseTextBox.Text; }
    }

    private void caNameTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckOkEnabled();
    }
  }
}
