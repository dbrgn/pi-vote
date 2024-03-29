﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.CaGui
{
  partial class CaPropertiesDialog
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
      this.okButton = new System.Windows.Forms.Button();
      this.okPanel = new System.Windows.Forms.Panel();
      this.caPanel = new System.Windows.Forms.Panel();
      this.caInfo = new Pirate.PiVote.CaGui.CaInfoControl();
      this.parentsPanel = new System.Windows.Forms.Panel();
      this.okPanel.SuspendLayout();
      this.caPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.Location = new System.Drawing.Point(312, 7);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 1;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // okPanel
      // 
      this.okPanel.Controls.Add(this.okButton);
      this.okPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.okPanel.Location = new System.Drawing.Point(0, 361);
      this.okPanel.Name = "okPanel";
      this.okPanel.Size = new System.Drawing.Size(403, 43);
      this.okPanel.TabIndex = 3;
      // 
      // caPanel
      // 
      this.caPanel.Controls.Add(this.caInfo);
      this.caPanel.Dock = System.Windows.Forms.DockStyle.Top;
      this.caPanel.Location = new System.Drawing.Point(0, 0);
      this.caPanel.Name = "caPanel";
      this.caPanel.Size = new System.Drawing.Size(403, 93);
      this.caPanel.TabIndex = 4;
      // 
      // caInfo
      // 
      this.caInfo.Certificate = null;
      this.caInfo.Font = new System.Drawing.Font("Arial", 8.25F);
      this.caInfo.Location = new System.Drawing.Point(12, 12);
      this.caInfo.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.caInfo.Name = "caInfo";
      this.caInfo.Size = new System.Drawing.Size(375, 67);
      this.caInfo.TabIndex = 2;
      this.caInfo.Title = "Certificate Authority";
      // 
      // parentsPanel
      // 
      this.parentsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.parentsPanel.Location = new System.Drawing.Point(0, 93);
      this.parentsPanel.Name = "parentsPanel";
      this.parentsPanel.Size = new System.Drawing.Size(403, 268);
      this.parentsPanel.TabIndex = 5;
      // 
      // CaPropertiesDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(403, 404);
      this.ControlBox = false;
      this.Controls.Add(this.parentsPanel);
      this.Controls.Add(this.caPanel);
      this.Controls.Add(this.okPanel);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(2000, 431);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(400, 431);
      this.Name = "CaPropertiesDialog";
      this.Text = "Certificate Authoirty Properties";
      this.Load += new System.EventHandler(this.CaNameDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RefuseDialog_KeyDown);
      this.okPanel.ResumeLayout(false);
      this.caPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button okButton;
    private CaInfoControl caInfo;
    private System.Windows.Forms.Panel okPanel;
    private System.Windows.Forms.Panel caPanel;
    private System.Windows.Forms.Panel parentsPanel;
  }
}