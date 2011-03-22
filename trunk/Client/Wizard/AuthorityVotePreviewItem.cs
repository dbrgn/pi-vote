/*
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
  public partial class AuthorityVotePreviewItem : WizardItem
  {
    private bool run = false;
    private Exception exception;
    private bool accept;
    private Signed<BadShareProof> signedBadShareProof;

    public VotingDescriptor VotingDescriptor { get; set; }

    public AuthorityVotePreviewItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      if (this.signedBadShareProof != null)
      {
        var badShareProofItem = new BadShareProofItem();
        badShareProofItem.IsAuthority = true;
        badShareProofItem.SignedBadShareProof = this.signedBadShareProof;
        return badShareProofItem;
      }
      else
      {
        return new AuthorityListVotingsItem();
      }
    }

    private void Execute()
    {
      string fileName = string.Format("{0}@{1}.pi-auth", Status.Certificate.Id.ToString(), VotingDescriptor.Id.ToString());
      string filePath = Path.Combine(Status.DataPath, fileName);

      if (File.Exists(filePath))
      {
        this.run = true;
        OnUpdateWizard();

        if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(Status.Certificate, Resources.AuthorityCheckSharesUnlockAction))
        {
          Status.VotingClient.CheckShares(VotingDescriptor.Id, (AuthorityCertificate)Status.Certificate, filePath, CheckSharesCompleteCallBack);

          while (this.run)
          {
            Status.UpdateProgress();
            Thread.Sleep(10);
          }

          Status.UpdateProgress();

          if (this.exception == null)
          {
            if (this.accept)
            {
              Status.SetMessage(Resources.AuthorityCheckSharesAccept, MessageType.Success);
            }
            else
            {
              Status.SetMessage(Resources.AuthorityCheckSharesDecline, MessageType.Error);
            }
          }
          else
          {
            Status.SetMessage(this.exception.Message, MessageType.Error);
          }

          Status.Certificate.Lock();
        }
        else
        {
          Status.SetMessage(Resources.AuthorityDecipherCanceled, MessageType.Info);
        }
      }
      else
      {
        Status.SetMessage(Resources.CreateVotingAuthFileMissing, MessageType.Error);
      }

      this.run = false;
      OnUpdateWizard();
    }

    private void CheckSharesCompleteCallBack(VotingDescriptor votingDescriptor, bool accept, Signed<BadShareProof> signedBadShareProof, Exception exception)
    {
      this.exception = exception;
      this.accept = accept;
      this.signedBadShareProof = signedBadShareProof;
      this.run = false;
    }

    public override WizardItem Previous()
    {
      return new AuthorityListVotingsItem();
    }

    public override WizardItem Cancel()
    {
      return null;
    }

    public override bool CanPrevious
    {
      get { return !this.run; }
    }

    public override bool CanCancel
    {
      get { return !this.run; }
    }

    public override bool CanNext
    {
      get { return !this.run && this.signedBadShareProof != null; }
    }

    public override bool CancelIsDone
    {
      get { return false; }
    }

    public override void Begin()
    {
      this.voteControl.Voting = VotingDescriptor;
      this.voteControl.Display(false);
      OnUpdateWizard();
    }

    private void verifyButton_Click(object sender, EventArgs e)
    {
      this.verifyButton.Enabled = false;

      Execute();

      this.verifyButton.Enabled = this.signedBadShareProof == null;
    }

    public override void UpdateLanguage()
    {
      this.verifyButton.Text = Resources.AuthorityVotePreviewVerify;
    }
  }
}
