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
      this.votingTabControl = new System.Windows.Forms.TabControl();
      this.currentTabPage = new System.Windows.Forms.TabPage();
      this.currentVotingListControl = new Pirate.PiVote.Circle.VotingListControl();
      this.scheduledTabPage = new System.Windows.Forms.TabPage();
      this.scheduledVotingListControl = new Pirate.PiVote.Circle.VotingListControl();
      this.pastTabPage = new System.Windows.Forms.TabPage();
      this.pastVotingListControl = new Pirate.PiVote.Circle.VotingListControl();
      this.votingTabControl.SuspendLayout();
      this.currentTabPage.SuspendLayout();
      this.scheduledTabPage.SuspendLayout();
      this.pastTabPage.SuspendLayout();
      this.SuspendLayout();
      // 
      // votingTabControl
      // 
      this.votingTabControl.Controls.Add(this.currentTabPage);
      this.votingTabControl.Controls.Add(this.scheduledTabPage);
      this.votingTabControl.Controls.Add(this.pastTabPage);
      this.votingTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.votingTabControl.Location = new System.Drawing.Point(0, 0);
      this.votingTabControl.Name = "votingTabControl";
      this.votingTabControl.SelectedIndex = 0;
      this.votingTabControl.Size = new System.Drawing.Size(928, 607);
      this.votingTabControl.TabIndex = 0;
      // 
      // currentTabPage
      // 
      this.currentTabPage.Controls.Add(this.currentVotingListControl);
      this.currentTabPage.Location = new System.Drawing.Point(4, 22);
      this.currentTabPage.Name = "currentTabPage";
      this.currentTabPage.Padding = new System.Windows.Forms.Padding(3);
      this.currentTabPage.Size = new System.Drawing.Size(920, 581);
      this.currentTabPage.TabIndex = 0;
      this.currentTabPage.Text = "Current";
      this.currentTabPage.UseVisualStyleBackColor = true;
      // 
      // currentVotingListControl
      // 
      this.currentVotingListControl.AutoScroll = true;
      this.currentVotingListControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.currentVotingListControl.Location = new System.Drawing.Point(3, 3);
      this.currentVotingListControl.Name = "currentVotingListControl";
      this.currentVotingListControl.Size = new System.Drawing.Size(914, 575);
      this.currentVotingListControl.TabIndex = 0;
      this.currentVotingListControl.VotingAction += new Pirate.PiVote.Circle.VotingActionHandler(this.CurrentVotingListControl_VotingAction);
      // 
      // scheduledTabPage
      // 
      this.scheduledTabPage.Controls.Add(this.scheduledVotingListControl);
      this.scheduledTabPage.Location = new System.Drawing.Point(4, 22);
      this.scheduledTabPage.Name = "scheduledTabPage";
      this.scheduledTabPage.Padding = new System.Windows.Forms.Padding(3);
      this.scheduledTabPage.Size = new System.Drawing.Size(920, 581);
      this.scheduledTabPage.TabIndex = 1;
      this.scheduledTabPage.Text = "Scheduled";
      this.scheduledTabPage.UseVisualStyleBackColor = true;
      // 
      // scheduledVotingListControl
      // 
      this.scheduledVotingListControl.AutoScroll = true;
      this.scheduledVotingListControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.scheduledVotingListControl.Location = new System.Drawing.Point(3, 3);
      this.scheduledVotingListControl.Name = "scheduledVotingListControl";
      this.scheduledVotingListControl.Size = new System.Drawing.Size(914, 575);
      this.scheduledVotingListControl.TabIndex = 1;
      this.scheduledVotingListControl.VotingAction += new Pirate.PiVote.Circle.VotingActionHandler(this.ScheduledVotingListControl_VotingAction);
      // 
      // pastTabPage
      // 
      this.pastTabPage.Controls.Add(this.pastVotingListControl);
      this.pastTabPage.Location = new System.Drawing.Point(4, 22);
      this.pastTabPage.Name = "pastTabPage";
      this.pastTabPage.Padding = new System.Windows.Forms.Padding(3);
      this.pastTabPage.Size = new System.Drawing.Size(920, 581);
      this.pastTabPage.TabIndex = 2;
      this.pastTabPage.Text = "Past";
      this.pastTabPage.UseVisualStyleBackColor = true;
      // 
      // pastVotingListControl
      // 
      this.pastVotingListControl.AutoScroll = true;
      this.pastVotingListControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.pastVotingListControl.Location = new System.Drawing.Point(3, 3);
      this.pastVotingListControl.Name = "pastVotingListControl";
      this.pastVotingListControl.Size = new System.Drawing.Size(914, 575);
      this.pastVotingListControl.TabIndex = 2;
      this.pastVotingListControl.VotingAction += new Pirate.PiVote.Circle.VotingActionHandler(this.PastVotingListControl_VotingAction);
      // 
      // VotingListsControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.votingTabControl);
      this.Name = "VotingListsControl";
      this.Size = new System.Drawing.Size(928, 607);
      this.votingTabControl.ResumeLayout(false);
      this.currentTabPage.ResumeLayout(false);
      this.scheduledTabPage.ResumeLayout(false);
      this.pastTabPage.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl votingTabControl;
    private System.Windows.Forms.TabPage currentTabPage;
    private System.Windows.Forms.TabPage scheduledTabPage;
    private VotingListControl currentVotingListControl;
    private VotingListControl scheduledVotingListControl;
    private System.Windows.Forms.TabPage pastTabPage;
    private VotingListControl pastVotingListControl;
  }
}
