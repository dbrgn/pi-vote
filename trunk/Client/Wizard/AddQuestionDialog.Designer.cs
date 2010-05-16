namespace Pirate.PiVote.Client
{
  partial class AddQuestionDialog
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

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.components = new System.ComponentModel.Container();
      this.optionLabel = new System.Windows.Forms.Label();
      this.optionListView = new System.Windows.Forms.ListView();
      this.textColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.descriptionColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.optionListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.textTextBox = new Pirate.PiVote.MultiLanguageTextBox();
      this.textLabel = new System.Windows.Forms.Label();
      this.optionNumberLabel = new System.Windows.Forms.Label();
      this.optionNumberUpDown = new System.Windows.Forms.NumericUpDown();
      this.descriptionTextBox = new Pirate.PiVote.MultiLanguageTextBox();
      this.descriptionLabel = new System.Windows.Forms.Label();
      this.cancelButton = new System.Windows.Forms.Button();
      this.okButton = new System.Windows.Forms.Button();
      this.optionListContextMenu.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)(this.optionNumberUpDown)).BeginInit();
      this.SuspendLayout();
      // 
      // optionLabel
      // 
      this.optionLabel.AutoSize = true;
      this.optionLabel.Location = new System.Drawing.Point(5, 81);
      this.optionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.optionLabel.Name = "optionLabel";
      this.optionLabel.Size = new System.Drawing.Size(47, 13);
      this.optionLabel.TabIndex = 39;
      this.optionLabel.Text = "Answers";
      // 
      // optionListView
      // 
      this.optionListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.optionListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.textColumnHeader,
            this.descriptionColumnHeader});
      this.optionListView.ContextMenuStrip = this.optionListContextMenu;
      this.optionListView.FullRowSelect = true;
      this.optionListView.Location = new System.Drawing.Point(108, 77);
      this.optionListView.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.optionListView.MultiSelect = false;
      this.optionListView.Name = "optionListView";
      this.optionListView.Size = new System.Drawing.Size(591, 144);
      this.optionListView.TabIndex = 42;
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
      // optionListContextMenu
      // 
      this.optionListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem});
      this.optionListContextMenu.Name = "contextMenu";
      this.optionListContextMenu.Size = new System.Drawing.Size(141, 56);
      this.optionListContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.optionListContextMenu_Opening);
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
      // textTextBox
      // 
      this.textTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textTextBox.Location = new System.Drawing.Point(108, 5);
      this.textTextBox.Multiline = false;
      this.textTextBox.Name = "textTextBox";
      this.textTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.textTextBox.Size = new System.Drawing.Size(591, 19);
      this.textTextBox.TabIndex = 38;
      // 
      // textLabel
      // 
      this.textLabel.AutoSize = true;
      this.textLabel.Location = new System.Drawing.Point(5, 7);
      this.textLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.textLabel.Name = "textLabel";
      this.textLabel.Size = new System.Drawing.Size(49, 13);
      this.textLabel.TabIndex = 37;
      this.textLabel.Text = "Question";
      // 
      // optionNumberLabel
      // 
      this.optionNumberLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.optionNumberLabel.AutoSize = true;
      this.optionNumberLabel.Location = new System.Drawing.Point(5, 225);
      this.optionNumberLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.optionNumberLabel.Name = "optionNumberLabel";
      this.optionNumberLabel.Size = new System.Drawing.Size(83, 13);
      this.optionNumberLabel.TabIndex = 40;
      this.optionNumberLabel.Text = "Answers / Voter";
      // 
      // optionNumberUpDown
      // 
      this.optionNumberUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.optionNumberUpDown.Location = new System.Drawing.Point(108, 224);
      this.optionNumberUpDown.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.optionNumberUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.optionNumberUpDown.Name = "optionNumberUpDown";
      this.optionNumberUpDown.Size = new System.Drawing.Size(77, 20);
      this.optionNumberUpDown.TabIndex = 41;
      this.optionNumberUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.optionNumberUpDown.ValueChanged += new System.EventHandler(this.optionNumberUpDown_ValueChanged);
      // 
      // descriptionTextBox
      // 
      this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.descriptionTextBox.Location = new System.Drawing.Point(108, 28);
      this.descriptionTextBox.Multiline = true;
      this.descriptionTextBox.Name = "descriptionTextBox";
      this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.descriptionTextBox.Size = new System.Drawing.Size(591, 45);
      this.descriptionTextBox.TabIndex = 44;
      // 
      // descriptionLabel
      // 
      this.descriptionLabel.AutoSize = true;
      this.descriptionLabel.Location = new System.Drawing.Point(5, 31);
      this.descriptionLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.descriptionLabel.Name = "descriptionLabel";
      this.descriptionLabel.Size = new System.Drawing.Size(60, 13);
      this.descriptionLabel.TabIndex = 43;
      this.descriptionLabel.Text = "Description";
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(602, 239);
      this.cancelButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(85, 23);
      this.cancelButton.TabIndex = 45;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.Location = new System.Drawing.Point(513, 239);
      this.okButton.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(85, 23);
      this.okButton.TabIndex = 46;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // AddQuestionDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(698, 273);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.descriptionTextBox);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.descriptionLabel);
      this.Controls.Add(this.optionLabel);
      this.Controls.Add(this.optionListView);
      this.Controls.Add(this.textTextBox);
      this.Controls.Add(this.textLabel);
      this.Controls.Add(this.optionNumberLabel);
      this.Controls.Add(this.optionNumberUpDown);
      this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
      this.Name = "AddQuestionDialog";
      this.Text = "AddQuestionDialog";
      this.optionListContextMenu.ResumeLayout(false);
      ((System.ComponentModel.ISupportInitialize)(this.optionNumberUpDown)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label optionLabel;
    private System.Windows.Forms.ListView optionListView;
    private System.Windows.Forms.ColumnHeader textColumnHeader;
    private System.Windows.Forms.ColumnHeader descriptionColumnHeader;
    private MultiLanguageTextBox textTextBox;
    private System.Windows.Forms.Label textLabel;
    private System.Windows.Forms.Label optionNumberLabel;
    private System.Windows.Forms.NumericUpDown optionNumberUpDown;
    private MultiLanguageTextBox descriptionTextBox;
    private System.Windows.Forms.Label descriptionLabel;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.ContextMenuStrip optionListContextMenu;
    private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
  }
}