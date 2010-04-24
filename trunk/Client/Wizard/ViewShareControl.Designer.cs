﻿namespace Pirate.PiVote.Client
{
  partial class ViewShareControl
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
      this.signatureLabel = new System.Windows.Forms.Label();
      this.signatureTextBox = new System.Windows.Forms.TextBox();
      this.dataTextBox = new System.Windows.Forms.TextBox();
      this.dataLabel = new System.Windows.Forms.Label();
      this.certificateControl = new Pirate.PiVote.Client.SmallCertificateControl();
      this.SuspendLayout();
      // 
      // signatureLabel
      // 
      this.signatureLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.signatureLabel.AutoSize = true;
      this.signatureLabel.Location = new System.Drawing.Point(479, 1);
      this.signatureLabel.Name = "signatureLabel";
      this.signatureLabel.Size = new System.Drawing.Size(55, 13);
      this.signatureLabel.TabIndex = 2;
      this.signatureLabel.Text = "Signature:";
      // 
      // signatureTextBox
      // 
      this.signatureTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.signatureTextBox.Location = new System.Drawing.Point(569, 0);
      this.signatureTextBox.Name = "signatureTextBox";
      this.signatureTextBox.ReadOnly = true;
      this.signatureTextBox.Size = new System.Drawing.Size(94, 20);
      this.signatureTextBox.TabIndex = 4;
      // 
      // dataTextBox
      // 
      this.dataTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.dataTextBox.Location = new System.Drawing.Point(569, 26);
      this.dataTextBox.Name = "dataTextBox";
      this.dataTextBox.ReadOnly = true;
      this.dataTextBox.Size = new System.Drawing.Size(94, 20);
      this.dataTextBox.TabIndex = 5;
      // 
      // dataLabel
      // 
      this.dataLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.dataLabel.AutoSize = true;
      this.dataLabel.Location = new System.Drawing.Point(479, 29);
      this.dataLabel.Name = "dataLabel";
      this.dataLabel.Size = new System.Drawing.Size(33, 13);
      this.dataLabel.TabIndex = 6;
      this.dataLabel.Text = "Data:";
      // 
      // certificateControl
      // 
      this.certificateControl.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.certificateControl.Certificate = null;
      this.certificateControl.CertificateStorage = null;
      this.certificateControl.Location = new System.Drawing.Point(0, 0);
      this.certificateControl.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.certificateControl.Name = "certificateControl";
      this.certificateControl.Size = new System.Drawing.Size(472, 46);
      this.certificateControl.TabIndex = 0;
      this.certificateControl.ValidationDate = new System.DateTime(2010, 4, 22, 23, 52, 17, 794);
      // 
      // ViewShareControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.dataLabel);
      this.Controls.Add(this.dataTextBox);
      this.Controls.Add(this.signatureTextBox);
      this.Controls.Add(this.signatureLabel);
      this.Controls.Add(this.certificateControl);
      this.Name = "ViewShareControl";
      this.Size = new System.Drawing.Size(662, 49);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private SmallCertificateControl certificateControl;
    private System.Windows.Forms.Label signatureLabel;
    private System.Windows.Forms.TextBox signatureTextBox;
    private System.Windows.Forms.TextBox dataTextBox;
    private System.Windows.Forms.Label dataLabel;

  }
}