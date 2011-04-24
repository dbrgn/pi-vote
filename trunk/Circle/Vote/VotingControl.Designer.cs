/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle.Vote
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
      this.questionTabs = new System.Windows.Forms.TabControl();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.tabPage2 = new System.Windows.Forms.TabPage();
      this.titleControl = new Pirate.PiVote.Circle.Vote.InfoControl();
      this.questionTabs.SuspendLayout();
      this.SuspendLayout();
      // 
      // questionTabs
      // 
      this.questionTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.questionTabs.Controls.Add(this.tabPage1);
      this.questionTabs.Controls.Add(this.tabPage2);
      this.questionTabs.Location = new System.Drawing.Point(3, 39);
      this.questionTabs.Name = "questionTabs";
      this.questionTabs.SelectedIndex = 0;
      this.questionTabs.Size = new System.Drawing.Size(591, 535);
      this.questionTabs.TabIndex = 0;
      // 
      // tabPage1
      // 
      this.tabPage1.Location = new System.Drawing.Point(4, 24);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage1.Size = new System.Drawing.Size(583, 507);
      this.tabPage1.TabIndex = 0;
      this.tabPage1.Text = "tabPage1";
      this.tabPage1.UseVisualStyleBackColor = true;
      // 
      // tabPage2
      // 
      this.tabPage2.Location = new System.Drawing.Point(4, 24);
      this.tabPage2.Name = "tabPage2";
      this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
      this.tabPage2.Size = new System.Drawing.Size(583, 507);
      this.tabPage2.TabIndex = 1;
      this.tabPage2.Text = "tabPage2";
      this.tabPage2.UseVisualStyleBackColor = true;
      // 
      // titleControl
      // 
      this.titleControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.titleControl.Description = null;
      this.titleControl.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.titleControl.InfoFont = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.titleControl.Location = new System.Drawing.Point(3, 3);
      this.titleControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.titleControl.MaximumSize = new System.Drawing.Size(2500, 37);
      this.titleControl.MinimumSize = new System.Drawing.Size(83, 30);
      this.titleControl.Name = "titleControl";
      this.titleControl.Size = new System.Drawing.Size(587, 30);
      this.titleControl.TabIndex = 1;
      this.titleControl.Title = "";
      this.titleControl.Url = null;
      // 
      // VotingControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.titleControl);
      this.Controls.Add(this.questionTabs);
      this.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.Name = "VotingControl";
      this.Size = new System.Drawing.Size(597, 577);
      this.questionTabs.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl questionTabs;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private InfoControl titleControl;
  }
}
