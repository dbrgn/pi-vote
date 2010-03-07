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
      this.openButton.Location = new System.Drawing.Point(77, 77);
      this.openButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.openButton.Name = "openButton";
      this.openButton.Size = new System.Drawing.Size(116, 20);
      this.openButton.TabIndex = 4;
      this.openButton.Text = "Open...";
      this.openButton.UseVisualStyleBackColor = true;
      this.openButton.Click += new System.EventHandler(this.openButton_Click);
      // 
      // SetSignatureResponsesItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.openButton);
      this.Name = "SetSignatureResponsesItem";
      this.Size = new System.Drawing.Size(700, 500);
      this.ResumeLayout(false);

    }

    #endregion

    private Button openButton;




  }
}
