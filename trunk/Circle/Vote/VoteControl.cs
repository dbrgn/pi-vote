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
  public partial class VoteControl : UserControl
  {
    private List<QuestionControl> questionControls;

    public VotingDescriptor Voting { get; set; }

    public event EventHandler ValidChanged;

    public VoteControl()
    {
      InitializeComponent();
    }

    public void Display(bool enable)
    {
      if (Voting == null)
        throw new InvalidOperationException("Voting must not be null.");

      this.titleLabel.Text = Voting.Title.Text;
      this.descriptionButton.Text = Resources.VoteDescriptionButton;

      int space = 2;
      int top = this.descriptionButton.Top + this.descriptionButton.Height + space;
      this.questionControls = new List<QuestionControl>();

      foreach (QuestionDescriptor question in Voting.Questions)
      {
        QuestionControl questionControl = new QuestionControl();
        questionControl.Question = question;
        questionControl.Top = top;
        questionControl.Width = this.containerPanel.ClientSize.Width;
        questionControl.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        questionControl.ValidChanged += new EventHandler(questionControl_ValidChanged);
        questionControl.Display(enable);
        this.containerPanel.Controls.Add(questionControl);
        this.questionControls.Add(questionControl);

        top += questionControl.Height + space;
      }
    }

    private void questionControl_ValidChanged(object sender, EventArgs e)
    {
      OnValidChanged();
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
        return Voting == null ? false : this.questionControls.All(questionControl => questionControl.Valid);
      }
    }

    private void descriptionButton_Click(object sender, EventArgs e)
    {
      VoteDescriptionForm.ShowDescription(Voting.Title.Text, Voting.Description.Text, Voting.Url.Text);
    }

    public IEnumerable<IEnumerable<bool>> Vota
    {
      get
      {
        return this.questionControls.Select(questionControl => questionControl.Vota);
      }
    }
  }
}
