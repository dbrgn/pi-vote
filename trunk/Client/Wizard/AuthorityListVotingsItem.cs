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
  public partial class AuthorityListVotingsItem : WizardItem
  {
    private enum AskForPartiallyDecipherCallBackState
    {
      Before,
      During,
      After
    }

    private bool run;
    private Exception exception;
    private VotingDescriptor votingDescriptor;
    private IEnumerable<VotingDescriptor> votings;
    private WizardItem nextItem;
    private AskForPartiallyDecipherCallBackState askForPartiallyDecipherCallBackState;
    private bool askForPartiallyDecipherCallBackResult;
    private int askForPartiallyDecipherValidBallots;

    public AuthorityListVotingsItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      return nextItem;
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
      get { return this.nextItem != null; }
    }

    public override bool CancelIsDone
    {
      get { return true; }
    }

    public override void Begin()
    {
      RefreshList();
    }

    public override void RefreshData()
    {
      RefreshList();
    }

    public void RefreshList()
    {
      this.run = true;
      OnUpdateWizard();

      Status.VotingClient.GetVotingList(Status.CertificateStorage, Status.DataPath, GetVotingListCompleted);

      while (this.run)
      {
        Status.UpdateProgress();
        Thread.Sleep(10);
      }

      Status.UpdateProgress();

      if (this.exception == null)
      {
        ReloadList();

        Status.SetMessage(Resources.VotingListDownloaded, MessageType.Info);
      }
      else
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
      }

      OnUpdateWizard();
      this.votingList.Enabled = true;
    }

    private void ReloadList()
    {
      this.votingList.Items.Clear();

      if (this.votings != null)
      {
        foreach (VotingDescriptor voting in this.votings.OrderByDescending(v => v.VoteFrom))
        {
          ListViewItem item = new ListViewItem(voting.Status.Text());
          item.SubItems.Add(voting.Title.Text);
          item.SubItems.Add(Status.GetGroupName(voting.GroupId));
          item.SubItems.Add(voting.VoteFrom.ToShortDateString());
          item.SubItems.Add(voting.VoteUntil.ToShortDateString());

          switch (voting.Status)
          {
            case VotingStatus.New:
            case VotingStatus.Sharing:
            case VotingStatus.Deciphering:
              item.SubItems.Add(voting.AuthoritiesDone.Count().ToString() + " / " + voting.AuthorityCount.ToString());
              break;
            default:
              item.SubItems.Add(string.Empty);
              break;
          }

          switch (voting.Status)
          {
            case VotingStatus.Voting:
            case VotingStatus.Deciphering:
            case VotingStatus.Finished:
            case VotingStatus.Offline:
              item.SubItems.Add(voting.EnvelopeCount.ToString());
              break;
            default:
              item.SubItems.Add(string.Empty);
              break;
          }

          item.Tag = voting;
          this.votingList.Items.Add(item);
        }
      }
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
          !voting.AuthoritiesDone.Contains(Status.Certificate.Id) &&
          voting.EnvelopeCount > 0;
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
        SetGuiEnable(false);

        ListViewItem item = this.votingList.SelectedItems[0];
        VotingDescriptor voting = (VotingDescriptor)item.Tag;

        string fileName = string.Format("{0}@{1}.pi-auth", Status.Certificate.Id.ToString(), voting.Id.ToString());
        string filePath = Path.Combine(Status.DataPath, fileName);

        this.run = true;
        OnUpdateWizard();

        if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(Status.Certificate, GuiResources.UnlockActionAuthorityCreateShares))
        {
          Status.VotingClient.CreateSharePart(voting.Id, (AuthorityCertificate)Status.Certificate, filePath, CreateSharesCompleteCallBack);

          while (this.run)
          {
            Status.UpdateProgress();
            Thread.Sleep(10);
          }

          Status.UpdateProgress();

          if (this.exception == null)
          {
            item.Tag = this.votingDescriptor;
            item.SubItems[0].Text = this.votingDescriptor.Status.Text();
            item.SubItems[5].Text =
              this.votingDescriptor.Status == VotingStatus.New ||
              this.votingDescriptor.Status == VotingStatus.Sharing ||
              this.votingDescriptor.Status == VotingStatus.Deciphering ?
              this.votingDescriptor.AuthoritiesDone.Count().ToString() + " / " + this.votingDescriptor.AuthorityCount.ToString() :
              string.Empty;
            votingList_SelectedIndexChanged(this.votingList, new EventArgs());

            Status.SetMessage(Resources.AuthorityCreateSharesDone, MessageType.Success);
          }
          else
          {
            Status.SetMessage(this.exception.Message, MessageType.Error);
          }

          Status.Certificate.Lock();
        }
        else
        {
          Status.SetMessage(Resources.AuthorityCreateSharesCanceled, MessageType.Info);
        }

        OnUpdateWizard();
        SetGuiEnable(true);
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
        SetGuiEnable(false);

        ListViewItem item = this.votingList.SelectedItems[0];
        VotingDescriptor voting = (VotingDescriptor)item.Tag;

        var authorityVotePreviewItem = new AuthorityVotePreviewItem();
        authorityVotePreviewItem.VotingDescriptor = voting;

        this.nextItem = authorityVotePreviewItem;
        OnNextStep();
      }
    }

    private void SetGuiEnable(bool enable)
    {
      if (enable)
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
      else
      {
        this.createSharesButton.Enabled = false;
        this.checkSharesButton.Enabled = false;
        this.decipherButton.Enabled = false;
      }

      this.votingList.Enabled = enable;
    }

    private void decipherButton_Click(object sender, EventArgs e)
    {
      if (this.votingList.SelectedIndices.Count > 0)
      {
        SetGuiEnable(false);

        ListViewItem item = this.votingList.SelectedItems[0];
        VotingDescriptor voting = (VotingDescriptor)item.Tag;

        string fileName = string.Format("{0}@{1}.pi-auth", Status.Certificate.Id.ToString(), voting.Id.ToString());
        string filePath = Path.Combine(Status.DataPath, fileName);

        if (File.Exists(filePath))
        {
          this.run = true;
          OnUpdateWizard();

          if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(Status.Certificate, GuiResources.UnlockActionAuthorityDecipher))
          {
            this.askForPartiallyDecipherCallBackState = AskForPartiallyDecipherCallBackState.Before;

            Status.VotingClient.CreateDeciphers(voting.Id, (AuthorityCertificate)Status.Certificate, filePath, AskForPartiallyDecipherCallBack, CreateDeciphersCompleteCallBack);

            while (this.run)
            {
              if (this.askForPartiallyDecipherCallBackState == AskForPartiallyDecipherCallBackState.During)
              {
                this.askForPartiallyDecipherCallBackResult =
                  MessageForm.Show(
                  string.Format(GuiResources.AskForPartiallyDecipher, this.askForPartiallyDecipherValidBallots),
                  GuiResources.MessageBoxTitle,
                  MessageBoxButtons.YesNo,
                  MessageBoxIcon.Question,
                  DialogResult.No)
                  == DialogResult.Yes;

                this.askForPartiallyDecipherCallBackState = AskForPartiallyDecipherCallBackState.After;
              }

              Status.UpdateProgress();
              Thread.Sleep(10);
            }

            Status.UpdateProgress();

            if (this.exception == null)
            {
              item.Tag = this.votingDescriptor;
              item.SubItems[0].Text = this.votingDescriptor.Status.Text();
              item.SubItems[5].Text =
                this.votingDescriptor.Status == VotingStatus.New ||
                this.votingDescriptor.Status == VotingStatus.Sharing ||
                this.votingDescriptor.Status == VotingStatus.Deciphering ?
                this.votingDescriptor.AuthoritiesDone.Count().ToString() + " / " + this.votingDescriptor.AuthorityCount.ToString() :
                string.Empty;
              votingList_SelectedIndexChanged(this.votingList, new EventArgs());

              Status.SetMessage(Resources.AuthorityDecipherDone, MessageType.Success);
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

          OnUpdateWizard();
        }
        else
        {
          Status.SetMessage(Resources.CreateVotingAuthFileMissing, MessageType.Error);
          OnUpdateWizard();
        }

        SetGuiEnable(true);
      }
    }

    private bool AskForPartiallyDecipherCallBack(int validEnvelopeCount)
    {
      this.askForPartiallyDecipherValidBallots = validEnvelopeCount;
      this.askForPartiallyDecipherCallBackState = AskForPartiallyDecipherCallBackState.During;

      while (this.askForPartiallyDecipherCallBackState == AskForPartiallyDecipherCallBackState.During)
      {
        Thread.Sleep(10);
      }

      return this.askForPartiallyDecipherCallBackResult;
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
      this.groupColumnHeader.Text = Resources.VotingGroup;
      this.statusColumnHeader.Text = Resources.AuthorityListStatus;
      this.voteFromColumnHeader.Text = Resources.VotingListVoteFrom;
      this.voteUntilColumnHeader.Text = Resources.VotingListVoteUntil;
      this.authorityColumnHeader.Text = Resources.VotingListAuthorities;
      this.envelopesColumnHeader.Text = Resources.VotingListEnvelopes;
    }

    private void refreshMenu_Click(object sender, EventArgs e)
    {
      RefreshList();
    }
  }
}
