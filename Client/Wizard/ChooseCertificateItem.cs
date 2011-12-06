/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;
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
        Status.Certificate = null;
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
          !(Status.Certificate is CACertificate) &&
          (!(Status.Certificate is VoterCertificate) || Status.ServerCertificate != null);
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
      Status.CertificateFileName = null;

      if (Status.ServerCertificate == null)
      {
        this.createMenu.Enabled = false;
      }

      DirectoryInfo directory = new DirectoryInfo(Status.DataPath);
      this.certificateList.Items.Clear();

      foreach (FileInfo file in directory.GetFiles(Files.CertificatePattern))
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
      LoadCertificate();
    }

    private void LoadCertificate()
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Title = Resources.ChooseCertificateLoadDialog;
      dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      dialog.CheckPathExists = true;
      dialog.CheckFileExists = true;
      dialog.Filter = Files.CertificateFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        try
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

          Status.SetMessage(Resources.SimpleChooseCertificateImportDone, MessageType.Success);
        }
        catch
        {
          Status.SetMessage(Resources.CertificateLoadInvalid, MessageType.Error);
        }
      }
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.certificateListLabel.Text = Resources.ChooseCertificateListHeader;
      this.infoLabel.Text = Resources.ChooseCertificateInfo;

      this.createMenu.Text = Resources.ChooseCertificateCreateButton;
      this.saveMenu.Text = Resources.ChooseCertificateSaveButton;
      this.loadMenu.Text = Resources.ChooseCertificateLoadButton;
      this.deleteMenu.Text = Resources.ChooseCertificateDeleteButton;

      this.backupMenu.Text = Resources.ChooseCertificateBackupButton;
      this.restoreMenu.Text = Resources.ChooseCertificateRestoreButton;

      this.backupMenu.Text = Resources.ChooseCertificateBackupButton;
      this.restoreMenu.Text = Resources.ChooseCertificateRestoreButton;

      this.encryptMenu.Text = Resources.ChooseCertificateEncrytButton;
      this.changePassphraseMenu.Text = Resources.ChooseCertificateChangePassphraseButton;

      this.verifyShareproofMenu.Text = Resources.ChooseCertificateVerifyBadShareProof;

      this.typeColumnHeader.Text = Resources.ChooseCertificateType;
      this.nameColumnHeader.Text = Resources.ChooseCertificateFullName;
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
        }

        OnUpdateWizard();
      }
    }

    private void createButton_Click(object sender, EventArgs e)
    {
      this.nextIsCreate = true;

      OnNextStep();
    }

    private void VerifyShareProof()
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

    private void saveButton_Click(object sender, EventArgs e)
    {
      SaveCertificate();
    }

    private void SaveCertificate()
    {
      if (this.certificateList.SelectedItems.Count > 0)
      {
        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Title = Resources.ChooseCertificateSaveDialog;
        dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        dialog.CheckPathExists = true;
        dialog.Filter = Files.CertificateFileFilter;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          ListViewItem item = this.certificateList.SelectedItems[0];
          KeyValuePair<string, Certificate> tag = (KeyValuePair<string, Certificate>)item.Tag;

          File.Copy(tag.Key, dialog.FileName);
        }
      }
    }

    private void DeleteCertificate()
    {
      if (this.certificateList.SelectedItems.Count > 0)
      {
        ListViewItem item = this.certificateList.SelectedItems[0];
        KeyValuePair<string, Certificate> tag = (KeyValuePair<string, Certificate>)item.Tag;

        File.Move(tag.Key, tag.Key + Files.BakExtension);

        item.Remove();
      }
    }

    private void saveMenu_Click(object sender, EventArgs e)
    {
      SaveCertificate();
    }

    private void loadMenu_Click(object sender, EventArgs e)
    {
      LoadCertificate();
    }

    private void createMenu_Click(object sender, EventArgs e)
    {
      this.nextIsCreate = true;

      OnNextStep();
    }

    private void certificateListMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
      this.saveMenu.Enabled = this.certificateList.SelectedItems.Count > 0;
      this.deleteMenu.Enabled = this.certificateList.SelectedItems.Count > 0;

      if (this.certificateList.SelectedItems.Count > 0)
      {
        ListViewItem item = this.certificateList.SelectedItems[0];

        if (item.Tag != null)
        {
          KeyValuePair<string, Certificate> value = (KeyValuePair<string, Certificate>)item.Tag;

          if (value.Value.PrivateKeyStatus == PrivateKeyStatus.Unencrypted)
          {
            this.encryptMenu.Enabled = true;
            this.changePassphraseMenu.Enabled = false;
          }
          else
          {
            this.encryptMenu.Enabled = false;
            this.changePassphraseMenu.Enabled = true;
          }
        }
        else
        {
          this.encryptMenu.Enabled = false;
          this.changePassphraseMenu.Enabled = false;
        }
      }
      else
      {
        this.encryptMenu.Enabled = false;
        this.changePassphraseMenu.Enabled = false;
      }
    }

    private void Backup()
    {
      SaveFileDialog dialog = new SaveFileDialog();
      dialog.Title = Resources.ChooseCertificateBackupDialog;
      dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      dialog.CheckPathExists = true;
      dialog.OverwritePrompt = true;
      dialog.Filter = Files.BackupFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        BackupFile backup = new BackupFile(Status.DataPath, dialog.FileName);

        backup.BeginCreate();

        while (!backup.Complete)
        {
          Status.SetProgress(Resources.ChooseCertificateBackupRunning, backup.Progress);
          Thread.Sleep(10);
        }

        if (backup.Exception == null)
        {
          Status.SetMessage(Resources.ChooseCertificateBackupSuccess, MessageType.Success);
        }
        else
        {
          Status.SetMessage(Resources.ChooseCertificateBackupFailed + backup.Exception.Message, MessageType.Error);
        }
      }
    }

    private void Restore()
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Title = Resources.ChooseCertificateRestoreDialog;
      dialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
      dialog.CheckPathExists = true;
      dialog.CheckFileExists = true;
      dialog.Filter = Files.BackupFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        BackupFile backup = new BackupFile(Status.DataPath, dialog.FileName);

        backup.BeginExtract();

        while (!backup.Complete)
        {
          Status.SetProgress(Resources.ChooseCertificateRestoreRunning, backup.Progress);
          Thread.Sleep(10);
        }

        if (backup.Exception == null)
        {
          Status.SetMessage(Resources.ChooseCertificateRestoreSuccess, MessageType.Success);
          Begin();
        }
        else
        {
          Status.SetMessage(Resources.ChooseCertificateRestoreFailed + backup.Exception.Message, MessageType.Error);
        }
      }
    }

    private void SetEnable(bool enable)
    {
      this.certificateList.Enabled = enable;
    }

    private void deleteMenu_Click(object sender, EventArgs e)
    {
      DeleteCertificate();
    }

    private void encryptMenu_Click(object sender, EventArgs e)
    {
      if (this.certificateList.SelectedItems.Count > 0)
      {
        ListViewItem item = this.certificateList.SelectedItems[0];

        if (item.Tag != null)
        {
          KeyValuePair<string, Certificate> value = (KeyValuePair<string, Certificate>)item.Tag;

          if (value.Value.PrivateKeyStatus == PrivateKeyStatus.Unencrypted)
          {
            var result = EncryptPrivateKeyDialog.ShowSetPassphrase();

            if (result.First == DialogResult.OK &&
                !result.Second.IsNullOrEmpty())
            {
              value.Value.EncryptPrivateKey(result.Second);
              value.Value.Save(value.Key);
            }
          }
        }
      }
    }

    private void changePassphraseMenu_Click(object sender, EventArgs e)
    {
      if (this.certificateList.SelectedItems.Count > 0)
      {
        ListViewItem item = this.certificateList.SelectedItems[0];

        if (item.Tag != null)
        {
          KeyValuePair<string, Certificate> value = (KeyValuePair<string, Certificate>)item.Tag;

          if (value.Value.PrivateKeyStatus == PrivateKeyStatus.Encrypted || 
              value.Value.PrivateKeyStatus == PrivateKeyStatus.Decrypted)
          {
            bool done = false;
            string message = null;

            while (!done)
            {
              var result = ChangePassphraseDialog.ShowChangePassphrase(value.Value, message);

              if (result.First == DialogResult.OK)
              {
                try
                {
                  if (result.Third.IsNullOrEmpty())
                  {
                    value.Value.DecryptPrivateKey(result.Second);
                  }
                  else
                  {
                    value.Value.ChangePassphrase(result.Second, result.Third);
                  }

                  value.Value.Save(value.Key);
                  done = true;
                }
                catch
                {
                  message = GuiResources.ChangePassphraseMessageWrong;
                }
              }
              else
              {
                done = true;
              }
            }
          }
        }
      }
    }

    private void backupMenu_Click(object sender, EventArgs e)
    {
      Backup();
    }

    private void restoreMenu_Click(object sender, EventArgs e)
    {
      Restore();
    }

    private void verifyShareproofMenu_Click(object sender, EventArgs e)
    {
      VerifyShareProof();
    }
  }
}
