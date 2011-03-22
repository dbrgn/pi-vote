/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Pirate.PiVote.Client
{
  partial class ViewVotingAuthoritiesItem
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
      this.response0 = new Pirate.PiVote.Client.ViewShareResponseControl();
      this.organizingCertificate = new Pirate.PiVote.Client.SmallCertificateControl();
      this.response1 = new Pirate.PiVote.Client.ViewShareResponseControl();
      this.response2 = new Pirate.PiVote.Client.ViewShareResponseControl();
      this.response3 = new Pirate.PiVote.Client.ViewShareResponseControl();
      this.response4 = new Pirate.PiVote.Client.ViewShareResponseControl();
      this.organizingSignatureBox = new System.Windows.Forms.TextBox();
      this.organizingSignatureLabel = new System.Windows.Forms.Label();
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
      this.response0.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.response0.CertificateStorage = null;
      this.response0.Location = new System.Drawing.Point(6, 133);
      this.response0.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.response0.Name = "share0";
      this.response0.Parameters = null;
      this.response0.SignedShareReponse = null;
      this.response0.Size = new System.Drawing.Size(644, 20);
      this.response0.TabIndex = 4;
      this.response0.ValidationDate = new System.DateTime(2010, 4, 22, 23, 52, 17, 794);
      // 
      // organizingCertificate
      // 
      this.organizingCertificate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.organizingCertificate.Certificate = null;
      this.organizingCertificate.CertificateStorage = null;
      this.organizingCertificate.Location = new System.Drawing.Point(6, 86);
      this.organizingCertificate.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.organizingCertificate.Name = "organizingCertificate";
      this.organizingCertificate.Size = new System.Drawing.Size(456, 20);
      this.organizingCertificate.TabIndex = 5;
      this.organizingCertificate.ValidationDate = new System.DateTime(2010, 4, 23, 0, 3, 14, 100);
      // 
      // share1
      // 
      this.response1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.response1.CertificateStorage = null;
      this.response1.Location = new System.Drawing.Point(6, 163);
      this.response1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.response1.Name = "share1";
      this.response1.Parameters = null;
      this.response1.SignedShareReponse = null;
      this.response1.Size = new System.Drawing.Size(644, 20);
      this.response1.TabIndex = 6;
      this.response1.ValidationDate = new System.DateTime(2010, 4, 22, 23, 52, 17, 794);
      // 
      // share2
      // 
      this.response2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.response2.CertificateStorage = null;
      this.response2.Location = new System.Drawing.Point(6, 193);
      this.response2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.response2.Name = "share2";
      this.response2.Parameters = null;
      this.response2.SignedShareReponse = null;
      this.response2.Size = new System.Drawing.Size(644, 20);
      this.response2.TabIndex = 7;
      this.response2.ValidationDate = new System.DateTime(2010, 4, 22, 23, 52, 17, 794);
      // 
      // share3
      // 
      this.response3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.response3.CertificateStorage = null;
      this.response3.Location = new System.Drawing.Point(6, 223);
      this.response3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.response3.Name = "share3";
      this.response3.Parameters = null;
      this.response3.SignedShareReponse = null;
      this.response3.Size = new System.Drawing.Size(644, 20);
      this.response3.TabIndex = 8;
      this.response3.ValidationDate = new System.DateTime(2010, 4, 22, 23, 52, 17, 794);
      // 
      // share4
      // 
      this.response4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.response4.CertificateStorage = null;
      this.response4.Location = new System.Drawing.Point(6, 253);
      this.response4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.response4.Name = "share4";
      this.response4.Parameters = null;
      this.response4.SignedShareReponse = null;
      this.response4.Size = new System.Drawing.Size(644, 20);
      this.response4.TabIndex = 9;
      this.response4.ValidationDate = new System.DateTime(2010, 4, 22, 23, 52, 17, 794);
      // 
      // organizingSignatureBox
      // 
      this.organizingSignatureBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.organizingSignatureBox.Location = new System.Drawing.Point(556, 86);
      this.organizingSignatureBox.Name = "organizingSignatureBox";
      this.organizingSignatureBox.ReadOnly = true;
      this.organizingSignatureBox.Size = new System.Drawing.Size(94, 20);
      this.organizingSignatureBox.TabIndex = 11;
      // 
      // organizingSignatureLabel
      // 
      this.organizingSignatureLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.organizingSignatureLabel.AutoSize = true;
      this.organizingSignatureLabel.Location = new System.Drawing.Point(469, 89);
      this.organizingSignatureLabel.Name = "organizingSignatureLabel";
      this.organizingSignatureLabel.Size = new System.Drawing.Size(56, 14);
      this.organizingSignatureLabel.TabIndex = 10;
      this.organizingSignatureLabel.Text = "Signature:";
      // 
      // organizingLabel
      // 
      this.organizingLabel.AutoSize = true;
      this.organizingLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.organizingLabel.Location = new System.Drawing.Point(3, 67);
      this.organizingLabel.Name = "organizingLabel";
      this.organizingLabel.Size = new System.Drawing.Size(146, 14);
      this.organizingLabel.TabIndex = 16;
      this.organizingLabel.Text = "Organizing Administrator";
      // 
      // controlingAuthoritiesLabel
      // 
      this.controlingAuthoritiesLabel.AutoSize = true;
      this.controlingAuthoritiesLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.controlingAuthoritiesLabel.Location = new System.Drawing.Point(3, 114);
      this.controlingAuthoritiesLabel.Name = "controlingAuthoritiesLabel";
      this.controlingAuthoritiesLabel.Size = new System.Drawing.Size(130, 14);
      this.controlingAuthoritiesLabel.TabIndex = 17;
      this.controlingAuthoritiesLabel.Text = "Controling Authorities";
      // 
      // votingLabel
      // 
      this.votingLabel.AutoSize = true;
      this.votingLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.votingLabel.Location = new System.Drawing.Point(2, 0);
      this.votingLabel.Name = "votingLabel";
      this.votingLabel.Size = new System.Drawing.Size(103, 14);
      this.votingLabel.TabIndex = 18;
      this.votingLabel.Text = "Voting Procedure";
      // 
      // votingIdLabel
      // 
      this.votingIdLabel.AutoSize = true;
      this.votingIdLabel.Location = new System.Drawing.Point(2, 23);
      this.votingIdLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.votingIdLabel.Name = "votingIdLabel";
      this.votingIdLabel.Size = new System.Drawing.Size(18, 14);
      this.votingIdLabel.TabIndex = 19;
      this.votingIdLabel.Text = "Id:";
      // 
      // votingIdTextBox
      // 
      this.votingIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.votingIdTextBox.Location = new System.Drawing.Point(72, 20);
      this.votingIdTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.votingIdTextBox.Name = "votingIdTextBox";
      this.votingIdTextBox.ReadOnly = true;
      this.votingIdTextBox.Size = new System.Drawing.Size(391, 20);
      this.votingIdTextBox.TabIndex = 20;
      // 
      // votingTitleTextBox
      // 
      this.votingTitleTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.votingTitleTextBox.Location = new System.Drawing.Point(72, 41);
      this.votingTitleTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.votingTitleTextBox.Name = "votingTitleTextBox";
      this.votingTitleTextBox.ReadOnly = true;
      this.votingTitleTextBox.Size = new System.Drawing.Size(391, 20);
      this.votingTitleTextBox.TabIndex = 21;
      // 
      // votingTitleLabel
      // 
      this.votingTitleLabel.AutoSize = true;
      this.votingTitleLabel.Location = new System.Drawing.Point(2, 44);
      this.votingTitleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.votingTitleLabel.Name = "votingTitleLabel";
      this.votingTitleLabel.Size = new System.Drawing.Size(29, 14);
      this.votingTitleLabel.TabIndex = 22;
      this.votingTitleLabel.Text = "Title:";
      // 
      // ViewVotingAuthorities
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.votingTitleLabel);
      this.Controls.Add(this.votingTitleTextBox);
      this.Controls.Add(this.votingIdTextBox);
      this.Controls.Add(this.votingIdLabel);
      this.Controls.Add(this.votingLabel);
      this.Controls.Add(this.controlingAuthoritiesLabel);
      this.Controls.Add(this.organizingLabel);
      this.Controls.Add(this.organizingSignatureBox);
      this.Controls.Add(this.organizingSignatureLabel);
      this.Controls.Add(this.response4);
      this.Controls.Add(this.response3);
      this.Controls.Add(this.response2);
      this.Controls.Add(this.response1);
      this.Controls.Add(this.organizingCertificate);
      this.Controls.Add(this.response0);
      this.Margin = new System.Windows.Forms.Padding(3);
      this.Name = "ViewVotingAuthorities";
      this.Size = new System.Drawing.Size(654, 281);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private ViewShareResponseControl response0;
    private SmallCertificateControl organizingCertificate;
    private ViewShareResponseControl response1;
    private ViewShareResponseControl response2;
    private ViewShareResponseControl response3;
    private ViewShareResponseControl response4;
    private TextBox organizingSignatureBox;
    private Label organizingSignatureLabel;
    private Label organizingLabel;
    private Label controlingAuthoritiesLabel;
    private Label votingLabel;
    private Label votingIdLabel;
    private TextBox votingIdTextBox;
    private TextBox votingTitleTextBox;
    private Label votingTitleLabel;






  }
}
