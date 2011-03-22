/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle
{
  partial class VotingControl
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
      this.titleLabel = new System.Windows.Forms.Label();
      this.descriptionLabel = new System.Windows.Forms.Label();
      this.actionButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // titleLabel
      // 
      this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.titleLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.titleLabel.Location = new System.Drawing.Point(0, 0);
      this.titleLabel.Name = "titleLabel";
      this.titleLabel.Size = new System.Drawing.Size(498, 25);
      this.titleLabel.TabIndex = 0;
      this.titleLabel.Text = "Title";
      // 
      // descriptionLabel
      // 
      this.descriptionLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.descriptionLabel.Location = new System.Drawing.Point(0, 25);
      this.descriptionLabel.Name = "descriptionLabel";
      this.descriptionLabel.Size = new System.Drawing.Size(498, 58);
      this.descriptionLabel.TabIndex = 1;
      this.descriptionLabel.Text = "Description";
      // 
      // actionButton
      // 
      this.actionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.actionButton.Location = new System.Drawing.Point(384, 86);
      this.actionButton.Name = "actionButton";
      this.actionButton.Size = new System.Drawing.Size(111, 25);
      this.actionButton.TabIndex = 2;
      this.actionButton.Text = "&Action";
      this.actionButton.UseVisualStyleBackColor = true;
      this.actionButton.Click += new System.EventHandler(this.ActionButton_Click);
      // 
      // VotingControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
      this.Controls.Add(this.actionButton);
      this.Controls.Add(this.descriptionLabel);
      this.Controls.Add(this.titleLabel);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "VotingControl";
      this.Size = new System.Drawing.Size(498, 114);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label titleLabel;
    private System.Windows.Forms.Label descriptionLabel;
    private System.Windows.Forms.Button actionButton;
  }
}
