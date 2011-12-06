/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;

namespace Pirate.PiVote.Client
{
  public partial class BadShareProofItem : WizardItem
  {
    public bool IsAuthority { get; set; }
    public Signed<BadShareProof> SignedBadShareProof { get; set; }

    public BadShareProofItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      if (IsAuthority)
      {
        return new AuthorityListVotingsItem();
      }
      else
      {
        return null;
      }
    }

    public override WizardItem Previous()
    {
      return null;
    }

    public override WizardItem Cancel()
    {
      return null;
    }

    public override bool CanCancel
    {
      get { return true; }
    }

    public override bool CanNext
    {
      get { return IsAuthority; }
    }

    public override void Begin()
    {
      Display();
    }

    private void Display()
    {
      BadShareProof proof = SignedBadShareProof.Value;

      this.votingIdTextBox.Text = proof.Parameters.VotingId.ToString();
      this.votingTitleTextBox.Text = proof.Parameters.Title.Text;

      this.reportingCertificate.ValidationDate = proof.Parameters.VotingBeginDate;
      this.reportingCertificate.CertificateStorage = proof.CertificateStorage;
      this.reportingCertificate.Certificate = SignedBadShareProof.Certificate;

      if (SignedBadShareProof.Verify(proof.CertificateStorage, proof.Parameters.VotingBeginDate))
      {
        this.reportingSignatureBox.Text = GuiResources.CertificateValid;
        this.reportingSignatureBox.BackColor = Color.Green;
      }
      else
      {
        this.reportingSignatureBox.Text = GuiResources.CertificateInvalid;
        this.reportingSignatureBox.BackColor = Color.Red;
      }

      this.organizingCertificate.ValidationDate = proof.Parameters.VotingBeginDate;
      this.organizingCertificate.CertificateStorage = proof.CertificateStorage;
      this.organizingCertificate.Certificate = proof.SignedParameters.Certificate;

      if (proof.SignedParameters.Verify(proof.CertificateStorage, proof.Parameters.VotingBeginDate))
      {
        this.organizingSignatureBox.Text = GuiResources.CertificateValid;
        this.organizingSignatureBox.BackColor = Color.Green;
      }
      else
      {
        this.organizingSignatureBox.Text = GuiResources.CertificateInvalid;
        this.organizingSignatureBox.BackColor = Color.Red;
      }
        
      this.share0.ValidationDate = proof.Parameters.VotingBeginDate;
      this.share0.AuthorityIndex = 1;
      this.share0.Proof = SignedBadShareProof.Value;
      this.share0.ComplainingAuthorityCertificate = SignedBadShareProof.Certificate;
      this.share0.Display();

      this.share1.ValidationDate = proof.Parameters.VotingBeginDate;
      this.share1.AuthorityIndex = 2;
      this.share1.Proof = SignedBadShareProof.Value;
      this.share1.ComplainingAuthorityCertificate = SignedBadShareProof.Certificate;
      this.share1.Display();

      this.share2.ValidationDate = proof.Parameters.VotingBeginDate;
      this.share2.AuthorityIndex = 3;
      this.share2.Proof = SignedBadShareProof.Value;
      this.share2.ComplainingAuthorityCertificate = SignedBadShareProof.Certificate;
      this.share2.Display();

      this.share3.ValidationDate = proof.Parameters.VotingBeginDate;
      this.share3.AuthorityIndex = 4;
      this.share3.Proof = SignedBadShareProof.Value;
      this.share3.ComplainingAuthorityCertificate = SignedBadShareProof.Certificate;
      this.share3.Display();

      this.share4.ValidationDate = proof.Parameters.VotingBeginDate;
      this.share4.AuthorityIndex = 5;
      this.share4.Proof = SignedBadShareProof.Value;
      this.share4.ComplainingAuthorityCertificate = SignedBadShareProof.Certificate;
      this.share4.Display();
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.reportingLabel.Text = Resources.BadShareProofReporting;
      this.organizingLabel.Text = Resources.BadShareProofOrganizing;
      this.controlingAuthoritiesLabel.Text = Resources.BadShareProofControlling;
      this.reportingSignatureLabel.Text = Resources.BadShareProofSignature;
      this.organizingSignatureLabel.Text = Resources.BadShareProofSignature;
      this.votingLabel.Text = Resources.BadShareProofVoting;
      this.votingIdLabel.Text = Resources.BadShareProofVotingId;
      this.votingTitleLabel.Text = Resources.BadShareProofVotingTitle;

      this.reportingCertificate.SetLanguage();
      this.organizingCertificate.SetLanguage();
      this.share0.SetLanguage();
      this.share1.SetLanguage();
      this.share2.SetLanguage();
      this.share3.SetLanguage();
      this.share4.SetLanguage();
    }
  }
}
