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
  public partial class QuestionControl : UserControl
  {
    private List<VoteOptionControl> optionControls;

    public QuestionDescriptor Question { get; set; }

    public event EventHandler ValidChanged;

    public QuestionControl()
    {
      InitializeComponent();
    }

    public void Display()
    {
      if (Question == null)
        throw new InvalidOperationException("Question must not be null.");

      this.questionLabel.Text = Question.Question.Text;
      this.descriptionButton.Text = Resources.VoteDescriptionButton;

      int space = 10;
      int top = this.questionLabel.Top + this.questionLabel.Height + space;
      this.optionControls = new List<VoteOptionControl>();

      foreach (OptionDescriptor option in Question.Options)
      {
        VoteOptionControl optionControl = new VoteOptionControl();
        optionControl.Option = option;
        optionControl.MultiOption = Question.MaxOptions > 1;
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
        return Question == null ? false : this.optionControls.Where(optionControl => optionControl.Checked).Count() == Question.MaxOptions;
      }
    }

    private void descriptionButton_Click(object sender, EventArgs e)
    {
    }

    public IEnumerable<bool> Vota
    {
      get
      {
        return this.optionControls.Select(optionControl => optionControl.Checked);
      }
    }

    private void descriptionButton_Click_1(object sender, EventArgs e)
    {
      VoteDescriptionForm.ShowDescription(Question.Question.Text, string.Empty);
    }
  }
}
