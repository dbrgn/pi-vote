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
  partial class VoteItem
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
      this.voteControl = new Pirate.PiVote.Client.VoteControl();
      this.SuspendLayout();
      // 
      // voteControl
      // 
      this.voteControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.voteControl.Font = new System.Drawing.Font("Arial", 8.25F);
      this.voteControl.Location = new System.Drawing.Point(0, 0);
      this.voteControl.Name = "voteControl";
      this.voteControl.Size = new System.Drawing.Size(597, 487);
      this.voteControl.TabIndex = 0;
      this.voteControl.Voting = null;
      // 
      // VoteItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.voteControl);
      this.Margin = new System.Windows.Forms.Padding(3);
      this.Name = "VoteItem";
      this.Size = new System.Drawing.Size(597, 487);
      this.ResumeLayout(false);

    }

    #endregion

    private VoteControl voteControl;





  }
}
