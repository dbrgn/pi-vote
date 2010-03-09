﻿/*
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
  partial class GetSignatureRequestsItem
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
      this.saveToButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // saveToButton
      // 
      this.saveToButton.Location = new System.Drawing.Point(77, 83);
      this.saveToButton.Margin = new System.Windows.Forms.Padding(2);
      this.saveToButton.Name = "saveToButton";
      this.saveToButton.Size = new System.Drawing.Size(116, 28);
      this.saveToButton.TabIndex = 4;
      this.saveToButton.Text = "Save to...";
      this.saveToButton.UseVisualStyleBackColor = true;
      this.saveToButton.Click += new System.EventHandler(this.saveToButton_Click);
      // 
      // GetSignatureRequestsItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.saveToButton);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "GetSignatureRequestsItem";
      this.Size = new System.Drawing.Size(700, 538);
      this.ResumeLayout(false);

    }

    #endregion

    private Button saveToButton;




  }
}
