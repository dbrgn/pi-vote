/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Client
{
  public partial class AdminChooseItem : WizardItem
  {
    private bool run;
    private Exception exception;
    private IEnumerable<VotingDescriptor> votings;

    public AdminChooseItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      return new CreateVotingItem();
    }

    public override WizardItem Previous()
    {
      return null;
    }

    public override WizardItem Cancel()
    {
      return null;
    }

    public override bool CanCancel
    {
      get { return true; }
    }

    public override bool CanNext
    {
      get { return false; }
    }

    public override bool CancelIsDone
    {
      get { return true; }
    }

    public override void Begin()
    {
      RefreshList();
    }

    public void RefreshList()
    {
      this.run = true;
      OnUpdateWizard();

      Status.VotingClient.GetVotingList(Status.CertificateStorage, Status.DataPath, GetVotingListCompleted);

      while (this.run)
      {
        Status.UpdateProgress();
        Thread.Sleep(10);
      }

      Status.UpdateProgress();

      if (this.exception == null)
      {
        this.votingList.Items.Clear();

        if (this.votings != null)
        {
          foreach (VotingDescriptor voting in this.votings.OrderBy(v => v.VoteFrom))
          {
            ListViewItem item = new ListViewItem(voting.Title.Text);
            item.SubItems.Add(Status.GetGroupName(voting.GroupId));
            item.SubItems.Add(voting.Status.Text());
            item.SubItems.Add(voting.VoteFrom.ToShortDateString());
            item.SubItems.Add(voting.VoteUntil.ToShortDateString());

            switch (voting.Status)
            {
              case VotingStatus.New:
              case VotingStatus.Sharing:
              case VotingStatus.Deciphering:
                item.SubItems.Add(voting.AuthoritiesDone.Count().ToString() + " / " + voting.AuthorityCount.ToString());
                break;
              default:
                item.SubItems.Add(string.Empty);
                break;
            }

            switch (voting.Status)
            {
              case VotingStatus.Voting:
              case VotingStatus.Deciphering:
              case VotingStatus.Finished:
              case VotingStatus.Offline:
                item.SubItems.Add(voting.EnvelopeCount.ToString());
                break;
              default:
                item.SubItems.Add(string.Empty);
                break;
            }

            item.Tag = voting;
            this.votingList.Items.Add(item);
          }
        }

        Status.SetMessage(Resources.VotingListDownloaded, MessageType.Info);
      }
      else
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
      }

      OnUpdateWizard();
      this.votingList.Enabled = true;
    }

    private void GetVotingListCompleted(IEnumerable<VotingDescriptor> votingList, Exception exception)
    {
      this.exception = exception;
      this.votings = votingList;
      this.run = false;
    }

    private void votingList_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.votingList.SelectedIndices.Count > 0)
      {
        ListViewItem item = this.votingList.SelectedItems[0];
        VotingDescriptor voting = (VotingDescriptor)item.Tag;
      }
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.createVotingMenu.Text = Resources.AdminChooseCreateVoting;
      this.downloadSignatureRequestsMenu.Text = Resources.AdminChooseDownloadSignatureRequests;
      this.uploadSignatureResponsesMenu.Text = Resources.AdminChooseUploadSignatureRessponse;
      this.uploadCertificateStorageMenu.Text = Resources.AdminChooseUploadCertificateStorage;

      this.titleColumnHeader.Text = Resources.AuthorityListTitle;
      this.groupColumnHeader.Text = Resources.VotingGroup;
      this.statusColumnHeader.Text = Resources.AuthorityListStatus;
      this.voteFromColumnHeader.Text = Resources.VotingListVoteFrom;
      this.voteUntilColumnHeader.Text = Resources.VotingListVoteUntil;
      this.authorityColumnHeader.Text = Resources.VotingListAuthorities;
      this.envelopesColumnHeader.Text = Resources.VotingListEnvelopes;
    }

    private void downloadSignatureRequestsMenu_Click(object sender, EventArgs e)
    {
      SaveFileDialog dialog = new SaveFileDialog();
      dialog.Title = Resources.SaveSignatureRequestDialog;
      dialog.CheckPathExists = true;
      dialog.Filter = Files.SignatureRequestFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        string savePath = Path.GetDirectoryName(dialog.FileName);

        this.run = true;
        OnUpdateWizard();

        Status.VotingClient.GetSignatureRequests(savePath, GetSignatureRequestsComplete);

        while (this.run)
        {
          Status.UpdateProgress();
          Thread.Sleep(10);
        }

        Status.UpdateProgress();

        if (this.exception != null)
        {
          Status.SetMessage(this.exception.Message, MessageType.Error);
        }

        OnUpdateWizard();
      }
    }

    private void GetSignatureRequestsComplete(Exception exception)
    {
      this.exception = exception;
      this.run = false;
    }

    private void uploadSignatureResponsesMenu_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Title = Resources.OpenSignatureResponseDialog;
      dialog.CheckPathExists = true;
      dialog.CheckFileExists = true;
      dialog.Multiselect = true;
      dialog.Filter = Files.SignatureResponseFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        this.run = true;
        OnUpdateWizard();

        Status.VotingClient.SetSignatureResponses(dialog.FileNames, SetSignatureResponsesComplete);

        while (this.run)
        {
          Status.UpdateProgress();
          Thread.Sleep(10);
        }

        Status.UpdateProgress();

        if (this.exception == null)
        {
          Status.SetMessage(Resources.SignatureResponseUploaded, MessageType.Success);
        }
        else
        {
          Status.SetMessage(this.exception.Message, MessageType.Error);
        }

        OnUpdateWizard();
      }
    }

    private void SetSignatureResponsesComplete(Exception exception)
    {
      this.exception = exception;
      this.run = false;
    }

    private void createVotingMenu_Click(object sender, EventArgs e)
    {
      OnNextStep();
    }

    private void uploadCertificateStorageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Title = Resources.OpenCertificateStorageDialog;
      dialog.CheckPathExists = true;
      dialog.CheckFileExists = true;
      dialog.Multiselect = false;
      dialog.Filter = Files.CertificateStorageFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        this.run = true;
        OnUpdateWizard();

        CertificateStorage certificateStorage = Serializable.Load<CertificateStorage>(dialog.FileName);
        Status.VotingClient.SetCertificateStorage(certificateStorage, SetCertificateStorageComplete);

        while (this.run)
        {
          Status.UpdateProgress();
          Thread.Sleep(10);
        }

        Status.UpdateProgress();

        if (this.exception == null)
        {
          Status.SetMessage(Resources.CertificateStorageUploaded, MessageType.Success);
        }
        else
        {
          Status.SetMessage(this.exception.Message, MessageType.Error);
        }

        OnUpdateWizard();
      }
    }

    private void SetCertificateStorageComplete(Exception exception)
    {
      this.exception = exception;
      this.run = false;
    }

    private void deleteMenu_Click(object sender, EventArgs e)
    {
      if (this.votingList.SelectedIndices.Count > 0)
      {
        ListViewItem item = this.votingList.SelectedItems[0];
        VotingDescriptor voting = (VotingDescriptor)item.Tag;

        if (MessageForm.Show(
          string.Format(Resources.AdminDeleteVotingWarning, voting.Id.ToString(), voting.Title.Text), 
          Resources.MessageBoxTitle, 
          MessageBoxButtons.YesNo, 
          MessageBoxIcon.Question,
          DialogResult.No) 
          == DialogResult.Yes)
        {
          if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(Status.Certificate, Resources.AdminDeleteVotingAction))
          {
            var command = new DeleteVotingRequest.Command(voting.Id);
            var signedCommand = new Signed<DeleteVotingRequest.Command>(command, Status.Certificate);
            this.run = true;

            Status.VotingClient.DeleteVoting(signedCommand, DeleteVotingComplete);

            while (this.run)
            {
              Status.UpdateProgress();
              Thread.Sleep(10);
            }

            Status.UpdateProgress();

            if (this.exception == null)
            {
              Status.SetMessage(Resources.AdminDeleteVotingDone, MessageType.Success);
            }
            else
            {
              Status.SetMessage(this.exception.Message, MessageType.Error);
            }

            item.Remove();

            OnUpdateWizard();
          }
        }
      }
    }

    private void DeleteVotingComplete(Exception exception)
    {
      this.exception = exception;
      this.run = false;
    }

    private void contextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
      if (this.votingList.SelectedIndices.Count > 0)
      {
        ListViewItem item = this.votingList.SelectedItems[0];
        VotingDescriptor voting = (VotingDescriptor)item.Tag;

        switch (voting.Status)
        {
          case VotingStatus.New:
          case VotingStatus.Sharing:
            this.deleteMenu.Enabled = true;
            break;
          default:
            this.deleteMenu.Enabled = false;
            break;
        }
      }
      else
      {
        this.deleteMenu.Enabled = false;
      }
    }

    private void refreshMenu_Click(object sender, EventArgs e)
    {
      RefreshList();
    }

    public override void RefreshData()
    {
      RefreshList();
    }
  }
}
