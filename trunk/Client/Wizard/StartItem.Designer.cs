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
      this.languageGroup = new System.Windows.Forms.GroupBox();
      this.frenchRadio = new System.Windows.Forms.RadioButton();
      this.germanRadio = new System.Windows.Forms.RadioButton();
      this.englishRadio = new System.Windows.Forms.RadioButton();
      this.certificateGroup = new System.Windows.Forms.GroupBox();
      this.languageGroup.SuspendLayout();
      this.certificateGroup.SuspendLayout();
      this.SuspendLayout();
      // 
      // haveCertificateRadio
      // 
      this.haveCertificateRadio.AutoSize = true;
      this.haveCertificateRadio.Location = new System.Drawing.Point(28, 36);
      this.haveCertificateRadio.Name = "haveCertificateRadio";
      this.haveCertificateRadio.Size = new System.Drawing.Size(119, 18);
      this.haveCertificateRadio.TabIndex = 0;
      this.haveCertificateRadio.TabStop = true;
      this.haveCertificateRadio.Text = "I\'ve got a certificate";
      this.haveCertificateRadio.UseVisualStyleBackColor = true;
      this.haveCertificateRadio.CheckedChanged += new System.EventHandler(this.haveCertificateRadio_CheckedChanged);
      // 
      // needCertificateRadio
      // 
      this.needCertificateRadio.AutoSize = true;
      this.needCertificateRadio.Location = new System.Drawing.Point(28, 114);
      this.needCertificateRadio.Name = "needCertificateRadio";
      this.needCertificateRadio.Size = new System.Drawing.Size(139, 18);
      this.needCertificateRadio.TabIndex = 1;
      this.needCertificateRadio.TabStop = true;
      this.needCertificateRadio.Text = "I need a new certificate";
      this.needCertificateRadio.UseVisualStyleBackColor = true;
      this.needCertificateRadio.CheckedChanged += new System.EventHandler(this.needCertificateRadio_CheckedChanged);
      // 
      // languageGroup
      // 
      this.languageGroup.Controls.Add(this.frenchRadio);
      this.languageGroup.Controls.Add(this.germanRadio);
      this.languageGroup.Controls.Add(this.englishRadio);
      this.languageGroup.Location = new System.Drawing.Point(45, 46);
      this.languageGroup.Name = "languageGroup";
      this.languageGroup.Size = new System.Drawing.Size(340, 186);
      this.languageGroup.TabIndex = 2;
      this.languageGroup.TabStop = false;
      this.languageGroup.Text = "Language / Sprache";
      // 
      // frenchRadio
      // 
      this.frenchRadio.AutoSize = true;
      this.frenchRadio.Location = new System.Drawing.Point(28, 122);
      this.frenchRadio.Name = "frenchRadio";
      this.frenchRadio.Size = new System.Drawing.Size(67, 18);
      this.frenchRadio.TabIndex = 2;
      this.frenchRadio.TabStop = true;
      this.frenchRadio.Text = "Français";
      this.frenchRadio.UseVisualStyleBackColor = true;
      this.frenchRadio.CheckedChanged += new System.EventHandler(this.frenchRadio_CheckedChanged);
      // 
      // germanRadio
      // 
      this.germanRadio.AutoSize = true;
      this.germanRadio.Location = new System.Drawing.Point(28, 80);
      this.germanRadio.Name = "germanRadio";
      this.germanRadio.Size = new System.Drawing.Size(65, 18);
      this.germanRadio.TabIndex = 1;
      this.germanRadio.TabStop = true;
      this.germanRadio.Text = "Deutsch";
      this.germanRadio.UseVisualStyleBackColor = true;
      this.germanRadio.CheckedChanged += new System.EventHandler(this.germanRadio_CheckedChanged);
      // 
      // englishRadio
      // 
      this.englishRadio.AutoSize = true;
      this.englishRadio.Location = new System.Drawing.Point(28, 39);
      this.englishRadio.Name = "englishRadio";
      this.englishRadio.Size = new System.Drawing.Size(59, 18);
      this.englishRadio.TabIndex = 0;
      this.englishRadio.TabStop = true;
      this.englishRadio.Text = "English";
      this.englishRadio.UseVisualStyleBackColor = true;
      this.englishRadio.CheckedChanged += new System.EventHandler(this.englishRadio_CheckedChanged);
      // 
      // certificateGroup
      // 
      this.certificateGroup.Controls.Add(this.haveCertificateRadio);
      this.certificateGroup.Controls.Add(this.needCertificateRadio);
      this.certificateGroup.Location = new System.Drawing.Point(45, 239);
      this.certificateGroup.Name = "certificateGroup";
      this.certificateGroup.Size = new System.Drawing.Size(340, 174);
      this.certificateGroup.TabIndex = 3;
      this.certificateGroup.TabStop = false;
      this.certificateGroup.Text = "Certificate";
      // 
      // StartItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.certificateGroup);
      this.Controls.Add(this.languageGroup);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "StartItem";
      this.Size = new System.Drawing.Size(700, 538);
      this.Load += new System.EventHandler(this.StartItem_Load);
      this.languageGroup.ResumeLayout(false);
      this.languageGroup.PerformLayout();
      this.certificateGroup.ResumeLayout(false);
      this.certificateGroup.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private RadioButton haveCertificateRadio;
    private RadioButton needCertificateRadio;
    private GroupBox languageGroup;
    private GroupBox certificateGroup;
    private RadioButton frenchRadio;
    private RadioButton germanRadio;
    private RadioButton englishRadio;
  }
}
