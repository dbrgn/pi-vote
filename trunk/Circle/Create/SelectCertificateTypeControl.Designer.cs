/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle.Create
{
  partial class SelectCertificateTypeControl
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
      this.voterCheckBox = new System.Windows.Forms.RadioButton();
      this.authorityCheckBox = new System.Windows.Forms.RadioButton();
      this.cancelButton = new System.Windows.Forms.Button();
      this.nextButton = new System.Windows.Forms.Button();
      this.voterSubGroupCheckBox = new System.Windows.Forms.RadioButton();
      this.voterLabel = new System.Windows.Forms.Label();
      this.voterSubgroupLabel = new System.Windows.Forms.Label();
      this.authorityLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // voterCheckBox
      // 
      this.voterCheckBox.AutoSize = true;
      this.voterCheckBox.Location = new System.Drawing.Point(14, 12);
      this.voterCheckBox.Name = "voterCheckBox";
      this.voterCheckBox.Size = new System.Drawing.Size(51, 18);
      this.voterCheckBox.TabIndex = 0;
      this.voterCheckBox.TabStop = true;
      this.voterCheckBox.Text = "Voter";
      this.voterCheckBox.UseVisualStyleBackColor = true;
      this.voterCheckBox.CheckedChanged += new System.EventHandler(this.voterCheckBox_CheckedChanged);
      // 
      // authorityCheckBox
      // 
      this.authorityCheckBox.AutoSize = true;
      this.authorityCheckBox.Location = new System.Drawing.Point(14, 177);
      this.authorityCheckBox.Name = "authorityCheckBox";
      this.authorityCheckBox.Size = new System.Drawing.Size(101, 18);
      this.authorityCheckBox.TabIndex = 1;
      this.authorityCheckBox.TabStop = true;
      this.authorityCheckBox.Text = "Voting Authority";
      this.authorityCheckBox.UseVisualStyleBackColor = true;
      this.authorityCheckBox.CheckedChanged += new System.EventHandler(this.authorityCheckBox_CheckedChanged);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(210, 242);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(128, 28);
      this.cancelButton.TabIndex = 49;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // nextButton
      // 
      this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.nextButton.Font = new System.Drawing.Font("Arial", 8.25F);
      this.nextButton.Location = new System.Drawing.Point(344, 242);
      this.nextButton.Name = "nextButton";
      this.nextButton.Size = new System.Drawing.Size(128, 28);
      this.nextButton.TabIndex = 48;
      this.nextButton.Text = "&Next";
      this.nextButton.UseVisualStyleBackColor = true;
      this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
      // 
      // voterSubGroupCheckBox
      // 
      this.voterSubGroupCheckBox.AutoSize = true;
      this.voterSubGroupCheckBox.Location = new System.Drawing.Point(14, 94);
      this.voterSubGroupCheckBox.Name = "voterSubGroupCheckBox";
      this.voterSubGroupCheckBox.Size = new System.Drawing.Size(117, 18);
      this.voterSubGroupCheckBox.TabIndex = 50;
      this.voterSubGroupCheckBox.TabStop = true;
      this.voterSubGroupCheckBox.Text = "Voter for subgroup";
      this.voterSubGroupCheckBox.UseVisualStyleBackColor = true;
      // 
      // voterLabel
      // 
      this.voterLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.voterLabel.Location = new System.Drawing.Point(30, 35);
      this.voterLabel.Name = "voterLabel";
      this.voterLabel.Size = new System.Drawing.Size(442, 56);
      this.voterLabel.TabIndex = 51;
      this.voterLabel.Text = "label1";
      // 
      // voterSubgroupLabel
      // 
      this.voterSubgroupLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.voterSubgroupLabel.Location = new System.Drawing.Point(30, 115);
      this.voterSubgroupLabel.Name = "voterSubgroupLabel";
      this.voterSubgroupLabel.Size = new System.Drawing.Size(442, 59);
      this.voterSubgroupLabel.TabIndex = 52;
      this.voterSubgroupLabel.Text = "label2";
      // 
      // authorityLabel
      // 
      this.authorityLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.authorityLabel.Location = new System.Drawing.Point(30, 198);
      this.authorityLabel.Name = "authorityLabel";
      this.authorityLabel.Size = new System.Drawing.Size(442, 41);
      this.authorityLabel.TabIndex = 53;
      this.authorityLabel.Text = "label3";
      // 
      // SelectCertificateTypeControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.authorityLabel);
      this.Controls.Add(this.voterSubgroupLabel);
      this.Controls.Add(this.voterLabel);
      this.Controls.Add(this.voterSubGroupCheckBox);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.nextButton);
      this.Controls.Add(this.authorityCheckBox);
      this.Controls.Add(this.voterCheckBox);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "SelectCertificateTypeControl";
      this.Size = new System.Drawing.Size(475, 273);
      this.Load += new System.EventHandler(this.SelectCertificateTypeControl_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.RadioButton voterCheckBox;
    private System.Windows.Forms.RadioButton authorityCheckBox;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button nextButton;
    private System.Windows.Forms.RadioButton voterSubGroupCheckBox;
    private System.Windows.Forms.Label voterLabel;
    private System.Windows.Forms.Label voterSubgroupLabel;
    private System.Windows.Forms.Label authorityLabel;
  }
}
