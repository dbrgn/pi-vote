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

namespace Pirate.PiVote.Kiosk
{
  public partial class ControlForm : Form
  {
    private MemberDatabase memberDatabase;

    private KioskServer server;

    public ControlForm()
    {
      InitializeComponent();
    }

    private void ControlForm_Load(object sender, EventArgs e)
    {
      SetupForm setupForm = new SetupForm();

      if (setupForm.ShowDialog() != DialogResult.OK)
      {
        Close();
        return;
      }
      else
      {
        this.server = new KioskServer(setupForm.CertificateStorage, setupForm.ServerCertificate);
        this.server.Start();

        this.memberDatabase = setupForm.MemberDatabase;
      }
    }

    private void ControlForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      if (this.server != null)
      {
        this.server.Stop();
      }
    }

    private void searchForTextBox_TextChanged(object sender, EventArgs e)
    {
      this.searchResultList.Items.Clear();

     var displayEntries = this.memberDatabase.Entries.Where(entry => entry.Name.ToLower().Contains(this.searchForTextBox.Text.ToLower()));

      foreach (var entry in displayEntries)
      {
        ListViewItem item = new ListViewItem(entry.Givenname);
        item.SubItems.Add(entry.Surname);
        item.SubItems.Add(entry.EmailAddress);

        this.searchResultList.Items.Add(item);
      }
    }

    private void searchResultList_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.searchResultList.SelectedItems.Count > 0)
      {
        var item = this.searchResultList.SelectedItems[0];

        this.givenNameTextBox.Text = item.SubItems[0].Text;
        this.surnameTextBox.Text = item.SubItems[1].Text;
        this.emailAddressTextBox.Text = item.SubItems[2].Text;
      }
    }

    private void resetButton_Click(object sender, EventArgs e)
    {
      this.givenNameTextBox.Text = string.Empty;
      this.surnameTextBox.Text = string.Empty;
      this.emailAddressTextBox.Text = string.Empty;
      this.searchForTextBox.Text = string.Empty;
      this.searchResultList.Items.Clear();
    }

    private void setButton_Click(object sender, EventArgs e)
    {
      var userData = new SignatureRequest(this.givenNameTextBox.Text, this.surnameTextBox.Text, this.emailAddressTextBox.Text);

      if (userData.Valid)
      {
        this.server.UserData = userData;
        this.setGivennameTextBox.Text = userData.FirstName;
        this.setSurnameTextBox.Text = userData.FamilyName;
        this.setEmailAddressTextBox.Text = userData.EmailAddress;
      }
      else
      {
        MessageBox.Show("Invalid user data.", "Pi-Vote Kisok - Control", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }
  }
}
