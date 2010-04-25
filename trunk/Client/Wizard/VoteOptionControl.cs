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
  public partial class VoteOptionControl : UserControl
  {
    public OptionDescriptor Option { get; set; }
    public bool MultiOption { get; set; }

    public event EventHandler CheckedChanged;

    public VoteOptionControl()
    {
      InitializeComponent();
    }

    public void Display()
    {
      if (Option == null)
        throw new InvalidOperationException("Option must not be null.");

      this.optionCheckBox.Visible = MultiOption;
      this.optionRadioButton.Visible = !MultiOption;

      this.optionCheckBox.Text = Option.Text.Text;
      this.optionRadioButton.Text = Option.Text.Text;

      this.descriptionButton.Text = Resources.VoteDescriptionButton;
    }

    public bool Checked
    {
      get 
      {
        return MultiOption ? this.optionCheckBox.Checked : this.optionRadioButton.Checked;
      }
      set 
      {
        this.optionCheckBox.Checked = value;
        this.optionRadioButton.Checked = value;
      }
    }

    private void OnCheckedChanged()
    {
      if (CheckedChanged != null)
      {
        CheckedChanged(this, new EventArgs());
      }
    }

    private void descriptionButton_Click(object sender, EventArgs e)
    {
      VoteDescriptionForm.ShowDescription(Option.Text.Text, Option.Description.Text);
    }

    private void optionRadioButton_CheckedChanged(object sender, EventArgs e)
    {
      OnCheckedChanged();
    }

    private void optionCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      OnCheckedChanged();
    }
  }
}
