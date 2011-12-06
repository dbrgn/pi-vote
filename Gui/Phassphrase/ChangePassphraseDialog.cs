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

namespace Pirate.PiVote.Gui
{
  public partial class ChangePassphraseDialog : Form
  {
    private int minLength;

    public ChangePassphraseDialog()
    {
      InitializeComponent();
    }

    private void ChangePassphraseDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();

      Text = GuiResources.EncryptPrivateKeyTitle;
      this.oldPassphraseLabel.Text = GuiResources.ChangePassphraseOldPassphrase;
      this.encryptCheckBox.Text = GuiResources.EncryptPrivateKeyChoose;
      this.passphraseLabel.Text = GuiResources.ChangePassphraseNewPassphrase;
      this.repeatLabel.Text = GuiResources.EncryptPrivateKeyRepeat;
      this.okButton.Text = GuiResources.ButtonOk;
      this.cancelButton.Text = GuiResources.ButtonCancel;

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
        return 
          this.oldPassphraseTextBox.Text.Length >= 1 &&
          !this.encryptCheckBox.Checked ||
          (this.passphraseTextBox.Text.Length >= this.minLength &&
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

    public static Tuple<DialogResult, string, string> ShowChangePassphrase(Certificate certificate, string message)
    {
      return ShowChangePassphrase(certificate, message, 0);
    }

    public static Tuple<DialogResult, string, string> ShowChangePassphrase(Certificate certificate, string message, int minLength)
    {
      ChangePassphraseDialog dialog = new ChangePassphraseDialog();

      if (minLength > 0)
      {
        dialog.encryptCheckBox.Checked = true;
        dialog.encryptCheckBox.Enabled = false;
        dialog.minLength = minLength;
      }
      else
      {
        dialog.minLength = 1;
      }

      dialog.certificateIdTextBox.Text = certificate.Id.ToString();
      dialog.certificateTypeTextBox.Text = certificate.TypeText;

      if (message.IsNullOrEmpty())
      {
        dialog.messageLabel.Text = GuiResources.ChangePassphraseMessageChange;
      }
      else
      {
        dialog.messageLabel.ForeColor = Color.Red;
        dialog.messageLabel.Text = message;
      }

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        if (dialog.encryptCheckBox.Checked)
        {
          return new Tuple<DialogResult, string, string>(DialogResult.OK, dialog.oldPassphraseTextBox.Text, dialog.passphraseTextBox.Text);
        }
        else
        {
          return new Tuple<DialogResult, string, string>(DialogResult.OK, dialog.oldPassphraseTextBox.Text, null);
        }
      }
      else
      {
        return new Tuple<DialogResult, string, string>(DialogResult.Cancel, null, null);
      }
    }

    private void oldPassphraseTextBox_TextChanged(object sender, EventArgs e)
    {
      this.okButton.Enabled = Valid;
    }
  }
}
