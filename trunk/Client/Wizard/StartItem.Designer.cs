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
      this.frenchRadio = new System.Windows.Forms.RadioButton();
      this.germanRadio = new System.Windows.Forms.RadioButton();
      this.englishRadio = new System.Windows.Forms.RadioButton();
      this.SuspendLayout();
      // 
      // frenchRadio
      // 
      this.frenchRadio.AutoSize = true;
      this.frenchRadio.Location = new System.Drawing.Point(27, 140);
      this.frenchRadio.Margin = new System.Windows.Forms.Padding(4);
      this.frenchRadio.Name = "frenchRadio";
      this.frenchRadio.Size = new System.Drawing.Size(91, 23);
      this.frenchRadio.TabIndex = 2;
      this.frenchRadio.TabStop = true;
      this.frenchRadio.Text = "Français";
      this.frenchRadio.UseVisualStyleBackColor = true;
      this.frenchRadio.CheckedChanged += new System.EventHandler(this.frenchRadio_CheckedChanged);
      // 
      // germanRadio
      // 
      this.germanRadio.AutoSize = true;
      this.germanRadio.Location = new System.Drawing.Point(27, 83);
      this.germanRadio.Margin = new System.Windows.Forms.Padding(4);
      this.germanRadio.Name = "germanRadio";
      this.germanRadio.Size = new System.Drawing.Size(87, 23);
      this.germanRadio.TabIndex = 1;
      this.germanRadio.TabStop = true;
      this.germanRadio.Text = "Deutsch";
      this.germanRadio.UseVisualStyleBackColor = true;
      this.germanRadio.CheckedChanged += new System.EventHandler(this.germanRadio_CheckedChanged);
      // 
      // englishRadio
      // 
      this.englishRadio.AutoSize = true;
      this.englishRadio.Location = new System.Drawing.Point(27, 28);
      this.englishRadio.Margin = new System.Windows.Forms.Padding(4);
      this.englishRadio.Name = "englishRadio";
      this.englishRadio.Size = new System.Drawing.Size(80, 23);
      this.englishRadio.TabIndex = 0;
      this.englishRadio.TabStop = true;
      this.englishRadio.Text = "English";
      this.englishRadio.UseVisualStyleBackColor = true;
      this.englishRadio.CheckedChanged += new System.EventHandler(this.englishRadio_CheckedChanged);
      // 
      // StartItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.frenchRadio);
      this.Controls.Add(this.germanRadio);
      this.Controls.Add(this.englishRadio);
      this.Margin = new System.Windows.Forms.Padding(4);
      this.Name = "StartItem";
      this.Size = new System.Drawing.Size(1050, 730);
      this.Load += new System.EventHandler(this.StartItem_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private RadioButton frenchRadio;
    private RadioButton germanRadio;
    private RadioButton englishRadio;
  }
}
