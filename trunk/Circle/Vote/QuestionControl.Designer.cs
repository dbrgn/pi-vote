/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle.Vote
{
  partial class QuestionControl
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
      this.optionsPanel = new System.Windows.Forms.Panel();
      this.maxOptionLabel = new System.Windows.Forms.Label();
      this.textControl = new Pirate.PiVote.Circle.Vote.InfoControl();
      this.SuspendLayout();
      // 
      // optionsPanel
      // 
      this.optionsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.optionsPanel.AutoScroll = true;
      this.optionsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.optionsPanel.Location = new System.Drawing.Point(3, 44);
      this.optionsPanel.Name = "optionsPanel";
      this.optionsPanel.Size = new System.Drawing.Size(658, 428);
      this.optionsPanel.TabIndex = 6;
      // 
      // maxOptionLabel
      // 
      this.maxOptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.maxOptionLabel.AutoSize = true;
      this.maxOptionLabel.Location = new System.Drawing.Point(3, 475);
      this.maxOptionLabel.Name = "maxOptionLabel";
      this.maxOptionLabel.Size = new System.Drawing.Size(84, 14);
      this.maxOptionLabel.TabIndex = 8;
      this.maxOptionLabel.Text = "maxOptionLabel";
      // 
      // textControl
      // 
      this.textControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textControl.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.textControl.Description = null;
      this.textControl.InfoFont = new System.Drawing.Font("Arial", 8.25F);
      this.textControl.Location = new System.Drawing.Point(3, 3);
      this.textControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.textControl.MaximumSize = new System.Drawing.Size(3000, 500);
      this.textControl.MinimumSize = new System.Drawing.Size(100, 28);
      this.textControl.Name = "textControl";
      this.textControl.Size = new System.Drawing.Size(658, 35);
      this.textControl.TabIndex = 7;
      this.textControl.Title = "";
      this.textControl.Url = null;
      // 
      // QuestionControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.maxOptionLabel);
      this.Controls.Add(this.textControl);
      this.Controls.Add(this.optionsPanel);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "QuestionControl";
      this.Size = new System.Drawing.Size(664, 496);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Panel optionsPanel;
    private InfoControl textControl;
    private System.Windows.Forms.Label maxOptionLabel;
  }
}
