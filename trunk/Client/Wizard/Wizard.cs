/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Client
{
  public partial class Wizard : Form
  {
    private WizardItem item;
    private WizardStatus status;

    public Wizard()
    {
      InitializeComponent();
      UpdateLanguage();
    }

    public void UpdateLanguage()
    {
      Text = Resources.PiVoteClient;
      this.previouseButton.Text = Resources.WizardPreviousButton;
      this.nextButton.Text = Resources.WizardNextButton;
      this.cancelButton.Text = Resources.WizardCancelButton;

      if (this.item != null)
      {
        this.item.UpdateLanguage();
      }
    }

    private void Wizard_Load(object sender, EventArgs e)
    {
      this.status = new WizardStatus(this.message1, this.progress1);
      this.status.CertificateStorage = new CertificateStorage();
      this.status.CertificateStorage.LoadRoot();
      this.status.UpdateProgress();

      this.status.VotingClient = new VotingClient(this.status.CertificateStorage);

      this.Height = 600;
      this.Width = 800;

      CenterToScreen();

      SetItem(new StartItem());
    }

    protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
    {
      base.OnClosing(e);

      if (this.status != null)
      {
        if (this.status.VotingClient != null)
        {
          this.status.VotingClient.Close();
        }
      }
    }

    private void SetItem(WizardItem item)
    {
      if (this.item != null)
      {
        this.item.UpdateWizard -= new EventHandler(item_UpdateWizard);
        this.item.NextStep -= new EventHandler(item_NextStep);
        this.item.ChangeLanguage -= new EventHandler(item_ChangeLanguage);
        this.itemPanel.Controls.Remove(this.item);
      }

      this.item = item;
      this.item.Status = this.status;

      this.itemPanel.Controls.Add(this.item);

      this.item.Dock = DockStyle.Fill;

      this.previouseButton.Enabled = this.item.CanPrevious;
      this.nextButton.Enabled = this.item.CanNext;
      this.cancelButton.Enabled = this.item.CanCancel;
      this.item.UpdateLanguage();

      this.item.UpdateWizard += new EventHandler(item_UpdateWizard);
      this.item.NextStep += new EventHandler(item_NextStep);
      this.item.ChangeLanguage += new EventHandler(item_ChangeLanguage);

      Refresh();
      Application.DoEvents();

      this.item.Begin();
    }

    private void item_ChangeLanguage(object sender, EventArgs e)
    {
      UpdateLanguage();
    }

    private void item_NextStep(object sender, EventArgs e)
    {
      SetItem(this.item.Next());
    }

    private void item_UpdateWizard(object sender, EventArgs e)
    {
      this.previouseButton.Enabled = this.item.CanPrevious;
      this.nextButton.Enabled = this.item.CanNext;
      this.cancelButton.Enabled = this.item.CanCancel;
    }

    private void itemPanel_Paint(object sender, PaintEventArgs e)
    {

    }

    private void previouseButton_Click(object sender, EventArgs e)
    {
      WizardItem item = this.item.Previous();

      if (item != null)
      {
        SetItem(item);
      }
    }

    private void nextButton_Click(object sender, EventArgs e)
    {
      WizardItem item = this.item.Next();

      if (item != null)
      {
        SetItem(item);
      }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      WizardItem item = this.item.Cancel();

      if (item != null)
      {
        SetItem(item);
      }
      else
      {
        Close();
      }
    }
  }
}
