/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Pirate.PiVote.Client
{
  partial class CheckCertificateItem
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
      this.connectBox = new System.Windows.Forms.TextBox();
      this.getCertificatesBox = new System.Windows.Forms.TextBox();
      this.connectLabel = new System.Windows.Forms.Label();
      this.getCertificatesLabel = new System.Windows.Forms.Label();
      this.checkStatusLabel = new System.Windows.Forms.Label();
      this.checkStatusBox = new System.Windows.Forms.TextBox();
      this.messageLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // connectBox
      // 
      this.connectBox.Enabled = false;
      this.connectBox.Location = new System.Drawing.Point(279, 72);
      this.connectBox.Name = "connectBox";
      this.connectBox.Size = new System.Drawing.Size(20, 20);
      this.connectBox.TabIndex = 0;
      // 
      // getCertificatesBox
      // 
      this.getCertificatesBox.Enabled = false;
      this.getCertificatesBox.Location = new System.Drawing.Point(279, 98);
      this.getCertificatesBox.Name = "getCertificatesBox";
      this.getCertificatesBox.Size = new System.Drawing.Size(20, 20);
      this.getCertificatesBox.TabIndex = 1;
      // 
      // connectLabel
      // 
      this.connectLabel.AutoSize = true;
      this.connectLabel.Location = new System.Drawing.Point(76, 75);
      this.connectLabel.Name = "connectLabel";
      this.connectLabel.Size = new System.Drawing.Size(137, 13);
      this.connectLabel.TabIndex = 2;
      this.connectLabel.Text = "Establish server connection";
      // 
      // getCertificatesLabel
      // 
      this.getCertificatesLabel.AutoSize = true;
      this.getCertificatesLabel.Location = new System.Drawing.Point(76, 101);
      this.getCertificatesLabel.Name = "getCertificatesLabel";
      this.getCertificatesLabel.Size = new System.Drawing.Size(142, 13);
      this.getCertificatesLabel.TabIndex = 3;
      this.getCertificatesLabel.Text = "Download certificate storage";
      // 
      // checkStatusLabel
      // 
      this.checkStatusLabel.AutoSize = true;
      this.checkStatusLabel.Location = new System.Drawing.Point(76, 127);
      this.checkStatusLabel.Name = "checkStatusLabel";
      this.checkStatusLabel.Size = new System.Drawing.Size(118, 13);
      this.checkStatusLabel.TabIndex = 5;
      this.checkStatusLabel.Text = "Check certificate status";
      // 
      // checkStatusBox
      // 
      this.checkStatusBox.Enabled = false;
      this.checkStatusBox.Location = new System.Drawing.Point(279, 124);
      this.checkStatusBox.Name = "checkStatusBox";
      this.checkStatusBox.Size = new System.Drawing.Size(20, 20);
      this.checkStatusBox.TabIndex = 4;
      // 
      // messageLabel
      // 
      this.messageLabel.Location = new System.Drawing.Point(76, 207);
      this.messageLabel.Name = "messageLabel";
      this.messageLabel.Size = new System.Drawing.Size(482, 118);
      this.messageLabel.TabIndex = 6;
      // 
      // CheckCertificateItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.messageLabel);
      this.Controls.Add(this.checkStatusLabel);
      this.Controls.Add(this.checkStatusBox);
      this.Controls.Add(this.getCertificatesLabel);
      this.Controls.Add(this.connectLabel);
      this.Controls.Add(this.getCertificatesBox);
      this.Controls.Add(this.connectBox);
      this.Name = "CheckCertificateItem";
      this.Size = new System.Drawing.Size(700, 500);
      this.Load += new System.EventHandler(this.StartWizardItem_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private TextBox connectBox;
    private TextBox getCertificatesBox;
    private Label connectLabel;
    private Label getCertificatesLabel;
    private Label checkStatusLabel;
    private TextBox checkStatusBox;
    private Label messageLabel;
  }
}
