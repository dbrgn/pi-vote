/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle
{
  partial class Master
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
      this.menuStrip1 = new System.Windows.Forms.MenuStrip();
      this.statusStrip = new System.Windows.Forms.StatusStrip();
      this.statusLabel = new System.Windows.Forms.ToolStripStatusLabel();
      this.progressBar = new System.Windows.Forms.ToolStripProgressBar();
      this.subProgressBar = new System.Windows.Forms.ToolStripProgressBar();
      this.mainPanel = new System.Windows.Forms.Panel();
      this.simpleCreateCertificateControl = new Pirate.PiVote.Circle.SimpleCreateCertificateControl();
      this.voteCastControl = new Pirate.PiVote.Circle.VoteCastControl();
      this.votingListsControl = new Pirate.PiVote.Circle.VotingListsControl();
      this.statusStrip.SuspendLayout();
      this.mainPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // menuStrip1
      // 
      this.menuStrip1.Location = new System.Drawing.Point(0, 0);
      this.menuStrip1.Name = "menuStrip1";
      this.menuStrip1.Size = new System.Drawing.Size(985, 24);
      this.menuStrip1.TabIndex = 0;
      this.menuStrip1.Text = "menuStrip1";
      // 
      // statusStrip
      // 
      this.statusStrip.Font = new System.Drawing.Font("Arial", 8.25F);
      this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLabel,
            this.progressBar,
            this.subProgressBar});
      this.statusStrip.Location = new System.Drawing.Point(0, 487);
      this.statusStrip.Name = "statusStrip";
      this.statusStrip.Size = new System.Drawing.Size(985, 23);
      this.statusStrip.TabIndex = 1;
      this.statusStrip.Text = "statusStrip";
      // 
      // statusLabel
      // 
      this.statusLabel.AutoSize = false;
      this.statusLabel.Name = "statusLabel";
      this.statusLabel.Size = new System.Drawing.Size(300, 18);
      this.statusLabel.Text = "???";
      // 
      // progressBar
      // 
      this.progressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.progressBar.AutoSize = false;
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(180, 17);
      this.progressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      // 
      // subProgressBar
      // 
      this.subProgressBar.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
      this.subProgressBar.AutoSize = false;
      this.subProgressBar.Name = "subProgressBar";
      this.subProgressBar.Size = new System.Drawing.Size(180, 17);
      this.subProgressBar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
      // 
      // mainPanel
      // 
      this.mainPanel.Controls.Add(this.simpleCreateCertificateControl);
      this.mainPanel.Controls.Add(this.voteCastControl);
      this.mainPanel.Controls.Add(this.votingListsControl);
      this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mainPanel.Location = new System.Drawing.Point(0, 24);
      this.mainPanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.mainPanel.Name = "mainPanel";
      this.mainPanel.Size = new System.Drawing.Size(985, 463);
      this.mainPanel.TabIndex = 2;
      // 
      // simpleCreateCertificateControl
      // 
      this.simpleCreateCertificateControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.simpleCreateCertificateControl.Location = new System.Drawing.Point(0, 0);
      this.simpleCreateCertificateControl.Margin = new System.Windows.Forms.Padding(1);
      this.simpleCreateCertificateControl.Name = "simpleCreateCertificateControl";
      this.simpleCreateCertificateControl.Size = new System.Drawing.Size(985, 463);
      this.simpleCreateCertificateControl.TabIndex = 2;
      this.simpleCreateCertificateControl.ReturnFromControl += new System.EventHandler(this.simpleCreateCertificateControl_ReturnFromControl);
      // 
      // voteCastControl
      // 
      this.voteCastControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.voteCastControl.Font = new System.Drawing.Font("Arial Narrow", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.voteCastControl.Location = new System.Drawing.Point(0, 0);
      this.voteCastControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.voteCastControl.Name = "voteCastControl";
      this.voteCastControl.Size = new System.Drawing.Size(985, 463);
      this.voteCastControl.TabIndex = 1;
      // 
      // votingListsControl
      // 
      this.votingListsControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.votingListsControl.Location = new System.Drawing.Point(0, 0);
      this.votingListsControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.votingListsControl.Name = "votingListsControl";
      this.votingListsControl.Size = new System.Drawing.Size(985, 463);
      this.votingListsControl.TabIndex = 0;
      this.votingListsControl.VotingAction += new Pirate.PiVote.Circle.VotingActionHandler(this.VotingListsControl_VotingAction);
      // 
      // Master
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(985, 510);
      this.Controls.Add(this.mainPanel);
      this.Controls.Add(this.statusStrip);
      this.Controls.Add(this.menuStrip1);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.MainMenuStrip = this.menuStrip1;
      this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.Name = "Master";
      this.Text = "Circle";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Master_FormClosing);
      this.Load += new System.EventHandler(this.Master_Load);
      this.statusStrip.ResumeLayout(false);
      this.statusStrip.PerformLayout();
      this.mainPanel.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
    private System.Windows.Forms.StatusStrip statusStrip;
    private System.Windows.Forms.Panel mainPanel;
    private System.Windows.Forms.ToolStripStatusLabel statusLabel;
    private VotingListsControl votingListsControl;
    private VoteCastControl voteCastControl;
    private System.Windows.Forms.ToolStripProgressBar progressBar;
    private System.Windows.Forms.ToolStripProgressBar subProgressBar;
    private SimpleCreateCertificateControl simpleCreateCertificateControl;
  }
}

