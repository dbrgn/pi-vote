﻿/*
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

    [Browsable(true)]
    public event EventHandler CreateCertificate;

    [Browsable(true)]
    public event EventHandler ResumeCertificate;

    public VotingListsControl()
    {
      InitializeComponent();
    }

    public void Set(CircleController controller, IEnumerable<VotingDescriptor2> votings)
    {
      this.currentVotingListControl.Height = this.currentTabPage.ClientRectangle.Height - this.currentVotingListControl.Top;
      this.pastVotingListControl.Height = this.pastTabPage.ClientRectangle.Height - this.pastVotingListControl.Top;

      this.currentVotingListControl.Set(controller,
        votings.Where(voting =>
          voting.Status == VotingStatus.New ||
          voting.Status == VotingStatus.Sharing ||
          voting.Status == VotingStatus.Ready ||
          voting.Status == VotingStatus.Voting ||
          voting.Status == VotingStatus.Deciphering ||
          (voting.Status == VotingStatus.Finished &&
          DateTime.Now.Subtract(voting.VoteUntil).Days <= 14d))
          .OrderByDescending(voting => voting.VoteFrom));

      this.pastVotingListControl.Set(controller,
        votings.Where(voting =>
          voting.Status == VotingStatus.Aborted ||
          voting.Status == VotingStatus.Offline ||
          (voting.Status == VotingStatus.Finished &&
          DateTime.Now.Subtract(voting.VoteUntil).Days > 14d))
          .OrderByDescending(voting => voting.VoteFrom));

      this.certificateStatus.Controller = controller;
      this.certificateStatus.UpdateDisplay();
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
      this.pastTabPage.Text = Resources.VotingListPast;

      this.currentVotingListControl.UpdateLanguage();
      this.pastVotingListControl.UpdateLanguage();
      this.certificateStatus.UpdateDisplay();
    }

    private void OnCreateCertificate()
    {
      if (CreateCertificate != null)
      {
        CreateCertificate(this, new EventArgs());
      }
    }

    private void OnResumeCertificate()
    {
      if (ResumeCertificate != null)
      {
        ResumeCertificate(this, new EventArgs());
      }
    }

    private void CertificateStatus_CreateCertificate(object sender, EventArgs e)
    {
      OnCreateCertificate();
    }

    private void CertificateStatus_ResumeCertificate(object sender, EventArgs e)
    {
      OnResumeCertificate();
    }

    private void votingTabPage_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.currentVotingListControl.UpdateDisplay();
      this.pastVotingListControl.UpdateDisplay();
      this.certificateStatus.UpdateDisplay();
    }
  }
}
