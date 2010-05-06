/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Client
{
  public partial class Progress : UserControl
  {
    public WizardStatus Status { get; set; }

    public Progress()
    {
      InitializeComponent();
    }

    private void Progress_Load(object sender, EventArgs e)
    {
      this.progessLabel.Text = string.Empty;
      this.subProgressLabel.Text = string.Empty;
    }

    public void Set(string message, double progress)
    {
      this.progessLabel.Visible = true;
      this.progressBar.Visible = true;
      this.subProgressLabel.Visible = false;
      this.subProgessBar.Visible = false;
      this.progessLabel.Text = message;
      this.progressBar.Value = Convert.ToInt32(100d * progress);
    }

    public void UpdateProgress()
    {
      if (Status != null && Status.VotingClient != null)
      {
        VotingClient.Operation operation = Status.VotingClient.CurrentOperation;
        if (operation != null)
        {
          if (operation.Text.IsNullOrEmpty())
          {
            this.progessLabel.Visible = false;
            this.progressBar.Visible = false;
          }
          else
          {
            this.progessLabel.Visible = true;
            this.progressBar.Visible = true;
            this.progessLabel.Text = operation.Text;
            int progressValue = Convert.ToInt32(100d * operation.Progress);
            if (this.progressBar.Value != progressValue) this.progressBar.Value = progressValue;
          }

          if (operation.Text.IsNullOrEmpty())
          {
            this.subProgressLabel.Visible = false;
            this.subProgessBar.Visible = false;
          }
          else
          {
            this.subProgressLabel.Visible = true;
            this.subProgessBar.Visible = true;
            this.subProgressLabel.Text = operation.SubText;
            int progressValue = Convert.ToInt32(100d * operation.SubProgress);
            if (this.subProgessBar.Value != progressValue) this.subProgessBar.Value = progressValue;
          }
        }
        else
        {
          this.progessLabel.Visible = false;
          this.progressBar.Visible = false;
          this.subProgressLabel.Visible = false;
          this.subProgessBar.Visible = false;
        }
      }
      else
      {
        this.progessLabel.Visible = false;
        this.progressBar.Visible = false;
        this.subProgressLabel.Visible = false;
        this.subProgessBar.Visible = false;
      }
    }
  }
}
