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
using Emil.GMP;

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
      //BigInt probability = ((BigInt)2).Power(16 * this.initialCheckProofCountTrackBar.Value);
      //this.infoLabel.Text = string.Format(Resources.ConfigProofCountInfo, this.initialCheckProofCountTrackBar.Value, probability);
    }

    private void initialCheckProofCountTrackBar_Scroll(object sender, EventArgs e)
    {
      //BigInt probability = ((BigInt)2).Power(16 * this.initialCheckProofCountTrackBar.Value);
      //this.infoLabel.Text = string.Format(Resources.ConfigProofCountInfo, this.initialCheckProofCountTrackBar.Value, probability);
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
        //BigInt probability = ((BigInt)2).Power(16 * this.initialCheckProofCountTrackBar.Value);
        //this.infoLabel.Text = string.Format(Resources.ConfigProofCountInfo, this.initialCheckProofCountTrackBar.Value, probability);
      }
    }
  }
}
