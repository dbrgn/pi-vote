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

namespace Pirate.PiVote.Client
{
  public partial class CertificateForm : Form
  {
    public CertificateForm()
    {
      InitializeComponent();

      CenterToScreen();
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void SetLanguage()
    {
      Text = Resources.CertificateFormTitle;
      this.closeButton.Text = Resources.CertificateFormCloseButton;
      this.certificateControl.SetLanguage();
    }

    public static void ShowCertificate(Certificate certificate, CertificateStorage certificateStorage, DateTime validationDate)
    {
      CertificateForm form = new CertificateForm();
      form.SetLanguage();
      form.certificateControl.ValidationDate = validationDate;
      form.certificateControl.CertificateStorage = certificateStorage;
      form.certificateControl.Certificate = certificate;
      form.ShowDialog();
    }
  }
}
