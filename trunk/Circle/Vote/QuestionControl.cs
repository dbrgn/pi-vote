/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Circle.Vote
{
  public partial class QuestionControl : UserControl
  {
    private const int Space = 16;
    private Dictionary<int, RadioButton> singleOptionControls;
    private Dictionary<int, CheckBox> multiOptionControls;
    private QuestionDescriptor question;

    public event EventHandler ValidChanged;

    public QuestionControl()
    {
      InitializeComponent();
    }

    public void Display(QuestionDescriptor question)
    {
      this.question = question;

      this.textControl.Title = question.Text.Text;
      this.textControl.Description = question.Description.Text;
      this.textControl.Url = question.Url.Text;
      this.textControl.FixLayout();
      this.textControl.BeginInfo();

      if (question.MaxOptions == 1)
      {
        this.maxOptionLabel.Text = "You must select one option.";
        this.singleOptionControls = new Dictionary<int, RadioButton>();
        int index = 0;
        int top = Space;

        foreach (OptionDescriptor option in question.Options)
        {
          InfoControl optionInfo = new InfoControl();
          optionInfo.Title = string.Empty;
          optionInfo.Description = option.Description.Text;
          optionInfo.Url = option.Url.Text;
          optionInfo.Left = Space;
          optionInfo.Top = top;
          optionInfo.Width = this.optionsPanel.Width - (2 * Space);
          optionInfo.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
          this.optionsPanel.Controls.Add(optionInfo);
          optionInfo.BeginInfo();

          RadioButton optionControl = new RadioButton();
          optionControl.Text = option.Text.Text;
          optionControl.Left = Space;
          optionControl.Top = top;
          this.optionsPanel.Controls.Add(optionControl);
          optionControl.BringToFront();
          optionControl.CheckedChanged += new EventHandler(OptionControl_CheckedChanged);
          this.singleOptionControls.Add(index, optionControl);

          top += optionControl.Height + Space;
          index++;
        }
      }
      else
      {
        this.maxOptionLabel.Text = string.Format("You can select up to {0} options", question.MaxOptions);
        this.multiOptionControls = new Dictionary<int, CheckBox>();
        int index = 0;
        int top = Space;

        foreach (OptionDescriptor option in question.Options)
        {
          if (option.Text.Text != Resources.OptionAbstainSpecial)
          {
            InfoControl optionInfo = new InfoControl();
            optionInfo.Title = string.Empty;
            optionInfo.Description = option.Description.Text;
            optionInfo.Url = option.Url.Text;
            optionInfo.Left = Space;
            optionInfo.Top = top;
            optionInfo.Width = this.optionsPanel.Width - (2 * Space);
            optionInfo.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
            this.optionsPanel.Controls.Add(optionInfo);
            optionInfo.BeginInfo();

            CheckBox optionControl = new CheckBox();
            optionControl.Text = option.Text.Text;
            optionControl.Left = Space;
            optionControl.Top = top;
            this.optionsPanel.Controls.Add(optionControl);
            optionControl.BringToFront();
            optionControl.CheckedChanged += new EventHandler(OptionControl_CheckedChanged);
            this.multiOptionControls.Add(index, optionControl);

            top += optionControl.Height + Space;
          }

          index++;
        }
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

    public IEnumerable<bool> Vota
    {
      get
      {
        bool[] vota = new bool[this.question.Options.Count()];
        int index = 0;
        int selectedOptionCount = 0;

        foreach (OptionDescriptor option in this.question.Options)
        {
          if (this.singleOptionControls != null)
          {
            if (this.singleOptionControls.ContainsKey(index))
            {
              vota[index] = this.singleOptionControls[index].Checked;
              selectedOptionCount += vota[index] ? 1 : 0;
            }
          }
          else if (this.multiOptionControls != null)
          {
            if (this.multiOptionControls.ContainsKey(index))
            {
              vota[index] = this.multiOptionControls[index].Checked;
              selectedOptionCount += vota[index] ? 1 : 0;
            }
          }
          else
          {
            throw new InvalidOperationException("Not ready.");
          }

          index++;
        }

        index = 0;

        foreach (OptionDescriptor option in this.question.Options)
        {
          if (selectedOptionCount < this.question.MaxOptions &&
              option.Text.Text == Resources.OptionAbstainSpecial)
          {
            vota[index] = true;
            selectedOptionCount++;
          }

          index++;
        }

        return vota;
      }
    }

    public bool Valid
    {
      get
      {
        if (this.singleOptionControls != null)
        {
          int selectedOptionCount = this.singleOptionControls.Values.Sum(optionControl => optionControl.Checked ? 1 : 0);
          return selectedOptionCount == 1;
        }
        else if (this.multiOptionControls != null)
        {
          int selectedOptionCount = this.multiOptionControls.Values.Sum(optionControl => optionControl.Checked ? 1 : 0);
          return selectedOptionCount <= this.question.MaxOptions;
        }
        else
        {
          return false;
        }
      }
    }
  }
}
