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
  partial class ChooseCertificateItem
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
      this.loadButton = new System.Windows.Forms.Button();
      this.idLabel = new System.Windows.Forms.Label();
      this.typeLabel = new System.Windows.Forms.Label();
      this.nameLabel = new System.Windows.Forms.Label();
      this.idTextBox = new System.Windows.Forms.TextBox();
      this.typeTextBox = new System.Windows.Forms.TextBox();
      this.nameTextBox = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // loadButton
      // 
      this.loadButton.Location = new System.Drawing.Point(58, 47);
      this.loadButton.Name = "loadButton";
      this.loadButton.Size = new System.Drawing.Size(112, 25);
      this.loadButton.TabIndex = 0;
      this.loadButton.Text = "&Load...";
      this.loadButton.UseVisualStyleBackColor = true;
      this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
      // 
      // idLabel
      // 
      this.idLabel.AutoSize = true;
      this.idLabel.Location = new System.Drawing.Point(55, 118);
      this.idLabel.Name = "idLabel";
      this.idLabel.Size = new System.Drawing.Size(18, 14);
      this.idLabel.TabIndex = 1;
      this.idLabel.Text = "Id:";
      // 
      // typeLabel
      // 
      this.typeLabel.AutoSize = true;
      this.typeLabel.Location = new System.Drawing.Point(55, 146);
      this.typeLabel.Name = "typeLabel";
      this.typeLabel.Size = new System.Drawing.Size(33, 14);
      this.typeLabel.TabIndex = 2;
      this.typeLabel.Text = "Type:";
      // 
      // nameLabel
      // 
      this.nameLabel.AutoSize = true;
      this.nameLabel.Location = new System.Drawing.Point(55, 174);
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Size = new System.Drawing.Size(55, 14);
      this.nameLabel.TabIndex = 3;
      this.nameLabel.Text = "Full name:";
      // 
      // idTextBox
      // 
      this.idTextBox.Location = new System.Drawing.Point(153, 115);
      this.idTextBox.Name = "idTextBox";
      this.idTextBox.ReadOnly = true;
      this.idTextBox.Size = new System.Drawing.Size(245, 20);
      this.idTextBox.TabIndex = 4;
      // 
      // typeTextBox
      // 
      this.typeTextBox.Location = new System.Drawing.Point(153, 143);
      this.typeTextBox.Name = "typeTextBox";
      this.typeTextBox.ReadOnly = true;
      this.typeTextBox.Size = new System.Drawing.Size(245, 20);
      this.typeTextBox.TabIndex = 5;
      // 
      // nameTextBox
      // 
      this.nameTextBox.Location = new System.Drawing.Point(153, 171);
      this.nameTextBox.Name = "nameTextBox";
      this.nameTextBox.ReadOnly = true;
      this.nameTextBox.Size = new System.Drawing.Size(245, 20);
      this.nameTextBox.TabIndex = 6;
      // 
      // ChooseCertificateItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.nameTextBox);
      this.Controls.Add(this.typeTextBox);
      this.Controls.Add(this.idTextBox);
      this.Controls.Add(this.nameLabel);
      this.Controls.Add(this.typeLabel);
      this.Controls.Add(this.idLabel);
      this.Controls.Add(this.loadButton);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "ChooseCertificateItem";
      this.Size = new System.Drawing.Size(700, 538);
      this.Load += new System.EventHandler(this.StartWizardItem_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Button loadButton;
    private Label idLabel;
    private Label typeLabel;
    private Label nameLabel;
    private TextBox idTextBox;
    private TextBox typeTextBox;
    private TextBox nameTextBox;
  }
}
