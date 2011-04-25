/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;
using Pirate.PiVote.Printing;

namespace Pirate.PiVote.Circle.Create
{
  public partial class PrintAndUploadCertificateControl : CreateCertificateControl
  {
    public PrintAndUploadCertificateControl()
    {
      InitializeComponent();

      this.typeLabel.Text = Resources.CreateCertificateFinishType;
      this.idLabel.Text = Resources.CreateCertificateFinishId;
      this.nameLabel.Text = Resources.CreateCertificateFinishName;
      this.emailLabel.Text = Resources.CreateCertificateFinishEmailAddress;
      this.printButton.Text = Resources.CreateCertificateFinishPrint;
      this.uploadButton.Text = Resources.CreateCertificateFinishUpload;
      this.infoLabel.Text = Resources.CreateCertificateFinishInfo;
      this.doneButton.Text = GuiResources.ButtonDone;
    }

    private void PrintAndUploadCertificateControl_Load(object sender, EventArgs e)
    {
      this.typeTextBox.Text = Status.Certificate.TypeText;
      this.idTextBox.Text = Status.Certificate.Id.ToString();
      this.nameTextBox.Text = Status.SignatureRequest.FullName;
      this.emailTextBox.Text = Status.SignatureRequest.EmailAddress;
    }

    private void printButton_Click(object sender, EventArgs e)
    {
      SignatureRequestDocument document = new SignatureRequestDocument(Status.SignatureRequest, Status.Certificate, Status.Controller.Status.GetGroupName);

      PrintDialog dialog = new PrintDialog();
      dialog.Document = document;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        document.Print();
      }
    }

    private void uploadButton_Click(object sender, EventArgs e)
    {
      if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(Status.Certificate, GuiResources.UnlockActionSignRequest))
      {
        try
        {
          Pirate.PiVote.Circle.Status.TextStatusDialog.ShowInfo(Status.Controller, FindForm());

          var secureSignatureRequest = new Secure<SignatureRequest>(Status.SignatureRequest, Status.Controller.Status.CaCertificate, Status.Certificate);
          var secureSignatureRequestInfo = new Secure<SignatureRequestInfo>(Status.SignatureRequestInfo, Status.Controller.Status.ServerCertificate, Status.Certificate);

          Status.Controller.SetSignatureRequest(secureSignatureRequest, secureSignatureRequestInfo);
        }
        catch (Exception exception)
        {
          MessageForm.Show(exception.Message, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
          Status.Certificate.Lock();
          Pirate.PiVote.Circle.Status.TextStatusDialog.HideInfo();
        }
      }
    }

    private void doneButton_Click(object sender, EventArgs e)
    {
      OnCloseCreateDialog();
    }
  }
}
