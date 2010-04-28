/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Client
{
  public partial class ViewShareControl : UserControl
  {
    public int AuthorityIndex { get; set; }

    public BadShareProof Proof { get; set; }

    public Certificate ComplainingAuthorityCertificate { get; set; }

    public DateTime ValidationDate
    {
      get { return certificateControl.ValidationDate; }
      set { certificateControl.ValidationDate = value; }
    }

    public void Display()
    {
      if (Proof != null && 
        AuthorityIndex >= 1 &&
        AuthorityIndex <= Proof.Parameters.QV.AuthorityCount)
      {
        Certificate authorityCertificate = Proof.Authorities[AuthorityIndex];

        this.certificateControl.CertificateStorage = Proof.CertificateStorage;
        this.certificateControl.Certificate = authorityCertificate;

        var signedSharePart = Proof.AllShareParts.ShareParts.Where(x => x.Value.AuthorityIndex == AuthorityIndex).First();
        var sharePart = signedSharePart.Value;

        if (signedSharePart.Verify(Proof.CertificateStorage, ValidationDate) &&
            signedSharePart.Certificate.IsIdentic(authorityCertificate))
        {
          this.signatureTextBox.Text = Resources.CertificateValid;
          this.signatureTextBox.BackColor = Color.Green;
        }
        else
        {
          this.signatureTextBox.Text = Resources.CertificateInvalid;
          this.signatureTextBox.BackColor = Color.Red;
        }

        TrapDoor trapDoor = Proof.TrapDoors[AuthorityIndex];
        Encrypted<Share> encryptedShare = sharePart.EncryptedShares[Proof.ComplainingAuthorityIndex - 1];
        Share share = encryptedShare.DecryptWithTrapDoor(trapDoor, ComplainingAuthorityCertificate);

        if (share.Verify(Proof.ComplainingAuthorityIndex, Proof.Parameters, sharePart.VerificationValues))
        {
          this.dataTextBox.Text = Resources.CertificateValid;
          this.dataTextBox.BackColor = Color.Green;
        }
        else
        {
          this.dataTextBox.Text = Resources.CertificateInvalid;
          this.dataTextBox.BackColor = Color.Red;
        }
      }
    }

    public ViewShareControl()
    {
      InitializeComponent();
    }

    public void SetLanguage()
    {
      this.signatureLabel.Text = Resources.ViewShareSignature;
      this.dataLabel.Text = Resources.ViewShareData;

      this.certificateControl.SetLanguage();

      Display();
    }
  }
}
