using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Gui;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Prover
{
  public partial class Master : Form
  {
    private const string ProofFileFilter = "Pi-Vote Certificate Proof|*.pi-cert-proof";

    private Certificate certificate;

    public Master()
    {
      InitializeComponent();
    }

    private void createLoadCertificateButton_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Filter = Files.CertificateFileFilter;
      dialog.Title = "Load Certificate";
      dialog.CheckFileExists = true;
      dialog.Multiselect = false;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        this.certificate = Serializable.Load<Certificate>(dialog.FileName);
        this.createCertificateIdTextBox.Text = this.certificate.Id.ToString();
        this.createSaveProofButon.Enabled =
          this.certificate != null &&
          !this.createProofTextTextBox.Text.IsNullOrEmpty();
      }
    }

    private void verifyLoadProofButton_Click(object sender, EventArgs e)
    {
      OpenFileDialog dialog = new OpenFileDialog();
      dialog.Filter = ProofFileFilter;
      dialog.CheckFileExists = true;
      dialog.Multiselect = false;

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        var signedProof = Serializable.Load<Signed<CertificateProof>>(dialog.FileName);
        var proof = signedProof.Value;

        if (signedProof.VerifySimple())
        {
          this.verifyCertificateIdTextBox.Text = signedProof.Certificate.Id.ToString();
          this.verifyCertificateFingerprintTextBox.Text = signedProof.Certificate.Fingerprint;
          this.verifyProofTextTextBox.Text = proof.Text;
        }
        else
        { 
          MessageBox.Show("Signature on proof or self-signature not valid.", "Pi-Vote Certificate Proof", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
      }
    }

    private void createProofTextTextBox_TextChanged(object sender, EventArgs e)
    {
      this.createSaveProofButon.Enabled =
        this.certificate != null &&
        !this.createProofTextTextBox.Text.IsNullOrEmpty();
    }

    private void createSaveProofButon_Click(object sender, EventArgs e)
    {
      if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(this.certificate, "Sign Certificate Proof"))
      {
        var proof = new CertificateProof(this.createProofTextTextBox.Text);
        var signedProof = new Signed<CertificateProof>(proof, this.certificate);

        SaveFileDialog dialog = new SaveFileDialog();
        dialog.Filter = ProofFileFilter;
        dialog.Title = "Save Certificate Proof";

        if (dialog.ShowDialog() == DialogResult.OK)
        {
          signedProof.Save(dialog.FileName);
        }
      }
    }
  }
}
