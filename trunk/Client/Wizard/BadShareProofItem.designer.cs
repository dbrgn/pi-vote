/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Pirate.PiVote.Client
{
  partial class BadShareProofItem
  {
    /// <summary> 
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
      if (disposing && (components != null))
      {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.share0 = new Pirate.PiVote.Client.ViewShareControl();
      this.organizingCertificate = new Pirate.PiVote.Client.SmallCertificateControl();
      this.share1 = new Pirate.PiVote.Client.ViewShareControl();
      this.share2 = new Pirate.PiVote.Client.ViewShareControl();
      this.share3 = new Pirate.PiVote.Client.ViewShareControl();
      this.share4 = new Pirate.PiVote.Client.ViewShareControl();
      this.organizingSignatureBox = new System.Windows.Forms.TextBox();
      this.organizingSignatureLabel = new System.Windows.Forms.Label();
      this.reportingSignatureBox = new System.Windows.Forms.TextBox();
      this.reportingSignatureLabel = new System.Windows.Forms.Label();
      this.reportingCertificate = new Pirate.PiVote.Client.SmallCertificateControl();
      this.reportingLabel = new System.Windows.Forms.Label();
      this.organizingLabel = new System.Windows.Forms.Label();
      this.controlingAuthoritiesLabel = new System.Windows.Forms.Label();
      this.votingLabel = new System.Windows.Forms.Label();
      this.votingIdLabel = new System.Windows.Forms.Label();
      this.votingIdTextBox = new System.Windows.Forms.TextBox();
      this.votingTitleTextBox = new System.Windows.Forms.TextBox();
      this.votingTitleLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // share0
      // 
      this.share0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.share0.AuthorityIndex = 0;
      this.share0.ComplainingAuthorityCertificate = null;
      this.share0.Location = new System.Drawing.Point(0, 325);
      this.share0.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
      this.share0.Name = "share0";
      this.share0.Proof = null;
      this.share0.Size = new System.Drawing.Size(1044, 65);
      this.share0.TabIndex = 4;
      this.share0.ValidationDate = new System.DateTime(2010, 4, 22, 23, 52, 17, 794);
      // 
      // organizingCertificate
      // 
      this.organizingCertificate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.organizingCertificate.Certificate = null;
      this.organizingCertificate.CertificateStorage = null;
      this.organizingCertificate.Location = new System.Drawing.Point(0, 230);
      this.organizingCertificate.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
      this.organizingCertificate.Name = "organizingCertificate";
      this.organizingCertificate.Size = new System.Drawing.Size(762, 68);
      this.organizingCertificate.TabIndex = 5;
      this.organizingCertificate.ValidationDate = new System.DateTime(2010, 4, 23, 0, 3, 14, 100);
      // 
      // share1
      // 
      this.share1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.share1.AuthorityIndex = 0;
      this.share1.ComplainingAuthorityCertificate = null;
      this.share1.Location = new System.Drawing.Point(0, 399);
      this.share1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
      this.share1.Name = "share1";
      this.share1.Proof = null;
      this.share1.Size = new System.Drawing.Size(1044, 65);
      this.share1.TabIndex = 6;
      this.share1.ValidationDate = new System.DateTime(2010, 4, 22, 23, 52, 17, 794);
      // 
      // share2
      // 
      this.share2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.share2.AuthorityIndex = 0;
      this.share2.ComplainingAuthorityCertificate = null;
      this.share2.Location = new System.Drawing.Point(0, 472);
      this.share2.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
      this.share2.Name = "share2";
      this.share2.Proof = null;
      this.share2.Size = new System.Drawing.Size(1044, 65);
      this.share2.TabIndex = 7;
      this.share2.ValidationDate = new System.DateTime(2010, 4, 22, 23, 52, 17, 794);
      // 
      // share3
      // 
      this.share3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.share3.AuthorityIndex = 0;
      this.share3.ComplainingAuthorityCertificate = null;
      this.share3.Location = new System.Drawing.Point(0, 545);
      this.share3.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
      this.share3.Name = "share3";
      this.share3.Proof = null;
      this.share3.Size = new System.Drawing.Size(1044, 65);
      this.share3.TabIndex = 8;
      this.share3.ValidationDate = new System.DateTime(2010, 4, 22, 23, 52, 17, 794);
      // 
      // share4
      // 
      this.share4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.share4.AuthorityIndex = 0;
      this.share4.ComplainingAuthorityCertificate = null;
      this.share4.Location = new System.Drawing.Point(0, 619);
      this.share4.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
      this.share4.Name = "share4";
      this.share4.Proof = null;
      this.share4.Size = new System.Drawing.Size(1044, 65);
      this.share4.TabIndex = 9;
      this.share4.ValidationDate = new System.DateTime(2010, 4, 22, 23, 52, 17, 794);
      // 
      // organizingSignatureBox
      // 
      this.organizingSignatureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.organizingSignatureBox.Location = new System.Drawing.Point(907, 232);
      this.organizingSignatureBox.Margin = new System.Windows.Forms.Padding(4);
      this.organizingSignatureBox.Name = "organizingSignatureBox";
      this.organizingSignatureBox.ReadOnly = true;
      this.organizingSignatureBox.Size = new System.Drawing.Size(139, 26);
      this.organizingSignatureBox.TabIndex = 11;
      // 
      // organizingSignatureLabel
      // 
      this.organizingSignatureLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.organizingSignatureLabel.AutoSize = true;
      this.organizingSignatureLabel.Location = new System.Drawing.Point(772, 235);
      this.organizingSignatureLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.organizingSignatureLabel.Name = "organizingSignatureLabel";
      this.organizingSignatureLabel.Size = new System.Drawing.Size(84, 19);
      this.organizingSignatureLabel.TabIndex = 10;
      this.organizingSignatureLabel.Text = "Signature:";
      // 
      // reportingSignatureBox
      // 
      this.reportingSignatureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.reportingSignatureBox.Location = new System.Drawing.Point(907, 131);
      this.reportingSignatureBox.Margin = new System.Windows.Forms.Padding(4);
      this.reportingSignatureBox.Name = "reportingSignatureBox";
      this.reportingSignatureBox.ReadOnly = true;
      this.reportingSignatureBox.Size = new System.Drawing.Size(139, 26);
      this.reportingSignatureBox.TabIndex = 14;
      // 
      // reportingSignatureLabel
      // 
      this.reportingSignatureLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.reportingSignatureLabel.AutoSize = true;
      this.reportingSignatureLabel.Location = new System.Drawing.Point(772, 134);
      this.reportingSignatureLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.reportingSignatureLabel.Name = "reportingSignatureLabel";
      this.reportingSignatureLabel.Size = new System.Drawing.Size(84, 19);
      this.reportingSignatureLabel.TabIndex = 13;
      this.reportingSignatureLabel.Text = "Signature:";
      // 
      // reportingCertificate
      // 
      this.reportingCertificate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.reportingCertificate.Certificate = null;
      this.reportingCertificate.CertificateStorage = null;
      this.reportingCertificate.Location = new System.Drawing.Point(0, 130);
      this.reportingCertificate.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
      this.reportingCertificate.Name = "reportingCertificate";
      this.reportingCertificate.Size = new System.Drawing.Size(762, 73);
      this.reportingCertificate.TabIndex = 12;
      this.reportingCertificate.ValidationDate = new System.DateTime(2010, 4, 23, 0, 3, 14, 100);
      // 
      // reportingLabel
      // 
      this.reportingLabel.AutoSize = true;
      this.reportingLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.reportingLabel.Location = new System.Drawing.Point(-4, 107);
      this.reportingLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.reportingLabel.Name = "reportingLabel";
      this.reportingLabel.Size = new System.Drawing.Size(164, 19);
      this.reportingLabel.TabIndex = 15;
      this.reportingLabel.Text = "Reporting Authority";
      // 
      // organizingLabel
      // 
      this.organizingLabel.AutoSize = true;
      this.organizingLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.organizingLabel.Location = new System.Drawing.Point(-4, 207);
      this.organizingLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.organizingLabel.Name = "organizingLabel";
      this.organizingLabel.Size = new System.Drawing.Size(206, 19);
      this.organizingLabel.TabIndex = 16;
      this.organizingLabel.Text = "Organizing Administrator";
      // 
      // controlingAuthoritiesLabel
      // 
      this.controlingAuthoritiesLabel.AutoSize = true;
      this.controlingAuthoritiesLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.controlingAuthoritiesLabel.Location = new System.Drawing.Point(-4, 302);
      this.controlingAuthoritiesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.controlingAuthoritiesLabel.Name = "controlingAuthoritiesLabel";
      this.controlingAuthoritiesLabel.Size = new System.Drawing.Size(182, 19);
      this.controlingAuthoritiesLabel.TabIndex = 17;
      this.controlingAuthoritiesLabel.Text = "Controling Authorities";
      // 
      // votingLabel
      // 
      this.votingLabel.AutoSize = true;
      this.votingLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.votingLabel.Location = new System.Drawing.Point(-4, 0);
      this.votingLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.votingLabel.Name = "votingLabel";
      this.votingLabel.Size = new System.Drawing.Size(146, 19);
      this.votingLabel.TabIndex = 18;
      this.votingLabel.Text = "Voting Procedure";
      // 
      // votingIdLabel
      // 
      this.votingIdLabel.AutoSize = true;
      this.votingIdLabel.Location = new System.Drawing.Point(-4, 33);
      this.votingIdLabel.Name = "votingIdLabel";
      this.votingIdLabel.Size = new System.Drawing.Size(28, 19);
      this.votingIdLabel.TabIndex = 19;
      this.votingIdLabel.Text = "Id:";
      // 
      // votingIdTextBox
      // 
      this.votingIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.votingIdTextBox.Location = new System.Drawing.Point(108, 30);
      this.votingIdTextBox.Name = "votingIdTextBox";
      this.votingIdTextBox.ReadOnly = true;
      this.votingIdTextBox.Size = new System.Drawing.Size(654, 26);
      this.votingIdTextBox.TabIndex = 20;
      // 
      // votingTitleTextBox
      // 
      this.votingTitleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.votingTitleTextBox.Location = new System.Drawing.Point(108, 62);
      this.votingTitleTextBox.Name = "votingTitleTextBox";
      this.votingTitleTextBox.ReadOnly = true;
      this.votingTitleTextBox.Size = new System.Drawing.Size(654, 26);
      this.votingTitleTextBox.TabIndex = 21;
      // 
      // votingTitleLabel
      // 
      this.votingTitleLabel.AutoSize = true;
      this.votingTitleLabel.Location = new System.Drawing.Point(-4, 65);
      this.votingTitleLabel.Name = "votingTitleLabel";
      this.votingTitleLabel.Size = new System.Drawing.Size(42, 19);
      this.votingTitleLabel.TabIndex = 22;
      this.votingTitleLabel.Text = "Title:";
      // 
      // BadShareProofItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.votingTitleLabel);
      this.Controls.Add(this.votingTitleTextBox);
      this.Controls.Add(this.votingIdTextBox);
      this.Controls.Add(this.votingIdLabel);
      this.Controls.Add(this.votingLabel);
      this.Controls.Add(this.controlingAuthoritiesLabel);
      this.Controls.Add(this.organizingLabel);
      this.Controls.Add(this.reportingLabel);
      this.Controls.Add(this.reportingSignatureBox);
      this.Controls.Add(this.reportingSignatureLabel);
      this.Controls.Add(this.reportingCertificate);
      this.Controls.Add(this.organizingSignatureBox);
      this.Controls.Add(this.organizingSignatureLabel);
      this.Controls.Add(this.share4);
      this.Controls.Add(this.share3);
      this.Controls.Add(this.share2);
      this.Controls.Add(this.share1);
      this.Controls.Add(this.organizingCertificate);
      this.Controls.Add(this.share0);
      this.Margin = new System.Windows.Forms.Padding(4);
      this.Name = "BadShareProofItem";
      this.Size = new System.Drawing.Size(1050, 730);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private ViewShareControl share0;
    private SmallCertificateControl organizingCertificate;
    private ViewShareControl share1;
    private ViewShareControl share2;
    private ViewShareControl share3;
    private ViewShareControl share4;
    private TextBox organizingSignatureBox;
    private Label organizingSignatureLabel;
    private TextBox reportingSignatureBox;
    private Label reportingSignatureLabel;
    private SmallCertificateControl reportingCertificate;
    private Label reportingLabel;
    private Label organizingLabel;
    private Label controlingAuthoritiesLabel;
    private Label votingLabel;
    private Label votingIdLabel;
    private TextBox votingIdTextBox;
    private TextBox votingTitleTextBox;
    private Label votingTitleLabel;






  }
}
