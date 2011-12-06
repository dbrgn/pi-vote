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
  partial class ChooseCertificateItem
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
      this.infoLabel = new System.Windows.Forms.Label();
      this.certificateListLabel = new System.Windows.Forms.Label();
      this.certificateList = new System.Windows.Forms.ListView();
      this.typeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.idColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.certificateListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.createMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.loadMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.saveMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.deleteMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.encryptMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.changePassphraseMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.backupMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.restoreMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
      this.verifyShareproofMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.certificateListMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // infoLabel
      // 
      this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.infoLabel.Location = new System.Drawing.Point(4, 19);
      this.infoLabel.Name = "infoLabel";
      this.infoLabel.Size = new System.Drawing.Size(693, 30);
      this.infoLabel.TabIndex = 11;
      this.infoLabel.Text = "Info";
      // 
      // certificateListLabel
      // 
      this.certificateListLabel.AutoSize = true;
      this.certificateListLabel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.certificateListLabel.Location = new System.Drawing.Point(4, 0);
      this.certificateListLabel.Name = "certificateListLabel";
      this.certificateListLabel.Size = new System.Drawing.Size(97, 14);
      this.certificateListLabel.TabIndex = 10;
      this.certificateListLabel.Text = "Your certificates";
      // 
      // certificateList
      // 
      this.certificateList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.certificateList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.typeColumnHeader,
            this.idColumnHeader,
            this.nameColumnHeader});
      this.certificateList.ContextMenuStrip = this.certificateListMenu;
      this.certificateList.FullRowSelect = true;
      this.certificateList.Location = new System.Drawing.Point(3, 52);
      this.certificateList.MultiSelect = false;
      this.certificateList.Name = "certificateList";
      this.certificateList.Size = new System.Drawing.Size(694, 332);
      this.certificateList.TabIndex = 7;
      this.certificateList.UseCompatibleStateImageBehavior = false;
      this.certificateList.View = System.Windows.Forms.View.Details;
      this.certificateList.SelectedIndexChanged += new System.EventHandler(this.certificateList_SelectedIndexChanged);
      // 
      // typeColumnHeader
      // 
      this.typeColumnHeader.Text = "Type";
      this.typeColumnHeader.Width = 150;
      // 
      // idColumnHeader
      // 
      this.idColumnHeader.Text = "Id";
      this.idColumnHeader.Width = 220;
      // 
      // nameColumnHeader
      // 
      this.nameColumnHeader.Text = "Name";
      this.nameColumnHeader.Width = 300;
      // 
      // certificateListMenu
      // 
      this.certificateListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createMenu,
            this.loadMenu,
            this.saveMenu,
            this.deleteMenu,
            this.toolStripMenuItem1,
            this.encryptMenu,
            this.changePassphraseMenu,
            this.toolStripMenuItem2,
            this.backupMenu,
            this.restoreMenu,
            this.toolStripMenuItem3,
            this.verifyShareproofMenu});
      this.certificateListMenu.Name = "certificateListMenu";
      this.certificateListMenu.Size = new System.Drawing.Size(182, 220);
      this.certificateListMenu.Opening += new System.ComponentModel.CancelEventHandler(this.certificateListMenu_Opening);
      // 
      // createMenu
      // 
      this.createMenu.Name = "createMenu";
      this.createMenu.Size = new System.Drawing.Size(181, 22);
      this.createMenu.Text = "&Create...";
      this.createMenu.Click += new System.EventHandler(this.createMenu_Click);
      // 
      // loadMenu
      // 
      this.loadMenu.Name = "loadMenu";
      this.loadMenu.Size = new System.Drawing.Size(181, 22);
      this.loadMenu.Text = "&Load...";
      this.loadMenu.Click += new System.EventHandler(this.loadMenu_Click);
      // 
      // saveMenu
      // 
      this.saveMenu.Name = "saveMenu";
      this.saveMenu.Size = new System.Drawing.Size(181, 22);
      this.saveMenu.Text = "&Save...";
      this.saveMenu.Click += new System.EventHandler(this.saveMenu_Click);
      // 
      // deleteMenu
      // 
      this.deleteMenu.Name = "deleteMenu";
      this.deleteMenu.Size = new System.Drawing.Size(181, 22);
      this.deleteMenu.Text = "&Delete";
      this.deleteMenu.Click += new System.EventHandler(this.deleteMenu_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(178, 6);
      // 
      // encryptMenu
      // 
      this.encryptMenu.Name = "encryptMenu";
      this.encryptMenu.Size = new System.Drawing.Size(181, 22);
      this.encryptMenu.Text = "&Encrypt...";
      this.encryptMenu.Click += new System.EventHandler(this.encryptMenu_Click);
      // 
      // changePassphraseMenu
      // 
      this.changePassphraseMenu.Name = "changePassphraseMenu";
      this.changePassphraseMenu.Size = new System.Drawing.Size(181, 22);
      this.changePassphraseMenu.Text = "Change &Passphrase...";
      this.changePassphraseMenu.Click += new System.EventHandler(this.changePassphraseMenu_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(178, 6);
      // 
      // backupMenu
      // 
      this.backupMenu.Name = "backupMenu";
      this.backupMenu.Size = new System.Drawing.Size(181, 22);
      this.backupMenu.Text = "&Backup";
      this.backupMenu.Click += new System.EventHandler(this.backupMenu_Click);
      // 
      // restoreMenu
      // 
      this.restoreMenu.Name = "restoreMenu";
      this.restoreMenu.Size = new System.Drawing.Size(181, 22);
      this.restoreMenu.Text = "&Restore";
      this.restoreMenu.Click += new System.EventHandler(this.restoreMenu_Click);
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(178, 6);
      // 
      // verifyShareproofMenu
      // 
      this.verifyShareproofMenu.Name = "verifyShareproofMenu";
      this.verifyShareproofMenu.Size = new System.Drawing.Size(181, 22);
      this.verifyShareproofMenu.Text = "Verify share &proof";
      this.verifyShareproofMenu.Click += new System.EventHandler(this.verifyShareproofMenu_Click);
      // 
      // ChooseCertificateItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.certificateList);
      this.Controls.Add(this.infoLabel);
      this.Controls.Add(this.certificateListLabel);
      this.Margin = new System.Windows.Forms.Padding(3);
      this.Name = "ChooseCertificateItem";
      this.Size = new System.Drawing.Size(700, 387);
      this.Load += new System.EventHandler(this.StartWizardItem_Load);
      this.certificateListMenu.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private ListView certificateList;
    private ColumnHeader typeColumnHeader;
    private ColumnHeader idColumnHeader;
    private ColumnHeader nameColumnHeader;
    private Label certificateListLabel;
    private Label infoLabel;
    private ContextMenuStrip certificateListMenu;
    private ToolStripMenuItem saveMenu;
    private ToolStripMenuItem loadMenu;
    private ToolStripMenuItem createMenu;
    private ToolStripMenuItem deleteMenu;
    private ToolStripMenuItem encryptMenu;
    private ToolStripMenuItem changePassphraseMenu;
    private ToolStripSeparator toolStripMenuItem1;
    private ToolStripSeparator toolStripMenuItem2;
    private ToolStripMenuItem backupMenu;
    private ToolStripMenuItem restoreMenu;
    private ToolStripSeparator toolStripMenuItem3;
    private ToolStripMenuItem verifyShareproofMenu;
  }
}
