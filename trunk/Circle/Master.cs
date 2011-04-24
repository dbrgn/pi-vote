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
using Pirate.PiVote.Gui;

namespace Pirate.PiVote.Circle
{
  public partial class Master : Form
  {
    private CircleController Controller { get; set; }

    public Master()
    {
      InitializeComponent();
    }

    private void Master_Load(object sender, EventArgs e)
    {
      CenterToScreen();
      Show();

      Controller = new CircleController();

      Status.TextStatusDialog.ShowInfo(Controller);

      try
      {
        Controller.Prepare();
        Controller.LoadCertificates();

        var votings = Controller.GetVotingList();
        this.votingListsControl.Set(Controller, votings);
      }
      catch (Exception exception)
      {
        MessageForm.Show(exception.Message, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        Close();
      }

      Status.TextStatusDialog.HideInfo();
    }

    private void Master_FormClosing(object sender, FormClosingEventArgs e)
    {
      Controller.Disconnect();
    }

    private void VotingListsControl_VotingAction(VotingDescriptor2 voting)
    {
      switch (voting.Status)
      {
        case VotingStatus.New:
          GoShare(voting);
          break;
        case VotingStatus.Sharing:
          GoCheck(voting);
          break;
        case VotingStatus.Voting:
          GoVote(voting);
          break;
        case VotingStatus.Deciphering:
          GoDecipher(voting);
          break;
        case VotingStatus.Finished:
          GoTally(voting);
          break;
      }
    }

    private void GoTally(VotingDescriptor2 voting)
    {
      Status.TextStatusDialog.ShowInfo(Controller);

      try
      {
        IDictionary<Guid, VoteReceiptStatus> voteReceiptStatus;
        VotingResult result = Controller.Tally(voting, out voteReceiptStatus);
      }
      catch (Exception exception)
      {
        MessageForm.Show(exception.Message, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      Status.TextStatusDialog.HideInfo();
    }

    private void GoDecipher(VotingDescriptor2 voting)
    {
      var certificate = Controller.GetAuthorityCertificate(voting);

      if (certificate != null)
      {
        Status.TextStatusDialog.ShowInfo(Controller);

        try
        {
          Controller.Decipher(certificate, voting);
          var votings = Controller.GetVotingList();
          this.votingListsControl.Set(Controller, votings);
        }
        catch (Exception exception)
        {
          MessageForm.Show(exception.Message, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        Status.TextStatusDialog.HideInfo();
      }
    }

    private void GoShare(VotingDescriptor2 voting)
    {
      var certificate = Controller.GetAuthorityCertificate(voting);

      if (certificate != null)
      {
        Status.TextStatusDialog.ShowInfo(Controller);

        try
        {
          Controller.CreateShares(certificate, voting);
          var votings = Controller.GetVotingList();
          this.votingListsControl.Set(Controller, votings);
        }
        catch (Exception exception)
        {
          MessageForm.Show(exception.Message, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        Status.TextStatusDialog.HideInfo();
      }
    }

    private void GoCheck(VotingDescriptor2 voting)
    {
      var certificate = Controller.GetAuthorityCertificate(voting);

      if (certificate != null)
      {
        Status.TextStatusDialog.ShowInfo(Controller);

        try
        {
          Controller.CheckShares(certificate, voting);
          var votings = Controller.GetVotingList();
          this.votingListsControl.Set(Controller, votings);
        }
        catch (Exception exception)
        {
          MessageForm.Show(exception.Message, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        Status.TextStatusDialog.HideInfo();
      }
    }

    private void GoVote(VotingDescriptor2 voting)
    {
      var certificate = Controller.GetVoterCertificateThatCanVote(voting);

      if (certificate == null)
      {
        Create.CreateCertificateDialog.ShowCreateNewVoterCertificate(Controller);
      }
      else
      {
        switch (certificate.Validate(Controller.Status.CertificateStorage))
        {
          case CertificateValidationResult.NoSignature:
            Create.CreateCertificateDialog.TryFixVoterCertificate(Controller, certificate);
            certificate = null;
            break;
          case CertificateValidationResult.NotYetValid:
            MessageForm.Show(
              string.Format("Your certificate id {0} of type {1} is not yet valid.", certificate.Id.ToString(), certificate.TypeText),
              Resources.MessageBoxTitle,
              MessageBoxButtons.OK,
              MessageBoxIcon.Information);
            certificate = null;
            break;
          case CertificateValidationResult.Outdated:
            Controller.DeactiveCertificate(certificate);
            MessageForm.Show(
              string.Format("Your certificate id {0} of type {1} was outdated and therefore deactivated. You must create a new certificate.", certificate.Id.ToString(), certificate.TypeText),
              Resources.MessageBoxTitle,
              MessageBoxButtons.OK,
              MessageBoxIcon.Information);
            Create.CreateCertificateDialog.ShowCreateNewVoterCertificate(Controller);
            certificate = null;
            break;
          case CertificateValidationResult.Revoked:
            Controller.DeactiveCertificate(certificate);
            MessageForm.Show(
              string.Format("Your certificate id {0} of type {1} was revoked and therefore deactivated. You must create a new certificate.", certificate.Id.ToString(), certificate.TypeText),
              Resources.MessageBoxTitle,
              MessageBoxButtons.OK,
              MessageBoxIcon.Information);
            Create.CreateCertificateDialog.ShowCreateNewVoterCertificate(Controller);
            certificate = null;
            break;
          case CertificateValidationResult.Valid:
            break;
          default:
            MessageForm.Show(
              string.Format("Your certificate id {0} of type {1} is invalid. It's status is {2}", certificate.Id.ToString(), certificate.TypeText, certificate.Validate(Controller.Status.CertificateStorage).ToString()),
              Resources.MessageBoxTitle,
              MessageBoxButtons.OK,
              MessageBoxIcon.Information);
            certificate = null;
            break;
        }
      }

      if (certificate != null)
      {
        Vote.VotingDialog.ShowVoting(Controller, certificate, voting);
      }
    }

    private void createNewToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Create.CreateCertificateDialog.ShowCreateNewCertificate(Controller);
    }

    private void refreshToolStripMenuItem_Click(object sender, EventArgs e)
    {
      RefreshVotings();
    }

    private void RefreshVotings()
    {
      Status.TextStatusDialog.ShowInfo(Controller);

      try
      {
        var votings = Controller.GetVotingList();
        this.votingListsControl.Set(Controller, votings);
      }
      catch (Exception exception)
      {
        MessageForm.Show(exception.Message, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }

      Status.TextStatusDialog.HideInfo();
    }

    private void Master_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.F5)
      {
        RefreshVotings();
      }
    }
  }
}
