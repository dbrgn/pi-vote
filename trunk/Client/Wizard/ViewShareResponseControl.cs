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
using Pirate.PiVote.Gui;

namespace Pirate.PiVote.Client
{
  public partial class ViewShareResponseControl : UserControl
  {
    public VotingParameters Parameters { get; set; }

    public Signed<ShareResponse> SignedShareReponse { get; set; }

    public CertificateStorage CertificateStorage { get; set; }

    public DateTime ValidationDate
    {
      get { return certificateControl.ValidationDate; }
      set { certificateControl.ValidationDate = value; }
    }

    public void Display()
    {
      if (Parameters != null &&
          SignedShareReponse != null &
          CertificateStorage != null)
      {
        Certificate authorityCertificate = SignedShareReponse.Certificate;

        this.certificateControl.CertificateStorage = CertificateStorage;
        this.certificateControl.Certificate = authorityCertificate;

        var shareResponse = SignedShareReponse.Value;

        if (SignedShareReponse.Verify(CertificateStorage, ValidationDate) &&
          SignedShareReponse.Certificate is AuthorityCertificate)
        {
          this.dataTextBox.Text = GuiResources.CertificateValid;
          this.dataTextBox.BackColor = Color.Green;
        }
        else
        {
          this.dataTextBox.Text = GuiResources.CertificateInvalid;
          this.dataTextBox.BackColor = Color.Red;
        }
      }
    }

    public ViewShareResponseControl()
    {
      InitializeComponent();
    }

    public void SetLanguage()
    {
      this.dataLabel.Text = Resources.ViewShareData;

      this.certificateControl.SetLanguage();

      Display();
    }
  }
}
