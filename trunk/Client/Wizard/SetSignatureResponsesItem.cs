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
    private Exception exception;

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
          Application.DoEvents();
          Thread.Sleep(1);
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

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.openButton.Text = Resources.OpenSignatureResponseButton;
    }
  }
}
