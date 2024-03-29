﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Client
{
  public partial class VoteCompleteItem : WizardItem
  {
    public VotingDescriptor VotingDescriptor { get; set; }
    public VotingMaterial VotingMaterial { get; set; }
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
      return new ListVotingsItem();
    }

    public override WizardItem Previous()
    {
      return null;
    }

    public override WizardItem Cancel()
    {
      return null;
    }

    public override bool CanNext
    {
      get { return this.done; }
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

      if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(Status.Certificate, GuiResources.UnlockActionVote))
      {
        Status.VotingClient.ActivateVoter();
        Status.VotingClient.Vote(VotingMaterial, (VoterCertificate)Status.Certificate, Vota, VoteCompleted);

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
            Files.VoteReceiptFileName(Status.Certificate.Id, VotingDescriptor.Id)));

          Status.SetMessage(Resources.VoteCast, MessageType.Success);
        }
        else
        {
          Status.SetMessage(exception.Message, MessageType.Error);
        }

        Status.Certificate.Lock();
      }
      else
      {
        Status.SetMessage(Resources.VoteCanceled, MessageType.Info);
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

    public override void UpdateLanguage()
    {
      this.pictureBox.Image = Resources.VoteCompleteItem;
    }
  }
}
