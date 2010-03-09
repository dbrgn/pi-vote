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
      this.SuspendLayout();
      // 
      // votingList
      // 
      this.votingList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.titleColumnHeader,
            this.statusColumnHeader});
      this.votingList.Enabled = false;
      this.votingList.FullRowSelect = true;
      this.votingList.Location = new System.Drawing.Point(3, 3);
      this.votingList.MultiSelect = false;
      this.votingList.Name = "votingList";
      this.votingList.Size = new System.Drawing.Size(694, 532);
      this.votingList.TabIndex = 0;
      this.votingList.UseCompatibleStateImageBehavior = false;
      this.votingList.View = System.Windows.Forms.View.Details;
      this.votingList.SelectedIndexChanged += new System.EventHandler(this.votingList_SelectedIndexChanged);
      // 
      // titleColumnHeader
      // 
      this.titleColumnHeader.Text = "Title";
      this.titleColumnHeader.Width = 460;
      // 
      // statusColumnHeader
      // 
      this.statusColumnHeader.Text = "Status";
      this.statusColumnHeader.Width = 150;
      // 
      // ListVotingsItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.votingList);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "ListVotingsItem";
      this.Size = new System.Drawing.Size(700, 538);
      this.ResumeLayout(false);

    }

    #endregion

    private ListView votingList;
    private ColumnHeader titleColumnHeader;
    private ColumnHeader statusColumnHeader;




  }
}
