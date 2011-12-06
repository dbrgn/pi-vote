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

namespace Pirate.PiVote.Circle.Create
{
  public partial class CreateCertificateDialog : Form
  {
    private CreateCertificateControl control;

    public CreateCertificateDialog()
    {
      InitializeComponent();
    }

    private void CreateCertificateDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();

      Text = Resources.CreateCertificateDialogTitle;
    }

    private void SetControl(CreateCertificateControl control, CreateDialogStatus status)
    {
      Controls.Clear();

      this.control = control;

      if (status != null)
      {
        this.control.Status = status;
      }

      this.control.Dock = DockStyle.Fill;
      this.control.ShowNextControl += new ShowNextControlHandler(Control_ShowNextControl);
      this.control.CloseCreateDialog += new CloseCreateDialogHandler(Control_CloseCreateDialog);
      this.control.Prepare();
      this.Controls.Add(this.control);
    }

    private void CreateNewCertificate(CircleController controller)
    {
      SetControl(
        new SelectCertificateTypeControl(), 
        new CreateDialogStatus(controller));
    }

    private void CreateNewVoterCertificate(CircleController controller)
    {
      SetControl(
        new EnterVoterCertificateDataControl(), 
        new CreateDialogStatus(controller));
    }

    private void CreateNewAuthorityCertificate(CircleController controller)
    {
      SetControl(
        new EnterAuthorityCertificateDataControl(),
        new CreateDialogStatus(controller));
    }

    private bool TryResumeCertificateCreation(CircleController controller, Certificate certificate)
    {
      CreateDialogStatus status = new CreateDialogStatus(controller);
      status.Certificate = certificate;

      if (status.TryLoadSignatureRequest())
      {
        SetControl(
          new PrintAndUploadCertificateControl(),
          status);

        return true;
      }
      else
      {
        return false;
      }
    }

    private void Control_CloseCreateDialog()
    {
      Close();
    }

    private void Control_ShowNextControl(CreateCertificateControl nextControl)
    {
      SetControl(nextControl, null);
    }

    public static void ShowCreateNewCertificate(CircleController controller)
    {
      CreateCertificateDialog dialog = new CreateCertificateDialog();
      dialog.CreateNewCertificate(controller);
      dialog.ShowDialog();
    }

    public static void TryFixVoterCertificate(CircleController controller, Certificate certificate)
    {
      CreateCertificateDialog dialog = new CreateCertificateDialog();
      if (dialog.TryResumeCertificateCreation(controller, certificate))
      {
        dialog.ShowDialog();
      }
      else
      {
        dialog.CreateNewVoterCertificate(controller);
        dialog.ShowDialog();
      }
    }

    public static void ShowCreateNewVoterCertificate(CircleController controller)
    {
      CreateCertificateDialog dialog = new CreateCertificateDialog();
      dialog.CreateNewVoterCertificate(controller);
      dialog.ShowDialog();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
      this.control.BeforeClose();
    }
  }
}
