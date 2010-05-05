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
      this.spaceLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // saveToButton
      // 
      this.saveToButton.Location = new System.Drawing.Point(2, 2);
      this.saveToButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.saveToButton.Name = "saveToButton";
      this.saveToButton.Size = new System.Drawing.Size(140, 25);
      this.saveToButton.TabIndex = 4;
      this.saveToButton.Text = "Save to...";
      this.saveToButton.UseVisualStyleBackColor = true;
      this.saveToButton.Click += new System.EventHandler(this.saveToButton_Click);
      // 
      // spaceLabel
      // 
      this.spaceLabel.AutoSize = true;
      this.spaceLabel.ForeColor = System.Drawing.Color.Red;
      this.spaceLabel.Location = new System.Drawing.Point(208, 262);
      this.spaceLabel.Name = "spaceLabel";
      this.spaceLabel.Size = new System.Drawing.Size(274, 14);
      this.spaceLabel.TabIndex = 5;
      this.spaceLabel.Text = "This space shall be occupied by an appropriate picture.";
      // 
      // GetSignatureRequestsItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.spaceLabel);
      this.Controls.Add(this.saveToButton);
      this.Margin = new System.Windows.Forms.Padding(3, 3, 3, 3);
      this.Name = "GetSignatureRequestsItem";
      this.Size = new System.Drawing.Size(700, 487);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Button saveToButton;
    private Label spaceLabel;




  }
}
