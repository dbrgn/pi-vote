﻿/*
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

namespace Pirate.PiVote.Client
{
  public partial class AuthorityListVotingsItem : WizardItem
  {
    private bool run;
    private Exception exception;
    private bool accept;
    private VotingClient.VotingDescriptor votingDescriptor;
    private IEnumerable<VotingClient.VotingDescriptor> votings;

    public AuthorityListVotingsItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      return null;
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

    public override void Begin()
    {
      this.run = true;

      Status.VotingClient.GetVotingList(Status.CertificateStorage, GetVotingListCompleted);

      while (this.run)
      {
        Status.UpdateProgress();
        Application.DoEvents();
        Thread.Sleep(1);
      }

      Status.UpdateProgress();

      if (this.exception == null)
      {
        if (this.votings != null)
        {
          foreach (VotingClient.VotingDescriptor voting in this.votings)
          {
            ListViewItem item = new ListViewItem(voting.Title);
            item.SubItems.Add(voting.Status.Text());
            item.Tag = voting;
            this.votingList.Items.Add(item);
          }
        }
      }
      else
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
      }

      this.votingList.Enabled = true;
    }

    private void GetVotingListCompleted(IEnumerable<VotingClient.VotingDescriptor> votingList, Exception exception)
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
        VotingClient.VotingDescriptor voting = (VotingClient.VotingDescriptor)item.Tag;

        this.createSharesButton.Enabled = 
          voting.Status == VotingStatus.New && 
          !voting.AuthoritiesDone.Contains(Status.Certificate.Id);
        this.checkSharesButton.Enabled = 
          voting.Status == VotingStatus.Sharing && 
          !voting.AuthoritiesDone.Contains(Status.Certificate.Id);
        this.decipherButton.Enabled = 
          voting.Status == VotingStatus.Deciphering && 
          !voting.AuthoritiesDone.Contains(Status.Certificate.Id);
      }
      else
      {
        this.createSharesButton.Enabled = false;
        this.checkSharesButton.Enabled = false;
        this.decipherButton.Enabled = false;
      }
    }

    private void createSharesButton_Click(object sender, EventArgs e)
    {
      if (this.votingList.SelectedIndices.Count > 0)
      {
        ListViewItem item = this.votingList.SelectedItems[0];
        VotingClient.VotingDescriptor voting = (VotingClient.VotingDescriptor)item.Tag;

        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Title = Resources.AuthoritySaveData;
        dialog.OverwritePrompt = true;
        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        dialog.CheckPathExists = true;
        dialog.Filter = Files.AuthorityDataFileFilter;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          this.createSharesButton.Enabled = false;
          this.votingList.Enabled = false;

          this.run = true;
          OnUpdateWizard();

          Status.VotingClient.CreateSharePart(voting.Id, (AuthorityCertificate)Status.Certificate, dialog.FileName, CreateSharesCompleteCallBack);

          while (this.run)
          {
            Status.UpdateProgress();
            Application.DoEvents();
            Thread.Sleep(1);
          }

          Status.UpdateProgress();

          if (this.exception == null)
          {
            item.Tag = this.votingDescriptor;
            item.SubItems[1].Text = this.votingDescriptor.Status.Text();
            votingList_SelectedIndexChanged(this.votingList, new EventArgs());

            Status.SetMessage(Resources.AuthorityCreateSharesDone, MessageType.Success);
          }
          else
          {
            Status.SetMessage(this.exception.Message, MessageType.Error);
          }

          Status.UpdateProgress();
          this.votingList.Enabled = true;
          OnUpdateWizard();
        }
      }
    }

    private void CreateSharesCompleteCallBack(VotingClient.VotingDescriptor votingDescriptor, Exception exception)
    {
      this.exception = exception;
      this.votingDescriptor = votingDescriptor;
      this.run = false;
    }

    private void checkSharesButton_Click(object sender, EventArgs e)
    {
      if (this.votingList.SelectedIndices.Count > 0)
      {
        ListViewItem item = this.votingList.SelectedItems[0];
        VotingClient.VotingDescriptor voting = (VotingClient.VotingDescriptor)item.Tag;

        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Title = Resources.AuthorityLoadData;
        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        dialog.CheckPathExists = true;
        dialog.CheckFileExists = true;
        dialog.Filter = Files.AuthorityDataFileFilter;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          this.checkSharesButton.Enabled = false;
          this.votingList.Enabled = false;

          this.run = true;
          OnUpdateWizard();

          Status.VotingClient.CheckShares(voting.Id, (AuthorityCertificate)Status.Certificate, dialog.FileName, CheckSharesCompleteCallBack);

          while (this.run)
          {
            Status.UpdateProgress();
            Application.DoEvents();
            Thread.Sleep(1);
          }

          Status.UpdateProgress();

          if (this.exception == null)
          {
            item.Tag = this.votingDescriptor;
            item.SubItems[1].Text = this.votingDescriptor.Status.Text();
            votingList_SelectedIndexChanged(this.votingList, new EventArgs());

            if (this.accept)
            {
              Status.SetMessage(Resources.AuthorityCheckSharesAccept, MessageType.Success);
            }
            else
            {
              Status.SetMessage(Resources.AuthorityCheckSharesDecline, MessageType.Warning);
            }
          }
          else
          {
            Status.SetMessage(this.exception.Message, MessageType.Error);
          }

          Status.UpdateProgress();
          this.votingList.Enabled = true;
          OnUpdateWizard();
        }
      }
    }

    private void CheckSharesCompleteCallBack(VotingClient.VotingDescriptor votingDescriptor, bool accept, Exception exception)
    {
      this.exception = exception;
      this.votingDescriptor = votingDescriptor;
      this.accept = accept;
      this.run = false;
    }

    private void decipherButton_Click(object sender, EventArgs e)
    {
      if (this.votingList.SelectedIndices.Count > 0)
      {
        ListViewItem item = this.votingList.SelectedItems[0];
        VotingClient.VotingDescriptor voting = (VotingClient.VotingDescriptor)item.Tag;

        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Title = Resources.AuthorityLoadData;
        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        dialog.CheckPathExists = true;
        dialog.CheckFileExists = true;
        dialog.Filter = Files.AuthorityDataFileFilter;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          this.checkSharesButton.Enabled = false;
          this.votingList.Enabled = false;

          this.run = true;
          OnUpdateWizard();

          Status.VotingClient.CreateDeciphers(voting.Id, (AuthorityCertificate)Status.Certificate, dialog.FileName, CreateDeciphersCompleteCallBack);

          while (this.run)
          {
            Status.UpdateProgress();
            Application.DoEvents();
            Thread.Sleep(1);
          }

          Status.UpdateProgress();

          if (this.exception == null)
          {
            item.Tag = this.votingDescriptor;
            item.SubItems[1].Text = this.votingDescriptor.Status.Text();
            votingList_SelectedIndexChanged(this.votingList, new EventArgs());

            Status.SetMessage(Resources.AuthorityDecipherDone, MessageType.Success);
          }
          else
          {
            Status.SetMessage(this.exception.Message, MessageType.Error);
          }

          Status.UpdateProgress();
          this.votingList.Enabled = true;
          OnUpdateWizard();
        }
      }
    }

    private void CreateDeciphersCompleteCallBack(VotingClient.VotingDescriptor votingDescriptor, Exception exception)
    {
      this.exception = exception;
      this.votingDescriptor = votingDescriptor;
      this.run = false;
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.createSharesButton.Text = Resources.AuthorityCreateShares;
      this.checkSharesButton.Text = Resources.AuthorityCheckShares;
      this.decipherButton.Text = Resources.AuthorityDecipher;
      this.titleColumnHeader.Text = Resources.AuthorityListTitle;
      this.statusColumnHeader.Text = Resources.AuthorityListStatus;
    }
  }
}