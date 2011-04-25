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
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Circle
{
  public delegate void VotingActionHandler(VotingDescriptor2 voting);

  public partial class VotingControl : UserControl
  {
    private VotingDescriptor2 voting;

    [Browsable(true)]
    public event VotingActionHandler VotingAction;

    public VotingControl()
    {
      InitializeComponent();
    }

    public CircleController Controller { get; set; }
    
    public VotingDescriptor2 Voting
    {
      get
      {
        return this.voting;
      }
      set
      {
        if (Controller == null)
        {
          throw new InvalidOperationException("Controller must be set.");
        }

        if (value == null)
        {
          throw new ArgumentNullException("value");
        }

        this.voting = value;

        UpdateDisplay();
      }
    }

    public void UpdateDisplay()
    {
      if (this.voting != null)
      {
        bool isAuthority = Controller.GetAuthorityCertificates().Count() > 0;
        bool isAuthorityForVoting = Controller.GetAuthorityCertificate(this.voting) != null;
        bool hasVoterCertificate = Controller.GetVoterCertificate(this.voting) != null;
        bool hasVoterCertificateThatCanVote = Controller.GetVoterCertificateThatCanVote(this.voting) != null;

        this.titleLabel.Text = this.voting.Title.Text;
        this.descriptionLabel.Text = this.voting.Description.Text;

        switch (this.voting.Status)
        {
          case VotingStatus.Voting:
            this.statusLabel.Text = string.Format(Resources.VotingStatusVote, this.voting.EnvelopeCount);
            this.actionButton.Text = Resources.VotingActionVote;
            this.actionButton.Visible = true;
            this.actionButton.Enabled = !hasVoterCertificate || hasVoterCertificateThatCanVote;
            break;
          case VotingStatus.New:
            this.statusLabel.Text = string.Format(Resources.VotingStatusShare, this.voting.AuthorityCount - this.voting.AuthoritiesDone.Count(), this.voting.AuthorityCount);
            this.actionButton.Text = Resources.VotingActionShare;
            this.actionButton.Visible = isAuthority;
            this.actionButton.Enabled = isAuthorityForVoting;
            break;
          case VotingStatus.Sharing:
            this.statusLabel.Text = string.Format(Resources.VotingStatusCheck, this.voting.AuthorityCount - this.voting.AuthoritiesDone.Count(), this.voting.AuthorityCount);
            this.actionButton.Text = Resources.VotingActionCheck;
            this.actionButton.Visible = isAuthority;
            this.actionButton.Enabled = isAuthorityForVoting;
            break;
          case VotingStatus.Deciphering:
            this.statusLabel.Text = string.Format(Resources.VotingStatusDecipher, this.voting.AuthorityCount - this.voting.AuthoritiesDone.Count(), this.voting.AuthorityCount);
            this.actionButton.Text = Resources.VotingActionDecipher;
            this.actionButton.Visible = isAuthority;
            this.actionButton.Enabled = isAuthorityForVoting;
            break;
          case VotingStatus.Finished:
            this.statusLabel.Text = string.Format(Resources.VotingStatusTally, this.voting.EnvelopeCount);
            this.actionButton.Text = Resources.VotingActionTally;
            this.actionButton.Visible = true;
            break;
          case VotingStatus.Aborted:
            this.statusLabel.Text = Resources.VotingStatusAborted;
            this.actionButton.Text = string.Empty;
            this.actionButton.Visible = false;
            break;
          case VotingStatus.Offline:
            this.statusLabel.Text = Resources.VotingStatusOffline;
            this.actionButton.Text = string.Empty;
            this.actionButton.Visible = false;
            break;
          case VotingStatus.Ready:
            this.statusLabel.Text = string.Format(Resources.VotingStatusReady, this.voting.VoteFrom.ToShortDateString());
            this.actionButton.Text = string.Empty;
            this.actionButton.Visible = false;
            break;
          default:
            this.actionButton.Text = string.Empty;
            this.actionButton.Visible = false;
            break;
        }
      }
    }

    public void OnVotingAction()
    {
      if (VotingAction != null)
      {
        VotingAction(Voting);
        UpdateDisplay();
      }
    }

    private void ActionButton_Click(object sender, EventArgs e)
    {
      OnVotingAction();
    }
  }
}
