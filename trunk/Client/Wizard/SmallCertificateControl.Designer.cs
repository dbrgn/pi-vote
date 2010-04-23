﻿namespace Pirate.PiVote.Client
{
  partial class SmallCertificateControl
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
      this.typeTextBox = new System.Windows.Forms.TextBox();
      this.typeLabel = new System.Windows.Forms.Label();
      this.nameTextBox = new System.Windows.Forms.TextBox();
      this.nameLabel = new System.Windows.Forms.Label();
      this.detailButton = new System.Windows.Forms.Button();
      this.statusLabel = new System.Windows.Forms.Label();
      this.statusTextBox = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // typeTextBox
      // 
      this.typeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.typeTextBox.Location = new System.Drawing.Point(99, 0);
      this.typeTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.typeTextBox.Name = "typeTextBox";
      this.typeTextBox.ReadOnly = true;
      this.typeTextBox.Size = new System.Drawing.Size(385, 26);
      this.typeTextBox.TabIndex = 0;
      // 
      // typeLabel
      // 
      this.typeLabel.AutoSize = true;
      this.typeLabel.Location = new System.Drawing.Point(-4, 3);
      this.typeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.typeLabel.Name = "typeLabel";
      this.typeLabel.Size = new System.Drawing.Size(47, 20);
      this.typeLabel.TabIndex = 1;
      this.typeLabel.Text = "Type:";
      // 
      // nameTextBox
      // 
      this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.nameTextBox.Location = new System.Drawing.Point(99, 36);
      this.nameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.nameTextBox.Name = "nameTextBox";
      this.nameTextBox.ReadOnly = true;
      this.nameTextBox.Size = new System.Drawing.Size(802, 26);
      this.nameTextBox.TabIndex = 3;
      // 
      // nameLabel
      // 
      this.nameLabel.AutoSize = true;
      this.nameLabel.Location = new System.Drawing.Point(-4, 39);
      this.nameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Size = new System.Drawing.Size(55, 20);
      this.nameLabel.TabIndex = 7;
      this.nameLabel.Text = "Name:";
      // 
      // detailButton
      // 
      this.detailButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.detailButton.Location = new System.Drawing.Point(793, 0);
      this.detailButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.detailButton.Name = "detailButton";
      this.detailButton.Size = new System.Drawing.Size(108, 26);
      this.detailButton.TabIndex = 8;
      this.detailButton.Text = "&Details...";
      this.detailButton.UseVisualStyleBackColor = true;
      this.detailButton.Click += new System.EventHandler(this.detailButton_Click);
      // 
      // statusLabel
      // 
      this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.statusLabel.AutoSize = true;
      this.statusLabel.Location = new System.Drawing.Point(492, 3);
      this.statusLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.statusLabel.Name = "statusLabel";
      this.statusLabel.Size = new System.Drawing.Size(60, 20);
      this.statusLabel.TabIndex = 9;
      this.statusLabel.Text = "Status:";
      // 
      // statusTextBox
      // 
      this.statusTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.statusTextBox.Location = new System.Drawing.Point(609, 0);
      this.statusTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.statusTextBox.Name = "statusTextBox";
      this.statusTextBox.ReadOnly = true;
      this.statusTextBox.Size = new System.Drawing.Size(176, 26);
      this.statusTextBox.TabIndex = 10;
      // 
      // SmallCertificateControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.statusTextBox);
      this.Controls.Add(this.statusLabel);
      this.Controls.Add(this.detailButton);
      this.Controls.Add(this.nameLabel);
      this.Controls.Add(this.nameTextBox);
      this.Controls.Add(this.typeLabel);
      this.Controls.Add(this.typeTextBox);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "SmallCertificateControl";
      this.Size = new System.Drawing.Size(901, 64);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox typeTextBox;
    private System.Windows.Forms.Label typeLabel;
    private System.Windows.Forms.TextBox nameTextBox;
    private System.Windows.Forms.Label nameLabel;
    private System.Windows.Forms.Button detailButton;
    private System.Windows.Forms.Label statusLabel;
    private System.Windows.Forms.TextBox statusTextBox;
  }
}
