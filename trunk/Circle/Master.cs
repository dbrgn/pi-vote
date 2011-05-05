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
using System.IO;
using System.Linq;
using System.Net.Sockets;
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
      Controller = new CircleController();

      Status.InitScreen.ShowInfo(Controller, this);

      try
      {
        Controller.Prepare();
      }
      catch (InvalidOperationException)
      {
        MessageForm.Show(Resources.CannotConnectMessage,  Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        Close();
        return;
      }
      catch (SocketException)
      {
        MessageForm.Show(Resources.CannotConnectMessage,  Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        Close();
        return;
      }
      catch (Exception exception)
      {
        Error.ErrorDialog.ShowError(exception);
        Close();
        return;
      }

      try
      {
        Controller.CheckUpdate();
        Controller.LoadCertificates();
        RefreshInternal();
      }
      catch (Exception exception)
      {
        Error.ErrorDialog.ShowError(exception);
        Close();
        return;
      }

      Status.InitScreen.HideInfo();

      Show();
    }

    private void Master_FormClosing(object sender, FormClosingEventArgs e)
    {
      Controller.Disconnect();
    }

    private void VotingListsControl_VotingAction(VotingActionType type, VotingDescriptor2 voting)
    {
      switch (type)
      {
        case VotingActionType.Type1:
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
            case VotingStatus.Offline:
              GoTally(voting);
              break;
          }
          break;
        case VotingActionType.Type2:
          switch (voting.Status)
          {
            case VotingStatus.New:
            case VotingStatus.Sharing:
            case VotingStatus.Ready:
              GoDelete(voting);
              break;
            case VotingStatus.Offline:
              GoDeleteStored(voting);
              break;
            case VotingStatus.Finished:
              GoDownload(voting);
              break;
          }
          break;
      }
    }

    private void GoDownload(VotingDescriptor2 voting)
    {
      Status.TextStatusDialog.ShowInfo(Controller, this);

      try
      {
        Controller.Download(voting);

        var votings = Controller.GetVotingList();
        this.votingListsControl.Set(Controller, votings);
      }
      catch (Exception exception)
      {
        Error.ErrorDialog.ShowError(exception);
      }

      Status.TextStatusDialog.HideInfo();
    }

    private void GoDelete(VotingDescriptor2 voting)
    {
      Status.TextStatusDialog.ShowInfo(Controller, this);

      var adminCertificate = Controller.GetAdminCertificate();

      if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(adminCertificate, GuiResources.UnlockActionDelete))
      {
        try
        {
          if (adminCertificate != null)
          {
            Controller.Delete(voting, adminCertificate);

            var votings = Controller.GetVotingList();
            this.votingListsControl.Set(Controller, votings);
          }
        }
        catch (Exception exception)
        {
          Error.ErrorDialog.ShowError(exception);
        }
        finally
        {
          adminCertificate.Lock();
          Status.TextStatusDialog.HideInfo();
        }
      }
    }

    private void GoDeleteStored(VotingDescriptor2 voting)
    {
      try
      {
        if (!voting.OfflinePath.IsNullOrEmpty() &&
            Directory.Exists(voting.OfflinePath))
        {
          Directory.Delete(voting.OfflinePath, true);

          RefreshVotings();
        }
      }
      catch (Exception exception)
      {
        Error.ErrorDialog.ShowError(exception);
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
        Error.ErrorDialog.ShowError(exception);
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
          Error.ErrorDialog.ShowError(exception);
        }

        Status.TextStatusDialog.HideInfo();
      }
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
          Error.ErrorDialog.ShowError(exception);
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
          Error.ErrorDialog.ShowError(exception);
        }

        Status.TextStatusDialog.HideInfo();
      }
    }

    private void GoVote(VotingDescriptor2 voting)
    {
      var certificate = Controller.GetVoterCertificateThatCanVote(voting);

      if (certificate != null)
      {
        Vote.VotingDialog.ShowVoting(Controller, certificate, voting);

        RefreshVotings();
      }
    }

    private void CreateCertificateMenu_Click(object sender, EventArgs e)
    {
      Create.CreateCertificateDialog.ShowCreateNewCertificate(Controller);
      Controller.LoadCertificates();
      RefreshInternal();
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

        RefreshInternal();
      }
      catch (Exception exception)
      {
        Error.ErrorDialog.ShowError(exception);
      }
      finally
      {
        Status.TextStatusDialog.HideInfo();
      }
    }

    private void RefreshInternal()
    {
      bool isAdmin = Controller.GetAdminCertificate() != null;
      this.createVotingMenu.Visible = isAdmin;
      this.downloadSignatureRequestsMenu.Visible = isAdmin;
      this.uploadSignatureResponsesMenu.Visible = isAdmin;
      this.uploadCertificateStorageMenu.Visible = isAdmin;

      var votings = Controller.GetVotingList();
      this.votingListsControl.Set(Controller, votings);
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

        RefreshInternal();
      }
      catch (Exception exception)
      {
        Error.ErrorDialog.ShowError(exception);
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
      this.createVotingMenu.Text = Resources.MenuCreateVoting;

      this.certificatesMenu.Text = Resources.MenuCertificates;
      this.createCertificateMenu.Text = Resources.MenuCertificatesCreateNew;
      this.resumeCreationMenu.Text = Resources.MenuCertificateResumeCreation;
      this.reloadCertificateMenu.Text = Resources.MenuCertificatesReload;
      this.manageToolStripMenuItem.Text = Resources.MenuManage;

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

    private void manageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Certificates.CertificateManagerDialog.ShowCertificates(Controller);

      RefreshVotings();
    }

    private void createVotingMenu_Click(object sender, EventArgs e)
    {
      CreateVoting.CreateVotingDialog.ShowCreateVoting(Controller);

      RefreshVotings();
    }

    private void downloadSignatureRequestsMenu_Click(object sender, EventArgs e)
    {
      SaveFileDialog dialog = new SaveFileDialog();
      dialog.Title = Resources.DownloadSignatureRequestsSaveDialogTitle;
      dialog.CheckPathExists = true;
      dialog.Filter = Files.SignatureRequestFileFilter;
      dialog.FileName = Resources.DownloadSignatureRequestsFileName;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        string savePath = Path.GetDirectoryName(dialog.FileName);
        Status.TextStatusDialog.ShowInfo(Controller, this);

        try
        {
          Controller.DownloadSignatureRequests(savePath);
          MessageForm.Show(Resources.DownloadSignatureRequestsDone, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception exception)
        {
          Error.ErrorDialog.ShowError(exception);
        }

        Status.TextStatusDialog.HideInfo();
      }
    }

    private void uploadSignatureResponsesMenu_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Title = Resources.UploadSignatureResponsesOpenDialogTitle;
      dialog.CheckPathExists = true;
      dialog.CheckFileExists = true;
      dialog.Multiselect = true;
      dialog.Filter = Files.SignatureResponseFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        Status.TextStatusDialog.ShowInfo(Controller, this);

        try
        {
          Controller.UploadSignatureResponses(dialog.FileNames);
          MessageForm.Show(Resources.UploadSignatureResponsesDone, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception exception)
        {
          Error.ErrorDialog.ShowError(exception);
        }

        Status.TextStatusDialog.HideInfo();
      }
    }

    private void uploadCertificateStorageMenu_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Title = Resources.UploadCertificateStorageOpenDialogTitle;
      dialog.CheckPathExists = true;
      dialog.CheckFileExists = true;
      dialog.Multiselect = false;
      dialog.Filter = Files.CertificateStorageFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        Status.TextStatusDialog.ShowInfo(Controller, this);

        try
        {
          Controller.UploadCertificateStorage(dialog.FileName);
          MessageForm.Show(Resources.UploadCertificateStorageDone, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception exception)
        {
          Error.ErrorDialog.ShowError(exception);
        }

        Status.TextStatusDialog.HideInfo();
      }
    }

    private void VotingListsControl_CreateCertificate(object sender, EventArgs e)
    {
      Create.CreateCertificateDialog.ShowCreateNewVoterCertificate(Controller);
      Controller.LoadCertificates();
      RefreshInternal();
    }

    private void VotingListsControl_ResumeCertificate(object sender, EventArgs e)
    {
      var lastCertificate = Controller
        .GetVoterCertificates(0)
        .OrderByDescending(certificate => certificate.CreationDate)
        .FirstOrDefault();
      Create.CreateCertificateDialog.TryFixVoterCertificate(Controller, lastCertificate);
      Controller.LoadCertificates();
      RefreshInternal();
    }
  }
}
