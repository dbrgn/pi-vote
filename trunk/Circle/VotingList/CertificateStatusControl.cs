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

namespace Pirate.PiVote.Circle.VotingList
{
  public partial class CertificateStatusControl : UserControl
  {
    private bool resume;

    public event EventHandler CreateCertificate;

    public event EventHandler ResumeCertificate;

    public CertificateStatusControl()
    {
      InitializeComponent();
    }

    public CircleController Controller { get; set; }

    public void UpdateDisplay()
    {
      if (Controller != null)
      {
        var certificates = Controller.GetVoterCertificates(0);

        if (certificates.Count() < 1)
        {
          this.statusLabel.Text = Resources.CertificateStatusNoCertificate;
          SetCreateAction();
        }
        else
        {
          var validCertificate = certificates.Where(certificate => certificate.Validate(Controller.Status.CertificateStorage) == Crypto.CertificateValidationResult.Valid).FirstOrDefault();

          if (validCertificate == null)
          {
            var lastCertificate = certificates.OrderByDescending(certificate => certificate.CreationDate).FirstOrDefault();
            var result = lastCertificate.Validate(Controller.Status.CertificateStorage);

            switch (result)
            {
              case CertificateValidationResult.Outdated:
                this.statusLabel.Text = Resources.CertificateStatusOutdated;
                SetCreateAction();
                break;
              case CertificateValidationResult.Revoked:
                this.statusLabel.Text = Resources.CertificateStatusRevoked;
                SetCreateAction();
                break;
              case CertificateValidationResult.NotYetValid:
                this.statusLabel.Text = string.Format(Resources.CertificateStatusNotYetValid, lastCertificate.ExpectedValidFrom(Controller.Status.CertificateStorage).ToShortDateString());
                SetNoAction();
                break;
              case CertificateValidationResult.CrlMissing:
                this.statusLabel.Text = Resources.CertificateStatusCrlMissing;
                SetNoAction();
                break;
              case CertificateValidationResult.NoSignature:
                this.statusLabel.Text = Resources.CertificateStatusNoSignature;
                SetResumeAction();
                break;
              default:
                this.statusLabel.Text = string.Format(Resources.CertificateStatusCorrupt, result.Text());
                SetCreateAction();
                break;
            }
          }
          else
          {
            this.statusLabel.Text = string.Format(Resources.CertificateStatusValid, validCertificate.ExpectedValidUntil(Controller.Status.CertificateStorage, DateTime.Now).ToShortDateString());
            SetNoAction();
          }
        }
      }
    }

    private void SetResumeAction()
    {
      this.actionButton.Text = Resources.CertificateStatusResumeAction;
      this.actionButton.Visible = true;
      this.statusLabel.Width = this.actionButton.Left - 16;
      this.resume = true;
      ForeColor = Color.OrangeRed;
    }

    private void SetCreateAction()
    {
      this.actionButton.Text = Resources.CertificateStatusCreateAction;
      this.actionButton.Visible = true;
      this.statusLabel.Width = this.actionButton.Left - 16;
      this.resume = false;
      ForeColor = Color.OrangeRed;
    }

    private void SetNoAction()
    {
      this.statusLabel.Width = Width - 6;
      this.actionButton.Visible = false;
      ForeColor = Color.DarkGreen;
    }

    private void OnCreateCertificate()
    {
      if (CreateCertificate != null)
      {
        CreateCertificate(this, new EventArgs());
      }
    }

    private void OnResumeCertificate()
    {
      if (ResumeCertificate != null)
      {
        ResumeCertificate(this, new EventArgs());
      }
    }

    private void actionButton_Click(object sender, EventArgs e)
    {
      if (this.resume)
      {
        OnResumeCertificate();
      }
      else
      {
        OnCreateCertificate();
      }
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);
      e.Graphics.DrawRectangle(new Pen(ForeColor, 2), 2, 2, Width - 4, Height - 4);
    }
  }
}
