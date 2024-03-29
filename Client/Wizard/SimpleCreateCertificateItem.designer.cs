﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Pirate.PiVote.Gui;

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
      this.groupComboBox = new Pirate.PiVote.Gui.GroupComboBox();
      this.groupLabel = new System.Windows.Forms.Label();
      this.emailNotificationCheckBox = new System.Windows.Forms.CheckBox();
      this.mainPanel = new System.Windows.Forms.Panel();
      this.mainPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // functionNameLabel
      // 
      this.functionNameLabel.AutoSize = true;
      this.functionNameLabel.Location = new System.Drawing.Point(8, 208);
      this.functionNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.functionNameLabel.Name = "functionNameLabel";
      this.functionNameLabel.Size = new System.Drawing.Size(77, 14);
      this.functionNameLabel.TabIndex = 31;
      this.functionNameLabel.Text = "Party function:";
      // 
      // functionNameTextBox
      // 
      this.functionNameTextBox.Location = new System.Drawing.Point(122, 205);
      this.functionNameTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.functionNameTextBox.Name = "functionNameTextBox";
      this.functionNameTextBox.Size = new System.Drawing.Size(402, 20);
      this.functionNameTextBox.TabIndex = 3;
      this.functionNameTextBox.TextChanged += new System.EventHandler(this.functionNameTextBox_TextChanged);
      // 
      // typeLabel
      // 
      this.typeLabel.AutoSize = true;
      this.typeLabel.Location = new System.Drawing.Point(8, 135);
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
      this.typeComboBox.Location = new System.Drawing.Point(122, 132);
      this.typeComboBox.Margin = new System.Windows.Forms.Padding(2);
      this.typeComboBox.Name = "typeComboBox";
      this.typeComboBox.Size = new System.Drawing.Size(402, 22);
      this.typeComboBox.TabIndex = 0;
      this.typeComboBox.SelectedIndexChanged += new System.EventHandler(this.typeComboBox_SelectedIndexChanged);
      // 
      // uploadButton
      // 
      this.uploadButton.Location = new System.Drawing.Point(394, 328);
      this.uploadButton.Margin = new System.Windows.Forms.Padding(2);
      this.uploadButton.Name = "uploadButton";
      this.uploadButton.Size = new System.Drawing.Size(131, 25);
      this.uploadButton.TabIndex = 9;
      this.uploadButton.Text = "&Upload";
      this.uploadButton.UseVisualStyleBackColor = true;
      this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
      // 
      // printButton
      // 
      this.printButton.Location = new System.Drawing.Point(258, 328);
      this.printButton.Margin = new System.Windows.Forms.Padding(2);
      this.printButton.Name = "printButton";
      this.printButton.Size = new System.Drawing.Size(131, 25);
      this.printButton.TabIndex = 8;
      this.printButton.Text = "&Print";
      this.printButton.UseVisualStyleBackColor = true;
      this.printButton.Click += new System.EventHandler(this.printButton_Click);
      // 
      // createButton
      // 
      this.createButton.Location = new System.Drawing.Point(123, 328);
      this.createButton.Margin = new System.Windows.Forms.Padding(2);
      this.createButton.Name = "createButton";
      this.createButton.Size = new System.Drawing.Size(131, 25);
      this.createButton.TabIndex = 7;
      this.createButton.Text = "&Create";
      this.createButton.UseVisualStyleBackColor = true;
      this.createButton.Click += new System.EventHandler(this.createButton_Click);
      // 
      // explainCreateLabel
      // 
      this.explainCreateLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.explainCreateLabel.Location = new System.Drawing.Point(8, 0);
      this.explainCreateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.explainCreateLabel.Name = "explainCreateLabel";
      this.explainCreateLabel.Size = new System.Drawing.Size(545, 126);
      this.explainCreateLabel.TabIndex = 24;
      this.explainCreateLabel.Text = resources.GetString("explainCreateLabel.Text");
      // 
      // emailAddressTextBox
      // 
      this.emailAddressTextBox.Location = new System.Drawing.Point(121, 229);
      this.emailAddressTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.emailAddressTextBox.Name = "emailAddressTextBox";
      this.emailAddressTextBox.Size = new System.Drawing.Size(403, 20);
      this.emailAddressTextBox.TabIndex = 4;
      this.emailAddressTextBox.TextChanged += new System.EventHandler(this.emailAddressTextBox_TextChanged);
      // 
      // emailAddressLabel
      // 
      this.emailAddressLabel.AutoSize = true;
      this.emailAddressLabel.Location = new System.Drawing.Point(8, 232);
      this.emailAddressLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.emailAddressLabel.Name = "emailAddressLabel";
      this.emailAddressLabel.Size = new System.Drawing.Size(77, 14);
      this.emailAddressLabel.TabIndex = 21;
      this.emailAddressLabel.Text = "Email address:";
      // 
      // familyNameTextBox
      // 
      this.familyNameTextBox.Location = new System.Drawing.Point(122, 181);
      this.familyNameTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.familyNameTextBox.Name = "familyNameTextBox";
      this.familyNameTextBox.Size = new System.Drawing.Size(403, 20);
      this.familyNameTextBox.TabIndex = 2;
      this.familyNameTextBox.TextChanged += new System.EventHandler(this.familyNameTextBox_TextChanged);
      // 
      // familyNameLabel
      // 
      this.familyNameLabel.AutoSize = true;
      this.familyNameLabel.Location = new System.Drawing.Point(8, 184);
      this.familyNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.familyNameLabel.Name = "familyNameLabel";
      this.familyNameLabel.Size = new System.Drawing.Size(69, 14);
      this.familyNameLabel.TabIndex = 19;
      this.familyNameLabel.Text = "Family name:";
      // 
      // firstNameTextBox
      // 
      this.firstNameTextBox.Location = new System.Drawing.Point(122, 157);
      this.firstNameTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.firstNameTextBox.Name = "firstNameTextBox";
      this.firstNameTextBox.Size = new System.Drawing.Size(403, 20);
      this.firstNameTextBox.TabIndex = 1;
      this.firstNameTextBox.TextChanged += new System.EventHandler(this.firstNameTextBox_TextChanged);
      // 
      // firstNameLabel
      // 
      this.firstNameLabel.AutoSize = true;
      this.firstNameLabel.Location = new System.Drawing.Point(8, 160);
      this.firstNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.firstNameLabel.Name = "firstNameLabel";
      this.firstNameLabel.Size = new System.Drawing.Size(60, 14);
      this.firstNameLabel.TabIndex = 17;
      this.firstNameLabel.Text = "First name:";
      // 
      // groupComboBox
      // 
      this.groupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.groupComboBox.FormattingEnabled = true;
      this.groupComboBox.Location = new System.Drawing.Point(121, 302);
      this.groupComboBox.Margin = new System.Windows.Forms.Padding(2);
      this.groupComboBox.Name = "groupComboBox";
      this.groupComboBox.Size = new System.Drawing.Size(402, 22);
      this.groupComboBox.TabIndex = 6;
      this.groupComboBox.Value = null;
      this.groupComboBox.SelectedIndexChanged += new System.EventHandler(this.cantonComboBox_SelectedIndexChanged);
      // 
      // groupLabel
      // 
      this.groupLabel.AutoSize = true;
      this.groupLabel.Location = new System.Drawing.Point(8, 305);
      this.groupLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.groupLabel.Name = "groupLabel";
      this.groupLabel.Size = new System.Drawing.Size(40, 14);
      this.groupLabel.TabIndex = 33;
      this.groupLabel.Text = "Group:";
      // 
      // emailNotificationCheckBox
      // 
      this.emailNotificationCheckBox.Location = new System.Drawing.Point(121, 254);
      this.emailNotificationCheckBox.Name = "emailNotificationCheckBox";
      this.emailNotificationCheckBox.Size = new System.Drawing.Size(402, 43);
      this.emailNotificationCheckBox.TabIndex = 5;
      this.emailNotificationCheckBox.Text = "Notify me by email, when an answer to my certificate signing request is available" +
          ". This requires your email address to be stored on the PiVote server.";
      this.emailNotificationCheckBox.UseVisualStyleBackColor = true;
      // 
      // mainPanel
      // 
      this.mainPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.mainPanel.AutoScroll = true;
      this.mainPanel.Controls.Add(this.explainCreateLabel);
      this.mainPanel.Controls.Add(this.emailNotificationCheckBox);
      this.mainPanel.Controls.Add(this.firstNameLabel);
      this.mainPanel.Controls.Add(this.groupLabel);
      this.mainPanel.Controls.Add(this.firstNameTextBox);
      this.mainPanel.Controls.Add(this.groupComboBox);
      this.mainPanel.Controls.Add(this.familyNameLabel);
      this.mainPanel.Controls.Add(this.functionNameLabel);
      this.mainPanel.Controls.Add(this.familyNameTextBox);
      this.mainPanel.Controls.Add(this.functionNameTextBox);
      this.mainPanel.Controls.Add(this.emailAddressLabel);
      this.mainPanel.Controls.Add(this.typeLabel);
      this.mainPanel.Controls.Add(this.emailAddressTextBox);
      this.mainPanel.Controls.Add(this.typeComboBox);
      this.mainPanel.Controls.Add(this.createButton);
      this.mainPanel.Controls.Add(this.uploadButton);
      this.mainPanel.Controls.Add(this.printButton);
      this.mainPanel.Location = new System.Drawing.Point(0, 0);
      this.mainPanel.Name = "mainPanel";
      this.mainPanel.Size = new System.Drawing.Size(555, 364);
      this.mainPanel.TabIndex = 34;
      // 
      // SimpleCreateCertificateItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.mainPanel);
      this.Margin = new System.Windows.Forms.Padding(1);
      this.Name = "SimpleCreateCertificateItem";
      this.Size = new System.Drawing.Size(555, 364);
      this.Load += new System.EventHandler(this.StartWizardItem_Load);
      this.mainPanel.ResumeLayout(false);
      this.mainPanel.PerformLayout();
      this.ResumeLayout(false);

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
    private GroupComboBox groupComboBox;
    private Label groupLabel;
    private CheckBox emailNotificationCheckBox;
    private Panel mainPanel;

  }
}
