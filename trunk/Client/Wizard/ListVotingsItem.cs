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
              VoteItem voteItem = new VoteItem();
              voteItem.VotingDescriptor = votingDescriptor;
              return voteItem;
            case VotingStatus.Finished:
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
            case VotingStatus.Finished:
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
      this.run = true;

      Status.VotingClient.GetVotingList(Status.CertificateStorage, GetVotingListCompleted);

      while (this.run)
      {
        Status.UpdateProgress();
        Application.DoEvents();
        Thread.Sleep(1);
      }

      Status.UpdateProgress();

      if (exception == null)
      {
        if (this.votings != null)
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

          foreach (VotingDescriptor voting in this.votings)
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
        }

        this.votingList.Enabled = true;
        Status.SetMessage(Resources.VotingListDownloaded, MessageType.Info);
      }
      else
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
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
      this.statusColumnHeader.Text = Resources.VotingStatus;
      this.voteFromColumnHeader.Text = Resources.VotingListVoteFrom;
      this.voteUntilColumnHeader.Text = Resources.VotingListVoteUntil;
      this.authoritiesColumnHeader.Text = Resources.VotingListAuthorities;
      this.envelopesColumnHeader.Text = Resources.VotingListEnvelopes;
    }
  }
}
