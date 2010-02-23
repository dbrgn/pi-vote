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
      this.nextPanel = new System.Windows.Forms.Panel();
      this.itemPanel = new System.Windows.Forms.Panel();
      this.cancelButton = new System.Windows.Forms.Button();
      this.nextButton = new System.Windows.Forms.Button();
      this.previouseButton = new System.Windows.Forms.Button();
      this.nextPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // nextPanel
      // 
      this.nextPanel.Controls.Add(this.previouseButton);
      this.nextPanel.Controls.Add(this.nextButton);
      this.nextPanel.Controls.Add(this.cancelButton);
      this.nextPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
      this.nextPanel.Location = new System.Drawing.Point(0, 513);
      this.nextPanel.Name = "nextPanel";
      this.nextPanel.Size = new System.Drawing.Size(732, 60);
      this.nextPanel.TabIndex = 0;
      // 
      // itemPanel
      // 
      this.itemPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.itemPanel.Location = new System.Drawing.Point(0, 0);
      this.itemPanel.Name = "itemPanel";
      this.itemPanel.Size = new System.Drawing.Size(732, 513);
      this.itemPanel.TabIndex = 1;
      this.itemPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.itemPanel_Paint);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(598, 16);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(122, 32);
      this.cancelButton.TabIndex = 0;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // nextButton
      // 
      this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.nextButton.Location = new System.Drawing.Point(470, 16);
      this.nextButton.Name = "nextButton";
      this.nextButton.Size = new System.Drawing.Size(122, 32);
      this.nextButton.TabIndex = 1;
      this.nextButton.Text = "&Next";
      this.nextButton.UseVisualStyleBackColor = true;
      this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
      // 
      // previouseButton
      // 
      this.previouseButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.previouseButton.Location = new System.Drawing.Point(342, 16);
      this.previouseButton.Name = "previouseButton";
      this.previouseButton.Size = new System.Drawing.Size(122, 32);
      this.previouseButton.TabIndex = 2;
      this.previouseButton.Text = "&Previous";
      this.previouseButton.UseVisualStyleBackColor = true;
      this.previouseButton.Click += new System.EventHandler(this.previouseButton_Click);
      // 
      // Wizard
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(732, 573);
      this.ControlBox = false;
      this.Controls.Add(this.itemPanel);
      this.Controls.Add(this.nextPanel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "Wizard";
      this.Text = "Pi-Vote";
      this.Load += new System.EventHandler(this.Wizard_Load);
      this.nextPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private Panel nextPanel;
    private Button previouseButton;
    private Button nextButton;
    private Button cancelButton;
    private Panel itemPanel;
  }
}