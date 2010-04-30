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
  public partial class CreateVotingItem : WizardItem
  {
    private bool run;
    private Exception exception;
    private Thread initThread;
    private List<AuthorityCertificate> authorityCertificates;
    private VotingParameters votingParameters;

    public CreateVotingItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      return null;
    }

    public override WizardItem Previous()
    {
      return new AdminChooseItem();
    }

    public override WizardItem Cancel()
    {
      return null;
    }

    public override bool CanCancel
    {
      get { return !this.run; }
    }

    public override bool CanPrevious
    {
      get { return !this.run; }
    }

    public override void Begin()
    {
      this.run = true;
      OnUpdateWizard();
      SetEnable(false);

      Status.VotingClient.GetAuthorityCertificates(GetAuthorityCertificatesCompleted);

      while (this.run)
      {
        Status.UpdateProgress();
        Application.DoEvents();
        Thread.Sleep(1);
      }

      Status.UpdateProgress();

      if (this.exception == null)
      {
        if (this.authorityCertificates != null)
        {
          this.authorityCertificates.ForEach(certificate => this.authority0List.Items.Add(string.Format("{0}, {1}", certificate.Id.ToString(), certificate.FullName)));
          this.authorityCertificates.ForEach(certificate => this.authority1List.Items.Add(string.Format("{0}, {1}", certificate.Id.ToString(), certificate.FullName)));
          this.authorityCertificates.ForEach(certificate => this.authority2List.Items.Add(string.Format("{0}, {1}", certificate.Id.ToString(), certificate.FullName)));
          this.authorityCertificates.ForEach(certificate => this.authority3List.Items.Add(string.Format("{0}, {1}", certificate.Id.ToString(), certificate.FullName)));
          this.authorityCertificates.ForEach(certificate => this.authority4List.Items.Add(string.Format("{0}, {1}", certificate.Id.ToString(), certificate.FullName)));
        }
        else
        {
          Status.SetMessage("No authority certificates from server.", MessageType.Error);
        }
      }
      else
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
      }

      SetEnable(true);
      this.run = false;
      OnUpdateWizard();
    }

    private void GetAuthorityCertificatesCompleted(IEnumerable<AuthorityCertificate> authorityCertificates, Exception exception)
    {
      if (exception == null)
      {
        this.authorityCertificates = new List<AuthorityCertificate>(authorityCertificates);
      }

      this.exception = exception;
      this.run = false;
    }

    private void SetEnable(bool enable)
    {
      this.authority0List.Enabled = enable;
      this.authority1List.Enabled = enable;
      this.authority2List.Enabled = enable;
      this.authority3List.Enabled = enable;
      this.authority4List.Enabled = enable;
      this.titleBox.Enabled = enable;
      this.descriptionBox.Enabled = enable;
      this.votingFromPicker.Enabled = enable;
      this.votingUntilPicker.Enabled = enable;

      if (enable)
      {
        this.createButton.Enabled = false;
      }
      else
      {
        CheckEnable();
      }
    }

    private void createButton_Click(object sender, EventArgs e)
    {
      this.run = true;
      OnUpdateWizard();
      SetEnable(false);

      this.initThread = new Thread(Init);
      this.initThread.Start();

      DateTime lastUpdate = DateTime.Now;
      int progress = 0;
      Status.SetProgress(Resources.CreateVotingSearchPrime, (double)progress / 10d);

      while (this.run)
      {
        if (DateTime.Now.Subtract(lastUpdate).TotalSeconds > 0.25d)
        {
          progress = (progress + 1) % 11;
          Status.SetProgress(Resources.CreateVotingSearchPrime, (double)progress / 10d);
          lastUpdate = DateTime.Now;
        }

        Application.DoEvents();
        Thread.Sleep(1);
      }

      this.run = true;
      Status.SetProgress(Resources.CreateVotingCreating, 0d);
      Application.DoEvents();

      foreach (ListViewItem item in this.questionListView.Items)
      {
        votingParameters.AddQuestion((Question)item.Tag);
      }

      Signed<VotingParameters> signedVotingParameters = new Signed<VotingParameters>(votingParameters, Status.Certificate);
      List<AuthorityCertificate> authorities = new List<AuthorityCertificate>();
      authorities.Add(this.authorityCertificates[this.authority0List.SelectedIndex]);
      authorities.Add(this.authorityCertificates[this.authority1List.SelectedIndex]);
      authorities.Add(this.authorityCertificates[this.authority2List.SelectedIndex]);
      authorities.Add(this.authorityCertificates[this.authority3List.SelectedIndex]);
      authorities.Add(this.authorityCertificates[this.authority4List.SelectedIndex]);

      Status.VotingClient.CreateVoting(signedVotingParameters, authorities, CreateVotingCompleted);

      while (this.run)
      {
        Application.DoEvents();
        Thread.Sleep(1);
      }

      if (this.exception == null)
      {
        Status.SetProgress(Resources.CreateVotingCreated, 1d);
      }
      else
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
      }

      Application.DoEvents();

      OnUpdateWizard();
    }

    private void CreateVotingCompleted(Exception exception)
    {
      this.exception = exception;
      this.run = false;
    }

    private void Init()
    {
      this.votingParameters =
        new VotingParameters(
          this.titleBox.Text,
          this.descriptionBox.Text,
          this.votingFromPicker.Value.Date,
          this.votingUntilPicker.Value.Date);

      this.run = false;
    }

    private void titleBox_TextChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void descriptionBox_TextChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void questionBox_TextChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void CheckEnable()
    {
      bool enable = true;

      enable &= !this.titleBox.IsEmpty;
      enable &= !this.descriptionBox.IsEmpty;
      enable &= this.votingFromPicker.Value.Date >= DateTime.Now.Date;
      enable &= this.votingUntilPicker.Value > this.votingFromPicker.Value;
      enable &= this.authority0List.SelectedIndex >= 0;
      enable &= this.authority1List.SelectedIndex >= 0;
      enable &= this.authority2List.SelectedIndex >= 0;
      enable &= this.authority3List.SelectedIndex >= 0;
      enable &= this.authority4List.SelectedIndex >= 0;

      List<int> indices = new List<int>();
      indices.Add(this.authority0List.SelectedIndex);
      indices.Add(this.authority1List.SelectedIndex);
      indices.Add(this.authority2List.SelectedIndex);
      indices.Add(this.authority3List.SelectedIndex);
      indices.Add(this.authority4List.SelectedIndex);
      enable &= !indices.Any(i => indices.Where(x => x == i).Count() > 1);

      enable &= this.questionListView.Items.Count >= 1;

      this.createButton.Enabled = enable;
    }

    private void votingFromPicker_ValueChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void votingUntilPicker_ValueChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void CreateVotingItem_Load(object sender, EventArgs e)
    {

    }

    private void authority0List_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void authority1List_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void authority2List_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void authority3List_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    private void authority4List_SelectedIndexChanged(object sender, EventArgs e)
    {
      CheckEnable();
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.authoritiesLabel.Text = Resources.CreateVotingAuthorities;
      this.titleLabel.Text = Resources.CreateVotingTitle;
      this.descriptionLabel.Text = Resources.CreateVotingDescription;
      this.votingFromLabel.Text = Resources.CreateVotingOpenFrom;
      this.votingUntilLabel.Text = Resources.CreateVotingOpenUntil;
      this.createButton.Text = Resources.CreateVotingButton;
      this.questionLabel.Text = Resources.CreateVotingQuestions;
      this.textColumnHeader.Text = Resources.CreateVotingQuestionText;
      this.descriptionColumnHeader.Text = Resources.CreateVotingQuestionDescription;
    }

    private void addToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Question question = AddQuestionDialog.ShowQuestion(null);

      if (question != null)
      {
        ListViewItem item = new ListViewItem(question.Text.AllLanguages);
        item.SubItems.Add(question.Description.AllLanguages);
        item.Tag = question;
        this.questionListView.Items.Add(item);

        CheckEnable();
      }
    }

    private void removeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.questionListView.SelectedItems.Count > 0)
      {
        this.questionListView.SelectedItems[0].Remove();

        CheckEnable();
      }
    }

    private void editToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.questionListView.SelectedItems.Count > 0)
      {
        ListViewItem item = this.questionListView.SelectedItems[0];
        Question question = (Question)item.Tag;
        question = AddQuestionDialog.ShowQuestion(question);

        if (question != null)
        {
          item.Text = question.Text.AllLanguages;
          item.SubItems[1].Text = question.Description.AllLanguages;
          item.Tag = question;
        }
      }
    }

    private void questionContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
      this.addToolStripMenuItem.Text = Resources.ContextMenuAdd;
      this.removeToolStripMenuItem.Text = Resources.ContextMenuRemove;
      this.editToolStripMenuItem.Text = Resources.ContextMenuEdit;

      this.removeToolStripMenuItem.Enabled = this.questionListView.SelectedItems.Count > 0;
      this.editToolStripMenuItem.Enabled = this.questionListView.SelectedItems.Count > 0;
    }
  }
}
