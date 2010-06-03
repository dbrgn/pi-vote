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
      Execute();

      if (this.signedBadShareProof != null)
      {
        var badShareProofItem = new BadShareProofItem();
        badShareProofItem.IsAuthority = true;
        badShareProofItem.SignedBadShareProof = this.signedBadShareProof;
        return badShareProofItem;
      }
      else
      {
        return this;
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

          OnUpdateWizard();
        }
        else
        {
          Status.SetMessage(this.exception.Message, MessageType.Error);
          OnUpdateWizard();
        }
      }
      else
      {
        Status.SetMessage(Resources.CreateVotingAuthFileMissing, MessageType.Error);
        OnUpdateWizard();
      }
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

    public override bool CanNext
    {
      get { return !this.run; }
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
  }
}
