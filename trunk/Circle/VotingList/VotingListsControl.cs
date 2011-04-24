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
          DateTime.Now.Subtract(voting.VoteUntil).Days <= 7d)));

      this.scheduledVotingListControl.Set(controller,
        votings.Where(voting => 
          voting.Status == VotingStatus.New || 
          voting.Status == VotingStatus.Sharing));

      this.pastVotingListControl.Set(controller,
        votings.Where(voting =>
          voting.Status == VotingStatus.Aborted ||
          voting.Status == VotingStatus.Offline ||
          (voting.Status == VotingStatus.Finished &&
          DateTime.Now.Subtract(voting.VoteUntil).Days > 7d)));
    }

    private void OnVotingAction(VotingDescriptor2 voting)
    {
      if (VotingAction != null)
      {
        VotingAction(voting);
      }
    }

    private void CurrentVotingListControl_VotingAction(VotingDescriptor2 voting)
    {
      OnVotingAction(voting);
    }

    private void ScheduledVotingListControl_VotingAction(VotingDescriptor2 voting)
    {
      OnVotingAction(voting);
    }

    private void PastVotingListControl_VotingAction(VotingDescriptor2 voting)
    {
      OnVotingAction(voting);
    }
  }
}
