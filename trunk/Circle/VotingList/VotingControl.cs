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
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Circle
{
  public delegate void VotingActionHandler(VotingDescriptor voting);

  public partial class VotingControl : UserControl
  {
    private VotingDescriptor voting;

    [Browsable(true)]
    public event VotingActionHandler VotingAction;

    public VotingControl()
    {
      InitializeComponent();
    }

    public VotingDescriptor Voting
    {
      get
      {
        return this.voting;
      }
      set
      {
        this.voting = value;

        if (this.voting != null)
        {
          this.titleLabel.Text = this.voting.Title.Text;
          this.descriptionLabel.Text = this.voting.Description.Text;

          switch (this.voting.Status)
          {
            case VotingStatus.Voting:
              this.actionButton.Text = "&Vote";
              this.actionButton.Visible = true;
              break;
            case VotingStatus.New:
              this.actionButton.Text = "&Create shares";
              this.actionButton.Visible = true;
              break;
            case VotingStatus.Sharing:
              this.actionButton.Text = "&Verify shares";
              this.actionButton.Visible = true;

              break;
            case VotingStatus.Deciphering:
              this.actionButton.Text = "&Decipher";
              this.actionButton.Visible = true;

              break;
            default:
              this.actionButton.Text = string.Empty;
              this.actionButton.Visible = false;
              break;
          }
        }
      }
    }

    public void OnVotingAction()
    {
      if (VotingAction != null)
      {
        VotingAction(Voting);
      }
    }

    private void ActionButton_Click(object sender, EventArgs e)
    {
      OnVotingAction();
    }
  }
}
