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
  partial class CreateRequestItem
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
      this.idTextBox = new System.Windows.Forms.TextBox();
      this.idLabel = new System.Windows.Forms.Label();
      this.typeTextBox = new System.Windows.Forms.TextBox();
      this.typeLabel = new System.Windows.Forms.Label();
      this.firstNameTextBox = new System.Windows.Forms.TextBox();
      this.firstNameLabel = new System.Windows.Forms.Label();
      this.familyNameTextBox = new System.Windows.Forms.TextBox();
      this.familyNameLabel = new System.Windows.Forms.Label();
      this.emailAddressTextBox = new System.Windows.Forms.TextBox();
      this.emailAddressLabel = new System.Windows.Forms.Label();
      this.sendButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // idTextBox
      // 
      this.idTextBox.Location = new System.Drawing.Point(174, 3);
      this.idTextBox.Name = "idTextBox";
      this.idTextBox.ReadOnly = true;
      this.idTextBox.Size = new System.Drawing.Size(335, 26);
      this.idTextBox.TabIndex = 8;
      // 
      // idLabel
      // 
      this.idLabel.AutoSize = true;
      this.idLabel.Location = new System.Drawing.Point(3, 6);
      this.idLabel.Name = "idLabel";
      this.idLabel.Size = new System.Drawing.Size(28, 19);
      this.idLabel.TabIndex = 7;
      this.idLabel.Text = "Id:";
      // 
      // typeTextBox
      // 
      this.typeTextBox.Location = new System.Drawing.Point(174, 35);
      this.typeTextBox.Name = "typeTextBox";
      this.typeTextBox.ReadOnly = true;
      this.typeTextBox.Size = new System.Drawing.Size(335, 26);
      this.typeTextBox.TabIndex = 10;
      // 
      // typeLabel
      // 
      this.typeLabel.AutoSize = true;
      this.typeLabel.Location = new System.Drawing.Point(3, 38);
      this.typeLabel.Name = "typeLabel";
      this.typeLabel.Size = new System.Drawing.Size(49, 19);
      this.typeLabel.TabIndex = 9;
      this.typeLabel.Text = "Type:";
      // 
      // firstNameTextBox
      // 
      this.firstNameTextBox.Location = new System.Drawing.Point(174, 67);
      this.firstNameTextBox.Name = "firstNameTextBox";
      this.firstNameTextBox.Size = new System.Drawing.Size(335, 26);
      this.firstNameTextBox.TabIndex = 12;
      this.firstNameTextBox.TextChanged += new System.EventHandler(this.firstNameTextBox_TextChanged);
      // 
      // firstNameLabel
      // 
      this.firstNameLabel.AutoSize = true;
      this.firstNameLabel.Location = new System.Drawing.Point(3, 70);
      this.firstNameLabel.Name = "firstNameLabel";
      this.firstNameLabel.Size = new System.Drawing.Size(91, 19);
      this.firstNameLabel.TabIndex = 11;
      this.firstNameLabel.Text = "First name:";
      // 
      // familyNameTextBox
      // 
      this.familyNameTextBox.Location = new System.Drawing.Point(174, 99);
      this.familyNameTextBox.Name = "familyNameTextBox";
      this.familyNameTextBox.Size = new System.Drawing.Size(335, 26);
      this.familyNameTextBox.TabIndex = 14;
      this.familyNameTextBox.TextChanged += new System.EventHandler(this.familyNameTextBox_TextChanged);
      // 
      // familyNameLabel
      // 
      this.familyNameLabel.AutoSize = true;
      this.familyNameLabel.Location = new System.Drawing.Point(3, 102);
      this.familyNameLabel.Name = "familyNameLabel";
      this.familyNameLabel.Size = new System.Drawing.Size(107, 19);
      this.familyNameLabel.TabIndex = 13;
      this.familyNameLabel.Text = "Family name:";
      // 
      // emailAddressTextBox
      // 
      this.emailAddressTextBox.Location = new System.Drawing.Point(174, 131);
      this.emailAddressTextBox.Name = "emailAddressTextBox";
      this.emailAddressTextBox.Size = new System.Drawing.Size(335, 26);
      this.emailAddressTextBox.TabIndex = 16;
      this.emailAddressTextBox.TextChanged += new System.EventHandler(this.functionTextBox_TextChanged);
      // 
      // emailAddressLabel
      // 
      this.emailAddressLabel.AutoSize = true;
      this.emailAddressLabel.Location = new System.Drawing.Point(3, 134);
      this.emailAddressLabel.Name = "emailAddressLabel";
      this.emailAddressLabel.Size = new System.Drawing.Size(117, 19);
      this.emailAddressLabel.TabIndex = 15;
      this.emailAddressLabel.Text = "Email address:";
      // 
      // sendButton
      // 
      this.sendButton.Enabled = false;
      this.sendButton.Location = new System.Drawing.Point(389, 163);
      this.sendButton.Name = "sendButton";
      this.sendButton.Size = new System.Drawing.Size(120, 32);
      this.sendButton.TabIndex = 17;
      this.sendButton.Text = "&Send";
      this.sendButton.UseVisualStyleBackColor = true;
      this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
      // 
      // CreateRequestItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.sendButton);
      this.Controls.Add(this.emailAddressTextBox);
      this.Controls.Add(this.emailAddressLabel);
      this.Controls.Add(this.familyNameTextBox);
      this.Controls.Add(this.familyNameLabel);
      this.Controls.Add(this.firstNameTextBox);
      this.Controls.Add(this.firstNameLabel);
      this.Controls.Add(this.typeTextBox);
      this.Controls.Add(this.typeLabel);
      this.Controls.Add(this.idTextBox);
      this.Controls.Add(this.idLabel);
      this.Name = "CreateRequestItem";
      this.Size = new System.Drawing.Size(700, 538);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private TextBox idTextBox;
    private Label idLabel;
    private TextBox typeTextBox;
    private Label typeLabel;
    private TextBox firstNameTextBox;
    private Label firstNameLabel;
    private TextBox familyNameTextBox;
    private Label familyNameLabel;
    private TextBox emailAddressTextBox;
    private Label emailAddressLabel;
    private Button sendButton;


  }
}
