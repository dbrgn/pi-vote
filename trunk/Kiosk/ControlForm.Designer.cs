/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Kiosk
{
  partial class ControlForm
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.searchForLabel = new System.Windows.Forms.Label();
      this.searchResultLabel = new System.Windows.Forms.Label();
      this.givennameLabel = new System.Windows.Forms.Label();
      this.surnameLabel = new System.Windows.Forms.Label();
      this.searchForTextBox = new System.Windows.Forms.TextBox();
      this.searchResultList = new System.Windows.Forms.ListView();
      this.givennameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.surnameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.emailColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.emailAddressLabel = new System.Windows.Forms.Label();
      this.givenNameTextBox = new System.Windows.Forms.TextBox();
      this.surnameTextBox = new System.Windows.Forms.TextBox();
      this.emailAddressTextBox = new System.Windows.Forms.TextBox();
      this.setButton = new System.Windows.Forms.Button();
      this.resetButton = new System.Windows.Forms.Button();
      this.setEmailAddressTextBox = new System.Windows.Forms.TextBox();
      this.setSurnameTextBox = new System.Windows.Forms.TextBox();
      this.setGivennameTextBox = new System.Windows.Forms.TextBox();
      this.setEmailAddressLabel = new System.Windows.Forms.Label();
      this.setSurnameLabel = new System.Windows.Forms.Label();
      this.setGivennameLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // searchForLabel
      // 
      this.searchForLabel.AutoSize = true;
      this.searchForLabel.Location = new System.Drawing.Point(15, 18);
      this.searchForLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.searchForLabel.Name = "searchForLabel";
      this.searchForLabel.Size = new System.Drawing.Size(117, 25);
      this.searchForLabel.TabIndex = 0;
      this.searchForLabel.Text = "Search for:";
      // 
      // searchResultLabel
      // 
      this.searchResultLabel.AutoSize = true;
      this.searchResultLabel.Location = new System.Drawing.Point(15, 62);
      this.searchResultLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.searchResultLabel.Name = "searchResultLabel";
      this.searchResultLabel.Size = new System.Drawing.Size(156, 25);
      this.searchResultLabel.TabIndex = 1;
      this.searchResultLabel.Text = "Search results:";
      // 
      // givennameLabel
      // 
      this.givennameLabel.AutoSize = true;
      this.givennameLabel.Location = new System.Drawing.Point(15, 378);
      this.givennameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.givennameLabel.Name = "givennameLabel";
      this.givennameLabel.Size = new System.Drawing.Size(127, 25);
      this.givennameLabel.TabIndex = 2;
      this.givennameLabel.Text = "Givenname:";
      // 
      // surnameLabel
      // 
      this.surnameLabel.AutoSize = true;
      this.surnameLabel.Location = new System.Drawing.Point(15, 421);
      this.surnameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.surnameLabel.Name = "surnameLabel";
      this.surnameLabel.Size = new System.Drawing.Size(104, 25);
      this.surnameLabel.TabIndex = 3;
      this.surnameLabel.Text = "Surname:";
      // 
      // searchForTextBox
      // 
      this.searchForTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.searchForTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.searchForTextBox.Location = new System.Drawing.Point(198, 15);
      this.searchForTextBox.Margin = new System.Windows.Forms.Padding(6);
      this.searchForTextBox.Name = "searchForTextBox";
      this.searchForTextBox.Size = new System.Drawing.Size(963, 31);
      this.searchForTextBox.TabIndex = 4;
      this.searchForTextBox.TextChanged += new System.EventHandler(this.searchForTextBox_TextChanged);
      // 
      // searchResultList
      // 
      this.searchResultList.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.searchResultList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.givennameColumnHeader,
            this.surnameColumnHeader,
            this.emailColumnHeader});
      this.searchResultList.FullRowSelect = true;
      this.searchResultList.HideSelection = false;
      this.searchResultList.Location = new System.Drawing.Point(198, 58);
      this.searchResultList.Margin = new System.Windows.Forms.Padding(6);
      this.searchResultList.MultiSelect = false;
      this.searchResultList.Name = "searchResultList";
      this.searchResultList.Size = new System.Drawing.Size(963, 305);
      this.searchResultList.TabIndex = 5;
      this.searchResultList.UseCompatibleStateImageBehavior = false;
      this.searchResultList.View = System.Windows.Forms.View.Details;
      this.searchResultList.SelectedIndexChanged += new System.EventHandler(this.searchResultList_SelectedIndexChanged);
      // 
      // givennameColumnHeader
      // 
      this.givennameColumnHeader.Text = "Givenname";
      this.givennameColumnHeader.Width = 300;
      // 
      // surnameColumnHeader
      // 
      this.surnameColumnHeader.Text = "Surname";
      this.surnameColumnHeader.Width = 300;
      // 
      // emailColumnHeader
      // 
      this.emailColumnHeader.Text = "Email Address";
      this.emailColumnHeader.Width = 300;
      // 
      // emailAddressLabel
      // 
      this.emailAddressLabel.AutoSize = true;
      this.emailAddressLabel.Location = new System.Drawing.Point(15, 464);
      this.emailAddressLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.emailAddressLabel.Name = "emailAddressLabel";
      this.emailAddressLabel.Size = new System.Drawing.Size(156, 25);
      this.emailAddressLabel.TabIndex = 6;
      this.emailAddressLabel.Text = "Email Address:";
      // 
      // givenNameTextBox
      // 
      this.givenNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.givenNameTextBox.Location = new System.Drawing.Point(198, 375);
      this.givenNameTextBox.Margin = new System.Windows.Forms.Padding(6);
      this.givenNameTextBox.Name = "givenNameTextBox";
      this.givenNameTextBox.Size = new System.Drawing.Size(963, 31);
      this.givenNameTextBox.TabIndex = 7;
      // 
      // surnameTextBox
      // 
      this.surnameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.surnameTextBox.Location = new System.Drawing.Point(198, 418);
      this.surnameTextBox.Margin = new System.Windows.Forms.Padding(6);
      this.surnameTextBox.Name = "surnameTextBox";
      this.surnameTextBox.Size = new System.Drawing.Size(963, 31);
      this.surnameTextBox.TabIndex = 8;
      // 
      // emailAddressTextBox
      // 
      this.emailAddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.emailAddressTextBox.Location = new System.Drawing.Point(198, 461);
      this.emailAddressTextBox.Margin = new System.Windows.Forms.Padding(6);
      this.emailAddressTextBox.Name = "emailAddressTextBox";
      this.emailAddressTextBox.Size = new System.Drawing.Size(963, 31);
      this.emailAddressTextBox.TabIndex = 9;
      // 
      // setButton
      // 
      this.setButton.Location = new System.Drawing.Point(355, 501);
      this.setButton.Name = "setButton";
      this.setButton.Size = new System.Drawing.Size(151, 43);
      this.setButton.TabIndex = 10;
      this.setButton.Text = "&Set";
      this.setButton.UseVisualStyleBackColor = true;
      this.setButton.Click += new System.EventHandler(this.setButton_Click);
      // 
      // resetButton
      // 
      this.resetButton.Location = new System.Drawing.Point(198, 501);
      this.resetButton.Name = "resetButton";
      this.resetButton.Size = new System.Drawing.Size(151, 43);
      this.resetButton.TabIndex = 11;
      this.resetButton.Text = "&Reset";
      this.resetButton.UseVisualStyleBackColor = true;
      this.resetButton.Click += new System.EventHandler(this.resetButton_Click);
      // 
      // setEmailAddressTextBox
      // 
      this.setEmailAddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.setEmailAddressTextBox.Location = new System.Drawing.Point(198, 639);
      this.setEmailAddressTextBox.Margin = new System.Windows.Forms.Padding(6);
      this.setEmailAddressTextBox.Name = "setEmailAddressTextBox";
      this.setEmailAddressTextBox.ReadOnly = true;
      this.setEmailAddressTextBox.Size = new System.Drawing.Size(963, 31);
      this.setEmailAddressTextBox.TabIndex = 17;
      // 
      // setSurnameTextBox
      // 
      this.setSurnameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.setSurnameTextBox.Location = new System.Drawing.Point(198, 596);
      this.setSurnameTextBox.Margin = new System.Windows.Forms.Padding(6);
      this.setSurnameTextBox.Name = "setSurnameTextBox";
      this.setSurnameTextBox.ReadOnly = true;
      this.setSurnameTextBox.Size = new System.Drawing.Size(963, 31);
      this.setSurnameTextBox.TabIndex = 16;
      // 
      // setGivennameTextBox
      // 
      this.setGivennameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.setGivennameTextBox.Location = new System.Drawing.Point(198, 553);
      this.setGivennameTextBox.Margin = new System.Windows.Forms.Padding(6);
      this.setGivennameTextBox.Name = "setGivennameTextBox";
      this.setGivennameTextBox.ReadOnly = true;
      this.setGivennameTextBox.Size = new System.Drawing.Size(963, 31);
      this.setGivennameTextBox.TabIndex = 15;
      // 
      // setEmailAddressLabel
      // 
      this.setEmailAddressLabel.AutoSize = true;
      this.setEmailAddressLabel.Location = new System.Drawing.Point(15, 642);
      this.setEmailAddressLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.setEmailAddressLabel.Name = "setEmailAddressLabel";
      this.setEmailAddressLabel.Size = new System.Drawing.Size(156, 25);
      this.setEmailAddressLabel.TabIndex = 14;
      this.setEmailAddressLabel.Text = "Email Address:";
      // 
      // setSurnameLabel
      // 
      this.setSurnameLabel.AutoSize = true;
      this.setSurnameLabel.Location = new System.Drawing.Point(15, 599);
      this.setSurnameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.setSurnameLabel.Name = "setSurnameLabel";
      this.setSurnameLabel.Size = new System.Drawing.Size(104, 25);
      this.setSurnameLabel.TabIndex = 13;
      this.setSurnameLabel.Text = "Surname:";
      // 
      // setGivennameLabel
      // 
      this.setGivennameLabel.AutoSize = true;
      this.setGivennameLabel.Location = new System.Drawing.Point(15, 556);
      this.setGivennameLabel.Margin = new System.Windows.Forms.Padding(6, 0, 6, 0);
      this.setGivennameLabel.Name = "setGivennameLabel";
      this.setGivennameLabel.Size = new System.Drawing.Size(127, 25);
      this.setGivennameLabel.TabIndex = 12;
      this.setGivennameLabel.Text = "Givenname:";
      // 
      // ControlForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1176, 856);
      this.Controls.Add(this.setEmailAddressTextBox);
      this.Controls.Add(this.setSurnameTextBox);
      this.Controls.Add(this.setGivennameTextBox);
      this.Controls.Add(this.setEmailAddressLabel);
      this.Controls.Add(this.setSurnameLabel);
      this.Controls.Add(this.setGivennameLabel);
      this.Controls.Add(this.resetButton);
      this.Controls.Add(this.setButton);
      this.Controls.Add(this.emailAddressTextBox);
      this.Controls.Add(this.surnameTextBox);
      this.Controls.Add(this.givenNameTextBox);
      this.Controls.Add(this.emailAddressLabel);
      this.Controls.Add(this.searchResultList);
      this.Controls.Add(this.searchForTextBox);
      this.Controls.Add(this.surnameLabel);
      this.Controls.Add(this.givennameLabel);
      this.Controls.Add(this.searchResultLabel);
      this.Controls.Add(this.searchForLabel);
      this.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.Margin = new System.Windows.Forms.Padding(6);
      this.Name = "ControlForm";
      this.Text = "ControlForm";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ControlForm_FormClosing);
      this.Load += new System.EventHandler(this.ControlForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label searchForLabel;
    private System.Windows.Forms.Label searchResultLabel;
    private System.Windows.Forms.Label givennameLabel;
    private System.Windows.Forms.Label surnameLabel;
    private System.Windows.Forms.TextBox searchForTextBox;
    private System.Windows.Forms.ListView searchResultList;
    private System.Windows.Forms.Label emailAddressLabel;
    private System.Windows.Forms.TextBox givenNameTextBox;
    private System.Windows.Forms.TextBox surnameTextBox;
    private System.Windows.Forms.TextBox emailAddressTextBox;
    private System.Windows.Forms.ColumnHeader givennameColumnHeader;
    private System.Windows.Forms.ColumnHeader surnameColumnHeader;
    private System.Windows.Forms.ColumnHeader emailColumnHeader;
    private System.Windows.Forms.Button setButton;
    private System.Windows.Forms.Button resetButton;
    private System.Windows.Forms.TextBox setEmailAddressTextBox;
    private System.Windows.Forms.TextBox setSurnameTextBox;
    private System.Windows.Forms.TextBox setGivennameTextBox;
    private System.Windows.Forms.Label setEmailAddressLabel;
    private System.Windows.Forms.Label setSurnameLabel;
    private System.Windows.Forms.Label setGivennameLabel;
  }
}