/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
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

    public static void ShowDescription(string text, string description)
    {
      VoteDescriptionForm form = new VoteDescriptionForm();
      form.Text = Resources.VoteDescriptionTitle;
      form.closeButton.Text = Resources.VoteDescriptionCloseButton;
      form.textTextBox.Text = text;
      form.descriptionTextBox.Text = description;
      form.ShowDialog();
    }
  }
}
