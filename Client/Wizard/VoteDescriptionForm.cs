﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Client
{
  public partial class VoteDescriptionForm : Form
  {
    public VoteDescriptionForm()
    {
      InitializeComponent();
      CenterToScreen();
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
      Close();
    }

    public static void ShowDescription(string text, string description, string url)
    {
      VoteDescriptionForm form = new VoteDescriptionForm();
      form.Text = Resources.VoteDescriptionTitle;
      form.closeButton.Text = Resources.VoteDescriptionCloseButton;
      form.textTextBox.Text = text;
      form.descriptionTextBox.Text = description;
      form.urlLink.Text = url;
      form.ShowDialog();
    }

    private void urlLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start(this.urlLink.Text);
    }
  }
}
