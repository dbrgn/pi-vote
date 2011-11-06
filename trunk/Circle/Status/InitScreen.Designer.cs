/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle.Status
{
  partial class InitScreen
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InitScreen));
      this.infoLabel = new System.Windows.Forms.Label();
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.pivoteLogo = new System.Windows.Forms.PictureBox();
      this.ppsLogo = new System.Windows.Forms.PictureBox();
      this.circleLabel = new System.Windows.Forms.Label();
      this.pivoteLabel = new System.Windows.Forms.Label();
      this.versionLabel = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.pivoteLogo)).BeginInit();
      ((System.ComponentModel.ISupportInitialize)(this.ppsLogo)).BeginInit();
      this.SuspendLayout();
      // 
      // infoLabel
      // 
      this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.infoLabel.Location = new System.Drawing.Point(9, 344);
      this.infoLabel.Name = "infoLabel";
      this.infoLabel.Size = new System.Drawing.Size(579, 22);
      this.infoLabel.TabIndex = 4;
      // 
      // progressBar
      // 
      this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(249)))), ((int)(((byte)(178)))), ((int)(((byte)(0)))));
      this.progressBar.Location = new System.Drawing.Point(12, 369);
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(576, 19);
      this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
      this.progressBar.TabIndex = 3;
      // 
      // pivoteLogo
      // 
      this.pivoteLogo.Image = ((System.Drawing.Image)(resources.GetObject("pivoteLogo.Image")));
      this.pivoteLogo.InitialImage = null;
      this.pivoteLogo.Location = new System.Drawing.Point(2, 27);
      this.pivoteLogo.Name = "pivoteLogo";
      this.pivoteLogo.Size = new System.Drawing.Size(302, 170);
      this.pivoteLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.pivoteLogo.TabIndex = 6;
      this.pivoteLogo.TabStop = false;
      // 
      // ppsLogo
      // 
      this.ppsLogo.Image = ((System.Drawing.Image)(resources.GetObject("ppsLogo.Image")));
      this.ppsLogo.InitialImage = null;
      this.ppsLogo.Location = new System.Drawing.Point(2, 221);
      this.ppsLogo.Name = "ppsLogo";
      this.ppsLogo.Size = new System.Drawing.Size(600, 110);
      this.ppsLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
      this.ppsLogo.TabIndex = 7;
      this.ppsLogo.TabStop = false;
      // 
      // circleLabel
      // 
      this.circleLabel.AutoSize = true;
      this.circleLabel.Font = new System.Drawing.Font("Arial", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.circleLabel.Location = new System.Drawing.Point(310, 27);
      this.circleLabel.Name = "circleLabel";
      this.circleLabel.Size = new System.Drawing.Size(211, 75);
      this.circleLabel.TabIndex = 8;
      this.circleLabel.Text = "Circle";
      // 
      // pivoteLabel
      // 
      this.pivoteLabel.AutoSize = true;
      this.pivoteLabel.Font = new System.Drawing.Font("Arial", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.pivoteLabel.Location = new System.Drawing.Point(310, 102);
      this.pivoteLabel.Name = "pivoteLabel";
      this.pivoteLabel.Size = new System.Drawing.Size(233, 75);
      this.pivoteLabel.TabIndex = 9;
      this.pivoteLabel.Text = "π-vote";
      // 
      // versionLabel
      // 
      this.versionLabel.AutoSize = true;
      this.versionLabel.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.versionLabel.Location = new System.Drawing.Point(319, 179);
      this.versionLabel.Name = "versionLabel";
      this.versionLabel.Size = new System.Drawing.Size(86, 22);
      this.versionLabel.TabIndex = 10;
      this.versionLabel.Text = "Version ";
      // 
      // InitScreen
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(600, 400);
      this.ControlBox = false;
      this.Controls.Add(this.versionLabel);
      this.Controls.Add(this.pivoteLabel);
      this.Controls.Add(this.circleLabel);
      this.Controls.Add(this.ppsLogo);
      this.Controls.Add(this.pivoteLogo);
      this.Controls.Add(this.infoLabel);
      this.Controls.Add(this.progressBar);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;

      // This hack is necessary because the mono compiler/runtime seems to be broken when it comes to icons.
#if !__MonoCS__
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
#endif

      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "InitScreen";
      this.ShowInTaskbar = false;
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.TextStatusDialog_FormClosing);
      this.Load += new System.EventHandler(this.TextStatusDialog_Load);
      ((System.ComponentModel.ISupportInitialize)(this.pivoteLogo)).EndInit();
      ((System.ComponentModel.ISupportInitialize)(this.ppsLogo)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label infoLabel;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.PictureBox pivoteLogo;
    private System.Windows.Forms.PictureBox ppsLogo;
    private System.Windows.Forms.Label circleLabel;
    private System.Windows.Forms.Label pivoteLabel;
    private System.Windows.Forms.Label versionLabel;

  }
}