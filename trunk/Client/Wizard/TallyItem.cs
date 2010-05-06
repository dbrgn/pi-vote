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
  public partial class TallyItem : WizardItem
  {
    private bool done = false;
    private bool run = false;
    private VotingResult result;
    private Exception exception;
    private IDictionary<Guid, VoteReceiptStatus> voteReceiptsStatus;

    public VotingDescriptor VotingDescriptor { get; set; }

    public IEnumerable<Signed<VoteReceipt>> VoteReceipts { get; set; }

    public TallyItem()
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
      Status.VotingClient.GetResult(VotingDescriptor.Id, VoteReceipts, GetResultComplete);

      while (this.run)
      {
        Status.UpdateProgress();
        Thread.Sleep(1);
      }

      Status.UpdateProgress();

      if (this.exception == null)
      {
        if (this.result != null)
        {
          ListViewItem title = new ListViewItem(this.result.Title.Text);
          this.resultList.Items.Add(title);

          ListViewItem totalBallots = new ListViewItem(Resources.TotalBallots);
          totalBallots.SubItems.Add(this.result.TotalBallots.ToString());
          this.resultList.Items.Add(totalBallots);

          ListViewItem invalidBallots = new ListViewItem(Resources.InvalidBallots);
          invalidBallots.SubItems.Add(this.result.InvalidBallots.ToString());
          this.resultList.Items.Add(invalidBallots);

          ListViewItem validBallots = new ListViewItem(Resources.ValidBallots);
          validBallots.SubItems.Add(this.result.ValidBallots.ToString());
          this.resultList.Items.Add(validBallots);

          ListViewItem space = null;

          foreach (QuestionResult question in this.result.Questions)
          {
            space = new ListViewItem(string.Empty);
            space.SubItems.Add(string.Empty);
            this.resultList.Items.Add(space);

            space = new ListViewItem(question.Text.Text);
            space.SubItems.Add(string.Empty);
            this.resultList.Items.Add(space);

            foreach (OptionResult option in question.Options)
            {
              ListViewItem optionBallots = new ListViewItem(option.Text.Text);
              optionBallots.SubItems.Add(option.Result.ToString());
              this.resultList.Items.Add(optionBallots);
            }
          }

          space = new ListViewItem(string.Empty);
          space.SubItems.Add(string.Empty);
          this.resultList.Items.Add(space);

          foreach (KeyValuePair<Guid, VoteReceiptStatus> voteReceipt in this.voteReceiptsStatus)
          {
            ListViewItem voteReceiptItem = new ListViewItem(Resources.TallyVoteReceipt + " " + voteReceipt.Key.ToString());

            switch (voteReceipt.Value)
            {
              case VoteReceiptStatus.NotFound:
                voteReceiptItem.SubItems.Add(Resources.TallyVoteReceiptNotFound);
                break;
              case VoteReceiptStatus.FoundBad:
                voteReceiptItem.SubItems.Add(Resources.TallyVoteReceiptFoundBad);
                break;
              case VoteReceiptStatus.FoundOk:
                voteReceiptItem.SubItems.Add(Resources.TallyVoteReceiptFoundOk);
                break;
              default:
                throw new InvalidOperationException("Unknown VoteReceiptStatus");
            }

            this.resultList.Items.Add(voteReceiptItem);
          }
        }
        else
        {
          Status.SetMessage("No result from server.", MessageType.Error);
        }
      }
      else
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
      }

      this.done = true;
      OnUpdateWizard();
    }

    private void GetResultComplete(VotingResult result, IDictionary<Guid, VoteReceiptStatus> voteReceiptsStatus, Exception exception)
    {
      this.voteReceiptsStatus = voteReceiptsStatus;
      this.exception = exception;
      this.result = result;
      this.run = false;
    }
  }
}
