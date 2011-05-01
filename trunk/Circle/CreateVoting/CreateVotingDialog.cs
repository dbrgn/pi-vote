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

namespace Pirate.PiVote.Circle.CreateVoting
{
  public partial class CreateVotingDialog : Form
  {
    public CreateVotingDialog()
    {
      InitializeComponent();
    }

    private void CreateCertificateDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();

      Text = Resources.CreateVotingDialogTitle;
    }

    private void CreateVoting(CircleController controller)
    {
      EnterDataControl control = new EnterDataControl();
      control.Status = new CreateDialogStatus(controller);
      control.Dock = DockStyle.Fill;
      control.ShowNextControl += new ShowNextControlHandler(Control_ShowNextControl);
      control.CloseCreateDialog += new CloseCreateDialogHandler(Control_CloseCreateDialog);
      control.Prepare();
      Controls.Add(control);
    }

    private void Control_CloseCreateDialog()
    {
      Close();
    }

    private void Control_ShowNextControl(CreateVotingControl nextControl)
    {
      Controls.Clear();
      nextControl.Dock = DockStyle.Fill;
      nextControl.ShowNextControl += new ShowNextControlHandler(Control_ShowNextControl);
      nextControl.CloseCreateDialog += new CloseCreateDialogHandler(Control_CloseCreateDialog);
      Controls.Add(nextControl);
      nextControl.Prepare();
    }

    public static void ShowCreateVoting(CircleController controller)
    {
      CreateVotingDialog dialog = new CreateVotingDialog();
      dialog.CreateVoting(controller);
      dialog.ShowDialog();
    }

    private void CreateVotingDialog_KeyDown(object sender, KeyEventArgs e)
    {
      if (Enabled)
      {
        switch (e.KeyCode)
        {
          case Keys.Escape:
            Close();
            break;
        }
      }
    }

    private void CreateVotingDialog_FormClosing(object sender, FormClosingEventArgs e)
    {
      e.Cancel = !Enabled;
    }
  }
}
