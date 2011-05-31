namespace Pirate.PiVote.Circle.Config
{
  partial class ConfigProofCountControl
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
      this.initialCheckProofCountTrackBar = new System.Windows.Forms.TrackBar();
      this.explainLabel = new System.Windows.Forms.Label();
      this.infoLabel = new System.Windows.Forms.Label();
      this.fastLabel = new System.Windows.Forms.Label();
      this.secureLabel = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.initialCheckProofCountTrackBar)).BeginInit();
      this.SuspendLayout();
      // 
      // initialCheckProofCountTrackBar
      // 
      this.initialCheckProofCountTrackBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.initialCheckProofCountTrackBar.Location = new System.Drawing.Point(3, 106);
      this.initialCheckProofCountTrackBar.Maximum = 8;
      this.initialCheckProofCountTrackBar.Minimum = 1;
      this.initialCheckProofCountTrackBar.Name = "initialCheckProofCountTrackBar";
      this.initialCheckProofCountTrackBar.Size = new System.Drawing.Size(440, 42);
      this.initialCheckProofCountTrackBar.TabIndex = 0;
      this.initialCheckProofCountTrackBar.Value = 1;
      this.initialCheckProofCountTrackBar.Scroll += new System.EventHandler(this.initialCheckProofCountTrackBar_Scroll);
      // 
      // explainLabel
      // 
      this.explainLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.explainLabel.Location = new System.Drawing.Point(3, 0);
      this.explainLabel.Name = "explainLabel";
      this.explainLabel.Size = new System.Drawing.Size(440, 80);
      this.explainLabel.TabIndex = 1;
      this.explainLabel.Text = "label1";
      // 
      // infoLabel
      // 
      this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.infoLabel.Location = new System.Drawing.Point(3, 154);
      this.infoLabel.Name = "infoLabel";
      this.infoLabel.Size = new System.Drawing.Size(440, 154);
      this.infoLabel.TabIndex = 2;
      this.infoLabel.Text = "label2";
      // 
      // fastLabel
      // 
      this.fastLabel.Location = new System.Drawing.Point(3, 80);
      this.fastLabel.Name = "fastLabel";
      this.fastLabel.Size = new System.Drawing.Size(135, 23);
      this.fastLabel.TabIndex = 3;
      this.fastLabel.Text = "fastLabel";
      this.fastLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
      // 
      // secureLabel
      // 
      this.secureLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.secureLabel.Location = new System.Drawing.Point(308, 80);
      this.secureLabel.Name = "secureLabel";
      this.secureLabel.Size = new System.Drawing.Size(135, 23);
      this.secureLabel.TabIndex = 4;
      this.secureLabel.Text = "secureLabel";
      this.secureLabel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
      // 
      // ConfigProofCountControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.secureLabel);
      this.Controls.Add(this.fastLabel);
      this.Controls.Add(this.infoLabel);
      this.Controls.Add(this.explainLabel);
      this.Controls.Add(this.initialCheckProofCountTrackBar);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "ConfigProofCountControl";
      this.Size = new System.Drawing.Size(446, 311);
      this.Load += new System.EventHandler(this.ConfigProofCountControl_Load);
      ((System.ComponentModel.ISupportInitialize)(this.initialCheckProofCountTrackBar)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TrackBar initialCheckProofCountTrackBar;
    private System.Windows.Forms.Label explainLabel;
    private System.Windows.Forms.Label infoLabel;
    private System.Windows.Forms.Label fastLabel;
    private System.Windows.Forms.Label secureLabel;
  }
}
