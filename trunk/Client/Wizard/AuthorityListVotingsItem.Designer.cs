/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace Pirate.PiVote.Client
{
  partial class AuthorityListVotingsItem
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
      this.votingList = new System.Windows.Forms.ListView();
      this.titleColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.groupColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.statusColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.voteFromColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.voteUntilColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.authorityColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.envelopesColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.votingListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.refreshMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.decipherButton = new System.Windows.Forms.Button();
      this.checkSharesButton = new System.Windows.Forms.Button();
      this.createSharesButton = new System.Windows.Forms.Button();
      this.votingListMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // votingList
      // 
      this.votingList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.votingList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.statusColumnHeader,
            this.titleColumnHeader,
            this.groupColumnHeader,
            this.voteFromColumnHeader,
            this.voteUntilColumnHeader,
            this.authorityColumnHeader,
            this.envelopesColumnHeader});
      this.votingList.ContextMenuStrip = this.votingListMenu;
      this.votingList.Enabled = false;
      this.votingList.FullRowSelect = true;
      this.votingList.HideSelection = false;
      this.votingList.Location = new System.Drawing.Point(2, 2);
      this.votingList.Margin = new System.Windows.Forms.Padding(2);
      this.votingList.MultiSelect = false;
      this.votingList.Name = "votingList";
      this.votingList.Size = new System.Drawing.Size(760, 362);
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
      this.groupColumnHeader.Width = 120;
      // 
      // statusColumnHeader
      // 
      this.statusColumnHeader.Text = "Status";
      this.statusColumnHeader.Width = 150;
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
      // votingListMenu
      // 
      this.votingListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.refreshMenu});
      this.votingListMenu.Name = "votingListMenu";
      this.votingListMenu.Size = new System.Drawing.Size(153, 48);
      // 
      // refreshMenu
      // 
      this.refreshMenu.Name = "refreshMenu";
      this.refreshMenu.Size = new System.Drawing.Size(152, 22);
      this.refreshMenu.Text = "&Refresh";
      this.refreshMenu.Click += new System.EventHandler(this.refreshMenu_Click);
      // 
      // decipherButton
      // 
      this.decipherButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.decipherButton.Enabled = false;
      this.decipherButton.Location = new System.Drawing.Point(619, 368);
      this.decipherButton.Margin = new System.Windows.Forms.Padding(2);
      this.decipherButton.Name = "decipherButton";
      this.decipherButton.Size = new System.Drawing.Size(142, 27);
      this.decipherButton.TabIndex = 1;
      this.decipherButton.Text = "&Decipher";
      this.decipherButton.UseVisualStyleBackColor = true;
      this.decipherButton.Click += new System.EventHandler(this.decipherButton_Click);
      // 
      // checkSharesButton
      // 
      this.checkSharesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.checkSharesButton.Enabled = false;
      this.checkSharesButton.Location = new System.Drawing.Point(471, 368);
      this.checkSharesButton.Margin = new System.Windows.Forms.Padding(2);
      this.checkSharesButton.Name = "checkSharesButton";
      this.checkSharesButton.Size = new System.Drawing.Size(144, 27);
      this.checkSharesButton.TabIndex = 2;
      this.checkSharesButton.Text = "&Check Shares";
      this.checkSharesButton.UseVisualStyleBackColor = true;
      this.checkSharesButton.Click += new System.EventHandler(this.checkSharesButton_Click);
      // 
      // createSharesButton
      // 
      this.createSharesButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.createSharesButton.Enabled = false;
      this.createSharesButton.Location = new System.Drawing.Point(323, 368);
      this.createSharesButton.Margin = new System.Windows.Forms.Padding(2);
      this.createSharesButton.Name = "createSharesButton";
      this.createSharesButton.Size = new System.Drawing.Size(144, 27);
      this.createSharesButton.TabIndex = 3;
      this.createSharesButton.Text = "Create &Shares";
      this.createSharesButton.UseVisualStyleBackColor = true;
      this.createSharesButton.Click += new System.EventHandler(this.createSharesButton_Click);
      // 
      // AuthorityListVotingsItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.createSharesButton);
      this.Controls.Add(this.checkSharesButton);
      this.Controls.Add(this.decipherButton);
      this.Controls.Add(this.votingList);
      this.Margin = new System.Windows.Forms.Padding(1);
      this.Name = "AuthorityListVotingsItem";
      this.Size = new System.Drawing.Size(763, 399);
      this.votingListMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private ListView votingList;
    private ColumnHeader titleColumnHeader;
    private ColumnHeader statusColumnHeader;
    private Button decipherButton;
    private Button checkSharesButton;
    private Button createSharesButton;
    private ColumnHeader voteFromColumnHeader;
    private ColumnHeader voteUntilColumnHeader;
    private ColumnHeader authorityColumnHeader;
    private ColumnHeader envelopesColumnHeader;
    private ColumnHeader groupColumnHeader;
    private ContextMenuStrip votingListMenu;
    private ToolStripMenuItem refreshMenu;




  }
}
