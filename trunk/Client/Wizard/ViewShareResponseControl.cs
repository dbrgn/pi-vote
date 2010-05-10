/*
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

        if (SignedShareReponse.Verify(CertificateStorage, ValidationDate))
        {
          this.dataTextBox.Text = Resources.CertificateValid;
          this.dataTextBox.BackColor = Color.Green;
        }
        else
        {
          this.dataTextBox.Text = Resources.CertificateInvalid;
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
