/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle
{
  partial class VotingListControl
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
      this.emptyLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // emptyLabel
      // 
      this.emptyLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.emptyLabel.Location = new System.Drawing.Point(0, 0);
      this.emptyLabel.Name = "emptyLabel";
      this.emptyLabel.Size = new System.Drawing.Size(939, 522);
      this.emptyLabel.TabIndex = 0;
      this.emptyLabel.Text = "Empty";
      this.emptyLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
      // 
      // VotingListControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.AutoScroll = true;
      this.Controls.Add(this.emptyLabel);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "VotingListControl";
      this.Size = new System.Drawing.Size(939, 522);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label emptyLabel;
  }
}
