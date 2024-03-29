﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Gui
{
  partial class DecryptPrivateKeyDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DecryptPrivateKeyDialog));
      this.infoLabel = new System.Windows.Forms.Label();
      this.passphraseTextBox = new System.Windows.Forms.TextBox();
      this.passphraseLabel = new System.Windows.Forms.Label();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.actionTextBox = new System.Windows.Forms.TextBox();
      this.certificateTypeTextBox = new System.Windows.Forms.TextBox();
      this.certificateIdTextBox = new System.Windows.Forms.TextBox();
      this.actionLabel = new System.Windows.Forms.Label();
      this.certificateTypeLabel = new System.Windows.Forms.Label();
      this.certificateIdLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // infoLabel
      // 
      this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.infoLabel.Location = new System.Drawing.Point(12, 9);
      this.infoLabel.Name = "infoLabel";
      this.infoLabel.Size = new System.Drawing.Size(465, 18);
      this.infoLabel.TabIndex = 0;
      this.infoLabel.Text = "Your private key is encrypted. You need to enter your passphrase to decrypt it.";
      // 
      // passphraseTextBox
      // 
      this.passphraseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.passphraseTextBox.Location = new System.Drawing.Point(131, 108);
      this.passphraseTextBox.Name = "passphraseTextBox";
      this.passphraseTextBox.PasswordChar = '*';
      this.passphraseTextBox.Size = new System.Drawing.Size(344, 20);
      this.passphraseTextBox.TabIndex = 0;
      this.passphraseTextBox.TextChanged += new System.EventHandler(this.passphraseTextBox_TextChanged);
      // 
      // passphraseLabel
      // 
      this.passphraseLabel.AutoSize = true;
      this.passphraseLabel.Location = new System.Drawing.Point(10, 110);
      this.passphraseLabel.Name = "passphraseLabel";
      this.passphraseLabel.Size = new System.Drawing.Size(68, 14);
      this.passphraseLabel.TabIndex = 3;
      this.passphraseLabel.Text = "Passphrase:";
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.Location = new System.Drawing.Point(283, 134);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(93, 26);
      this.okButton.TabIndex = 1;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(383, 134);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(93, 26);
      this.cancelButton.TabIndex = 2;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // actionTextBox
      // 
      this.actionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.actionTextBox.Location = new System.Drawing.Point(131, 82);
      this.actionTextBox.Name = "actionTextBox";
      this.actionTextBox.ReadOnly = true;
      this.actionTextBox.Size = new System.Drawing.Size(344, 20);
      this.actionTextBox.TabIndex = 5;
      // 
      // certificateTypeTextBox
      // 
      this.certificateTypeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.certificateTypeTextBox.Location = new System.Drawing.Point(131, 56);
      this.certificateTypeTextBox.Name = "certificateTypeTextBox";
      this.certificateTypeTextBox.ReadOnly = true;
      this.certificateTypeTextBox.Size = new System.Drawing.Size(344, 20);
      this.certificateTypeTextBox.TabIndex = 6;
      // 
      // certificateIdTextBox
      // 
      this.certificateIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.certificateIdTextBox.Location = new System.Drawing.Point(131, 30);
      this.certificateIdTextBox.Name = "certificateIdTextBox";
      this.certificateIdTextBox.ReadOnly = true;
      this.certificateIdTextBox.Size = new System.Drawing.Size(344, 20);
      this.certificateIdTextBox.TabIndex = 7;
      // 
      // actionLabel
      // 
      this.actionLabel.AutoSize = true;
      this.actionLabel.Location = new System.Drawing.Point(11, 84);
      this.actionLabel.Name = "actionLabel";
      this.actionLabel.Size = new System.Drawing.Size(41, 14);
      this.actionLabel.TabIndex = 8;
      this.actionLabel.Text = "Action:";
      // 
      // certificateTypeLabel
      // 
      this.certificateTypeLabel.AutoSize = true;
      this.certificateTypeLabel.Location = new System.Drawing.Point(11, 59);
      this.certificateTypeLabel.Name = "certificateTypeLabel";
      this.certificateTypeLabel.Size = new System.Drawing.Size(85, 14);
      this.certificateTypeLabel.TabIndex = 9;
      this.certificateTypeLabel.Text = "Certificate Type:";
      // 
      // certificateIdLabel
      // 
      this.certificateIdLabel.AutoSize = true;
      this.certificateIdLabel.Location = new System.Drawing.Point(11, 33);
      this.certificateIdLabel.Name = "certificateIdLabel";
      this.certificateIdLabel.Size = new System.Drawing.Size(70, 14);
      this.certificateIdLabel.TabIndex = 10;
      this.certificateIdLabel.Text = "Certificate Id:";
      // 
      // DecryptPrivateKeyDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(489, 172);
      this.ControlBox = false;
      this.Controls.Add(this.certificateIdLabel);
      this.Controls.Add(this.certificateTypeLabel);
      this.Controls.Add(this.actionLabel);
      this.Controls.Add(this.certificateIdTextBox);
      this.Controls.Add(this.certificateTypeTextBox);
      this.Controls.Add(this.actionTextBox);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.passphraseTextBox);
      this.Controls.Add(this.passphraseLabel);
      this.Controls.Add(this.infoLabel);
      this.Font = new System.Drawing.Font("Arial", 8.25F);

      // This hack is necessary because the mono compiler/runtime seems to be broken when it comes to icons.
#if !__MonoCS__
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
#endif

      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(2000, 219);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(400, 180);
      this.Name = "DecryptPrivateKeyDialog";
      this.ShowInTaskbar = false;
      this.Text = "Pi-Vote Private Key Protection";
      this.TopMost = true;
      this.Load += new System.EventHandler(this.EncryptPrivateKeyDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EncryptPrivateKeyDialog_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label infoLabel;
    private System.Windows.Forms.TextBox passphraseTextBox;
    private System.Windows.Forms.Label passphraseLabel;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.TextBox actionTextBox;
    private System.Windows.Forms.TextBox certificateTypeTextBox;
    private System.Windows.Forms.TextBox certificateIdTextBox;
    private System.Windows.Forms.Label actionLabel;
    private System.Windows.Forms.Label certificateTypeLabel;
    private System.Windows.Forms.Label certificateIdLabel;
  }
}