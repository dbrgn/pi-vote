namespace Pirate.PiVote.Circle.Certificates
{
  partial class CertificateManagerDialog
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
      this.certificateList = new System.Windows.Forms.ListView();
      this.idColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.typeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.groupNameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.statusColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.validFromColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.validUntilColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.certificateContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.createNewContextMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.removeContextMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.importContextMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.exportContextMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.backupContextMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.restoreContextMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.setPasswordContextMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.refreshContextMenu = new System.Windows.Forms.ToolStripMenuItem();
      this.closeButton = new System.Windows.Forms.Button();
      this.restoreButton = new System.Windows.Forms.Button();
      this.backupButton = new System.Windows.Forms.Button();
      this.exportButton = new System.Windows.Forms.Button();
      this.importButton = new System.Windows.Forms.Button();
      this.removeButton = new System.Windows.Forms.Button();
      this.createNewButton = new System.Windows.Forms.Button();
      this.setPasswordButton = new System.Windows.Forms.Button();
      this.refreshButton = new System.Windows.Forms.Button();
      this.certificateContextMenu.SuspendLayout();
      this.SuspendLayout();
      // 
      // certificateList
      // 
      this.certificateList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.certificateList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idColumnHeader,
            this.typeColumnHeader,
            this.groupNameColumnHeader,
            this.statusColumnHeader,
            this.validFromColumnHeader,
            this.validUntilColumnHeader});
      this.certificateList.ContextMenuStrip = this.certificateContextMenu;
      this.certificateList.FullRowSelect = true;
      this.certificateList.HideSelection = false;
      this.certificateList.Location = new System.Drawing.Point(12, 12);
      this.certificateList.MultiSelect = false;
      this.certificateList.Name = "certificateList";
      this.certificateList.Size = new System.Drawing.Size(768, 485);
      this.certificateList.TabIndex = 0;
      this.certificateList.UseCompatibleStateImageBehavior = false;
      this.certificateList.View = System.Windows.Forms.View.Details;
      this.certificateList.SelectedIndexChanged += new System.EventHandler(this.certificateList_SelectedIndexChanged);
      // 
      // idColumnHeader
      // 
      this.idColumnHeader.Text = "Id";
      this.idColumnHeader.Width = 220;
      // 
      // typeColumnHeader
      // 
      this.typeColumnHeader.Text = "Type";
      this.typeColumnHeader.Width = 120;
      // 
      // groupNameColumnHeader
      // 
      this.groupNameColumnHeader.Text = "Group/Name";
      this.groupNameColumnHeader.Width = 140;
      // 
      // statusColumnHeader
      // 
      this.statusColumnHeader.Text = "Status";
      this.statusColumnHeader.Width = 100;
      // 
      // validFromColumnHeader
      // 
      this.validFromColumnHeader.Text = "Valid from";
      this.validFromColumnHeader.Width = 80;
      // 
      // validUntilColumnHeader
      // 
      this.validUntilColumnHeader.Text = "Valid until";
      this.validUntilColumnHeader.Width = 80;
      // 
      // certificateContextMenu
      // 
      this.certificateContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.createNewContextMenu,
            this.removeContextMenu,
            this.importContextMenu,
            this.exportContextMenu,
            this.backupContextMenu,
            this.restoreContextMenu,
            this.setPasswordContextMenu,
            this.refreshContextMenu});
      this.certificateContextMenu.Name = "certificateContextMenu";
      this.certificateContextMenu.Size = new System.Drawing.Size(152, 180);
      // 
      // createNewContextMenu
      // 
      this.createNewContextMenu.Name = "createNewContextMenu";
      this.createNewContextMenu.Size = new System.Drawing.Size(152, 22);
      this.createNewContextMenu.Text = "&Create New...";
      this.createNewContextMenu.Click += new System.EventHandler(this.createNewContextMenu_Click);
      // 
      // removeContextMenu
      // 
      this.removeContextMenu.Name = "removeContextMenu";
      this.removeContextMenu.Size = new System.Drawing.Size(152, 22);
      this.removeContextMenu.Text = "&Remove";
      this.removeContextMenu.Click += new System.EventHandler(this.removeContextMenu_Click);
      // 
      // importContextMenu
      // 
      this.importContextMenu.Name = "importContextMenu";
      this.importContextMenu.Size = new System.Drawing.Size(152, 22);
      this.importContextMenu.Text = "&Import...";
      this.importContextMenu.Click += new System.EventHandler(this.importContextMenu_Click);
      // 
      // exportContextMenu
      // 
      this.exportContextMenu.Name = "exportContextMenu";
      this.exportContextMenu.Size = new System.Drawing.Size(152, 22);
      this.exportContextMenu.Text = "&Export...";
      this.exportContextMenu.Click += new System.EventHandler(this.exportContextMenu_Click);
      // 
      // backupContextMenu
      // 
      this.backupContextMenu.Name = "backupContextMenu";
      this.backupContextMenu.Size = new System.Drawing.Size(152, 22);
      this.backupContextMenu.Text = "&Backup...";
      this.backupContextMenu.Click += new System.EventHandler(this.backupContextMenu_Click);
      // 
      // restoreContextMenu
      // 
      this.restoreContextMenu.Name = "restoreContextMenu";
      this.restoreContextMenu.Size = new System.Drawing.Size(152, 22);
      this.restoreContextMenu.Text = "&Restore...";
      this.restoreContextMenu.Click += new System.EventHandler(this.restoreContextMenu_Click);
      // 
      // setPasswordContextMenu
      // 
      this.setPasswordContextMenu.Name = "setPasswordContextMenu";
      this.setPasswordContextMenu.Size = new System.Drawing.Size(152, 22);
      this.setPasswordContextMenu.Text = "Set &Password...";
      this.setPasswordContextMenu.Click += new System.EventHandler(this.setPasswordContextMenu_Click);
      // 
      // refreshContextMenu
      // 
      this.refreshContextMenu.Name = "refreshContextMenu";
      this.refreshContextMenu.Size = new System.Drawing.Size(152, 22);
      this.refreshContextMenu.Text = "&Refresh";
      this.refreshContextMenu.Click += new System.EventHandler(this.refreshContextMenu_Click);
      // 
      // closeButton
      // 
      this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.closeButton.Location = new System.Drawing.Point(672, 535);
      this.closeButton.Name = "closeButton";
      this.closeButton.Size = new System.Drawing.Size(106, 25);
      this.closeButton.TabIndex = 1;
      this.closeButton.Text = "&Close";
      this.closeButton.UseVisualStyleBackColor = true;
      this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
      // 
      // restoreButton
      // 
      this.restoreButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.restoreButton.Location = new System.Drawing.Point(448, 535);
      this.restoreButton.Name = "restoreButton";
      this.restoreButton.Size = new System.Drawing.Size(106, 25);
      this.restoreButton.TabIndex = 2;
      this.restoreButton.Text = "&Restore...";
      this.restoreButton.UseVisualStyleBackColor = true;
      this.restoreButton.Click += new System.EventHandler(this.restoreButton_Click);
      // 
      // backupButton
      // 
      this.backupButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.backupButton.Location = new System.Drawing.Point(336, 535);
      this.backupButton.Name = "backupButton";
      this.backupButton.Size = new System.Drawing.Size(106, 25);
      this.backupButton.TabIndex = 3;
      this.backupButton.Text = "&Backup...";
      this.backupButton.UseVisualStyleBackColor = true;
      this.backupButton.Click += new System.EventHandler(this.backupButton_Click);
      // 
      // exportButton
      // 
      this.exportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.exportButton.Location = new System.Drawing.Point(672, 504);
      this.exportButton.Name = "exportButton";
      this.exportButton.Size = new System.Drawing.Size(106, 25);
      this.exportButton.TabIndex = 4;
      this.exportButton.Text = "&Export...";
      this.exportButton.UseVisualStyleBackColor = true;
      this.exportButton.Click += new System.EventHandler(this.exportButton_Click);
      // 
      // importButton
      // 
      this.importButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.importButton.Location = new System.Drawing.Point(560, 504);
      this.importButton.Name = "importButton";
      this.importButton.Size = new System.Drawing.Size(106, 25);
      this.importButton.TabIndex = 5;
      this.importButton.Text = "&Import...";
      this.importButton.UseVisualStyleBackColor = true;
      this.importButton.Click += new System.EventHandler(this.importButton_Click);
      // 
      // removeButton
      // 
      this.removeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.removeButton.Location = new System.Drawing.Point(448, 504);
      this.removeButton.Name = "removeButton";
      this.removeButton.Size = new System.Drawing.Size(106, 25);
      this.removeButton.TabIndex = 6;
      this.removeButton.Text = "&Remove";
      this.removeButton.UseVisualStyleBackColor = true;
      this.removeButton.Click += new System.EventHandler(this.removeButton_Click);
      // 
      // createNewButton
      // 
      this.createNewButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.createNewButton.Location = new System.Drawing.Point(336, 504);
      this.createNewButton.Name = "createNewButton";
      this.createNewButton.Size = new System.Drawing.Size(106, 25);
      this.createNewButton.TabIndex = 7;
      this.createNewButton.Text = "&Create New...";
      this.createNewButton.UseVisualStyleBackColor = true;
      this.createNewButton.Click += new System.EventHandler(this.createNewButton_Click);
      // 
      // setPasswordButton
      // 
      this.setPasswordButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.setPasswordButton.Location = new System.Drawing.Point(560, 535);
      this.setPasswordButton.Name = "setPasswordButton";
      this.setPasswordButton.Size = new System.Drawing.Size(106, 25);
      this.setPasswordButton.TabIndex = 8;
      this.setPasswordButton.Text = "Set &Password...";
      this.setPasswordButton.UseVisualStyleBackColor = true;
      this.setPasswordButton.Click += new System.EventHandler(this.setPasswordButton_Click);
      // 
      // refreshButton
      // 
      this.refreshButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.refreshButton.Location = new System.Drawing.Point(224, 504);
      this.refreshButton.Name = "refreshButton";
      this.refreshButton.Size = new System.Drawing.Size(106, 25);
      this.refreshButton.TabIndex = 9;
      this.refreshButton.Text = "&Refresh";
      this.refreshButton.UseVisualStyleBackColor = true;
      this.refreshButton.Click += new System.EventHandler(this.refreshButton_Click);
      // 
      // CertificateManagerDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(792, 573);
      this.Controls.Add(this.refreshButton);
      this.Controls.Add(this.setPasswordButton);
      this.Controls.Add(this.createNewButton);
      this.Controls.Add(this.removeButton);
      this.Controls.Add(this.importButton);
      this.Controls.Add(this.exportButton);
      this.Controls.Add(this.backupButton);
      this.Controls.Add(this.restoreButton);
      this.Controls.Add(this.closeButton);
      this.Controls.Add(this.certificateList);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.KeyPreview = true;
      this.MinimumSize = new System.Drawing.Size(600, 400);
      this.Name = "CertificateManagerDialog";
      this.Text = "CertificateManagerDialog";
      this.Load += new System.EventHandler(this.CertificateManagerDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CertificateManagerDialog_KeyDown);
      this.certificateContextMenu.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.ListView certificateList;
    private System.Windows.Forms.ColumnHeader idColumnHeader;
    private System.Windows.Forms.ColumnHeader typeColumnHeader;
    private System.Windows.Forms.ColumnHeader groupNameColumnHeader;
    private System.Windows.Forms.ColumnHeader statusColumnHeader;
    private System.Windows.Forms.ColumnHeader validFromColumnHeader;
    private System.Windows.Forms.ColumnHeader validUntilColumnHeader;
    private System.Windows.Forms.Button closeButton;
    private System.Windows.Forms.Button restoreButton;
    private System.Windows.Forms.Button backupButton;
    private System.Windows.Forms.Button exportButton;
    private System.Windows.Forms.Button importButton;
    private System.Windows.Forms.Button removeButton;
    private System.Windows.Forms.Button createNewButton;
    private System.Windows.Forms.Button setPasswordButton;
    private System.Windows.Forms.ContextMenuStrip certificateContextMenu;
    private System.Windows.Forms.ToolStripMenuItem createNewContextMenu;
    private System.Windows.Forms.ToolStripMenuItem removeContextMenu;
    private System.Windows.Forms.ToolStripMenuItem importContextMenu;
    private System.Windows.Forms.ToolStripMenuItem exportContextMenu;
    private System.Windows.Forms.ToolStripMenuItem backupContextMenu;
    private System.Windows.Forms.ToolStripMenuItem restoreContextMenu;
    private System.Windows.Forms.ToolStripMenuItem setPasswordContextMenu;
    private System.Windows.Forms.Button refreshButton;
    private System.Windows.Forms.ToolStripMenuItem refreshContextMenu;
  }
}