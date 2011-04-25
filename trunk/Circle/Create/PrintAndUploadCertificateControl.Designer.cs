/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle.Create
{
  partial class PrintAndUploadCertificateControl
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
      this.typeLabel = new System.Windows.Forms.Label();
      this.typeTextBox = new System.Windows.Forms.TextBox();
      this.idTextBox = new System.Windows.Forms.TextBox();
      this.nameTextBox = new System.Windows.Forms.TextBox();
      this.idLabel = new System.Windows.Forms.Label();
      this.nameLabel = new System.Windows.Forms.Label();
      this.emailTextBox = new System.Windows.Forms.TextBox();
      this.emailLabel = new System.Windows.Forms.Label();
      this.doneButton = new System.Windows.Forms.Button();
      this.uploadButton = new System.Windows.Forms.Button();
      this.printButton = new System.Windows.Forms.Button();
      this.infoLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // typeLabel
      // 
      this.typeLabel.AutoSize = true;
      this.typeLabel.Location = new System.Drawing.Point(3, 5);
      this.typeLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.typeLabel.Name = "typeLabel";
      this.typeLabel.Size = new System.Drawing.Size(80, 13);
      this.typeLabel.TabIndex = 42;
      this.typeLabel.Text = "Certificate type:";
      // 
      // typeTextBox
      // 
      this.typeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.typeTextBox.Location = new System.Drawing.Point(117, 2);
      this.typeTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.typeTextBox.Name = "typeTextBox";
      this.typeTextBox.ReadOnly = true;
      this.typeTextBox.Size = new System.Drawing.Size(340, 20);
      this.typeTextBox.TabIndex = 41;
      // 
      // idTextBox
      // 
      this.idTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.idTextBox.Location = new System.Drawing.Point(117, 26);
      this.idTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.idTextBox.Name = "idTextBox";
      this.idTextBox.ReadOnly = true;
      this.idTextBox.Size = new System.Drawing.Size(340, 20);
      this.idTextBox.TabIndex = 43;
      // 
      // nameTextBox
      // 
      this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.nameTextBox.Location = new System.Drawing.Point(117, 50);
      this.nameTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.nameTextBox.Name = "nameTextBox";
      this.nameTextBox.ReadOnly = true;
      this.nameTextBox.Size = new System.Drawing.Size(340, 20);
      this.nameTextBox.TabIndex = 44;
      // 
      // idLabel
      // 
      this.idLabel.AutoSize = true;
      this.idLabel.Location = new System.Drawing.Point(3, 29);
      this.idLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.idLabel.Name = "idLabel";
      this.idLabel.Size = new System.Drawing.Size(68, 13);
      this.idLabel.TabIndex = 45;
      this.idLabel.Text = "Certificate id:";
      // 
      // nameLabel
      // 
      this.nameLabel.AutoSize = true;
      this.nameLabel.Location = new System.Drawing.Point(3, 53);
      this.nameLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Size = new System.Drawing.Size(38, 13);
      this.nameLabel.TabIndex = 46;
      this.nameLabel.Text = "Name:";
      // 
      // emailTextBox
      // 
      this.emailTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.emailTextBox.Font = new System.Drawing.Font("Arial", 8.25F);
      this.emailTextBox.Location = new System.Drawing.Point(117, 74);
      this.emailTextBox.Margin = new System.Windows.Forms.Padding(2);
      this.emailTextBox.Name = "emailTextBox";
      this.emailTextBox.ReadOnly = true;
      this.emailTextBox.Size = new System.Drawing.Size(340, 20);
      this.emailTextBox.TabIndex = 47;
      // 
      // emailLabel
      // 
      this.emailLabel.AutoSize = true;
      this.emailLabel.Location = new System.Drawing.Point(3, 77);
      this.emailLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.emailLabel.Name = "emailLabel";
      this.emailLabel.Size = new System.Drawing.Size(75, 13);
      this.emailLabel.TabIndex = 48;
      this.emailLabel.Text = "Email address:";
      // 
      // doneButton
      // 
      this.doneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.doneButton.Location = new System.Drawing.Point(328, 188);
      this.doneButton.Name = "doneButton";
      this.doneButton.Size = new System.Drawing.Size(128, 28);
      this.doneButton.TabIndex = 49;
      this.doneButton.Text = "&Done";
      this.doneButton.UseVisualStyleBackColor = true;
      this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
      // 
      // uploadButton
      // 
      this.uploadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.uploadButton.Location = new System.Drawing.Point(194, 188);
      this.uploadButton.Name = "uploadButton";
      this.uploadButton.Size = new System.Drawing.Size(128, 28);
      this.uploadButton.TabIndex = 50;
      this.uploadButton.Text = "&Upload";
      this.uploadButton.UseVisualStyleBackColor = true;
      this.uploadButton.Click += new System.EventHandler(this.uploadButton_Click);
      // 
      // printButton
      // 
      this.printButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.printButton.Location = new System.Drawing.Point(60, 188);
      this.printButton.Name = "printButton";
      this.printButton.Size = new System.Drawing.Size(128, 28);
      this.printButton.TabIndex = 51;
      this.printButton.Text = "&Print";
      this.printButton.UseVisualStyleBackColor = true;
      this.printButton.Click += new System.EventHandler(this.printButton_Click);
      // 
      // infoLabel
      // 
      this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.infoLabel.Location = new System.Drawing.Point(3, 102);
      this.infoLabel.Name = "infoLabel";
      this.infoLabel.Size = new System.Drawing.Size(453, 80);
      this.infoLabel.TabIndex = 52;
      this.infoLabel.Text = "Info";
      // 
      // PrintAndUploadCertificateControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.infoLabel);
      this.Controls.Add(this.printButton);
      this.Controls.Add(this.uploadButton);
      this.Controls.Add(this.doneButton);
      this.Controls.Add(this.emailLabel);
      this.Controls.Add(this.emailTextBox);
      this.Controls.Add(this.nameLabel);
      this.Controls.Add(this.idLabel);
      this.Controls.Add(this.nameTextBox);
      this.Controls.Add(this.idTextBox);
      this.Controls.Add(this.typeLabel);
      this.Controls.Add(this.typeTextBox);
      this.Name = "PrintAndUploadCertificateControl";
      this.Size = new System.Drawing.Size(459, 219);
      this.Load += new System.EventHandler(this.PrintAndUploadCertificateControl_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label typeLabel;
    private System.Windows.Forms.TextBox typeTextBox;
    private System.Windows.Forms.TextBox idTextBox;
    private System.Windows.Forms.TextBox nameTextBox;
    private System.Windows.Forms.Label idLabel;
    private System.Windows.Forms.Label nameLabel;
    private System.Windows.Forms.TextBox emailTextBox;
    private System.Windows.Forms.Label emailLabel;
    private System.Windows.Forms.Button doneButton;
    private System.Windows.Forms.Button uploadButton;
    private System.Windows.Forms.Button printButton;
    private System.Windows.Forms.Label infoLabel;

  }
}
