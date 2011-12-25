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
using Pirate.PiVote.Gui;

namespace Pirate.PiVote.Circle.Create
{
  public partial class SelectCertificateTypeControl : CreateCertificateControl
  {
    public SelectCertificateTypeControl()
    {
      InitializeComponent();

      this.voterCheckBox.Text = Resources.CreateCertificateSelectVoter;
      this.voterLabel.Text = Resources.CreateCertificateSelectVoterInfo;
      this.voterSubGroupCheckBox.Text = Resources.CreateCertificateSelectVoterSubgroup;
      this.voterSubgroupLabel.Text = Resources.CreateCertificateSelectVoterSubgroupInfo;
      this.authorityCheckBox.Text = Resources.CreateCertificateSelectAuthority;
      this.authorityLabel.Text = Resources.CreateCertificateSelectAuthorityInfo;
      this.notaryCheckBox.Text = Resources.CreateCertificateSelectNotary;
      this.notaryLabel.Text = Resources.CreateCertificateSelectNotaryInfo;
      this.nextButton.Text = GuiResources.ButtonNext;
      this.cancelButton.Text = GuiResources.ButtonCancel;
    }

    private void SelectCertificateTypeControl_Load(object sender, EventArgs e)
    {
      this.nextButton.Enabled = false;
    }

    private void CheckValid()
    {
      this.nextButton.Enabled =
        this.voterCheckBox.Checked ||
        this.authorityCheckBox.Checked ||
        this.voterSubGroupCheckBox.Checked ||
        this.notaryCheckBox.Checked;
    }

    private void voterCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      CheckValid();
    }

    private void authorityCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      CheckValid();
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
      else if (this.voterSubGroupCheckBox.Checked)
      {
        var nextControl = new EnterVoterSubgroupDataControl();
        nextControl.Status = Status;
        OnShowNextControl(nextControl);
      }
      else if (this.notaryCheckBox.Checked)
      {
        var nextControl = new EnterNotaryDataControl();
        nextControl.Status = Status;
        OnShowNextControl(nextControl);
      }
    }

    public override void Prepare()
    {
      var certificates = Status.Controller.GetValidVoterCertificates();
      bool haveParentCertificate = certificates.Any(certificate => certificate.GroupId == 0);
      this.voterSubGroupCheckBox.Enabled = haveParentCertificate;
      this.authorityCheckBox.Enabled = haveParentCertificate;
      this.notaryCheckBox.Enabled = haveParentCertificate;
    }

    private void notaryCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      CheckValid();
    }

    private void voterSubGroupCheckBox_CheckedChanged(object sender, EventArgs e)
    {
      CheckValid();
    }
  }
}
