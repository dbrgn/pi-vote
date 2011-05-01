/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
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

namespace Pirate.PiVote.Circle.CreateVoting
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
      this.urlTextBox = new Pirate.PiVote.MultiLanguageTextBox();
      this.urlLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // textLabel
      // 
      this.textLabel.AutoSize = true;
      this.textLabel.Location = new System.Drawing.Point(8, 11);
      this.textLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.textLabel.Name = "textLabel";
      this.textLabel.Size = new System.Drawing.Size(30, 14);
      this.textLabel.TabIndex = 0;
      this.textLabel.Text = "Text:";
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
      this.textTextBox.Size = new System.Drawing.Size(527, 20);
      this.textTextBox.TabIndex = 0;
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
      this.descriptionTextBox.Size = new System.Drawing.Size(527, 169);
      this.descriptionTextBox.TabIndex = 1;
      // 
      // descriptionLabel
      // 
      this.descriptionLabel.AutoSize = true;
      this.descriptionLabel.Location = new System.Drawing.Point(8, 35);
      this.descriptionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.descriptionLabel.Name = "descriptionLabel";
      this.descriptionLabel.Size = new System.Drawing.Size(64, 14);
      this.descriptionLabel.TabIndex = 3;
      this.descriptionLabel.Text = "Description:";
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(554, 231);
      this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(80, 23);
      this.cancelButton.TabIndex = 4;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.Enabled = false;
      this.okButton.Location = new System.Drawing.Point(470, 231);
      this.okButton.Margin = new System.Windows.Forms.Padding(2);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(80, 23);
      this.okButton.TabIndex = 3;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // urlTextBox
      // 
      this.urlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.urlTextBox.Location = new System.Drawing.Point(107, 205);
      this.urlTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.urlTextBox.Multiline = false;
      this.urlTextBox.Name = "urlTextBox";
      this.urlTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.urlTextBox.Size = new System.Drawing.Size(527, 22);
      this.urlTextBox.TabIndex = 5;
      // 
      // urlLabel
      // 
      this.urlLabel.AutoSize = true;
      this.urlLabel.Location = new System.Drawing.Point(8, 208);
      this.urlLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.urlLabel.Name = "urlLabel";
      this.urlLabel.Size = new System.Drawing.Size(23, 14);
      this.urlLabel.TabIndex = 6;
      this.urlLabel.Text = "Url:";
      // 
      // AddOptionDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(645, 265);
      this.ControlBox = false;
      this.Controls.Add(this.urlLabel);
      this.Controls.Add(this.urlTextBox);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.descriptionLabel);
      this.Controls.Add(this.descriptionTextBox);
      this.Controls.Add(this.textTextBox);
      this.Controls.Add(this.textLabel);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(2);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(400, 150);
      this.Name = "AddOptionDialog";
      this.Text = "AddOptionDialog";
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
    private MultiLanguageTextBox urlTextBox;
    private Label urlLabel;
  }
}