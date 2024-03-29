﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Client
{
  public partial class ViewVotingAuthoritiesItem : WizardItem
  {
    private bool run = false;
    private VotingMaterial votingMaterial;
    private Exception exception;

    public VotingDescriptor VotingDescriptor { get; set; }

    public ViewVotingAuthoritiesItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      VoteItem voteItem = new VoteItem();
      voteItem.VotingMaterial = this.votingMaterial;
      voteItem.VotingDescriptor = VotingDescriptor;
      return voteItem;
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
      get { return !this.run; }
    }

    public override bool CanNext
    {
      get { return !this.run; }
    }

    private void GetVotingMaterialComplete(VotingMaterial votingMaterial, Exception exception)
    {
      this.exception = exception;
      this.votingMaterial = votingMaterial;
      this.run = false;
    }

    public override void Begin()
    {
      this.run = true;
      OnUpdateWizard();

      Status.VotingClient.GetVotingMaterial(VotingDescriptor.Id, GetVotingMaterialComplete);

      while (this.run)
      {
        Status.UpdateProgress();
        Thread.Sleep(10);
      }

      if (this.exception != null)
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
      }
      else
      {
        VotingParameters parameters = this.votingMaterial.Parameters.Value;

        this.votingIdTextBox.Text = parameters.VotingId.ToString();
        this.votingTitleTextBox.Text = parameters.Title.Text;

        this.organizingCertificate.ValidationDate = parameters.VotingBeginDate;
        this.organizingCertificate.CertificateStorage = Status.CertificateStorage;
        this.organizingCertificate.Certificate = this.votingMaterial.Parameters.Certificate;

        if (this.votingMaterial.Parameters.Verify(Status.CertificateStorage, parameters.VotingBeginDate) &&
            this.votingMaterial.Parameters.Certificate is AdminCertificate)
        {
          this.organizingSignatureBox.Text = GuiResources.CertificateValid;
          this.organizingSignatureBox.BackColor = Color.Green;
        }
        else
        {
          this.organizingSignatureBox.Text = GuiResources.CertificateInvalid;
          this.organizingSignatureBox.BackColor = Color.Red;
        }

        this.response0.ValidationDate = parameters.VotingBeginDate;
        this.response0.Parameters = parameters;
        this.response0.CertificateStorage = Status.CertificateStorage;
        this.response0.SignedShareReponse = this.votingMaterial.PublicKeyParts.ElementAt(0);
        this.response0.Display();

        this.response1.ValidationDate = parameters.VotingBeginDate;
        this.response1.Parameters = parameters;
        this.response1.CertificateStorage = Status.CertificateStorage;
        this.response1.SignedShareReponse = this.votingMaterial.PublicKeyParts.ElementAt(1);
        this.response1.Display();

        this.response2.ValidationDate = parameters.VotingBeginDate;
        this.response2.Parameters = parameters;
        this.response2.CertificateStorage = Status.CertificateStorage;
        this.response2.SignedShareReponse = this.votingMaterial.PublicKeyParts.ElementAt(2);
        this.response2.Display();

        this.response3.ValidationDate = parameters.VotingBeginDate;
        this.response3.Parameters = parameters;
        this.response3.CertificateStorage = Status.CertificateStorage;
        this.response3.SignedShareReponse = this.votingMaterial.PublicKeyParts.ElementAt(3);
        this.response3.Display();

        this.response4.ValidationDate = parameters.VotingBeginDate;
        this.response4.Parameters = parameters;
        this.response4.CertificateStorage = Status.CertificateStorage;
        this.response4.SignedShareReponse = this.votingMaterial.PublicKeyParts.ElementAt(4);
        this.response4.Display();

        Status.SetMessage(Resources.ViewVotingAuthoritiesDownloaded, MessageType.Success);
      }

      OnUpdateWizard();
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.organizingLabel.Text = Resources.BadShareProofOrganizing;
      this.controlingAuthoritiesLabel.Text = Resources.BadShareProofControlling;
      this.organizingSignatureLabel.Text = Resources.BadShareProofSignature;
      this.votingLabel.Text = Resources.BadShareProofVoting;
      this.votingIdLabel.Text = Resources.BadShareProofVotingId;
      this.votingTitleLabel.Text = Resources.BadShareProofVotingTitle;

      this.organizingCertificate.SetLanguage();
      this.response0.SetLanguage();
      this.response1.SetLanguage();
      this.response2.SetLanguage();
      this.response3.SetLanguage();
      this.response4.SetLanguage();
    }
  }
}
