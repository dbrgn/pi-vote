using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Client
{
  public partial class DecryptPrivateKeyDialog : Form
  {
    public DecryptPrivateKeyDialog()
    {
      InitializeComponent();
    }

    private void EncryptPrivateKeyDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();

      Text = Resources.EncryptPrivateKeyTitle;
      this.certificateIdLabel.Text = Resources.EncryptPrivateKeyCertificateId;
      this.certificateTypeLabel.Text = Resources.EncryptPrivateKeyCertificateType;
      this.actionLabel.Text = Resources.EncryptPrivateKeyUnlockAction;
      this.passphraseLabel.Text = Resources.EncryptPrivateKeyPassphrase;
      this.okButton.Text = Resources.OkButton;
      this.cancelButton.Text = Resources.CancelButton;

      this.okButton.Enabled = Valid;
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
        return this.passphraseTextBox.Text.Length >= 1;
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

    public static string ShowSetPassphrase(string certificateId, string certificateType, string actionName, string message)
    {
      DecryptPrivateKeyDialog dialog = new DecryptPrivateKeyDialog();
      dialog.certificateIdTextBox.Text = certificateId;
      dialog.certificateTypeTextBox.Text = certificateType;
      dialog.actionTextBox.Text = actionName;

      if (message == null)
      {
        dialog.infoLabel.Text = Resources.EncryptPrivateKeyEncrypted;
      }
      else
      {
        dialog.infoLabel.ForeColor = Color.Red;
        dialog.infoLabel.Text = message;
      }

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        return dialog.passphraseTextBox.Text;
      }
      else
      {
        return null;
      }
    }

    public static bool TryDecryptIfNessecary(Certificate certificate, string actionName)
    {
      if (certificate.PrivateKeyStatus == PrivateKeyStatus.Encrypted)
      {
        bool unlocked = false;
        string message = null;

        while (!unlocked)
        {
          string passphrase = ShowSetPassphrase(certificate.Id.ToString(), certificate.TypeText, actionName, message);

          if (passphrase.IsNullOrEmpty())
            break;

          try
          {
            certificate.Unlock(passphrase);
            unlocked = true;
          }
          catch
          {
            message = Resources.EncryptPrivateKeyWrongPassphrase;
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
