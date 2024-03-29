﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.CaGui
{
  partial class SignDialog
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
      this.validUntilLabel = new System.Windows.Forms.Label();
      this.cancelButton = new System.Windows.Forms.Button();
      this.okButton = new System.Windows.Forms.Button();
      this.idTextBox = new System.Windows.Forms.TextBox();
      this.nameTextBox = new System.Windows.Forms.TextBox();
      this.nameLabel = new System.Windows.Forms.Label();
      this.idLabel = new System.Windows.Forms.Label();
      this.validUntilPicker = new System.Windows.Forms.DateTimePicker();
      this.typeLabel = new System.Windows.Forms.Label();
      this.typeTextBox = new System.Windows.Forms.TextBox();
      this.emailAddressLabel = new System.Windows.Forms.Label();
      this.emailAddressTextBox = new System.Windows.Forms.TextBox();
      this.cantonlabel = new System.Windows.Forms.Label();
      this.cantonTextBox = new System.Windows.Forms.TextBox();
      this.fingerprintLabel = new System.Windows.Forms.Label();
      this.fingerprintTextBox = new System.Windows.Forms.TextBox();
      this.acceptSignRadioButton = new System.Windows.Forms.RadioButton();
      this.reasonComboBox = new System.Windows.Forms.ComboBox();
      this.reasonLabel = new System.Windows.Forms.Label();
      this.refuseRadioButton = new System.Windows.Forms.RadioButton();
      this.signedByFingerPrintLabel = new System.Windows.Forms.Label();
      this.signedByFingerprintTextBox = new System.Windows.Forms.TextBox();
      this.signedByEmailAddressLabel = new System.Windows.Forms.Label();
      this.signedByEmailAddressTextBox = new System.Windows.Forms.TextBox();
      this.signedByIdLabel = new System.Windows.Forms.Label();
      this.signedByNameLabel = new System.Windows.Forms.Label();
      this.signedByNameTextBox = new System.Windows.Forms.TextBox();
      this.signedByIdTextBox = new System.Windows.Forms.TextBox();
      this.signedByLabel = new System.Windows.Forms.Label();
      this.signedByStatusLabel = new System.Windows.Forms.Label();
      this.signedByStatusTextBox = new System.Windows.Forms.TextBox();
      this.signedBySignatureLabel = new System.Windows.Forms.Label();
      this.signedBySignatureTextBox = new System.Windows.Forms.TextBox();
      this.printButton = new System.Windows.Forms.Button();
      this.validFromPicker = new System.Windows.Forms.DateTimePicker();
      this.validFromLabel = new System.Windows.Forms.Label();
      this.signedByTypeLabel = new System.Windows.Forms.Label();
      this.signedByCantonLabel = new System.Windows.Forms.Label();
      this.certificateLabel = new System.Windows.Forms.Label();
      this.signedByTypeTextBox = new System.Windows.Forms.TextBox();
      this.signedByCantonTextBox = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // validUntilLabel
      // 
      this.validUntilLabel.AutoSize = true;
      this.validUntilLabel.Location = new System.Drawing.Point(8, 286);
      this.validUntilLabel.Name = "validUntilLabel";
      this.validUntilLabel.Size = new System.Drawing.Size(55, 14);
      this.validUntilLabel.TabIndex = 0;
      this.validUntilLabel.Text = "Valid until:";
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(823, 368);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 7;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.Enabled = false;
      this.okButton.Location = new System.Drawing.Point(742, 368);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 6;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // idTextBox
      // 
      this.idTextBox.Location = new System.Drawing.Point(94, 31);
      this.idTextBox.Name = "idTextBox";
      this.idTextBox.ReadOnly = true;
      this.idTextBox.Size = new System.Drawing.Size(350, 20);
      this.idTextBox.TabIndex = 3;
      // 
      // nameTextBox
      // 
      this.nameTextBox.Location = new System.Drawing.Point(94, 83);
      this.nameTextBox.Name = "nameTextBox";
      this.nameTextBox.ReadOnly = true;
      this.nameTextBox.Size = new System.Drawing.Size(350, 20);
      this.nameTextBox.TabIndex = 4;
      // 
      // nameLabel
      // 
      this.nameLabel.AutoSize = true;
      this.nameLabel.Location = new System.Drawing.Point(8, 86);
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Size = new System.Drawing.Size(37, 14);
      this.nameLabel.TabIndex = 5;
      this.nameLabel.Text = "Name:";
      // 
      // idLabel
      // 
      this.idLabel.AutoSize = true;
      this.idLabel.Location = new System.Drawing.Point(8, 34);
      this.idLabel.Name = "idLabel";
      this.idLabel.Size = new System.Drawing.Size(18, 14);
      this.idLabel.TabIndex = 6;
      this.idLabel.Text = "Id:";
      // 
      // validUntilPicker
      // 
      this.validUntilPicker.Enabled = false;
      this.validUntilPicker.Location = new System.Drawing.Point(94, 281);
      this.validUntilPicker.Name = "validUntilPicker";
      this.validUntilPicker.Size = new System.Drawing.Size(350, 20);
      this.validUntilPicker.TabIndex = 3;
      this.validUntilPicker.ValueChanged += new System.EventHandler(this.validUntilPicker_ValueChanged);
      // 
      // typeLabel
      // 
      this.typeLabel.AutoSize = true;
      this.typeLabel.Location = new System.Drawing.Point(8, 60);
      this.typeLabel.Name = "typeLabel";
      this.typeLabel.Size = new System.Drawing.Size(33, 14);
      this.typeLabel.TabIndex = 9;
      this.typeLabel.Text = "Type:";
      // 
      // typeTextBox
      // 
      this.typeTextBox.Location = new System.Drawing.Point(94, 57);
      this.typeTextBox.Name = "typeTextBox";
      this.typeTextBox.ReadOnly = true;
      this.typeTextBox.Size = new System.Drawing.Size(350, 20);
      this.typeTextBox.TabIndex = 8;
      // 
      // emailAddressLabel
      // 
      this.emailAddressLabel.AutoSize = true;
      this.emailAddressLabel.Location = new System.Drawing.Point(8, 112);
      this.emailAddressLabel.Name = "emailAddressLabel";
      this.emailAddressLabel.Size = new System.Drawing.Size(77, 14);
      this.emailAddressLabel.TabIndex = 11;
      this.emailAddressLabel.Text = "Email address:";
      // 
      // emailAddressTextBox
      // 
      this.emailAddressTextBox.Location = new System.Drawing.Point(94, 109);
      this.emailAddressTextBox.Name = "emailAddressTextBox";
      this.emailAddressTextBox.ReadOnly = true;
      this.emailAddressTextBox.Size = new System.Drawing.Size(350, 20);
      this.emailAddressTextBox.TabIndex = 10;
      // 
      // cantonlabel
      // 
      this.cantonlabel.AutoSize = true;
      this.cantonlabel.Location = new System.Drawing.Point(8, 138);
      this.cantonlabel.Name = "cantonlabel";
      this.cantonlabel.Size = new System.Drawing.Size(44, 14);
      this.cantonlabel.TabIndex = 13;
      this.cantonlabel.Text = "Canton:";
      // 
      // cantonTextBox
      // 
      this.cantonTextBox.Location = new System.Drawing.Point(94, 135);
      this.cantonTextBox.Name = "cantonTextBox";
      this.cantonTextBox.ReadOnly = true;
      this.cantonTextBox.Size = new System.Drawing.Size(350, 20);
      this.cantonTextBox.TabIndex = 12;
      // 
      // fingerprintLabel
      // 
      this.fingerprintLabel.AutoSize = true;
      this.fingerprintLabel.Location = new System.Drawing.Point(8, 164);
      this.fingerprintLabel.Name = "fingerprintLabel";
      this.fingerprintLabel.Size = new System.Drawing.Size(61, 14);
      this.fingerprintLabel.TabIndex = 15;
      this.fingerprintLabel.Text = "Fingerprint:";
      // 
      // fingerprintTextBox
      // 
      this.fingerprintTextBox.Location = new System.Drawing.Point(94, 161);
      this.fingerprintTextBox.Multiline = true;
      this.fingerprintTextBox.Name = "fingerprintTextBox";
      this.fingerprintTextBox.ReadOnly = true;
      this.fingerprintTextBox.Size = new System.Drawing.Size(350, 49);
      this.fingerprintTextBox.TabIndex = 14;
      // 
      // acceptSignRadioButton
      // 
      this.acceptSignRadioButton.AutoSize = true;
      this.acceptSignRadioButton.Location = new System.Drawing.Point(11, 231);
      this.acceptSignRadioButton.Name = "acceptSignRadioButton";
      this.acceptSignRadioButton.Size = new System.Drawing.Size(105, 18);
      this.acceptSignRadioButton.TabIndex = 1;
      this.acceptSignRadioButton.TabStop = true;
      this.acceptSignRadioButton.Text = "Accept and Sign";
      this.acceptSignRadioButton.UseVisualStyleBackColor = true;
      this.acceptSignRadioButton.CheckedChanged += new System.EventHandler(this.acceptSignRadioButton_CheckedChanged);
      // 
      // reasonComboBox
      // 
      this.reasonComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.reasonComboBox.Enabled = false;
      this.reasonComboBox.FormattingEnabled = true;
      this.reasonComboBox.Location = new System.Drawing.Point(94, 341);
      this.reasonComboBox.Name = "reasonComboBox";
      this.reasonComboBox.Size = new System.Drawing.Size(350, 22);
      this.reasonComboBox.TabIndex = 5;
      this.reasonComboBox.SelectedIndexChanged += new System.EventHandler(this.reasonComboBox_SelectedIndexChanged);
      // 
      // reasonLabel
      // 
      this.reasonLabel.AutoSize = true;
      this.reasonLabel.Location = new System.Drawing.Point(8, 344);
      this.reasonLabel.Name = "reasonLabel";
      this.reasonLabel.Size = new System.Drawing.Size(47, 14);
      this.reasonLabel.TabIndex = 18;
      this.reasonLabel.Text = "Reason:";
      // 
      // refuseRadioButton
      // 
      this.refuseRadioButton.AutoSize = true;
      this.refuseRadioButton.Location = new System.Drawing.Point(11, 318);
      this.refuseRadioButton.Name = "refuseRadioButton";
      this.refuseRadioButton.Size = new System.Drawing.Size(60, 18);
      this.refuseRadioButton.TabIndex = 4;
      this.refuseRadioButton.TabStop = true;
      this.refuseRadioButton.Text = "Refuse";
      this.refuseRadioButton.UseVisualStyleBackColor = true;
      this.refuseRadioButton.CheckedChanged += new System.EventHandler(this.refuseRadioButton_CheckedChanged);
      // 
      // signedByFingerPrintLabel
      // 
      this.signedByFingerPrintLabel.AutoSize = true;
      this.signedByFingerPrintLabel.Location = new System.Drawing.Point(465, 164);
      this.signedByFingerPrintLabel.Name = "signedByFingerPrintLabel";
      this.signedByFingerPrintLabel.Size = new System.Drawing.Size(61, 14);
      this.signedByFingerPrintLabel.TabIndex = 27;
      this.signedByFingerPrintLabel.Text = "Fingerprint:";
      // 
      // signedByFingerprintTextBox
      // 
      this.signedByFingerprintTextBox.Location = new System.Drawing.Point(548, 161);
      this.signedByFingerprintTextBox.Multiline = true;
      this.signedByFingerprintTextBox.Name = "signedByFingerprintTextBox";
      this.signedByFingerprintTextBox.ReadOnly = true;
      this.signedByFingerprintTextBox.Size = new System.Drawing.Size(350, 49);
      this.signedByFingerprintTextBox.TabIndex = 26;
      // 
      // signedByEmailAddressLabel
      // 
      this.signedByEmailAddressLabel.AutoSize = true;
      this.signedByEmailAddressLabel.Location = new System.Drawing.Point(465, 112);
      this.signedByEmailAddressLabel.Name = "signedByEmailAddressLabel";
      this.signedByEmailAddressLabel.Size = new System.Drawing.Size(77, 14);
      this.signedByEmailAddressLabel.TabIndex = 25;
      this.signedByEmailAddressLabel.Text = "Email address:";
      // 
      // signedByEmailAddressTextBox
      // 
      this.signedByEmailAddressTextBox.Location = new System.Drawing.Point(548, 109);
      this.signedByEmailAddressTextBox.Name = "signedByEmailAddressTextBox";
      this.signedByEmailAddressTextBox.ReadOnly = true;
      this.signedByEmailAddressTextBox.Size = new System.Drawing.Size(350, 20);
      this.signedByEmailAddressTextBox.TabIndex = 24;
      // 
      // signedByIdLabel
      // 
      this.signedByIdLabel.AutoSize = true;
      this.signedByIdLabel.Location = new System.Drawing.Point(465, 34);
      this.signedByIdLabel.Name = "signedByIdLabel";
      this.signedByIdLabel.Size = new System.Drawing.Size(18, 14);
      this.signedByIdLabel.TabIndex = 23;
      this.signedByIdLabel.Text = "Id:";
      // 
      // signedByNameLabel
      // 
      this.signedByNameLabel.AutoSize = true;
      this.signedByNameLabel.Location = new System.Drawing.Point(465, 86);
      this.signedByNameLabel.Name = "signedByNameLabel";
      this.signedByNameLabel.Size = new System.Drawing.Size(37, 14);
      this.signedByNameLabel.TabIndex = 22;
      this.signedByNameLabel.Text = "Name:";
      // 
      // signedByNameTextBox
      // 
      this.signedByNameTextBox.Location = new System.Drawing.Point(548, 83);
      this.signedByNameTextBox.Name = "signedByNameTextBox";
      this.signedByNameTextBox.ReadOnly = true;
      this.signedByNameTextBox.Size = new System.Drawing.Size(350, 20);
      this.signedByNameTextBox.TabIndex = 21;
      // 
      // signedByIdTextBox
      // 
      this.signedByIdTextBox.Location = new System.Drawing.Point(548, 31);
      this.signedByIdTextBox.Name = "signedByIdTextBox";
      this.signedByIdTextBox.ReadOnly = true;
      this.signedByIdTextBox.Size = new System.Drawing.Size(350, 20);
      this.signedByIdTextBox.TabIndex = 20;
      // 
      // signedByLabel
      // 
      this.signedByLabel.AutoSize = true;
      this.signedByLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.signedByLabel.Location = new System.Drawing.Point(465, 9);
      this.signedByLabel.Name = "signedByLabel";
      this.signedByLabel.Size = new System.Drawing.Size(122, 14);
      this.signedByLabel.TabIndex = 28;
      this.signedByLabel.Text = "Signed by certificate:";
      // 
      // signedByStatusLabel
      // 
      this.signedByStatusLabel.AutoSize = true;
      this.signedByStatusLabel.Location = new System.Drawing.Point(465, 219);
      this.signedByStatusLabel.Name = "signedByStatusLabel";
      this.signedByStatusLabel.Size = new System.Drawing.Size(41, 14);
      this.signedByStatusLabel.TabIndex = 30;
      this.signedByStatusLabel.Text = "Status:";
      // 
      // signedByStatusTextBox
      // 
      this.signedByStatusTextBox.Location = new System.Drawing.Point(548, 216);
      this.signedByStatusTextBox.Name = "signedByStatusTextBox";
      this.signedByStatusTextBox.ReadOnly = true;
      this.signedByStatusTextBox.Size = new System.Drawing.Size(350, 20);
      this.signedByStatusTextBox.TabIndex = 29;
      // 
      // signedBySignatureLabel
      // 
      this.signedBySignatureLabel.AutoSize = true;
      this.signedBySignatureLabel.Location = new System.Drawing.Point(465, 245);
      this.signedBySignatureLabel.Name = "signedBySignatureLabel";
      this.signedBySignatureLabel.Size = new System.Drawing.Size(56, 14);
      this.signedBySignatureLabel.TabIndex = 32;
      this.signedBySignatureLabel.Text = "Signature:";
      // 
      // signedBySignatureTextBox
      // 
      this.signedBySignatureTextBox.Location = new System.Drawing.Point(548, 242);
      this.signedBySignatureTextBox.Name = "signedBySignatureTextBox";
      this.signedBySignatureTextBox.ReadOnly = true;
      this.signedBySignatureTextBox.Size = new System.Drawing.Size(350, 20);
      this.signedBySignatureTextBox.TabIndex = 31;
      // 
      // printButton
      // 
      this.printButton.Enabled = false;
      this.printButton.Location = new System.Drawing.Point(823, 268);
      this.printButton.Name = "printButton";
      this.printButton.Size = new System.Drawing.Size(75, 23);
      this.printButton.TabIndex = 0;
      this.printButton.Text = "&Print";
      this.printButton.UseVisualStyleBackColor = true;
      this.printButton.Click += new System.EventHandler(this.printButton_Click);
      // 
      // validFromPicker
      // 
      this.validFromPicker.Enabled = false;
      this.validFromPicker.Location = new System.Drawing.Point(94, 255);
      this.validFromPicker.Name = "validFromPicker";
      this.validFromPicker.Size = new System.Drawing.Size(350, 20);
      this.validFromPicker.TabIndex = 2;
      this.validFromPicker.ValueChanged += new System.EventHandler(this.validFromPicker_ValueChanged);
      // 
      // validFromLabel
      // 
      this.validFromLabel.AutoSize = true;
      this.validFromLabel.Location = new System.Drawing.Point(8, 260);
      this.validFromLabel.Name = "validFromLabel";
      this.validFromLabel.Size = new System.Drawing.Size(58, 14);
      this.validFromLabel.TabIndex = 35;
      this.validFromLabel.Text = "Valid from:";
      // 
      // signedByTypeLabel
      // 
      this.signedByTypeLabel.AutoSize = true;
      this.signedByTypeLabel.Location = new System.Drawing.Point(465, 60);
      this.signedByTypeLabel.Name = "signedByTypeLabel";
      this.signedByTypeLabel.Size = new System.Drawing.Size(33, 14);
      this.signedByTypeLabel.TabIndex = 36;
      this.signedByTypeLabel.Text = "Type:";
      // 
      // signedByCantonLabel
      // 
      this.signedByCantonLabel.AutoSize = true;
      this.signedByCantonLabel.Location = new System.Drawing.Point(465, 138);
      this.signedByCantonLabel.Name = "signedByCantonLabel";
      this.signedByCantonLabel.Size = new System.Drawing.Size(44, 14);
      this.signedByCantonLabel.TabIndex = 37;
      this.signedByCantonLabel.Text = "Canton:";
      // 
      // certificateLabel
      // 
      this.certificateLabel.AutoSize = true;
      this.certificateLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.certificateLabel.Location = new System.Drawing.Point(8, 9);
      this.certificateLabel.Name = "certificateLabel";
      this.certificateLabel.Size = new System.Drawing.Size(125, 14);
      this.certificateLabel.TabIndex = 38;
      this.certificateLabel.Text = "Verifying request for:";
      // 
      // signedByTypeTextBox
      // 
      this.signedByTypeTextBox.Location = new System.Drawing.Point(548, 57);
      this.signedByTypeTextBox.Name = "signedByTypeTextBox";
      this.signedByTypeTextBox.ReadOnly = true;
      this.signedByTypeTextBox.Size = new System.Drawing.Size(350, 20);
      this.signedByTypeTextBox.TabIndex = 39;
      // 
      // signedByCantonTextBox
      // 
      this.signedByCantonTextBox.Location = new System.Drawing.Point(548, 135);
      this.signedByCantonTextBox.Name = "signedByCantonTextBox";
      this.signedByCantonTextBox.ReadOnly = true;
      this.signedByCantonTextBox.Size = new System.Drawing.Size(350, 20);
      this.signedByCantonTextBox.TabIndex = 40;
      // 
      // SignDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(915, 403);
      this.ControlBox = false;
      this.Controls.Add(this.signedByCantonTextBox);
      this.Controls.Add(this.signedByTypeTextBox);
      this.Controls.Add(this.certificateLabel);
      this.Controls.Add(this.signedByCantonLabel);
      this.Controls.Add(this.signedByTypeLabel);
      this.Controls.Add(this.validFromLabel);
      this.Controls.Add(this.validFromPicker);
      this.Controls.Add(this.printButton);
      this.Controls.Add(this.signedBySignatureLabel);
      this.Controls.Add(this.signedBySignatureTextBox);
      this.Controls.Add(this.signedByStatusLabel);
      this.Controls.Add(this.signedByStatusTextBox);
      this.Controls.Add(this.signedByLabel);
      this.Controls.Add(this.signedByFingerPrintLabel);
      this.Controls.Add(this.signedByFingerprintTextBox);
      this.Controls.Add(this.signedByEmailAddressLabel);
      this.Controls.Add(this.signedByEmailAddressTextBox);
      this.Controls.Add(this.signedByIdLabel);
      this.Controls.Add(this.signedByNameLabel);
      this.Controls.Add(this.signedByNameTextBox);
      this.Controls.Add(this.signedByIdTextBox);
      this.Controls.Add(this.refuseRadioButton);
      this.Controls.Add(this.reasonLabel);
      this.Controls.Add(this.reasonComboBox);
      this.Controls.Add(this.acceptSignRadioButton);
      this.Controls.Add(this.fingerprintLabel);
      this.Controls.Add(this.fingerprintTextBox);
      this.Controls.Add(this.cantonlabel);
      this.Controls.Add(this.cantonTextBox);
      this.Controls.Add(this.emailAddressLabel);
      this.Controls.Add(this.emailAddressTextBox);
      this.Controls.Add(this.typeLabel);
      this.Controls.Add(this.typeTextBox);
      this.Controls.Add(this.validUntilPicker);
      this.Controls.Add(this.idLabel);
      this.Controls.Add(this.nameLabel);
      this.Controls.Add(this.nameTextBox);
      this.Controls.Add(this.idTextBox);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.validUntilLabel);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(923, 430);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(923, 430);
      this.Name = "SignDialog";
      this.Text = "Verify Signature Request";
      this.Load += new System.EventHandler(this.CaNameDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RefuseDialog_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label validUntilLabel;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.TextBox idTextBox;
    private System.Windows.Forms.TextBox nameTextBox;
    private System.Windows.Forms.Label nameLabel;
    private System.Windows.Forms.Label idLabel;
    private System.Windows.Forms.DateTimePicker validUntilPicker;
    private System.Windows.Forms.Label typeLabel;
    private System.Windows.Forms.TextBox typeTextBox;
    private System.Windows.Forms.Label emailAddressLabel;
    private System.Windows.Forms.TextBox emailAddressTextBox;
    private System.Windows.Forms.Label cantonlabel;
    private System.Windows.Forms.TextBox cantonTextBox;
    private System.Windows.Forms.Label fingerprintLabel;
    private System.Windows.Forms.TextBox fingerprintTextBox;
    private System.Windows.Forms.RadioButton acceptSignRadioButton;
    private System.Windows.Forms.ComboBox reasonComboBox;
    private System.Windows.Forms.Label reasonLabel;
    private System.Windows.Forms.RadioButton refuseRadioButton;
    private System.Windows.Forms.Label signedByFingerPrintLabel;
    private System.Windows.Forms.TextBox signedByFingerprintTextBox;
    private System.Windows.Forms.Label signedByEmailAddressLabel;
    private System.Windows.Forms.TextBox signedByEmailAddressTextBox;
    private System.Windows.Forms.Label signedByIdLabel;
    private System.Windows.Forms.Label signedByNameLabel;
    private System.Windows.Forms.TextBox signedByNameTextBox;
    private System.Windows.Forms.TextBox signedByIdTextBox;
    private System.Windows.Forms.Label signedByLabel;
    private System.Windows.Forms.Label signedByStatusLabel;
    private System.Windows.Forms.TextBox signedByStatusTextBox;
    private System.Windows.Forms.Label signedBySignatureLabel;
    private System.Windows.Forms.TextBox signedBySignatureTextBox;
    private System.Windows.Forms.Button printButton;
    private System.Windows.Forms.DateTimePicker validFromPicker;
    private System.Windows.Forms.Label validFromLabel;
    private System.Windows.Forms.Label signedByTypeLabel;
    private System.Windows.Forms.Label signedByCantonLabel;
    private System.Windows.Forms.Label certificateLabel;
    private System.Windows.Forms.TextBox signedByTypeTextBox;
    private System.Windows.Forms.TextBox signedByCantonTextBox;
  }
}