﻿/*
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
  public partial class CreateAdminDialog : Form
  {
    public CreateAdminDialog()
    {
      InitializeComponent();
    }

    private void CaNameDialog_Load(object sender, EventArgs e)
    {
      CenterToScreen();
      this.validUntilPicker.Value = DateTime.Now.AddYears(1);
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

    private void CheckOkEnabled()
    {
      this.okButton.Enabled =
        !this.firstNameTextBox.Text.IsNullOrEmpty() &&
        !this.familyNameTextBox.Text.IsNullOrEmpty() &&
        !this.functionTextBox.Text.IsNullOrEmpty() &&
        !this.emailAddressTextBox.Text.IsNullOrEmpty();
    }

    private void firstNameTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckOkEnabled();
    }

    private void familyNameTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckOkEnabled();
    }

    private void functionTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckOkEnabled();
    }

    private void emailAddressTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckOkEnabled();
    }

    public string FirstName
    {
      get { return this.firstNameTextBox.Text; }
      set { this.firstNameTextBox.Text = value; }
    }

    public string FamilyName
    {
      get { return this.familyNameTextBox.Text; }
      set { this.familyNameTextBox.Text = value; }
    }

    public string Function
    {
      get { return this.functionTextBox.Text; }
      set { this.functionTextBox.Text = value; }
    }

    public string EmailAddress
    {
      get { return this.emailAddressTextBox.Text; }
      set { this.emailAddressTextBox.Text = value; }
    }

    public DateTime ValidUntil
    {
      get { return this.validUntilPicker.Value; }
      set { this.validUntilPicker.Value = value; }
    }
  }
}