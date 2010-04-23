namespace Pirate.PiVote.Client
{
  partial class CertificateForm
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
      this.certificateControl = new Pirate.PiVote.Client.CertificateControl();
      this.closeButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // certificateControl
      // 
      this.certificateControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.certificateControl.Certificate = null;
      this.certificateControl.CertificateStorage = null;
      this.certificateControl.Location = new System.Drawing.Point(18, 18);
      this.certificateControl.Margin = new System.Windows.Forms.Padding(108, 51, 108, 51);
      this.certificateControl.Name = "certificateControl";
      this.certificateControl.Size = new System.Drawing.Size(890, 334);
      this.certificateControl.TabIndex = 0;
      this.certificateControl.ValidationDate = new System.DateTime(2010, 4, 23, 11, 13, 34, 819);
      // 
      // closeButton
      // 
      this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.closeButton.Location = new System.Drawing.Point(795, 362);
      this.closeButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.closeButton.Name = "closeButton";
      this.closeButton.Size = new System.Drawing.Size(112, 35);
      this.closeButton.TabIndex = 1;
      this.closeButton.Text = "&Close";
      this.closeButton.UseVisualStyleBackColor = true;
      this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
      // 
      // CertificateForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(926, 415);
      this.Controls.Add(this.closeButton);
      this.Controls.Add(this.certificateControl);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "CertificateForm";
      this.Text = "View Certificate";
      this.ResumeLayout(false);

    }

    #endregion

    private CertificateControl certificateControl;
    private System.Windows.Forms.Button closeButton;
  }
}