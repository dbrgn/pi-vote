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
  public partial class VotingControl : UserControl
  {
    private List<QuestionControl> questionControls;

    public event EventHandler VotingChanged;

    public VotingControl()
    {
      InitializeComponent();

      this.questionTabs.SelectedIndexChanged += new EventHandler(questionTabs_SelectedIndexChanged);
    }

    private void questionTabs_SelectedIndexChanged(object sender, EventArgs e)
    {
      OnVotingChanged();
    }

    public bool CanNext
    {
      get { return this.questionTabs.SelectedIndex + 1 < this.questionTabs.TabCount; }
    }

    public bool CanPrevious
    {
      get { return this.questionTabs.SelectedIndex > 0; }
    }

    public void Next()
    {
      if (this.questionTabs.SelectedIndex + 1 < this.questionTabs.TabCount)
      {
        this.questionTabs.SelectedIndex++;
      }
    }

    public void Previous()
    {
      if (this.questionTabs.SelectedIndex > 0)
      {
        this.questionTabs.SelectedIndex--;
      }
    }

    public bool Valid
    {
      get
      {
        if (this.questionControls != null &&
            this.questionControls.Count > 0)
        {
          return this.questionControls.All(questionControl => questionControl.Valid);
        }
        else
        {
          return false;
        }
      }
    }

    public IEnumerable<IEnumerable<bool>> Vota
    {
      get
      {
        if (this.questionControls != null &&
            this.questionControls.Count > 0)
        {
          return this.questionControls.Select(questionControl => questionControl.Vota);
        }
        else
        {
          throw new InvalidOperationException("Not ready.");
        }
      }
    }

    public void Display(VotingDescriptor voting)
    {
      this.questionControls = new List<QuestionControl>();
      this.titleControl.Title = voting.Title.Text;
      this.titleControl.Description = voting.Description.Text;
      this.titleControl.Url = voting.Url.Text;
      this.titleControl.BeginInfo();

      this.questionTabs.TabPages.Clear();
      int number = 1;

      foreach (var question in voting.Questions)
      {
        TabPage tabPage = new TabPage();
        tabPage.Text = string.Format("Question #{0}", number);
        this.questionTabs.TabPages.Add(tabPage);

        QuestionControl questionControl = new QuestionControl();
        tabPage.Controls.Add(questionControl);
        questionControl.Dock = DockStyle.Fill;
        questionControl.Display(question);
        questionControl.ValidChanged += new EventHandler(QuestionControl_ValidChanged);
        this.questionControls.Add(questionControl);

        number++;
      }
    }

    private void QuestionControl_ValidChanged(object sender, EventArgs e)
    {
      OnVotingChanged();
    }

    private void OnVotingChanged()
    {
      if (VotingChanged != null)
      {
        VotingChanged(this, new EventArgs());
      }
    }
  }
}
