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

    private CACertificate Certificate;

    private CertificateStorage CertificateStorage;

    public Master()
    {
      InitializeComponent();
    }

    private void Master_Load(object sender, EventArgs e)
    {
      CenterToScreen();
      LoadFiles();
      LoadEntries();
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

      this.createToolStripMenuItem.Enabled = Certificate == null;
      this.signatureRequestToolStripMenuItem.Enabled = Certificate != null;
      this.signatureResponseToolStripMenuItem.Enabled = Certificate != null;
      this.generateRevocationListToolStripMenuItem.Enabled = Certificate != null;
      this.importRequestsToolStripMenuItem.Enabled = Certificate != null;
      this.exportRootCertificateToolStripMenuItem.Enabled = Certificate != null;
      this.cAPropertiesToolStripMenuItem.Enabled = Certificate != null;
      this.createAdminCertificateToolStripMenuItem.Enabled = Certificate != null;
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
      else
      {
        return "Unknown";
      }
    }

    private void AddEntry(CertificateAuthorityEntry entry, string fileName)
    {
      SignatureRequest request = entry.Request.Value;
      ListViewItem item = new ListViewItem(entry.Certificate.Id.ToString());

      item.SubItems.Add(TypeName(entry.Request.Certificate));
      item.SubItems.Add(string.Format("{0}, {1}", request.FamilyName, request.FirstName));

      if (entry.Response == null)
      {
        item.SubItems.Add(string.Empty);
        item.SubItems.Add(string.Empty);
        item.SubItems.Add(string.Empty);
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

      this.entryListView.Items.Add(item);
    }

    private void createToolStripMenuItem_Click(object sender, EventArgs e)
    {
      CreateCaDialog dialog = new CreateCaDialog();

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        if (dialog.RootCa)
        {
          Certificate = new CACertificate(dialog.CaName);
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
          openDialog.Filter = "Pi-Vote Certificate|*.pi-cert";

          if (openDialog.ShowDialog() == DialogResult.OK)
          {
            CACertificate caCertificate = Serializable.Load<CACertificate>(openDialog.FileName);
            CertificateStorage.AddRoot(caCertificate);
            Certificate = new CACertificate(dialog.CaName);
            Certificate.CreateSelfSignature();
            Certificate.Save(DataPath(CaCertFileName));
            CertificateStorage.Add(Certificate.OnlyPublicPart);
            CertificateStorage.Save(DataPath(StorageFileName));
          }
        }
      }

      this.createToolStripMenuItem.Enabled = Certificate == null;
      this.signatureRequestToolStripMenuItem.Enabled = Certificate != null;
      this.signatureResponseToolStripMenuItem.Enabled = Certificate != null;
      this.generateRevocationListToolStripMenuItem.Enabled = Certificate != null;
      this.importRequestsToolStripMenuItem.Enabled = Certificate != null;
      this.exportRootCertificateToolStripMenuItem.Enabled = Certificate != null;
      this.cAPropertiesToolStripMenuItem.Enabled = Certificate != null;
      this.createAdminCertificateToolStripMenuItem.Enabled = Certificate != null;
    }

    private void entryListView_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    private void signatureRequestToolStripMenuItem_Click(object sender, EventArgs e)
    {
      SaveFileDialog dialog = new SaveFileDialog();
      dialog.Title = "Save Signature Request";
      dialog.CheckPathExists = true;
      dialog.Filter = "Pi-Vote Signature Request|*.pi-sig-req";

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
      dialog.Filter = "Pi-Vote Signature Request|*.pi-sig-resp";

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
      dialog.Filter = "Pi-Vote Certificate Storage|*.pi-cert-storage";

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
      dialog.Filter = "Pi-Vote Certificate Storage|*.pi-cert-storage";

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
      dialog.Filter = "Pi-Vote Signature Request|*.pi-sig-req";

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        foreach (string fileName in dialog.FileNames)
        {
          Signed<SignatureRequest> signedRequest = Serializable.Load<Signed<SignatureRequest>>(fileName);
          bool alreadyAdded = false;

          foreach (ListViewItem item in this.entryListView.Items)
          {
            if (item.Text == signedRequest.Certificate.Id.ToString())
            {
              alreadyAdded = true;
            }
          }

          if (!alreadyAdded)
          {
            CertificateAuthorityEntry entry = new CertificateAuthorityEntry(signedRequest);
            string entryFileName = DataPath(entry.Certificate.Id.ToString() + ".pi-ca-entry");
            entry.Save(DataPath(entryFileName));
            AddEntry(entry, entryFileName);
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
        dialog.CertificateId = this.entryListView.SelectedItems[0].Text;
        dialog.CertificateType = this.entryListView.SelectedItems[0].SubItems[1].Text;
        dialog.CertificateName = this.entryListView.SelectedItems[0].SubItems[2].Text;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          entry.Sign(Certificate, dialog.ValidUntil);
          entry.Save(DataPath(fileName));
          item.SubItems[3].Text = entry.Response.Value.Signature.ValidFrom.ToString();
          item.SubItems[4].Text = entry.Response.Value.Signature.ValidUntil.ToString();
          item.SubItems[5].Text = "Valid";

          SaveFileDialog saveDialog = new SaveFileDialog();
          saveDialog.Title = "Export Signature Response";
          saveDialog.CheckPathExists = true;
          saveDialog.Filter = "Pi-Vote Signature Response|*.pi-sig-resp";

          if (saveDialog.ShowDialog() == DialogResult.OK)
          {
            entry.Response.Save(saveDialog.FileName);
          }
        }
      }
    }

    private void refuseToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.entryListView.SelectedItems.Count > 0)
      {
        ListViewItem item = this.entryListView.SelectedItems[0];
        string fileName = (string)item.Tag;
        CertificateAuthorityEntry entry = Serializable.Load<CertificateAuthorityEntry>(fileName);

        RefuseDialog dialog = new RefuseDialog();
        dialog.CertificateId = this.entryListView.SelectedItems[0].Text;
        dialog.CertificateType = this.entryListView.SelectedItems[0].SubItems[1].Text;
        dialog.CertificateName = this.entryListView.SelectedItems[0].SubItems[2].Text;

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          entry.Refuse(Certificate, dialog.Reason);
          entry.Save(DataPath(fileName));
          item.SubItems[3].Text = "N/A";
          item.SubItems[4].Text = "N/A";
          item.SubItems[5].Text = "Refused";

          SaveFileDialog saveDialog = new SaveFileDialog();
          saveDialog.Title = "Export Signature Response";
          saveDialog.CheckPathExists = true;
          saveDialog.Filter = "Pi-Vote Signature Response|*.pi-sig-resp";

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

        string message = string.Format(
          "Do you really want to revoke Id {0}, Type {1}, Name {2}", 
          (string)item.Text, 
          (string)item.SubItems[1].Text,
          (string)item.SubItems[2].Text);

        if (MessageBox.Show(message, "Revoke Certificate", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
          entry.Revoke();
          entry.Save(DataPath(fileName));
          item.SubItems[5].Text = "Revoked";
        }
      }
    }

    private void entryListContextMenu_Opening(object sender, CancelEventArgs e)
    {
      if (this.entryListView.SelectedItems.Count > 0)
      {
        if (this.entryListView.SelectedItems[0].SubItems[3].Text == string.Empty)
        {
          this.signToolStripMenuItem.Enabled = true;
          this.refuseToolStripMenuItem.Enabled = true;
          this.revokeToolStripMenuItem.Enabled = false;
          this.exportResponseToolStripMenuItem.Enabled = false;
        }
        else
        {
          this.signToolStripMenuItem.Enabled = false;
          this.refuseToolStripMenuItem.Enabled = false;
          this.revokeToolStripMenuItem.Enabled = true;
          this.exportResponseToolStripMenuItem.Enabled = true;
        }
      }
      else
      {
        this.signToolStripMenuItem.Enabled = false;
        this.refuseToolStripMenuItem.Enabled = false;
        this.revokeToolStripMenuItem.Enabled = false;
        this.exportResponseToolStripMenuItem.Enabled = false;
      }
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
        dialog.Filter = "Pi-Vote Signature Response|*.pi-sig-resp";

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
      dialog.Filter = "Pi-Vote Certificate|*.pi-cert";

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
        saveDialog.Filter = "Pi-Vote Certificate|*.pi-cert";

        if (saveDialog.ShowDialog() == DialogResult.OK)
        {
          string fullName = string.Format("{0} {1}, {2}", dialog.FirstName, dialog.FamilyName, dialog.Function);
          AdminCertificate certificate = new AdminCertificate(fullName);
          certificate.CreateSelfSignature();

          SignatureRequest request = new SignatureRequest(dialog.FirstName, dialog.FamilyName, dialog.EmailAddress);
          Signed<SignatureRequest> signedRequest = new Signed<SignatureRequest>(request, certificate);

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
  }
}
