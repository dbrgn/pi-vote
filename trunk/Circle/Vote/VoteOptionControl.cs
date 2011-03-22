/*
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

namespace Pirate.PiVote.Circle
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

    public void Display(bool enable)
    {
      if (Option == null)
        throw new InvalidOperationException("Option must not be null.");

      this.optionCheckBox.Visible = MultiOption;
      this.optionRadioButton.Visible = !MultiOption;

      this.optionCheckBox.Text = Option.Text.Text;
      this.optionRadioButton.Text = Option.Text.Text;

      this.optionCheckBox.Enabled = enable;
      this.optionRadioButton.Enabled = enable;

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
      VoteDescriptionForm.ShowDescription(Option.Text.Text, Option.Description.Text, Option.Url.Text);
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
