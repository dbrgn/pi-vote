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
  public partial class CaPropertiesDialog : Form
  {
    public CaPropertiesDialog()
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
      }
    }

    public void Set(CACertificate certificate, CertificateStorage certificateStorage)
    {
      this.caInfo.Certificate = certificate;

      int index = 0;
      Height = this.caPanel.Height + this.okPanel.Height + 30;

      foreach (Signature signature in certificate.Signatures)
      {
        SignatureInfoControl signatureInfo = new SignatureInfoControl();
        signatureInfo.Title = string.Format("Parent #{0} Authority", index);
        signatureInfo.Set(signature, certificateStorage);
        signatureInfo.Left = this.caInfo.Left;
        signatureInfo.Top = index * (signatureInfo.Height + 10);
        this.parentsPanel.Controls.Add(signatureInfo);
        Height = (index + 1) * (signatureInfo.Height + 10) + this.caPanel.Height + this.okPanel.Height + 30;
        index++;
      }
    }
  }
}
