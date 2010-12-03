﻿ /*
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.CaGui
{
  public partial class Master : Form
  {
    private enum Column
    {
      Id = 0,
      Type = 1,
      Group = 2,
      Name = 3,
      ValidFrom = 4,
      ValidUntil = 5,
      Status = 6
    }

    private const string CaCertFileName = "ca.pi-cert";
    private const string StorageFileName = "ca.pi-cert-storage";
    private const string DataPathPart = "Data";

    private string dataPath;
    private double loadProgress;

    private Column orderBy = Column.Name;

    private bool sortDescending = false;

    private CACertificate CaCertificate { get; set; }

    private CertificateStorage CertificateStorage { get; set; }

    private List<ListEntry> Entries { get; set; }

    public Master()
    {
      InitializeComponent();
    }

    private void Master_Load(object sender, EventArgs e)
    {
      CenterToScreen();

      WaitForm waitForm = new WaitForm();
      waitForm.Update("Loading CA GUI...", "Loading basic files...", 0d);
      waitForm.Show();

      LoadFiles();

      Thread loadDataThread = new Thread(LoadEntries);
      this.loadProgress = 0;
      loadDataThread.Start();

      while (loadDataThread.IsAlive)
      {
        waitForm.Update("Loading CA GUI...", "Loading entries...", this.loadProgress);
        Application.DoEvents();
        Thread.Sleep(1);
      }

      waitForm.Update("Loading CA GUI...", "Loading main view...", 0d);
      DisplayEntries();

      waitForm.Close();

      Show();
    }

    private string DataPath(string fileName)
    {
      return Path.Combine(this.dataPath, fileName);
    }

    private void LoadFiles()
    {
      this.dataPath = Path.Combine(Application.StartupPath, DataPathPart);
      
      if (!Directory.Exists(this.dataPath))
      {
        Directory.CreateDirectory(this.dataPath);
      }

      if (File.Exists(DataPath(CaCertFileName)))
      {
        CaCertificate = Serializable.Load<CACertificate>(DataPath(CaCertFileName));

        if (!DecryptCaKeyDialog.TryUnlock(CaCertificate))
        {
          Close();
          return;
        }
      }

      if (File.Exists(DataPath(StorageFileName)))
      {
        CertificateStorage = Serializable.Load<CertificateStorage>(DataPath(StorageFileName));
      }
      else
      {
        CertificateStorage = new CertificateStorage();
        CertificateStorage.Save(DataPath(StorageFileName));
      }

      foreach (Signed<RevocationList> signedRevocationList in CertificateStorage.SignedRevocationLists.Where(list => list.Certificate.IsIdentic(CaCertificate)))
      {
        AddRevocationList(signedRevocationList.Value);
      }
    }

    private void AddRevocationList(RevocationList revocationList)
    {
      ListViewItem item = new ListViewItem(revocationList.ValidFrom.ToString());
      item.SubItems.Add(revocationList.ValidUntil.ToString());
      item.SubItems.Add(revocationList.RevokedCertificates.Count.ToString());
      this.crlListView.Items.Add(item);
    }

    private void LoadEntries()
    {
      Entries = new List<ListEntry>();
      DirectoryInfo directory = new DirectoryInfo(this.dataPath);
      var files = directory.GetFiles("*.pi-ca-entry");
      int counter = 0;

      foreach (FileInfo file in files)
      {
        Entries.Add(new ListEntry(file.FullName));

        counter++;
        this.loadProgress = 100d / (double)files.Count() * (double)counter;
      }
    }

    private void DisplayEntries()
    {
      SortEntries(FilterEntries(Entries))
        .Foreach(listEntry => this.entryListView.Items.Add(listEntry.CreateItem(CaCertificate)));
    }

    private IEnumerable<ListEntry> FilterEntries(IEnumerable<ListEntry> input)
    {
      return input
        .Where(listEntry => listEntry.ContainsToken(this.searchTestBox.Text, CaCertificate) &&
                            listEntry.IsOfType(this.searchTypeBox.Value) &&
                            listEntry.IsOfStatus(this.searchStatusBox.Value) &&
                            !this.searchDateActive.Checked || listEntry.IsOfDate(this.searchDateBox.Value));
    }

    private IEnumerable<ListEntry> SortEntries(IEnumerable<ListEntry> input)
    {
      if (this.sortDescending)
      {
        switch (this.orderBy)
        {
          case Column.Id:
            return input.OrderByDescending(listEntry => listEntry.Entry.Certificate.Id.ToString());
          case Column.Type:
            return input.OrderByDescending(listEntry => listEntry.Entry.Certificate.ToType());
          case Column.Group:
            return input.OrderByDescending(listEntry => listEntry.Entry.Certificate.GetGroupName());
          case Column.Name:
            return input.OrderByDescending(listEntry => listEntry.Entry.RequestValue(CaCertificate).FullName);
          case Column.ValidFrom:
            return input.OrderByDescending(listEntry => listEntry.ValidFrom);
          case Column.ValidUntil:
            return input.OrderByDescending(listEntry => listEntry.ValidUntil);
          case Column.Status:
            return input.OrderByDescending(listEntry => listEntry.Status);
          default:
            return input;
        }
      }
      else
      {
        switch (this.orderBy)
        {
          case Column.Id:
            return input.OrderBy(listEntry => listEntry.Entry.Certificate.Id.ToString());
          case Column.Type:
            return input.OrderBy(listEntry => listEntry.Entry.Certificate.ToType());
          case Column.Group:
            return input.OrderBy(listEntry => listEntry.Entry.Certificate.GetGroupName());
          case Column.Name:
            return input.OrderBy(listEntry => listEntry.Entry.RequestValue(CaCertificate).FullName);
          case Column.ValidFrom:
            return input.OrderBy(listEntry => listEntry.ValidFrom);
          case Column.ValidUntil:
            return input.OrderBy(listEntry => listEntry.ValidUntil);
          case Column.Status:
            return input.OrderBy(listEntry => listEntry.Status);
          default:
            return input;
        }
      }
    }

    private void UpdateList()
    {
      if (Entries != null)
      {
        SuspendLayout();

        this.entryListView.Items.Clear();

        SortEntries(FilterEntries(Entries))
          .Foreach(listEntry => this.entryListView.Items.Add(listEntry.Item));

        ResumeLayout();
      }
    }

    private void createToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CreateCaDialog dialog = new CreateCaDialog();

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        if (dialog.RootCa)
        {
          CaCertificate = new CACertificate(dialog.Passphrase, dialog.CaName);
          CaCertificate.CreateSelfSignature();
          CaCertificate.Save(DataPath(CaCertFileName));
          CertificateStorage.AddRoot(CaCertificate.OnlyPublicPart);
          CertificateStorage.Save(DataPath(StorageFileName));
        }
        else
        {
          OpenFileDialog openDialog = new OpenFileDialog();
          openDialog.Title = "Open Root Certificate Authority Certificate";
          openDialog.CheckPathExists = true;
          openDialog.CheckFileExists = true;
          openDialog.Filter = Files.CertificateFileFilter;

          if (openDialog.ShowDialog() == DialogResult.OK)
          {
            CACertificate caCertificate = Serializable.Load<CACertificate>(openDialog.FileName);
            CertificateStorage.AddRoot(caCertificate);
            CaCertificate = new CACertificate(dialog.Passphrase, dialog.CaName);
            CaCertificate.CreateSelfSignature();
            CaCertificate.Save(DataPath(CaCertFileName));
            CertificateStorage.Add(CaCertificate.OnlyPublicPart);
            CertificateStorage.Save(DataPath(StorageFileName));
          }
        }
      }
    }

    private void entryListView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void signatureRequestToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SaveFileDialog dialog = new SaveFileDialog();
      dialog.Title = "Save Signature Request";
      dialog.CheckPathExists = true;
      dialog.Filter = Files.SignatureRequestFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        SignatureRequest request = new SignatureRequest(CaCertificate.FullName, "CA", string.Empty);
        Signed<SignatureRequest> signedRequest = new Signed<SignatureRequest>(request, CaCertificate);
        signedRequest.Save(dialog.FileName);
      }
    }

    private void signatureResponseToolStripMenuItem_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Title = "Open Signature Response";
      dialog.CheckPathExists = true;
      dialog.CheckFileExists = true;
      dialog.Filter = Files.SignatureResponseFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        Signed<SignatureResponse> signedResponse = Serializable.Load<Signed<SignatureResponse>>(dialog.FileName);

        if (signedResponse.VerifySimple())
        {
          SignatureResponse response = signedResponse.Value;

          switch (response.Status)
          {
            case SignatureResponseStatus.Accepted:
              CaCertificate.AddSignature(response.Signature);
              CertificateStorage.Get(CaCertificate.Id).AddSignature(response.Signature);
              CaCertificate.Save(DataPath(CaCertFileName));
              CertificateStorage.Save(DataPath(StorageFileName));
              MessageBox.Show("Signature added to CA certificate.", "Signature response", MessageBoxButtons.OK, MessageBoxIcon.Information);
              break;
            case SignatureResponseStatus.Declined:
              MessageBox.Show("Signature response was declined. Rease: \n" + response.Reason, "Signature response", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              break;
            default:
              MessageBox.Show("Signature response status not valid", "Signature response", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
              break;
          }
        }
        else
        {
          MessageBox.Show("Signature on signature response not valid", "Signature response", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
      }
    }

    private void exportPublicKeyToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SaveFileDialog dialog = new SaveFileDialog();
      dialog.Title = "Export certificate storage";
      dialog.CheckPathExists = true;
      dialog.Filter = Files.CertificateStorageFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        CertificateStorage.Save(dialog.FileName);
      }
    }

    private void importCertificateStorageToolStripMenuItem_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Title = "Import Certificate Storage";
      dialog.CheckPathExists = true;
      dialog.CheckFileExists = true;
      dialog.Filter = Files.CertificateStorageFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        CertificateStorage certificateStorage = Serializable.Load<CertificateStorage>(dialog.FileName);
        CertificateStorage.Add(certificateStorage);

        MessageBox.Show("Certificate storage import completed.", "Import Certificate Storage", MessageBoxButtons.OK, MessageBoxIcon.Information);
      }
    }

    private void importRequestsToolStripMenuItem_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Title = "Open Signature Requests";
      dialog.CheckPathExists = true;
      dialog.CheckFileExists = true;
      dialog.Multiselect = true;
      dialog.Filter = Files.SignatureRequestFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        List<string> alreadyAddedList = new List<string>();
        List<string> invalidList = new List<string>();

        foreach (string fileName in dialog.FileNames)
        {
          Secure<SignatureRequest> secureSignatureRequest = Serializable.Load<Secure<SignatureRequest>>(fileName);

          if (secureSignatureRequest.VerifySimple())
          {
            if (Entries.Any(listEntry => listEntry.Entry.Certificate.Id == secureSignatureRequest.Certificate.Id))
            {
              alreadyAddedList.Add(fileName);
            }
            else
            {
              CertificateAuthorityEntry entry = new CertificateAuthorityEntry(secureSignatureRequest);
              string entryFileName = DataPath(entry.Certificate.Id.ToString() + ".pi-ca-entry");
              entry.Save(DataPath(entryFileName));

              ListEntry listEntry = new ListEntry(entryFileName, entry);
              Entries.Add(listEntry);
              this.entryListView.Items.Add(listEntry.CreateItem(CaCertificate));
            }
          }
          else
          {
            invalidList.Add(fileName);
          }
        }

        if (invalidList.Count > 0)
        {
          StringBuilder message = new StringBuilder();
          message.AppendLine("The following request are not valid:");
          invalidList.ForEach(invalidRequest => message.AppendLine(invalidRequest));
          MessageBox.Show(message.ToString(), "Pi-Vote CA GUI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        if (alreadyAddedList.Count > 0)
        {
          StringBuilder message = new StringBuilder();
          message.AppendLine("The following request are already in your list:");
          alreadyAddedList.ForEach(invalidRequest => message.AppendLine(invalidRequest));
          MessageBox.Show(message.ToString(), "Pi-Vote CA GUI", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
      }
    }

    private bool EntrySelected
    {
      get { return this.entryListView.SelectedItems.Count > 0; }
    }

    private ListEntry SelectedEntry
    {
      get
      {
        if (EntrySelected)
        {
          return (ListEntry)this.entryListView.SelectedItems[0].Tag;
        }
        else
        {
          return null;
        }
      }
    }

    private void generateRevocationListToolStripMenuItem_Click(object sender, EventArgs e)
    {
      GenerateCrlDialog dialog = new GenerateCrlDialog();
      dialog.CertificateId = CaCertificate.Id.ToString();
      dialog.CertificateName = CaCertificate.FullName;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        IEnumerable<Guid> revokedIds = Entries.Where(listEntry => listEntry.Entry.Revoked).Select(listEntry => listEntry.Entry.Certificate.Id);
        RevocationList revocationList = new RevocationList(CaCertificate.Id, dialog.ValidFrom, dialog.ValidUntil, revokedIds);
        string validFrom = revocationList.ValidFrom.ToString("yyyyMMdd");
        string validUntil = revocationList.ValidUntil.ToString("yyyyMMdd");
        string crlFileName = string.Format("{0}_{1}_{2}.pi-crl", CaCertificate.Id.ToString(), validFrom, validUntil);
        Signed<RevocationList> signedRevocationList = new Signed<RevocationList>(revocationList, CaCertificate);
        CertificateStorage.AddRevocationList(signedRevocationList);
        CertificateStorage.Save(DataPath(StorageFileName));
        AddRevocationList(revocationList);
      }
    }

    private void signToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (EntrySelected)
      {
        var listEntry = SelectedEntry;

        SignDialog dialog = new SignDialog();
        dialog.Display(listEntry.Entry, CertificateStorage, CaCertificate, Entries.Select(lstEntry => lstEntry.Entry));

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          if (dialog.Accept)
          {
            listEntry.Entry.Sign(CaCertificate, dialog.ValidFrom, dialog.ValidUntil);
            listEntry.Save();
            listEntry.UpdateItem(CaCertificate);
          }
          else
          {
            listEntry.Entry.Refuse(CaCertificate, dialog.Reason);
            listEntry.Save();
            listEntry.UpdateItem(CaCertificate);
          }

          SaveFileDialog saveDialog = new SaveFileDialog();
          saveDialog.Title = "Export Signature Response";
          saveDialog.CheckPathExists = true;
          saveDialog.Filter = Files.SignatureResponseFileFilter;

          if (saveDialog.ShowDialog() == DialogResult.OK)
          {
            listEntry.Entry.Response.Save(saveDialog.FileName);
          }
        }
      }
    }

    private void revokeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (EntrySelected)
      {
        var listEntry = SelectedEntry;

        RevokeDialog dialog = new RevokeDialog();
        dialog.Display(listEntry.Entry, CertificateStorage, CaCertificate);

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          listEntry.Entry.Revoke();
          listEntry.Save();
          listEntry.UpdateItem(CaCertificate);
        }
      }
    }

    private void entryListContextMenu_Opening(object sender, CancelEventArgs e)
    {
      bool hasRow = this.entryListView.SelectedItems.Count > 0;
      bool isAnwered = hasRow && this.entryListView.SelectedItems[0].SubItems[4].Text != string.Empty;
      bool notAnswered = hasRow && this.entryListView.SelectedItems[0].SubItems[4].Text == string.Empty;
      bool canSign = CaCertificate != null && CaCertificate.Validate(CertificateStorage) == CertificateValidationResult.Valid;

      this.signToolStripMenuItem.Enabled = notAnswered && canSign;
      this.revokeToolStripMenuItem.Enabled = isAnwered && canSign;
      this.exportResponseToolStripMenuItem.Enabled = isAnwered;
    }

    private void exportResponseToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (EntrySelected)
      {
        var listEntry = SelectedEntry;

        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Title = "Export Signature Response";
        dialog.CheckPathExists = true;
        dialog.Filter = Files.SignatureResponseFileFilter;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          listEntry.Entry.Response.Save(dialog.FileName);
        }
      }
    }

    private void exportRootCertificateToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SaveFileDialog dialog = new SaveFileDialog();
      dialog.Title = "Export Root Certificate";
      dialog.CheckPathExists = true;
      dialog.Filter = Files.CertificateFileFilter;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        Certificate rootCertificate = CertificateStorage.Certificates.Where(certificate => CertificateStorage.IsRootCertificate(certificate)).First();
        rootCertificate.OnlyPublicPart.Save(dialog.FileName);
      }
    }

    private void cAPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CaPropertiesDialog dialog = new CaPropertiesDialog();
      dialog.Set(CaCertificate, CertificateStorage);
      dialog.ShowDialog();
    }

    private void createAdminCertificateToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CreateAdminDialog dialog = new CreateAdminDialog();

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        SaveFileDialog saveDialog = new SaveFileDialog();
        saveDialog.Title = "Save Admin Certificate";
        saveDialog.CheckPathExists = true;
        saveDialog.Filter = Files.CertificateFileFilter;

        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
          string fullName = string.Format("{0} {1}, {2}", dialog.FirstName, dialog.FamilyName, dialog.Function);
          AdminCertificate certificate = new AdminCertificate(Language.English, dialog.Passphrase, fullName);
          certificate.CreateSelfSignature();

          SignatureRequest request = new SignatureRequest(dialog.FirstName, dialog.FamilyName, dialog.EmailAddress);
          Secure<SignatureRequest> signedRequest = new Secure<SignatureRequest>(request, CaCertificate, certificate);

          CertificateAuthorityEntry entry = new CertificateAuthorityEntry(signedRequest);
          entry.Sign(CaCertificate, DateTime.Now, dialog.ValidUntil);
          certificate.AddSignature(entry.Response.Value.Signature);

          string entryFileName = DataPath(entry.Certificate.Id.ToString() + ".pi-ca-entry");
          entry.Save(DataPath(entryFileName));

          ListEntry listEntry = new ListEntry(entryFileName, entry);
          Entries.Add(listEntry);
          this.entryListView.Items.Add(listEntry.CreateItem(CaCertificate));

          certificate.Save(saveDialog.FileName);
        }
      }
    }

    private void mainMenu_MenuActivate(object sender, EventArgs e)
    {
      bool haveCertificate = CaCertificate != null;
      bool validCertificate = haveCertificate &&
        CaCertificate.Validate(CertificateStorage) == CertificateValidationResult.Valid;
      bool canSign = validCertificate &&
        CertificateStorage.HasValidRevocationList(CaCertificate.Id, DateTime.Now);

      this.createToolStripMenuItem.Enabled = !haveCertificate;
      this.signatureRequestToolStripMenuItem.Enabled = haveCertificate;
      this.signatureResponseToolStripMenuItem.Enabled = haveCertificate;
      this.generateRevocationListToolStripMenuItem.Enabled = validCertificate;
      this.importRequestsToolStripMenuItem.Enabled = canSign;
      this.exportRootCertificateToolStripMenuItem.Enabled = haveCertificate;
      this.cAPropertiesToolStripMenuItem.Enabled = haveCertificate;
      this.createAdminCertificateToolStripMenuItem.Enabled = canSign;
      this.createServerCertifiToolStripMenuItem.Enabled = canSign;
    }

    private void createServerCertifiToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CreateServerDialog dialog = new CreateServerDialog();

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        SaveFileDialog saveDialog = new SaveFileDialog();
        saveDialog.Title = "Save Server Certificate";
        saveDialog.CheckPathExists = true;
        saveDialog.Filter = Files.CertificateFileFilter;

        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
          ServerCertificate certificate = new ServerCertificate(dialog.FullName);
          certificate.CreateSelfSignature();

          SignatureRequest request = new SignatureRequest(dialog.FullName, string.Empty, string.Empty);
          Secure<SignatureRequest> signedRequest = new Secure<SignatureRequest>(request, CaCertificate, certificate);

          CertificateAuthorityEntry entry = new CertificateAuthorityEntry(signedRequest);
          entry.Sign(CaCertificate, DateTime.Now, dialog.ValidUntil);
          certificate.AddSignature(entry.Response.Value.Signature);

          string entryFileName = DataPath(entry.Certificate.Id.ToString() + ".pi-ca-entry");
          entry.Save(DataPath(entryFileName));

          ListEntry listEntry = new ListEntry(entryFileName, entry);
          Entries.Add(listEntry);
          this.entryListView.Items.Add(listEntry.CreateItem(CaCertificate));

          certificate.Save(saveDialog.FileName);
        }
      }
    }

    private void searchTestBox_TextChanged(object sender, EventArgs e)
    {
      UpdateList();
    }

    private void searchTypeBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateList();
    }

    private void searchStatusBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateList();
    }

    private void searchDateActive_CheckedChanged(object sender, EventArgs e)
    {
      UpdateList();
    }

    private void searchDateBox_ValueChanged(object sender, EventArgs e)
    {
      UpdateList();
    }

    private void entryListView_ColumnClick(object sender, ColumnClickEventArgs e)
    {
      Column newColumn = (Column)e.Column;

      if (this.orderBy != newColumn)
      {
        this.orderBy = newColumn;
        this.sortDescending = false;
      }
      else
      {
        this.sortDescending = !this.sortDescending;
      }

      UpdateList();
    }
  }
}
