﻿/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Client
{
  partial class AddOptionDialog
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
      this.textLabel = new System.Windows.Forms.Label();
      this.textTextBox = new System.Windows.Forms.TextBox();
      this.descriptionTextBox = new System.Windows.Forms.TextBox();
      this.descriptionLabel = new System.Windows.Forms.Label();
      this.cancelButton = new System.Windows.Forms.Button();
      this.okButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // textLabel
      // 
      this.textLabel.AutoSize = true;
      this.textLabel.Location = new System.Drawing.Point(12, 15);
      this.textLabel.Name = "textLabel";
      this.textLabel.Size = new System.Drawing.Size(35, 13);
      this.textLabel.TabIndex = 0;
      this.textLabel.Text = "label1";
      // 
      // textTextBox
      // 
      this.textTextBox.Location = new System.Drawing.Point(109, 12);
      this.textTextBox.Name = "textTextBox";
      this.textTextBox.Size = new System.Drawing.Size(323, 20);
      this.textTextBox.TabIndex = 1;
      this.textTextBox.TextChanged += new System.EventHandler(this.textTextBox_TextChanged);
      // 
      // descriptionTextBox
      // 
      this.descriptionTextBox.Location = new System.Drawing.Point(109, 38);
      this.descriptionTextBox.Multiline = true;
      this.descriptionTextBox.Name = "descriptionTextBox";
      this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.descriptionTextBox.Size = new System.Drawing.Size(323, 48);
      this.descriptionTextBox.TabIndex = 2;
      // 
      // descriptionLabel
      // 
      this.descriptionLabel.AutoSize = true;
      this.descriptionLabel.Location = new System.Drawing.Point(12, 41);
      this.descriptionLabel.Name = "descriptionLabel";
      this.descriptionLabel.Size = new System.Drawing.Size(35, 13);
      this.descriptionLabel.TabIndex = 3;
      this.descriptionLabel.Text = "label2";
      // 
      // cancelButton
      // 
      this.cancelButton.Location = new System.Drawing.Point(341, 92);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(91, 23);
      this.cancelButton.TabIndex = 4;
      this.cancelButton.Text = "button1";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Enabled = false;
      this.okButton.Location = new System.Drawing.Point(244, 92);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(91, 23);
      this.okButton.TabIndex = 5;
      this.okButton.Text = "button2";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // AddOptionDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(444, 126);
      this.ControlBox = false;
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.descriptionLabel);
      this.Controls.Add(this.descriptionTextBox);
      this.Controls.Add(this.textTextBox);
      this.Controls.Add(this.textLabel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "AddOptionDialog";
      this.Text = "AddOptionDialog";
      this.Load += new System.EventHandler(this.AddOptionDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.AddOptionDialog_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label textLabel;
    private System.Windows.Forms.TextBox textTextBox;
    private System.Windows.Forms.TextBox descriptionTextBox;
    private System.Windows.Forms.Label descriptionLabel;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
  }
}