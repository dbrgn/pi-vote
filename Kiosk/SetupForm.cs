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
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Kiosk
{
  public partial class SetupForm : Form
  {
    private Properties.Settings settings;

    public CertificateStorage CertificateStorage { get; private set; }

    public Certificate ServerCertificate { get; private set; }

    public MemberDatabase MemberDatabase { get; private set; }

    public SetupForm()
    {
      InitializeComponent();
    }

    private void certificateStorageBrowseButton_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Title = "Pi-Vote Kiosk - Open certificate storage";
      dialog.CheckFileExists = true;
      dialog.Filter = Files.CertificateStorageFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        CertificateStorage = Serializable.Load<CertificateStorage>(dialog.FileName);
        this.certificateStorageTextBox.Text = dialog.FileName;
      }

      CheckValid();
    }

    private void serverCertificateBrowseButton_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Title = "Pi-Vote Kiosk - Open server certificate";
      dialog.CheckFileExists = true;
      dialog.Filter = Files.CertificateFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        ServerCertificate = Serializable.Load<Certificate>(dialog.FileName).OnlyPublicPart;

        if (ServerCertificate is ServerCertificate)
        {
          this.serverCertificateTextBox.Text = dialog.FileName;
        }
        else
        {
          MessageBox.Show("The selected certificate is not a server certificate.", "Pi-Vote Kiosk - Open server certificate", MessageBoxButtons.OK, MessageBoxIcon.Information);

          ServerCertificate = null;
          return;
        }
      }

      CheckValid();
    }

    private void CheckValid()
    {
      bool valid = true;

      valid &= CertificateStorage != null;
      valid &= ServerCertificate != null;
      valid &= MemberDatabase != null;

      this.startButton.Enabled = valid;
    }

    private void SetupForm_Load(object sender, EventArgs e)
    {
      CenterToScreen();

      this.settings = new Properties.Settings();

      if (File.Exists(this.settings.CertificateStorageFileName))
      {
        this.certificateStorageTextBox.Text = this.settings.CertificateStorageFileName;
        CertificateStorage = Serializable.Load<CertificateStorage>(this.settings.CertificateStorageFileName);
      }

      if (File.Exists(this.settings.ServerCertificateFilename))
      {
        this.serverCertificateTextBox.Text = this.settings.ServerCertificateFilename;
        ServerCertificate = Serializable.Load<Certificate>(this.settings.ServerCertificateFilename).OnlyPublicPart;
      }

      if (File.Exists(this.settings.MemberDatabaseFilename))
      {
        this.memberDatabaseTextBox.Text = this.settings.MemberDatabaseFilename;

        MemberDatabase = new MemberDatabase();
        MemberDatabase.ImportCsv(this.settings.MemberDatabaseFilename);

        if (MemberDatabase.Entries.Count() < 1)
        {
          MemberDatabase = null;
        }
      }

      CheckValid();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }

    private void startButton_Click(object sender, EventArgs e)
    {
      this.settings.CertificateStorageFileName = this.certificateStorageTextBox.Text;
      this.settings.ServerCertificateFilename = this.serverCertificateTextBox.Text;
      this.settings.MemberDatabaseFilename = this.memberDatabaseTextBox.Text;
      this.settings.Save();

      DialogResult = DialogResult.OK;
      Close();
    }

    private void memberDatabaseBrowseButton_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Title = "Pi-Vote Kiosk - Open member database";
      dialog.CheckFileExists = true;
      dialog.Filter = "Comma Seperated Values|*.csv";

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        MemberDatabase = new MemberDatabase();
        MemberDatabase.ImportCsv(dialog.FileName);

        if (MemberDatabase.Entries.Count() < 1)
        {
          MemberDatabase = null;
        }
        else
        {
          this.memberDatabaseTextBox.Text = dialog.FileName;
        }
      }

      CheckValid();
    }
  }
}
