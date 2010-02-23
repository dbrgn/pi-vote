﻿namespace Pirate.PiVote.CaGui
{
  partial class CreateCaDialog
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
      this.nameLabel = new System.Windows.Forms.Label();
      this.nameTextBox = new System.Windows.Forms.TextBox();
      this.cancelButton = new System.Windows.Forms.Button();
      this.okButton = new System.Windows.Forms.Button();
      this.rootCaLabel = new System.Windows.Forms.Label();
      this.rootCaCheckBox = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // nameLabel
      // 
      this.nameLabel.AutoSize = true;
      this.nameLabel.Location = new System.Drawing.Point(32, 23);
      this.nameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Size = new System.Drawing.Size(81, 20);
      this.nameLabel.TabIndex = 0;
      this.nameLabel.Text = "CA Name:";
      // 
      // nameTextBox
      // 
      this.nameTextBox.Location = new System.Drawing.Point(123, 18);
      this.nameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.nameTextBox.Name = "nameTextBox";
      this.nameTextBox.Size = new System.Drawing.Size(314, 26);
      this.nameTextBox.TabIndex = 0;
      this.nameTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
      // 
      // cancelButton
      // 
      this.cancelButton.Location = new System.Drawing.Point(321, 149);
      this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(112, 35);
      this.cancelButton.TabIndex = 2;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Enabled = false;
      this.okButton.Location = new System.Drawing.Point(200, 149);
      this.okButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(112, 35);
      this.okButton.TabIndex = 1;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // rootCaLabel
      // 
      this.rootCaLabel.AutoSize = true;
      this.rootCaLabel.Location = new System.Drawing.Point(32, 75);
      this.rootCaLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.rootCaLabel.Name = "rootCaLabel";
      this.rootCaLabel.Size = new System.Drawing.Size(74, 20);
      this.rootCaLabel.TabIndex = 4;
      this.rootCaLabel.Text = "Root CA:";
      // 
      // rootCaCheckBox
      // 
      this.rootCaCheckBox.AutoSize = true;
      this.rootCaCheckBox.Location = new System.Drawing.Point(123, 74);
      this.rootCaCheckBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.rootCaCheckBox.Name = "rootCaCheckBox";
      this.rootCaCheckBox.Size = new System.Drawing.Size(15, 14);
      this.rootCaCheckBox.TabIndex = 3;
      this.rootCaCheckBox.UseVisualStyleBackColor = true;
      // 
      // CreateCaDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(458, 203);
      this.ControlBox = false;
      this.Controls.Add(this.rootCaLabel);
      this.Controls.Add(this.rootCaCheckBox);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.nameTextBox);
      this.Controls.Add(this.nameLabel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "CreateCaDialog";
      this.Text = "Create Certificate Authority";
      this.Load += new System.EventHandler(this.CaNameDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CreateCaDialog_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label nameLabel;
    private System.Windows.Forms.TextBox nameTextBox;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Label rootCaLabel;
    private System.Windows.Forms.CheckBox rootCaCheckBox;
  }
}