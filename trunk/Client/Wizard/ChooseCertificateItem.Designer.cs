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
      this.certificateList = new System.Windows.Forms.ListView();
      this.typeColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.idColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.nameColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.createButton = new System.Windows.Forms.Button();
      this.verifyShareProofButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // loadButton
      // 
      this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.loadButton.Location = new System.Drawing.Point(4, 430);
      this.loadButton.Margin = new System.Windows.Forms.Padding(4);
      this.loadButton.Name = "loadButton";
      this.loadButton.Size = new System.Drawing.Size(168, 34);
      this.loadButton.TabIndex = 0;
      this.loadButton.Text = "&Load...";
      this.loadButton.UseVisualStyleBackColor = true;
      this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
      // 
      // idLabel
      // 
      this.idLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.idLabel.AutoSize = true;
      this.idLabel.Location = new System.Drawing.Point(6, 476);
      this.idLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.idLabel.Name = "idLabel";
      this.idLabel.Size = new System.Drawing.Size(28, 19);
      this.idLabel.TabIndex = 1;
      this.idLabel.Text = "Id:";
      // 
      // typeLabel
      // 
      this.typeLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.typeLabel.AutoSize = true;
      this.typeLabel.Location = new System.Drawing.Point(6, 514);
      this.typeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.typeLabel.Name = "typeLabel";
      this.typeLabel.Size = new System.Drawing.Size(49, 19);
      this.typeLabel.TabIndex = 2;
      this.typeLabel.Text = "Type:";
      // 
      // nameLabel
      // 
      this.nameLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.nameLabel.AutoSize = true;
      this.nameLabel.Location = new System.Drawing.Point(6, 552);
      this.nameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Size = new System.Drawing.Size(84, 19);
      this.nameLabel.TabIndex = 3;
      this.nameLabel.Text = "Full name:";
      // 
      // idTextBox
      // 
      this.idTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.idTextBox.Location = new System.Drawing.Point(153, 472);
      this.idTextBox.Margin = new System.Windows.Forms.Padding(4);
      this.idTextBox.Name = "idTextBox";
      this.idTextBox.ReadOnly = true;
      this.idTextBox.Size = new System.Drawing.Size(366, 26);
      this.idTextBox.TabIndex = 4;
      // 
      // typeTextBox
      // 
      this.typeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.typeTextBox.Location = new System.Drawing.Point(153, 510);
      this.typeTextBox.Margin = new System.Windows.Forms.Padding(4);
      this.typeTextBox.Name = "typeTextBox";
      this.typeTextBox.ReadOnly = true;
      this.typeTextBox.Size = new System.Drawing.Size(366, 26);
      this.typeTextBox.TabIndex = 5;
      // 
      // nameTextBox
      // 
      this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.nameTextBox.Location = new System.Drawing.Point(153, 548);
      this.nameTextBox.Margin = new System.Windows.Forms.Padding(4);
      this.nameTextBox.Name = "nameTextBox";
      this.nameTextBox.ReadOnly = true;
      this.nameTextBox.Size = new System.Drawing.Size(366, 26);
      this.nameTextBox.TabIndex = 6;
      // 
      // certificateList
      // 
      this.certificateList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.certificateList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.typeColumnHeader,
            this.idColumnHeader,
            this.nameColumnHeader});
      this.certificateList.FullRowSelect = true;
      this.certificateList.Location = new System.Drawing.Point(4, 4);
      this.certificateList.Margin = new System.Windows.Forms.Padding(4);
      this.certificateList.MultiSelect = false;
      this.certificateList.Name = "certificateList";
      this.certificateList.Size = new System.Drawing.Size(1039, 417);
      this.certificateList.TabIndex = 7;
      this.certificateList.UseCompatibleStateImageBehavior = false;
      this.certificateList.View = System.Windows.Forms.View.Details;
      this.certificateList.SelectedIndexChanged += new System.EventHandler(this.certificateList_SelectedIndexChanged);
      // 
      // typeColumnHeader
      // 
      this.typeColumnHeader.Text = "Type";
      this.typeColumnHeader.Width = 150;
      // 
      // idColumnHeader
      // 
      this.idColumnHeader.Text = "Id";
      this.idColumnHeader.Width = 220;
      // 
      // nameColumnHeader
      // 
      this.nameColumnHeader.Text = "Name";
      this.nameColumnHeader.Width = 300;
      // 
      // createButton
      // 
      this.createButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.createButton.Location = new System.Drawing.Point(182, 430);
      this.createButton.Margin = new System.Windows.Forms.Padding(4);
      this.createButton.Name = "createButton";
      this.createButton.Size = new System.Drawing.Size(168, 34);
      this.createButton.TabIndex = 8;
      this.createButton.Text = "&Create...";
      this.createButton.UseVisualStyleBackColor = true;
      this.createButton.Click += new System.EventHandler(this.createButton_Click);
      // 
      // verifyShareProofButton
      // 
      this.verifyShareProofButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.verifyShareProofButton.Location = new System.Drawing.Point(358, 430);
      this.verifyShareProofButton.Margin = new System.Windows.Forms.Padding(4);
      this.verifyShareProofButton.Name = "verifyShareProofButton";
      this.verifyShareProofButton.Size = new System.Drawing.Size(234, 34);
      this.verifyShareProofButton.TabIndex = 9;
      this.verifyShareProofButton.Text = "Verfiy share &proof...";
      this.verifyShareProofButton.UseVisualStyleBackColor = true;
      this.verifyShareProofButton.Click += new System.EventHandler(this.verifyShareProofButton_Click);
      // 
      // ChooseCertificateItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.verifyShareProofButton);
      this.Controls.Add(this.createButton);
      this.Controls.Add(this.certificateList);
      this.Controls.Add(this.nameTextBox);
      this.Controls.Add(this.typeTextBox);
      this.Controls.Add(this.idTextBox);
      this.Controls.Add(this.nameLabel);
      this.Controls.Add(this.typeLabel);
      this.Controls.Add(this.idLabel);
      this.Controls.Add(this.loadButton);
      this.Margin = new System.Windows.Forms.Padding(4);
      this.Name = "ChooseCertificateItem";
      this.Size = new System.Drawing.Size(1050, 580);
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
    private ListView certificateList;
    private ColumnHeader typeColumnHeader;
    private ColumnHeader idColumnHeader;
    private ColumnHeader nameColumnHeader;
    private Button createButton;
    private Button verifyShareProofButton;
  }
}
