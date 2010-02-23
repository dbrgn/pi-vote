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
  public partial class SignDialog : Form
  {
    public SignDialog()
    {
      InitializeComponent();
    }

    private void CaNameDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();

      this.validUntilPicker.Value = DateTime.Now.AddYears(2);
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

    public DateTime ValidUntil
    {
      get { return this.validUntilPicker.Value; }
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

    private void validUntilPicker_ValueChanged(object sender, EventArgs e)
    {
      this.okButton.Enabled = this.validUntilPicker.Value > DateTime.Now;
    }
  }
}
