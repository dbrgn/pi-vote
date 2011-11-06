/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle.CreateVoting
{
  partial class CreateVotingDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateVotingDialog));
      this.SuspendLayout();
      // 
      // CreateVotingDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(792, 573);
      this.Font = new System.Drawing.Font("Arial", 8.25F);

      // This hack is necessary because the mono compiler/runtime seems to be broken when it comes to icons.
#if !__MonoCS__
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
#endif

      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(800, 600);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(600, 300);
      this.Name = "CreateVotingDialog";
      this.Text = "Circle - Create Certificate";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreateVotingDialog_FormClosing);
      this.Load += new System.EventHandler(this.CreateCertificateDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CreateVotingDialog_KeyDown);
      this.ResumeLayout(false);

    }

    #endregion
  }
}