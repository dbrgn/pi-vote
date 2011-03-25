/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.CaGui
{
  partial class CountDialog
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
      this.dateLabel = new System.Windows.Forms.Label();
      this.datePicker = new System.Windows.Forms.DateTimePicker();
      this.totalLabel = new System.Windows.Forms.Label();
      this.totalBox = new System.Windows.Forms.TextBox();
      this.validBox = new System.Windows.Forms.TextBox();
      this.validLabel = new System.Windows.Forms.Label();
      this.outdatedBox = new System.Windows.Forms.TextBox();
      this.revokedBox = new System.Windows.Forms.TextBox();
      this.outdatedLabel = new System.Windows.Forms.Label();
      this.revokedLabel = new System.Windows.Forms.Label();
      this.refusedBox = new System.Windows.Forms.TextBox();
      this.pendingBox = new System.Windows.Forms.TextBox();
      this.refusedLabel = new System.Windows.Forms.Label();
      this.pendingLabel = new System.Windows.Forms.Label();
      this.okButton = new System.Windows.Forms.Button();
      this.notYetBox = new System.Windows.Forms.TextBox();
      this.notYetLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // dateLabel
      // 
      this.dateLabel.AutoSize = true;
      this.dateLabel.Location = new System.Drawing.Point(12, 15);
      this.dateLabel.Name = "dateLabel";
      this.dateLabel.Size = new System.Drawing.Size(69, 13);
      this.dateLabel.TabIndex = 0;
      this.dateLabel.Text = "Valid at date:";
      // 
      // datePicker
      // 
      this.datePicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.datePicker.Location = new System.Drawing.Point(144, 12);
      this.datePicker.Name = "datePicker";
      this.datePicker.Size = new System.Drawing.Size(190, 20);
      this.datePicker.TabIndex = 1;
      this.datePicker.ValueChanged += new System.EventHandler(this.datePicker_ValueChanged);
      // 
      // totalLabel
      // 
      this.totalLabel.AutoSize = true;
      this.totalLabel.Location = new System.Drawing.Point(12, 41);
      this.totalLabel.Name = "totalLabel";
      this.totalLabel.Size = new System.Drawing.Size(88, 13);
      this.totalLabel.TabIndex = 2;
      this.totalLabel.Text = "Total certificates:";
      // 
      // totalBox
      // 
      this.totalBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.totalBox.Location = new System.Drawing.Point(144, 38);
      this.totalBox.Name = "totalBox";
      this.totalBox.ReadOnly = true;
      this.totalBox.Size = new System.Drawing.Size(190, 20);
      this.totalBox.TabIndex = 3;
      // 
      // validBox
      // 
      this.validBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.validBox.Location = new System.Drawing.Point(144, 64);
      this.validBox.Name = "validBox";
      this.validBox.ReadOnly = true;
      this.validBox.Size = new System.Drawing.Size(190, 20);
      this.validBox.TabIndex = 4;
      // 
      // validLabel
      // 
      this.validLabel.AutoSize = true;
      this.validLabel.Location = new System.Drawing.Point(12, 67);
      this.validLabel.Name = "validLabel";
      this.validLabel.Size = new System.Drawing.Size(87, 13);
      this.validLabel.TabIndex = 5;
      this.validLabel.Text = "Valid certificates:";
      // 
      // outdatedBox
      // 
      this.outdatedBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.outdatedBox.Location = new System.Drawing.Point(144, 90);
      this.outdatedBox.Name = "outdatedBox";
      this.outdatedBox.ReadOnly = true;
      this.outdatedBox.Size = new System.Drawing.Size(190, 20);
      this.outdatedBox.TabIndex = 6;
      // 
      // revokedBox
      // 
      this.revokedBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.revokedBox.Location = new System.Drawing.Point(144, 142);
      this.revokedBox.Name = "revokedBox";
      this.revokedBox.ReadOnly = true;
      this.revokedBox.Size = new System.Drawing.Size(190, 20);
      this.revokedBox.TabIndex = 7;
      // 
      // outdatedLabel
      // 
      this.outdatedLabel.AutoSize = true;
      this.outdatedLabel.Location = new System.Drawing.Point(12, 93);
      this.outdatedLabel.Name = "outdatedLabel";
      this.outdatedLabel.Size = new System.Drawing.Size(108, 13);
      this.outdatedLabel.TabIndex = 8;
      this.outdatedLabel.Text = "Outdated certificates:";
      // 
      // revokedLabel
      // 
      this.revokedLabel.AutoSize = true;
      this.revokedLabel.Location = new System.Drawing.Point(13, 145);
      this.revokedLabel.Name = "revokedLabel";
      this.revokedLabel.Size = new System.Drawing.Size(108, 13);
      this.revokedLabel.TabIndex = 9;
      this.revokedLabel.Text = "Revoked certificates:";
      // 
      // refusedBox
      // 
      this.refusedBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.refusedBox.Location = new System.Drawing.Point(144, 168);
      this.refusedBox.Name = "refusedBox";
      this.refusedBox.ReadOnly = true;
      this.refusedBox.Size = new System.Drawing.Size(190, 20);
      this.refusedBox.TabIndex = 10;
      // 
      // pendingBox
      // 
      this.pendingBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.pendingBox.Location = new System.Drawing.Point(144, 194);
      this.pendingBox.Name = "pendingBox";
      this.pendingBox.ReadOnly = true;
      this.pendingBox.Size = new System.Drawing.Size(190, 20);
      this.pendingBox.TabIndex = 11;
      // 
      // refusedLabel
      // 
      this.refusedLabel.AutoSize = true;
      this.refusedLabel.Location = new System.Drawing.Point(12, 171);
      this.refusedLabel.Name = "refusedLabel";
      this.refusedLabel.Size = new System.Drawing.Size(104, 13);
      this.refusedLabel.TabIndex = 12;
      this.refusedLabel.Text = "Refused certificates:";
      // 
      // pendingLabel
      // 
      this.pendingLabel.AutoSize = true;
      this.pendingLabel.Location = new System.Drawing.Point(12, 197);
      this.pendingLabel.Name = "pendingLabel";
      this.pendingLabel.Size = new System.Drawing.Size(103, 13);
      this.pendingLabel.TabIndex = 13;
      this.pendingLabel.Text = "Pending certificates:";
      // 
      // okButton
      // 
      this.okButton.Location = new System.Drawing.Point(236, 220);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(98, 27);
      this.okButton.TabIndex = 14;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // notYetBox
      // 
      this.notYetBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.notYetBox.Location = new System.Drawing.Point(144, 116);
      this.notYetBox.Name = "notYetBox";
      this.notYetBox.ReadOnly = true;
      this.notYetBox.Size = new System.Drawing.Size(190, 20);
      this.notYetBox.TabIndex = 15;
      // 
      // notYetLabel
      // 
      this.notYetLabel.AutoSize = true;
      this.notYetLabel.Location = new System.Drawing.Point(13, 119);
      this.notYetLabel.Name = "notYetLabel";
      this.notYetLabel.Size = new System.Drawing.Size(123, 13);
      this.notYetLabel.TabIndex = 16;
      this.notYetLabel.Text = "Not yet valid certificates:";
      // 
      // CountDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(346, 257);
      this.Controls.Add(this.notYetLabel);
      this.Controls.Add(this.notYetBox);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.pendingLabel);
      this.Controls.Add(this.refusedLabel);
      this.Controls.Add(this.pendingBox);
      this.Controls.Add(this.refusedBox);
      this.Controls.Add(this.revokedLabel);
      this.Controls.Add(this.outdatedLabel);
      this.Controls.Add(this.revokedBox);
      this.Controls.Add(this.outdatedBox);
      this.Controls.Add(this.validLabel);
      this.Controls.Add(this.validBox);
      this.Controls.Add(this.totalBox);
      this.Controls.Add(this.totalLabel);
      this.Controls.Add(this.datePicker);
      this.Controls.Add(this.dateLabel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(200, 27);
      this.Name = "CountDialog";
      this.Text = "CountDialog";
      this.Load += new System.EventHandler(this.CountDialog_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label dateLabel;
    private System.Windows.Forms.DateTimePicker datePicker;
    private System.Windows.Forms.Label totalLabel;
    private System.Windows.Forms.TextBox totalBox;
    private System.Windows.Forms.TextBox validBox;
    private System.Windows.Forms.Label validLabel;
    private System.Windows.Forms.TextBox outdatedBox;
    private System.Windows.Forms.TextBox revokedBox;
    private System.Windows.Forms.Label outdatedLabel;
    private System.Windows.Forms.Label revokedLabel;
    private System.Windows.Forms.TextBox refusedBox;
    private System.Windows.Forms.TextBox pendingBox;
    private System.Windows.Forms.Label refusedLabel;
    private System.Windows.Forms.Label pendingLabel;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.TextBox notYetBox;
    private System.Windows.Forms.Label notYetLabel;
  }
}