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
      this.votingList = new System.Windows.Forms.ListView();
      this.titleColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.statusColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.voteFromColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.voteUntilColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.authoritiesColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.envelopesColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // votingList
      // 
      this.votingList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.votingList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.titleColumnHeader,
            this.statusColumnHeader,
            this.voteFromColumnHeader,
            this.voteUntilColumnHeader,
            this.authoritiesColumnHeader,
            this.envelopesColumnHeader});
      this.votingList.Enabled = false;
      this.votingList.FullRowSelect = true;
      this.votingList.Location = new System.Drawing.Point(3, 3);
      this.votingList.MultiSelect = false;
      this.votingList.Name = "votingList";
      this.votingList.Size = new System.Drawing.Size(694, 482);
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
      this.Size = new System.Drawing.Size(700, 487);
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




  }
}
