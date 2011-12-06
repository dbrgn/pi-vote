/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
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

namespace Pirate.PiVote.Gui
{
  public partial class CertificateControl : UserControl
  {
    private Certificate certificate;

    public DateTime ValidationDate { get; set; }

    public CertificateStorage CertificateStorage { get; set; }

    public Certificate Certificate
    {
      get { return this.certificate; }
      set
      {
        this.certificate = value;

        Display();
      }
    }

    private void Display()
    {
      if (this.certificate != null && CertificateStorage != null)
      {
        this.typeTextBox.Text = this.certificate.TypeText;
        this.idTextBox.Text = this.certificate.Id.ToString();
        this.nameTextBox.Text = this.certificate.FullName;
        this.creationDateTextBox.Text = this.certificate.CreationDate.ToString();

        this.signatureList.Items.Clear();

        foreach (Signature signature in this.certificate.Signatures)
        {
          ListViewItem item = null;

          if (CertificateStorage.Has(signature.SignerId))
          {
            Certificate signer = CertificateStorage.Get(signature.SignerId);
            item = new ListViewItem(signer.Id.ToString());
            item.SubItems.Add(signer.FullName);
          }
          else
          {
            item = new ListViewItem(string.Empty);
            item.SubItems.Add(GuiResources.CertificateSignatureUnknown);
          }

          item.SubItems.Add(signature.ValidFrom.ToString());
          item.SubItems.Add(signature.ValidUntil.ToString());

          if (signature.Verify(this.certificate.GetSignatureContent(), CertificateStorage, ValidationDate) == CertificateValidationResult.Valid)
          {
            item.SubItems.Add(GuiResources.CertificateSignatureValid);
          }
          else
          {
            item.SubItems.Add(GuiResources.CertificateSignatureInvalid);
          }

          item.Tag = signature;

          this.signatureList.Items.Add(item);
        }
      }
    }

    public void SetLanguage()
    {
      this.typeLabel.Text = GuiResources.CertificateType;
      this.idLabel.Text = GuiResources.CertificateId;
      this.nameLabel.Text = GuiResources.CertificateName;
      this.creationDateLabel.Text = GuiResources.CertificateCreationDate;
      this.signaturesLabel.Text = GuiResources.CertificateSignatures;
      this.idColumnHeader.Text = GuiResources.CertificateSignatureId;
      this.nameColumnHeader.Text = GuiResources.CertificateSignatureName;
      this.validFromColumnHeader.Text = GuiResources.CertificateSignatureValidFrom;
      this.validUntilColumnHeader.Text = GuiResources.CertificateSignatureValidUntil;
      this.statusColumnHeader.Text = GuiResources.CertificateSignatureStatus;

      Certificate = Certificate;
    }

    public CertificateControl()
    {
      InitializeComponent();
      ValidationDate = DateTime.Now;
    }

    private void signatureList_DoubleClick(object sender, EventArgs e)
    {
      if (this.signatureList.SelectedItems.Count > 0)
      {
        ListViewItem item = this.signatureList.SelectedItems[0];
        Signature signature = (Signature)item.Tag;

        if (CertificateStorage.Has(signature.SignerId))
        {
          Certificate signerCertificate = CertificateStorage.Get(signature.SignerId);
          CertificateForm.ShowCertificate(signerCertificate, CertificateStorage, ValidationDate);
        }
      }
    }
  }
}
