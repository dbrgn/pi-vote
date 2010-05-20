﻿/*
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
      this.alphaTitleLabel = new System.Windows.Forms.Label();
      this.titlelLabel = new System.Windows.Forms.Label();
      this.alphaWarningLabel = new System.Windows.Forms.Label();
      this.alphaBugLink = new System.Windows.Forms.LinkLabel();
      this.advancedOptionsCheckBox = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // frenchRadio
      // 
      this.frenchRadio.AutoSize = true;
      this.frenchRadio.Location = new System.Drawing.Point(60, 558);
      this.frenchRadio.Margin = new System.Windows.Forms.Padding(4);
      this.frenchRadio.Name = "frenchRadio";
      this.frenchRadio.Size = new System.Drawing.Size(97, 23);
      this.frenchRadio.TabIndex = 2;
      this.frenchRadio.TabStop = true;
      this.frenchRadio.Text = "Français";
      this.frenchRadio.UseVisualStyleBackColor = true;
      this.frenchRadio.CheckedChanged += new System.EventHandler(this.frenchRadio_CheckedChanged);
      // 
      // germanRadio
      // 
      this.germanRadio.AutoSize = true;
      this.germanRadio.Location = new System.Drawing.Point(60, 501);
      this.germanRadio.Margin = new System.Windows.Forms.Padding(4);
      this.germanRadio.Name = "germanRadio";
      this.germanRadio.Size = new System.Drawing.Size(93, 23);
      this.germanRadio.TabIndex = 1;
      this.germanRadio.TabStop = true;
      this.germanRadio.Text = "Deutsch";
      this.germanRadio.UseVisualStyleBackColor = true;
      this.germanRadio.CheckedChanged += new System.EventHandler(this.germanRadio_CheckedChanged);
      // 
      // englishRadio
      // 
      this.englishRadio.AutoSize = true;
      this.englishRadio.Location = new System.Drawing.Point(60, 447);
      this.englishRadio.Margin = new System.Windows.Forms.Padding(4);
      this.englishRadio.Name = "englishRadio";
      this.englishRadio.Size = new System.Drawing.Size(86, 23);
      this.englishRadio.TabIndex = 0;
      this.englishRadio.TabStop = true;
      this.englishRadio.Text = "English";
      this.englishRadio.UseVisualStyleBackColor = true;
      this.englishRadio.CheckedChanged += new System.EventHandler(this.englishRadio_CheckedChanged);
      // 
      // alphaTitleLabel
      // 
      this.alphaTitleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.alphaTitleLabel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.alphaTitleLabel.ForeColor = System.Drawing.Color.Red;
      this.alphaTitleLabel.Location = new System.Drawing.Point(314, 52);
      this.alphaTitleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.alphaTitleLabel.Name = "alphaTitleLabel";
      this.alphaTitleLabel.Size = new System.Drawing.Size(736, 40);
      this.alphaTitleLabel.TabIndex = 3;
      this.alphaTitleLabel.Text = "Alpha Version";
      // 
      // titlelLabel
      // 
      this.titlelLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.titlelLabel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.titlelLabel.Location = new System.Drawing.Point(314, 4);
      this.titlelLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.titlelLabel.Name = "titlelLabel";
      this.titlelLabel.Size = new System.Drawing.Size(732, 48);
      this.titlelLabel.TabIndex = 4;
      this.titlelLabel.Text = "Pirate Party Switzerland eVoting";
      // 
      // alphaWarningLabel
      // 
      this.alphaWarningLabel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.alphaWarningLabel.ForeColor = System.Drawing.Color.Red;
      this.alphaWarningLabel.Location = new System.Drawing.Point(314, 93);
      this.alphaWarningLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.alphaWarningLabel.Name = "alphaWarningLabel";
      this.alphaWarningLabel.Size = new System.Drawing.Size(732, 158);
      this.alphaWarningLabel.TabIndex = 5;
      this.alphaWarningLabel.Text = "This is alpha verion of this software. All voting are just for test purposes. Ple" +
          "ase report all bugs to:";
      // 
      // alphaBugLink
      // 
      this.alphaBugLink.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.alphaBugLink.Location = new System.Drawing.Point(314, 250);
      this.alphaBugLink.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.alphaBugLink.Name = "alphaBugLink";
      this.alphaBugLink.Size = new System.Drawing.Size(732, 54);
      this.alphaBugLink.TabIndex = 7;
      this.alphaBugLink.TabStop = true;
      this.alphaBugLink.Text = "http://pi-vote.origo.ethz.ch/";
      // 
      // advancedOptionsCheckBox
      // 
      this.advancedOptionsCheckBox.AutoSize = true;
      this.advancedOptionsCheckBox.Location = new System.Drawing.Point(389, 448);
      this.advancedOptionsCheckBox.Name = "advancedOptionsCheckBox";
      this.advancedOptionsCheckBox.Size = new System.Drawing.Size(166, 23);
      this.advancedOptionsCheckBox.TabIndex = 8;
      this.advancedOptionsCheckBox.Text = "Advanced Options";
      this.advancedOptionsCheckBox.UseVisualStyleBackColor = true;
      // 
      // StartItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.advancedOptionsCheckBox);
      this.Controls.Add(this.alphaBugLink);
      this.Controls.Add(this.alphaWarningLabel);
      this.Controls.Add(this.titlelLabel);
      this.Controls.Add(this.alphaTitleLabel);
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
    private Label alphaTitleLabel;
    private Label titlelLabel;
    private Label alphaWarningLabel;
    private LinkLabel alphaBugLink;
    private CheckBox advancedOptionsCheckBox;
  }
}
