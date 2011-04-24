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

namespace Pirate.PiVote.Circle.Vote
{
  public partial class InfoForm : Form
  {
    public InfoForm()
    {
      InitializeComponent();
    }

    public string Description
    {
      get { return this.descriptionLabel.Text; }
      set { this.descriptionLabel.Text = value; }
    }

    public void AdjustHeight()
    {
      Graphics graphics = Graphics.FromHwnd(Handle);
      Height = (int)graphics.MeasureString(Description, this.descriptionLabel.Font, Width).Height + 30;
      graphics.Dispose();
    }
  }
}
