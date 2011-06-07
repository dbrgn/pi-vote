namespace Pirate.PiVote.Circle.Certificates
{
  partial class BackupProgressDialog
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
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.fileLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // progressBar
      // 
      this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar.Location = new System.Drawing.Point(12, 29);
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(768, 19);
      this.progressBar.TabIndex = 0;
      // 
      // fileLabel
      // 
      this.fileLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.fileLabel.Location = new System.Drawing.Point(9, 9);
      this.fileLabel.Name = "fileLabel";
      this.fileLabel.Size = new System.Drawing.Size(771, 17);
      this.fileLabel.TabIndex = 1;
      this.fileLabel.Text = "label1";
      // 
      // BackupProgressDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(792, 60);
      this.ControlBox = false;
      this.Controls.Add(this.fileLabel);
      this.Controls.Add(this.progressBar);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(2000, 87);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(400, 87);
      this.Name = "BackupProgressDialog";
      this.Text = "BackupProgressDialog";
      this.Load += new System.EventHandler(this.BackupProgressDialog_Load);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label fileLabel;
  }
}