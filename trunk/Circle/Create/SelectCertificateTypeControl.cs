/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pirate.PiVote.Circle.Create
{
  public partial class SelectCertificateTypeControl : CreateCertificateControl
  {
    public SelectCertificateTypeControl()
    {
      InitializeComponent();
    }

    private void SelectCertificateTypeControl_Load(object sender, EventArgs e)
    {
      this.nextButton.Enabled = false;
    }

    private void voterCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.nextButton.Enabled = this.voterCheckBox.Checked || this.authorityCheckBox.Checked;
    }

    private void authorityCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      this.nextButton.Enabled = this.voterCheckBox.Checked || this.authorityCheckBox.Checked;
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      OnCloseCreateDialog();
    }

    private void nextButton_Click(object sender, EventArgs e)
    {
      if (this.voterCheckBox.Checked)
      {
        var nextControl = new EnterVoterCertificateDataControl();
        nextControl.Status = Status;
        OnShowNextControl(nextControl);
      }
      else if (this.authorityCheckBox.Checked)
      {
        var nextControl = new EnterAuthorityCertificateDataControl();
        nextControl.Status = Status;
        OnShowNextControl(nextControl);
      }
    }
  }
}
