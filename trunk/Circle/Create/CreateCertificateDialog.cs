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
    public CreateCertificateDialog()
    {
      InitializeComponent();
    }

    private void CreateCertificateDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();
    }

    private void CreateNewCertificate(CircleController controller)
    {
      SelectCertificateTypeControl control = new SelectCertificateTypeControl();
      control.Status = new CreateDialogStatus(controller);
      control.Dock = DockStyle.Fill;
      control.ShowNextControl += new ShowNextControlHandler(Control_ShowNextControl);
      control.CloseCreateDialog += new CloseCreateDialogHandler(Control_CloseCreateDialog);
      Controls.Add(control);
    }

    private void CreateNewVoterCertificate(CircleController controller)
    {
      EnterVoterCertificateDataControl control = new EnterVoterCertificateDataControl();
      control.Status = new CreateDialogStatus(controller);
      control.Dock = DockStyle.Fill;
      control.ShowNextControl += new ShowNextControlHandler(Control_ShowNextControl);
      control.CloseCreateDialog += new CloseCreateDialogHandler(Control_CloseCreateDialog);
      Controls.Add(control);
    }

    private void CreateNewAuthorityCertificate(CircleController controller)
    {
      EnterAuthorityCertificateDataControl control = new EnterAuthorityCertificateDataControl();
      control.Status = new CreateDialogStatus(controller);
      control.Dock = DockStyle.Fill;
      control.ShowNextControl += new ShowNextControlHandler(Control_ShowNextControl);
      control.CloseCreateDialog += new CloseCreateDialogHandler(Control_CloseCreateDialog);
      Controls.Add(control);
    }

    private bool TryResumeCertificateCreation(CircleController controller, Certificate certificate)
    {
      CreateDialogStatus status = new CreateDialogStatus(controller);
      status.Certificate = certificate;

      if (status.TryLoadSignatureRequest())
      {
        PrintAndUploadCertificateControl control = new PrintAndUploadCertificateControl();
        control.Status = new CreateDialogStatus(controller);
        control.Status = status;
        control.Dock = DockStyle.Fill;
        control.ShowNextControl += new ShowNextControlHandler(Control_ShowNextControl);
        control.CloseCreateDialog += new CloseCreateDialogHandler(Control_CloseCreateDialog);
        Controls.Add(control);
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
      Controls.Clear();
      nextControl.Dock = DockStyle.Fill;
      nextControl.ShowNextControl += new ShowNextControlHandler(Control_ShowNextControl);
      nextControl.CloseCreateDialog += new CloseCreateDialogHandler(Control_CloseCreateDialog);
      Controls.Add(nextControl);
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
  }
}
