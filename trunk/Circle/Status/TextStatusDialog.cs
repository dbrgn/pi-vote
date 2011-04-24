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

namespace Pirate.PiVote.Circle.Status
{
  public partial class TextStatusDialog : Form
  {
    private static TextStatusDialog instance;

    private CircleController controller;

    private Timer timer;

    public static void ShowInfo(CircleController controller)
    {
      if (instance == null)
      {
        instance = new TextStatusDialog();
      }

      instance.Show();
      instance.controller = controller;
    }

    public static void HideInfo()
    {
      if (instance != null)
      {
        instance.Hide();
      }
    }

    public TextStatusDialog()
    {
      InitializeComponent();
    }

    public string Info
    {
      get { return this.infoLabel.Text; }
      set { this.infoLabel.Text = value; }
    }

    private void TextStatusDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();

      this.timer = new Timer();
      this.timer.Tick += new EventHandler(Timer_Tick);
      this.timer.Interval = 25;
      this.timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      this.infoLabel.Text = this.controller.Text;
      this.subInfoLabel.Text = this.controller.SubText;
      this.progressBar.Value = Convert.ToInt32(this.controller.Progress);
      this.subProgressBar.Value = Convert.ToInt32(this.controller.SubProgress);
    }

    private void TextStatusDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.timer.Stop();
    }
  }
}
