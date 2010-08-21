namespace Pirate.PiVote.CaGui
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Master));
      this.mainMenu = new System.Windows.Forms.MenuStrip();
      this.selfToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.cAPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
      this.createToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.signatureRequestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.signatureResponseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
      this.exportPublicKeyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.importCertificateStorageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
      this.exportRootCertificateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.signaturesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.importRequestsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.generateRevocationListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
      this.createAdminCertificateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.createServerCertifiToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.entryListView = new System.Windows.Forms.ListView();
      this.idColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.typeColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.validFromColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.validUntilColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.statusColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.entryListContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
      this.signToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.exportResponseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.revokeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
      this.splitContainer1 = new System.Windows.Forms.SplitContainer();
      this.searchDateActive = new System.Windows.Forms.CheckBox();
      this.searchTestBox = new System.Windows.Forms.TextBox();
      this.searchTypeBox = new System.Windows.Forms.ComboBox();
      this.searchStatusBox = new System.Windows.Forms.ComboBox();
      this.searchDateBox = new System.Windows.Forms.DateTimePicker();
      this.crlListView = new System.Windows.Forms.ListView();
      this.crlValidFromColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.crlValidUntilColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.revokedCount = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.mainMenu.SuspendLayout();
      this.entryListContextMenu.SuspendLayout();
      this.splitContainer1.Panel1.SuspendLayout();
      this.splitContainer1.Panel2.SuspendLayout();
      this.splitContainer1.SuspendLayout();
      this.SuspendLayout();
      // 
      // mainMenu
      // 
      this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selfToolStripMenuItem,
            this.signaturesToolStripMenuItem});
      this.mainMenu.Location = new System.Drawing.Point(0, 0);
      this.mainMenu.Name = "mainMenu";
      this.mainMenu.Size = new System.Drawing.Size(1050, 24);
      this.mainMenu.TabIndex = 0;
      this.mainMenu.Text = "menuStrip1";
      this.mainMenu.MenuActivate += new System.EventHandler(this.mainMenu_MenuActivate);
      // 
      // selfToolStripMenuItem
      // 
      this.selfToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cAPropertiesToolStripMenuItem,
            this.toolStripMenuItem2,
            this.createToolStripMenuItem,
            this.signatureRequestToolStripMenuItem,
            this.signatureResponseToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exportPublicKeyToolStripMenuItem,
            this.importCertificateStorageToolStripMenuItem,
            this.toolStripMenuItem3,
            this.exportRootCertificateToolStripMenuItem});
      this.selfToolStripMenuItem.Name = "selfToolStripMenuItem";
      this.selfToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
      this.selfToolStripMenuItem.Text = "&Self";
      // 
      // cAPropertiesToolStripMenuItem
      // 
      this.cAPropertiesToolStripMenuItem.Name = "cAPropertiesToolStripMenuItem";
      this.cAPropertiesToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
      this.cAPropertiesToolStripMenuItem.Text = "CA &Properties";
      this.cAPropertiesToolStripMenuItem.Click += new System.EventHandler(this.cAPropertiesToolStripMenuItem_Click);
      // 
      // toolStripMenuItem2
      // 
      this.toolStripMenuItem2.Name = "toolStripMenuItem2";
      this.toolStripMenuItem2.Size = new System.Drawing.Size(202, 6);
      // 
      // createToolStripMenuItem
      // 
      this.createToolStripMenuItem.Name = "createToolStripMenuItem";
      this.createToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
      this.createToolStripMenuItem.Text = "Create &Certificate";
      this.createToolStripMenuItem.Click += new System.EventHandler(this.createToolStripMenuItem_Click);
      // 
      // signatureRequestToolStripMenuItem
      // 
      this.signatureRequestToolStripMenuItem.Name = "signatureRequestToolStripMenuItem";
      this.signatureRequestToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
      this.signatureRequestToolStripMenuItem.Text = "Create Signature &Request";
      this.signatureRequestToolStripMenuItem.Click += new System.EventHandler(this.signatureRequestToolStripMenuItem_Click);
      // 
      // signatureResponseToolStripMenuItem
      // 
      this.signatureResponseToolStripMenuItem.Name = "signatureResponseToolStripMenuItem";
      this.signatureResponseToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
      this.signatureResponseToolStripMenuItem.Text = "Import Signature &Response";
      this.signatureResponseToolStripMenuItem.Click += new System.EventHandler(this.signatureResponseToolStripMenuItem_Click);
      // 
      // toolStripMenuItem1
      // 
      this.toolStripMenuItem1.Name = "toolStripMenuItem1";
      this.toolStripMenuItem1.Size = new System.Drawing.Size(202, 6);
      // 
      // exportPublicKeyToolStripMenuItem
      // 
      this.exportPublicKeyToolStripMenuItem.Name = "exportPublicKeyToolStripMenuItem";
      this.exportPublicKeyToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
      this.exportPublicKeyToolStripMenuItem.Text = "&Export Certificate Storage";
      this.exportPublicKeyToolStripMenuItem.Click += new System.EventHandler(this.exportPublicKeyToolStripMenuItem_Click);
      // 
      // importCertificateStorageToolStripMenuItem
      // 
      this.importCertificateStorageToolStripMenuItem.Name = "importCertificateStorageToolStripMenuItem";
      this.importCertificateStorageToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
      this.importCertificateStorageToolStripMenuItem.Text = "&Import Certificate Storage";
      this.importCertificateStorageToolStripMenuItem.Click += new System.EventHandler(this.importCertificateStorageToolStripMenuItem_Click);
      // 
      // toolStripMenuItem3
      // 
      this.toolStripMenuItem3.Name = "toolStripMenuItem3";
      this.toolStripMenuItem3.Size = new System.Drawing.Size(202, 6);
      // 
      // exportRootCertificateToolStripMenuItem
      // 
      this.exportRootCertificateToolStripMenuItem.Name = "exportRootCertificateToolStripMenuItem";
      this.exportRootCertificateToolStripMenuItem.Size = new System.Drawing.Size(205, 22);
      this.exportRootCertificateToolStripMenuItem.Text = "Export Root &Certificate";
      this.exportRootCertificateToolStripMenuItem.Click += new System.EventHandler(this.exportRootCertificateToolStripMenuItem_Click);
      // 
      // signaturesToolStripMenuItem
      // 
      this.signaturesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.importRequestsToolStripMenuItem,
            this.generateRevocationListToolStripMenuItem,
            this.toolStripMenuItem4,
            this.createAdminCertificateToolStripMenuItem,
            this.createServerCertifiToolStripMenuItem});
      this.signaturesToolStripMenuItem.Name = "signaturesToolStripMenuItem";
      this.signaturesToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
      this.signaturesToolStripMenuItem.Text = "&Signatures";
      // 
      // importRequestsToolStripMenuItem
      // 
      this.importRequestsToolStripMenuItem.Name = "importRequestsToolStripMenuItem";
      this.importRequestsToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.importRequestsToolStripMenuItem.Text = "&Import Requests";
      this.importRequestsToolStripMenuItem.Click += new System.EventHandler(this.importRequestsToolStripMenuItem_Click);
      // 
      // generateRevocationListToolStripMenuItem
      // 
      this.generateRevocationListToolStripMenuItem.Name = "generateRevocationListToolStripMenuItem";
      this.generateRevocationListToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.generateRevocationListToolStripMenuItem.Text = "&Generate Revocation List";
      this.generateRevocationListToolStripMenuItem.Click += new System.EventHandler(this.generateRevocationListToolStripMenuItem_Click);
      // 
      // toolStripMenuItem4
      // 
      this.toolStripMenuItem4.Name = "toolStripMenuItem4";
      this.toolStripMenuItem4.Size = new System.Drawing.Size(192, 6);
      // 
      // createAdminCertificateToolStripMenuItem
      // 
      this.createAdminCertificateToolStripMenuItem.Name = "createAdminCertificateToolStripMenuItem";
      this.createAdminCertificateToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.createAdminCertificateToolStripMenuItem.Text = "Create &Admin Certificate";
      this.createAdminCertificateToolStripMenuItem.Click += new System.EventHandler(this.createAdminCertificateToolStripMenuItem_Click);
      // 
      // createServerCertifiToolStripMenuItem
      // 
      this.createServerCertifiToolStripMenuItem.Name = "createServerCertifiToolStripMenuItem";
      this.createServerCertifiToolStripMenuItem.Size = new System.Drawing.Size(195, 22);
      this.createServerCertifiToolStripMenuItem.Text = "Create &Server Certificate";
      this.createServerCertifiToolStripMenuItem.Click += new System.EventHandler(this.createServerCertifiToolStripMenuItem_Click);
      // 
      // entryListView
      // 
      this.entryListView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.entryListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idColumnHeader,
            this.typeColumnHeader,
            this.nameColumnHeader,
            this.validFromColumnHeader,
            this.validUntilColumnHeader,
            this.statusColumnHeader});
      this.entryListView.ContextMenuStrip = this.entryListContextMenu;
      this.entryListView.FullRowSelect = true;
      this.entryListView.HideSelection = false;
      this.entryListView.Location = new System.Drawing.Point(0, 31);
      this.entryListView.MultiSelect = false;
      this.entryListView.Name = "entryListView";
      this.entryListView.Size = new System.Drawing.Size(1050, 402);
      this.entryListView.TabIndex = 1;
      this.entryListView.UseCompatibleStateImageBehavior = false;
      this.entryListView.View = System.Windows.Forms.View.Details;
      this.entryListView.SelectedIndexChanged += new System.EventHandler(this.entryListView_SelectedIndexChanged);
      // 
      // idColumnHeader
      // 
      this.idColumnHeader.Text = "Id";
      this.idColumnHeader.Width = 220;
      // 
      // typeColumnHeader
      // 
      this.typeColumnHeader.Text = "Type";
      this.typeColumnHeader.Width = 100;
      // 
      // nameColumnHeader
      // 
      this.nameColumnHeader.Text = "Name";
      this.nameColumnHeader.Width = 300;
      // 
      // validFromColumnHeader
      // 
      this.validFromColumnHeader.Text = "Valid From";
      this.validFromColumnHeader.Width = 150;
      // 
      // validUntilColumnHeader
      // 
      this.validUntilColumnHeader.Text = "Valid Until";
      this.validUntilColumnHeader.Width = 150;
      // 
      // statusColumnHeader
      // 
      this.statusColumnHeader.Text = "Status";
      this.statusColumnHeader.Width = 120;
      // 
      // entryListContextMenu
      // 
      this.entryListContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.signToolStripMenuItem,
            this.exportResponseToolStripMenuItem,
            this.revokeToolStripMenuItem});
      this.entryListContextMenu.Name = "entryListContextMenu";
      this.entryListContextMenu.Size = new System.Drawing.Size(157, 92);
      this.entryListContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.entryListContextMenu_Opening);
      // 
      // signToolStripMenuItem
      // 
      this.signToolStripMenuItem.Name = "signToolStripMenuItem";
      this.signToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
      this.signToolStripMenuItem.Text = "&Verify...";
      this.signToolStripMenuItem.Click += new System.EventHandler(this.signToolStripMenuItem_Click);
      // 
      // exportResponseToolStripMenuItem
      // 
      this.exportResponseToolStripMenuItem.Name = "exportResponseToolStripMenuItem";
      this.exportResponseToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
      this.exportResponseToolStripMenuItem.Text = "&Export Response";
      this.exportResponseToolStripMenuItem.Click += new System.EventHandler(this.exportResponseToolStripMenuItem_Click);
      // 
      // revokeToolStripMenuItem
      // 
      this.revokeToolStripMenuItem.Name = "revokeToolStripMenuItem";
      this.revokeToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
      this.revokeToolStripMenuItem.Text = "&Revoke";
      this.revokeToolStripMenuItem.Click += new System.EventHandler(this.revokeToolStripMenuItem_Click);
      // 
      // splitContainer1
      // 
      this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.splitContainer1.Location = new System.Drawing.Point(0, 24);
      this.splitContainer1.Name = "splitContainer1";
      this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
      // 
      // splitContainer1.Panel1
      // 
      this.splitContainer1.Panel1.Controls.Add(this.searchDateActive);
      this.splitContainer1.Panel1.Controls.Add(this.searchTestBox);
      this.splitContainer1.Panel1.Controls.Add(this.searchTypeBox);
      this.splitContainer1.Panel1.Controls.Add(this.searchStatusBox);
      this.splitContainer1.Panel1.Controls.Add(this.searchDateBox);
      this.splitContainer1.Panel1.Controls.Add(this.entryListView);
      // 
      // splitContainer1.Panel2
      // 
      this.splitContainer1.Panel2.Controls.Add(this.crlListView);
      this.splitContainer1.Size = new System.Drawing.Size(1050, 647);
      this.splitContainer1.SplitterDistance = 434;
      this.splitContainer1.TabIndex = 2;
      // 
      // searchDateActive
      // 
      this.searchDateActive.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.searchDateActive.AutoSize = true;
      this.searchDateActive.Location = new System.Drawing.Point(754, 7);
      this.searchDateActive.Name = "searchDateActive";
      this.searchDateActive.Size = new System.Drawing.Size(15, 14);
      this.searchDateActive.TabIndex = 6;
      this.searchDateActive.UseVisualStyleBackColor = true;
      this.searchDateActive.CheckedChanged += new System.EventHandler(this.searchDateActive_CheckedChanged);
      // 
      // searchTestBox
      // 
      this.searchTestBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.searchTestBox.Location = new System.Drawing.Point(3, 4);
      this.searchTestBox.Name = "searchTestBox";
      this.searchTestBox.Size = new System.Drawing.Size(547, 20);
      this.searchTestBox.TabIndex = 5;
      this.searchTestBox.TextChanged += new System.EventHandler(this.searchTestBox_TextChanged);
      // 
      // searchTypeBox
      // 
      this.searchTypeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.searchTypeBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.searchTypeBox.FormattingEnabled = true;
      this.searchTypeBox.Location = new System.Drawing.Point(556, 4);
      this.searchTypeBox.Name = "searchTypeBox";
      this.searchTypeBox.Size = new System.Drawing.Size(192, 22);
      this.searchTypeBox.TabIndex = 4;
      this.searchTypeBox.SelectedIndexChanged += new System.EventHandler(this.searchTypeBox_SelectedIndexChanged);
      // 
      // searchStatusBox
      // 
      this.searchStatusBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.searchStatusBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.searchStatusBox.FormattingEnabled = true;
      this.searchStatusBox.Location = new System.Drawing.Point(882, 4);
      this.searchStatusBox.Name = "searchStatusBox";
      this.searchStatusBox.Size = new System.Drawing.Size(168, 22);
      this.searchStatusBox.TabIndex = 3;
      this.searchStatusBox.SelectedIndexChanged += new System.EventHandler(this.searchStatusBox_SelectedIndexChanged);
      // 
      // searchDateBox
      // 
      this.searchDateBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.searchDateBox.Format = System.Windows.Forms.DateTimePickerFormat.Short;
      this.searchDateBox.Location = new System.Drawing.Point(775, 4);
      this.searchDateBox.Name = "searchDateBox";
      this.searchDateBox.Size = new System.Drawing.Size(101, 20);
      this.searchDateBox.TabIndex = 2;
      this.searchDateBox.ValueChanged += new System.EventHandler(this.searchDateBox_ValueChanged);
      // 
      // crlListView
      // 
      this.crlListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.crlValidFromColumnHeader,
            this.crlValidUntilColumnHeader,
            this.revokedCount});
      this.crlListView.ContextMenuStrip = this.entryListContextMenu;
      this.crlListView.Dock = System.Windows.Forms.DockStyle.Fill;
      this.crlListView.FullRowSelect = true;
      this.crlListView.HideSelection = false;
      this.crlListView.Location = new System.Drawing.Point(0, 0);
      this.crlListView.MultiSelect = false;
      this.crlListView.Name = "crlListView";
      this.crlListView.Size = new System.Drawing.Size(1050, 209);
      this.crlListView.TabIndex = 2;
      this.crlListView.UseCompatibleStateImageBehavior = false;
      this.crlListView.View = System.Windows.Forms.View.Details;
      // 
      // crlValidFromColumnHeader
      // 
      this.crlValidFromColumnHeader.Text = "Valid From";
      this.crlValidFromColumnHeader.Width = 150;
      // 
      // crlValidUntilColumnHeader
      // 
      this.crlValidUntilColumnHeader.Text = "Valid Until";
      this.crlValidUntilColumnHeader.Width = 150;
      // 
      // revokedCount
      // 
      this.revokedCount.Text = "# Revoked";
      this.revokedCount.Width = 100;
      // 
      // Master
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(1050, 671);
      this.Controls.Add(this.splitContainer1);
      this.Controls.Add(this.mainMenu);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MainMenuStrip = this.mainMenu;
      this.Name = "Master";
      this.Text = "PiVote Certificate Authority";
      this.Load += new System.EventHandler(this.Master_Load);
      this.mainMenu.ResumeLayout(false);
      this.mainMenu.PerformLayout();
      this.entryListContextMenu.ResumeLayout(false);
      this.splitContainer1.Panel1.ResumeLayout(false);
      this.splitContainer1.Panel1.PerformLayout();
      this.splitContainer1.Panel2.ResumeLayout(false);
      this.splitContainer1.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.MenuStrip mainMenu;
    private System.Windows.Forms.ListView entryListView;
    private System.Windows.Forms.ColumnHeader idColumnHeader;
    private System.Windows.Forms.ColumnHeader nameColumnHeader;
    private System.Windows.Forms.ColumnHeader validFromColumnHeader;
    private System.Windows.Forms.ColumnHeader validUntilColumnHeader;
    private System.Windows.Forms.ColumnHeader statusColumnHeader;
    private System.Windows.Forms.ToolStripMenuItem selfToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem createToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem signatureRequestToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem signatureResponseToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem signaturesToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem importRequestsToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem generateRevocationListToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
    private System.Windows.Forms.ToolStripMenuItem exportPublicKeyToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem importCertificateStorageToolStripMenuItem;
    private System.Windows.Forms.ContextMenuStrip entryListContextMenu;
    private System.Windows.Forms.ToolStripMenuItem signToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem revokeToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem exportResponseToolStripMenuItem;
    private System.Windows.Forms.ColumnHeader typeColumnHeader;
    private System.Windows.Forms.ToolStripMenuItem exportRootCertificateToolStripMenuItem;
    private System.Windows.Forms.SplitContainer splitContainer1;
    private System.Windows.Forms.ListView crlListView;
    private System.Windows.Forms.ColumnHeader crlValidFromColumnHeader;
    private System.Windows.Forms.ColumnHeader crlValidUntilColumnHeader;
    private System.Windows.Forms.ColumnHeader revokedCount;
    private System.Windows.Forms.ToolStripMenuItem cAPropertiesToolStripMenuItem;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
    private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
    private System.Windows.Forms.ToolStripMenuItem createAdminCertificateToolStripMenuItem;
    private System.Windows.Forms.ToolStripMenuItem createServerCertifiToolStripMenuItem;
    private System.Windows.Forms.TextBox searchTestBox;
    private System.Windows.Forms.ComboBox searchTypeBox;
    private System.Windows.Forms.ComboBox searchStatusBox;
    private System.Windows.Forms.DateTimePicker searchDateBox;
    private System.Windows.Forms.CheckBox searchDateActive;
  }
}

