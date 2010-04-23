namespace Pirate.PiVote.Client
{
  partial class CertificateControl
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
      this.typeTextBox = new System.Windows.Forms.TextBox();
      this.typeLabel = new System.Windows.Forms.Label();
      this.idTextBox = new System.Windows.Forms.TextBox();
      this.nameTextBox = new System.Windows.Forms.TextBox();
      this.idLabel = new System.Windows.Forms.Label();
      this.nameLabel = new System.Windows.Forms.Label();
      this.creationDateLabel = new System.Windows.Forms.Label();
      this.creationDateTextBox = new System.Windows.Forms.TextBox();
      this.signaturesLabel = new System.Windows.Forms.Label();
      this.signatureList = new System.Windows.Forms.ListView();
      this.idColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.nameColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.validFromColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.validUntilColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.statusColumnHeader = new System.Windows.Forms.ColumnHeader();
      this.SuspendLayout();
      // 
      // typeTextBox
      // 
      this.typeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.typeTextBox.Location = new System.Drawing.Point(150, 0);
      this.typeTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.typeTextBox.Name = "typeTextBox";
      this.typeTextBox.ReadOnly = true;
      this.typeTextBox.Size = new System.Drawing.Size(680, 26);
      this.typeTextBox.TabIndex = 0;
      // 
      // typeLabel
      // 
      this.typeLabel.AutoSize = true;
      this.typeLabel.Location = new System.Drawing.Point(-4, 5);
      this.typeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.typeLabel.Name = "typeLabel";
      this.typeLabel.Size = new System.Drawing.Size(47, 20);
      this.typeLabel.TabIndex = 1;
      this.typeLabel.Text = "Type:";
      // 
      // idTextBox
      // 
      this.idTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.idTextBox.Location = new System.Drawing.Point(150, 40);
      this.idTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.idTextBox.Name = "idTextBox";
      this.idTextBox.ReadOnly = true;
      this.idTextBox.Size = new System.Drawing.Size(680, 26);
      this.idTextBox.TabIndex = 2;
      // 
      // nameTextBox
      // 
      this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.nameTextBox.Location = new System.Drawing.Point(150, 80);
      this.nameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.nameTextBox.Name = "nameTextBox";
      this.nameTextBox.ReadOnly = true;
      this.nameTextBox.Size = new System.Drawing.Size(680, 26);
      this.nameTextBox.TabIndex = 3;
      // 
      // idLabel
      // 
      this.idLabel.AutoSize = true;
      this.idLabel.Location = new System.Drawing.Point(-4, 45);
      this.idLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.idLabel.Name = "idLabel";
      this.idLabel.Size = new System.Drawing.Size(27, 20);
      this.idLabel.TabIndex = 6;
      this.idLabel.Text = "Id:";
      // 
      // nameLabel
      // 
      this.nameLabel.AutoSize = true;
      this.nameLabel.Location = new System.Drawing.Point(-4, 85);
      this.nameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Size = new System.Drawing.Size(55, 20);
      this.nameLabel.TabIndex = 7;
      this.nameLabel.Text = "Name:";
      // 
      // creationDateLabel
      // 
      this.creationDateLabel.AutoSize = true;
      this.creationDateLabel.Location = new System.Drawing.Point(-4, 125);
      this.creationDateLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.creationDateLabel.Name = "creationDateLabel";
      this.creationDateLabel.Size = new System.Drawing.Size(112, 20);
      this.creationDateLabel.TabIndex = 8;
      this.creationDateLabel.Text = "Creation Date:";
      // 
      // creationDateTextBox
      // 
      this.creationDateTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.creationDateTextBox.Location = new System.Drawing.Point(150, 120);
      this.creationDateTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.creationDateTextBox.Name = "creationDateTextBox";
      this.creationDateTextBox.ReadOnly = true;
      this.creationDateTextBox.Size = new System.Drawing.Size(680, 26);
      this.creationDateTextBox.TabIndex = 9;
      // 
      // signaturesLabel
      // 
      this.signaturesLabel.AutoSize = true;
      this.signaturesLabel.Location = new System.Drawing.Point(-4, 165);
      this.signaturesLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.signaturesLabel.Name = "signaturesLabel";
      this.signaturesLabel.Size = new System.Drawing.Size(90, 20);
      this.signaturesLabel.TabIndex = 11;
      this.signaturesLabel.Text = "Signatures:";
      // 
      // signatureList
      // 
      this.signatureList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.signatureList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idColumnHeader,
            this.nameColumnHeader,
            this.validFromColumnHeader,
            this.validUntilColumnHeader,
            this.statusColumnHeader});
      this.signatureList.Location = new System.Drawing.Point(150, 160);
      this.signatureList.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.signatureList.MultiSelect = false;
      this.signatureList.Name = "signatureList";
      this.signatureList.Size = new System.Drawing.Size(680, 386);
      this.signatureList.TabIndex = 12;
      this.signatureList.UseCompatibleStateImageBehavior = false;
      this.signatureList.View = System.Windows.Forms.View.Details;
      this.signatureList.DoubleClick += new System.EventHandler(this.signatureList_DoubleClick);
      // 
      // idColumnHeader
      // 
      this.idColumnHeader.Text = "Id";
      this.idColumnHeader.Width = 120;
      // 
      // nameColumnHeader
      // 
      this.nameColumnHeader.Text = "Name";
      this.nameColumnHeader.Width = 120;
      // 
      // validFromColumnHeader
      // 
      this.validFromColumnHeader.Text = "Valid From";
      this.validFromColumnHeader.Width = 80;
      // 
      // validUntilColumnHeader
      // 
      this.validUntilColumnHeader.Text = "Vaild Until";
      this.validUntilColumnHeader.Width = 80;
      // 
      // statusColumnHeader
      // 
      this.statusColumnHeader.Text = "Status";
      this.statusColumnHeader.Width = 50;
      // 
      // CertificateControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.signatureList);
      this.Controls.Add(this.signaturesLabel);
      this.Controls.Add(this.creationDateTextBox);
      this.Controls.Add(this.creationDateLabel);
      this.Controls.Add(this.nameLabel);
      this.Controls.Add(this.idLabel);
      this.Controls.Add(this.nameTextBox);
      this.Controls.Add(this.idTextBox);
      this.Controls.Add(this.typeLabel);
      this.Controls.Add(this.typeTextBox);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "CertificateControl";
      this.Size = new System.Drawing.Size(832, 548);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox typeTextBox;
    private System.Windows.Forms.Label typeLabel;
    private System.Windows.Forms.TextBox idTextBox;
    private System.Windows.Forms.TextBox nameTextBox;
    private System.Windows.Forms.Label idLabel;
    private System.Windows.Forms.Label nameLabel;
    private System.Windows.Forms.Label creationDateLabel;
    private System.Windows.Forms.TextBox creationDateTextBox;
    private System.Windows.Forms.Label signaturesLabel;
    private System.Windows.Forms.ListView signatureList;
    private System.Windows.Forms.ColumnHeader idColumnHeader;
    private System.Windows.Forms.ColumnHeader nameColumnHeader;
    private System.Windows.Forms.ColumnHeader validFromColumnHeader;
    private System.Windows.Forms.ColumnHeader validUntilColumnHeader;
    private System.Windows.Forms.ColumnHeader statusColumnHeader;
  }
}
