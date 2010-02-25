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
  partial class SetSignatureResponsesItem
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
      this.progressLabel = new System.Windows.Forms.Label();
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.subProgressLabel = new System.Windows.Forms.Label();
      this.openButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // progressLabel
      // 
      this.progressLabel.AutoSize = true;
      this.progressLabel.Location = new System.Drawing.Point(112, 189);
      this.progressLabel.Name = "progressLabel";
      this.progressLabel.Size = new System.Drawing.Size(0, 20);
      this.progressLabel.TabIndex = 0;
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(116, 212);
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(801, 23);
      this.progressBar.TabIndex = 1;
      // 
      // subProgressLabel
      // 
      this.subProgressLabel.AutoSize = true;
      this.subProgressLabel.Location = new System.Drawing.Point(112, 254);
      this.subProgressLabel.Name = "subProgressLabel";
      this.subProgressLabel.Size = new System.Drawing.Size(0, 20);
      this.subProgressLabel.TabIndex = 3;
      // 
      // openButton
      // 
      this.openButton.Location = new System.Drawing.Point(116, 118);
      this.openButton.Name = "openButton";
      this.openButton.Size = new System.Drawing.Size(174, 31);
      this.openButton.TabIndex = 4;
      this.openButton.Text = "Open...";
      this.openButton.UseVisualStyleBackColor = true;
      this.openButton.Click += new System.EventHandler(this.openButton_Click);
      // 
      // SetSignatureResponsesItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.openButton);
      this.Controls.Add(this.subProgressLabel);
      this.Controls.Add(this.progressBar);
      this.Controls.Add(this.progressLabel);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "SetSignatureResponsesItem";
      this.Size = new System.Drawing.Size(1050, 769);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Label progressLabel;
    private ProgressBar progressBar;
    private Label subProgressLabel;
    private Button openButton;




  }
}
