﻿/*
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
    private const string RootCertificateFileName = "root.pi-cert";

    private WizardItem item;
    private WizardStatus status;

    public Wizard()
    {
      InitializeComponent();
    }

    private void Wizard_Load(object sender, EventArgs e)
    {
      this.status = new WizardStatus();
      this.status.CertificateStorage = new CertificateStorage();

      if (File.Exists(RootCertificateFileName))
      {
        Certificate rootCertificate = Serializable.Load<Certificate>(RootCertificateFileName);
        this.status.CertificateStorage.AddRoot(rootCertificate);
      }
      else
      {
        MessageBox.Show("The root certificate file is missing.", "Pi-Vote Client", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        Close();
      }

      this.status.VotingClient = new VotingClient(this.status.CertificateStorage);

      CenterToScreen();
      SetItem(new StartItem());
    }

    private void SetItem(WizardItem item)
    {
      if (this.item != null)
      {
        this.item.UpdateWizard -= new EventHandler(item_UpdateWizard);
        this.itemPanel.Controls.Remove(this.item);
      }

      this.item = item;
      this.item.Status = this.status;

      this.itemPanel.Controls.Add(this.item);

      this.item.Width = 700;
      this.item.Left = this.itemPanel.Width / 2 - this.item.Width / 2;

      this.item.Height = 500;
      this.item.Top = this.itemPanel.Height / 2 - this.item.Height / 2;

      this.previouseButton.Enabled = this.item.CanPrevious;
      this.nextButton.Enabled = this.item.CanNext;
      this.cancelButton.Enabled = this.item.CanCancel;

      this.item.UpdateWizard += new EventHandler(item_UpdateWizard);
      this.item.NextStep += new EventHandler(item_NextStep);

      Refresh();
      Application.DoEvents();

      this.item.Begin();
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