/*
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
using System.IO;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.CaGui
{
  public partial class Master : Form
  {
    private const string CaCertFileName = "ca.pi-cert";
    private const string StorageFileName = "ca.pi-cert-storage";
    private const string DataPathPart = "Data";

    private string dataPath;

    private CACertificate Certificate { get; set; }

    private CertificateStorage CertificateStorage { get; set; }

    private Dictionary<CertificateAuthorityEntry, ListViewItem> Items { get; set; }

    public Master()
    {
      InitializeComponent();
    }

    private void Master_Load(object sender, EventArgs e)
    {
      CenterToScreen();
      PrepareSearch();
      LoadFiles();
      LoadEntries();
    }

    private void PrepareSearch()
    {
      this.searchTypeBox.Items.Add("All Types");
      this.searchTypeBox.Items.Add("CA");
      this.searchTypeBox.Items.Add("Admin");
      this.searchTypeBox.Items.Add("Authority");
      this.searchTypeBox.Items.Add("Voter");
      this.searchTypeBox.Items.Add("Server");
      this.searchTypeBox.SelectedIndex = 0;

      this.searchStatusBox.Items.Add("All Status");
      this.searchStatusBox.Items.Add("New");
      this.searchStatusBox.Items.Add("Valid");
      this.searchStatusBox.Items.Add("Revoked");
      this.searchStatusBox.Items.Add("Refused");
      this.searchStatusBox.SelectedIndex = 0;
    }

    private bool IsOfSearchedStatus(CertificateAuthorityEntry entry)
    {
      if (this.searchStatusBox.SelectedIndex == 0)
        return true;

      if (entry.Response == null)
      {
        return this.searchStatusBox.SelectedIndex == 1;
      }
      else if (entry.Response.Value.Status == SignatureResponseStatus.Accepted)
      {
        if (entry.Revoked)
        {
          return this.searchStatusBox.SelectedIndex == 3;
        }
        else
        {
          return this.searchStatusBox.SelectedIndex == 2;
        }
      }
      else if (entry.Response.Value.Status == SignatureResponseStatus.Declined)
      {
        return this.searchStatusBox.SelectedIndex == 4;
      }
      else
      {
        return true;
      }
    }

    private bool IsOfSearchedType(Certificate certificate)
    {
      if (this.searchTypeBox.SelectedIndex == 0)
        return true;

      if (certificate is CACertificate)
      {
        return this.searchTypeBox.SelectedIndex == 1;
      }
      else if (certificate is AdminCertificate)
      {
        return this.searchTypeBox.SelectedIndex == 2;
      }
      else if (certificate is AuthorityCertificate)
      {
        return this.searchTypeBox.SelectedIndex == 3;
      }
      else if (certificate is VoterCertificate)
      {
        return this.searchTypeBox.SelectedIndex == 4;
      }
      else if (certificate is ServerCertificate)
      {
        return this.searchTypeBox.SelectedIndex == 5;
      }
      else
      {
        return true;
      }
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
        Certificate = Serializable.Load<CACertificate>(DataPath(CaCertFileName));
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

      foreach (Signed<RevocationList> signedRevocationList in CertificateStorage.SignedRevocationLists.Where(list => list.Certificate.IsIdentic(Certificate)))
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
      Items = new Dictionary<CertificateAuthorityEntry, ListViewItem>();
      DirectoryInfo directory = new DirectoryInfo(this.dataPath);

      foreach (FileInfo file in directory.GetFiles("*.pi-ca-entry"))
      {
        CertificateAuthorityEntry entry = Serializable.Load<CertificateAuthorityEntry>(file.FullName);
        AddEntry(entry, file.FullName);
      }
    }

    private string TypeName(Certificate certificate)
    {
      if (certificate is CACertificate)
      {
        return "CA";
      }
      else if (certificate is AdminCertificate)
      {
        return "Admin";
      }
      else if (certificate is AuthorityCertificate)
      {
        return "Authority";
      }
      else if (certificate is VoterCertificate)
      {
        return "Voter";
      }
      else if (certificate is ServerCertificate)
      {
        return "Server";
      }
      else
      {
        return "Unknown";
      }
    }

    private bool IsOfSearchedDate(CertificateAuthorityEntry entry)
    {
      if (this.searchDateActive.Checked)
      {
        if (entry.Response == null)
        {
          return false;
        }
        else if (entry.Response.Value.Status == SignatureResponseStatus.Accepted)
        {
          return entry.Response.Value.Signature.ValidFrom <= this.searchDateBox.Value &&
                 entry.Response.Value.Signature.ValidUntil >= this.searchDateBox.Value;
        }
        else
        {
          return false;
        }
      }
      else
      {
        return true;
      }
    }

    private void UpdateList()
    {
      this.entryListView.Items.Clear();

      Items
        .Where(entry => entry.Key.RequestValue(Certificate).FullName.ToLower().Contains(this.searchTestBox.Text.ToLower()) || entry.Key.Request.Certificate.Id.ToString().ToLower().Contains(this.searchTestBox.Text.ToLower()))
        .Where(entry => IsOfSearchedType(entry.Key.Request.Certificate))
        .Where(entry => IsOfSearchedStatus(entry.Key))
        .Where(entry => IsOfSearchedDate(entry.Key))
        .Select(entry => entry.Value)
        .Foreach(item => this.entryListView.Items.Add(item));
    }

    private void AddEntry(CertificateAuthorityEntry entry, string fileName)
    {
      SignatureRequest request = entry.RequestValue(Certificate);
      ListViewItem item = new ListViewItem(entry.Certificate.Id.ToString());

      item.SubItems.Add(TypeName(entry.Request.Certificate));

      if (request.FamilyName.IsNullOrEmpty())
      {
        item.SubItems.Add(request.FirstName);
      }
      else
      {
        item.SubItems.Add(string.Format("{0}, {1}", request.FamilyName, request.FirstName));
      }

      if (entry.Response == null)
      {
        item.SubItems.Add(string.Empty);
        item.SubItems.Add(string.Empty);
        item.SubItems.Add("New");
      }
      else
      {
        SignatureResponse response = entry.Response.Value;
        switch (response.Status)
        {
          case SignatureResponseStatus.Accepted:
            item.SubItems.Add(response.Signature.ValidFrom.ToString());
            item.SubItems.Add(response.Signature.ValidUntil.ToString());

            if (entry.Revoked)
            {
              item.SubItems.Add("Revoked");
            }
            else
            {
              item.SubItems.Add("Valid");
            }
            break;
          case SignatureResponseStatus.Declined:
            item.SubItems.Add("N/A");
            item.SubItems.Add("N/A");
            item.SubItems.Add("Refused");
            break;
          default:
            break;
        }
      }

      item.Tag = fileName;

      Items.Add(entry, item);
      UpdateList();
    }

    private void createToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CreateCaDialog dialog = new CreateCaDialog();

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        if (dialog.RootCa)
        {
          Certificate = new CACertificate(null, dialog.CaName);
          Certificate.CreateSelfSignature();
          Certificate.Save(DataPath(CaCertFileName));
          CertificateStorage.AddRoot(Certificate.OnlyPublicPart);
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
            Certificate = new CACertificate(null, dialog.CaName);
            Certificate.CreateSelfSignature();
            Certificate.Save(DataPath(CaCertFileName));
            CertificateStorage.Add(Certificate.OnlyPublicPart);
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
        SignatureRequest request = new SignatureRequest(Certificate.FullName, "CA", string.Empty);
        Signed<SignatureRequest> signedRequest = new Signed<SignatureRequest>(request, Certificate);
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
              Certificate.AddSignature(response.Signature);
              CertificateStorage.Get(Certificate.Id).AddSignature(response.Signature);
              Certificate.Save(DataPath(CaCertFileName));
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
        foreach (string fileName in dialog.FileNames)
        {
          Secure<SignatureRequest> secureSignatureRequest = Serializable.Load<Secure<SignatureRequest>>(fileName);

          if (secureSignatureRequest.VerifySimple())
          {
            if (Items.Where(entry => entry.Key.Certificate.Id ==  secureSignatureRequest.Certificate.Id).Count() < 1)
            {
              CertificateAuthorityEntry entry = new CertificateAuthorityEntry(secureSignatureRequest);
              string entryFileName = DataPath(entry.Certificate.Id.ToString() + ".pi-ca-entry");
              entry.Save(DataPath(entryFileName));
              AddEntry(entry, entryFileName);
            }
            else
            {
              MessageBox.Show("Request in file " + fileName + " is already added.", "CaGui", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
          }
          else
          {
            MessageBox.Show("Request in file " + fileName + " is not valid.", "CaGui", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          }
        }
      }
    }

    private void generateRevocationListToolStripMenuItem_Click(object sender, EventArgs e)
    {
      GenerateCrlDialog dialog = new GenerateCrlDialog();
      dialog.CertificateId = Certificate.Id.ToString();
      dialog.CertificateName = Certificate.FullName;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        List<Guid> revokedIds = new List<Guid>();

        foreach (ListViewItem item in this.entryListView.Items)
        {
          string fileName = (string)item.Tag;
          CertificateAuthorityEntry entry = Serializable.Load<CertificateAuthorityEntry>(fileName);

          if (entry.Revoked)
          {
            revokedIds.Add(entry.Request.Certificate.Id);
          }
        }

        RevocationList revocationList = new RevocationList(Certificate.Id, dialog.ValidFrom, dialog.ValidUntil, revokedIds);
        string validFrom = revocationList.ValidFrom.ToString("yyyyMMdd");
        string validUntil = revocationList.ValidUntil.ToString("yyyyMMdd");
        string crlFileName = string.Format("{0}_{1}_{2}.pi-crl", Certificate.Id.ToString(), validFrom, validUntil);
        Signed<RevocationList> signedRevocationList = new Signed<RevocationList>(revocationList, Certificate);
        CertificateStorage.AddRevocationList(signedRevocationList);
        CertificateStorage.Save(DataPath(StorageFileName));
        AddRevocationList(revocationList);
      }
    }

    private void signToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.entryListView.SelectedItems.Count > 0)
      {
        ListViewItem item = this.entryListView.SelectedItems[0];
        string fileName = (string)item.Tag;
        CertificateAuthorityEntry entry = Serializable.Load<CertificateAuthorityEntry>(fileName);

        SignDialog dialog = new SignDialog();
        dialog.Display(entry, CertificateStorage, Certificate);

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          if (dialog.Accept)
          {
            entry.Sign(Certificate, dialog.ValidUntil);
            entry.Save(DataPath(fileName));
            item.SubItems[3].Text = entry.Response.Value.Signature.ValidFrom.ToString();
            item.SubItems[4].Text = entry.Response.Value.Signature.ValidUntil.ToString();
            item.SubItems[5].Text = "Valid";
          }
          else
          {
            entry.Refuse(Certificate, dialog.Reason);
            entry.Save(DataPath(fileName));
            item.SubItems[3].Text = "N/A";
            item.SubItems[4].Text = "N/A";
            item.SubItems[5].Text = "Refused";
          }

          SaveFileDialog saveDialog = new SaveFileDialog();
          saveDialog.Title = "Export Signature Response";
          saveDialog.CheckPathExists = true;
          saveDialog.Filter = Files.SignatureResponseFileFilter;

          if (saveDialog.ShowDialog() == DialogResult.OK)
          {
            entry.Response.Save(saveDialog.FileName);
          }
        }
      }
    }

    private void revokeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.entryListView.SelectedItems.Count > 0)
      {
        ListViewItem item = this.entryListView.SelectedItems[0];
        string fileName = (string)this.entryListView.SelectedItems[0].Tag;
        CertificateAuthorityEntry entry = Serializable.Load<CertificateAuthorityEntry>(fileName);

        RevokeDialog dialog = new RevokeDialog();
        dialog.Display(entry, CertificateStorage, Certificate);

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          entry.Revoke();
          entry.Save(DataPath(fileName));
          item.SubItems[5].Text = "Revoked";
        }
      }
    }

    private void entryListContextMenu_Opening(object sender, CancelEventArgs e)
    {
      bool hasRow = this.entryListView.SelectedItems.Count > 0;
      bool isAnwered = hasRow && this.entryListView.SelectedItems[0].SubItems[3].Text != string.Empty;
      bool notAnswered = hasRow && this.entryListView.SelectedItems[0].SubItems[3].Text == string.Empty;
      bool canSign = Certificate != null && Certificate.Validate(CertificateStorage) == CertificateValidationResult.Valid;

      this.signToolStripMenuItem.Enabled = notAnswered && canSign;
      this.revokeToolStripMenuItem.Enabled = isAnwered && canSign;
      this.exportResponseToolStripMenuItem.Enabled = isAnwered;
    }

    private void exportResponseToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.entryListView.SelectedItems.Count > 0)
      {
        string fileName = (string)this.entryListView.SelectedItems[0].Tag;
        CertificateAuthorityEntry entry = Serializable.Load<CertificateAuthorityEntry>(fileName);

        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Title = "Export Signature Response";
        dialog.CheckPathExists = true;
        dialog.Filter = Files.SignatureResponseFileFilter;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          entry.Response.Save(dialog.FileName);
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
        rootCertificate.Save(dialog.FileName);
      }
    }

    private void cAPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CaPropertiesDialog dialog = new CaPropertiesDialog();
      dialog.Set(Certificate, CertificateStorage);
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
          AdminCertificate certificate = new AdminCertificate(Language.English, null, fullName);
          certificate.CreateSelfSignature();

          SignatureRequest request = new SignatureRequest(dialog.FirstName, dialog.FamilyName, dialog.EmailAddress);
          Secure<SignatureRequest> signedRequest = new Secure<SignatureRequest>(request, certificate, Certificate);

          CertificateAuthorityEntry entry = new CertificateAuthorityEntry(signedRequest);
          entry.Sign(Certificate, dialog.ValidUntil);
          certificate.AddSignature(entry.Response.Value.Signature);

          string entryFileName = DataPath(entry.Certificate.Id.ToString() + ".pi-ca-entry");
          entry.Save(DataPath(entryFileName));
          AddEntry(entry, entryFileName);

          certificate.Save(saveDialog.FileName);
        }
      }
    }

    private void mainMenu_MenuActivate(object sender, EventArgs e)
    {
      bool haveCertificate = Certificate != null;
      bool canSign = haveCertificate && Certificate.Validate(CertificateStorage) == CertificateValidationResult.Valid;

      this.createToolStripMenuItem.Enabled = !haveCertificate;
      this.signatureRequestToolStripMenuItem.Enabled = haveCertificate;
      this.signatureResponseToolStripMenuItem.Enabled = haveCertificate;
      this.generateRevocationListToolStripMenuItem.Enabled = canSign;
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
          Secure<SignatureRequest> signedRequest = new Secure<SignatureRequest>(request, certificate, Certificate);

          CertificateAuthorityEntry entry = new CertificateAuthorityEntry(signedRequest);
          entry.Sign(Certificate, dialog.ValidUntil);
          certificate.AddSignature(entry.Response.Value.Signature);

          string entryFileName = DataPath(entry.Certificate.Id.ToString() + ".pi-ca-entry");
          entry.Save(DataPath(entryFileName));
          AddEntry(entry, entryFileName);

          certificate.Save(saveDialog.FileName);
        }
      }
    }

    private void searchTestBox_TextChanged(object sender, EventArgs e)
    {
      if (Items != null)
      {
        UpdateList();
      }
    }

    private void searchTypeBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (Items != null)
      {
        UpdateList();
      }
    }

    private void searchStatusBox_SelectedIndexChanged(object sender, EventArgs e)
    {
      if (Items != null)
      {
        UpdateList();
      }
    }

    private void searchDateActive_CheckedChanged(object sender, EventArgs e)
    {
      if (Items != null)
      {
        UpdateList();
      }
    }

    private void searchDateBox_ValueChanged(object sender, EventArgs e)
    {
      if (Items != null)
      {
        UpdateList();
      }
    }
  }
}
