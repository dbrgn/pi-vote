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
  public partial class VoteControl : UserControl
  {
    private List<VoteOptionControl> optionControls;

    public VotingDescriptor Voting { get; set; }

    public event EventHandler ValidChanged;

    public VoteControl()
    {
      InitializeComponent();
    }

    public void Display()
    {
      if (Voting == null)
        throw new InvalidOperationException("Voting must not be null.");

      this.titleLabel.Text = Voting.Title.Text;
      this.questionLabel.Text = Voting.Description.Text;
      this.descriptionButton.Text = Resources.VoteDescriptionButton;

      int space = 10;
      int top = this.questionLabel.Top + this.questionLabel.Height + space;
      this.optionControls = new List<VoteOptionControl>();

      foreach (OptionDescriptor option in Voting.Options)
      {
        VoteOptionControl optionControl = new VoteOptionControl();
        optionControl.Option = option;
        optionControl.MultiOption = Voting.MaxOptions > 1;
        optionControl.Top = top;
        optionControl.Width = Width;
        optionControl.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        optionControl.CheckedChanged += OptionControl_CheckedChanged;
        optionControl.Display();
        Controls.Add(optionControl);
        this.optionControls.Add(optionControl);

        top += optionControl.Height + space;
      }
    }

    private void OptionControl_CheckedChanged(object sender, EventArgs e)
    {
      OnValidChanged();
    }

    private void OnValidChanged()
    {
      if (ValidChanged != null)
      {
        ValidChanged(this, new EventArgs());
      }
    }

    public bool Valid
    {
      get
      {
        return Voting == null ? false : this.optionControls.Where(optionControl => optionControl.Checked).Count() == Voting.MaxOptions;
      }
    }

    private void descriptionButton_Click(object sender, EventArgs e)
    {
      VoteDescriptionForm.ShowDescription(Voting.Title.Text, Voting.Description.Text);
    }

    public IEnumerable<bool> Vota
    {
      get
      {
        return this.optionControls.Select(optionControl => optionControl.Checked);
      }
    }
  }
}
