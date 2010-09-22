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
      this.verifyShareProofButton = new System.Windows.Forms.Button();
      this.createButton = new System.Windows.Forms.Button();
      this.certificateList = new System.Windows.Forms.ListView();
      this.typeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.idColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.certificateListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.saveMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.deleteMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.loadMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.createMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.loadButton = new System.Windows.Forms.Button();
      this.backupButton = new System.Windows.Forms.Button();
      this.restoreButton = new System.Windows.Forms.Button();
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
      // verifyShareProofButton
      // 
      this.verifyShareProofButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.verifyShareProofButton.Location = new System.Drawing.Point(239, 361);
      this.verifyShareProofButton.Name = "verifyShareProofButton";
      this.verifyShareProofButton.Size = new System.Drawing.Size(156, 23);
      this.verifyShareProofButton.TabIndex = 9;
      this.verifyShareProofButton.Text = "Verfiy share &proof...";
      this.verifyShareProofButton.UseVisualStyleBackColor = true;
      this.verifyShareProofButton.Click += new System.EventHandler(this.verifyShareProofButton_Click);
      // 
      // createButton
      // 
      this.createButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.createButton.Location = new System.Drawing.Point(121, 361);
      this.createButton.Name = "createButton";
      this.createButton.Size = new System.Drawing.Size(112, 23);
      this.createButton.TabIndex = 8;
      this.createButton.Text = "&Create...";
      this.createButton.UseVisualStyleBackColor = true;
      this.createButton.Click += new System.EventHandler(this.createButton_Click);
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
      this.certificateList.Size = new System.Drawing.Size(694, 303);
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
            this.saveMenu,
            this.deleteMenu,
            this.loadMenu,
            this.createMenu});
      this.certificateListMenu.Name = "certificateListMenu";
      this.certificateListMenu.Size = new System.Drawing.Size(120, 92);
      this.certificateListMenu.Opening += new System.ComponentModel.CancelEventHandler(this.certificateListMenu_Opening);
      // 
      // saveMenu
      // 
      this.saveMenu.Name = "saveMenu";
      this.saveMenu.Size = new System.Drawing.Size(119, 22);
      this.saveMenu.Text = "&Save...";
      this.saveMenu.Click += new System.EventHandler(this.saveMenu_Click);
      // 
      // deleteMenu
      // 
      this.deleteMenu.Name = "deleteMenu";
      this.deleteMenu.Size = new System.Drawing.Size(119, 22);
      this.deleteMenu.Text = "&Delete";
      this.deleteMenu.Click += new System.EventHandler(this.deleteMenu_Click);
      // 
      // loadMenu
      // 
      this.loadMenu.Name = "loadMenu";
      this.loadMenu.Size = new System.Drawing.Size(119, 22);
      this.loadMenu.Text = "&Load...";
      this.loadMenu.Click += new System.EventHandler(this.loadMenu_Click);
      // 
      // createMenu
      // 
      this.createMenu.Name = "createMenu";
      this.createMenu.Size = new System.Drawing.Size(119, 22);
      this.createMenu.Text = "&Create...";
      this.createMenu.Click += new System.EventHandler(this.createMenu_Click);
      // 
      // loadButton
      // 
      this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.loadButton.Location = new System.Drawing.Point(3, 361);
      this.loadButton.Name = "loadButton";
      this.loadButton.Size = new System.Drawing.Size(112, 23);
      this.loadButton.TabIndex = 0;
      this.loadButton.Text = "&Load...";
      this.loadButton.UseVisualStyleBackColor = true;
      this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
      // 
      // backupButton
      // 
      this.backupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.backupButton.Location = new System.Drawing.Point(401, 361);
      this.backupButton.Name = "backupButton";
      this.backupButton.Size = new System.Drawing.Size(112, 23);
      this.backupButton.TabIndex = 14;
      this.backupButton.Text = "&Backup...";
      this.backupButton.UseVisualStyleBackColor = true;
      this.backupButton.Click += new System.EventHandler(this.backupButton_Click);
      // 
      // restoreButton
      // 
      this.restoreButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.restoreButton.Location = new System.Drawing.Point(519, 361);
      this.restoreButton.Name = "restoreButton";
      this.restoreButton.Size = new System.Drawing.Size(112, 23);
      this.restoreButton.TabIndex = 15;
      this.restoreButton.Text = "&Restore...";
      this.restoreButton.UseVisualStyleBackColor = true;
      this.restoreButton.Click += new System.EventHandler(this.restoreButton_Click);
      // 
      // ChooseCertificateItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.certificateList);
      this.Controls.Add(this.restoreButton);
      this.Controls.Add(this.infoLabel);
      this.Controls.Add(this.backupButton);
      this.Controls.Add(this.certificateListLabel);
      this.Controls.Add(this.verifyShareProofButton);
      this.Controls.Add(this.createButton);
      this.Controls.Add(this.loadButton);
      this.Margin = new System.Windows.Forms.Padding(3);
      this.Name = "ChooseCertificateItem";
      this.Size = new System.Drawing.Size(700, 387);
      this.Load += new System.EventHandler(this.StartWizardItem_Load);
      this.certificateListMenu.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Button loadButton;
    private ListView certificateList;
    private ColumnHeader typeColumnHeader;
    private ColumnHeader idColumnHeader;
    private ColumnHeader nameColumnHeader;
    private Button createButton;
    private Button verifyShareProofButton;
    private Label certificateListLabel;
    private Label infoLabel;
    private ContextMenuStrip certificateListMenu;
    private ToolStripMenuItem saveMenu;
    private ToolStripMenuItem loadMenu;
    private ToolStripMenuItem createMenu;
    private Button backupButton;
    private Button restoreButton;
    private ToolStripMenuItem deleteMenu;
  }
}
