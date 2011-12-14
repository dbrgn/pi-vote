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
using System.Threading;
using System.Windows.Forms;

namespace Pirate.PiVote.Circle.Status
{
  public partial class TextStatusDialog : Form
  {
    private static TextStatusDialog instance;

    private CircleController controller;

    private Form parentForm;

    private System.Windows.Forms.Timer timer;

    public static void ShowInfo(CircleController controller, Form parentForm)
    {
      if (instance == null)
      {
        instance = new TextStatusDialog();
      }

      instance = new TextStatusDialog();
      instance.Text = Resources.StatusDialogTitle;
      instance.controller = controller;
      instance.parentForm = parentForm;
      instance.parentForm.Enabled = false;
      instance.Show();
    }

    public static void HideInfo()
    {
      if (instance != null)
      {
        instance.parentForm.Enabled = true;
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
      var screenBounds = Screen.PrimaryScreen.Bounds;

      if (screenBounds.Width < Width)
      {
        Width = screenBounds.Width;
      }

      CenterToScreen();
      this.progressBar.MarqueeAnimationSpeed = Environment.OSVersion.Platform == PlatformID.Unix ? 3000 : 100;

      this.timer = new System.Windows.Forms.Timer();
      this.timer.Tick += new EventHandler(Timer_Tick);
      this.timer.Interval = 25;
      this.timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      this.controller.UpdateStatus();
      this.infoLabel.Text = this.controller.Text;
      this.subInfoLabel.Text = this.controller.SubText;
      this.progressBar.Style = this.controller.HasProgress ? ProgressBarStyle.Continuous : ProgressBarStyle.Marquee;
      this.progressBar.Value = Convert.ToInt32(this.controller.Progress * 100d);
    }

    private void TextStatusDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.timer.Stop();
    }
  }
}
