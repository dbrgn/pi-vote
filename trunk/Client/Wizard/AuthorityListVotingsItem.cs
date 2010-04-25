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
  public partial class AuthorityListVotingsItem : WizardItem
  {
    private bool run;
    private Exception exception;
    private bool accept;
    private Signed<BadShareProof> signedBadShareProof;
    private VotingDescriptor votingDescriptor;
    private IEnumerable<VotingDescriptor> votings;

    public AuthorityListVotingsItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      if (this.signedBadShareProof == null)
      {
        return null;
      }
      else
      {
        var badShareProofItem = new BadShareProofItem();
        badShareProofItem.IsAuthority = true;
        badShareProofItem.SignedBadShareProof = this.signedBadShareProof;
        return badShareProofItem;
      }
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
      get { return true; }
    }

    public override bool CanNext
    {
      get { return this.signedBadShareProof != null; }
    }

    public override void Begin()
    {
      this.run = true;

      Status.VotingClient.GetVotingList(Status.CertificateStorage, GetVotingListCompleted);

      while (this.run)
      {
        Status.UpdateProgress();
        Application.DoEvents();
        Thread.Sleep(1);
      }

      Status.UpdateProgress();

      if (this.exception == null)
      {
        if (this.votings != null)
        {
          foreach (VotingDescriptor voting in this.votings.OrderBy(v => v.VoteFrom))
          {
            ListViewItem item = new ListViewItem(voting.Title.Text);
            item.SubItems.Add(voting.Status.Text());
            item.SubItems.Add(voting.VoteFrom.ToShortDateString());
            item.SubItems.Add(voting.VoteUntil.ToShortDateString());
            if (voting.AuthoritiesDone == null)
            {
              item.SubItems.Add(string.Empty);
            }
            else
            {
              item.SubItems.Add(voting.AuthoritiesDone.Count().ToString() + " / " + voting.AuthorityCount.ToString());
            }
            if (voting.Status == VotingStatus.Voting ||
                voting.Status == VotingStatus.Deciphering ||
                voting.Status == VotingStatus.Finished)
            {
              item.SubItems.Add(voting.EnvelopeCount.ToString());
            }
            else
            {
              item.SubItems.Add(string.Empty);
            }
            item.Tag = voting;
            this.votingList.Items.Add(item);
          }
        }
      }
      else
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
      }

      this.votingList.Enabled = true;
    }

    private void GetVotingListCompleted(IEnumerable<VotingDescriptor> votingList, Exception exception)
    {
      this.exception = exception;
      this.votings = votingList;
      this.run = false;
    }

    private void votingList_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.votingList.SelectedIndices.Count > 0)
      {
        ListViewItem item = this.votingList.SelectedItems[0];
        VotingDescriptor voting = (VotingDescriptor)item.Tag;

        this.createSharesButton.Enabled = 
          voting.Status == VotingStatus.New && 
          !voting.AuthoritiesDone.Contains(Status.Certificate.Id);
        this.checkSharesButton.Enabled = 
          voting.Status == VotingStatus.Sharing && 
          !voting.AuthoritiesDone.Contains(Status.Certificate.Id);
        this.decipherButton.Enabled = 
          voting.Status == VotingStatus.Deciphering && 
          !voting.AuthoritiesDone.Contains(Status.Certificate.Id);
      }
      else
      {
        this.createSharesButton.Enabled = false;
        this.checkSharesButton.Enabled = false;
        this.decipherButton.Enabled = false;
      }
    }

    private void createSharesButton_Click(object sender, EventArgs e)
    {
      if (this.votingList.SelectedIndices.Count > 0)
      {
        ListViewItem item = this.votingList.SelectedItems[0];
        VotingDescriptor voting = (VotingDescriptor)item.Tag;

        string fileName = string.Format("{0}@{1}.pi-auth", Status.Certificate.Id.ToString(), voting.Id.ToString());
        string filePath = Path.Combine(Status.DataPath, fileName);

        this.createSharesButton.Enabled = false;
        this.votingList.Enabled = false;

        this.run = true;
        OnUpdateWizard();

        Status.VotingClient.CreateSharePart(voting.Id, (AuthorityCertificate)Status.Certificate, filePath, CreateSharesCompleteCallBack);

        while (this.run)
        {
          Status.UpdateProgress();
          Application.DoEvents();
          Thread.Sleep(1);
        }

        Status.UpdateProgress();

        if (this.exception == null)
        {
          item.Tag = this.votingDescriptor;
          item.SubItems[1].Text = this.votingDescriptor.Status.Text();
          item.SubItems[4].Text = this.votingDescriptor.AuthoritiesDone == null ? string.Empty :
            this.votingDescriptor.AuthoritiesDone.Count().ToString() + " / " + this.votingDescriptor.AuthorityCount.ToString();
          votingList_SelectedIndexChanged(this.votingList, new EventArgs());

          Status.SetMessage(Resources.AuthorityCreateSharesDone, MessageType.Success);
        }
        else
        {
          Status.SetMessage(this.exception.Message, MessageType.Error);
        }

        this.votingList.Enabled = true;
        OnUpdateWizard();
      }
    }

    private void CreateSharesCompleteCallBack(VotingDescriptor votingDescriptor, Exception exception)
    {
      this.exception = exception;
      this.votingDescriptor = votingDescriptor;
      this.run = false;
    }

    private void checkSharesButton_Click(object sender, EventArgs e)
    {
      if (this.votingList.SelectedIndices.Count > 0)
      {
        ListViewItem item = this.votingList.SelectedItems[0];
        VotingDescriptor voting = (VotingDescriptor)item.Tag;

        string fileName = string.Format("{0}@{1}.pi-auth", Status.Certificate.Id.ToString(), voting.Id.ToString());
        string filePath = Path.Combine(Status.DataPath, fileName);

        if (File.Exists(filePath))
        {
          this.checkSharesButton.Enabled = false;
          this.votingList.Enabled = false;

          this.run = true;
          OnUpdateWizard();

          Status.VotingClient.CheckShares(voting.Id, (AuthorityCertificate)Status.Certificate, filePath, CheckSharesCompleteCallBack);

          while (this.run)
          {
            Status.UpdateProgress();
            Application.DoEvents();
            Thread.Sleep(1);
          }

          Status.UpdateProgress();

          if (this.exception == null)
          {
            item.Tag = this.votingDescriptor;
            item.SubItems[1].Text = this.votingDescriptor.Status.Text();
            item.SubItems[4].Text = this.votingDescriptor.AuthoritiesDone == null ? string.Empty : 
              this.votingDescriptor.AuthoritiesDone.Count().ToString() + " / " + this.votingDescriptor.AuthorityCount.ToString();
            votingList_SelectedIndexChanged(this.votingList, new EventArgs());

            if (this.accept)
            {
              Status.SetMessage(Resources.AuthorityCheckSharesAccept, MessageType.Success);
            }
            else
            {
              Status.SetMessage(Resources.AuthorityCheckSharesDecline, MessageType.Warning);
            }

            OnNextStep();
          }
          else
          {
            Status.SetMessage(this.exception.Message, MessageType.Error);
            this.votingList.Enabled = true;
            OnUpdateWizard();
          }
        }
        else
        {
          Status.SetMessage(Resources.CreateVotingAuthFileMissing, MessageType.Error);
          this.votingList.Enabled = true;
          OnUpdateWizard();
        }
      }
    }

    private void CheckSharesCompleteCallBack(VotingDescriptor votingDescriptor, bool accept, Signed<BadShareProof> signedBadShareProof, Exception exception)
    {
      this.exception = exception;
      this.votingDescriptor = votingDescriptor;
      this.accept = accept;
      this.signedBadShareProof = signedBadShareProof;
      this.run = false;
    }

    private void decipherButton_Click(object sender, EventArgs e)
    {
      if (this.votingList.SelectedIndices.Count > 0)
      {
        ListViewItem item = this.votingList.SelectedItems[0];
        VotingDescriptor voting = (VotingDescriptor)item.Tag;

        string fileName = string.Format("{0}@{1}.pi-auth", Status.Certificate.Id.ToString(), voting.Id.ToString());
        string filePath = Path.Combine(Status.DataPath, fileName);

        if (File.Exists(filePath))
        {
          this.checkSharesButton.Enabled = false;
          this.votingList.Enabled = false;

          this.run = true;
          OnUpdateWizard();

          Status.VotingClient.CreateDeciphers(voting.Id, (AuthorityCertificate)Status.Certificate, filePath, CreateDeciphersCompleteCallBack);

          while (this.run)
          {
            Status.UpdateProgress();
            Application.DoEvents();
            Thread.Sleep(1);
          }

          Status.UpdateProgress();

          if (this.exception == null)
          {
            item.Tag = this.votingDescriptor;
            item.SubItems[1].Text = this.votingDescriptor.Status.Text();
            item.SubItems[4].Text = this.votingDescriptor.AuthoritiesDone == null ? string.Empty :
              this.votingDescriptor.AuthoritiesDone.Count().ToString() + " / " + this.votingDescriptor.AuthorityCount.ToString();
            votingList_SelectedIndexChanged(this.votingList, new EventArgs());

            Status.SetMessage(Resources.AuthorityDecipherDone, MessageType.Success);
          }
          else
          {
            Status.SetMessage(this.exception.Message, MessageType.Error);
          }

          Status.UpdateProgress();
          this.votingList.Enabled = true;
          OnUpdateWizard();
        }
        else
        {
          Status.SetMessage(Resources.CreateVotingAuthFileMissing, MessageType.Error);
          this.votingList.Enabled = true;
          OnUpdateWizard();
        }
      }
    }

    private void CreateDeciphersCompleteCallBack(VotingDescriptor votingDescriptor, Exception exception)
    {
      this.exception = exception;
      this.votingDescriptor = votingDescriptor;
      this.run = false;
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.createSharesButton.Text = Resources.AuthorityCreateShares;
      this.checkSharesButton.Text = Resources.AuthorityCheckShares;
      this.decipherButton.Text = Resources.AuthorityDecipher;
      this.titleColumnHeader.Text = Resources.AuthorityListTitle;
      this.statusColumnHeader.Text = Resources.AuthorityListStatus;
      this.voteFromColumnHeader.Text = Resources.VotingListVoteFrom;
      this.voteUntilColumnHeader.Text = Resources.VotingListVoteUntil;
      this.authorityColumnHeader.Text = Resources.VotingListAuthorities;
      this.envelopesColumnHeader.Text = Resources.VotingListEnvelopes;
    }
  }
}
