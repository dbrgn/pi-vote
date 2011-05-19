namespace Pirate.PiVote.Circle.Result
{
  partial class ResultDisplayDialog
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ResultDisplayDialog));
      this.okButton = new System.Windows.Forms.Button();
      this.resultList = new System.Windows.Forms.ListView();
      this.keyColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.valueColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.SuspendLayout();
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.Location = new System.Drawing.Point(631, 439);
      this.okButton.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(100, 27);
      this.okButton.TabIndex = 1;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // resultList
      // 
      this.resultList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.keyColumn,
            this.valueColumn});
      this.resultList.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.resultList.FullRowSelect = true;
      this.resultList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
      this.resultList.Location = new System.Drawing.Point(11, 12);
      this.resultList.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.resultList.Name = "resultList";
      this.resultList.Size = new System.Drawing.Size(720, 421);
      this.resultList.TabIndex = 3;
      this.resultList.UseCompatibleStateImageBehavior = false;
      this.resultList.View = System.Windows.Forms.View.Details;
      // 
      // keyColumn
      // 
      this.keyColumn.Text = "Text";
      // 
      // valueColumn
      // 
      this.valueColumn.Text = "Count";
      // 
      // ResultDisplayDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(742, 473);
      this.Controls.Add(this.resultList);
      this.Controls.Add(this.okButton);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(2, 3, 2, 3);
      this.MinimumSize = new System.Drawing.Size(300, 300);
      this.Name = "ResultDisplayDialog";
      this.Text = "ResultDisplayDialog";
      this.Load += new System.EventHandler(this.ResultDisplayDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ResultDisplayDialog_KeyDown);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.ListView resultList;
    private System.Windows.Forms.ColumnHeader keyColumn;
    private System.Windows.Forms.ColumnHeader valueColumn;
  }
}