/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Pirate.PiVote.Client
{
  partial class AdminChooseItem
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
      this.components = new System.ComponentModel.Container();
      this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.createVotingMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.deleteMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.downloadSignatureRequestsMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.uploadSignatureResponsesMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.uploadCertificateStorageMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.titleColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.groupColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.statusColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.voteFromColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.voteUntilColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.authorityColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.envelopesColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.votingList = new System.Windows.Forms.ListView();
      this.contextMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // contextMenu
      // 
      this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createVotingMenu,
            this.deleteMenu,
            this.toolStripMenuItem1,
            this.downloadSignatureRequestsMenu,
            this.uploadSignatureResponsesMenu,
            this.toolStripMenuItem2,
            this.uploadCertificateStorageMenu});
      this.contextMenu.Name = "contextMenu";
      this.contextMenu.Size = new System.Drawing.Size(219, 148);
      this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
      // 
      // createVotingMenu
      // 
      this.createVotingMenu.Name = "createVotingMenu";
      this.createVotingMenu.Size = new System.Drawing.Size(218, 22);
      this.createVotingMenu.Text = "&Create";
      this.createVotingMenu.Click += new System.EventHandler(this.createVotingMenu_Click);
      // 
      // deleteMenu
      // 
      this.deleteMenu.Name = "deleteMenu";
      this.deleteMenu.Size = new System.Drawing.Size(218, 22);
      this.deleteMenu.Text = "&Delete";
      this.deleteMenu.Click += new System.EventHandler(this.deleteMenu_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(215, 6);
      // 
      // downloadSignatureRequestsMenu
      // 
      this.downloadSignatureRequestsMenu.Name = "downloadSignatureRequestsMenu";
      this.downloadSignatureRequestsMenu.Size = new System.Drawing.Size(218, 22);
      this.downloadSignatureRequestsMenu.Text = "&Download Signature Requests";
      this.downloadSignatureRequestsMenu.Click += new System.EventHandler(this.downloadSignatureRequestsMenu_Click);
      // 
      // uploadSignatureResponsesMenu
      // 
      this.uploadSignatureResponsesMenu.Name = "uploadSignatureResponsesMenu";
      this.uploadSignatureResponsesMenu.Size = new System.Drawing.Size(218, 22);
      this.uploadSignatureResponsesMenu.Text = "&Upload Signature Responses";
      this.uploadSignatureResponsesMenu.Click += new System.EventHandler(this.uploadSignatureResponsesMenu_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(215, 6);
      // 
      // uploadCertificateStorageMenu
      // 
      this.uploadCertificateStorageMenu.Name = "uploadCertificateStorageMenu";
      this.uploadCertificateStorageMenu.Size = new System.Drawing.Size(218, 22);
      this.uploadCertificateStorageMenu.Text = "Upload Certificate &Storage";
      this.uploadCertificateStorageMenu.Click += new System.EventHandler(this.uploadCertificateStorageToolStripMenuItem_Click);
      // 
      // titleColumnHeader
      // 
      this.titleColumnHeader.Text = "Title";
      this.titleColumnHeader.Width = 300;
      // 
      // groupColumnHeader
      // 
      this.groupColumnHeader.Text = "Group";
      this.groupColumnHeader.Width = 120;
      // 
      // statusColumnHeader
      // 
      this.statusColumnHeader.Text = "Status";
      this.statusColumnHeader.Width = 100;
      // 
      // voteFromColumnHeader
      // 
      this.voteFromColumnHeader.Text = "From";
      this.voteFromColumnHeader.Width = 70;
      // 
      // voteUntilColumnHeader
      // 
      this.voteUntilColumnHeader.Text = "Until";
      this.voteUntilColumnHeader.Width = 70;
      // 
      // authorityColumnHeader
      // 
      this.authorityColumnHeader.Text = "Authorities";
      this.authorityColumnHeader.Width = 70;
      // 
      // envelopesColumnHeader
      // 
      this.envelopesColumnHeader.Text = "Votes";
      this.envelopesColumnHeader.Width = 70;
      // 
      // votingList
      // 
      this.votingList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.votingList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.titleColumnHeader,
            this.groupColumnHeader,
            this.statusColumnHeader,
            this.voteFromColumnHeader,
            this.voteUntilColumnHeader,
            this.authorityColumnHeader,
            this.envelopesColumnHeader});
      this.votingList.ContextMenuStrip = this.contextMenu;
      this.votingList.Enabled = false;
      this.votingList.FullRowSelect = true;
      this.votingList.HideSelection = false;
      this.votingList.Location = new System.Drawing.Point(2, 2);
      this.votingList.Margin = new System.Windows.Forms.Padding(2);
      this.votingList.MultiSelect = false;
      this.votingList.Name = "votingList";
      this.votingList.Size = new System.Drawing.Size(756, 358);
      this.votingList.TabIndex = 9;
      this.votingList.UseCompatibleStateImageBehavior = false;
      this.votingList.View = System.Windows.Forms.View.Details;
      // 
      // AdminChooseItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.votingList);
      this.Margin = new System.Windows.Forms.Padding(1);
      this.Name = "AdminChooseItem";
      this.Size = new System.Drawing.Size(760, 362);
      this.contextMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private ContextMenuStrip contextMenu;
    private ToolStripMenuItem deleteMenu;
    private ColumnHeader titleColumnHeader;
    private ColumnHeader groupColumnHeader;
    private ColumnHeader statusColumnHeader;
    private ColumnHeader voteFromColumnHeader;
    private ColumnHeader voteUntilColumnHeader;
    private ColumnHeader authorityColumnHeader;
    private ColumnHeader envelopesColumnHeader;
    private ListView votingList;
    private ToolStripMenuItem createVotingMenu;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripMenuItem downloadSignatureRequestsMenu;
    private ToolStripMenuItem uploadSignatureResponsesMenu;
    private ToolStripSeparator toolStripMenuItem2;
    private ToolStripMenuItem uploadCertificateStorageMenu;



  }
}
