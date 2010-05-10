namespace Pirate.PiVote.Client
{
  partial class ViewShareResponseControl
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
      this.dataTextBox = new System.Windows.Forms.TextBox();
      this.dataLabel = new System.Windows.Forms.Label();
      this.certificateControl = new Pirate.PiVote.Client.SmallCertificateControl();
      this.SuspendLayout();
      // 
      // dataTextBox
      // 
      this.dataTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.dataTextBox.Location = new System.Drawing.Point(568, 0);
      this.dataTextBox.Name = "dataTextBox";
      this.dataTextBox.ReadOnly = true;
      this.dataTextBox.Size = new System.Drawing.Size(94, 20);
      this.dataTextBox.TabIndex = 5;
      // 
      // dataLabel
      // 
      this.dataLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.dataLabel.AutoSize = true;
      this.dataLabel.Location = new System.Drawing.Point(478, 3);
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
      this.Controls.Add(this.certificateControl);
      this.Name = "ViewShareControl";
      this.Size = new System.Drawing.Size(662, 22);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private SmallCertificateControl certificateControl;
    private System.Windows.Forms.TextBox dataTextBox;
    private System.Windows.Forms.Label dataLabel;

  }
}
