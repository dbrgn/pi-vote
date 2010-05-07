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
      this.spaceLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // idTextBox
      // 
      this.idTextBox.Location = new System.Drawing.Point(116, 2);
      this.idTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.idTextBox.Name = "idTextBox";
      this.idTextBox.ReadOnly = true;
      this.idTextBox.Size = new System.Drawing.Size(262, 20);
      this.idTextBox.TabIndex = 8;
      // 
      // idLabel
      // 
      this.idLabel.AutoSize = true;
      this.idLabel.Location = new System.Drawing.Point(2, 5);
      this.idLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.idLabel.Name = "idLabel";
      this.idLabel.Size = new System.Drawing.Size(18, 14);
      this.idLabel.TabIndex = 7;
      this.idLabel.Text = "Id:";
      // 
      // typeTextBox
      // 
      this.typeTextBox.Location = new System.Drawing.Point(116, 26);
      this.typeTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.typeTextBox.Name = "typeTextBox";
      this.typeTextBox.ReadOnly = true;
      this.typeTextBox.Size = new System.Drawing.Size(262, 20);
      this.typeTextBox.TabIndex = 10;
      // 
      // typeLabel
      // 
      this.typeLabel.AutoSize = true;
      this.typeLabel.Location = new System.Drawing.Point(2, 29);
      this.typeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.typeLabel.Name = "typeLabel";
      this.typeLabel.Size = new System.Drawing.Size(33, 14);
      this.typeLabel.TabIndex = 9;
      this.typeLabel.Text = "Type:";
      // 
      // firstNameTextBox
      // 
      this.firstNameTextBox.Location = new System.Drawing.Point(116, 50);
      this.firstNameTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.firstNameTextBox.Name = "firstNameTextBox";
      this.firstNameTextBox.Size = new System.Drawing.Size(262, 20);
      this.firstNameTextBox.TabIndex = 12;
      this.firstNameTextBox.TextChanged += new System.EventHandler(this.firstNameTextBox_TextChanged);
      // 
      // firstNameLabel
      // 
      this.firstNameLabel.AutoSize = true;
      this.firstNameLabel.Location = new System.Drawing.Point(2, 53);
      this.firstNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.firstNameLabel.Name = "firstNameLabel";
      this.firstNameLabel.Size = new System.Drawing.Size(60, 14);
      this.firstNameLabel.TabIndex = 11;
      this.firstNameLabel.Text = "First name:";
      // 
      // familyNameTextBox
      // 
      this.familyNameTextBox.Location = new System.Drawing.Point(116, 74);
      this.familyNameTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.familyNameTextBox.Name = "familyNameTextBox";
      this.familyNameTextBox.Size = new System.Drawing.Size(262, 20);
      this.familyNameTextBox.TabIndex = 14;
      this.familyNameTextBox.TextChanged += new System.EventHandler(this.familyNameTextBox_TextChanged);
      // 
      // familyNameLabel
      // 
      this.familyNameLabel.AutoSize = true;
      this.familyNameLabel.Location = new System.Drawing.Point(2, 77);
      this.familyNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.familyNameLabel.Name = "familyNameLabel";
      this.familyNameLabel.Size = new System.Drawing.Size(69, 14);
      this.familyNameLabel.TabIndex = 13;
      this.familyNameLabel.Text = "Family name:";
      // 
      // emailAddressTextBox
      // 
      this.emailAddressTextBox.Location = new System.Drawing.Point(116, 98);
      this.emailAddressTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.emailAddressTextBox.Name = "emailAddressTextBox";
      this.emailAddressTextBox.Size = new System.Drawing.Size(262, 20);
      this.emailAddressTextBox.TabIndex = 16;
      this.emailAddressTextBox.TextChanged += new System.EventHandler(this.functionTextBox_TextChanged);
      // 
      // emailAddressLabel
      // 
      this.emailAddressLabel.AutoSize = true;
      this.emailAddressLabel.Location = new System.Drawing.Point(2, 101);
      this.emailAddressLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.emailAddressLabel.Name = "emailAddressLabel";
      this.emailAddressLabel.Size = new System.Drawing.Size(77, 14);
      this.emailAddressLabel.TabIndex = 15;
      this.emailAddressLabel.Text = "Email address:";
      // 
      // sendButton
      // 
      this.sendButton.Enabled = false;
      this.sendButton.Location = new System.Drawing.Point(240, 122);
      this.sendButton.Margin = new System.Windows.Forms.Padding(2);
      this.sendButton.Name = "sendButton";
      this.sendButton.Size = new System.Drawing.Size(138, 25);
      this.sendButton.TabIndex = 17;
      this.sendButton.Text = "Print and &Send";
      this.sendButton.UseVisualStyleBackColor = true;
      this.sendButton.Click += new System.EventHandler(this.sendButton_Click);
      // 
      // spaceLabel
      // 
      this.spaceLabel.AutoSize = true;
      this.spaceLabel.ForeColor = System.Drawing.Color.Red;
      this.spaceLabel.Location = new System.Drawing.Point(70, 253);
      this.spaceLabel.Name = "spaceLabel";
      this.spaceLabel.Size = new System.Drawing.Size(274, 14);
      this.spaceLabel.TabIndex = 18;
      this.spaceLabel.Text = "This space shall be occupied by an appropriate picture.";
      // 
      // CreateRequestItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.spaceLabel);
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
      this.Margin = new System.Windows.Forms.Padding(1);
      this.Name = "CreateRequestItem";
      this.Size = new System.Drawing.Size(467, 359);
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
    private Label spaceLabel;


  }
}
