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
  partial class CreateCertificateItem
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
      this.typeComboBox = new System.Windows.Forms.ComboBox();
      this.firstNameTextBox = new System.Windows.Forms.TextBox();
      this.familyNameTextBox = new System.Windows.Forms.TextBox();
      this.typeLabel = new System.Windows.Forms.Label();
      this.firstNameLabel = new System.Windows.Forms.Label();
      this.familyNameLabel = new System.Windows.Forms.Label();
      this.functionNameTextBox = new System.Windows.Forms.TextBox();
      this.functionNameLabel = new System.Windows.Forms.Label();
      this.saveButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // typeComboBox
      // 
      this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.typeComboBox.FormattingEnabled = true;
      this.typeComboBox.Location = new System.Drawing.Point(133, 4);
      this.typeComboBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.typeComboBox.Name = "typeComboBox";
      this.typeComboBox.Size = new System.Drawing.Size(255, 22);
      this.typeComboBox.TabIndex = 0;
      this.typeComboBox.SelectedIndexChanged += new System.EventHandler(this.typeComboBox_SelectedIndexChanged);
      // 
      // firstNameTextBox
      // 
      this.firstNameTextBox.Location = new System.Drawing.Point(133, 30);
      this.firstNameTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.firstNameTextBox.Name = "firstNameTextBox";
      this.firstNameTextBox.Size = new System.Drawing.Size(255, 20);
      this.firstNameTextBox.TabIndex = 1;
      this.firstNameTextBox.TextChanged += new System.EventHandler(this.firstNameTextBox_TextChanged);
      // 
      // familyNameTextBox
      // 
      this.familyNameTextBox.Location = new System.Drawing.Point(133, 54);
      this.familyNameTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.familyNameTextBox.Name = "familyNameTextBox";
      this.familyNameTextBox.Size = new System.Drawing.Size(255, 20);
      this.familyNameTextBox.TabIndex = 2;
      this.familyNameTextBox.TextChanged += new System.EventHandler(this.familyNameTextBox_TextChanged);
      // 
      // typeLabel
      // 
      this.typeLabel.AutoSize = true;
      this.typeLabel.Location = new System.Drawing.Point(2, 7);
      this.typeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.typeLabel.Name = "typeLabel";
      this.typeLabel.Size = new System.Drawing.Size(83, 14);
      this.typeLabel.TabIndex = 3;
      this.typeLabel.Text = "Certificate type:";
      // 
      // firstNameLabel
      // 
      this.firstNameLabel.AutoSize = true;
      this.firstNameLabel.Location = new System.Drawing.Point(2, 33);
      this.firstNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.firstNameLabel.Name = "firstNameLabel";
      this.firstNameLabel.Size = new System.Drawing.Size(57, 14);
      this.firstNameLabel.TabIndex = 4;
      this.firstNameLabel.Text = "Firstname:";
      // 
      // familyNameLabel
      // 
      this.familyNameLabel.AutoSize = true;
      this.familyNameLabel.Location = new System.Drawing.Point(2, 57);
      this.familyNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.familyNameLabel.Name = "familyNameLabel";
      this.familyNameLabel.Size = new System.Drawing.Size(69, 14);
      this.familyNameLabel.TabIndex = 5;
      this.familyNameLabel.Text = "Family name:";
      // 
      // functionNameTextBox
      // 
      this.functionNameTextBox.Location = new System.Drawing.Point(133, 78);
      this.functionNameTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.functionNameTextBox.Name = "functionNameTextBox";
      this.functionNameTextBox.Size = new System.Drawing.Size(255, 20);
      this.functionNameTextBox.TabIndex = 6;
      this.functionNameTextBox.TextChanged += new System.EventHandler(this.functionNameTextBox_TextChanged);
      // 
      // functionNameLabel
      // 
      this.functionNameLabel.AutoSize = true;
      this.functionNameLabel.Location = new System.Drawing.Point(2, 81);
      this.functionNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.functionNameLabel.Name = "functionNameLabel";
      this.functionNameLabel.Size = new System.Drawing.Size(77, 14);
      this.functionNameLabel.TabIndex = 7;
      this.functionNameLabel.Text = "Party function:";
      // 
      // saveButton
      // 
      this.saveButton.Location = new System.Drawing.Point(294, 102);
      this.saveButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.saveButton.Name = "saveButton";
      this.saveButton.Size = new System.Drawing.Size(94, 24);
      this.saveButton.TabIndex = 8;
      this.saveButton.Text = "&Save...";
      this.saveButton.UseVisualStyleBackColor = true;
      this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
      // 
      // CreateCertificateItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.saveButton);
      this.Controls.Add(this.functionNameLabel);
      this.Controls.Add(this.functionNameTextBox);
      this.Controls.Add(this.familyNameLabel);
      this.Controls.Add(this.firstNameLabel);
      this.Controls.Add(this.typeLabel);
      this.Controls.Add(this.familyNameTextBox);
      this.Controls.Add(this.firstNameTextBox);
      this.Controls.Add(this.typeComboBox);
      this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
      this.Name = "CreateCertificateItem";
      this.Size = new System.Drawing.Size(467, 359);
      this.Load += new System.EventHandler(this.StartWizardItem_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private ComboBox typeComboBox;
    private TextBox firstNameTextBox;
    private TextBox familyNameTextBox;
    private Label typeLabel;
    private Label firstNameLabel;
    private Label familyNameLabel;
    private TextBox functionNameTextBox;
    private Label functionNameLabel;
    private Button saveButton;
  }
}
