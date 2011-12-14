/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle.Status
{
  partial class TextStatusDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextStatusDialog));
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.infoLabel = new System.Windows.Forms.Label();
      this.subInfoLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // progressBar
      // 
      this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar.Location = new System.Drawing.Point(12, 59);
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(568, 19);
      this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.progressBar.TabIndex = 0;
      // 
      // infoLabel
      // 
      this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.infoLabel.Location = new System.Drawing.Point(12, 10);
      this.infoLabel.Name = "infoLabel";
      this.infoLabel.Size = new System.Drawing.Size(571, 22);
      this.infoLabel.TabIndex = 1;
      this.infoLabel.Text = "Text";
      // 
      // subInfoLabel
      // 
      this.subInfoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.subInfoLabel.Location = new System.Drawing.Point(12, 31);
      this.subInfoLabel.Name = "subInfoLabel";
      this.subInfoLabel.Size = new System.Drawing.Size(571, 25);
      this.subInfoLabel.TabIndex = 2;
      this.subInfoLabel.Text = "Text";
      // 
      // TextStatusDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(592, 90);
      this.ControlBox = false;
      this.Controls.Add(this.subInfoLabel);
      this.Controls.Add(this.infoLabel);
      this.Controls.Add(this.progressBar);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(600, 117);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(600, 117);
      this.Name = "TextStatusDialog";
      this.ShowInTaskbar = false;
      this.Text = "Status";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextStatusDialog_FormClosing);
      this.Load += new System.EventHandler(this.TextStatusDialog_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label infoLabel;
    private System.Windows.Forms.Label subInfoLabel;
  }
}