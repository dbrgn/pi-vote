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
using Pirate.PiVote;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Gui;

namespace Pirate.PiVote.Circle.Vote
{
  public partial class VotingDialog : Form
  {
    private VotingDescriptor2 voting;

    private CircleController controller;

    private VoterCertificate voterCertificate;

    public VotingDialog()
    {
      InitializeComponent();

      Text = Resources.VotingDialogTile;
      this.voteButton.Text = Resources.VotingDialogVote;
      this.nextButton.Text = GuiResources.ButtonNext;
      this.previousButton.Text = GuiResources.ButtonPrevious;
      this.cancelButton.Text = GuiResources.ButtonCancel;

      this.votingControl.VotingChanged += new EventHandler(VotingControl_VotingChanged);
    }

    private void VotingControl_VotingChanged(object sender, EventArgs e)
    {
      this.nextButton.Enabled = this.votingControl.CanNext;
      this.previousButton.Enabled = this.votingControl.CanPrevious;
      this.voteButton.Enabled = this.votingControl.Valid;
    }

    private void VotingDialog_Load(object sender, EventArgs e)
    {
      var screenBounds = Screen.PrimaryScreen.Bounds;

      if (screenBounds.Width < Width)
      {
        Width = screenBounds.Width;
      }

      if (screenBounds.Height < Height)
      {
        Height = screenBounds.Height;
      } 
      
      CenterToScreen();
    }

    public void Display(CircleController controller, VoterCertificate voterCertificate, VotingDescriptor2 voting)
    {
      this.controller = controller;
      this.voterCertificate = voterCertificate;
      this.voting = voting;

      this.votingControl.Display(voting);

      this.cancelButton.Enabled = true;
      this.nextButton.Enabled = this.votingControl.CanNext;
      this.previousButton.Enabled = this.votingControl.CanPrevious;
      this.voteButton.Enabled = this.votingControl.Valid;
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      Close();
    }

    private void voteButton_Click(object sender, EventArgs e)
    {
      if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(this.voterCertificate, GuiResources.UnlockActionVote))
      {
        Status.TextStatusDialog.ShowInfo(controller, this);

        try
        {
          this.controller.Vote(this.voterCertificate, this.voting, this.votingControl.Vota);
        }
        catch (Exception exception)
        {
          Error.ErrorDialog.ShowError(exception);
        }
        finally
        {
          this.voterCertificate.Lock();
        }

        Status.TextStatusDialog.HideInfo();
      }

      Close();
    }

    private void nextButton_Click(object sender, EventArgs e)
    {
      this.votingControl.Next();
    }

    private void previousButton_Click(object sender, EventArgs e)
    {
      this.votingControl.Previous();
    }

    public static void ShowVoting(CircleController controller, VoterCertificate voterCertificate, VotingDescriptor2 voting)
    {
      VotingDialog dialog = new VotingDialog();
      dialog.Display(controller, voterCertificate, voting);
      dialog.ShowDialog();
    }
  }
}
