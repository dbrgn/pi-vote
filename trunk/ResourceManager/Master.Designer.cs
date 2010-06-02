namespace ResourceManager
{
  partial class Master
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
      this.pathTextBox = new System.Windows.Forms.TextBox();
      this.loadButton = new System.Windows.Forms.Button();
      this.loadList = new System.Windows.Forms.ListView();
      this.fileColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.englishColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.germanColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.frenchColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.loadContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.loadContextMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // pathTextBox
      // 
      this.pathTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.pathTextBox.Location = new System.Drawing.Point(12, 12);
      this.pathTextBox.Name = "pathTextBox";
      this.pathTextBox.Size = new System.Drawing.Size(459, 20);
      this.pathTextBox.TabIndex = 0;
      this.pathTextBox.TextChanged += new System.EventHandler(this.pathTextBox_TextChanged);
      // 
      // loadButton
      // 
      this.loadButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.loadButton.Location = new System.Drawing.Point(477, 12);
      this.loadButton.Name = "loadButton";
      this.loadButton.Size = new System.Drawing.Size(75, 20);
      this.loadButton.TabIndex = 1;
      this.loadButton.Text = "Load";
      this.loadButton.UseVisualStyleBackColor = true;
      this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
      // 
      // loadList
      // 
      this.loadList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.loadList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fileColumnHeader,
            this.englishColumnHeader,
            this.germanColumnHeader,
            this.frenchColumnHeader});
      this.loadList.ContextMenuStrip = this.loadContextMenu;
      this.loadList.FullRowSelect = true;
      this.loadList.Location = new System.Drawing.Point(12, 38);
      this.loadList.MultiSelect = false;
      this.loadList.Name = "loadList";
      this.loadList.Size = new System.Drawing.Size(540, 173);
      this.loadList.TabIndex = 2;
      this.loadList.UseCompatibleStateImageBehavior = false;
      this.loadList.View = System.Windows.Forms.View.Details;
      // 
      // fileColumnHeader
      // 
      this.fileColumnHeader.Text = "File";
      this.fileColumnHeader.Width = 200;
      // 
      // englishColumnHeader
      // 
      this.englishColumnHeader.Text = "English";
      this.englishColumnHeader.Width = 100;
      // 
      // germanColumnHeader
      // 
      this.germanColumnHeader.Text = "German";
      this.germanColumnHeader.Width = 100;
      // 
      // frenchColumnHeader
      // 
      this.frenchColumnHeader.Text = "French";
      this.frenchColumnHeader.Width = 100;
      // 
      // loadContextMenu
      // 
      this.loadContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportToolStripMenuItem,
            this.importToolStripMenuItem,
            this.saveToolStripMenuItem});
      this.loadContextMenu.Name = "loadContextMenu";
      this.loadContextMenu.Size = new System.Drawing.Size(107, 70);
      this.loadContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.loadContextMenu_Opening);
      // 
      // exportToolStripMenuItem
      // 
      this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
      this.exportToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
      this.exportToolStripMenuItem.Text = "&Export";
      this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
      // 
      // importToolStripMenuItem
      // 
      this.importToolStripMenuItem.Name = "importToolStripMenuItem";
      this.importToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
      this.importToolStripMenuItem.Text = "&Import";
      this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
      // 
      // saveToolStripMenuItem
      // 
      this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
      this.saveToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
      this.saveToolStripMenuItem.Text = "&Save";
      this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
      // 
      // Master
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(564, 224);
      this.Controls.Add(this.loadList);
      this.Controls.Add(this.loadButton);
      this.Controls.Add(this.pathTextBox);
      this.Name = "Master";
      this.Text = "Resource Manager";
      this.loadContextMenu.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox pathTextBox;
    private System.Windows.Forms.Button loadButton;
    private System.Windows.Forms.ListView loadList;
    private System.Windows.Forms.ColumnHeader fileColumnHeader;
    private System.Windows.Forms.ColumnHeader englishColumnHeader;
    private System.Windows.Forms.ColumnHeader germanColumnHeader;
    private System.Windows.Forms.ColumnHeader frenchColumnHeader;
    private System.Windows.Forms.ContextMenuStrip loadContextMenu;
    private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
  }
}

