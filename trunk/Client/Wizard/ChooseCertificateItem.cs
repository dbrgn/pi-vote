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
    private bool nextIsCreate;
    private BadShareProofItem nextItemBadShareProof;

    public ChooseCertificateItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      if (this.nextItemBadShareProof != null)
      {
        return this.nextItemBadShareProof;
      }
      if (this.nextIsCreate)
      {
        return new SimpleCreateCertificateItem();
      }
      else
      {
        if (Status.Certificate is CACertificate)
        {
          return null;
        }
        else
        {
          CheckCertificateItem checkItem = new CheckCertificateItem();
          checkItem.PreviousItem = this;
          return checkItem;
        }
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

    public override void Begin()
    {
      this.nextIsCreate = false;
      Status.Certificate = null;

      DirectoryInfo directory = new DirectoryInfo(Status.DataPath);
      this.certificateList.Items.Clear();

      foreach (FileInfo file in directory.GetFiles("*.pi-cert"))
      {
        try
        {
          Certificate certificate = Serializable.Load<Certificate>(file.FullName);

          ListViewItem item = new ListViewItem(certificate.TypeText);
          item.SubItems.Add(certificate.Id.ToString());

          if (certificate is VoterCertificate)
          {
            item.SubItems.Add(Status.GetGroupName(((VoterCertificate)certificate).GroupId));
          }
          else
          {
            item.SubItems.Add(certificate.FullName);
          }

          item.Tag = new KeyValuePair<string, Certificate>(file.FullName, certificate);
          this.certificateList.Items.Add(item);
        }
        catch
        {
          ListViewItem item = new ListViewItem("Cannot load file.");
          item.SubItems.Add(string.Empty);
          item.SubItems.Add(file.Name);
          item.Tag = null;
          this.certificateList.Items.Add(item);
        }
      }

      OnUpdateWizard();
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
        Certificate certificate = Serializable.Load<Certificate>(dialog.FileName);

        string newFileName = Path.Combine(Status.DataPath, certificate.Id.ToString() + ".pi-cert");
        File.Copy(dialog.FileName, newFileName);

          ListViewItem item = new ListViewItem(certificate.TypeText);
        item.SubItems.Add(certificate.Id.ToString());
        item.SubItems.Add(certificate.FullName.ToString());
        item.Tag = new KeyValuePair<string, Certificate>(newFileName, certificate);
        this.certificateList.Items.Add(item);

        item.Selected = true;
      }
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.certificateListLabel.Text = Resources.ChooseCertificateListHeader;

      this.idLabel.Text = Resources.ChooseCertificateId;
      this.typeLabel.Text = Resources.ChooseCertificateType;
      this.nameLabel.Text = Resources.ChooseCertificateFullName;
      
      this.loadButton.Text = Resources.ChooseCertificateLoadButton;
      this.createButton.Text = Resources.ChooseCertificateCreateButton;

      this.typeColumnHeader.Text = Resources.ChooseCertificateType;
      this.idLabel.Text = Resources.ChooseCertificateId;
      this.nameColumnHeader.Text = Resources.ChooseCertificateFullName;

      this.verifyShareProofButton.Text = Resources.ChooseCertificateVerifyBadShareProof;
    }

    private void certificateList_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (this.certificateList.SelectedItems.Count > 0)
      {
        ListViewItem item = this.certificateList.SelectedItems[0];

        if (item.Tag != null)
        {
          KeyValuePair<string, Certificate> value = (KeyValuePair<string, Certificate>)item.Tag;
          Status.CertificateFileName = value.Key;
          Status.Certificate = value.Value;

          this.idTextBox.Text = Status.Certificate.Id.ToString();
          this.typeTextBox.Text = Status.Certificate.TypeText;
          this.nameTextBox.Text = Status.Certificate.FullName;
        }
        else
        {
          this.idTextBox.Text = string.Empty;
          this.typeTextBox.Text = string.Empty;
          this.nameTextBox.Text = string.Empty;
        }

        OnUpdateWizard();
      }
    }

    private void createButton_Click(object sender, EventArgs e)
    {
      this.nextIsCreate = true;

      OnNextStep();
    }

    private void verifyShareProofButton_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Title = Resources.ChooseCertificateLoadDialog;
      dialog.InitialDirectory = Status.DataPath;
      dialog.CheckPathExists = true;
      dialog.CheckFileExists = true;
      dialog.Filter = Files.BadShareProofFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        this.nextItemBadShareProof = new BadShareProofItem();
        this.nextItemBadShareProof.IsAuthority = false;
        this.nextItemBadShareProof.SignedBadShareProof = Serializable.Load<Signed<BadShareProof>>(dialog.FileName);
        OnNextStep();
      }
    }
  }
}
