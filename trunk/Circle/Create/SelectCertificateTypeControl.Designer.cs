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
      this.SuspendLayout();
      // 
      // voterCheckBox
      // 
      this.voterCheckBox.AutoSize = true;
      this.voterCheckBox.Location = new System.Drawing.Point(24, 15);
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
      this.authorityCheckBox.Location = new System.Drawing.Point(24, 59);
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
      this.cancelButton.Location = new System.Drawing.Point(210, 147);
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
      this.nextButton.Location = new System.Drawing.Point(344, 147);
      this.nextButton.Name = "nextButton";
      this.nextButton.Size = new System.Drawing.Size(128, 28);
      this.nextButton.TabIndex = 48;
      this.nextButton.Text = "&Next";
      this.nextButton.UseVisualStyleBackColor = true;
      this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
      // 
      // SelectCertificateTypeControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.nextButton);
      this.Controls.Add(this.authorityCheckBox);
      this.Controls.Add(this.voterCheckBox);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "SelectCertificateTypeControl";
      this.Size = new System.Drawing.Size(475, 178);
      this.Load += new System.EventHandler(this.SelectCertificateTypeControl_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.RadioButton voterCheckBox;
    private System.Windows.Forms.RadioButton authorityCheckBox;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button nextButton;
  }
}
