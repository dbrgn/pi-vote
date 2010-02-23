/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Client
{
  public partial class ChooseCertificateItem : WizardItem
  {
    public ChooseCertificateItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      if (Status.Certificate is CACertificate)
      {
        return null;
      }
      else
      {
        return new CheckCertificateItem();
      }
    }

    public override WizardItem Previous()
    {
      return new StartItem();
    }

    public override WizardItem Cancel()
    {
      return null;
    }

    public override bool CanNext
    {
      get
      {
        return Status.Certificate != null && 
          !(Status.Certificate is CACertificate);
      }
    }

    public override bool CanPrevious
    {
      get { return true; }
    }

    public override bool CanCancel
    {
      get { return true; }
    }

    private void StartWizardItem_Load(object sender, EventArgs e)
    {

    }

    private void loadButton_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Title = "Load certificate";
      dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      dialog.CheckPathExists = true;
      dialog.CheckFileExists = true;
      dialog.Filter = "Pi-Vote Certificate|*.pi-cert";

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        Status.CertificateFileName = dialog.FileName;
        Status.Certificate = Serializable.Load<Certificate>(Status.CertificateFileName);

        this.idTextBox.Text = Status.Certificate.Id.ToString();

        if (Status.Certificate is CACertificate)
        {
          this.typeTextBox.Text = "Certificate Authority";
          this.nameTextBox.Text = ((CACertificate)Status.Certificate).FullName;
        }
        else if (Status.Certificate is AuthorityCertificate)
        {
          this.typeTextBox.Text = "Voting Authority";
          this.nameTextBox.Text = ((AuthorityCertificate)Status.Certificate).FullName;
        }
        else if (Status.Certificate is AdminCertificate)
        {
          this.typeTextBox.Text = "Administrator";
          this.nameTextBox.Text = ((AdminCertificate)Status.Certificate).FullName;
        }
        else if (Status.Certificate is VoterCertificate)
        {
          this.typeTextBox.Text = "Voter";
          this.nameTextBox.Text = "N/A";
        }
        else
        {
          this.typeTextBox.Text = "Unknown";
          this.nameTextBox.Text = "N/A";
        }

        OnUpdateWizard();
      }
    }
  }
}
