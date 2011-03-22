/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Kiosk
{
  partial class KioskForm
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
      this.givennameLabel = new System.Windows.Forms.Label();
      this.givennameTextBox = new System.Windows.Forms.TextBox();
      this.surnameTextBox = new System.Windows.Forms.TextBox();
      this.emailAddressTextBox = new System.Windows.Forms.TextBox();
      this.surnameLabel = new System.Windows.Forms.Label();
      this.emailAddressLabel = new System.Windows.Forms.Label();
      this.passphraseLabel = new System.Windows.Forms.Label();
      this.passphraseTextBox = new System.Windows.Forms.TextBox();
      this.repeatTextBox = new System.Windows.Forms.TextBox();
      this.repeatLabel = new System.Windows.Forms.Label();
      this.serverCertificateLabel = new System.Windows.Forms.Label();
      this.label2 = new System.Windows.Forms.Label();
      this.serverCertificateTextBox = new System.Windows.Forms.TextBox();
      this.certificateStorageTextBox = new System.Windows.Forms.TextBox();
      this.certificateStorageLabel = new System.Windows.Forms.Label();
      this.okButton = new System.Windows.Forms.Button();
      this.requestStatusTextBox = new System.Windows.Forms.TextBox();
      this.reqzestStatusLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // givennameLabel
      // 
      this.givennameLabel.AutoSize = true;
      this.givennameLabel.Location = new System.Drawing.Point(39, 159);
      this.givennameLabel.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
      this.givennameLabel.Name = "givennameLabel";
      this.givennameLabel.Size = new System.Drawing.Size(150, 36);
      this.givennameLabel.TabIndex = 0;
      this.givennameLabel.Text = "Vorname:";
      // 
      // givennameTextBox
      // 
      this.givennameTextBox.Location = new System.Drawing.Point(379, 156);
      this.givennameTextBox.Name = "givennameTextBox";
      this.givennameTextBox.ReadOnly = true;
      this.givennameTextBox.Size = new System.Drawing.Size(651, 44);
      this.givennameTextBox.TabIndex = 1;
      // 
      // surnameTextBox
      // 
      this.surnameTextBox.Location = new System.Drawing.Point(379, 206);
      this.surnameTextBox.Name = "surnameTextBox";
      this.surnameTextBox.ReadOnly = true;
      this.surnameTextBox.Size = new System.Drawing.Size(651, 44);
      this.surnameTextBox.TabIndex = 2;
      // 
      // emailAddressTextBox
      // 
      this.emailAddressTextBox.Location = new System.Drawing.Point(379, 256);
      this.emailAddressTextBox.Name = "emailAddressTextBox";
      this.emailAddressTextBox.ReadOnly = true;
      this.emailAddressTextBox.Size = new System.Drawing.Size(651, 44);
      this.emailAddressTextBox.TabIndex = 3;
      // 
      // surnameLabel
      // 
      this.surnameLabel.AutoSize = true;
      this.surnameLabel.Location = new System.Drawing.Point(39, 209);
      this.surnameLabel.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
      this.surnameLabel.Name = "surnameLabel";
      this.surnameLabel.Size = new System.Drawing.Size(177, 36);
      this.surnameLabel.TabIndex = 4;
      this.surnameLabel.Text = "Nachname:";
      // 
      // emailAddressLabel
      // 
      this.emailAddressLabel.AutoSize = true;
      this.emailAddressLabel.Location = new System.Drawing.Point(39, 259);
      this.emailAddressLabel.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
      this.emailAddressLabel.Name = "emailAddressLabel";
      this.emailAddressLabel.Size = new System.Drawing.Size(214, 36);
      this.emailAddressLabel.TabIndex = 5;
      this.emailAddressLabel.Text = "Emailadresse:";
      // 
      // passphraseLabel
      // 
      this.passphraseLabel.AutoSize = true;
      this.passphraseLabel.Location = new System.Drawing.Point(39, 332);
      this.passphraseLabel.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
      this.passphraseLabel.Name = "passphraseLabel";
      this.passphraseLabel.Size = new System.Drawing.Size(190, 36);
      this.passphraseLabel.TabIndex = 6;
      this.passphraseLabel.Text = "Passphrase:";
      // 
      // passphraseTextBox
      // 
      this.passphraseTextBox.Enabled = false;
      this.passphraseTextBox.Location = new System.Drawing.Point(379, 329);
      this.passphraseTextBox.Name = "passphraseTextBox";
      this.passphraseTextBox.Size = new System.Drawing.Size(651, 44);
      this.passphraseTextBox.TabIndex = 7;
      this.passphraseTextBox.UseSystemPasswordChar = true;
      this.passphraseTextBox.TextChanged += new System.EventHandler(this.passphraseTextBox_TextChanged);
      // 
      // repeatTextBox
      // 
      this.repeatTextBox.Enabled = false;
      this.repeatTextBox.Location = new System.Drawing.Point(379, 379);
      this.repeatTextBox.Name = "repeatTextBox";
      this.repeatTextBox.Size = new System.Drawing.Size(651, 44);
      this.repeatTextBox.TabIndex = 8;
      this.repeatTextBox.UseSystemPasswordChar = true;
      this.repeatTextBox.TextChanged += new System.EventHandler(this.repeatTextBox_TextChanged);
      // 
      // repeatLabel
      // 
      this.repeatLabel.AutoSize = true;
      this.repeatLabel.Location = new System.Drawing.Point(39, 382);
      this.repeatLabel.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
      this.repeatLabel.Name = "repeatLabel";
      this.repeatLabel.Size = new System.Drawing.Size(202, 36);
      this.repeatLabel.TabIndex = 9;
      this.repeatLabel.Text = "Wiederholen:";
      // 
      // serverCertificateLabel
      // 
      this.serverCertificateLabel.AutoSize = true;
      this.serverCertificateLabel.Location = new System.Drawing.Point(39, 78);
      this.serverCertificateLabel.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
      this.serverCertificateLabel.Name = "serverCertificateLabel";
      this.serverCertificateLabel.Size = new System.Drawing.Size(225, 36);
      this.serverCertificateLabel.TabIndex = 10;
      this.serverCertificateLabel.Text = "Serverzertifikat";
      // 
      // label2
      // 
      this.label2.AutoSize = true;
      this.label2.Location = new System.Drawing.Point(119, 28);
      this.label2.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
      this.label2.Name = "label2";
      this.label2.Size = new System.Drawing.Size(0, 36);
      this.label2.TabIndex = 11;
      // 
      // serverCertificateTextBox
      // 
      this.serverCertificateTextBox.Location = new System.Drawing.Point(379, 75);
      this.serverCertificateTextBox.Name = "serverCertificateTextBox";
      this.serverCertificateTextBox.ReadOnly = true;
      this.serverCertificateTextBox.Size = new System.Drawing.Size(651, 44);
      this.serverCertificateTextBox.TabIndex = 12;
      // 
      // certificateStorageTextBox
      // 
      this.certificateStorageTextBox.Location = new System.Drawing.Point(379, 25);
      this.certificateStorageTextBox.Name = "certificateStorageTextBox";
      this.certificateStorageTextBox.ReadOnly = true;
      this.certificateStorageTextBox.Size = new System.Drawing.Size(651, 44);
      this.certificateStorageTextBox.TabIndex = 13;
      // 
      // certificateStorageLabel
      // 
      this.certificateStorageLabel.AutoSize = true;
      this.certificateStorageLabel.Location = new System.Drawing.Point(39, 28);
      this.certificateStorageLabel.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
      this.certificateStorageLabel.Name = "certificateStorageLabel";
      this.certificateStorageLabel.Size = new System.Drawing.Size(236, 36);
      this.certificateStorageLabel.TabIndex = 14;
      this.certificateStorageLabel.Text = "Basiszertifikate:";
      // 
      // okButton
      // 
      this.okButton.Enabled = false;
      this.okButton.Location = new System.Drawing.Point(858, 429);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(172, 51);
      this.okButton.TabIndex = 15;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // requestStatusTextBox
      // 
      this.requestStatusTextBox.Location = new System.Drawing.Point(379, 486);
      this.requestStatusTextBox.Name = "requestStatusTextBox";
      this.requestStatusTextBox.ReadOnly = true;
      this.requestStatusTextBox.Size = new System.Drawing.Size(651, 44);
      this.requestStatusTextBox.TabIndex = 16;
      // 
      // reqzestStatusLabel
      // 
      this.reqzestStatusLabel.AutoSize = true;
      this.reqzestStatusLabel.Location = new System.Drawing.Point(39, 489);
      this.reqzestStatusLabel.Margin = new System.Windows.Forms.Padding(9, 0, 9, 0);
      this.reqzestStatusLabel.Name = "reqzestStatusLabel";
      this.reqzestStatusLabel.Size = new System.Drawing.Size(331, 36);
      this.reqzestStatusLabel.TabIndex = 17;
      this.reqzestStatusLabel.Text = "Zertifizierungsanfrage:";
      // 
      // KioskForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(18F, 36F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1134, 579);
      this.Controls.Add(this.reqzestStatusLabel);
      this.Controls.Add(this.requestStatusTextBox);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.certificateStorageLabel);
      this.Controls.Add(this.certificateStorageTextBox);
      this.Controls.Add(this.serverCertificateTextBox);
      this.Controls.Add(this.label2);
      this.Controls.Add(this.serverCertificateLabel);
      this.Controls.Add(this.repeatLabel);
      this.Controls.Add(this.repeatTextBox);
      this.Controls.Add(this.passphraseTextBox);
      this.Controls.Add(this.passphraseLabel);
      this.Controls.Add(this.emailAddressLabel);
      this.Controls.Add(this.surnameLabel);
      this.Controls.Add(this.emailAddressTextBox);
      this.Controls.Add(this.surnameTextBox);
      this.Controls.Add(this.givennameTextBox);
      this.Controls.Add(this.givennameLabel);
      this.Font = new System.Drawing.Font("Arial", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(9, 8, 9, 8);
      this.Name = "KioskForm";
      this.Text = "Pi-Vote Kiosk";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KioskForm_FormClosing);
      this.Load += new System.EventHandler(this.KioskForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label givennameLabel;
    private System.Windows.Forms.TextBox givennameTextBox;
    private System.Windows.Forms.TextBox surnameTextBox;
    private System.Windows.Forms.TextBox emailAddressTextBox;
    private System.Windows.Forms.Label surnameLabel;
    private System.Windows.Forms.Label emailAddressLabel;
    private System.Windows.Forms.Label passphraseLabel;
    private System.Windows.Forms.TextBox passphraseTextBox;
    private System.Windows.Forms.TextBox repeatTextBox;
    private System.Windows.Forms.Label repeatLabel;
    private System.Windows.Forms.Label serverCertificateLabel;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.TextBox serverCertificateTextBox;
    private System.Windows.Forms.TextBox certificateStorageTextBox;
    private System.Windows.Forms.Label certificateStorageLabel;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.TextBox requestStatusTextBox;
    private System.Windows.Forms.Label reqzestStatusLabel;
  }
}

