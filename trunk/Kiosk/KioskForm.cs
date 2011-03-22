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
using System.Threading;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Kiosk
{
  public partial class KioskForm : Form
  {
    private CACertificate caCertificate;

    private ClientController controller;

    private ClientControllerState state;

    private bool run;

    private bool halted;

    public KioskForm()
    {
      InitializeComponent();
    }

    private void KioskForm_Load(object sender, EventArgs e)
    {
      WindowState = FormWindowState.Maximized;

      this.caCertificate = Serializable.Load<CACertificate>(Files.RootCertificateFileName);
      this.controller = new ClientController();
      this.state = controller.State;
      this.run = true;
      this.halted = false;

      Show();

      this.certificateStorageTextBox.Text = "Laden...";

      while (this.run)
      {
        DoWork();
        Application.DoEvents();
        Thread.Sleep(1);
      }

      this.halted = true;

      Close();
    }

    private void DoWork()
    {
      if (this.controller.Faulted)
      {
        MessageBox.Show(
          "An error occurred:" + Environment.NewLine + this.controller.Fault.ToString(),
          "Pi-Vote Kiosk",
          MessageBoxButtons.OK,
          MessageBoxIcon.Error);

        this.run = false;
      }

      if (this.state != this.controller.State)
      {
        switch (this.controller.State)
        {
          case ClientControllerState.GotCertificateStorage:
            this.certificateStorageTextBox.Text = "Geladen";
            this.serverCertificateTextBox.Text = "Laden...";
            break;
          case ClientControllerState.GotServerCertificate:
            this.serverCertificateTextBox.Text = "Geladen";
            break;
          case ClientControllerState.GotUserData:
            this.givennameTextBox.Text = this.controller.UserData.FirstName;
            this.surnameTextBox.Text = this.controller.UserData.FamilyName;
            this.emailAddressTextBox.Text = this.controller.UserData.EmailAddress;
            this.passphraseTextBox.Enabled = true;
            this.repeatTextBox.Enabled = true;
            break;
          case ClientControllerState.Done:
            this.requestStatusTextBox.Text = "Übermittelt";
            break;
        }

        this.state = this.controller.State;
      }
      else if (this.state == ClientControllerState.GotUserData)
      {
        // Update user data if it has changed
        this.givennameTextBox.Text = this.controller.UserData.FirstName;
        this.surnameTextBox.Text = this.controller.UserData.FamilyName;
        this.emailAddressTextBox.Text = this.controller.UserData.EmailAddress;
      }
    }

    private void KioskForm_FormClosing(object sender, FormClosingEventArgs e)
    {
      this.run = false;
      e.Cancel = !this.halted;
    }

    private void passphraseTextBox_TextChanged(object sender, EventArgs e)
    {
      this.okButton.Enabled = 
        this.passphraseTextBox.Text.Length >= 1 &&
        this.passphraseTextBox.Text == this.repeatTextBox.Text;
    }

    private void repeatTextBox_TextChanged(object sender, EventArgs e)
    {
      this.okButton.Enabled =
        this.passphraseTextBox.Text.Length >= 1 &&
        this.passphraseTextBox.Text == this.repeatTextBox.Text;
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      this.passphraseTextBox.Enabled = false;
      this.repeatTextBox.Enabled = false;
      this.okButton.Enabled = false;

      var signatureRequest = this.controller.UserData;
      var certificate = new VoterCertificate(Language.German, this.passphraseTextBox.Text, 0);
      certificate.CreateSelfSignature();
      var secureRequest = new Secure<SignatureRequest>(signatureRequest, this.caCertificate, certificate);
      var requestInfo = new SignatureRequestInfo(string.Empty);
      var secureRequestInfo = new Secure<SignatureRequestInfo>(requestInfo, this.controller.ServerCertificate, certificate);
      var requestContainer = new RequestContainer(signatureRequest, secureRequest, secureRequestInfo);

      this.requestStatusTextBox.Text = "Übermitteln...";
      this.controller.RequestContainer = requestContainer;
    }
  }
}
