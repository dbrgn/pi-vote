/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Client
{
  public partial class AddOptionDialog : Form
  {
    public AddOptionDialog()
    {
      InitializeComponent();

      this.okButton.Text = GuiResources.OkButton;
      this.cancelButton.Text = GuiResources.CancelButton;
      this.textLabel.Text = Resources.AddOptionText;
      this.descriptionLabel.Text = Resources.AddOptionDescription;
      this.urlLabel.Text = Resources.AddOptionUrl;
      Text = Resources.AddOptionTitle;

      CenterToScreen();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
      Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }

    private void textTextBox_TextChanged(object sender, EventArgs e)
    {
      this.okButton.Enabled = !this.textTextBox.IsEmpty;
    }

    private void AddOptionDialog_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          if (this.okButton.Enabled)
          {
            DialogResult = DialogResult.OK;
            Close();
          }
          break;
        case Keys.Escape:
          DialogResult = DialogResult.Cancel;
          Close();
          break;
      }
    }

    public MultiLanguageString OptionText { get { return this.textTextBox.Text; } }

    public MultiLanguageString OptionDescription { get { return this.descriptionTextBox.Text; } }

    public MultiLanguageString OptionUrl { get { return this.urlTextBox.Text; } }
  }
}
