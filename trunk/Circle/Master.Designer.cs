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
      this.mainMenu = new System.Windows.Forms.MenuStrip();
      this.votingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.refreshToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.certificatesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.createNewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.mainPanel = new System.Windows.Forms.Panel();
      this.votingListsControl = new Pirate.PiVote.Circle.VotingListsControl();
      this.mainMenu.SuspendLayout();
      this.mainPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // mainMenu
      // 
      this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.votingsToolStripMenuItem,
            this.certificatesToolStripMenuItem});
      this.mainMenu.Location = new System.Drawing.Point(0, 0);
      this.mainMenu.Name = "mainMenu";
      this.mainMenu.Size = new System.Drawing.Size(1016, 24);
      this.mainMenu.TabIndex = 0;
      this.mainMenu.Text = "menuStrip1";
      // 
      // votingsToolStripMenuItem
      // 
      this.votingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshToolStripMenuItem});
      this.votingsToolStripMenuItem.Name = "votingsToolStripMenuItem";
      this.votingsToolStripMenuItem.Size = new System.Drawing.Size(54, 20);
      this.votingsToolStripMenuItem.Text = "&Votings";
      // 
      // refreshToolStripMenuItem
      // 
      this.refreshToolStripMenuItem.Name = "refreshToolStripMenuItem";
      this.refreshToolStripMenuItem.ShortcutKeyDisplayString = "F5";
      this.refreshToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
      this.refreshToolStripMenuItem.Text = "&Refresh";
      this.refreshToolStripMenuItem.Click += new System.EventHandler(this.refreshToolStripMenuItem_Click);
      // 
      // certificatesToolStripMenuItem
      // 
      this.certificatesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewToolStripMenuItem});
      this.certificatesToolStripMenuItem.Name = "certificatesToolStripMenuItem";
      this.certificatesToolStripMenuItem.Size = new System.Drawing.Size(74, 20);
      this.certificatesToolStripMenuItem.Text = "&Certificates";
      // 
      // createNewToolStripMenuItem
      // 
      this.createNewToolStripMenuItem.Name = "createNewToolStripMenuItem";
      this.createNewToolStripMenuItem.Size = new System.Drawing.Size(131, 22);
      this.createNewToolStripMenuItem.Text = "&Create New";
      this.createNewToolStripMenuItem.Click += new System.EventHandler(this.createNewToolStripMenuItem_Click);
      // 
      // mainPanel
      // 
      this.mainPanel.Controls.Add(this.votingListsControl);
      this.mainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
      this.mainPanel.Location = new System.Drawing.Point(0, 24);
      this.mainPanel.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.mainPanel.Name = "mainPanel";
      this.mainPanel.Size = new System.Drawing.Size(1016, 549);
      this.mainPanel.TabIndex = 2;
      // 
      // votingListsControl
      // 
      this.votingListsControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.votingListsControl.Location = new System.Drawing.Point(0, 0);
      this.votingListsControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.votingListsControl.Name = "votingListsControl";
      this.votingListsControl.Size = new System.Drawing.Size(1016, 549);
      this.votingListsControl.TabIndex = 0;
      this.votingListsControl.VotingAction += new Pirate.PiVote.Circle.VotingActionHandler(this.VotingListsControl_VotingAction);
      // 
      // Master
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1016, 573);
      this.Controls.Add(this.mainPanel);
      this.Controls.Add(this.mainMenu);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.KeyPreview = true;
      this.MainMenuStrip = this.mainMenu;
      this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.MinimumSize = new System.Drawing.Size(300, 200);
      this.Name = "Master";
      this.Text = "Circle";
      this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Master_FormClosing);
      this.Load += new System.EventHandler(this.Master_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Master_KeyDown);
      this.mainMenu.ResumeLayout(false);
      this.mainMenu.PerformLayout();
      this.mainPanel.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mainMenu;
    private System.Windows.Forms.Panel mainPanel;
    private VotingListsControl votingListsControl;
    private System.Windows.Forms.ToolStripMenuItem certificatesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem createNewToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem votingsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem refreshToolStripMenuItem;
  }
}

