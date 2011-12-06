/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle.VotingList
{
  partial class CertificateStatusControl
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
      this.statusLabel = new System.Windows.Forms.Label();
      this.actionButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // statusLabel
      // 
      this.statusLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.statusLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.statusLabel.Location = new System.Drawing.Point(3, 3);
      this.statusLabel.Name = "statusLabel";
      this.statusLabel.Size = new System.Drawing.Size(491, 25);
      this.statusLabel.TabIndex = 0;
      this.statusLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // actionButton
      // 
      this.actionButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.actionButton.Location = new System.Drawing.Point(500, 4);
      this.actionButton.Name = "actionButton";
      this.actionButton.Size = new System.Drawing.Size(144, 25);
      this.actionButton.TabIndex = 1;
      this.actionButton.UseVisualStyleBackColor = true;
      this.actionButton.Click += new System.EventHandler(this.actionButton_Click);
      // 
      // CertificateStatusControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.actionButton);
      this.Controls.Add(this.statusLabel);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "CertificateStatusControl";
      this.Size = new System.Drawing.Size(647, 32);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label statusLabel;
    private System.Windows.Forms.Button actionButton;
  }
}
