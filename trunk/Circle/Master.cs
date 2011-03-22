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
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Circle
{
  public partial class Master : Form
  {
    private Timer timer;

    private CircleController Controller { get; set; }

    public Master()
    {
      InitializeComponent();
    }

    private void Master_Load(object sender, EventArgs e)
    {
      this.voteCastControl.Visible = false;
      this.simpleCreateCertificateControl.Visible = false;

      CenterToScreen();
      Show();

      Controller = new CircleController();

      this.timer = new Timer();
      this.timer.Tick += new EventHandler(Timer_Tick);
      this.timer.Interval = 100;
      this.timer.Start();

      Controller.LoadCertificates();
      Controller.Prepare();

      var votings = Controller.GetVotingList();

      if (votings != null)
      {
        this.votingListsControl.Set(votings);
      }
    }

    private void Timer_Tick(object sender, EventArgs e)
    {
      this.statusLabel.Text = Controller.Text;
      this.progressBar.Value = Convert.ToInt32(Controller.Progress * 100d);
      this.subProgressBar.Value = Convert.ToInt32(Controller.SubProgress * 100d);
    }

    private void Master_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.timer.Stop();
      Controller.Disconnect();
    }

    private void VotingListsControl_VotingAction(VotingDescriptor voting)
    {
      switch (voting.Status)
      {
        case VotingStatus.New:
          break;
        case VotingStatus.Sharing:
          break;
        case VotingStatus.Voting:
          GoVote(voting);
          break;
        case VotingStatus.Deciphering:
          break;
        case VotingStatus.Finished:
          break;
      }
    }

    private void GoVote(VotingDescriptor voting)
    {
      var certificate = Controller.GetVoterCertificate(voting.GroupId);

      if (certificate != null)
      {
        switch (certificate.Validate(Controller.Status.CertificateStorage))
        {
          case CertificateValidationResult.Valid:
            this.votingListsControl.Visible = false;
            this.voteCastControl.Visible = true;
            this.voteCastControl.Set(Controller, certificate, voting);
            break;
          case CertificateValidationResult.NotYetValid:
            var status = Controller.GetSignatureResponse(certificate);
            break;
          default:
            break;
        }
      }
      else
      {
        this.votingListsControl.Visible = false;
        this.simpleCreateCertificateControl.Visible = true;
        this.simpleCreateCertificateControl.Set(Controller, null, voting.GroupId);
      }
    }

    private void simpleCreateCertificateControl_ReturnFromControl(object sender, EventArgs e)
    {
      this.votingListsControl.Visible = true;
      this.simpleCreateCertificateControl.Visible = false;
    }
  }
}
