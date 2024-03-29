﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
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

      Status.VotingClient.ActivateVoter();

      if (VotingDescriptor.OfflinePath.IsNullOrEmpty())
      {
        Status.VotingClient.GetResult(VotingDescriptor.Id, VoteReceipts, BaseParameters.StandardProofCount, GetResultComplete);
      }
      else
      {
        Status.VotingClient.GetResult(VotingDescriptor.OfflinePath, VoteReceipts, BaseParameters.StandardProofCount, GetResultComplete);
      }

      while (this.run)
      {
        Status.UpdateProgress();
        Thread.Sleep(10);
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

            ListViewItem questionItem = new ListViewItem(question.Text.Text);
            questionItem.SubItems.Add(string.Empty);
            this.resultList.Items.Add(questionItem);

            int abstainCount = 0;
            bool hasAbstain = false;

            foreach (OptionResult option in question.Options)
            {
              if (option.IsAbstentionSpecial)
              {
                hasAbstain = true;
                abstainCount += option.Result;
              }
              else
              {
                ListViewItem optionBallots = new ListViewItem(option.Text.Text);
                optionBallots.SubItems.Add(option.Result >= 0 ? option.Result.ToString() : Resources.InvalidResult);
                this.resultList.Items.Add(optionBallots);
              }
            }

            if (hasAbstain)
            {
              ListViewItem abstainBallots = new ListViewItem(Resources.OptionAbstain);
              abstainBallots.SubItems.Add(abstainCount >= 0 ? abstainCount.ToString() : Resources.InvalidResult);
              this.resultList.Items.Add(abstainBallots);
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

          Status.SetMessage(Resources.TallyDone, MessageType.Success);
        }
        else
        {
          Status.SetMessage(Resources.TallyNoResult, MessageType.Error);
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
