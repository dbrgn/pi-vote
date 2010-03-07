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
  public class WizardStatus
  {
    public CertificateStorage CertificateStorage { get; set; }

    public Certificate Certificate { get; set; }

    public string CertificateFileName { get; set; }

    public VotingClient VotingClient { get; set; }

    private Message message;

    private Progress progress;

    public WizardStatus(Message message, Progress progress)
    {
      this.message = message;
      this.progress = progress;
      this.progress.Status = this;
    }

    public void SetMessage(string message, MessageType type)
    {
      this.message.Visible = true;
      this.progress.Visible = false;
      this.message.Set(message, type);
    }

    public void UpdateProgress()
    {
      this.message.Visible = false;
      this.progress.Visible = true;
      this.progress.Refresh();
    }

    public void SetProgress(string message, double progress)
    {
      this.message.Visible = false;
      this.progress.Visible = true;
      this.progress.Set(message, progress);
    }
  }
}
