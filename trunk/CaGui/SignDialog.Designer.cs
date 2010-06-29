﻿namespace Pirate.PiVote.CaGui
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
      this.SuspendLayout();
      // 
      // validUntilLabel
      // 
      this.validUntilLabel.AutoSize = true;
      this.validUntilLabel.Location = new System.Drawing.Point(11, 172);
      this.validUntilLabel.Name = "validUntilLabel";
      this.validUntilLabel.Size = new System.Drawing.Size(55, 14);
      this.validUntilLabel.TabIndex = 0;
      this.validUntilLabel.Text = "Valid until:";
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(463, 195);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 2;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.Enabled = false;
      this.okButton.Location = new System.Drawing.Point(382, 195);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 1;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // idTextBox
      // 
      this.idTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.idTextBox.Location = new System.Drawing.Point(94, 12);
      this.idTextBox.Name = "idTextBox";
      this.idTextBox.ReadOnly = true;
      this.idTextBox.Size = new System.Drawing.Size(444, 20);
      this.idTextBox.TabIndex = 3;
      // 
      // nameTextBox
      // 
      this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.nameTextBox.Location = new System.Drawing.Point(94, 65);
      this.nameTextBox.Name = "nameTextBox";
      this.nameTextBox.ReadOnly = true;
      this.nameTextBox.Size = new System.Drawing.Size(444, 20);
      this.nameTextBox.TabIndex = 4;
      // 
      // nameLabel
      // 
      this.nameLabel.AutoSize = true;
      this.nameLabel.Location = new System.Drawing.Point(11, 69);
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Size = new System.Drawing.Size(37, 14);
      this.nameLabel.TabIndex = 5;
      this.nameLabel.Text = "Name:";
      // 
      // idLabel
      // 
      this.idLabel.AutoSize = true;
      this.idLabel.Location = new System.Drawing.Point(12, 15);
      this.idLabel.Name = "idLabel";
      this.idLabel.Size = new System.Drawing.Size(18, 14);
      this.idLabel.TabIndex = 6;
      this.idLabel.Text = "Id:";
      // 
      // validUntilPicker
      // 
      this.validUntilPicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.validUntilPicker.Location = new System.Drawing.Point(94, 169);
      this.validUntilPicker.Name = "validUntilPicker";
      this.validUntilPicker.Size = new System.Drawing.Size(444, 20);
      this.validUntilPicker.TabIndex = 7;
      this.validUntilPicker.ValueChanged += new System.EventHandler(this.validUntilPicker_ValueChanged);
      // 
      // typeLabel
      // 
      this.typeLabel.AutoSize = true;
      this.typeLabel.Location = new System.Drawing.Point(11, 42);
      this.typeLabel.Name = "typeLabel";
      this.typeLabel.Size = new System.Drawing.Size(33, 14);
      this.typeLabel.TabIndex = 9;
      this.typeLabel.Text = "Type:";
      // 
      // typeTextBox
      // 
      this.typeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.typeTextBox.Location = new System.Drawing.Point(94, 39);
      this.typeTextBox.Name = "typeTextBox";
      this.typeTextBox.ReadOnly = true;
      this.typeTextBox.Size = new System.Drawing.Size(444, 20);
      this.typeTextBox.TabIndex = 8;
      // 
      // emailAddressLabel
      // 
      this.emailAddressLabel.AutoSize = true;
      this.emailAddressLabel.Location = new System.Drawing.Point(11, 95);
      this.emailAddressLabel.Name = "emailAddressLabel";
      this.emailAddressLabel.Size = new System.Drawing.Size(77, 14);
      this.emailAddressLabel.TabIndex = 11;
      this.emailAddressLabel.Text = "Email address:";
      // 
      // emailAddressTextBox
      // 
      this.emailAddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.emailAddressTextBox.Location = new System.Drawing.Point(94, 91);
      this.emailAddressTextBox.Name = "emailAddressTextBox";
      this.emailAddressTextBox.ReadOnly = true;
      this.emailAddressTextBox.Size = new System.Drawing.Size(444, 20);
      this.emailAddressTextBox.TabIndex = 10;
      // 
      // cantonlabel
      // 
      this.cantonlabel.AutoSize = true;
      this.cantonlabel.Location = new System.Drawing.Point(11, 121);
      this.cantonlabel.Name = "cantonlabel";
      this.cantonlabel.Size = new System.Drawing.Size(44, 14);
      this.cantonlabel.TabIndex = 13;
      this.cantonlabel.Text = "Canton:";
      // 
      // cantonTextBox
      // 
      this.cantonTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cantonTextBox.Location = new System.Drawing.Point(94, 117);
      this.cantonTextBox.Name = "cantonTextBox";
      this.cantonTextBox.ReadOnly = true;
      this.cantonTextBox.Size = new System.Drawing.Size(444, 20);
      this.cantonTextBox.TabIndex = 12;
      // 
      // fingerprintLabel
      // 
      this.fingerprintLabel.AutoSize = true;
      this.fingerprintLabel.Location = new System.Drawing.Point(11, 147);
      this.fingerprintLabel.Name = "fingerprintLabel";
      this.fingerprintLabel.Size = new System.Drawing.Size(61, 14);
      this.fingerprintLabel.TabIndex = 15;
      this.fingerprintLabel.Text = "Fingerprint:";
      // 
      // fingerprintTextBox
      // 
      this.fingerprintTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.fingerprintTextBox.Location = new System.Drawing.Point(94, 143);
      this.fingerprintTextBox.Name = "fingerprintTextBox";
      this.fingerprintTextBox.ReadOnly = true;
      this.fingerprintTextBox.Size = new System.Drawing.Size(444, 20);
      this.fingerprintTextBox.TabIndex = 14;
      // 
      // SignDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(550, 231);
      this.ControlBox = false;
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
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SignDialog";
      this.Text = "Refuse Signature Request";
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
  }
}