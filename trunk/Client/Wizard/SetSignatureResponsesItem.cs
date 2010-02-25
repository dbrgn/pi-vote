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
  public partial class SetSignatureResponsesItem : WizardItem
  {
    private bool run;

    public SetSignatureResponsesItem()
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
    }

    private void openButton_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Title = "Open Signature Responses.";
      dialog.CheckPathExists = true;
      dialog.CheckFileExists = true;
      dialog.Multiselect = true;
      dialog.Filter = "Pi-Vote Signature Response|*.pi-sig-resp";

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        this.run = true;
        OnUpdateWizard();

        Status.VotingClient.SetSignatureResponses(dialog.FileNames, SetSignatureResponsesComplete);

        while (this.run)
        {
          UpdateProgress();
          Application.DoEvents();
          Thread.Sleep(1);
        }

        UpdateProgress();
        OnUpdateWizard();
      }
    }

    private void UpdateProgress()
    {
      VotingClient.Operation operation = Status.VotingClient.CurrentOperation;

      if (operation != null)
      {
        this.progressLabel.Text = operation.Text;
        this.progressBar.Value = Convert.ToInt32(100d * operation.Progress);
      }
    }

    private void SetSignatureResponsesComplete(Exception exception)
    {
      if (exception != null)
      {
        MessageBox.Show(exception.ToString());
      }

      this.run = false;
    }
  }
}
