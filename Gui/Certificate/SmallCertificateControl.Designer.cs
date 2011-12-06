/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Gui
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
      this.nameTextBox = new System.Windows.Forms.TextBox();
      this.nameLabel = new System.Windows.Forms.Label();
      this.detailButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // nameTextBox
      // 
      this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.nameTextBox.Location = new System.Drawing.Point(65, 0);
      this.nameTextBox.Name = "nameTextBox";
      this.nameTextBox.ReadOnly = true;
      this.nameTextBox.Size = new System.Drawing.Size(458, 20);
      this.nameTextBox.TabIndex = 3;
      // 
      // nameLabel
      // 
      this.nameLabel.AutoSize = true;
      this.nameLabel.Location = new System.Drawing.Point(-3, 4);
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Size = new System.Drawing.Size(37, 14);
      this.nameLabel.TabIndex = 7;
      this.nameLabel.Text = "Name:";
      // 
      // detailButton
      // 
      this.detailButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.detailButton.Location = new System.Drawing.Point(529, 0);
      this.detailButton.Name = "detailButton";
      this.detailButton.Size = new System.Drawing.Size(72, 20);
      this.detailButton.TabIndex = 8;
      this.detailButton.Text = "&Details...";
      this.detailButton.UseVisualStyleBackColor = true;
      this.detailButton.Click += new System.EventHandler(this.detailButton_Click);
      // 
      // SmallCertificateControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.detailButton);
      this.Controls.Add(this.nameLabel);
      this.Controls.Add(this.nameTextBox);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "SmallCertificateControl";
      this.Size = new System.Drawing.Size(601, 21);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox nameTextBox;
    private System.Windows.Forms.Label nameLabel;
    private System.Windows.Forms.Button detailButton;
  }
}
