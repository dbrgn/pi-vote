﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Client
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
      this.questionLabel = new System.Windows.Forms.Label();
      this.descriptionButton = new System.Windows.Forms.Button();
      this.maxOptionsLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // questionLabel
      // 
      this.questionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.questionLabel.Font = new System.Drawing.Font("Arial", 8.25F);
      this.questionLabel.Location = new System.Drawing.Point(3, 7);
      this.questionLabel.Name = "questionLabel";
      this.questionLabel.Size = new System.Drawing.Size(458, 26);
      this.questionLabel.TabIndex = 6;
      this.questionLabel.Text = "Question";
      // 
      // descriptionButton
      // 
      this.descriptionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.descriptionButton.Location = new System.Drawing.Point(467, 3);
      this.descriptionButton.Name = "descriptionButton";
      this.descriptionButton.Size = new System.Drawing.Size(111, 26);
      this.descriptionButton.TabIndex = 5;
      this.descriptionButton.Text = "&Description";
      this.descriptionButton.UseVisualStyleBackColor = true;
      this.descriptionButton.Click += new System.EventHandler(this.DescriptionButton_Click);
      // 
      // maxOptionsLabel
      // 
      this.maxOptionsLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.maxOptionsLabel.Location = new System.Drawing.Point(3, 33);
      this.maxOptionsLabel.Name = "maxOptionsLabel";
      this.maxOptionsLabel.Size = new System.Drawing.Size(458, 14);
      this.maxOptionsLabel.TabIndex = 7;
      this.maxOptionsLabel.Text = "max options";
      // 
      // QuestionControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.maxOptionsLabel);
      this.Controls.Add(this.questionLabel);
      this.Controls.Add(this.descriptionButton);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "QuestionControl";
      this.Size = new System.Drawing.Size(581, 194);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label questionLabel;
    private System.Windows.Forms.Button descriptionButton;
    private System.Windows.Forms.Label maxOptionsLabel;
  }
}
