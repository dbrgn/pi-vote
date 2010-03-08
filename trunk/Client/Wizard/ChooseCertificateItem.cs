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
      dialog.Title = Resources.ChooseCertificateLoadDialog;
      dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      dialog.CheckPathExists = true;
      dialog.CheckFileExists = true;
      dialog.Filter = Files.CertificateFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        Status.CertificateFileName = dialog.FileName;
        Status.Certificate = Serializable.Load<Certificate>(Status.CertificateFileName);

        this.idTextBox.Text = Status.Certificate.Id.ToString();

        if (Status.Certificate is CACertificate)
        {
          this.typeTextBox.Text = Resources.ChooseCertificateTypeCA;
          this.nameTextBox.Text = ((CACertificate)Status.Certificate).FullName;
        }
        else if (Status.Certificate is AuthorityCertificate)
        {
          this.typeTextBox.Text = Resources.ChooseCertificateTypeAuthority;
          this.nameTextBox.Text = ((AuthorityCertificate)Status.Certificate).FullName;
        }
        else if (Status.Certificate is AdminCertificate)
        {
          this.typeTextBox.Text = Resources.ChooseCertificateTypeAdmin;
          this.nameTextBox.Text = ((AdminCertificate)Status.Certificate).FullName;
        }
        else if (Status.Certificate is VoterCertificate)
        {
          this.typeTextBox.Text = Resources.ChooseCertificateTypeVoter;
          this.nameTextBox.Text = Resources.ChooseCertificateNotAvailable;
        }
        else
        {
          this.typeTextBox.Text = Resources.ChooseCertificateTypeUnknown;
          this.nameTextBox.Text = Resources.ChooseCertificateNotAvailable;
        }

        OnUpdateWizard();
      }
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.idLabel.Text = Resources.ChooseCertificateId;
      this.typeLabel.Text = Resources.ChooseCertificateType;
      this.nameLabel.Text = Resources.ChooseCertificateFullName;
      this.loadButton.Text = Resources.ChooseCertificateLoadButton;
    }
  }
}
