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
  partial class SimpleCreateCertificateItem
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleCreateCertificateItem));
      this.functionNameLabel = new System.Windows.Forms.Label();
      this.functionNameTextBox = new System.Windows.Forms.TextBox();
      this.typeLabel = new System.Windows.Forms.Label();
      this.typeComboBox = new System.Windows.Forms.ComboBox();
      this.uploadButton = new System.Windows.Forms.Button();
      this.printButton = new System.Windows.Forms.Button();
      this.createButton = new System.Windows.Forms.Button();
      this.explainCreateLabel = new System.Windows.Forms.Label();
      this.emailAddressTextBox = new System.Windows.Forms.TextBox();
      this.emailAddressLabel = new System.Windows.Forms.Label();
      this.familyNameTextBox = new System.Windows.Forms.TextBox();
      this.familyNameLabel = new System.Windows.Forms.Label();
      this.firstNameTextBox = new System.Windows.Forms.TextBox();
      this.firstNameLabel = new System.Windows.Forms.Label();
      this.cantonComboBox = new System.Windows.Forms.ComboBox();
      this.cantonLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // functionNameLabel
      // 
      this.functionNameLabel.AutoSize = true;
      this.functionNameLabel.Location = new System.Drawing.Point(16, 249);
      this.functionNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.functionNameLabel.Name = "functionNameLabel";
      this.functionNameLabel.Size = new System.Drawing.Size(77, 14);
      this.functionNameLabel.TabIndex = 31;
      this.functionNameLabel.Text = "Party function:";
      // 
      // functionNameTextBox
      // 
      this.functionNameTextBox.Location = new System.Drawing.Point(130, 246);
      this.functionNameTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.functionNameTextBox.Name = "functionNameTextBox";
      this.functionNameTextBox.Size = new System.Drawing.Size(402, 20);
      this.functionNameTextBox.TabIndex = 3;
      this.functionNameTextBox.TextChanged += new System.EventHandler(this.functionNameTextBox_TextChanged);
      // 
      // typeLabel
      // 
      this.typeLabel.AutoSize = true;
      this.typeLabel.Location = new System.Drawing.Point(16, 176);
      this.typeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.typeLabel.Name = "typeLabel";
      this.typeLabel.Size = new System.Drawing.Size(83, 14);
      this.typeLabel.TabIndex = 29;
      this.typeLabel.Text = "Certificate type:";
      // 
      // typeComboBox
      // 
      this.typeComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.typeComboBox.FormattingEnabled = true;
      this.typeComboBox.Location = new System.Drawing.Point(130, 173);
      this.typeComboBox.Margin = new System.Windows.Forms.Padding(2);
      this.typeComboBox.Name = "typeComboBox";
      this.typeComboBox.Size = new System.Drawing.Size(402, 22);
      this.typeComboBox.TabIndex = 0;
      this.typeComboBox.SelectedIndexChanged += new System.EventHandler(this.typeComboBox_SelectedIndexChanged);
      // 
      // uploadButton
      // 
      this.uploadButton.Location = new System.Drawing.Point(402, 320);
      this.uploadButton.Margin = new System.Windows.Forms.Padding(2);
      this.uploadButton.Name = "uploadButton";
      this.uploadButton.Size = new System.Drawing.Size(131, 25);
      this.uploadButton.TabIndex = 7;
      this.uploadButton.Text = "&Upload";
      this.uploadButton.UseVisualStyleBackColor = true;
      this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
      // 
      // printButton
      // 
      this.printButton.Location = new System.Drawing.Point(266, 320);
      this.printButton.Margin = new System.Windows.Forms.Padding(2);
      this.printButton.Name = "printButton";
      this.printButton.Size = new System.Drawing.Size(131, 25);
      this.printButton.TabIndex = 6;
      this.printButton.Text = "&Print";
      this.printButton.UseVisualStyleBackColor = true;
      this.printButton.Click += new System.EventHandler(this.printButton_Click);
      // 
      // createButton
      // 
      this.createButton.Location = new System.Drawing.Point(131, 320);
      this.createButton.Margin = new System.Windows.Forms.Padding(2);
      this.createButton.Name = "createButton";
      this.createButton.Size = new System.Drawing.Size(131, 25);
      this.createButton.TabIndex = 5;
      this.createButton.Text = "&Create";
      this.createButton.UseVisualStyleBackColor = true;
      this.createButton.Click += new System.EventHandler(this.createButton_Click);
      // 
      // explainCreateLabel
      // 
      this.explainCreateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.explainCreateLabel.Location = new System.Drawing.Point(16, 41);
      this.explainCreateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.explainCreateLabel.Name = "explainCreateLabel";
      this.explainCreateLabel.Size = new System.Drawing.Size(539, 126);
      this.explainCreateLabel.TabIndex = 24;
      this.explainCreateLabel.Text = resources.GetString("explainCreateLabel.Text");
      // 
      // emailAddressTextBox
      // 
      this.emailAddressTextBox.Location = new System.Drawing.Point(129, 270);
      this.emailAddressTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.emailAddressTextBox.Name = "emailAddressTextBox";
      this.emailAddressTextBox.Size = new System.Drawing.Size(403, 20);
      this.emailAddressTextBox.TabIndex = 4;
      this.emailAddressTextBox.TextChanged += new System.EventHandler(this.emailAddressTextBox_TextChanged);
      // 
      // emailAddressLabel
      // 
      this.emailAddressLabel.AutoSize = true;
      this.emailAddressLabel.Location = new System.Drawing.Point(16, 273);
      this.emailAddressLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.emailAddressLabel.Name = "emailAddressLabel";
      this.emailAddressLabel.Size = new System.Drawing.Size(77, 14);
      this.emailAddressLabel.TabIndex = 21;
      this.emailAddressLabel.Text = "Email address:";
      // 
      // familyNameTextBox
      // 
      this.familyNameTextBox.Location = new System.Drawing.Point(130, 222);
      this.familyNameTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.familyNameTextBox.Name = "familyNameTextBox";
      this.familyNameTextBox.Size = new System.Drawing.Size(403, 20);
      this.familyNameTextBox.TabIndex = 2;
      this.familyNameTextBox.TextChanged += new System.EventHandler(this.familyNameTextBox_TextChanged);
      // 
      // familyNameLabel
      // 
      this.familyNameLabel.AutoSize = true;
      this.familyNameLabel.Location = new System.Drawing.Point(16, 225);
      this.familyNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.familyNameLabel.Name = "familyNameLabel";
      this.familyNameLabel.Size = new System.Drawing.Size(69, 14);
      this.familyNameLabel.TabIndex = 19;
      this.familyNameLabel.Text = "Family name:";
      // 
      // firstNameTextBox
      // 
      this.firstNameTextBox.Location = new System.Drawing.Point(130, 198);
      this.firstNameTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.firstNameTextBox.Name = "firstNameTextBox";
      this.firstNameTextBox.Size = new System.Drawing.Size(403, 20);
      this.firstNameTextBox.TabIndex = 1;
      this.firstNameTextBox.TextChanged += new System.EventHandler(this.firstNameTextBox_TextChanged);
      // 
      // firstNameLabel
      // 
      this.firstNameLabel.AutoSize = true;
      this.firstNameLabel.Location = new System.Drawing.Point(16, 201);
      this.firstNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.firstNameLabel.Name = "firstNameLabel";
      this.firstNameLabel.Size = new System.Drawing.Size(60, 14);
      this.firstNameLabel.TabIndex = 17;
      this.firstNameLabel.Text = "First name:";
      // 
      // cantonComboBox
      // 
      this.cantonComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.cantonComboBox.FormattingEnabled = true;
      this.cantonComboBox.Location = new System.Drawing.Point(129, 294);
      this.cantonComboBox.Margin = new System.Windows.Forms.Padding(2);
      this.cantonComboBox.Name = "cantonComboBox";
      this.cantonComboBox.Size = new System.Drawing.Size(402, 22);
      this.cantonComboBox.TabIndex = 32;
      // 
      // cantonLabel
      // 
      this.cantonLabel.AutoSize = true;
      this.cantonLabel.Location = new System.Drawing.Point(16, 297);
      this.cantonLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.cantonLabel.Name = "cantonLabel";
      this.cantonLabel.Size = new System.Drawing.Size(44, 14);
      this.cantonLabel.TabIndex = 33;
      this.cantonLabel.Text = "Canton:";
      // 
      // SimpleCreateCertificateItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.cantonLabel);
      this.Controls.Add(this.cantonComboBox);
      this.Controls.Add(this.functionNameLabel);
      this.Controls.Add(this.functionNameTextBox);
      this.Controls.Add(this.typeLabel);
      this.Controls.Add(this.typeComboBox);
      this.Controls.Add(this.uploadButton);
      this.Controls.Add(this.printButton);
      this.Controls.Add(this.createButton);
      this.Controls.Add(this.explainCreateLabel);
      this.Controls.Add(this.emailAddressTextBox);
      this.Controls.Add(this.emailAddressLabel);
      this.Controls.Add(this.familyNameTextBox);
      this.Controls.Add(this.familyNameLabel);
      this.Controls.Add(this.firstNameTextBox);
      this.Controls.Add(this.firstNameLabel);
      this.Margin = new System.Windows.Forms.Padding(1);
      this.Name = "SimpleCreateCertificateItem";
      this.Size = new System.Drawing.Size(555, 359);
      this.Load += new System.EventHandler(this.StartWizardItem_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private TextBox emailAddressTextBox;
    private Label emailAddressLabel;
    private TextBox familyNameTextBox;
    private Label familyNameLabel;
    private TextBox firstNameTextBox;
    private Label firstNameLabel;
    private Label explainCreateLabel;
    private Button createButton;
    private Button printButton;
    private Button uploadButton;
    private Label functionNameLabel;
    private TextBox functionNameTextBox;
    private Label typeLabel;
    private ComboBox typeComboBox;
    private ComboBox cantonComboBox;
    private Label cantonLabel;

  }
}
