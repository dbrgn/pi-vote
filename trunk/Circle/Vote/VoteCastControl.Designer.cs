/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle
{
  partial class VoteCastControl
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
      this.voteButton = new System.Windows.Forms.Button();
      this.voteControl = new Pirate.PiVote.Circle.VoteControl();
      this.SuspendLayout();
      // 
      // voteButton
      // 
      this.voteButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.voteButton.Enabled = false;
      this.voteButton.Location = new System.Drawing.Point(429, 335);
      this.voteButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.voteButton.Name = "voteButton";
      this.voteButton.Size = new System.Drawing.Size(114, 31);
      this.voteButton.TabIndex = 2;
      this.voteButton.Text = "&Vote";
      this.voteButton.UseVisualStyleBackColor = true;
      this.voteButton.Click += new System.EventHandler(this.voteButton_Click);
      // 
      // voteControl
      // 
      this.voteControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.voteControl.Font = new System.Drawing.Font("Arial", 8.25F);
      this.voteControl.Location = new System.Drawing.Point(0, 0);
      this.voteControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.voteControl.Name = "voteControl";
      this.voteControl.Size = new System.Drawing.Size(543, 328);
      this.voteControl.TabIndex = 0;
      this.voteControl.Voting = null;
      // 
      // VoteCastControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.voteButton);
      this.Controls.Add(this.voteControl);
      this.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.Name = "VoteCastControl";
      this.Size = new System.Drawing.Size(543, 369);
      this.ResumeLayout(false);

    }

    #endregion

    private VoteControl voteControl;
    private System.Windows.Forms.Button voteButton;
  }
}
