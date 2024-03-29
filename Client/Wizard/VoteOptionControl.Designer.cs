﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Client
{
  partial class VoteOptionControl
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
      this.optionRadioButton = new System.Windows.Forms.RadioButton();
      this.descriptionButton = new System.Windows.Forms.Button();
      this.optionCheckBox = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // optionRadioButton
      // 
      this.optionRadioButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.optionRadioButton.Font = new System.Drawing.Font("Arial", 8.25F);
      this.optionRadioButton.Location = new System.Drawing.Point(3, 4);
      this.optionRadioButton.Name = "optionRadioButton";
      this.optionRadioButton.Size = new System.Drawing.Size(376, 18);
      this.optionRadioButton.TabIndex = 0;
      this.optionRadioButton.TabStop = true;
      this.optionRadioButton.Text = "Option";
      this.optionRadioButton.UseVisualStyleBackColor = true;
      this.optionRadioButton.CheckedChanged += new System.EventHandler(this.optionRadioButton_CheckedChanged);
      // 
      // descriptionButton
      // 
      this.descriptionButton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.descriptionButton.Location = new System.Drawing.Point(385, 0);
      this.descriptionButton.Name = "descriptionButton";
      this.descriptionButton.Size = new System.Drawing.Size(111, 26);
      this.descriptionButton.TabIndex = 1;
      this.descriptionButton.Text = "&Description";
      this.descriptionButton.UseVisualStyleBackColor = true;
      this.descriptionButton.Click += new System.EventHandler(this.descriptionButton_Click);
      // 
      // optionCheckBox
      // 
      this.optionCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.optionCheckBox.Font = new System.Drawing.Font("Arial", 8.25F);
      this.optionCheckBox.Location = new System.Drawing.Point(3, 4);
      this.optionCheckBox.Name = "optionCheckBox";
      this.optionCheckBox.Size = new System.Drawing.Size(377, 18);
      this.optionCheckBox.TabIndex = 2;
      this.optionCheckBox.Text = "Option";
      this.optionCheckBox.UseVisualStyleBackColor = true;
      this.optionCheckBox.CheckedChanged += new System.EventHandler(this.optionCheckBox_CheckedChanged);
      // 
      // VoteOptionControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.optionCheckBox);
      this.Controls.Add(this.descriptionButton);
      this.Controls.Add(this.optionRadioButton);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "VoteOptionControl";
      this.Size = new System.Drawing.Size(496, 26);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.RadioButton optionRadioButton;
    private System.Windows.Forms.Button descriptionButton;
    private System.Windows.Forms.CheckBox optionCheckBox;
  }
}
