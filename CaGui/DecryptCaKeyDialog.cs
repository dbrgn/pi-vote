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
  public partial class DecryptCaKeyDialog : Form
  {
    public DecryptCaKeyDialog()
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
        this.passphraseTextBox.Text.Length >= 12;
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

    public static bool TryUnlock(Certificate certificate)
    {
      if (certificate.PrivateKeyStatus == PrivateKeyStatus.Encrypted)
      {
        bool unlocked = false;

        while (!unlocked)
        {
          DecryptCaKeyDialog dialog = new DecryptCaKeyDialog();
          dialog.caIdTextBox.Text = certificate.Id.ToString();
          dialog.caNameTextBox.Text = certificate.FullName;

          if (dialog.ShowDialog() == DialogResult.OK)
          {
            try
            {
              certificate.Unlock(dialog.passphraseTextBox.Text);
              unlocked = true;
            }
            catch
            { }
          }
          else
          {
            break;
          }
        }

        return unlocked;
      }
      else if (certificate.PrivateKeyStatus == PrivateKeyStatus.Unavailable)
      {
        return false;
      }
      else
      {
        return true;
      }
    }
  }
}
