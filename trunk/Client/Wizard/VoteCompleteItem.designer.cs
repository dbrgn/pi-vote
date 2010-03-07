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
      this.SuspendLayout();
      // 
      // castLabel
      // 
      this.castLabel.AutoSize = true;
      this.castLabel.Location = new System.Drawing.Point(3, 196);
      this.castLabel.Name = "castLabel";
      this.castLabel.Size = new System.Drawing.Size(0, 13);
      this.castLabel.TabIndex = 2;
      // 
      // VoteCompleteItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.castLabel);
      this.Name = "VoteCompleteItem";
      this.Size = new System.Drawing.Size(700, 500);
      this.Load += new System.EventHandler(this.VoteCompleteItem_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Label castLabel;




  }
}
