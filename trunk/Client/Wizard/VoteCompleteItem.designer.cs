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
  partial class VoteCompleteItem
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
      this.castLabel = new System.Windows.Forms.Label();
      this.spaceLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // castLabel
      // 
      this.castLabel.AutoSize = true;
      this.castLabel.Location = new System.Drawing.Point(46, 192);
      this.castLabel.Name = "castLabel";
      this.castLabel.Size = new System.Drawing.Size(0, 14);
      this.castLabel.TabIndex = 2;
      // 
      // spaceLabel
      // 
      this.spaceLabel.AutoSize = true;
      this.spaceLabel.ForeColor = System.Drawing.Color.Red;
      this.spaceLabel.Location = new System.Drawing.Point(213, 236);
      this.spaceLabel.Name = "spaceLabel";
      this.spaceLabel.Size = new System.Drawing.Size(274, 14);
      this.spaceLabel.TabIndex = 3;
      this.spaceLabel.Text = "This space shall be occupied by an appropriate picture.";
      // 
      // VoteCompleteItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.spaceLabel);
      this.Controls.Add(this.castLabel);
      this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
      this.Name = "VoteCompleteItem";
      this.Size = new System.Drawing.Size(700, 487);
      this.Load += new System.EventHandler(this.VoteCompleteItem_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Label castLabel;
    private Label spaceLabel;




  }
}
