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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Client
{
  public partial class ListVotingsItem : WizardItem
  {
    private bool run;
    private Exception exception;
    private IEnumerable<VotingDescriptor> votings;
    private Dictionary<Guid, List<Signed<VoteReceipt>>> voteReceipts;

    public ListVotingsItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      if (this.votingList.SelectedIndices.Count > 0)
      {
        var votingDescriptor = (VotingDescriptor)this.votingList.SelectedItems[0].Tag;

        switch (votingDescriptor.Status)
        {
          case VotingStatus.Voting:
            if (Status.Certificate != null)
            {
              ViewVotingAuthoritiesItem viewVotingAuthoritiesItem = new ViewVotingAuthoritiesItem();
              viewVotingAuthoritiesItem.VotingDescriptor = votingDescriptor;
              return viewVotingAuthoritiesItem;
            }
            else
            {
              return null;
            }
          case VotingStatus.Finished:
          case VotingStatus.Offline:
            TallyItem tallyItem = new TallyItem();
            tallyItem.VotingDescriptor = votingDescriptor;
            tallyItem.VoteReceipts =
              this.voteReceipts.ContainsKey(votingDescriptor.Id) ?
              this.voteReceipts[votingDescriptor.Id] :
              new List<Signed<VoteReceipt>>();
            return tallyItem;
          default:
            return null;
        }
      }
      else
      {
        return null;
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
      get { return !this.run; }
    }

    public override bool CancelIsDone
    {
      get { return true; }
    }

    public override bool CanNext
    {
      get
      {
        if (this.votingList.SelectedIndices.Count > 0)
        {
          var votingDescriptor = (VotingDescriptor)this.votingList.SelectedItems[0].Tag;

          switch (votingDescriptor.Status)
          {
            case VotingStatus.Voting:
              return Status.Certificate != null &&
                (votingDescriptor.GroupId == ((VoterCertificate)Status.Certificate).GroupId);
            case VotingStatus.Finished:
            case VotingStatus.Offline:
              return true;
            default:
              return false;
          }
        }
        else
        {
          return false;
        }
      }
    }

    public override void Begin()
    {
      RefreshList();
    }

    public override void RefreshData()
    {
      RefreshList();
    }

    private void RefreshList()
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

      if (exception == null)
      {
        if (this.votings != null)
        {
          LoadVoteReceipts();

          this.votingList.Items.Clear();

          foreach (VotingDescriptor voting in this.votings.OrderByDescending(v => v.VoteFrom))
          {
            if (Status.Certificate == null ||
                !(Status.Certificate is VoterCertificate) ||
                ((VoterCertificate)Status.Certificate).GroupId == voting.GroupId)
            {
              AddVotingToList(voting);
            }
          }
        }

        this.votingList.Enabled = true;
        Status.SetMessage(Resources.VotingListDownloaded, MessageType.Info);
      }
      else
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
      }

      OnUpdateWizard();
    }

    private void AddVotingToList(VotingDescriptor voting)
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

      if (this.voteReceipts.ContainsKey(voting.Id))
      {
        var voteReceiptList = this.voteReceipts[voting.Id];

        if (voteReceiptList.Count > 1)
        {
          item.SubItems.Add(voteReceiptList.Count.ToString());
        }
        else
        {
          item.SubItems.Add(Resources.ListVotingsVotedYes);
        }
      }
      else
      {
        item.SubItems.Add(Resources.ListVotingsVotedNo);
      }

      item.Tag = voting;
      this.votingList.Items.Add(item);
    }

    private void LoadVoteReceipts()
    {
      DirectoryInfo dataDirectory = new DirectoryInfo(Status.DataPath);
      this.voteReceipts = new Dictionary<Guid, List<Signed<VoteReceipt>>>();

      foreach (FileInfo file in dataDirectory.GetFiles("*.pi-receipt"))
      {
        Signed<VoteReceipt> signedVoteReceipt = Serializable.Load<Signed<VoteReceipt>>(file.FullName);
        VoteReceipt voteReceipt = signedVoteReceipt.Value;

        if (!this.voteReceipts.ContainsKey(voteReceipt.VotingId))
        {
          this.voteReceipts.Add(voteReceipt.VotingId, new List<Signed<VoteReceipt>>());
        }

        this.voteReceipts[voteReceipt.VotingId].Add(signedVoteReceipt);
      }
    }

    private void GetVotingListCompleted(IEnumerable<VotingDescriptor> votingList, Exception exception)
    {
      this.votings = votingList;
      this.exception = exception;
      this.run = false;
    }

    private void votingList_SelectedIndexChanged(object sender, EventArgs e)
    {
      OnUpdateWizard();
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.titleColumnHeader.Text = Resources.VotingTitle;
      this.groupColumnHeader.Text = Resources.VotingGroup;
      this.statusColumnHeader.Text = Resources.VotingStatus;
      this.voteFromColumnHeader.Text = Resources.VotingListVoteFrom;
      this.voteUntilColumnHeader.Text = Resources.VotingListVoteUntil;
      this.authoritiesColumnHeader.Text = Resources.VotingListAuthorities;
      this.envelopesColumnHeader.Text = Resources.VotingListEnvelopes;
    }

    private void downloadVotingMenuItem_Click(object sender, EventArgs e)
    {
      if (this.votingList.SelectedIndices.Count > 0)
      {
        var votingDescriptor = (VotingDescriptor)this.votingList.SelectedItems[0].Tag;

        if (votingDescriptor.Status == VotingStatus.Finished)
        {
          DownloadVoting(votingDescriptor);
        }
      }
    }

    private void DownloadVoting(VotingDescriptor votingDescriptor)
    {
      this.run = true;
      this.votingList.Enabled = false;
      OnUpdateWizard();

      Status.VotingClient.DownloadVoting(votingDescriptor.Id, Status.DataPath, DownloadVotingComplete);

      while (this.run)
      {
        Status.UpdateProgress();
        Thread.Sleep(10);
      }

      if (this.exception == null)
      {
        RefreshList();

        Status.SetMessage(Resources.ListVotingDownloadSuccess, MessageType.Success);
      }
      else
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
      }

      this.votingList.Enabled = true;
      OnUpdateWizard();
    }

    private void DownloadVotingComplete(Exception exception)
    {
      this.run = false;
      this.exception = exception;
    }

    private void votingListContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
      this.downloadVotingMenuItem.Text = Resources.ListVotingDownloadMenu;

      if (this.votingList.SelectedIndices.Count > 0)
      {
        var votingDescriptor = (VotingDescriptor)this.votingList.SelectedItems[0].Tag;

        this.downloadVotingMenuItem.Enabled = votingDescriptor.Status == VotingStatus.Finished;
      }
      else
      {
        this.downloadVotingMenuItem.Enabled = false;
      }
    }

    private void refreshItem_Click(object sender, EventArgs e)
    {
      RefreshList();
    }
  }
}
