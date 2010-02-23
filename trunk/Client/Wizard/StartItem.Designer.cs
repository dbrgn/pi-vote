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
  partial class StartItem
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
      this.haveCertificateRadio = new System.Windows.Forms.RadioButton();
      this.needCertificateRadio = new System.Windows.Forms.RadioButton();
      this.SuspendLayout();
      // 
      // haveCertificateRadio
      // 
      this.haveCertificateRadio.AutoSize = true;
      this.haveCertificateRadio.Location = new System.Drawing.Point(61, 67);
      this.haveCertificateRadio.Name = "haveCertificateRadio";
      this.haveCertificateRadio.Size = new System.Drawing.Size(118, 17);
      this.haveCertificateRadio.TabIndex = 0;
      this.haveCertificateRadio.TabStop = true;
      this.haveCertificateRadio.Text = "I\'ve got a certificate";
      this.haveCertificateRadio.UseVisualStyleBackColor = true;
      this.haveCertificateRadio.CheckedChanged += new System.EventHandler(this.haveCertificateRadio_CheckedChanged);
      // 
      // needCertificateRadio
      // 
      this.needCertificateRadio.AutoSize = true;
      this.needCertificateRadio.Location = new System.Drawing.Point(61, 140);
      this.needCertificateRadio.Name = "needCertificateRadio";
      this.needCertificateRadio.Size = new System.Drawing.Size(136, 17);
      this.needCertificateRadio.TabIndex = 1;
      this.needCertificateRadio.TabStop = true;
      this.needCertificateRadio.Text = "I need a new certificate";
      this.needCertificateRadio.UseVisualStyleBackColor = true;
      this.needCertificateRadio.CheckedChanged += new System.EventHandler(this.needCertificateRadio_CheckedChanged);
      // 
      // StartItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.needCertificateRadio);
      this.Controls.Add(this.haveCertificateRadio);
      this.Name = "StartItem";
      this.Size = new System.Drawing.Size(700, 500);
      this.Load += new System.EventHandler(this.StartItem_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private RadioButton haveCertificateRadio;
    private RadioButton needCertificateRadio;
  }
}
