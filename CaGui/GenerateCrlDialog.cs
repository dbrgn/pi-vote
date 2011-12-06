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

namespace Pirate.PiVote.CaGui
{
  public partial class GenerateCrlDialog : Form
  {
    public GenerateCrlDialog()
    {
      InitializeComponent();
    }

    private void CaNameDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();

      this.validFromPicker.Value = DateTime.Now;
      this.validUntilPicker.Value = DateTime.Now.AddMonths(1);
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

    public string CertificateId
    {
      get { return this.idTextBox.Text; }
      set { this.idTextBox.Text = value; }
    }

    public string CertificateName
    {
      get { return this.nameTextBox.Text; }
      set { this.nameTextBox.Text = value; }
    }

    public DateTime ValidFrom
    {
      get { return this.validFromPicker.Value; ; }
    }

    public DateTime ValidUntil
    {
      get { return this.validUntilPicker.Value; ; }
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

    private void validFromPicker_ValueChanged(object sender, EventArgs e)
    {
      this.okButton.Enabled = this.validUntilPicker.Value > this.validFromPicker.Value;
    }

    private void validUntilPicker_ValueChanged(object sender, EventArgs e)
    {
      this.okButton.Enabled = this.validUntilPicker.Value > this.validFromPicker.Value;
    }
  }
}
