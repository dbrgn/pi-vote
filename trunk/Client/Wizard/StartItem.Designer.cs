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
      this.tileImage = new System.Windows.Forms.PictureBox();
      this.frenchRadio = new System.Windows.Forms.RadioButton();
      this.englishRadio = new System.Windows.Forms.RadioButton();
      this.germanRadio = new System.Windows.Forms.RadioButton();
      this.urlLink = new System.Windows.Forms.LinkLabel();
      this.welcomeLabel = new System.Windows.Forms.Label();
      this.titlelLabel = new System.Windows.Forms.Label();
      this.languagePanel = new System.Windows.Forms.Panel();
      this.optionPanel = new System.Windows.Forms.Panel();
      this.tallyOnlyRadio = new System.Windows.Forms.RadioButton();
      this.votingRadio = new System.Windows.Forms.RadioButton();
      this.advancedOptionsRadio = new System.Windows.Forms.RadioButton();
      this.selectPanel = new System.Windows.Forms.Panel();
      ((System.ComponentModel.ISupportInitialize)(this.tileImage)).BeginInit();
      this.languagePanel.SuspendLayout();
      this.optionPanel.SuspendLayout();
      this.selectPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // tileImage
      // 
      this.tileImage.Location = new System.Drawing.Point(0, 0);
      this.tileImage.Name = "tileImage";
      this.tileImage.Size = new System.Drawing.Size(200, 200);
      this.tileImage.TabIndex = 11;
      this.tileImage.TabStop = false;
      // 
      // frenchRadio
      // 
      this.frenchRadio.AutoSize = true;
      this.frenchRadio.Location = new System.Drawing.Point(3, 51);
      this.frenchRadio.Name = "frenchRadio";
      this.frenchRadio.Size = new System.Drawing.Size(67, 18);
      this.frenchRadio.TabIndex = 2;
      this.frenchRadio.TabStop = true;
      this.frenchRadio.Text = "Français";
      this.frenchRadio.UseVisualStyleBackColor = true;
      this.frenchRadio.CheckedChanged += new System.EventHandler(this.frenchRadio_CheckedChanged);
      // 
      // englishRadio
      // 
      this.englishRadio.AutoSize = true;
      this.englishRadio.Location = new System.Drawing.Point(3, 3);
      this.englishRadio.Name = "englishRadio";
      this.englishRadio.Size = new System.Drawing.Size(59, 18);
      this.englishRadio.TabIndex = 0;
      this.englishRadio.TabStop = true;
      this.englishRadio.Text = "English";
      this.englishRadio.UseVisualStyleBackColor = true;
      this.englishRadio.CheckedChanged += new System.EventHandler(this.englishRadio_CheckedChanged);
      // 
      // germanRadio
      // 
      this.germanRadio.AutoSize = true;
      this.germanRadio.Location = new System.Drawing.Point(3, 27);
      this.germanRadio.Name = "germanRadio";
      this.germanRadio.Size = new System.Drawing.Size(65, 18);
      this.germanRadio.TabIndex = 1;
      this.germanRadio.TabStop = true;
      this.germanRadio.Text = "Deutsch";
      this.germanRadio.UseVisualStyleBackColor = true;
      this.germanRadio.CheckedChanged += new System.EventHandler(this.germanRadio_CheckedChanged);
      // 
      // urlLink
      // 
      this.urlLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.urlLink.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.urlLink.Location = new System.Drawing.Point(206, 173);
      this.urlLink.Name = "urlLink";
      this.urlLink.Size = new System.Drawing.Size(294, 27);
      this.urlLink.TabIndex = 7;
      this.urlLink.TabStop = true;
      this.urlLink.Text = "url";
      this.urlLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.alphaBugLink_LinkClicked);
      // 
      // welcomeLabel
      // 
      this.welcomeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.welcomeLabel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.welcomeLabel.ForeColor = System.Drawing.SystemColors.ControlText;
      this.welcomeLabel.Location = new System.Drawing.Point(206, 35);
      this.welcomeLabel.Name = "welcomeLabel";
      this.welcomeLabel.Size = new System.Drawing.Size(288, 138);
      this.welcomeLabel.TabIndex = 5;
      this.welcomeLabel.Text = "Welcome Message";
      // 
      // titlelLabel
      // 
      this.titlelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.titlelLabel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.titlelLabel.Location = new System.Drawing.Point(206, 3);
      this.titlelLabel.Name = "titlelLabel";
      this.titlelLabel.Size = new System.Drawing.Size(288, 32);
      this.titlelLabel.TabIndex = 4;
      this.titlelLabel.Text = "System Name";
      // 
      // languagePanel
      // 
      this.languagePanel.Controls.Add(this.frenchRadio);
      this.languagePanel.Controls.Add(this.germanRadio);
      this.languagePanel.Controls.Add(this.englishRadio);
      this.languagePanel.Location = new System.Drawing.Point(0, 0);
      this.languagePanel.Name = "languagePanel";
      this.languagePanel.Size = new System.Drawing.Size(150, 76);
      this.languagePanel.TabIndex = 12;
      // 
      // optionPanel
      // 
      this.optionPanel.Controls.Add(this.tallyOnlyRadio);
      this.optionPanel.Controls.Add(this.votingRadio);
      this.optionPanel.Controls.Add(this.advancedOptionsRadio);
      this.optionPanel.Location = new System.Drawing.Point(200, 0);
      this.optionPanel.Name = "optionPanel";
      this.optionPanel.Size = new System.Drawing.Size(150, 76);
      this.optionPanel.TabIndex = 13;
      // 
      // tallyOnlyRadio
      // 
      this.tallyOnlyRadio.AutoSize = true;
      this.tallyOnlyRadio.Location = new System.Drawing.Point(4, 51);
      this.tallyOnlyRadio.Name = "tallyOnlyRadio";
      this.tallyOnlyRadio.Size = new System.Drawing.Size(69, 18);
      this.tallyOnlyRadio.TabIndex = 5;
      this.tallyOnlyRadio.TabStop = true;
      this.tallyOnlyRadio.Text = "Tally only";
      this.tallyOnlyRadio.UseVisualStyleBackColor = true;
      // 
      // votingRadio
      // 
      this.votingRadio.AutoSize = true;
      this.votingRadio.Location = new System.Drawing.Point(4, 3);
      this.votingRadio.Name = "votingRadio";
      this.votingRadio.Size = new System.Drawing.Size(55, 18);
      this.votingRadio.TabIndex = 3;
      this.votingRadio.TabStop = true;
      this.votingRadio.Text = "Voting";
      this.votingRadio.UseVisualStyleBackColor = true;
      // 
      // advancedOptionsRadio
      // 
      this.advancedOptionsRadio.AutoSize = true;
      this.advancedOptionsRadio.Location = new System.Drawing.Point(4, 27);
      this.advancedOptionsRadio.Name = "advancedOptionsRadio";
      this.advancedOptionsRadio.Size = new System.Drawing.Size(115, 18);
      this.advancedOptionsRadio.TabIndex = 4;
      this.advancedOptionsRadio.TabStop = true;
      this.advancedOptionsRadio.Text = "Advanced Options";
      this.advancedOptionsRadio.UseVisualStyleBackColor = true;
      // 
      // selectPanel
      // 
      this.selectPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.selectPanel.Controls.Add(this.languagePanel);
      this.selectPanel.Controls.Add(this.optionPanel);
      this.selectPanel.Location = new System.Drawing.Point(3, 203);
      this.selectPanel.Name = "selectPanel";
      this.selectPanel.Size = new System.Drawing.Size(491, 82);
      this.selectPanel.TabIndex = 14;
      this.selectPanel.Resize += new System.EventHandler(this.selectPanel_Resize);
      // 
      // StartItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.selectPanel);
      this.Controls.Add(this.tileImage);
      this.Controls.Add(this.urlLink);
      this.Controls.Add(this.welcomeLabel);
      this.Controls.Add(this.titlelLabel);
      this.Margin = new System.Windows.Forms.Padding(3);
      this.Name = "StartItem";
      this.Size = new System.Drawing.Size(500, 288);
      this.Load += new System.EventHandler(this.StartItem_Load);
      ((System.ComponentModel.ISupportInitialize)(this.tileImage)).EndInit();
      this.languagePanel.ResumeLayout(false);
      this.languagePanel.PerformLayout();
      this.optionPanel.ResumeLayout(false);
      this.optionPanel.PerformLayout();
      this.selectPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private RadioButton frenchRadio;
    private RadioButton germanRadio;
    private RadioButton englishRadio;
    private Label titlelLabel;
    private Label welcomeLabel;
    private LinkLabel urlLink;
    private PictureBox tileImage;
    private Panel languagePanel;
    private Panel optionPanel;
    private RadioButton tallyOnlyRadio;
    private RadioButton votingRadio;
    private RadioButton advancedOptionsRadio;
    private Panel selectPanel;
  }
}
