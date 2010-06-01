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
      this.pathTextBox = new System.Windows.Forms.TextBox();
      this.loadButton = new System.Windows.Forms.Button();
      this.loadList = new System.Windows.Forms.ListView();
      this.fileColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.englishColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.germanColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.frenchColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.importList = new System.Windows.Forms.ListView();
      this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.exportButton = new System.Windows.Forms.Button();
      this.importButton = new System.Windows.Forms.Button();
      this.saveButton = new System.Windows.Forms.Button();
      this.SuspendLayout();
      // 
      // pathTextBox
      // 
      this.pathTextBox.Location = new System.Drawing.Point(12, 12);
      this.pathTextBox.Name = "pathTextBox";
      this.pathTextBox.Size = new System.Drawing.Size(459, 20);
      this.pathTextBox.TabIndex = 0;
      // 
      // loadButton
      // 
      this.loadButton.Location = new System.Drawing.Point(477, 12);
      this.loadButton.Name = "loadButton";
      this.loadButton.Size = new System.Drawing.Size(75, 20);
      this.loadButton.TabIndex = 1;
      this.loadButton.Text = "Load";
      this.loadButton.UseVisualStyleBackColor = true;
      // 
      // loadList
      // 
      this.loadList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.fileColumnHeader,
            this.englishColumnHeader,
            this.germanColumnHeader,
            this.frenchColumnHeader});
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
      // importList
      // 
      this.importList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4});
      this.importList.FullRowSelect = true;
      this.importList.Location = new System.Drawing.Point(12, 246);
      this.importList.MultiSelect = false;
      this.importList.Name = "importList";
      this.importList.Size = new System.Drawing.Size(540, 173);
      this.importList.TabIndex = 3;
      this.importList.UseCompatibleStateImageBehavior = false;
      this.importList.View = System.Windows.Forms.View.Details;
      // 
      // columnHeader1
      // 
      this.columnHeader1.Text = "File";
      this.columnHeader1.Width = 200;
      // 
      // columnHeader2
      // 
      this.columnHeader2.Text = "English";
      this.columnHeader2.Width = 100;
      // 
      // columnHeader3
      // 
      this.columnHeader3.Text = "German";
      this.columnHeader3.Width = 100;
      // 
      // columnHeader4
      // 
      this.columnHeader4.Text = "French";
      this.columnHeader4.Width = 100;
      // 
      // exportButton
      // 
      this.exportButton.Location = new System.Drawing.Point(12, 217);
      this.exportButton.Name = "exportButton";
      this.exportButton.Size = new System.Drawing.Size(103, 23);
      this.exportButton.TabIndex = 4;
      this.exportButton.Text = "Export";
      this.exportButton.UseVisualStyleBackColor = true;
      // 
      // importButton
      // 
      this.importButton.Location = new System.Drawing.Point(121, 217);
      this.importButton.Name = "importButton";
      this.importButton.Size = new System.Drawing.Size(103, 23);
      this.importButton.TabIndex = 5;
      this.importButton.Text = "Import";
      this.importButton.UseVisualStyleBackColor = true;
      // 
      // saveButton
      // 
      this.saveButton.Location = new System.Drawing.Point(12, 425);
      this.saveButton.Name = "saveButton";
      this.saveButton.Size = new System.Drawing.Size(103, 23);
      this.saveButton.TabIndex = 6;
      this.saveButton.Text = "Save";
      this.saveButton.UseVisualStyleBackColor = true;
      // 
      // Form1
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(564, 455);
      this.Controls.Add(this.saveButton);
      this.Controls.Add(this.importButton);
      this.Controls.Add(this.exportButton);
      this.Controls.Add(this.importList);
      this.Controls.Add(this.loadList);
      this.Controls.Add(this.loadButton);
      this.Controls.Add(this.pathTextBox);
      this.Name = "Form1";
      this.Text = "Form1";
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
    private System.Windows.Forms.ListView importList;
    private System.Windows.Forms.ColumnHeader columnHeader1;
    private System.Windows.Forms.ColumnHeader columnHeader2;
    private System.Windows.Forms.ColumnHeader columnHeader3;
    private System.Windows.Forms.ColumnHeader columnHeader4;
    private System.Windows.Forms.Button exportButton;
    private System.Windows.Forms.Button importButton;
    private System.Windows.Forms.Button saveButton;
  }
}

