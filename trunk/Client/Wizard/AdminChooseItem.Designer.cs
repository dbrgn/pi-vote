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
  partial class AdminChooseItem
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
      this.downloadSignatureRequestsButton = new System.Windows.Forms.Button();
      this.uploadSignatureResponsesButton = new System.Windows.Forms.Button();
      this.createVotingButton = new System.Windows.Forms.Button();
      this.uploadCertificateStorageButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // downloadSignatureRequestsButton
      // 
      this.downloadSignatureRequestsButton.Location = new System.Drawing.Point(2, 2);
      this.downloadSignatureRequestsButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.downloadSignatureRequestsButton.Name = "downloadSignatureRequestsButton";
      this.downloadSignatureRequestsButton.Size = new System.Drawing.Size(411, 25);
      this.downloadSignatureRequestsButton.TabIndex = 5;
      this.downloadSignatureRequestsButton.Text = "Download signature requests from server...";
      this.downloadSignatureRequestsButton.UseVisualStyleBackColor = true;
      this.downloadSignatureRequestsButton.Click += new System.EventHandler(this.saveToButton_Click);
      // 
      // uploadSignatureResponsesButton
      // 
      this.uploadSignatureResponsesButton.Location = new System.Drawing.Point(2, 31);
      this.uploadSignatureResponsesButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.uploadSignatureResponsesButton.Name = "uploadSignatureResponsesButton";
      this.uploadSignatureResponsesButton.Size = new System.Drawing.Size(411, 25);
      this.uploadSignatureResponsesButton.TabIndex = 6;
      this.uploadSignatureResponsesButton.Text = "Upload signature responses to server...";
      this.uploadSignatureResponsesButton.UseVisualStyleBackColor = true;
      this.uploadSignatureResponsesButton.Click += new System.EventHandler(this.openButton_Click);
      // 
      // createVotingButton
      // 
      this.createVotingButton.Location = new System.Drawing.Point(2, 89);
      this.createVotingButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.createVotingButton.Name = "createVotingButton";
      this.createVotingButton.Size = new System.Drawing.Size(411, 25);
      this.createVotingButton.TabIndex = 7;
      this.createVotingButton.Text = "Create a voting procedure";
      this.createVotingButton.UseVisualStyleBackColor = true;
      this.createVotingButton.Click += new System.EventHandler(this.createVotingButton_Click);
      // 
      // uploadCertificateStorageButton
      // 
      this.uploadCertificateStorageButton.Location = new System.Drawing.Point(2, 60);
      this.uploadCertificateStorageButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.uploadCertificateStorageButton.Name = "uploadCertificateStorageButton";
      this.uploadCertificateStorageButton.Size = new System.Drawing.Size(411, 25);
      this.uploadCertificateStorageButton.TabIndex = 8;
      this.uploadCertificateStorageButton.Text = "Upload certificate storage to server...";
      this.uploadCertificateStorageButton.UseVisualStyleBackColor = true;
      this.uploadCertificateStorageButton.Click += new System.EventHandler(this.uploadCertificateStorageButton_Click);
      // 
      // AdminChooseItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.uploadCertificateStorageButton);
      this.Controls.Add(this.createVotingButton);
      this.Controls.Add(this.uploadSignatureResponsesButton);
      this.Controls.Add(this.downloadSignatureRequestsButton);
      this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
      this.Name = "AdminChooseItem";
      this.Size = new System.Drawing.Size(467, 359);
      this.ResumeLayout(false);

    }

    #endregion

    private Button downloadSignatureRequestsButton;
    private Button uploadSignatureResponsesButton;
    private Button createVotingButton;
    private Button uploadCertificateStorageButton;



  }
}
