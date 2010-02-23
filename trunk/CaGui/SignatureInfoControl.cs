/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.CaGui
{
  public partial class SignatureInfoControl : UserControl
  {
    public SignatureInfoControl()
    {
      InitializeComponent();
    }

    public void Set(Signature signature, CertificateStorage certificateStorage)
    {
      this.caIdTextBox.Text = signature.SignerId.ToString();
      this.validFromTextBox.Text = signature.ValidFrom.ToString();
      this.validUntilTextBox.Text = signature.ValidUntil.ToString();

      if (certificateStorage.Has(signature.SignerId))
      {
        this.caNameTextBox.Text = ((CACertificate)certificateStorage.Get(signature.SignerId)).FullName;
      }
      else
      {
        this.caNameTextBox.Text = "N/A";
      }
    }

    public string Title
    {
      get { return this.caLabel.Text; }
      set { this.caLabel.Text = value; }
    }
  }
}
