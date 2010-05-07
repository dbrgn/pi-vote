﻿/*
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
    public VotingDescriptor VotingDescriptor { get; set; }
    public IEnumerable<IEnumerable<bool>> Vota;

    private bool done = false;
    private bool run = false;
    private Exception exception;
    private Signed<VoteReceipt> voteReceipt;

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

    public override bool CancelIsDone
    {
      get { return this.done; }
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
        Thread.Sleep(10);
      }

      Status.UpdateProgress();

      if (this.exception == null)
      {
        this.voteReceipt.Save(
          Path.Combine(Status.DataPath,
          string.Format("{0}@{1}.pi-receipt",
          Status.Certificate.Id.ToString(), VotingDescriptor.Id.ToString())));

        Status.SetMessage(Resources.VoteCast, MessageType.Success);
      }
      else
      {
        Status.SetMessage(exception.Message, MessageType.Error);
      }

      this.done = true;
      OnUpdateWizard();
    }

    public void VoteCompleted(Signed<VoteReceipt> voteReceipt, Exception exception)
    {
      this.voteReceipt = voteReceipt;
      this.exception = exception;
      this.run = false;
    }

    private void VoteCompleteItem_Load(object sender, EventArgs e)
    {

    }
  }
}
