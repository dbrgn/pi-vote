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
  partial class CreateVotingItem
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
      this.authoritiesLabel = new System.Windows.Forms.Label();
      this.titleLabel = new System.Windows.Forms.Label();
      this.titleBox = new Pirate.PiVote.MultiLanguageTextBox();
      this.descriptionBox = new Pirate.PiVote.MultiLanguageTextBox();
      this.descriptionLabel = new System.Windows.Forms.Label();
      this.createButton = new System.Windows.Forms.Button();
      this.votingFromPicker = new System.Windows.Forms.DateTimePicker();
      this.votingUntilPicker = new System.Windows.Forms.DateTimePicker();
      this.votingFromLabel = new System.Windows.Forms.Label();
      this.votingUntilLabel = new System.Windows.Forms.Label();
      this.authority0List = new System.Windows.Forms.ComboBox();
      this.authority1List = new System.Windows.Forms.ComboBox();
      this.authority2List = new System.Windows.Forms.ComboBox();
      this.authority3List = new System.Windows.Forms.ComboBox();
      this.authority4List = new System.Windows.Forms.ComboBox();
      this.questionLabel = new System.Windows.Forms.Label();
      this.questionListView = new System.Windows.Forms.ListView();
      this.textColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.descriptionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.questionContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.questionContextMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // authoritiesLabel
      // 
      this.authoritiesLabel.AutoSize = true;
      this.authoritiesLabel.Location = new System.Drawing.Point(3, 6);
      this.authoritiesLabel.Name = "authoritiesLabel";
      this.authoritiesLabel.Size = new System.Drawing.Size(86, 19);
      this.authoritiesLabel.TabIndex = 1;
      this.authoritiesLabel.Text = "Authorities";
      // 
      // titleLabel
      // 
      this.titleLabel.AutoSize = true;
      this.titleLabel.Location = new System.Drawing.Point(3, 202);
      this.titleLabel.Name = "titleLabel";
      this.titleLabel.Size = new System.Drawing.Size(37, 19);
      this.titleLabel.TabIndex = 16;
      this.titleLabel.Text = "Title";
      // 
      // titleBox
      // 
      this.titleBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.titleBox.Location = new System.Drawing.Point(158, 198);
      this.titleBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.titleBox.Multiline = false;
      this.titleBox.Name = "titleBox";
      this.titleBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.titleBox.Size = new System.Drawing.Size(780, 30);
      this.titleBox.TabIndex = 17;
      this.titleBox.TextChanged += new System.EventHandler(this.titleBox_TextChanged);
      // 
      // descriptionBox
      // 
      this.descriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.descriptionBox.Location = new System.Drawing.Point(158, 234);
      this.descriptionBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.descriptionBox.Multiline = true;
      this.descriptionBox.Name = "descriptionBox";
      this.descriptionBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.descriptionBox.Size = new System.Drawing.Size(780, 117);
      this.descriptionBox.TabIndex = 19;
      this.descriptionBox.TextChanged += new System.EventHandler(this.descriptionBox_TextChanged);
      // 
      // descriptionLabel
      // 
      this.descriptionLabel.AutoSize = true;
      this.descriptionLabel.Location = new System.Drawing.Point(3, 238);
      this.descriptionLabel.Name = "descriptionLabel";
      this.descriptionLabel.Size = new System.Drawing.Size(92, 19);
      this.descriptionLabel.TabIndex = 18;
      this.descriptionLabel.Text = "Description";
      // 
      // createButton
      // 
      this.createButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.createButton.Enabled = false;
      this.createButton.Location = new System.Drawing.Point(158, 626);
      this.createButton.Name = "createButton";
      this.createButton.Size = new System.Drawing.Size(165, 39);
      this.createButton.TabIndex = 32;
      this.createButton.Text = "Create";
      this.createButton.UseVisualStyleBackColor = true;
      this.createButton.Click += new System.EventHandler(this.createButton_Click);
      // 
      // votingFromPicker
      // 
      this.votingFromPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.votingFromPicker.Location = new System.Drawing.Point(158, 554);
      this.votingFromPicker.Name = "votingFromPicker";
      this.votingFromPicker.Size = new System.Drawing.Size(334, 26);
      this.votingFromPicker.TabIndex = 30;
      this.votingFromPicker.ValueChanged += new System.EventHandler(this.votingFromPicker_ValueChanged);
      // 
      // votingUntilPicker
      // 
      this.votingUntilPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.votingUntilPicker.Location = new System.Drawing.Point(158, 590);
      this.votingUntilPicker.Name = "votingUntilPicker";
      this.votingUntilPicker.Size = new System.Drawing.Size(334, 26);
      this.votingUntilPicker.TabIndex = 31;
      this.votingUntilPicker.ValueChanged += new System.EventHandler(this.votingUntilPicker_ValueChanged);
      // 
      // votingFromLabel
      // 
      this.votingFromLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.votingFromLabel.AutoSize = true;
      this.votingFromLabel.Location = new System.Drawing.Point(8, 561);
      this.votingFromLabel.Name = "votingFromLabel";
      this.votingFromLabel.Size = new System.Drawing.Size(86, 19);
      this.votingFromLabel.TabIndex = 34;
      this.votingFromLabel.Text = "Open from";
      // 
      // votingUntilLabel
      // 
      this.votingUntilLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.votingUntilLabel.AutoSize = true;
      this.votingUntilLabel.Location = new System.Drawing.Point(8, 597);
      this.votingUntilLabel.Name = "votingUntilLabel";
      this.votingUntilLabel.Size = new System.Drawing.Size(38, 19);
      this.votingUntilLabel.TabIndex = 35;
      this.votingUntilLabel.Text = "until";
      // 
      // authority0List
      // 
      this.authority0List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.authority0List.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.authority0List.FormattingEnabled = true;
      this.authority0List.Location = new System.Drawing.Point(158, 3);
      this.authority0List.Name = "authority0List";
      this.authority0List.Size = new System.Drawing.Size(778, 27);
      this.authority0List.TabIndex = 37;
      this.authority0List.SelectedIndexChanged += new System.EventHandler(this.authority0List_SelectedIndexChanged);
      // 
      // authority1List
      // 
      this.authority1List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.authority1List.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.authority1List.FormattingEnabled = true;
      this.authority1List.Location = new System.Drawing.Point(158, 81);
      this.authority1List.Name = "authority1List";
      this.authority1List.Size = new System.Drawing.Size(778, 27);
      this.authority1List.TabIndex = 38;
      this.authority1List.SelectedIndexChanged += new System.EventHandler(this.authority1List_SelectedIndexChanged);
      // 
      // authority2List
      // 
      this.authority2List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.authority2List.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.authority2List.FormattingEnabled = true;
      this.authority2List.Location = new System.Drawing.Point(158, 42);
      this.authority2List.Name = "authority2List";
      this.authority2List.Size = new System.Drawing.Size(778, 27);
      this.authority2List.TabIndex = 39;
      this.authority2List.SelectedIndexChanged += new System.EventHandler(this.authority2List_SelectedIndexChanged);
      // 
      // authority3List
      // 
      this.authority3List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.authority3List.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.authority3List.FormattingEnabled = true;
      this.authority3List.Location = new System.Drawing.Point(158, 120);
      this.authority3List.Name = "authority3List";
      this.authority3List.Size = new System.Drawing.Size(778, 27);
      this.authority3List.TabIndex = 40;
      this.authority3List.SelectedIndexChanged += new System.EventHandler(this.authority3List_SelectedIndexChanged);
      // 
      // authority4List
      // 
      this.authority4List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.authority4List.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.authority4List.FormattingEnabled = true;
      this.authority4List.Location = new System.Drawing.Point(158, 159);
      this.authority4List.Name = "authority4List";
      this.authority4List.Size = new System.Drawing.Size(778, 27);
      this.authority4List.TabIndex = 41;
      this.authority4List.SelectedIndexChanged += new System.EventHandler(this.authority4List_SelectedIndexChanged);
      // 
      // questionLabel
      // 
      this.questionLabel.AutoSize = true;
      this.questionLabel.Location = new System.Drawing.Point(3, 364);
      this.questionLabel.Name = "questionLabel";
      this.questionLabel.Size = new System.Drawing.Size(81, 19);
      this.questionLabel.TabIndex = 43;
      this.questionLabel.Text = "Questions";
      // 
      // questionListView
      // 
      this.questionListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.questionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.textColumnHeader,
            this.descriptionColumnHeader});
      this.questionListView.ContextMenuStrip = this.questionContextMenu;
      this.questionListView.FullRowSelect = true;
      this.questionListView.Location = new System.Drawing.Point(158, 359);
      this.questionListView.MultiSelect = false;
      this.questionListView.Name = "questionListView";
      this.questionListView.Size = new System.Drawing.Size(778, 189);
      this.questionListView.TabIndex = 44;
      this.questionListView.UseCompatibleStateImageBehavior = false;
      this.questionListView.View = System.Windows.Forms.View.Details;
      // 
      // textColumnHeader
      // 
      this.textColumnHeader.Text = "Text";
      this.textColumnHeader.Width = 150;
      // 
      // descriptionColumnHeader
      // 
      this.descriptionColumnHeader.Text = "Description";
      this.descriptionColumnHeader.Width = 390;
      // 
      // questionContextMenu
      // 
      this.questionContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem,
            this.editToolStripMenuItem});
      this.questionContextMenu.Name = "questionContextMenu";
      this.questionContextMenu.Size = new System.Drawing.Size(153, 104);
      this.questionContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.questionContextMenu_Opening);
      // 
      // addToolStripMenuItem
      // 
      this.addToolStripMenuItem.Name = "addToolStripMenuItem";
      this.addToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
      this.addToolStripMenuItem.Text = "&Add";
      this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
      // 
      // removeToolStripMenuItem
      // 
      this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
      this.removeToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
      this.removeToolStripMenuItem.Text = "&Remove";
      this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
      // 
      // editToolStripMenuItem
      // 
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      this.editToolStripMenuItem.Size = new System.Drawing.Size(152, 26);
      this.editToolStripMenuItem.Text = "&Edit";
      this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
      // 
      // CreateVotingItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.questionLabel);
      this.Controls.Add(this.questionListView);
      this.Controls.Add(this.authority0List);
      this.Controls.Add(this.authority4List);
      this.Controls.Add(this.authority2List);
      this.Controls.Add(this.authority3List);
      this.Controls.Add(this.authority1List);
      this.Controls.Add(this.authoritiesLabel);
      this.Controls.Add(this.descriptionBox);
      this.Controls.Add(this.votingUntilLabel);
      this.Controls.Add(this.votingFromLabel);
      this.Controls.Add(this.votingFromPicker);
      this.Controls.Add(this.createButton);
      this.Controls.Add(this.votingUntilPicker);
      this.Controls.Add(this.descriptionLabel);
      this.Controls.Add(this.titleBox);
      this.Controls.Add(this.titleLabel);
      this.Margin = new System.Windows.Forms.Padding(2);
      this.Name = "CreateVotingItem";
      this.Size = new System.Drawing.Size(940, 668);
      this.Load += new System.EventHandler(this.CreateVotingItem_Load);
      this.questionContextMenu.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Label authoritiesLabel;
    private Label titleLabel;
    private MultiLanguageTextBox titleBox;
    private MultiLanguageTextBox descriptionBox;
    private Label descriptionLabel;
    private Button createButton;
    private DateTimePicker votingFromPicker;
    private DateTimePicker votingUntilPicker;
    private Label votingFromLabel;
    private Label votingUntilLabel;
    private ComboBox authority0List;
    private ComboBox authority1List;
    private ComboBox authority2List;
    private ComboBox authority3List;
    private ComboBox authority4List;
    private Label questionLabel;
    private ListView questionListView;
    private ColumnHeader textColumnHeader;
    private ColumnHeader descriptionColumnHeader;
    private ContextMenuStrip questionContextMenu;
    private ToolStripMenuItem addToolStripMenuItem;
    private ToolStripMenuItem removeToolStripMenuItem;
    private ToolStripMenuItem editToolStripMenuItem;





  }
}
