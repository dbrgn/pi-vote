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

namespace Pirate.PiVote.Circle.Config
{
  public partial class ConfigDialog : Form
  {
    public ConfigDialog()
    {
      InitializeComponent();
    }

    public UserConfig Config { get; set; }

    private void ConfigDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();

      Text = Resources.ConfigTitle;
      this.proofCountTab.Text = Resources.ConfigTabProofCount;
      this.okButton.Text = Gui.GuiResources.ButtonOk;
      this.cancelButton.Text = Gui.GuiResources.ButtonCancel;
      this.configProofCountControl1.InitialProofCount = Config.InitialCheckProofCount;
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      Config.InitialCheckProofCount = this.configProofCountControl1.InitialProofCount;
      Config.Save();
      Close();
    }
  }
}
