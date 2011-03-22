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

namespace Pirate.PiVote.Gui
{
  public partial class EncryptPrivateKeyDialog : Form
  {
    public EncryptPrivateKeyDialog()
    {
      InitializeComponent();
    }

    private void EncryptPrivateKeyDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();

      Text = GuiResources.EncryptPrivateKeyTitle;
      this.infoLabel.Text = GuiResources.EncryptPrivateKeyInfo;
      this.encryptCheckBox.Text = GuiResources.EncryptPrivateKeyChoose;
      this.passphraseLabel.Text = GuiResources.EncryptPrivateKeyPassphrase;
      this.repeatLabel.Text = GuiResources.EncryptPrivateKeyRepeat;
      this.okButton.Text = GuiResources.OkButton;
      this.cancelButton.Text = GuiResources.CancelButton;

      this.okButton.Enabled = Valid;
      this.passphraseTextBox.Enabled = this.encryptCheckBox.Checked;
      this.repeatTextBox.Enabled = this.encryptCheckBox.Checked;
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      if (Valid)
      {
        DialogResult = DialogResult.OK;
        Close();
      }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }

    private bool Valid
    {
      get
      {
        return !this.encryptCheckBox.Checked ||
          (this.passphraseTextBox.Text.Length >= 1 &&
           this.passphraseTextBox.Text == this.repeatTextBox.Text);
      }
    }

    private void passphraseTextBox_TextChanged(object sender, EventArgs e)
    {
      this.okButton.Enabled = Valid;
    }

    private void repeatTextBox_TextChanged(object sender, EventArgs e)
    {
      this.okButton.Enabled = Valid;
    }

    private void EncryptPrivateKeyDialog_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Escape:
          DialogResult = DialogResult.Cancel;
          Close();
          break;
        case Keys.Enter:
          if (Valid)
          {
            DialogResult = DialogResult.OK;
            Close();
          }
          break;
      }
    }

    private void encryptCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.passphraseTextBox.Enabled = this.encryptCheckBox.Checked;
      this.repeatTextBox.Enabled = this.encryptCheckBox.Checked;
      this.okButton.Enabled = Valid;
    }

    public static Tuple<DialogResult, string> ShowSetPassphrase()
    {
      EncryptPrivateKeyDialog dialog = new EncryptPrivateKeyDialog();

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        if (dialog.encryptCheckBox.Checked)
        {
          return new Tuple<DialogResult, string>(DialogResult.OK, dialog.passphraseTextBox.Text);
        }
        else
        {
          return new Tuple<DialogResult, string>(DialogResult.OK, null);
        }
      }
      else
      {
        return new Tuple<DialogResult, string>(DialogResult.Cancel, null);
      }
    }
  }
}
