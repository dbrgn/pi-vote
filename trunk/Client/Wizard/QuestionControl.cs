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
    private Dictionary<OptionDescriptor, VoteOptionControl> optionControls;

    public QuestionDescriptor Question { get; set; }

    public event EventHandler ValidChanged;

    public QuestionControl()
    {
      InitializeComponent();
    }

    public void Display(bool enable)
    {
      if (Question == null)
        throw new InvalidOperationException("Question must not be null.");

      this.questionLabel.Text = Question.Text.Text;
      this.descriptionButton.Text = Resources.VoteDescriptionButton;

      if (Question.MaxOptions > 1)
      {
        this.maxOptionsLabel.Text = string.Format(Resources.VoteMaxOptions, Question.MaxOptions);
      }
      else
      {
        this.maxOptionsLabel.Text = Resources.VoteSingleOption;
      }

      int space = 2;
      int top = this.maxOptionsLabel.Top + this.maxOptionsLabel.Height + space;
      this.optionControls = new Dictionary<OptionDescriptor, VoteOptionControl>();

      foreach (OptionDescriptor option in Question.Options)
      {
        if (option.Text.Text == Resources.OptionAbstainSpecial)
        {
          this.optionControls.Add(option, null);
        }
        else
        {
          VoteOptionControl optionControl = new VoteOptionControl();
          optionControl.Option = option;
          optionControl.MultiOption = Question.MaxOptions > 1;
          optionControl.Top = top;
          optionControl.Width = Width;
          optionControl.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
          optionControl.CheckedChanged += OptionControl_CheckedChanged;
          optionControl.Display(enable);
          Controls.Add(optionControl);
          this.optionControls.Add(option, optionControl);

          top += optionControl.Height + space;
        }
      }

      this.Height = top + this.optionControls.Values.First().Height;
    }

    private void OptionControl_CheckedChanged(object sender, EventArgs e)
    {
      if (Question.MaxOptions < 2)
      {
        VoteOptionControl activeControl = (VoteOptionControl)sender;

        if (activeControl.Checked)
        {
          foreach (VoteOptionControl optionControl in this.optionControls.Values)
          {
            if (optionControl != activeControl)
            {
              optionControl.Checked = false;
            }
          }
        }
      }

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
        if (Question == null)
        {
          return false;
        }
        else if (Question.MaxOptions > 1)
        {
          return this.optionControls.Values.Count(optionControl => optionControl != null && optionControl.Checked) <= Question.MaxOptions;
        }
        else 
        {
          return this.optionControls.Values.Count(optionControl => optionControl != null && optionControl.Checked) == Question.MaxOptions;
        }
      }
    }

    public IEnumerable<bool> Vota
    {
      get
      {
        List<bool> vota = new List<bool>();
        int abstainCount = Question.MaxOptions - this.optionControls.Values.Count(optionControl => optionControl != null && optionControl.Checked);

        foreach (OptionDescriptor option in Question.Options)
        {
          if (this.optionControls[option] != null)
          {
            vota.Add(this.optionControls[option].Checked);
          }
          else if (abstainCount > 0)
          {
            abstainCount--;
            vota.Add(true);
          }
          else
          {
            vota.Add(false);
          }
        }

        return vota.ToArray();
      }
    }

    private void DescriptionButton_Click(object sender, EventArgs e)
    {
      VoteDescriptionForm.ShowDescription(Question.Text.Text, Question.Description.Text, Question.Url.Text);
    }
  }
}
