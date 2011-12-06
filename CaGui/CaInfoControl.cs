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
using System.IO;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.CaGui
{
  public partial class CaInfoControl : UserControl
  {
    private CACertificate certificate;

    public CaInfoControl()
    {
      InitializeComponent();
    }

    public CACertificate Certificate
    {
      get { return this.certificate; }
      set
      {
        this.certificate = value;

        if (this.certificate != null)
        {
          this.caIdTextBox.Text = this.certificate.Id.ToString();
          this.caNameTextBox.Text = this.certificate.FullName;
        }
        else
        {
          this.caIdTextBox.Text = "N/A";
          this.caNameTextBox.Text = "N/A";
        }
      }
    }

    public string Title
    {
      get { return this.caLabel.Text; }
      set { this.caLabel.Text = value; }
    }
  }
}
