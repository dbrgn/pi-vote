/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;

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
        AuthorityIndex <= Proof.Parameters.AuthorityCount)
      {
        Certificate authorityCertificate = Proof.Authorities[AuthorityIndex];

        this.certificateControl.CertificateStorage = Proof.CertificateStorage;
        this.certificateControl.Certificate = authorityCertificate;

        var signedSharePart = Proof.AllShareParts.ShareParts.Where(x => x.Value.AuthorityIndex == AuthorityIndex).First();
        var sharePart = signedSharePart.Value;

        TrapDoor trapDoor = Proof.TrapDoors[AuthorityIndex];
        Encrypted<Share> encryptedShare = sharePart.EncryptedShares[Proof.ComplainingAuthorityIndex - 1];
        Share share = encryptedShare.DecryptWithTrapDoor(trapDoor, ComplainingAuthorityCertificate);

        if (signedSharePart.Verify(Proof.CertificateStorage, ValidationDate) &&
            signedSharePart.Certificate.IsIdentic(authorityCertificate) &&
            share.Verify(Proof.ComplainingAuthorityIndex, Proof.Parameters, sharePart.VerificationValues))
        {
          this.dataTextBox.Text = GuiResources.CertificateValid;
          this.dataTextBox.BackColor = Color.Green;
        }
        else
        {
          this.dataTextBox.Text = GuiResources.CertificateInvalid;
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
      this.dataLabel.Text = Resources.ViewShareData;

      this.certificateControl.SetLanguage();

      Display();
    }
  }
}
