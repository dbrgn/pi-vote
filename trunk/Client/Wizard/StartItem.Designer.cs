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
      this.titlelLabel = new System.Windows.Forms.Label();
      this.welcomeLabel = new System.Windows.Forms.Label();
      this.urlLink = new System.Windows.Forms.LinkLabel();
      this.languageGroupBox = new System.Windows.Forms.GroupBox();
      this.optionGroupBox = new System.Windows.Forms.GroupBox();
      this.tallyOnlyRadio = new System.Windows.Forms.RadioButton();
      this.votingRadio = new System.Windows.Forms.RadioButton();
      this.advancedOptionsRadio = new System.Windows.Forms.RadioButton();
      this.tileImage = new System.Windows.Forms.PictureBox();
      this.languageGroupBox.SuspendLayout();
      this.optionGroupBox.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tileImage)).BeginInit();
      this.SuspendLayout();
      // 
      // frenchRadio
      // 
      this.frenchRadio.AutoSize = true;
      this.frenchRadio.Location = new System.Drawing.Point(6, 93);
      this.frenchRadio.Name = "frenchRadio";
      this.frenchRadio.Size = new System.Drawing.Size(65, 17);
      this.frenchRadio.TabIndex = 2;
      this.frenchRadio.TabStop = true;
      this.frenchRadio.Text = "Français";
      this.frenchRadio.UseVisualStyleBackColor = true;
      this.frenchRadio.CheckedChanged += new System.EventHandler(this.frenchRadio_CheckedChanged);
      // 
      // germanRadio
      // 
      this.germanRadio.AutoSize = true;
      this.germanRadio.Location = new System.Drawing.Point(6, 55);
      this.germanRadio.Name = "germanRadio";
      this.germanRadio.Size = new System.Drawing.Size(65, 17);
      this.germanRadio.TabIndex = 1;
      this.germanRadio.TabStop = true;
      this.germanRadio.Text = "Deutsch";
      this.germanRadio.UseVisualStyleBackColor = true;
      this.germanRadio.CheckedChanged += new System.EventHandler(this.germanRadio_CheckedChanged);
      // 
      // englishRadio
      // 
      this.englishRadio.AutoSize = true;
      this.englishRadio.Location = new System.Drawing.Point(6, 19);
      this.englishRadio.Name = "englishRadio";
      this.englishRadio.Size = new System.Drawing.Size(59, 17);
      this.englishRadio.TabIndex = 0;
      this.englishRadio.TabStop = true;
      this.englishRadio.Text = "English";
      this.englishRadio.UseVisualStyleBackColor = true;
      this.englishRadio.CheckedChanged += new System.EventHandler(this.englishRadio_CheckedChanged);
      // 
      // titlelLabel
      // 
      this.titlelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.titlelLabel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.titlelLabel.Location = new System.Drawing.Point(206, 3);
      this.titlelLabel.Name = "titlelLabel";
      this.titlelLabel.Size = new System.Drawing.Size(488, 32);
      this.titlelLabel.TabIndex = 4;
      this.titlelLabel.Text = "System Name";
      // 
      // welcomeLabel
      // 
      this.welcomeLabel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.welcomeLabel.ForeColor = System.Drawing.SystemColors.ControlText;
      this.welcomeLabel.Location = new System.Drawing.Point(206, 35);
      this.welcomeLabel.Name = "welcomeLabel";
      this.welcomeLabel.Size = new System.Drawing.Size(488, 138);
      this.welcomeLabel.TabIndex = 5;
      this.welcomeLabel.Text = "Welcome Message";
      // 
      // urlLink
      // 
      this.urlLink.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.urlLink.Location = new System.Drawing.Point(206, 173);
      this.urlLink.Name = "urlLink";
      this.urlLink.Size = new System.Drawing.Size(488, 27);
      this.urlLink.TabIndex = 7;
      this.urlLink.TabStop = true;
      this.urlLink.Text = "url";
      this.urlLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.alphaBugLink_LinkClicked);
      // 
      // languageGroupBox
      // 
      this.languageGroupBox.Controls.Add(this.frenchRadio);
      this.languageGroupBox.Controls.Add(this.englishRadio);
      this.languageGroupBox.Controls.Add(this.germanRadio);
      this.languageGroupBox.Location = new System.Drawing.Point(33, 268);
      this.languageGroupBox.Name = "languageGroupBox";
      this.languageGroupBox.Size = new System.Drawing.Size(241, 125);
      this.languageGroupBox.TabIndex = 9;
      this.languageGroupBox.TabStop = false;
      // 
      // optionGroupBox
      // 
      this.optionGroupBox.Controls.Add(this.tallyOnlyRadio);
      this.optionGroupBox.Controls.Add(this.votingRadio);
      this.optionGroupBox.Controls.Add(this.advancedOptionsRadio);
      this.optionGroupBox.Location = new System.Drawing.Point(280, 268);
      this.optionGroupBox.Name = "optionGroupBox";
      this.optionGroupBox.Size = new System.Drawing.Size(241, 125);
      this.optionGroupBox.TabIndex = 10;
      this.optionGroupBox.TabStop = false;
      // 
      // tallyOnlyRadio
      // 
      this.tallyOnlyRadio.AutoSize = true;
      this.tallyOnlyRadio.Location = new System.Drawing.Point(6, 93);
      this.tallyOnlyRadio.Name = "tallyOnlyRadio";
      this.tallyOnlyRadio.Size = new System.Drawing.Size(69, 17);
      this.tallyOnlyRadio.TabIndex = 2;
      this.tallyOnlyRadio.TabStop = true;
      this.tallyOnlyRadio.Text = "Tally only";
      this.tallyOnlyRadio.UseVisualStyleBackColor = true;
      // 
      // votingRadio
      // 
      this.votingRadio.AutoSize = true;
      this.votingRadio.Location = new System.Drawing.Point(6, 19);
      this.votingRadio.Name = "votingRadio";
      this.votingRadio.Size = new System.Drawing.Size(55, 17);
      this.votingRadio.TabIndex = 0;
      this.votingRadio.TabStop = true;
      this.votingRadio.Text = "Voting";
      this.votingRadio.UseVisualStyleBackColor = true;
      // 
      // advancedOptionsRadio
      // 
      this.advancedOptionsRadio.AutoSize = true;
      this.advancedOptionsRadio.Location = new System.Drawing.Point(6, 55);
      this.advancedOptionsRadio.Name = "advancedOptionsRadio";
      this.advancedOptionsRadio.Size = new System.Drawing.Size(113, 17);
      this.advancedOptionsRadio.TabIndex = 1;
      this.advancedOptionsRadio.TabStop = true;
      this.advancedOptionsRadio.Text = "Advanced Options";
      this.advancedOptionsRadio.UseVisualStyleBackColor = true;
      // 
      // tileImage
      // 
      this.tileImage.Location = new System.Drawing.Point(0, 0);
      this.tileImage.Name = "tileImage";
      this.tileImage.Size = new System.Drawing.Size(200, 200);
      this.tileImage.TabIndex = 11;
      this.tileImage.TabStop = false;
      // 
      // StartItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.tileImage);
      this.Controls.Add(this.optionGroupBox);
      this.Controls.Add(this.languageGroupBox);
      this.Controls.Add(this.urlLink);
      this.Controls.Add(this.welcomeLabel);
      this.Controls.Add(this.titlelLabel);
      this.Margin = new System.Windows.Forms.Padding(3);
      this.Name = "StartItem";
      this.Size = new System.Drawing.Size(700, 487);
      this.Load += new System.EventHandler(this.StartItem_Load);
      this.languageGroupBox.ResumeLayout(false);
      this.languageGroupBox.PerformLayout();
      this.optionGroupBox.ResumeLayout(false);
      this.optionGroupBox.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)(this.tileImage)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private RadioButton frenchRadio;
    private RadioButton germanRadio;
    private RadioButton englishRadio;
    private Label titlelLabel;
    private Label welcomeLabel;
    private LinkLabel urlLink;
    private GroupBox languageGroupBox;
    private GroupBox optionGroupBox;
    private RadioButton tallyOnlyRadio;
    private RadioButton votingRadio;
    private RadioButton advancedOptionsRadio;
    private PictureBox tileImage;
  }
}
