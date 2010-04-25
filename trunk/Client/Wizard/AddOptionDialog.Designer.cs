/*
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
      this.textTextBox = new Pirate.PiVote.MultiLanguageTextBox();
      this.descriptionTextBox = new Pirate.PiVote.MultiLanguageTextBox();
      this.descriptionLabel = new System.Windows.Forms.Label();
      this.cancelButton = new System.Windows.Forms.Button();
      this.okButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // textLabel
      // 
      this.textLabel.AutoSize = true;
      this.textLabel.Location = new System.Drawing.Point(8, 11);
      this.textLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.textLabel.Name = "textLabel";
      this.textLabel.Size = new System.Drawing.Size(35, 14);
      this.textLabel.TabIndex = 0;
      this.textLabel.Text = "label1";
      // 
      // textTextBox
      // 
      this.textTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textTextBox.Location = new System.Drawing.Point(107, 8);
      this.textTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.textTextBox.Multiline = false;
      this.textTextBox.Name = "textTextBox";
      this.textTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.textTextBox.Size = new System.Drawing.Size(333, 20);
      this.textTextBox.TabIndex = 1;
      this.textTextBox.TextChanged += new System.EventHandler(this.textTextBox_TextChanged);
      // 
      // descriptionTextBox
      // 
      this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.descriptionTextBox.Location = new System.Drawing.Point(107, 32);
      this.descriptionTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.descriptionTextBox.Multiline = true;
      this.descriptionTextBox.Name = "descriptionTextBox";
      this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.descriptionTextBox.Size = new System.Drawing.Size(333, 68);
      this.descriptionTextBox.TabIndex = 2;
      // 
      // descriptionLabel
      // 
      this.descriptionLabel.AutoSize = true;
      this.descriptionLabel.Location = new System.Drawing.Point(8, 32);
      this.descriptionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.descriptionLabel.Name = "descriptionLabel";
      this.descriptionLabel.Size = new System.Drawing.Size(35, 14);
      this.descriptionLabel.TabIndex = 3;
      this.descriptionLabel.Text = "label2";
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(360, 104);
      this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(80, 23);
      this.cancelButton.TabIndex = 4;
      this.cancelButton.Text = "button1";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.Enabled = false;
      this.okButton.Location = new System.Drawing.Point(276, 104);
      this.okButton.Margin = new System.Windows.Forms.Padding(2);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(80, 23);
      this.okButton.TabIndex = 5;
      this.okButton.Text = "button2";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // AddOptionDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(451, 138);
      this.ControlBox = false;
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.descriptionLabel);
      this.Controls.Add(this.descriptionTextBox);
      this.Controls.Add(this.textTextBox);
      this.Controls.Add(this.textLabel);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(2);
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
    private MultiLanguageTextBox textTextBox;
    private MultiLanguageTextBox descriptionTextBox;
    private System.Windows.Forms.Label descriptionLabel;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
  }
}