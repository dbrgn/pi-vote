/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

namespace Pirate.PiVote.Circle
{
  partial class VotingListsControl
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
      this.votingTabPage = new System.Windows.Forms.TabControl();
      this.currentTabPage = new System.Windows.Forms.TabPage();
      this.pastTabPage = new System.Windows.Forms.TabPage();
      this.certificateStatus = new Pirate.PiVote.Circle.VotingList.CertificateStatusControl();
      this.currentVotingListControl = new Pirate.PiVote.Circle.VotingListControl();
      this.pastVotingListControl = new Pirate.PiVote.Circle.VotingListControl();
      this.votingTabPage.SuspendLayout();
      this.currentTabPage.SuspendLayout();
      this.pastTabPage.SuspendLayout();
      this.SuspendLayout();
      // 
      // votingTabPage
      // 
      this.votingTabPage.Controls.Add(this.currentTabPage);
      this.votingTabPage.Controls.Add(this.pastTabPage);
      this.votingTabPage.Dock = System.Windows.Forms.DockStyle.Fill;
      this.votingTabPage.Location = new System.Drawing.Point(0, 0);
      this.votingTabPage.Name = "votingTabPage";
      this.votingTabPage.SelectedIndex = 0;
      this.votingTabPage.Size = new System.Drawing.Size(928, 654);
      this.votingTabPage.TabIndex = 0;
      // 
      // currentTabPage
      // 
      this.currentTabPage.Controls.Add(this.certificateStatus);
      this.currentTabPage.Controls.Add(this.currentVotingListControl);
      this.currentTabPage.Location = new System.Drawing.Point(4, 23);
      this.currentTabPage.Name = "currentTabPage";
      this.currentTabPage.Padding = new System.Windows.Forms.Padding(3);
      this.currentTabPage.Size = new System.Drawing.Size(920, 627);
      this.currentTabPage.TabIndex = 0;
      this.currentTabPage.Text = "Current";
      this.currentTabPage.UseVisualStyleBackColor = true;
      // 
      // pastTabPage
      // 
      this.pastTabPage.Controls.Add(this.pastVotingListControl);
      this.pastTabPage.Location = new System.Drawing.Point(4, 23);
      this.pastTabPage.Name = "pastTabPage";
      this.pastTabPage.Padding = new System.Windows.Forms.Padding(3);
      this.pastTabPage.Size = new System.Drawing.Size(920, 627);
      this.pastTabPage.TabIndex = 2;
      this.pastTabPage.Text = "Past";
      this.pastTabPage.UseVisualStyleBackColor = true;
      // 
      // certificateStatus
      // 
      this.certificateStatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.certificateStatus.Controller = null;
      this.certificateStatus.Font = new System.Drawing.Font("Arial", 8.25F);
      this.certificateStatus.Location = new System.Drawing.Point(0, 0);
      this.certificateStatus.Name = "certificateStatus";
      this.certificateStatus.Size = new System.Drawing.Size(920, 32);
      this.certificateStatus.TabIndex = 1;
      this.certificateStatus.CreateCertificate += new System.EventHandler(this.CertificateStatus_CreateCertificate);
      this.certificateStatus.ResumeCertificate += new System.EventHandler(this.CertificateStatus_ResumeCertificate);
      // 
      // currentVotingListControl
      // 
      this.currentVotingListControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.currentVotingListControl.AutoScroll = true;
      this.currentVotingListControl.Font = new System.Drawing.Font("Arial", 8.25F);
      this.currentVotingListControl.Location = new System.Drawing.Point(0, 38);
      this.currentVotingListControl.Name = "currentVotingListControl";
      this.currentVotingListControl.Size = new System.Drawing.Size(921, 585);
      this.currentVotingListControl.TabIndex = 0;
      this.currentVotingListControl.VotingAction += new Pirate.PiVote.Circle.VotingActionHandler(this.CurrentVotingListControl_VotingAction);
      // 
      // pastVotingListControl
      // 
      this.pastVotingListControl.AutoScroll = true;
      this.pastVotingListControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pastVotingListControl.Font = new System.Drawing.Font("Arial", 8.25F);
      this.pastVotingListControl.Location = new System.Drawing.Point(3, 3);
      this.pastVotingListControl.Name = "pastVotingListControl";
      this.pastVotingListControl.Size = new System.Drawing.Size(914, 621);
      this.pastVotingListControl.TabIndex = 2;
      this.pastVotingListControl.VotingAction += new Pirate.PiVote.Circle.VotingActionHandler(this.PastVotingListControl_VotingAction);
      // 
      // VotingListsControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.Controls.Add(this.votingTabPage);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "VotingListsControl";
      this.Size = new System.Drawing.Size(928, 654);
      this.votingTabPage.ResumeLayout(false);
      this.currentTabPage.ResumeLayout(false);
      this.pastTabPage.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl votingTabPage;
    private System.Windows.Forms.TabPage currentTabPage;
    private VotingListControl currentVotingListControl;
    private System.Windows.Forms.TabPage pastTabPage;
    private VotingListControl pastVotingListControl;
    private VotingList.CertificateStatusControl certificateStatus;
  }
}
