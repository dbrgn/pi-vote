namespace Pirate.PiVote.CaGui
{
  partial class WaitForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaitForm));
      this.info0Label = new System.Windows.Forms.Label();
      this.info1Label = new System.Windows.Forms.Label();
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.SuspendLayout();
      // 
      // info0Label
      // 
      this.info0Label.AutoSize = true;
      this.info0Label.Location = new System.Drawing.Point(12, 9);
      this.info0Label.Name = "info0Label";
      this.info0Label.Size = new System.Drawing.Size(43, 17);
      this.info0Label.TabIndex = 0;
      this.info0Label.Text = "Info 1";
      // 
      // info1Label
      // 
      this.info1Label.AutoSize = true;
      this.info1Label.Location = new System.Drawing.Point(12, 37);
      this.info1Label.Name = "info1Label";
      this.info1Label.Size = new System.Drawing.Size(43, 17);
      this.info1Label.TabIndex = 1;
      this.info1Label.Text = "Info 2";
      // 
      // progressBar
      // 
      this.progressBar.Location = new System.Drawing.Point(15, 68);
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(454, 23);
      this.progressBar.TabIndex = 2;
      // 
      // WaitForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(481, 103);
      this.ControlBox = false;
      this.Controls.Add(this.progressBar);
      this.Controls.Add(this.info1Label);
      this.Controls.Add(this.info0Label);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "WaitForm";
      this.Text = "Pi-Vote CA GUI";
      this.Load += new System.EventHandler(this.WaitForm_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label info0Label;
    private System.Windows.Forms.Label info1Label;
    private System.Windows.Forms.ProgressBar progressBar;
  }
}