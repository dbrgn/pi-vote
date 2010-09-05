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
  partial class ListVotingsItem
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
      this.votingListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.downloadVotingMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.votingList = new System.Windows.Forms.ListView();
      this.titleColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.groupColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.statusColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.voteFromColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.voteUntilColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.authoritiesColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.envelopesColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.votingListContextMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // votingListContextMenu
      // 
      this.votingListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.downloadVotingMenuItem});
      this.votingListContextMenu.Name = "votingListContextMenu";
      this.votingListContextMenu.Size = new System.Drawing.Size(122, 26);
      this.votingListContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.votingListContextMenu_Opening);
      // 
      // downloadVotingMenuItem
      // 
      this.downloadVotingMenuItem.Name = "downloadVotingMenuItem";
      this.downloadVotingMenuItem.Size = new System.Drawing.Size(121, 22);
      this.downloadVotingMenuItem.Text = "&Download";
      this.downloadVotingMenuItem.Click += new System.EventHandler(this.downloadVotingMenuItem_Click);
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
            this.authoritiesColumnHeader,
            this.envelopesColumnHeader});
      this.votingList.ContextMenuStrip = this.votingListContextMenu;
      this.votingList.Enabled = false;
      this.votingList.FullRowSelect = true;
      this.votingList.HideSelection = false;
      this.votingList.Location = new System.Drawing.Point(3, 3);
      this.votingList.MultiSelect = false;
      this.votingList.Name = "votingList";
      this.votingList.Size = new System.Drawing.Size(759, 482);
      this.votingList.TabIndex = 0;
      this.votingList.UseCompatibleStateImageBehavior = false;
      this.votingList.View = System.Windows.Forms.View.Details;
      this.votingList.SelectedIndexChanged += new System.EventHandler(this.votingList_SelectedIndexChanged);
      // 
      // titleColumnHeader
      // 
      this.titleColumnHeader.Text = "Title";
      this.titleColumnHeader.Width = 300;
      // 
      // groupColumnHeader
      // 
      this.groupColumnHeader.Text = "Group";
      this.groupColumnHeader.Width = 150;
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
      // authoritiesColumnHeader
      // 
      this.authoritiesColumnHeader.Text = "Authorities";
      this.authoritiesColumnHeader.Width = 70;
      // 
      // envelopesColumnHeader
      // 
      this.envelopesColumnHeader.Text = "Votes";
      this.envelopesColumnHeader.Width = 70;
      // 
      // ListVotingsItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.votingList);
      this.Margin = new System.Windows.Forms.Padding(3);
      this.Name = "ListVotingsItem";
      this.Size = new System.Drawing.Size(765, 487);
      this.votingListContextMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private ListView votingList;
    private ColumnHeader titleColumnHeader;
    private ColumnHeader statusColumnHeader;
    private ColumnHeader voteFromColumnHeader;
    private ColumnHeader voteUntilColumnHeader;
    private ColumnHeader authoritiesColumnHeader;
    private ColumnHeader envelopesColumnHeader;
    private ContextMenuStrip votingListContextMenu;
    private ToolStripMenuItem downloadVotingMenuItem;
    private ColumnHeader groupColumnHeader;




  }
}
