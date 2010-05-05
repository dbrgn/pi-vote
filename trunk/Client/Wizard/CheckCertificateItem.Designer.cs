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
      this.spaceLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // spaceLabel
      // 
      this.spaceLabel.AutoSize = true;
      this.spaceLabel.ForeColor = System.Drawing.Color.Red;
      this.spaceLabel.Location = new System.Drawing.Point(95, 161);
      this.spaceLabel.Name = "spaceLabel";
      this.spaceLabel.Size = new System.Drawing.Size(274, 14);
      this.spaceLabel.TabIndex = 0;
      this.spaceLabel.Text = "This space shall be occupied by an appropriate picture.";
      // 
      // CheckCertificateItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.spaceLabel);
      this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
      this.Name = "CheckCertificateItem";
      this.Size = new System.Drawing.Size(467, 359);
      this.Load += new System.EventHandler(this.StartWizardItem_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Label spaceLabel;

  }
}
