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
      this.optionLabel.Location = new System.Drawing.Point(7, 124);
      this.optionLabel.Name = "optionLabel";
      this.optionLabel.Size = new System.Drawing.Size(70, 20);
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
      this.optionListView.Location = new System.Drawing.Point(162, 119);
      this.optionListView.MultiSelect = false;
      this.optionListView.Name = "optionListView";
      this.optionListView.Size = new System.Drawing.Size(906, 164);
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
      this.textTextBox.Location = new System.Drawing.Point(162, 7);
      this.textTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.textTextBox.Multiline = false;
      this.textTextBox.Name = "textTextBox";
      this.textTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
      this.textTextBox.Size = new System.Drawing.Size(906, 30);
      this.textTextBox.TabIndex = 38;
      // 
      // textLabel
      // 
      this.textLabel.AutoSize = true;
      this.textLabel.Location = new System.Drawing.Point(7, 11);
      this.textLabel.Name = "textLabel";
      this.textLabel.Size = new System.Drawing.Size(73, 20);
      this.textLabel.TabIndex = 37;
      this.textLabel.Text = "Question";
      // 
      // optionNumberLabel
      // 
      this.optionNumberLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.optionNumberLabel.AutoSize = true;
      this.optionNumberLabel.Location = new System.Drawing.Point(7, 291);
      this.optionNumberLabel.Name = "optionNumberLabel";
      this.optionNumberLabel.Size = new System.Drawing.Size(121, 20);
      this.optionNumberLabel.TabIndex = 40;
      this.optionNumberLabel.Text = "Answers / Voter";
      // 
      // optionNumberUpDown
      // 
      this.optionNumberUpDown.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.optionNumberUpDown.Location = new System.Drawing.Point(162, 289);
      this.optionNumberUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
      this.optionNumberUpDown.Name = "optionNumberUpDown";
      this.optionNumberUpDown.Size = new System.Drawing.Size(114, 26);
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
      this.descriptionTextBox.Location = new System.Drawing.Point(162, 43);
      this.descriptionTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.descriptionTextBox.Multiline = true;
      this.descriptionTextBox.Name = "descriptionTextBox";
      this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.descriptionTextBox.Size = new System.Drawing.Size(906, 70);
      this.descriptionTextBox.TabIndex = 44;
      // 
      // descriptionLabel
      // 
      this.descriptionLabel.AutoSize = true;
      this.descriptionLabel.Location = new System.Drawing.Point(7, 47);
      this.descriptionLabel.Name = "descriptionLabel";
      this.descriptionLabel.Size = new System.Drawing.Size(89, 20);
      this.descriptionLabel.TabIndex = 43;
      this.descriptionLabel.Text = "Description";
      // 
      // cancelButton
      // 
      this.cancelButton.Location = new System.Drawing.Point(951, 323);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(103, 29);
      this.cancelButton.TabIndex = 45;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Location = new System.Drawing.Point(842, 323);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(103, 29);
      this.okButton.TabIndex = 46;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // AddQuestionDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(1066, 364);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.descriptionTextBox);
      this.Controls.Add(this.descriptionLabel);
      this.Controls.Add(this.optionLabel);
      this.Controls.Add(this.optionListView);
      this.Controls.Add(this.textTextBox);
      this.Controls.Add(this.textLabel);
      this.Controls.Add(this.optionNumberLabel);
      this.Controls.Add(this.optionNumberUpDown);
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