/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using Pirate.PiVote.Gui;

namespace Pirate.PiVote.Circle.CreateVoting
{
  partial class EnterDataControl
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
      this.urlTextBox = new MultiLanguageTextBox();
      this.questionLabel = new System.Windows.Forms.Label();
      this.questionListView = new System.Windows.Forms.ListView();
      this.textColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.descriptionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.questionContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.descriptionBox = new MultiLanguageTextBox();
      this.descriptionLabel = new System.Windows.Forms.Label();
      this.titleBox = new MultiLanguageTextBox();
      this.titleLabel = new System.Windows.Forms.Label();
      this.horizontalSplit = new System.Windows.Forms.SplitContainer();
      this.urlLabel = new System.Windows.Forms.Label();
      this.cancelButton = new System.Windows.Forms.Button();
      this.nextButton = new System.Windows.Forms.Button();
      this.clearButton = new System.Windows.Forms.Button();
      this.questionContextMenu.SuspendLayout();
      this.horizontalSplit.Panel1.SuspendLayout();
      this.horizontalSplit.Panel2.SuspendLayout();
      this.horizontalSplit.SuspendLayout();
      this.SuspendLayout();
      // 
      // urlTextBox
      // 
      this.urlTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.urlTextBox.Location = new System.Drawing.Point(101, 206);
      this.urlTextBox.Multiline = false;
      this.urlTextBox.Name = "urlTextBox";
      this.urlTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.urlTextBox.Size = new System.Drawing.Size(567, 24);
      this.urlTextBox.TabIndex = 46;
      this.urlTextBox.TextChanged += new System.EventHandler(this.UrlBox_TextChanged);
      // 
      // questionLabel
      // 
      this.questionLabel.AutoSize = true;
      this.questionLabel.Location = new System.Drawing.Point(1, 5);
      this.questionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.questionLabel.Name = "questionLabel";
      this.questionLabel.Size = new System.Drawing.Size(56, 14);
      this.questionLabel.TabIndex = 50;
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
      this.questionListView.Location = new System.Drawing.Point(101, 2);
      this.questionListView.Margin = new System.Windows.Forms.Padding(2);
      this.questionListView.MultiSelect = false;
      this.questionListView.Name = "questionListView";
      this.questionListView.Size = new System.Drawing.Size(566, 225);
      this.questionListView.TabIndex = 47;
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
      this.questionContextMenu.Size = new System.Drawing.Size(114, 70);
      this.questionContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.questionContextMenu_Opening);
      // 
      // addToolStripMenuItem
      // 
      this.addToolStripMenuItem.Name = "addToolStripMenuItem";
      this.addToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.addToolStripMenuItem.Text = "&Add";
      this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
      // 
      // removeToolStripMenuItem
      // 
      this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
      this.removeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.removeToolStripMenuItem.Text = "&Remove";
      this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
      // 
      // editToolStripMenuItem
      // 
      this.editToolStripMenuItem.Name = "editToolStripMenuItem";
      this.editToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
      this.editToolStripMenuItem.Text = "&Edit";
      this.editToolStripMenuItem.Click += new System.EventHandler(this.editToolStripMenuItem_Click);
      // 
      // descriptionBox
      // 
      this.descriptionBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.descriptionBox.Location = new System.Drawing.Point(102, 31);
      this.descriptionBox.Multiline = true;
      this.descriptionBox.Name = "descriptionBox";
      this.descriptionBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.descriptionBox.Size = new System.Drawing.Size(566, 169);
      this.descriptionBox.TabIndex = 45;
      this.descriptionBox.TextChanged += new System.EventHandler(this.DescriptionBox_TextChanged);
      // 
      // descriptionLabel
      // 
      this.descriptionLabel.AutoSize = true;
      this.descriptionLabel.Location = new System.Drawing.Point(2, 34);
      this.descriptionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.descriptionLabel.Name = "descriptionLabel";
      this.descriptionLabel.Size = new System.Drawing.Size(61, 14);
      this.descriptionLabel.TabIndex = 49;
      this.descriptionLabel.Text = "Description";
      // 
      // titleBox
      // 
      this.titleBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.titleBox.Location = new System.Drawing.Point(101, 3);
      this.titleBox.Multiline = false;
      this.titleBox.Name = "titleBox";
      this.titleBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.titleBox.Size = new System.Drawing.Size(566, 22);
      this.titleBox.TabIndex = 44;
      this.titleBox.TextChanged += new System.EventHandler(this.TitleBox_TextChanged);
      // 
      // titleLabel
      // 
      this.titleLabel.AutoSize = true;
      this.titleLabel.Location = new System.Drawing.Point(2, 6);
      this.titleLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.titleLabel.Name = "titleLabel";
      this.titleLabel.Size = new System.Drawing.Size(26, 14);
      this.titleLabel.TabIndex = 48;
      this.titleLabel.Text = "Title";
      // 
      // horizontalSplit
      // 
      this.horizontalSplit.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.horizontalSplit.Location = new System.Drawing.Point(3, 0);
      this.horizontalSplit.Name = "horizontalSplit";
      this.horizontalSplit.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // horizontalSplit.Panel1
      // 
      this.horizontalSplit.Panel1.Controls.Add(this.urlLabel);
      this.horizontalSplit.Panel1.Controls.Add(this.urlTextBox);
      this.horizontalSplit.Panel1.Controls.Add(this.titleLabel);
      this.horizontalSplit.Panel1.Controls.Add(this.descriptionLabel);
      this.horizontalSplit.Panel1.Controls.Add(this.descriptionBox);
      this.horizontalSplit.Panel1.Controls.Add(this.titleBox);
      // 
      // horizontalSplit.Panel2
      // 
      this.horizontalSplit.Panel2.Controls.Add(this.questionListView);
      this.horizontalSplit.Panel2.Controls.Add(this.questionLabel);
      this.horizontalSplit.Size = new System.Drawing.Size(670, 466);
      this.horizontalSplit.SplitterDistance = 233;
      this.horizontalSplit.TabIndex = 51;
      // 
      // urlLabel
      // 
      this.urlLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.urlLabel.AutoSize = true;
      this.urlLabel.Location = new System.Drawing.Point(2, 209);
      this.urlLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.urlLabel.Name = "urlLabel";
      this.urlLabel.Size = new System.Drawing.Size(23, 14);
      this.urlLabel.TabIndex = 50;
      this.urlLabel.Text = "Url:";
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(411, 472);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(128, 28);
      this.cancelButton.TabIndex = 53;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // nextButton
      // 
      this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.nextButton.Font = new System.Drawing.Font("Arial", 8.25F);
      this.nextButton.Location = new System.Drawing.Point(545, 472);
      this.nextButton.Name = "nextButton";
      this.nextButton.Size = new System.Drawing.Size(128, 28);
      this.nextButton.TabIndex = 52;
      this.nextButton.Text = "&Next";
      this.nextButton.UseVisualStyleBackColor = true;
      this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
      // 
      // clearButton
      // 
      this.clearButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.clearButton.Location = new System.Drawing.Point(277, 472);
      this.clearButton.Name = "clearButton";
      this.clearButton.Size = new System.Drawing.Size(128, 28);
      this.clearButton.TabIndex = 54;
      this.clearButton.Text = "&Clear";
      this.clearButton.UseVisualStyleBackColor = true;
      this.clearButton.Click += new System.EventHandler(this.clearButton_Click);
      // 
      // EnterDataControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.clearButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.nextButton);
      this.Controls.Add(this.horizontalSplit);
      this.Name = "EnterDataControl";
      this.Size = new System.Drawing.Size(676, 503);
      this.questionContextMenu.ResumeLayout(false);
      this.horizontalSplit.Panel1.ResumeLayout(false);
      this.horizontalSplit.Panel1.PerformLayout();
      this.horizontalSplit.Panel2.ResumeLayout(false);
      this.horizontalSplit.Panel2.PerformLayout();
      this.horizontalSplit.ResumeLayout(false);
      this.ResumeLayout(false);

    }
    #endregion

    private MultiLanguageTextBox urlTextBox;
    private System.Windows.Forms.Label questionLabel;
    private System.Windows.Forms.ListView questionListView;
    private System.Windows.Forms.ColumnHeader textColumnHeader;
    private System.Windows.Forms.ColumnHeader descriptionColumnHeader;
    private MultiLanguageTextBox descriptionBox;
    private System.Windows.Forms.Label descriptionLabel;
    private MultiLanguageTextBox titleBox;
    private System.Windows.Forms.Label titleLabel;
    private System.Windows.Forms.SplitContainer horizontalSplit;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button nextButton;
    private System.Windows.Forms.Button clearButton;
    private System.Windows.Forms.ContextMenuStrip questionContextMenu;
    private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
    private System.Windows.Forms.Label urlLabel;
  }
}
