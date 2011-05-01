/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle.Vote
{
  partial class InfoControl
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

        if (this.timer != null)
        {
          this.timer.Stop();
          this.timer.Dispose();
          this.timer = null;
        }

        if (this.infoForm != null)
        {
          this.infoForm.Close();
          this.infoForm = null;
        }
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
      this.titleBox = new System.Windows.Forms.Label();
      this.webBox = new System.Windows.Forms.LinkLabel();
      this.SuspendLayout();
      // 
      // titleBox
      // 
      this.titleBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.titleBox.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.titleBox.Location = new System.Drawing.Point(2, 3);
      this.titleBox.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.titleBox.Name = "titleBox";
      this.titleBox.Size = new System.Drawing.Size(428, 22);
      this.titleBox.TabIndex = 1;
      // 
      // webBox
      // 
      this.webBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.webBox.AutoSize = true;
      this.webBox.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.webBox.Location = new System.Drawing.Point(436, 3);
      this.webBox.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.webBox.Name = "webBox";
      this.webBox.Size = new System.Drawing.Size(37, 20);
      this.webBox.TabIndex = 2;
      this.webBox.TabStop = true;
      this.webBox.Text = "Web";
      this.webBox.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.webBox_LinkClicked);
      // 
      // InfoControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.webBox);
      this.Controls.Add(this.titleBox);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.MaximumSize = new System.Drawing.Size(3000, 35);
      this.MinimumSize = new System.Drawing.Size(100, 28);
      this.Name = "InfoControl";
      this.Size = new System.Drawing.Size(500, 28);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label titleBox;
    private System.Windows.Forms.LinkLabel webBox;
  }
}
