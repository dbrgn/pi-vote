/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

namespace Pirate.PiVote.Client
{
  partial class ScrollableContainerControl
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
      this.vScrollBar = new System.Windows.Forms.VScrollBar();
      this.SuspendLayout();
      // 
      // vScrollBar
      // 
      this.vScrollBar.Dock = System.Windows.Forms.DockStyle.Right;
      this.vScrollBar.Location = new System.Drawing.Point(1194, 0);
      this.vScrollBar.Name = "vScrollBar";
      this.vScrollBar.Size = new System.Drawing.Size(16, 726);
      this.vScrollBar.TabIndex = 0;
      // 
      // ScrollableContainerControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.vScrollBar);
      this.Font = new System.Drawing.Font("Arial", 8F);
      this.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
      this.Name = "ScrollableContainerControl";
      this.Size = new System.Drawing.Size(1210, 726);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.VScrollBar vScrollBar;
  }
}
