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
using System.Globalization;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Serialization;

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
      SetDefaultLanguage();
      Show();

      Controller = new CircleController();

      Status.TextStatusDialog.ShowInfo(Controller, this);

      try
      {
        Controller.Prepare();
        Controller.CheckUpdate();
        Controller.LoadCertificates();
        var votings = Controller.GetVotingList();
        this.votingListsControl.Set(Controller, votings);
      }
      catch (Exception exception)
      {
        MessageForm.Show(exception.ToString(), Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
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
      Status.TextStatusDialog.ShowInfo(Controller, this);
      
      try
      {
        IDictionary<Guid, VoteReceiptStatus> voteReceiptStatus;
        var votingResult = Controller.Tally(voting, out voteReceiptStatus);
        Result.ResultDisplayDialog.ShowResult(votingResult, voteReceiptStatus);
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
        Status.TextStatusDialog.ShowInfo(Controller, this);

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

    private void Decipher(VotingDescriptor2 voting, AuthorityCertificate certificate)
    {
    }

    private void GoShare(VotingDescriptor2 voting)
    {
      var certificate = Controller.GetAuthorityCertificate(voting);

      if (certificate != null)
      {
        Status.TextStatusDialog.ShowInfo(Controller, this);

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
        Status.TextStatusDialog.ShowInfo(Controller, this);

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

        Status.TextStatusDialog.ShowInfo(Controller, this);
        var votings = Controller.GetVotingList();
        this.votingListsControl.Set(Controller, votings);
        Status.TextStatusDialog.HideInfo();
      }
    }

    private void CreateCertificateMenu_Click(object sender, EventArgs e)
    {
      Create.CreateCertificateDialog.ShowCreateNewCertificate(Controller);
    }

    private void RefreshVotingsMenu_Click(object sender, EventArgs e)
    {
      RefreshVotings();
    }

    private void RefreshVotings()
    {
      try
      {
        Status.TextStatusDialog.ShowInfo(Controller, this);
        var votings = Controller.GetVotingList();
        this.votingListsControl.Set(Controller, votings);
        Status.TextStatusDialog.HideInfo();
      }
      catch (Exception exception)
      {
        MessageForm.Show(exception.Message, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
    }

    private void Master_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.F5)
      {
        RefreshVotings();
      }
    }

    private void ReloadCertificateMenu_Click(object sender, EventArgs e)
    {
      Status.TextStatusDialog.ShowInfo(Controller, this);

      try
      {
        Controller.LoadCertificates();
      }
      catch (Exception exception)
      {
        MessageForm.Show(exception.Message, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        Close();
      }

      Status.TextStatusDialog.HideInfo();
    }

    private void EnglishMenu_Click(object sender, EventArgs e)
    {
      SetLanguage(Language.English);
    }

    private void GermanMenu_Click(object sender, EventArgs e)
    {
      SetLanguage(Language.German);
    }

    private void FrenchMenu_Click(object sender, EventArgs e)
    {
      SetLanguage(Language.French);
    }

    private void SetLanguage(Language language)
    {
      this.englishMenu.Checked = language == Language.English;
      this.germanMenu.Checked = language == Language.German;
      this.frenchMenu.Checked = language == Language.French;

      Resources.Culture = language.ToCulture();
      LibraryResources.Culture = language.ToCulture();
      GuiResources.Culture = language.ToCulture();
      UpdateLanguage();
    }

    private void SetDefaultLanguage()
    {
      if (CultureInfo.CurrentCulture.Name.StartsWith("de"))
      {
        SetLanguage(Language.German);
      }
      else if (CultureInfo.CurrentCulture.Name.StartsWith("fr"))
      {
        SetLanguage(Language.French);
      }
      else
      {
        SetLanguage(Language.English);
      }
    }

    private void UpdateLanguage()
    {
      Text = Resources.MessageBoxTitle;

      this.votingsMenu.Text = Resources.MenuVotings;
      this.refreshVotingsMenu.Text = Resources.MenuVotingsRefresh;

      this.certificatesMenu.Text = Resources.MenuCertificates;
      this.createCertificateMenu.Text = Resources.MenuCertificatesCreateNew;
      this.resumeCreationMenu.Text = Resources.MenuCertificateResumeCreation;
      this.reloadCertificateMenu.Text = Resources.MenuCertificatesReload;

      this.languageMenu.Text = Resources.MenuLanguage;

      this.votingListsControl.UpdateLanguage();
    }

    private void resumeCreationMenu_Click(object sender, EventArgs e)
    {
      var certificates = Controller.GetVoterCertificates();
      var resumeCertificate = certificates
        .Where(certificate => certificate.Validate(Controller.Status.CertificateStorage) == CertificateValidationResult.NoSignature).FirstOrDefault();

      if (resumeCertificate != null)
      {
        Create.CreateCertificateDialog.TryFixVoterCertificate(Controller, resumeCertificate);
      }
      else
      {
        MessageForm.Show(Resources.MenuCertificateResumeCreationNothing, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }
  }
}
