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
  public partial class VoteCompleteItem : WizardItem
  {
    public VotingClient.VotingDescriptor VotingDescriptor { get; set; }
    public List<bool> Vota;

    private bool run;
    private Exception exception;

    public VoteCompleteItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      return null;
    }

    public override WizardItem Previous()
    {
      return null;
    }

    public override WizardItem Cancel()
    {
      return null;
    }

    public override bool CanCancel
    {
      get { return !this.run; }
    }

    public override void Begin()
    {
      this.run = true;
      OnUpdateWizard();

      Status.VotingClient.ActivateVoter((VoterCertificate)Status.Certificate);
      Status.VotingClient.Vote(VotingDescriptor.Id, Vota, VoteCompleted);

      while (this.run)
      {
        Status.UpdateProgress();
        Application.DoEvents();
        Thread.Sleep(1);
      }

      Status.UpdateProgress();

      if (exception == null)
      {
        Status.SetMessage("Your vote is now cast.", MessageType.Success);
      }
      else
      {
        Status.SetMessage(exception.Message, MessageType.Error);
      }

      OnUpdateWizard();
    }

    public void VoteCompleted(Exception exception)
    {
      this.exception = exception;
      this.run = false;
    }

    private void VoteCompleteItem_Load(object sender, EventArgs e)
    {

    }
  }
}
