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

namespace Pirate.PiVote.Client
{
  public partial class AuthorityListVotingsItem : WizardItem
  {
    private bool run;
    private bool success;
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
        VotingClient.Operation operation = Status.VotingClient.CurrentOperation;
        if (operation != null)
        {
          this.progressLabel.Text = operation.Text;
          this.progressBar.Value = Convert.ToInt32(100d * operation.Progress);
        }

        Application.DoEvents();
        Thread.Sleep(1);
      }

      if (this.votings != null)
      {
        foreach (VotingClient.VotingDescriptor voting in this.votings)
        {
          ListViewItem item = new ListViewItem(voting.Title);
          item.SubItems.Add(voting.Status.ToString());
          item.Tag = voting;
          this.votingList.Items.Add(item);
        }
      }

      this.progressBar.Value = this.progressBar.Maximum;
      this.votingList.Enabled = true;
    }

    private void GetVotingListCompleted(IEnumerable<VotingClient.VotingDescriptor> votingList, Exception exception)
    {
      if (exception == null)
      {
        this.votings = votingList;
      }
      else
      {
        MessageBox.Show(exception.ToString());
      }

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
        dialog.Title = "Save Authority Data";
        dialog.OverwritePrompt = true;
        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        dialog.CheckPathExists = true;
        dialog.Filter = "Pi-Vote Certificate|*.pi-auth";

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          this.createSharesButton.Enabled = false;
          this.votingList.Enabled = false;

          this.run = true;
          OnUpdateWizard();

          Status.VotingClient.CreateSharePart(voting.Id, (AuthorityCertificate)Status.Certificate, dialog.FileName, CreateSharesCompleteCallBack);

          while (this.run)
          {
            VotingClient.Operation operation = Status.VotingClient.CurrentOperation;
            if (operation != null)
            {
              this.progressLabel.Text = operation.Text;
              this.progressBar.Value = Convert.ToInt32(100d * operation.Progress);
            }

            Application.DoEvents();
            Thread.Sleep(1);
          }

          if (this.success)
          {
            item.Tag = this.votingDescriptor;
            item.SubItems[1].Text = this.votingDescriptor.Status.ToString();
            votingList_SelectedIndexChanged(this.votingList, new EventArgs());

            MessageBox.Show("Shares created and uploaded them to server.", "Pi-Vote,", MessageBoxButtons.OK, MessageBoxIcon.Information);
          }

          this.votingList.Enabled = true;
          OnUpdateWizard();
        }
      }
    }

    private void CreateSharesCompleteCallBack(VotingClient.VotingDescriptor votingDescriptor, Exception exception)
    {
      this.success = exception == null;
      this.votingDescriptor = votingDescriptor;

      if (exception != null)
      {
        MessageBox.Show(exception.ToString());
      }

      this.run = false;
    }

    private void checkSharesButton_Click(object sender, EventArgs e)
    {
      if (this.votingList.SelectedIndices.Count > 0)
      {
        ListViewItem item = this.votingList.SelectedItems[0];
        VotingClient.VotingDescriptor voting = (VotingClient.VotingDescriptor)item.Tag;

        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Title = "Load Authority Data";
        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        dialog.CheckPathExists = true;
        dialog.CheckFileExists = true;
        dialog.Filter = "Pi-Vote Certificate|*.pi-auth";

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          this.checkSharesButton.Enabled = false;
          this.votingList.Enabled = false;

          this.run = true;
          OnUpdateWizard();

          Status.VotingClient.CheckShares(voting.Id, (AuthorityCertificate)Status.Certificate, dialog.FileName, CheckSharesCompleteCallBack);

          while (this.run)
          {
            VotingClient.Operation operation = Status.VotingClient.CurrentOperation;
            if (operation != null)
            {
              this.progressLabel.Text = operation.Text;
              this.progressBar.Value = Convert.ToInt32(100d * operation.Progress);
            }

            Application.DoEvents();
            Thread.Sleep(1);
          }

          if (this.success)
          {
            item.Tag = this.votingDescriptor;
            item.SubItems[1].Text = this.votingDescriptor.Status.ToString();
            votingList_SelectedIndexChanged(this.votingList, new EventArgs());

            if (this.accept)
            {
              MessageBox.Show("Shares where verified and accepted. Our answer was uploaded to server.", "Pi-Vote,", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
              MessageBox.Show("Shares where verified but not accepted. Our answer was uploaded to server.", "Pi-Vote,", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
          }

          this.votingList.Enabled = true;
          OnUpdateWizard();
        }
      }
    }

    private void CheckSharesCompleteCallBack(VotingClient.VotingDescriptor votingDescriptor, bool accept, Exception exception)
    {
      this.success = exception == null;
      this.votingDescriptor = votingDescriptor;
      this.accept = accept;

      if (exception != null)
      {
        MessageBox.Show(exception.ToString());
      }

      this.run = false;
    }

    private void decipherButton_Click(object sender, EventArgs e)
    {
      if (this.votingList.SelectedIndices.Count > 0)
      {
        ListViewItem item = this.votingList.SelectedItems[0];
        VotingClient.VotingDescriptor voting = (VotingClient.VotingDescriptor)item.Tag;

        OpenFileDialog dialog = new OpenFileDialog();
        dialog.Title = "Load Authority Data";
        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        dialog.CheckPathExists = true;
        dialog.CheckFileExists = true;
        dialog.Filter = "Pi-Vote Certificate|*.pi-auth";

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          this.checkSharesButton.Enabled = false;
          this.votingList.Enabled = false;

          this.run = true;
          OnUpdateWizard();

          Status.VotingClient.CreateDeciphers(voting.Id, (AuthorityCertificate)Status.Certificate, dialog.FileName, CreateDeciphersCompleteCallBack);

          while (this.run)
          {
            VotingClient.Operation operation = Status.VotingClient.CurrentOperation;
            if (operation != null)
            {
              this.progressLabel.Text = operation.Text;
              this.progressBar.Value = Convert.ToInt32(100d * operation.Progress);
            }

            Application.DoEvents();
            Thread.Sleep(1);
          }

          if (this.success)
          {
            item.Tag = this.votingDescriptor;
            item.SubItems[1].Text = this.votingDescriptor.Status.ToString();
            votingList_SelectedIndexChanged(this.votingList, new EventArgs());

            MessageBox.Show("Partial deciphers where calculated and uploaded to the server.", "Pi-Vote,", MessageBoxButtons.OK, MessageBoxIcon.Information);
          }

          this.votingList.Enabled = true;
          OnUpdateWizard();
        }
      }
    }

    private void CreateDeciphersCompleteCallBack(VotingClient.VotingDescriptor votingDescriptor, Exception exception)
    {
      this.success = exception == null;
      this.votingDescriptor = votingDescriptor;

      if (exception != null)
      {
        MessageBox.Show(exception.ToString());
      }

      this.run = false;
    }
  }
}
