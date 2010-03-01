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
    private Thread initThread;
    private VotingParameters votingParameters;
    private List<AuthorityCertificate> authorityCertificates;

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
        Application.DoEvents();
        Thread.Sleep(1);
      }

      if (this.authorityCertificates != null)
      {
        this.authorityCertificates.ForEach(certificate => this.authority0List.Items.Add(string.Format("{0}, {1}", certificate.Id.ToString(), certificate.FullName)));
        this.authorityCertificates.ForEach(certificate => this.authority1List.Items.Add(string.Format("{0}, {1}", certificate.Id.ToString(), certificate.FullName)));
        this.authorityCertificates.ForEach(certificate => this.authority2List.Items.Add(string.Format("{0}, {1}", certificate.Id.ToString(), certificate.FullName)));
        this.authorityCertificates.ForEach(certificate => this.authority3List.Items.Add(string.Format("{0}, {1}", certificate.Id.ToString(), certificate.FullName)));
        this.authorityCertificates.ForEach(certificate => this.authority4List.Items.Add(string.Format("{0}, {1}", certificate.Id.ToString(), certificate.FullName)));
      }

      SetEnable(true);
    }

    private void GetAuthorityCertificatesCompleted(IEnumerable<AuthorityCertificate> authorityCertificates, Exception exception)
    {
      if (exception == null)
      {
        this.authorityCertificates = new List<AuthorityCertificate>(authorityCertificates);
      }
      else
      {
        MessageBox.Show(exception.ToString());
      }

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
      this.questionBox.Enabled = enable;
      this.optionTextAddBox.Enabled = enable;
      this.optionListView.Enabled = enable;
      this.optionAddButton.Enabled = enable;
      this.optionRemoveButton.Enabled = enable;
      this.optionNumberUpDown.Enabled = enable;
      this.votingFromPicker.Enabled = enable;
      this.votingUntilPicker.Enabled = enable;
      this.optionTextAddBox.Enabled = enable;
      this.optionDescriptionAddBox.Enabled = enable;

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

      this.votingParameters =
        new VotingParameters(
        this.titleBox.Text,
        this.descriptionBox.Text,
        this.questionBox.Text,
        this.votingFromPicker.Value.Date > DateTime.Now ? this.votingFromPicker.Value.Date : DateTime.Now,
        this.votingUntilPicker.Value.Date.AddDays(1).AddMinutes(-1));

      foreach (ListViewItem item in this.optionListView.Items)
      {
        this.votingParameters.AddOption(new Option(item.Text, item.SubItems[1].Text));
      }

      this.initThread = new Thread(Init);
      this.initThread.Start();

      DateTime lastUpdate = DateTime.Now;
      this.createProgress.Maximum = 10;
      this.createMessage.Text = "Searching safe prime number for crypto.";

      while (this.run)
      {
        if (DateTime.Now.Subtract(lastUpdate).TotalSeconds > 0.25d)
        {
          this.createProgress.Value = (this.createProgress.Value + 1) % this.createProgress.Maximum;
          lastUpdate = DateTime.Now;
        }

        Application.DoEvents();
        Thread.Sleep(1);
      }

      this.run = true;
      this.createMessage.Text = "Creating voting on server.";
      this.createProgress.Value = 0;
      Application.DoEvents();

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

      this.createProgress.Value = this.createProgress.Maximum;
      this.createMessage.Text = "Voting procedure created.";
      Application.DoEvents();

      OnUpdateWizard();
    }

    private void CreateVotingCompleted(Exception exception)
    {
      if (exception != null)
      {
        MessageBox.Show(exception.Message);
      }

      this.run = false;
    }

    private void Init()
    {
      this.votingParameters.Initialize(Convert.ToInt32(this.optionNumberUpDown.Value));
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

      enable &= !this.titleBox.Text.IsNullOrEmpty();
      enable &= !this.descriptionBox.Text.IsNullOrEmpty();
      enable &= !this.questionBox.Text.IsNullOrEmpty();
      enable &= this.optionListView.Items.Count >= 2;
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

      this.createButton.Enabled = enable;
    }

    private void optionAddButton_Click(object sender, EventArgs e)
    {
      ListViewItem item = new ListViewItem(this.optionTextAddBox.Text);
      item.SubItems.Add(this.optionDescriptionAddBox.Text);
      this.optionListView.Items.Add(item);

      this.optionTextAddBox.Text = string.Empty;
      this.optionDescriptionAddBox.Text = string.Empty;

      this.optionNumberUpDown.Enabled = this.optionListView.Items.Count >= 2;

      if (this.optionListView.Items.Count >= 2)
      {
        this.optionNumberUpDown.Maximum = this.optionListView.Items.Count - 1;
      }
      
      CheckEnable();
    }

    private void optionRemoveButton_Click(object sender, EventArgs e)
    {
      if (this.optionListView.SelectedIndices.Count >= 0)
      {
        this.optionListView.Items.RemoveAt(this.optionListView.SelectedIndices[0]);
        this.optionNumberUpDown.Enabled = this.optionListView.Items.Count >= 2;

        if (this.optionListView.Items.Count >= 2)
        {
          this.optionNumberUpDown.Maximum = this.optionListView.Items.Count - 1;
        }

        CheckEnable();
      }
    }

    private void optionList_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.optionRemoveButton.Enabled = this.optionListView.SelectedIndices.Count >= 0;
    }

    private void optionAddBox_TextChanged(object sender, EventArgs e)
    {
      this.optionAddButton.Enabled = !this.optionTextAddBox.Text.IsNullOrEmpty() && !this.optionDescriptionAddBox.Text.IsNullOrEmpty();
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

    private void optionDescriptionAddBox_TextChanged(object sender, EventArgs e)
    {
      this.optionAddButton.Enabled = !this.optionTextAddBox.Text.IsNullOrEmpty() && !this.optionDescriptionAddBox.Text.IsNullOrEmpty();
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
  }
}
