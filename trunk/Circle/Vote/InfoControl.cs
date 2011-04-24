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

namespace Pirate.PiVote.Circle.Vote
{
  public partial class InfoControl : UserControl
  {
    private InfoForm infoForm;
    private bool infoFormShown;
    private string url;
    private Timer timer;

    public InfoControl()
    {
      InitializeComponent();
    }

    [Browsable(true)]
    public string Title
    {
      get { return this.titleBox.Text; }
      set { this.titleBox.Text = value; }}

    [Browsable(true)]
    public string Description { get; set; }

    [Browsable(true)]
    public string Url
    {
      get { return this.url; }
      set
      {
        this.url = value;
        this.webBox.Enabled = !this.url.IsNullOrEmpty();
      }
    }

    [Browsable(true)]
    public Font InfoFont
    {
      get { return this.titleBox.Font; }
      set
      {
        this.titleBox.Font = value;
        this.webBox.Font = value;
      }
    }

    public void BeginInfo()
    {
      this.infoForm = new InfoForm();
      this.infoForm.Description = Description;
      this.infoForm.TopMost = true;

      this.timer = new Timer();
      this.timer.Interval = 25;
      this.timer.Tick += new EventHandler(Timer_Tick);
      this.timer.Start();
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      Point zero = PointToScreen(new Point(0, 0));
      Rectangle desktopBounds = new Rectangle(zero, new Size(Width, Height));

      if (Visible &&
        FindForm().ContainsFocus &&
        desktopBounds.Contains(Cursor.Position) &&
        !this.infoForm.Description.IsNullOrEmpty())
      {
        if (!this.infoFormShown)
        {
          this.infoForm.Show();
          FindForm().Focus();
          this.infoForm.Location = PointToScreen(new Point(0, Height));
          this.infoForm.Width = Width;
          this.infoForm.AdjustHeight();
          this.infoFormShown = true;
        }
      }
      else
      {
        this.infoForm.Hide();
        this.infoFormShown = false;
      }
    }

    private void webBox_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      if (!this.url.IsNullOrEmpty())
      {
        System.Diagnostics.Process.Start(this.url);
      }
    }

    public void FixLayout()
    {
      this.titleBox.Width -= 100;
      this.webBox.Left -= 100;
    }
  }
}
