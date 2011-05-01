using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Circle.Certificates
{
  public partial class CertificateManagerDialog : Form
  {
    private CircleController controller;

    private Dictionary<Certificate, ListViewItem> certificates;

    public CertificateManagerDialog()
    {
      InitializeComponent();
    }

    private void CertificateManagerDialog_Load(object sender, EventArgs e)
    {
      var screenBounds = Screen.PrimaryScreen.Bounds;

      if (screenBounds.Width < Width)
      {
        Width = screenBounds.Width;
      }

      if (screenBounds.Height < Height)
      {
        Height = screenBounds.Height;
      } 

      CenterToScreen();

      this.exportButton.Enabled = this.certificateList.SelectedItems.Count > 0;
      this.removeButton.Enabled = this.certificateList.SelectedItems.Count > 0;
      this.setPasswordButton.Enabled = this.certificateList.SelectedItems.Count > 0;
      this.exportContextMenu.Enabled = this.certificateList.SelectedItems.Count > 0;
      this.removeContextMenu.Enabled = this.certificateList.SelectedItems.Count > 0;
      this.setPasswordContextMenu.Enabled = this.certificateList.SelectedItems.Count > 0;

      Text = Resources.CertificateManagerTitle;

      this.idColumnHeader.Text = Resources.CertificateManagerColumnId;
      this.typeColumnHeader.Text = Resources.CertificateManagerColumnType;
      this.groupNameColumnHeader.Text = Resources.CertificateManagerColumnGroupName;
      this.statusColumnHeader.Text = Resources.CertificateManagerColumnStatus;
      this.validFromColumnHeader.Text = Resources.CertificateManagerColumnValidFrom;
      this.validUntilColumnHeader.Text = Resources.CertificateManagerColumnValidUntil;

      this.importContextMenu.Text = Resources.CertificateManagerImport;
      this.exportContextMenu.Text = Resources.CertificateManagerExport;
      this.backupContextMenu.Text = Resources.CertificateManagerBackup;
      this.restoreContextMenu.Text = Resources.CertificateManagerRestore;
      this.createNewContextMenu.Text = Resources.CertificateManagerCreateNew;
      this.setPasswordContextMenu.Text = Resources.CertificateManagerSetPassword;
      this.removeContextMenu.Text = Resources.CertificateManagerRemove;
      this.refreshContextMenu.Text = Resources.CertificateManagerRefresh;
      
      this.importButton.Text = Resources.CertificateManagerImport;
      this.exportButton.Text = Resources.CertificateManagerExport;
      this.backupButton.Text = Resources.CertificateManagerBackup;
      this.restoreButton.Text = Resources.CertificateManagerRestore;
      this.createNewButton.Text = Resources.CertificateManagerCreateNew;
      this.setPasswordButton.Text = Resources.CertificateManagerSetPassword;
      this.removeButton.Text = Resources.CertificateManagerRemove;
      this.refreshButton.Text = Resources.CertificateManagerRefresh;
      this.closeButton.Text = Resources.CertificateManagerClose;
    }

    public void Set(CircleController controller)
    {
      this.controller = controller;

      LoadCertificates();
    }

    private void LoadCertificates()
    {
      this.certificateList.Items.Clear();
      this.certificates = new Dictionary<Certificate, ListViewItem>();

      foreach (var certificate in this.controller.Certificates)
      {
        AddItem(certificate);
      }
    }

    private void AddItem(Certificate certificate)
    {
      ListViewItem item = new ListViewItem(certificate.Id.ToString());
      item.SubItems.Add(certificate.TypeText);

      if (certificate is VoterCertificate)
      {
        item.SubItems.Add(controller.Status.GetGroupName(((VoterCertificate)certificate).GroupId));
      }
      else if (certificate is AuthorityCertificate)
      {
        item.SubItems.Add(((AuthorityCertificate)certificate).FullName);
      }
      else if (certificate is AdminCertificate)
      {
        item.SubItems.Add(((AdminCertificate)certificate).FullName);
      }
      else
      {
        item.SubItems.Add(string.Empty);
      }

      item.SubItems.Add(certificate.Validate(controller.Status.CertificateStorage).Text());

      DateTime validFrom = certificate.ExpectedValidFrom(controller.Status.CertificateStorage);

      if (validFrom == DateTime.MinValue)
      {
        item.SubItems.Add(Resources.CertificateManagerValidIndefinite);
      }
      else if (validFrom == DateTime.MaxValue)
      {
        item.SubItems.Add(string.Empty);
      }
      else
      {
        item.SubItems.Add(validFrom.ToShortDateString());
      }

      DateTime validUntil = certificate.ExpectedValidUntil(controller.Status.CertificateStorage, DateTime.Now);

      if (validUntil == DateTime.MinValue)
      {
        item.SubItems.Add(string.Empty);
      }
      else if (validUntil == DateTime.MaxValue)
      {
        item.SubItems.Add(Resources.CertificateManagerValidIndefinite);
      }
      else
      {
        item.SubItems.Add(validUntil.ToShortDateString());
      }

      item.Tag = certificate;
      this.certificateList.Items.Add(item);

      this.certificates.Add(certificate, item);
    }

    private void UpdateItem(Certificate certificate)
    {
      ListViewItem item = this.certificates[certificate];

      item.SubItems[3].Text = certificate.Validate(controller.Status.CertificateStorage).Text();

      DateTime validFrom = certificate.ExpectedValidFrom(controller.Status.CertificateStorage);

      if (validFrom == DateTime.MinValue)
      {
        item.SubItems[4].Text = Resources.CertificateManagerValidIndefinite;
      }
      else if (validFrom == DateTime.MaxValue)
      {
        item.SubItems[4].Text = string.Empty;
      }
      else
      {
        item.SubItems[4].Text = validFrom.ToShortDateString();
      }

      DateTime validUntil = certificate.ExpectedValidUntil(controller.Status.CertificateStorage, DateTime.Now);

      if (validUntil == DateTime.MinValue)
      {
        item.SubItems[5].Text = string.Empty;
      }
      else if (validUntil == DateTime.MaxValue)
      {
        item.SubItems[5].Text = Resources.CertificateManagerValidIndefinite;
      }
      else
      {
        item.SubItems[5].Text = validUntil.ToShortDateString();
      }
    }

    private void UpdateCertificates()
    {
      List<Certificate> removeList = new List<Certificate>(
        this.certificates.Keys.Where(certificate => !this.controller.Certificates.Contains(certificate)));

      foreach (var certificate in removeList)
      {
        this.certificateList.Items.Remove(this.certificates[certificate]);
        this.certificates.Remove(certificate);
      }

      foreach (var certificate in this.controller.Certificates)
      {
        if (this.certificates.ContainsKey(certificate))
        {
          UpdateItem(certificate);
        }
        else
        {
          AddItem(certificate);
        }
      }
    }

    private void closeButton_Click(object sender, EventArgs e)
    {
      Close();
    }

    public static void ShowCertificates(CircleController controller)
    {
      CertificateManagerDialog dialog = new CertificateManagerDialog();
      dialog.Set(controller);
      dialog.ShowDialog();
    }

    private void certificateList_SelectedIndexChanged(object sender, EventArgs e)
    {
      this.exportButton.Enabled = this.certificateList.SelectedItems.Count > 0;
      this.removeButton.Enabled = this.certificateList.SelectedItems.Count > 0;
      this.setPasswordButton.Enabled = this.certificateList.SelectedItems.Count > 0;
      this.exportContextMenu.Enabled = this.certificateList.SelectedItems.Count > 0;
      this.removeContextMenu.Enabled = this.certificateList.SelectedItems.Count > 0;
      this.setPasswordContextMenu.Enabled = this.certificateList.SelectedItems.Count > 0;
    }

    private void createNewButton_Click(object sender, EventArgs e)
    {
      CreateNew();
    }

    private void CreateNew()
    {
      Create.CreateCertificateDialog.ShowCreateNewCertificate(this.controller);
      UpdateCertificates();
    }

    private void removeButton_Click(object sender, EventArgs e)
    {
      Remove();
    }

    private void Remove()
    {
      if (this.certificateList.SelectedItems.Count > 0)
      {
        ListViewItem item = this.certificateList.SelectedItems[0];
        Certificate certificate = (Certificate)item.Tag;
        controller.DeactiveCertificate(certificate);
        item.Remove();
      }
    }

    private void importButton_Click(object sender, EventArgs e)
    {
      Import();
    }

    private void Import()
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Filter = Files.CertificateFileFilter;
      dialog.CheckFileExists = true;
      dialog.Multiselect = false;
      dialog.Title = "Import Certificate";

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        try
        {
          var certificate = Serializable.Load<Certificate>(dialog.FileName);
          this.controller.SaveCertificate(certificate);
          AddItem(certificate);
        }
        catch (Exception exception)
        {
          Error.ErrorDialog.ShowError(exception);
        }
      }
    }

    private void exportButton_Click(object sender, EventArgs e)
    {
      Export();
    }

    private void Export()
    {
      if (this.certificateList.SelectedItems.Count > 0)
      {
        ListViewItem item = this.certificateList.SelectedItems[0];
        Certificate certificate = (Certificate)item.Tag;

        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Filter = Files.CertificateFileFilter;
        dialog.CheckPathExists = true;
        dialog.Title = "Export Certificate";

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          try
          {
            certificate.Save(dialog.FileName);
          }
          catch (Exception exception)
          {
            Error.ErrorDialog.ShowError(exception);
          }
        }
      }
    }

    private void backupButton_Click(object sender, EventArgs e)
    {
      Backup();
    }

    private void Backup()
    {
      SaveFileDialog dialog = new SaveFileDialog();
      dialog.Filter = Files.BackupFileFilter;
      dialog.CheckPathExists = true;
      dialog.Title = Resources.SaveBackupDialogTitle;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        try
        {
          BackupFile backup = new BackupFile(this.controller.Status.DataPath, dialog.FileName);
          backup.BeginCreate();

          while (!backup.Complete)
          {
            Application.DoEvents();
            Thread.Sleep(1);
          }

          MessageForm.Show(Resources.SaveBackupDone, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception exception)
        {
          Error.ErrorDialog.ShowError(exception);
        }
      }
    }

    private void restoreButton_Click(object sender, EventArgs e)
    {
      Restore();
    }

    private void Restore()
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Filter = Files.BackupFileFilter;
      dialog.CheckFileExists = true;
      dialog.Multiselect = false;
      dialog.Title = Resources.RestoreBackupDialogTitle;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        try
        {
          BackupFile backup = new BackupFile(this.controller.Status.DataPath, dialog.FileName);
          backup.BeginExtract();

          while (!backup.Complete)
          {
            Application.DoEvents();
            Thread.Sleep(1);
          }

          Status.TextStatusDialog.ShowInfo(this.controller, this);
          this.controller.LoadCertificates();
          Status.TextStatusDialog.HideInfo();
          LoadCertificates();

          MessageForm.Show(Resources.RestoreBackupDone, Resources.MessageBoxTitle, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception exception)
        {
          Error.ErrorDialog.ShowError(exception);
        }
      }
    }

    private void setPasswordButton_Click(object sender, EventArgs e)
    {
      SetPassword();
    }

    private void SetPassword()
    {
      if (this.certificateList.SelectedItems.Count > 0)
      {
        ListViewItem item = this.certificateList.SelectedItems[0];
        Certificate certificate = (Certificate)item.Tag;

        if (certificate.PrivateKeyStatus == PrivateKeyStatus.Unencrypted)
        {
          var result = EncryptPrivateKeyDialog.ShowSetPassphrase();

          if (result.First == DialogResult.OK)
          {
            if (!result.Second.IsNullOrEmpty())
            {
              certificate.EncryptPrivateKey(result.Second);
              this.controller.SaveCertificate(certificate);
            }
          }
        }
        else if (certificate.PrivateKeyStatus == PrivateKeyStatus.Encrypted ||
            certificate.PrivateKeyStatus == PrivateKeyStatus.Decrypted)
        {
          bool done = false;
          string message = null;

          while (!done)
          {
            var result = ChangePassphraseDialog.ShowChangePassphrase(certificate, message);

            if (result.First == DialogResult.OK)
            {
              try
              {
                if (result.Third.IsNullOrEmpty())
                {
                  certificate.DecryptPrivateKey(result.Second);
                }
                else
                {
                  certificate.ChangePassphrase(result.Second, result.Third);
                }

                this.controller.SaveCertificate(certificate);
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

    private void createNewContextMenu_Click(object sender, EventArgs e)
    {
      CreateNew();
    }

    private void removeContextMenu_Click(object sender, EventArgs e)
    {
      Remove();
    }

    private void importContextMenu_Click(object sender, EventArgs e)
    {
      Import();
    }

    private void exportContextMenu_Click(object sender, EventArgs e)
    {
      Export();
    }

    private void backupContextMenu_Click(object sender, EventArgs e)
    {
      Backup();
    }

    private void restoreContextMenu_Click(object sender, EventArgs e)
    {
      Restore();
    }

    private void setPasswordContextMenu_Click(object sender, EventArgs e)
    {
      SetPassword();
    }

    private void CertificateManagerDialog_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Escape:
          Close();
          break;
      }
    }

    private void refreshButton_Click(object sender, EventArgs e)
    {
      RefreshCertificates();
    }

    private void RefreshCertificates()
    {
      Status.TextStatusDialog.ShowInfo(this.controller, this);
      this.controller.LoadCertificates();
      Status.TextStatusDialog.HideInfo();
      UpdateCertificates();
    }

    private void refreshContextMenu_Click(object sender, EventArgs e)
    {
      RefreshCertificates();
    }
  }
}
