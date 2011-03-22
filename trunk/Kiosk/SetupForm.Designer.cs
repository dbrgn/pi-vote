/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Kiosk
{
  partial class SetupForm
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.certificateStorageTextBox = new System.Windows.Forms.TextBox();
      this.certificateStorageBrowseButton = new System.Windows.Forms.Button();
      this.certificateStorageLabel = new System.Windows.Forms.Label();
      this.serverCertificateTextBox = new System.Windows.Forms.TextBox();
      this.serverCertificateBrowseButton = new System.Windows.Forms.Button();
      this.serverCertificateLabel = new System.Windows.Forms.Label();
      this.startButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.memberDatabaseTextBox = new System.Windows.Forms.TextBox();
      this.memberDatabaseBrowseButton = new System.Windows.Forms.Button();
      this.memberDatabaseLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // certificateStorageTextBox
      // 
      this.certificateStorageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.certificateStorageTextBox.Location = new System.Drawing.Point(116, 14);
      this.certificateStorageTextBox.Name = "certificateStorageTextBox";
      this.certificateStorageTextBox.ReadOnly = true;
      this.certificateStorageTextBox.Size = new System.Drawing.Size(356, 20);
      this.certificateStorageTextBox.TabIndex = 0;
      // 
      // certificateStorageBrowseButton
      // 
      this.certificateStorageBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.certificateStorageBrowseButton.Location = new System.Drawing.Point(478, 14);
      this.certificateStorageBrowseButton.Name = "certificateStorageBrowseButton";
      this.certificateStorageBrowseButton.Size = new System.Drawing.Size(83, 20);
      this.certificateStorageBrowseButton.TabIndex = 1;
      this.certificateStorageBrowseButton.Text = "&Browse...";
      this.certificateStorageBrowseButton.UseVisualStyleBackColor = true;
      this.certificateStorageBrowseButton.Click += new System.EventHandler(this.certificateStorageBrowseButton_Click);
      // 
      // certificateStorageLabel
      // 
      this.certificateStorageLabel.AutoSize = true;
      this.certificateStorageLabel.Location = new System.Drawing.Point(12, 17);
      this.certificateStorageLabel.Name = "certificateStorageLabel";
      this.certificateStorageLabel.Size = new System.Drawing.Size(95, 13);
      this.certificateStorageLabel.TabIndex = 2;
      this.certificateStorageLabel.Text = "Certificate storage:";
      // 
      // serverCertificateTextBox
      // 
      this.serverCertificateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.serverCertificateTextBox.Location = new System.Drawing.Point(116, 40);
      this.serverCertificateTextBox.Name = "serverCertificateTextBox";
      this.serverCertificateTextBox.ReadOnly = true;
      this.serverCertificateTextBox.Size = new System.Drawing.Size(356, 20);
      this.serverCertificateTextBox.TabIndex = 3;
      // 
      // serverCertificateBrowseButton
      // 
      this.serverCertificateBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.serverCertificateBrowseButton.Location = new System.Drawing.Point(478, 40);
      this.serverCertificateBrowseButton.Name = "serverCertificateBrowseButton";
      this.serverCertificateBrowseButton.Size = new System.Drawing.Size(83, 20);
      this.serverCertificateBrowseButton.TabIndex = 4;
      this.serverCertificateBrowseButton.Text = "&Browse...";
      this.serverCertificateBrowseButton.UseVisualStyleBackColor = true;
      this.serverCertificateBrowseButton.Click += new System.EventHandler(this.serverCertificateBrowseButton_Click);
      // 
      // serverCertificateLabel
      // 
      this.serverCertificateLabel.AutoSize = true;
      this.serverCertificateLabel.Location = new System.Drawing.Point(12, 44);
      this.serverCertificateLabel.Name = "serverCertificateLabel";
      this.serverCertificateLabel.Size = new System.Drawing.Size(90, 13);
      this.serverCertificateLabel.TabIndex = 5;
      this.serverCertificateLabel.Text = "Server certificate:";
      // 
      // startButton
      // 
      this.startButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.startButton.Enabled = false;
      this.startButton.Location = new System.Drawing.Point(478, 113);
      this.startButton.Name = "startButton";
      this.startButton.Size = new System.Drawing.Size(83, 23);
      this.startButton.TabIndex = 6;
      this.startButton.Text = "&Start";
      this.startButton.UseVisualStyleBackColor = true;
      this.startButton.Click += new System.EventHandler(this.startButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(389, 113);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(83, 23);
      this.cancelButton.TabIndex = 7;
      this.cancelButton.Text = "&Camcel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // memberDatabaseTextBox
      // 
      this.memberDatabaseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.memberDatabaseTextBox.Location = new System.Drawing.Point(116, 66);
      this.memberDatabaseTextBox.Name = "memberDatabaseTextBox";
      this.memberDatabaseTextBox.ReadOnly = true;
      this.memberDatabaseTextBox.Size = new System.Drawing.Size(356, 20);
      this.memberDatabaseTextBox.TabIndex = 8;
      // 
      // memberDatabaseBrowseButton
      // 
      this.memberDatabaseBrowseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.memberDatabaseBrowseButton.Location = new System.Drawing.Point(478, 66);
      this.memberDatabaseBrowseButton.Name = "memberDatabaseBrowseButton";
      this.memberDatabaseBrowseButton.Size = new System.Drawing.Size(83, 20);
      this.memberDatabaseBrowseButton.TabIndex = 9;
      this.memberDatabaseBrowseButton.Text = "&Browse...";
      this.memberDatabaseBrowseButton.UseVisualStyleBackColor = true;
      this.memberDatabaseBrowseButton.Click += new System.EventHandler(this.memberDatabaseBrowseButton_Click);
      // 
      // memberDatabaseLabel
      // 
      this.memberDatabaseLabel.AutoSize = true;
      this.memberDatabaseLabel.Location = new System.Drawing.Point(12, 69);
      this.memberDatabaseLabel.Name = "memberDatabaseLabel";
      this.memberDatabaseLabel.Size = new System.Drawing.Size(95, 13);
      this.memberDatabaseLabel.TabIndex = 10;
      this.memberDatabaseLabel.Text = "Member database:";
      // 
      // SetupForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(573, 148);
      this.Controls.Add(this.memberDatabaseLabel);
      this.Controls.Add(this.memberDatabaseBrowseButton);
      this.Controls.Add(this.memberDatabaseTextBox);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.startButton);
      this.Controls.Add(this.serverCertificateLabel);
      this.Controls.Add(this.serverCertificateBrowseButton);
      this.Controls.Add(this.serverCertificateTextBox);
      this.Controls.Add(this.certificateStorageLabel);
      this.Controls.Add(this.certificateStorageBrowseButton);
      this.Controls.Add(this.certificateStorageTextBox);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SetupForm";
      this.Text = "Pi-Vote Kiosk - Control Setup";
      this.Load += new System.EventHandler(this.SetupForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox certificateStorageTextBox;
    private System.Windows.Forms.Button certificateStorageBrowseButton;
    private System.Windows.Forms.Label certificateStorageLabel;
    private System.Windows.Forms.TextBox serverCertificateTextBox;
    private System.Windows.Forms.Button serverCertificateBrowseButton;
    private System.Windows.Forms.Label serverCertificateLabel;
    private System.Windows.Forms.Button startButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.TextBox memberDatabaseTextBox;
    private System.Windows.Forms.Button memberDatabaseBrowseButton;
    private System.Windows.Forms.Label memberDatabaseLabel;
  }
}