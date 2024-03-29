﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Pirate.PiVote.Client
{
  partial class Wizard
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Wizard));
      this.nextPanel = new System.Windows.Forms.Panel();
      this.previouseButton = new System.Windows.Forms.Button();
      this.nextButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.itemPanel = new System.Windows.Forms.Panel();
      this.progress1 = new Pirate.PiVote.Client.Progress();
      this.message1 = new Pirate.PiVote.Client.Message();
      this.nextPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // nextPanel
      // 
      this.nextPanel.Controls.Add(this.progress1);
      this.nextPanel.Controls.Add(this.message1);
      this.nextPanel.Controls.Add(this.previouseButton);
      this.nextPanel.Controls.Add(this.nextButton);
      this.nextPanel.Controls.Add(this.cancelButton);
      this.nextPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.nextPanel.Location = new System.Drawing.Point(0, 296);
      this.nextPanel.Margin = new System.Windows.Forms.Padding(2);
      this.nextPanel.Name = "nextPanel";
      this.nextPanel.Size = new System.Drawing.Size(528, 105);
      this.nextPanel.TabIndex = 0;
      // 
      // previouseButton
      // 
      this.previouseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.previouseButton.Location = new System.Drawing.Point(268, 74);
      this.previouseButton.Margin = new System.Windows.Forms.Padding(2);
      this.previouseButton.Name = "previouseButton";
      this.previouseButton.Size = new System.Drawing.Size(81, 23);
      this.previouseButton.TabIndex = 2;
      this.previouseButton.Text = "&Previous";
      this.previouseButton.UseVisualStyleBackColor = true;
      this.previouseButton.Click += new System.EventHandler(this.previouseButton_Click);
      // 
      // nextButton
      // 
      this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.nextButton.Location = new System.Drawing.Point(353, 74);
      this.nextButton.Margin = new System.Windows.Forms.Padding(2);
      this.nextButton.Name = "nextButton";
      this.nextButton.Size = new System.Drawing.Size(81, 23);
      this.nextButton.TabIndex = 1;
      this.nextButton.Text = "&Next";
      this.nextButton.UseVisualStyleBackColor = true;
      this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(438, 74);
      this.cancelButton.Margin = new System.Windows.Forms.Padding(2);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(81, 23);
      this.cancelButton.TabIndex = 0;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // itemPanel
      // 
      this.itemPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.itemPanel.Location = new System.Drawing.Point(0, 0);
      this.itemPanel.Margin = new System.Windows.Forms.Padding(2);
      this.itemPanel.Name = "itemPanel";
      this.itemPanel.Size = new System.Drawing.Size(528, 296);
      this.itemPanel.TabIndex = 1;
      this.itemPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.itemPanel_Paint);
      // 
      // progress1
      // 
      this.progress1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.progress1.Font = new System.Drawing.Font("Arial", 8.25F);
      this.progress1.Location = new System.Drawing.Point(8, 4);
      this.progress1.Margin = new System.Windows.Forms.Padding(1);
      this.progress1.Name = "progress1";
      this.progress1.Size = new System.Drawing.Size(512, 63);
      this.progress1.Status = null;
      this.progress1.TabIndex = 4;
      // 
      // message1
      // 
      this.message1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.message1.Font = new System.Drawing.Font("Arial", 8.25F);
      this.message1.Location = new System.Drawing.Point(8, 4);
      this.message1.Margin = new System.Windows.Forms.Padding(1);
      this.message1.Name = "message1";
      this.message1.Size = new System.Drawing.Size(512, 63);
      this.message1.TabIndex = 3;
      // 
      // Wizard
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(528, 401);
      this.ControlBox = false;
      this.Controls.Add(this.itemPanel);
      this.Controls.Add(this.nextPanel);
      this.Font = new System.Drawing.Font("Arial", 8.25F);

      // This hack is necessary because the mono compiler/runtime seems to be broken when it comes to icons.
#if !__MonoCS__
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
#endif

      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(2);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(536, 409);
      this.Name = "Wizard";
      this.Text = "Pi-Vote";
      this.Load += new System.EventHandler(this.Wizard_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Wizard_KeyDown);
      this.nextPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private Panel nextPanel;
    private Button previouseButton;
    private Button nextButton;
    private Button cancelButton;
    private Panel itemPanel;
    private Message message1;
    private Progress progress1;
  }
}