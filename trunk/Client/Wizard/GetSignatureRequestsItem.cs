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
  public partial class GetSignatureRequestsItem : WizardItem
  {
    private bool run;
    private Exception exception;
    private bool done = false;

    public GetSignatureRequestsItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      return new AdminChooseItem();
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

    public override bool CanNext
    {
      get { return this.done; }
    }

    public override void Begin()
    {
    }

    private void saveToButton_Click(object sender, EventArgs e)
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
          Application.DoEvents();
          Thread.Sleep(1);
        }

        Status.UpdateProgress();

        if (this.exception != null)
        {
          Status.SetMessage(this.exception.Message, MessageType.Error);
        }

        this.done = true;

        OnUpdateWizard();
      }
    }

    private void GetSignatureRequestsComplete(Exception exception)
    {
      this.exception = exception;
      this.run = false;
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.saveToButton.Text = Resources.SaveSignatureRequestDialog;
    }
  }
}
