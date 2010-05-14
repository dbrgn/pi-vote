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
  public partial class AdminChooseItem : WizardItem
  {
    private bool run = false;
    private Exception exception;

    public AdminChooseItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      return new CreateVotingItem();
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

    public override bool CancelIsDone
    {
      get { return true; }
    }

    public override void Begin()
    {
      OnUpdateWizard();
    }

    private void getSignatureRequestsRadio_CheckedChanged(object sender, EventArgs e)
    {
      OnUpdateWizard();
    }

    private void setSignatureResponsesRadio_CheckedChanged(object sender, EventArgs e)
    {
      OnUpdateWizard();
    }

    private void createVotingRadio_CheckedChanged(object sender, EventArgs e)
    {
      OnUpdateWizard();
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.createVotingButton.Text = Resources.AdminChooseCreateVoting;
      this.downloadSignatureRequestsButton.Text = Resources.AdminChooseDownloadSignatureRequests;
      this.uploadSignatureResponsesButton.Text = Resources.AdminChooseUploadSignatureRessponse;
      this.uploadCertificateStorageButton.Text = Resources.AdminChooseUploadCertificateStorage; 
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
          Thread.Sleep(10);
        }

        Status.UpdateProgress();

        if (this.exception != null)
        {
          Status.SetMessage(this.exception.Message, MessageType.Error);
        }

        OnUpdateWizard();
      }
    }

    private void GetSignatureRequestsComplete(Exception exception)
    {
      this.exception = exception;
      this.run = false;
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
          Thread.Sleep(10);
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

    private void createVotingButton_Click(object sender, EventArgs e)
    {
      OnNextStep();
    }

    private void uploadCertificateStorageButton_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Title = Resources.OpenCertificateStorageDialog;
      dialog.CheckPathExists = true;
      dialog.CheckFileExists = true;
      dialog.Multiselect = false;
      dialog.Filter = Files.CertificateStorageFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        this.run = true;
        OnUpdateWizard();

        CertificateStorage certificateStorage = Serializable.Load<CertificateStorage>(dialog.FileName);
        Status.VotingClient.SetCertificateStorage(certificateStorage, SetCertificateStorageComplete);

        while (this.run)
        {
          Status.UpdateProgress();
          Thread.Sleep(10);
        }

        Status.UpdateProgress();

        if (this.exception == null)
        {
          Status.SetMessage(Resources.CertificateStorageUploaded, MessageType.Success);
        }
        else
        {
          Status.SetMessage(this.exception.Message, MessageType.Error);
        }

        OnUpdateWizard();
      }
    }

    private void SetCertificateStorageComplete(Exception exception)
    {
      this.exception = exception;
      this.run = false;
    }
  }
}
