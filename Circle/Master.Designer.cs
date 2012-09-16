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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Master));
      this.mainMenu = new System.Windows.Forms.MenuStrip();
      this.votingsMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.refreshVotingsMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.createVotingMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.certificatesMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.createCertificateMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.resumeCreationMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.reloadCertificateMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.manageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.downloadSignatureRequestsMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.uploadSignatureResponsesMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.uploadCertificateStorageMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.generateSignCheckMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.languageMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.englishMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.germanMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.frenchMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.extrasMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.configMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.statsMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.statsCertificateRequestsMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.statsVotingMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.statsBallotsMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.statsTimelineMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.mainPanel = new System.Windows.Forms.Panel();
      this.votingListsControl = new Pirate.PiVote.Circle.VotingListsControl();
      this.mainMenu.SuspendLayout();
      this.mainPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // mainMenu
      // 
      this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.votingsMenu,
            this.certificatesMenu,
            this.languageMenu,
            this.extrasMenu});
      this.mainMenu.Location = new System.Drawing.Point(0, 0);
      this.mainMenu.Name = "mainMenu";
      this.mainMenu.Size = new System.Drawing.Size(1016, 24);
      this.mainMenu.TabIndex = 0;
      this.mainMenu.Text = "mainMenu";
      // 
      // votingsMenu
      // 
      this.votingsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshVotingsMenu,
            this.createVotingMenu});
      this.votingsMenu.Name = "votingsMenu";
      this.votingsMenu.Size = new System.Drawing.Size(54, 20);
      this.votingsMenu.Text = "&Votings";
      // 
      // refreshVotingsMenu
      // 
      this.refreshVotingsMenu.Name = "refreshVotingsMenu";
      this.refreshVotingsMenu.ShortcutKeyDisplayString = "F5";
      this.refreshVotingsMenu.Size = new System.Drawing.Size(131, 22);
      this.refreshVotingsMenu.Text = "&Refresh";
      this.refreshVotingsMenu.Click += new System.EventHandler(this.RefreshVotingsMenu_Click);
      // 
      // createVotingMenu
      // 
      this.createVotingMenu.Name = "createVotingMenu";
      this.createVotingMenu.Size = new System.Drawing.Size(131, 22);
      this.createVotingMenu.Text = "&Create";
      this.createVotingMenu.Click += new System.EventHandler(this.createVotingMenu_Click);
      // 
      // certificatesMenu
      // 
      this.certificatesMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createCertificateMenu,
            this.resumeCreationMenu,
            this.reloadCertificateMenu,
            this.manageToolStripMenuItem,
            this.downloadSignatureRequestsMenu,
            this.uploadSignatureResponsesMenu,
            this.uploadCertificateStorageMenu,
            this.generateSignCheckMenu});
      this.certificatesMenu.Name = "certificatesMenu";
      this.certificatesMenu.Size = new System.Drawing.Size(74, 20);
      this.certificatesMenu.Text = "&Certificates";
      // 
      // createCertificateMenu
      // 
      this.createCertificateMenu.Name = "createCertificateMenu";
      this.createCertificateMenu.Size = new System.Drawing.Size(214, 22);
      this.createCertificateMenu.Text = "&Create New";
      this.createCertificateMenu.Click += new System.EventHandler(this.CreateCertificateMenu_Click);
      // 
      // resumeCreationMenu
      // 
      this.resumeCreationMenu.Name = "resumeCreationMenu";
      this.resumeCreationMenu.Size = new System.Drawing.Size(214, 22);
      this.resumeCreationMenu.Text = "&Resume creation";
      this.resumeCreationMenu.Click += new System.EventHandler(this.resumeCreationMenu_Click);
      // 
      // reloadCertificateMenu
      // 
      this.reloadCertificateMenu.Name = "reloadCertificateMenu";
      this.reloadCertificateMenu.Size = new System.Drawing.Size(214, 22);
      this.reloadCertificateMenu.Text = "&Reload All";
      this.reloadCertificateMenu.Click += new System.EventHandler(this.ReloadCertificateMenu_Click);
      // 
      // manageToolStripMenuItem
      // 
      this.manageToolStripMenuItem.Name = "manageToolStripMenuItem";
      this.manageToolStripMenuItem.Size = new System.Drawing.Size(214, 22);
      this.manageToolStripMenuItem.Text = "&Manage...";
      this.manageToolStripMenuItem.Click += new System.EventHandler(this.manageToolStripMenuItem_Click);
      // 
      // downloadSignatureRequestsMenu
      // 
      this.downloadSignatureRequestsMenu.Name = "downloadSignatureRequestsMenu";
      this.downloadSignatureRequestsMenu.Size = new System.Drawing.Size(214, 22);
      this.downloadSignatureRequestsMenu.Text = "Download signature requests";
      this.downloadSignatureRequestsMenu.Click += new System.EventHandler(this.downloadSignatureRequestsMenu_Click);
      // 
      // uploadSignatureResponsesMenu
      // 
      this.uploadSignatureResponsesMenu.Name = "uploadSignatureResponsesMenu";
      this.uploadSignatureResponsesMenu.Size = new System.Drawing.Size(214, 22);
      this.uploadSignatureResponsesMenu.Text = "Upload signature responses";
      this.uploadSignatureResponsesMenu.Click += new System.EventHandler(this.uploadSignatureResponsesMenu_Click);
      // 
      // uploadCertificateStorageMenu
      // 
      this.uploadCertificateStorageMenu.Name = "uploadCertificateStorageMenu";
      this.uploadCertificateStorageMenu.Size = new System.Drawing.Size(214, 22);
      this.uploadCertificateStorageMenu.Text = "Upload certificate storage";
      this.uploadCertificateStorageMenu.Click += new System.EventHandler(this.uploadCertificateStorageMenu_Click);
      // 
      // generateSignCheckMenu
      // 
      this.generateSignCheckMenu.Name = "generateSignCheckMenu";
      this.generateSignCheckMenu.Size = new System.Drawing.Size(214, 22);
      this.generateSignCheckMenu.Text = "Generate Sign Check";
      this.generateSignCheckMenu.Click += new System.EventHandler(this.generateSignCheckMenu_Click);
      // 
      // languageMenu
      // 
      this.languageMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.englishMenu,
            this.germanMenu,
            this.frenchMenu});
      this.languageMenu.Name = "languageMenu";
      this.languageMenu.Size = new System.Drawing.Size(66, 20);
      this.languageMenu.Text = "&Language";
      // 
      // englishMenu
      // 
      this.englishMenu.Name = "englishMenu";
      this.englishMenu.Size = new System.Drawing.Size(114, 22);
      this.englishMenu.Text = "&English";
      this.englishMenu.Click += new System.EventHandler(this.EnglishMenu_Click);
      // 
      // germanMenu
      // 
      this.germanMenu.Name = "germanMenu";
      this.germanMenu.Size = new System.Drawing.Size(114, 22);
      this.germanMenu.Text = "&Deutsch";
      this.germanMenu.Click += new System.EventHandler(this.GermanMenu_Click);
      // 
      // frenchMenu
      // 
      this.frenchMenu.Name = "frenchMenu";
      this.frenchMenu.Size = new System.Drawing.Size(114, 22);
      this.frenchMenu.Text = "&Français";
      this.frenchMenu.Click += new System.EventHandler(this.FrenchMenu_Click);
      // 
      // extrasMenu
      // 
      this.extrasMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.configMenu,
            this.statsMenu});
      this.extrasMenu.Name = "extrasMenu";
      this.extrasMenu.Size = new System.Drawing.Size(50, 20);
      this.extrasMenu.Text = "E&xtras";
      // 
      // configMenu
      // 
      this.configMenu.Name = "configMenu";
      this.configMenu.Size = new System.Drawing.Size(152, 22);
      this.configMenu.Text = "&Config";
      this.configMenu.Click += new System.EventHandler(this.configMenu_Click);
      // 
      // statsMenu
      // 
      this.statsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statsCertificateRequestsMenu,
            this.statsVotingMenu,
            this.statsBallotsMenu,
            this.statsTimelineMenu});
      this.statsMenu.Name = "statsMenu";
      this.statsMenu.Size = new System.Drawing.Size(152, 22);
      this.statsMenu.Text = "st";
      // 
      // statsCertificateRequestsMenu
      // 
      this.statsCertificateRequestsMenu.Name = "statsCertificateRequestsMenu";
      this.statsCertificateRequestsMenu.Size = new System.Drawing.Size(172, 22);
      this.statsCertificateRequestsMenu.Text = "Certificate Requests";
      this.statsCertificateRequestsMenu.Click += new System.EventHandler(this.statsCertificateRequestsMenu_Click);
      // 
      // statsVotingMenu
      // 
      this.statsVotingMenu.Name = "statsVotingMenu";
      this.statsVotingMenu.Size = new System.Drawing.Size(172, 22);
      this.statsVotingMenu.Text = "Votings";
      this.statsVotingMenu.Click += new System.EventHandler(this.statsVotingMenu_Click);
      // 
      // statsBallotsMenu
      // 
      this.statsBallotsMenu.Name = "statsBallotsMenu";
      this.statsBallotsMenu.Size = new System.Drawing.Size(172, 22);
      this.statsBallotsMenu.Text = "Ballots";
      this.statsBallotsMenu.Click += new System.EventHandler(this.statsBallotsMenu_Click);
      // 
      // statsTimelineMenu
      // 
      this.statsTimelineMenu.Name = "statsTimelineMenu";
      this.statsTimelineMenu.Size = new System.Drawing.Size(172, 22);
      this.statsTimelineMenu.Text = "Timeline";
      this.statsTimelineMenu.Click += new System.EventHandler(this.statsTimelineMenu_Click);
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
      this.votingListsControl.BackColor = System.Drawing.Color.White;
      this.votingListsControl.Dock = System.Windows.Forms.DockStyle.Fill;
      this.votingListsControl.Font = new System.Drawing.Font("Arial", 8.25F);
      this.votingListsControl.Location = new System.Drawing.Point(0, 0);
      this.votingListsControl.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.votingListsControl.Name = "votingListsControl";
      this.votingListsControl.Size = new System.Drawing.Size(1016, 549);
      this.votingListsControl.TabIndex = 0;
      this.votingListsControl.VotingAction += new Pirate.PiVote.Circle.VotingActionHandler(this.VotingListsControl_VotingAction);
      this.votingListsControl.CreateCertificate += new System.EventHandler(this.VotingListsControl_CreateCertificate);
      this.votingListsControl.ResumeCertificate += new System.EventHandler(this.VotingListsControl_ResumeCertificate);
      // 
      // Master
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.BackColor = System.Drawing.Color.White;
      this.ClientSize = new System.Drawing.Size(1016, 573);
      this.Controls.Add(this.mainPanel);
      this.Controls.Add(this.mainMenu);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
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
    private System.Windows.Forms.ToolStripMenuItem certificatesMenu;
    private System.Windows.Forms.ToolStripMenuItem createCertificateMenu;
    private System.Windows.Forms.ToolStripMenuItem votingsMenu;
    private System.Windows.Forms.ToolStripMenuItem refreshVotingsMenu;
    private System.Windows.Forms.ToolStripMenuItem reloadCertificateMenu;
    private System.Windows.Forms.ToolStripMenuItem languageMenu;
    private System.Windows.Forms.ToolStripMenuItem englishMenu;
    private System.Windows.Forms.ToolStripMenuItem germanMenu;
    private System.Windows.Forms.ToolStripMenuItem frenchMenu;
    private System.Windows.Forms.ToolStripMenuItem resumeCreationMenu;
    private System.Windows.Forms.ToolStripMenuItem manageToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem createVotingMenu;
    private System.Windows.Forms.ToolStripMenuItem downloadSignatureRequestsMenu;
    private System.Windows.Forms.ToolStripMenuItem uploadSignatureResponsesMenu;
    private System.Windows.Forms.ToolStripMenuItem uploadCertificateStorageMenu;
    private System.Windows.Forms.ToolStripMenuItem extrasMenu;
    private System.Windows.Forms.ToolStripMenuItem configMenu;
    private System.Windows.Forms.ToolStripMenuItem generateSignCheckMenu;
    private System.Windows.Forms.ToolStripMenuItem statsMenu;
    private System.Windows.Forms.ToolStripMenuItem statsCertificateRequestsMenu;
    private System.Windows.Forms.ToolStripMenuItem statsVotingMenu;
    private System.Windows.Forms.ToolStripMenuItem statsBallotsMenu;
    private System.Windows.Forms.ToolStripMenuItem statsTimelineMenu;
  }
}

