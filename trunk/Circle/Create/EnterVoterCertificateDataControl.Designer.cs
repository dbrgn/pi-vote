/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle.Create
{
  partial class EnterVoterCertificateDataControl
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
      this.emailNotificationCheckBox = new System.Windows.Forms.CheckBox();
      this.firstNameLabel = new System.Windows.Forms.Label();
      this.firstNameTextBox = new System.Windows.Forms.TextBox();
      this.familyNameLabel = new System.Windows.Forms.Label();
      this.familyNameTextBox = new System.Windows.Forms.TextBox();
      this.emailAddressLabel = new System.Windows.Forms.Label();
      this.emailAddressTextBox = new System.Windows.Forms.TextBox();
      this.cancelButton = new System.Windows.Forms.Button();
      this.nextButton = new System.Windows.Forms.Button();
      this.infoLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // emailNotificationCheckBox
      // 
      this.emailNotificationCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.emailNotificationCheckBox.Location = new System.Drawing.Point(116, 112);
      this.emailNotificationCheckBox.Name = "emailNotificationCheckBox";
      this.emailNotificationCheckBox.Size = new System.Drawing.Size(429, 46);
      this.emailNotificationCheckBox.TabIndex = 38;
      this.emailNotificationCheckBox.Text = "Notify me by email, when an answer to my certificate signing request is available" +
          ". This requires your email address to be stored on the PiVote server.";
      this.emailNotificationCheckBox.UseVisualStyleBackColor = true;
      // 
      // firstNameLabel
      // 
      this.firstNameLabel.AutoSize = true;
      this.firstNameLabel.Location = new System.Drawing.Point(2, 43);
      this.firstNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.firstNameLabel.Name = "firstNameLabel";
      this.firstNameLabel.Size = new System.Drawing.Size(60, 14);
      this.firstNameLabel.TabIndex = 40;
      this.firstNameLabel.Text = "First name:";
      // 
      // firstNameTextBox
      // 
      this.firstNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.firstNameTextBox.Location = new System.Drawing.Point(116, 40);
      this.firstNameTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.firstNameTextBox.Name = "firstNameTextBox";
      this.firstNameTextBox.Size = new System.Drawing.Size(429, 20);
      this.firstNameTextBox.TabIndex = 34;
      this.firstNameTextBox.TextChanged += new System.EventHandler(this.firstNameTextBox_TextChanged);
      // 
      // familyNameLabel
      // 
      this.familyNameLabel.AutoSize = true;
      this.familyNameLabel.Location = new System.Drawing.Point(2, 66);
      this.familyNameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.familyNameLabel.Name = "familyNameLabel";
      this.familyNameLabel.Size = new System.Drawing.Size(69, 14);
      this.familyNameLabel.TabIndex = 41;
      this.familyNameLabel.Text = "Family name:";
      // 
      // familyNameTextBox
      // 
      this.familyNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.familyNameTextBox.Location = new System.Drawing.Point(116, 63);
      this.familyNameTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.familyNameTextBox.Name = "familyNameTextBox";
      this.familyNameTextBox.Size = new System.Drawing.Size(429, 20);
      this.familyNameTextBox.TabIndex = 35;
      this.familyNameTextBox.TextChanged += new System.EventHandler(this.familyNameTextBox_TextChanged);
      // 
      // emailAddressLabel
      // 
      this.emailAddressLabel.AutoSize = true;
      this.emailAddressLabel.Location = new System.Drawing.Point(2, 90);
      this.emailAddressLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.emailAddressLabel.Name = "emailAddressLabel";
      this.emailAddressLabel.Size = new System.Drawing.Size(77, 14);
      this.emailAddressLabel.TabIndex = 42;
      this.emailAddressLabel.Text = "Email address:";
      // 
      // emailAddressTextBox
      // 
      this.emailAddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.emailAddressTextBox.Location = new System.Drawing.Point(116, 87);
      this.emailAddressTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.emailAddressTextBox.Name = "emailAddressTextBox";
      this.emailAddressTextBox.Size = new System.Drawing.Size(429, 20);
      this.emailAddressTextBox.TabIndex = 37;
      this.emailAddressTextBox.TextChanged += new System.EventHandler(this.emailAddressTextBox_TextChanged);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(283, 267);
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
      this.nextButton.Location = new System.Drawing.Point(417, 267);
      this.nextButton.Name = "nextButton";
      this.nextButton.Size = new System.Drawing.Size(128, 30);
      this.nextButton.TabIndex = 46;
      this.nextButton.Text = "&Next";
      this.nextButton.UseVisualStyleBackColor = true;
      this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
      // 
      // infoLabel
      // 
      this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.infoLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.infoLabel.Location = new System.Drawing.Point(3, 5);
      this.infoLabel.Name = "infoLabel";
      this.infoLabel.Size = new System.Drawing.Size(542, 33);
      this.infoLabel.TabIndex = 48;
      this.infoLabel.Text = "label1";
      // 
      // EnterVoterCertificateDataControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.infoLabel);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.nextButton);
      this.Controls.Add(this.emailNotificationCheckBox);
      this.Controls.Add(this.firstNameLabel);
      this.Controls.Add(this.firstNameTextBox);
      this.Controls.Add(this.familyNameLabel);
      this.Controls.Add(this.familyNameTextBox);
      this.Controls.Add(this.emailAddressLabel);
      this.Controls.Add(this.emailAddressTextBox);
      this.Name = "EnterVoterCertificateDataControl";
      this.Size = new System.Drawing.Size(548, 300);
      this.Load += new System.EventHandler(this.EnterVoterCertificateDataControl_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.CheckBox emailNotificationCheckBox;
    private System.Windows.Forms.Label firstNameLabel;
    private System.Windows.Forms.TextBox firstNameTextBox;
    private System.Windows.Forms.Label familyNameLabel;
    private System.Windows.Forms.TextBox familyNameTextBox;
    private System.Windows.Forms.Label emailAddressLabel;
    private System.Windows.Forms.TextBox emailAddressTextBox;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button nextButton;
    private System.Windows.Forms.Label infoLabel;
  }
}
