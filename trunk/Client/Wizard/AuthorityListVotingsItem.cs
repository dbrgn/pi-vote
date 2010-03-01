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
          this.loadProgress.Value = Convert.ToInt32(100d * operation.SubProgress);
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

      this.loadProgress.Value = this.loadProgress.Maximum;
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

        this.createSharesButton.Enabled = voting.Status == VotingStatus.New && 
          !voting.AuthoritiesDone.Contains(Status.Certificate.Id);
        this.checkSharesButton.Enabled = voting.Status == VotingStatus.Sharing && 
          !voting.AuthoritiesDone.Contains(Status.Certificate.Id);
        this.decipherButton.Enabled = voting.Status == VotingStatus.Deciphering && 
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

    }

    private void checkSharesButton_Click(object sender, EventArgs e)
    {

    }

    private void decipherButton_Click(object sender, EventArgs e)
    {

    }
  }
}
