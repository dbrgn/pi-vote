namespace Pirate.PiVote.Client
{
  partial class Progress
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
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.progessLabel = new System.Windows.Forms.Label();
      this.subProgressLabel = new System.Windows.Forms.Label();
      this.subProgessBar = new System.Windows.Forms.ProgressBar();
      this.SuspendLayout();
      // 
      // progressBar
      // 
      this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar.Location = new System.Drawing.Point(0, 19);
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(593, 20);
      this.progressBar.TabIndex = 0;
      // 
      // progessLabel
      // 
      this.progessLabel.AutoSize = true;
      this.progessLabel.Location = new System.Drawing.Point(3, 3);
      this.progessLabel.Name = "progessLabel";
      this.progessLabel.Size = new System.Drawing.Size(35, 13);
      this.progessLabel.TabIndex = 1;
      this.progessLabel.Text = "label1";
      // 
      // subProgressLabel
      // 
      this.subProgressLabel.AutoSize = true;
      this.subProgressLabel.Location = new System.Drawing.Point(3, 49);
      this.subProgressLabel.Name = "subProgressLabel";
      this.subProgressLabel.Size = new System.Drawing.Size(35, 13);
      this.subProgressLabel.TabIndex = 3;
      this.subProgressLabel.Text = "label2";
      // 
      // subProgessBar
      // 
      this.subProgessBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.subProgessBar.Location = new System.Drawing.Point(0, 65);
      this.subProgessBar.Name = "subProgessBar";
      this.subProgessBar.Size = new System.Drawing.Size(593, 20);
      this.subProgessBar.TabIndex = 2;
      // 
      // Progress
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.progressBar);
      this.Controls.Add(this.subProgessBar);
      this.Controls.Add(this.subProgressLabel);
      this.Controls.Add(this.progessLabel);
      this.Name = "Progress";
      this.Size = new System.Drawing.Size(593, 93);
      this.Load += new System.EventHandler(this.Progress_Load);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label progessLabel;
    private System.Windows.Forms.Label subProgressLabel;
    private System.Windows.Forms.ProgressBar subProgessBar;
  }
}
