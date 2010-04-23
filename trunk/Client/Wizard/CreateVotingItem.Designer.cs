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
      this.titleBox = new System.Windows.Forms.TextBox();
      this.descriptionBox = new System.Windows.Forms.TextBox();
      this.descriptionLabel = new System.Windows.Forms.Label();
      this.questionBox = new System.Windows.Forms.TextBox();
      this.questionLabel = new System.Windows.Forms.Label();
      this.optionLabel = new System.Windows.Forms.Label();
      this.optionNumberUpDown = new System.Windows.Forms.NumericUpDown();
      this.optionNumberLabel = new System.Windows.Forms.Label();
      this.createButton = new System.Windows.Forms.Button();
      this.votingFromPicker = new System.Windows.Forms.DateTimePicker();
      this.votingUntilPicker = new System.Windows.Forms.DateTimePicker();
      this.votingFromLabel = new System.Windows.Forms.Label();
      this.votingUntilLabel = new System.Windows.Forms.Label();
      this.optionListView = new System.Windows.Forms.ListView();
      this.textColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.descriptionColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.authority0List = new System.Windows.Forms.ComboBox();
      this.authority1List = new System.Windows.Forms.ComboBox();
      this.authority2List = new System.Windows.Forms.ComboBox();
      this.authority3List = new System.Windows.Forms.ComboBox();
      this.authority4List = new System.Windows.Forms.ComboBox();
      ((System.ComponentModel.ISupportInitialize)(this.optionNumberUpDown)).BeginInit();
      this.contextMenu.SuspendLayout();
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
      this.titleLabel.Location = new System.Drawing.Point(10, 171);
      this.titleLabel.Name = "titleLabel";
      this.titleLabel.Size = new System.Drawing.Size(37, 19);
      this.titleLabel.TabIndex = 16;
      this.titleLabel.Text = "Title";
      // 
      // titleBox
      // 
      this.titleBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.titleBox.Location = new System.Drawing.Point(131, 168);
      this.titleBox.Name = "titleBox";
      this.titleBox.Size = new System.Drawing.Size(598, 26);
      this.titleBox.TabIndex = 17;
      this.titleBox.TextChanged += new System.EventHandler(this.titleBox_TextChanged);
      // 
      // descriptionBox
      // 
      this.descriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.descriptionBox.Location = new System.Drawing.Point(131, 200);
      this.descriptionBox.Multiline = true;
      this.descriptionBox.Name = "descriptionBox";
      this.descriptionBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.descriptionBox.Size = new System.Drawing.Size(598, 42);
      this.descriptionBox.TabIndex = 19;
      this.descriptionBox.TextChanged += new System.EventHandler(this.descriptionBox_TextChanged);
      // 
      // descriptionLabel
      // 
      this.descriptionLabel.AutoSize = true;
      this.descriptionLabel.Location = new System.Drawing.Point(10, 203);
      this.descriptionLabel.Name = "descriptionLabel";
      this.descriptionLabel.Size = new System.Drawing.Size(92, 19);
      this.descriptionLabel.TabIndex = 18;
      this.descriptionLabel.Text = "Description";
      // 
      // questionBox
      // 
      this.questionBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.questionBox.Location = new System.Drawing.Point(131, 248);
      this.questionBox.Name = "questionBox";
      this.questionBox.Size = new System.Drawing.Size(598, 26);
      this.questionBox.TabIndex = 21;
      this.questionBox.TextChanged += new System.EventHandler(this.questionBox_TextChanged);
      // 
      // questionLabel
      // 
      this.questionLabel.AutoSize = true;
      this.questionLabel.Location = new System.Drawing.Point(10, 251);
      this.questionLabel.Name = "questionLabel";
      this.questionLabel.Size = new System.Drawing.Size(73, 19);
      this.questionLabel.TabIndex = 20;
      this.questionLabel.Text = "Question";
      // 
      // optionLabel
      // 
      this.optionLabel.AutoSize = true;
      this.optionLabel.Location = new System.Drawing.Point(12, 285);
      this.optionLabel.Name = "optionLabel";
      this.optionLabel.Size = new System.Drawing.Size(71, 19);
      this.optionLabel.TabIndex = 26;
      this.optionLabel.Text = "Answers";
      // 
      // optionNumberUpDown
      // 
      this.optionNumberUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.optionNumberUpDown.Enabled = false;
      this.optionNumberUpDown.Location = new System.Drawing.Point(129, 376);
      this.optionNumberUpDown.Maximum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.optionNumberUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.optionNumberUpDown.Name = "optionNumberUpDown";
      this.optionNumberUpDown.Size = new System.Drawing.Size(114, 26);
      this.optionNumberUpDown.TabIndex = 29;
      this.optionNumberUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      // 
      // optionNumberLabel
      // 
      this.optionNumberLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.optionNumberLabel.AutoSize = true;
      this.optionNumberLabel.Location = new System.Drawing.Point(3, 378);
      this.optionNumberLabel.Name = "optionNumberLabel";
      this.optionNumberLabel.Size = new System.Drawing.Size(124, 19);
      this.optionNumberLabel.TabIndex = 28;
      this.optionNumberLabel.Text = "Answers / Voter";
      // 
      // createButton
      // 
      this.createButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.createButton.Enabled = false;
      this.createButton.Location = new System.Drawing.Point(131, 472);
      this.createButton.Name = "createButton";
      this.createButton.Size = new System.Drawing.Size(141, 31);
      this.createButton.TabIndex = 32;
      this.createButton.Text = "Create";
      this.createButton.UseVisualStyleBackColor = true;
      this.createButton.Click += new System.EventHandler(this.createButton_Click);
      // 
      // votingFromPicker
      // 
      this.votingFromPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.votingFromPicker.Location = new System.Drawing.Point(129, 408);
      this.votingFromPicker.Name = "votingFromPicker";
      this.votingFromPicker.Size = new System.Drawing.Size(293, 26);
      this.votingFromPicker.TabIndex = 30;
      this.votingFromPicker.ValueChanged += new System.EventHandler(this.votingFromPicker_ValueChanged);
      // 
      // votingUntilPicker
      // 
      this.votingUntilPicker.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.votingUntilPicker.Location = new System.Drawing.Point(131, 440);
      this.votingUntilPicker.Name = "votingUntilPicker";
      this.votingUntilPicker.Size = new System.Drawing.Size(291, 26);
      this.votingUntilPicker.TabIndex = 31;
      this.votingUntilPicker.ValueChanged += new System.EventHandler(this.votingUntilPicker_ValueChanged);
      // 
      // votingFromLabel
      // 
      this.votingFromLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.votingFromLabel.AutoSize = true;
      this.votingFromLabel.Location = new System.Drawing.Point(3, 414);
      this.votingFromLabel.Name = "votingFromLabel";
      this.votingFromLabel.Size = new System.Drawing.Size(86, 19);
      this.votingFromLabel.TabIndex = 34;
      this.votingFromLabel.Text = "Open from";
      // 
      // votingUntilLabel
      // 
      this.votingUntilLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.votingUntilLabel.AutoSize = true;
      this.votingUntilLabel.Location = new System.Drawing.Point(3, 446);
      this.votingUntilLabel.Name = "votingUntilLabel";
      this.votingUntilLabel.Size = new System.Drawing.Size(38, 19);
      this.votingUntilLabel.TabIndex = 35;
      this.votingUntilLabel.Text = "until";
      // 
      // optionListView
      // 
      this.optionListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.optionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.textColumnHeader,
            this.descriptionColumnHeader});
      this.optionListView.ContextMenuStrip = this.contextMenu;
      this.optionListView.FullRowSelect = true;
      this.optionListView.Location = new System.Drawing.Point(129, 280);
      this.optionListView.MultiSelect = false;
      this.optionListView.Name = "optionListView";
      this.optionListView.Size = new System.Drawing.Size(600, 90);
      this.optionListView.TabIndex = 36;
      this.optionListView.UseCompatibleStateImageBehavior = false;
      this.optionListView.View = System.Windows.Forms.View.Details;
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
      // contextMenu
      // 
      this.contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem});
      this.contextMenu.Name = "contextMenu";
      this.contextMenu.Size = new System.Drawing.Size(141, 56);
      this.contextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenu_Opening);
      // 
      // addToolStripMenuItem
      // 
      this.addToolStripMenuItem.Name = "addToolStripMenuItem";
      this.addToolStripMenuItem.Size = new System.Drawing.Size(140, 26);
      this.addToolStripMenuItem.Text = "&Add";
      this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
      // 
      // removeToolStripMenuItem
      // 
      this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
      this.removeToolStripMenuItem.Size = new System.Drawing.Size(140, 26);
      this.removeToolStripMenuItem.Text = "&Remove";
      this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
      // 
      // authority0List
      // 
      this.authority0List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.authority0List.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.authority0List.FormattingEnabled = true;
      this.authority0List.Location = new System.Drawing.Point(131, 3);
      this.authority0List.Name = "authority0List";
      this.authority0List.Size = new System.Drawing.Size(598, 27);
      this.authority0List.TabIndex = 37;
      this.authority0List.SelectedIndexChanged += new System.EventHandler(this.authority0List_SelectedIndexChanged);
      // 
      // authority1List
      // 
      this.authority1List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.authority1List.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.authority1List.FormattingEnabled = true;
      this.authority1List.Location = new System.Drawing.Point(131, 69);
      this.authority1List.Name = "authority1List";
      this.authority1List.Size = new System.Drawing.Size(598, 27);
      this.authority1List.TabIndex = 38;
      this.authority1List.SelectedIndexChanged += new System.EventHandler(this.authority1List_SelectedIndexChanged);
      // 
      // authority2List
      // 
      this.authority2List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.authority2List.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.authority2List.FormattingEnabled = true;
      this.authority2List.Location = new System.Drawing.Point(131, 36);
      this.authority2List.Name = "authority2List";
      this.authority2List.Size = new System.Drawing.Size(598, 27);
      this.authority2List.TabIndex = 39;
      this.authority2List.SelectedIndexChanged += new System.EventHandler(this.authority2List_SelectedIndexChanged);
      // 
      // authority3List
      // 
      this.authority3List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.authority3List.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.authority3List.FormattingEnabled = true;
      this.authority3List.Location = new System.Drawing.Point(131, 102);
      this.authority3List.Name = "authority3List";
      this.authority3List.Size = new System.Drawing.Size(598, 27);
      this.authority3List.TabIndex = 40;
      this.authority3List.SelectedIndexChanged += new System.EventHandler(this.authority3List_SelectedIndexChanged);
      // 
      // authority4List
      // 
      this.authority4List.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.authority4List.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.authority4List.FormattingEnabled = true;
      this.authority4List.Location = new System.Drawing.Point(131, 135);
      this.authority4List.Name = "authority4List";
      this.authority4List.Size = new System.Drawing.Size(598, 27);
      this.authority4List.TabIndex = 41;
      this.authority4List.SelectedIndexChanged += new System.EventHandler(this.authority4List_SelectedIndexChanged);
      // 
      // CreateVotingItem
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.authority2List);
      this.Controls.Add(this.authority4List);
      this.Controls.Add(this.optionListView);
      this.Controls.Add(this.authority3List);
      this.Controls.Add(this.authority1List);
      this.Controls.Add(this.optionLabel);
      this.Controls.Add(this.authority0List);
      this.Controls.Add(this.questionBox);
      this.Controls.Add(this.votingFromLabel);
      this.Controls.Add(this.votingFromPicker);
      this.Controls.Add(this.votingUntilLabel);
      this.Controls.Add(this.votingUntilPicker);
      this.Controls.Add(this.questionLabel);
      this.Controls.Add(this.createButton);
      this.Controls.Add(this.descriptionBox);
      this.Controls.Add(this.optionNumberLabel);
      this.Controls.Add(this.descriptionLabel);
      this.Controls.Add(this.optionNumberUpDown);
      this.Controls.Add(this.titleLabel);
      this.Controls.Add(this.authoritiesLabel);
      this.Controls.Add(this.titleBox);
      this.Name = "CreateVotingItem";
      this.Size = new System.Drawing.Size(732, 514);
      this.Load += new System.EventHandler(this.CreateVotingItem_Load);
      ((System.ComponentModel.ISupportInitialize)(this.optionNumberUpDown)).EndInit();
      this.contextMenu.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private Label authoritiesLabel;
    private Label titleLabel;
    private TextBox titleBox;
    private TextBox descriptionBox;
    private Label descriptionLabel;
    private TextBox questionBox;
    private Label questionLabel;
    private Label optionLabel;
    private NumericUpDown optionNumberUpDown;
    private Label optionNumberLabel;
    private Button createButton;
    private DateTimePicker votingFromPicker;
    private DateTimePicker votingUntilPicker;
    private Label votingFromLabel;
    private Label votingUntilLabel;
    private ListView optionListView;
    private ColumnHeader textColumnHeader;
    private ColumnHeader descriptionColumnHeader;
    private ComboBox authority0List;
    private ComboBox authority1List;
    private ComboBox authority2List;
    private ComboBox authority3List;
    private ComboBox authority4List;
    private ContextMenuStrip contextMenu;
    private ToolStripMenuItem addToolStripMenuItem;
    private ToolStripMenuItem removeToolStripMenuItem;





  }
}
