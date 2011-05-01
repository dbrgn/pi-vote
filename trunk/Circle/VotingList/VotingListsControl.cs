/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Circle
{
  public partial class VotingListsControl : UserControl
  {
    [Browsable(true)]
    public event VotingActionHandler VotingAction;

    public VotingListsControl()
    {
      InitializeComponent();
    }

    public void Set(CircleController controller, IEnumerable<VotingDescriptor2> votings)
    {
      this.currentVotingListControl.Set(controller,
        votings.Where(voting =>
          voting.Status == VotingStatus.Voting ||
          voting.Status == VotingStatus.Ready ||
          voting.Status == VotingStatus.Deciphering ||
          (voting.Status == VotingStatus.Finished &&
          DateTime.Now.Subtract(voting.VoteUntil).Days <= 7d))
          .OrderByDescending(voting => voting.VoteFrom));

      this.scheduledVotingListControl.Set(controller,
        votings.Where(voting =>
          voting.Status == VotingStatus.New ||
          voting.Status == VotingStatus.Sharing)
          .OrderByDescending(voting => voting.VoteFrom));

      this.pastVotingListControl.Set(controller,
        votings.Where(voting =>
          voting.Status == VotingStatus.Aborted ||
          (voting.Status == VotingStatus.Finished &&
          DateTime.Now.Subtract(voting.VoteUntil).Days > 7d))
          .OrderByDescending(voting => voting.VoteFrom));

      this.storedVotingListControl.Set(controller,
        votings.Where(voting =>
          voting.Status == VotingStatus.Offline)
          .OrderByDescending(voting => voting.VoteFrom));
    }

    private void OnVotingAction(VotingActionType type, VotingDescriptor2 voting)
    {
      if (VotingAction != null)
      {
        VotingAction(type, voting);
      }
    }

    private void CurrentVotingListControl_VotingAction(VotingActionType type, VotingDescriptor2 voting)
    {
      OnVotingAction(type, voting);
    }

    private void ScheduledVotingListControl_VotingAction(VotingActionType type, VotingDescriptor2 voting)
    {
      OnVotingAction(type, voting);
    }

    private void PastVotingListControl_VotingAction(VotingActionType type, VotingDescriptor2 voting)
    {
      OnVotingAction(type, voting);
    }

    private void storedVotingListControl_VotingAction(VotingActionType type, VotingDescriptor2 voting)
    {
      OnVotingAction(type, voting);
    }

    public void UpdateLanguage()
    {
      this.currentTabPage.Text = Resources.VotingListCurrent;
      this.scheduledTabPage.Text = Resources.VotingListScheduled;
      this.pastTabPage.Text = Resources.VotingListPast;
      this.storedTabPage.Text = Resources.VotingListStored;

      this.currentVotingListControl.UpdateLanguage();
      this.scheduledVotingListControl.UpdateLanguage();
      this.pastVotingListControl.UpdateLanguage();
      this.storedVotingListControl.UpdateLanguage();
    }
  }
}
