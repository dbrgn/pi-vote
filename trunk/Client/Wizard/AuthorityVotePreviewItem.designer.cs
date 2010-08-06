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
  partial class AuthorityVotePreviewItem
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
      this.verifyButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // voteControl
      // 
      this.voteControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.voteControl.Font = new System.Drawing.Font("Arial", 8.25F);
      this.voteControl.Location = new System.Drawing.Point(0, 0);
      this.voteControl.Name = "voteControl";
      this.voteControl.Size = new System.Drawing.Size(700, 562);
      this.voteControl.TabIndex = 0;
      this.voteControl.Voting = null;
      // 
      // verifyButton
      // 
      this.verifyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.verifyButton.Location = new System.Drawing.Point(589, 537);
      this.verifyButton.Name = "verifyButton";
      this.verifyButton.Size = new System.Drawing.Size(111, 25);
      this.verifyButton.TabIndex = 1;
      this.verifyButton.Text = "&Verify";
      this.verifyButton.UseVisualStyleBackColor = true;
      this.verifyButton.Click += new System.EventHandler(this.verifyButton_Click);
      // 
      // AuthorityVotePreviewItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.verifyButton);
      this.Controls.Add(this.voteControl);
      this.Margin = new System.Windows.Forms.Padding(3);
      this.Name = "AuthorityVotePreviewItem";
      this.Size = new System.Drawing.Size(700, 562);
      this.ResumeLayout(false);

    }

    #endregion

    private VoteControl voteControl;
    private Button verifyButton;




  }
}
