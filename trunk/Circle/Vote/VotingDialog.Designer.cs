﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle.Vote
{
  partial class VotingDialog
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
      this.votingControl = new Pirate.PiVote.Circle.Vote.VotingControl();
      this.cancelButton = new System.Windows.Forms.Button();
      this.voteButton = new System.Windows.Forms.Button();
      this.nextButton = new System.Windows.Forms.Button();
      this.previousButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // votingControl
      // 
      this.votingControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.votingControl.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.votingControl.Location = new System.Drawing.Point(0, 0);
      this.votingControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.votingControl.Name = "votingControl";
      this.votingControl.Size = new System.Drawing.Size(817, 630);
      this.votingControl.TabIndex = 0;
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(686, 636);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(119, 30);
      this.cancelButton.TabIndex = 1;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // voteButton
      // 
      this.voteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.voteButton.Location = new System.Drawing.Point(561, 636);
      this.voteButton.Name = "voteButton";
      this.voteButton.Size = new System.Drawing.Size(119, 30);
      this.voteButton.TabIndex = 2;
      this.voteButton.Text = "&Vote";
      this.voteButton.UseVisualStyleBackColor = true;
      this.voteButton.Click += new System.EventHandler(this.voteButton_Click);
      // 
      // nextButton
      // 
      this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.nextButton.Location = new System.Drawing.Point(436, 636);
      this.nextButton.Name = "nextButton";
      this.nextButton.Size = new System.Drawing.Size(119, 30);
      this.nextButton.TabIndex = 3;
      this.nextButton.Text = "&Next";
      this.nextButton.UseVisualStyleBackColor = true;
      this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
      // 
      // previousButton
      // 
      this.previousButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.previousButton.Location = new System.Drawing.Point(311, 636);
      this.previousButton.Name = "previousButton";
      this.previousButton.Size = new System.Drawing.Size(119, 30);
      this.previousButton.TabIndex = 4;
      this.previousButton.Text = "&Previouse";
      this.previousButton.UseVisualStyleBackColor = true;
      this.previousButton.Click += new System.EventHandler(this.previousButton_Click);
      // 
      // VotingDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(817, 675);
      this.Controls.Add(this.previousButton);
      this.Controls.Add(this.nextButton);
      this.Controls.Add(this.voteButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.votingControl);
      this.Name = "VotingDialog";
      this.Text = "VotingDialog";
      this.Load += new System.EventHandler(this.VotingDialog_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private VotingControl votingControl;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button voteButton;
    private System.Windows.Forms.Button nextButton;
    private System.Windows.Forms.Button previousButton;
  }
}