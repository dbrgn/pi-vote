﻿/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Client
{
  public partial class SmallCertificateControl : UserControl
  {
    private Certificate certificate;

    public DateTime ValidationDate { get; set; }

    public CertificateStorage CertificateStorage { get; set; }

    public Certificate Certificate
    {
      get { return this.certificate; }
      set
      {
        this.certificate = value;

        Display();
      }
    }

    private void Display()
    {
      if (this.certificate != null && CertificateStorage != null)
      {
        this.nameTextBox.Text = this.certificate.FullName;

        if (this.certificate.Valid(CertificateStorage, ValidationDate))
        {
          this.nameTextBox.BackColor = Color.Green;
        }
        else
        {
          this.nameTextBox.BackColor = Color.Red;
        }
      }
    }

    public void SetLanguage()
    {
      this.nameLabel.Text = Resources.CertificateName;
      this.detailButton.Text = Resources.CertificateDetail;

      Certificate = Certificate;
    }

    public SmallCertificateControl()
    {
      InitializeComponent();
      ValidationDate = DateTime.Now;
    }

    private void detailButton_Click(object sender, EventArgs e)
    {
      CertificateForm.ShowCertificate(this.certificate, CertificateStorage, ValidationDate);
    }
  }
}
