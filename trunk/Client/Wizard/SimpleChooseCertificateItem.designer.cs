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
  partial class SimpleChooseCertificateItem
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimpleChooseCertificateItem));
      this.createRadioButton = new System.Windows.Forms.RadioButton();
      this.importRadioButton = new System.Windows.Forms.RadioButton();
      this.advancedRadioButton = new System.Windows.Forms.RadioButton();
      this.emailAddressTextBox = new System.Windows.Forms.TextBox();
      this.emailAddressLabel = new System.Windows.Forms.Label();
      this.familyNameTextBox = new System.Windows.Forms.TextBox();
      this.familyNameLabel = new System.Windows.Forms.Label();
      this.firstNameTextBox = new System.Windows.Forms.TextBox();
      this.firstNameLabel = new System.Windows.Forms.Label();
      this.headerLabel = new System.Windows.Forms.Label();
      this.explainCreateLabel = new System.Windows.Forms.Label();
      this.createButton = new System.Windows.Forms.Button();
      this.printButton = new System.Windows.Forms.Button();
      this.uploadButton = new System.Windows.Forms.Button();
      this.importButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // createRadioButton
      // 
      this.createRadioButton.AutoSize = true;
      this.createRadioButton.Location = new System.Drawing.Point(0, 23);
      this.createRadioButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.createRadioButton.Name = "createRadioButton";
      this.createRadioButton.Size = new System.Drawing.Size(139, 18);
      this.createRadioButton.TabIndex = 9;
      this.createRadioButton.TabStop = true;
      this.createRadioButton.Text = "I need a new certificate";
      this.createRadioButton.UseVisualStyleBackColor = true;
      this.createRadioButton.CheckedChanged += new System.EventHandler(this.createRadioButton_CheckedChanged);
      // 
      // importRadioButton
      // 
      this.importRadioButton.AutoSize = true;
      this.importRadioButton.Location = new System.Drawing.Point(0, 265);
      this.importRadioButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.importRadioButton.Name = "importRadioButton";
      this.importRadioButton.Size = new System.Drawing.Size(153, 18);
      this.importRadioButton.TabIndex = 10;
      this.importRadioButton.TabStop = true;
      this.importRadioButton.Text = "I already have a certificate";
      this.importRadioButton.UseVisualStyleBackColor = true;
      this.importRadioButton.CheckedChanged += new System.EventHandler(this.importReadioButton_CheckedChanged);
      // 
      // advancedRadioButton
      // 
      this.advancedRadioButton.AutoSize = true;
      this.advancedRadioButton.Location = new System.Drawing.Point(0, 319);
      this.advancedRadioButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.advancedRadioButton.Name = "advancedRadioButton";
      this.advancedRadioButton.Size = new System.Drawing.Size(136, 18);
      this.advancedRadioButton.TabIndex = 11;
      this.advancedRadioButton.TabStop = true;
      this.advancedRadioButton.Text = "Show me more options";
      this.advancedRadioButton.UseVisualStyleBackColor = true;
      this.advancedRadioButton.CheckedChanged += new System.EventHandler(this.advancedRadioButton_CheckedChanged);
      // 
      // emailAddressTextBox
      // 
      this.emailAddressTextBox.Location = new System.Drawing.Point(130, 211);
      this.emailAddressTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.emailAddressTextBox.Name = "emailAddressTextBox";
      this.emailAddressTextBox.Size = new System.Drawing.Size(403, 20);
      this.emailAddressTextBox.TabIndex = 22;
      this.emailAddressTextBox.TextChanged += new System.EventHandler(this.emailAddressTextBox_TextChanged);
      // 
      // emailAddressLabel
      // 
      this.emailAddressLabel.AutoSize = true;
      this.emailAddressLabel.Location = new System.Drawing.Point(16, 213);
      this.emailAddressLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.emailAddressLabel.Name = "emailAddressLabel";
      this.emailAddressLabel.Size = new System.Drawing.Size(77, 14);
      this.emailAddressLabel.TabIndex = 21;
      this.emailAddressLabel.Text = "Email address:";
      // 
      // familyNameTextBox
      // 
      this.familyNameTextBox.Location = new System.Drawing.Point(130, 190);
      this.familyNameTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.familyNameTextBox.Name = "familyNameTextBox";
      this.familyNameTextBox.Size = new System.Drawing.Size(403, 20);
      this.familyNameTextBox.TabIndex = 20;
      this.familyNameTextBox.TextChanged += new System.EventHandler(this.familyNameTextBox_TextChanged);
      // 
      // familyNameLabel
      // 
      this.familyNameLabel.AutoSize = true;
      this.familyNameLabel.Location = new System.Drawing.Point(16, 192);
      this.familyNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.familyNameLabel.Name = "familyNameLabel";
      this.familyNameLabel.Size = new System.Drawing.Size(69, 14);
      this.familyNameLabel.TabIndex = 19;
      this.familyNameLabel.Text = "Family name:";
      // 
      // firstNameTextBox
      // 
      this.firstNameTextBox.Location = new System.Drawing.Point(130, 169);
      this.firstNameTextBox.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.firstNameTextBox.Name = "firstNameTextBox";
      this.firstNameTextBox.Size = new System.Drawing.Size(403, 20);
      this.firstNameTextBox.TabIndex = 18;
      this.firstNameTextBox.TextChanged += new System.EventHandler(this.firstNameTextBox_TextChanged);
      // 
      // firstNameLabel
      // 
      this.firstNameLabel.AutoSize = true;
      this.firstNameLabel.Location = new System.Drawing.Point(16, 171);
      this.firstNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.firstNameLabel.Name = "firstNameLabel";
      this.firstNameLabel.Size = new System.Drawing.Size(60, 14);
      this.firstNameLabel.TabIndex = 17;
      this.firstNameLabel.Text = "First name:";
      // 
      // headerLabel
      // 
      this.headerLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.headerLabel.Location = new System.Drawing.Point(2, 0);
      this.headerLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.headerLabel.Name = "headerLabel";
      this.headerLabel.Size = new System.Drawing.Size(551, 23);
      this.headerLabel.TabIndex = 23;
      this.headerLabel.Text = "In order to vote you need a certificate proofing your identitiy.\r\n";
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
      // createButton
      // 
      this.createButton.Location = new System.Drawing.Point(130, 233);
      this.createButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.createButton.Name = "createButton";
      this.createButton.Size = new System.Drawing.Size(131, 25);
      this.createButton.TabIndex = 25;
      this.createButton.Text = "&Create";
      this.createButton.UseVisualStyleBackColor = true;
      this.createButton.Click += new System.EventHandler(this.createButton_Click);
      // 
      // printButton
      // 
      this.printButton.Location = new System.Drawing.Point(265, 233);
      this.printButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.printButton.Name = "printButton";
      this.printButton.Size = new System.Drawing.Size(131, 25);
      this.printButton.TabIndex = 26;
      this.printButton.Text = "&Print";
      this.printButton.UseVisualStyleBackColor = true;
      this.printButton.Click += new System.EventHandler(this.printButton_Click);
      // 
      // uploadButton
      // 
      this.uploadButton.Location = new System.Drawing.Point(401, 233);
      this.uploadButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.uploadButton.Name = "uploadButton";
      this.uploadButton.Size = new System.Drawing.Size(131, 25);
      this.uploadButton.TabIndex = 27;
      this.uploadButton.Text = "&Upload";
      this.uploadButton.UseVisualStyleBackColor = true;
      this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
      // 
      // importButton
      // 
      this.importButton.Location = new System.Drawing.Point(130, 285);
      this.importButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.importButton.Name = "importButton";
      this.importButton.Size = new System.Drawing.Size(131, 25);
      this.importButton.TabIndex = 28;
      this.importButton.Text = "&Import";
      this.importButton.UseVisualStyleBackColor = true;
      this.importButton.Click += new System.EventHandler(this.importButton_Click);
      // 
      // SimpleChooseCertificateItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.importButton);
      this.Controls.Add(this.uploadButton);
      this.Controls.Add(this.printButton);
      this.Controls.Add(this.createButton);
      this.Controls.Add(this.explainCreateLabel);
      this.Controls.Add(this.headerLabel);
      this.Controls.Add(this.emailAddressTextBox);
      this.Controls.Add(this.emailAddressLabel);
      this.Controls.Add(this.familyNameTextBox);
      this.Controls.Add(this.familyNameLabel);
      this.Controls.Add(this.firstNameTextBox);
      this.Controls.Add(this.firstNameLabel);
      this.Controls.Add(this.advancedRadioButton);
      this.Controls.Add(this.importRadioButton);
      this.Controls.Add(this.createRadioButton);
      this.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
      this.Name = "SimpleChooseCertificateItem";
      this.Size = new System.Drawing.Size(555, 359);
      this.Load += new System.EventHandler(this.StartWizardItem_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private RadioButton createRadioButton;
    private RadioButton importRadioButton;
    private RadioButton advancedRadioButton;
    private TextBox emailAddressTextBox;
    private Label emailAddressLabel;
    private TextBox familyNameTextBox;
    private Label familyNameLabel;
    private TextBox firstNameTextBox;
    private Label firstNameLabel;
    private Label headerLabel;
    private Label explainCreateLabel;
    private Button createButton;
    private Button printButton;
    private Button uploadButton;
    private Button importButton;

  }
}
