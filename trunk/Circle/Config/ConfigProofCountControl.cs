/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pirate.PiVote.Circle.Config
{
  public partial class ConfigProofCountControl : UserControl
  {
    public ConfigProofCountControl()
    {
      InitializeComponent();
    }

    private void ConfigProofCountControl_Load(object sender, EventArgs e)
    {
      this.explainLabel.Text = Resources.ConfigProofCountExplain;
      this.fastLabel.Text = Resources.ConfigProofCountFast;
      this.secureLabel.Text = Resources.ConfigProofCountSecure;
      this.infoLabel.Text = string.Format(Resources.ConfigProofCountInfo, this.initialCheckProofCountTrackBar.Value, Math.Pow(2, 16 * this.initialCheckProofCountTrackBar.Value));
    }

    private void initialCheckProofCountTrackBar_Scroll(object sender, EventArgs e)
    {
      this.infoLabel.Text = string.Format(Resources.ConfigProofCountInfo, this.initialCheckProofCountTrackBar.Value, Math.Pow(2, 16 * this.initialCheckProofCountTrackBar.Value));
    }

    public int InitialProofCount
    {
      get
      {
        return this.initialCheckProofCountTrackBar.Value;
      }
      set
      {
        this.initialCheckProofCountTrackBar.Value = value;
      }
    }
  }
}
