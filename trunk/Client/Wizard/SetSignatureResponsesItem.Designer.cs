/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Pirate.PiVote.Client
{
  partial class SetSignatureResponsesItem
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
      this.openButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // openButton
      // 
      this.openButton.Location = new System.Drawing.Point(0, 3);
      this.openButton.Name = "openButton";
      this.openButton.Size = new System.Drawing.Size(174, 37);
      this.openButton.TabIndex = 4;
      this.openButton.Text = "Open...";
      this.openButton.UseVisualStyleBackColor = true;
      this.openButton.Click += new System.EventHandler(this.openButton_Click);
      // 
      // SetSignatureResponsesItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.openButton);
      this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.Name = "SetSignatureResponsesItem";
      this.Size = new System.Drawing.Size(1050, 730);
      this.ResumeLayout(false);

    }

    #endregion

    private Button openButton;




  }
}
