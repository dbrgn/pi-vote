/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Circle
{
  public partial class VoteCastControl : UserControl
  {
    private CircleController controller;

    private VoterCertificate voterCertificate;

    public VoteCastControl()
    {
      InitializeComponent();

      this.voteControl.ValidChanged += new EventHandler(VoteControl_ValidChanged);
    }

    private void VoteControl_ValidChanged(object sender, EventArgs e)
    {
      this.voteButton.Enabled = this.voteControl.Valid;
    }

    private void voteButton_Click(object sender, EventArgs e)
    {
      this.voteControl.Enabled = false;
      this.voteButton.Enabled = false;

      if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(this.voterCertificate, "Vote"))
      {
        this.controller.Vote(this.voterCertificate, this.voteControl.Voting, this.voteControl.Vota);
      }
    }

    public void Set(CircleController controller, VoterCertificate voterCertificate, VotingDescriptor voting)
    {
      this.controller = controller;
      this.voterCertificate = voterCertificate;
      this.voteControl.Voting = voting;
      this.voteControl.Display(true);
    }
  }
}
