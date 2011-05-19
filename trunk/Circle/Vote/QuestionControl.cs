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
    private const int BoxWidth = 16;
    private const int BoxTop = 2;
    private Dictionary<InfoControl, int> optionControls;
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
      this.textControl.BeginInfo();

      int upperSpace = this.optionsPanel.Top - this.textControl.Top - this.textControl.Height;
      int lowerSpace = Height - this.optionsPanel.Top - this.optionsPanel.Height;
      this.textControl.Height = this.textControl.RequiredHeight;
      this.optionsPanel.Top = this.textControl.Top + this.textControl.Height + upperSpace;
      this.optionsPanel.Height = Height - this.optionsPanel.Top - lowerSpace;

      this.optionControls = new Dictionary<InfoControl, int>();

      if (question.MaxOptions == 1)
      {
        this.maxOptionLabel.Text = Resources.VotingDialogMaxOptionSingle;
        this.singleOptionControls = new Dictionary<int, RadioButton>();
        int index = 0;
        int top = Space;

        foreach (OptionDescriptor option in question.Options)
        {
          AddOptionInfo(index, top, option);

          RadioButton optionControl = new RadioButton();
          optionControl.Text = string.Empty;
          optionControl.Left = Space;
          optionControl.Top = top + BoxTop;
          optionControl.Width = BoxWidth;
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
        this.maxOptionLabel.Text = string.Format(Resources.VotingDialogMaxOptionMulti, question.MaxOptions);
        this.multiOptionControls = new Dictionary<int, CheckBox>();
        int index = 0;
        int top = Space;

        foreach (OptionDescriptor option in question.Options)
        {
          if (!option.IsAbstentionSpecial)
          {
            AddOptionInfo(index, top, option);

            CheckBox optionControl = new CheckBox();
            optionControl.Text = string.Empty;
            optionControl.Top = top + BoxTop;
            optionControl.Width = BoxWidth;
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

    private void AddOptionInfo(int index, int top, OptionDescriptor option)
    {
      InfoControl optionInfo = new InfoControl();
      optionInfo.Title = option.Text.Text;
      optionInfo.Description = option.Description.Text;
      optionInfo.Url = option.Url.Text;
      optionInfo.Left = Space + BoxWidth;
      optionInfo.Top = top;
      optionInfo.Width = this.optionsPanel.Width - (2 * Space);
      optionInfo.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
      optionInfo.Click += new EventHandler(optionInfo_Click);
      optionInfo.InfoFont = Font;
      this.optionsPanel.Controls.Add(optionInfo);
      this.optionControls.Add(optionInfo, index);
      optionInfo.BeginInfo();
    }

    private void optionInfo_Click(object sender, EventArgs e)
    {
      if (this.singleOptionControls != null)
      {
        this.singleOptionControls[this.optionControls[(InfoControl)sender]].Checked = true;
      }
      else if (this.multiOptionControls != null)
      {
        this.multiOptionControls[this.optionControls[(InfoControl)sender]].Checked = true;
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
              option.IsAbstentionSpecial)
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
