/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Pirate.PiVote.CaGui
{
  public partial class RefuseDialog : Form
  {
    public RefuseDialog()
    {
      InitializeComponent();
    }

    private void CaNameDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
      Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }

    private void nameTextBox_TextChanged(object sender, EventArgs e)
    {
      this.okButton.Enabled = this.reasonTextBox.Text.Length > 0;
    }

    public string CertificateId
    {
      get { return this.idTextBox.Text; }
      set { this.idTextBox.Text = value; }
    }

    public string CertificateType
    {
      get { return this.typeTextBox.Text; }
      set { this.typeTextBox.Text = value; }
    }

    public string CertificateName
    {
      get { return this.nameTextBox.Text; }
      set { this.nameTextBox.Text = value; }
    }

    public string Reason
    {
      get { return this.reasonTextBox.Text; }
    }

    private void RefuseDialog_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
          if (this.okButton.Enabled)
          {
            DialogResult = DialogResult.OK;
            Close();
          }
          break;
        case Keys.Escape:
          DialogResult = DialogResult.Cancel;
          Close();
          break;
      }
    }
  }
}
