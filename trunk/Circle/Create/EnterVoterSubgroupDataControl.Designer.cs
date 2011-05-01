﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle.Create
{
  partial class EnterVoterSubgroupDataControl
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
      this.groupLabel = new System.Windows.Forms.Label();
      this.groupComboBox = new Pirate.PiVote.Gui.GroupComboBox();
      this.cancelButton = new System.Windows.Forms.Button();
      this.nextButton = new System.Windows.Forms.Button();
      this.baseCertificateComboBox = new System.Windows.Forms.ComboBox();
      this.baseCertificateLabel = new System.Windows.Forms.Label();
      this.baseValidUnitlTextBox = new System.Windows.Forms.TextBox();
      this.baseValidUntilLabel = new System.Windows.Forms.Label();
      this.emailNotificationCheckBox = new System.Windows.Forms.CheckBox();
      this.firstNameLabel = new System.Windows.Forms.Label();
      this.firstNameTextBox = new System.Windows.Forms.TextBox();
      this.familyNameLabel = new System.Windows.Forms.Label();
      this.familyNameTextBox = new System.Windows.Forms.TextBox();
      this.emailAddressLabel = new System.Windows.Forms.Label();
      this.emailAddressTextBox = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // groupLabel
      // 
      this.groupLabel.AutoSize = true;
      this.groupLabel.Location = new System.Drawing.Point(2, 183);
      this.groupLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.groupLabel.Name = "groupLabel";
      this.groupLabel.Size = new System.Drawing.Size(40, 14);
      this.groupLabel.TabIndex = 44;
      this.groupLabel.Text = "Group:";
      // 
      // groupComboBox
      // 
      this.groupComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.groupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.groupComboBox.FormattingEnabled = true;
      this.groupComboBox.Location = new System.Drawing.Point(115, 180);
      this.groupComboBox.Margin = new System.Windows.Forms.Padding(2);
      this.groupComboBox.Name = "groupComboBox";
      this.groupComboBox.Size = new System.Drawing.Size(430, 22);
      this.groupComboBox.TabIndex = 39;
      this.groupComboBox.Value = null;
      this.groupComboBox.SelectedIndexChanged += new System.EventHandler(this.groupComboBox_SelectedIndexChanged);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(283, 216);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(128, 30);
      this.cancelButton.TabIndex = 47;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // nextButton
      // 
      this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.nextButton.Font = new System.Drawing.Font("Arial", 8.25F);
      this.nextButton.Location = new System.Drawing.Point(417, 216);
      this.nextButton.Name = "nextButton";
      this.nextButton.Size = new System.Drawing.Size(128, 30);
      this.nextButton.TabIndex = 46;
      this.nextButton.Text = "&Next";
      this.nextButton.UseVisualStyleBackColor = true;
      this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
      // 
      // baseCertificateComboBox
      // 
      this.baseCertificateComboBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.baseCertificateComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.baseCertificateComboBox.FormattingEnabled = true;
      this.baseCertificateComboBox.Location = new System.Drawing.Point(117, 1);
      this.baseCertificateComboBox.Name = "baseCertificateComboBox";
      this.baseCertificateComboBox.Size = new System.Drawing.Size(428, 22);
      this.baseCertificateComboBox.TabIndex = 48;
      this.baseCertificateComboBox.SelectedIndexChanged += new System.EventHandler(this.baseCertificateComboBox_SelectedIndexChanged);
      // 
      // baseCertificateLabel
      // 
      this.baseCertificateLabel.AutoSize = true;
      this.baseCertificateLabel.Location = new System.Drawing.Point(5, 4);
      this.baseCertificateLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.baseCertificateLabel.Name = "baseCertificateLabel";
      this.baseCertificateLabel.Size = new System.Drawing.Size(86, 14);
      this.baseCertificateLabel.TabIndex = 49;
      this.baseCertificateLabel.Text = "Base certificate:";
      // 
      // baseValidUnitlTextBox
      // 
      this.baseValidUnitlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.baseValidUnitlTextBox.Location = new System.Drawing.Point(115, 26);
      this.baseValidUnitlTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.baseValidUnitlTextBox.Name = "baseValidUnitlTextBox";
      this.baseValidUnitlTextBox.ReadOnly = true;
      this.baseValidUnitlTextBox.Size = new System.Drawing.Size(430, 20);
      this.baseValidUnitlTextBox.TabIndex = 50;
      // 
      // baseValidUntilLabel
      // 
      this.baseValidUntilLabel.AutoSize = true;
      this.baseValidUntilLabel.Location = new System.Drawing.Point(5, 29);
      this.baseValidUntilLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.baseValidUntilLabel.Name = "baseValidUntilLabel";
      this.baseValidUntilLabel.Size = new System.Drawing.Size(55, 14);
      this.baseValidUntilLabel.TabIndex = 51;
      this.baseValidUntilLabel.Text = "Valid until:";
      // 
      // emailNotificationCheckBox
      // 
      this.emailNotificationCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.emailNotificationCheckBox.Location = new System.Drawing.Point(115, 123);
      this.emailNotificationCheckBox.Name = "emailNotificationCheckBox";
      this.emailNotificationCheckBox.Size = new System.Drawing.Size(430, 46);
      this.emailNotificationCheckBox.TabIndex = 55;
      this.emailNotificationCheckBox.Text = "Notify me by email, when an answer to my certificate signing request is available" +
          ". This requires your email address to be stored on the PiVote server.";
      this.emailNotificationCheckBox.UseVisualStyleBackColor = true;
      // 
      // firstNameLabel
      // 
      this.firstNameLabel.AutoSize = true;
      this.firstNameLabel.Location = new System.Drawing.Point(4, 53);
      this.firstNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.firstNameLabel.Name = "firstNameLabel";
      this.firstNameLabel.Size = new System.Drawing.Size(60, 14);
      this.firstNameLabel.TabIndex = 56;
      this.firstNameLabel.Text = "First name:";
      // 
      // firstNameTextBox
      // 
      this.firstNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.firstNameTextBox.Location = new System.Drawing.Point(115, 50);
      this.firstNameTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.firstNameTextBox.Name = "firstNameTextBox";
      this.firstNameTextBox.Size = new System.Drawing.Size(430, 20);
      this.firstNameTextBox.TabIndex = 52;
      // 
      // familyNameLabel
      // 
      this.familyNameLabel.AutoSize = true;
      this.familyNameLabel.Location = new System.Drawing.Point(2, 77);
      this.familyNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.familyNameLabel.Name = "familyNameLabel";
      this.familyNameLabel.Size = new System.Drawing.Size(69, 14);
      this.familyNameLabel.TabIndex = 57;
      this.familyNameLabel.Text = "Family name:";
      // 
      // familyNameTextBox
      // 
      this.familyNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.familyNameTextBox.Location = new System.Drawing.Point(115, 74);
      this.familyNameTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.familyNameTextBox.Name = "familyNameTextBox";
      this.familyNameTextBox.Size = new System.Drawing.Size(430, 20);
      this.familyNameTextBox.TabIndex = 53;
      // 
      // emailAddressLabel
      // 
      this.emailAddressLabel.AutoSize = true;
      this.emailAddressLabel.Location = new System.Drawing.Point(4, 101);
      this.emailAddressLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.emailAddressLabel.Name = "emailAddressLabel";
      this.emailAddressLabel.Size = new System.Drawing.Size(77, 14);
      this.emailAddressLabel.TabIndex = 58;
      this.emailAddressLabel.Text = "Email address:";
      // 
      // emailAddressTextBox
      // 
      this.emailAddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.emailAddressTextBox.Location = new System.Drawing.Point(115, 98);
      this.emailAddressTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.emailAddressTextBox.Name = "emailAddressTextBox";
      this.emailAddressTextBox.Size = new System.Drawing.Size(430, 20);
      this.emailAddressTextBox.TabIndex = 54;
      // 
      // EnterVoterSubgroupDataControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.emailNotificationCheckBox);
      this.Controls.Add(this.firstNameLabel);
      this.Controls.Add(this.firstNameTextBox);
      this.Controls.Add(this.familyNameLabel);
      this.Controls.Add(this.familyNameTextBox);
      this.Controls.Add(this.emailAddressLabel);
      this.Controls.Add(this.emailAddressTextBox);
      this.Controls.Add(this.baseValidUntilLabel);
      this.Controls.Add(this.baseValidUnitlTextBox);
      this.Controls.Add(this.baseCertificateLabel);
      this.Controls.Add(this.baseCertificateComboBox);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.nextButton);
      this.Controls.Add(this.groupLabel);
      this.Controls.Add(this.groupComboBox);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "EnterVoterSubgroupDataControl";
      this.Size = new System.Drawing.Size(548, 250);
      this.Load += new System.EventHandler(this.EnterVoterCertificateDataControl_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label groupLabel;
    private Gui.GroupComboBox groupComboBox;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button nextButton;
    private System.Windows.Forms.ComboBox baseCertificateComboBox;
    private System.Windows.Forms.Label baseCertificateLabel;
    private System.Windows.Forms.TextBox baseValidUnitlTextBox;
    private System.Windows.Forms.Label baseValidUntilLabel;
    private System.Windows.Forms.CheckBox emailNotificationCheckBox;
    private System.Windows.Forms.Label firstNameLabel;
    private System.Windows.Forms.TextBox firstNameTextBox;
    private System.Windows.Forms.Label familyNameLabel;
    private System.Windows.Forms.TextBox familyNameTextBox;
    private System.Windows.Forms.Label emailAddressLabel;
    private System.Windows.Forms.TextBox emailAddressTextBox;
  }
}
