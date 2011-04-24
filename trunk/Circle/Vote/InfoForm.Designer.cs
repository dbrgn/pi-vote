/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle.Vote
{
  partial class InfoForm
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.descriptionLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // descriptionLabel
      // 
      this.descriptionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.descriptionLabel.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.descriptionLabel.Location = new System.Drawing.Point(0, 0);
      this.descriptionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.descriptionLabel.Name = "descriptionLabel";
      this.descriptionLabel.Size = new System.Drawing.Size(243, 315);
      this.descriptionLabel.TabIndex = 0;
      this.descriptionLabel.Text = "label1";
      // 
      // InfoForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(5F, 15F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(243, 315);
      this.ControlBox = false;
      this.Controls.Add(this.descriptionLabel);
      this.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "InfoForm";
      this.ShowIcon = false;
      this.ShowInTaskbar = false;
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label descriptionLabel;
  }
}