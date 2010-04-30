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
  public partial class AddOptionDialog : Form
  {
    public AddOptionDialog()
    {
      InitializeComponent();

      this.okButton.Text = Resources.OkButton;
      this.cancelButton.Text = Resources.CancelButton;
      this.textLabel.Text = Resources.AddOptionText;
      this.descriptionLabel.Text = Resources.AddOptionDescription;
      Text = Resources.AddOptionTitle;
    }

    private void AddOptionDialog_Load(object sender, EventArgs e)
    {

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
  }
}
