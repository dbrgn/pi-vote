﻿namespace Pirate.PiVote.CaGui
{
  partial class CreateAdminDialog
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
      this.cancelButton = new System.Windows.Forms.Button();
      this.okButton = new System.Windows.Forms.Button();
      this.firstNameTextBox = new System.Windows.Forms.TextBox();
      this.emailAddressTextBox = new System.Windows.Forms.TextBox();
      this.emailAddressLabel = new System.Windows.Forms.Label();
      this.firstNameLabel = new System.Windows.Forms.Label();
      this.familyNameLabel = new System.Windows.Forms.Label();
      this.familyNameTextBox = new System.Windows.Forms.TextBox();
      this.functionLabel = new System.Windows.Forms.Label();
      this.functionTextBox = new System.Windows.Forms.TextBox();
      this.validUntilPicker = new System.Windows.Forms.DateTimePicker();
      this.validUntilLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // cancelButton
      // 
      this.cancelButton.Location = new System.Drawing.Point(464, 218);
      this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(112, 35);
      this.cancelButton.TabIndex = 5;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Enabled = false;
      this.okButton.Location = new System.Drawing.Point(342, 218);
      this.okButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(112, 35);
      this.okButton.TabIndex = 4;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // firstNameTextBox
      // 
      this.firstNameTextBox.Location = new System.Drawing.Point(140, 18);
      this.firstNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.firstNameTextBox.Name = "firstNameTextBox";
      this.firstNameTextBox.Size = new System.Drawing.Size(434, 26);
      this.firstNameTextBox.TabIndex = 0;
      this.firstNameTextBox.TextChanged += new System.EventHandler(this.firstNameTextBox_TextChanged);
      // 
      // emailAddressTextBox
      // 
      this.emailAddressTextBox.Location = new System.Drawing.Point(140, 138);
      this.emailAddressTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.emailAddressTextBox.Name = "emailAddressTextBox";
      this.emailAddressTextBox.Size = new System.Drawing.Size(434, 26);
      this.emailAddressTextBox.TabIndex = 3;
      this.emailAddressTextBox.TextChanged += new System.EventHandler(this.emailAddressTextBox_TextChanged);
      // 
      // emailAddressLabel
      // 
      this.emailAddressLabel.AutoSize = true;
      this.emailAddressLabel.Location = new System.Drawing.Point(18, 143);
      this.emailAddressLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.emailAddressLabel.Name = "emailAddressLabel";
      this.emailAddressLabel.Size = new System.Drawing.Size(113, 20);
      this.emailAddressLabel.TabIndex = 5;
      this.emailAddressLabel.Text = "Email address:";
      // 
      // firstNameLabel
      // 
      this.firstNameLabel.AutoSize = true;
      this.firstNameLabel.Location = new System.Drawing.Point(18, 23);
      this.firstNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.firstNameLabel.Name = "firstNameLabel";
      this.firstNameLabel.Size = new System.Drawing.Size(88, 20);
      this.firstNameLabel.TabIndex = 6;
      this.firstNameLabel.Text = "First name:";
      // 
      // familyNameLabel
      // 
      this.familyNameLabel.AutoSize = true;
      this.familyNameLabel.Location = new System.Drawing.Point(18, 63);
      this.familyNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.familyNameLabel.Name = "familyNameLabel";
      this.familyNameLabel.Size = new System.Drawing.Size(102, 20);
      this.familyNameLabel.TabIndex = 11;
      this.familyNameLabel.Text = "Family name:";
      // 
      // familyNameTextBox
      // 
      this.familyNameTextBox.Location = new System.Drawing.Point(140, 58);
      this.familyNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.familyNameTextBox.Name = "familyNameTextBox";
      this.familyNameTextBox.Size = new System.Drawing.Size(434, 26);
      this.familyNameTextBox.TabIndex = 1;
      this.familyNameTextBox.TextChanged += new System.EventHandler(this.familyNameTextBox_TextChanged);
      // 
      // functionLabel
      // 
      this.functionLabel.AutoSize = true;
      this.functionLabel.Location = new System.Drawing.Point(18, 103);
      this.functionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.functionLabel.Name = "functionLabel";
      this.functionLabel.Size = new System.Drawing.Size(75, 20);
      this.functionLabel.TabIndex = 13;
      this.functionLabel.Text = "Function:";
      // 
      // functionTextBox
      // 
      this.functionTextBox.Location = new System.Drawing.Point(140, 98);
      this.functionTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.functionTextBox.Name = "functionTextBox";
      this.functionTextBox.Size = new System.Drawing.Size(434, 26);
      this.functionTextBox.TabIndex = 2;
      this.functionTextBox.TextChanged += new System.EventHandler(this.functionTextBox_TextChanged);
      // 
      // validUntilPicker
      // 
      this.validUntilPicker.Location = new System.Drawing.Point(140, 178);
      this.validUntilPicker.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.validUntilPicker.Name = "validUntilPicker";
      this.validUntilPicker.Size = new System.Drawing.Size(434, 26);
      this.validUntilPicker.TabIndex = 14;
      // 
      // validUntilLabel
      // 
      this.validUntilLabel.AutoSize = true;
      this.validUntilLabel.Location = new System.Drawing.Point(18, 188);
      this.validUntilLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.validUntilLabel.Name = "validUntilLabel";
      this.validUntilLabel.Size = new System.Drawing.Size(81, 20);
      this.validUntilLabel.TabIndex = 15;
      this.validUntilLabel.Text = "Valid until:";
      // 
      // CreateAdminDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(594, 272);
      this.ControlBox = false;
      this.Controls.Add(this.validUntilPicker);
      this.Controls.Add(this.validUntilLabel);
      this.Controls.Add(this.functionLabel);
      this.Controls.Add(this.functionTextBox);
      this.Controls.Add(this.familyNameLabel);
      this.Controls.Add(this.familyNameTextBox);
      this.Controls.Add(this.firstNameLabel);
      this.Controls.Add(this.emailAddressLabel);
      this.Controls.Add(this.emailAddressTextBox);
      this.Controls.Add(this.firstNameTextBox);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "CreateAdminDialog";
      this.Text = "Create Admin Certificate";
      this.Load += new System.EventHandler(this.CaNameDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RefuseDialog_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.TextBox firstNameTextBox;
    private System.Windows.Forms.TextBox emailAddressTextBox;
    private System.Windows.Forms.Label emailAddressLabel;
    private System.Windows.Forms.Label firstNameLabel;
    private System.Windows.Forms.Label familyNameLabel;
    private System.Windows.Forms.TextBox familyNameTextBox;
    private System.Windows.Forms.Label functionLabel;
    private System.Windows.Forms.TextBox functionTextBox;
    private System.Windows.Forms.DateTimePicker validUntilPicker;
    private System.Windows.Forms.Label validUntilLabel;
  }
}