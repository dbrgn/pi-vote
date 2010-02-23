namespace Pirate.PiVote.CaGui
{
  partial class RefuseDialog
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
      this.reasonLabel = new System.Windows.Forms.Label();
      this.reasonTextBox = new System.Windows.Forms.TextBox();
      this.cancelButton = new System.Windows.Forms.Button();
      this.okButton = new System.Windows.Forms.Button();
      this.idTextBox = new System.Windows.Forms.TextBox();
      this.nameTextBox = new System.Windows.Forms.TextBox();
      this.nameLabel = new System.Windows.Forms.Label();
      this.idLabel = new System.Windows.Forms.Label();
      this.typeLabel = new System.Windows.Forms.Label();
      this.typeTextBox = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // reasonLabel
      // 
      this.reasonLabel.AutoSize = true;
      this.reasonLabel.Location = new System.Drawing.Point(18, 143);
      this.reasonLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.reasonLabel.Name = "reasonLabel";
      this.reasonLabel.Size = new System.Drawing.Size(69, 20);
      this.reasonLabel.TabIndex = 0;
      this.reasonLabel.Text = "Reason:";
      // 
      // reasonTextBox
      // 
      this.reasonTextBox.Location = new System.Drawing.Point(123, 138);
      this.reasonTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.reasonTextBox.Multiline = true;
      this.reasonTextBox.Name = "reasonTextBox";
      this.reasonTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.reasonTextBox.Size = new System.Drawing.Size(451, 190);
      this.reasonTextBox.TabIndex = 0;
      this.reasonTextBox.TextChanged += new System.EventHandler(this.nameTextBox_TextChanged);
      // 
      // cancelButton
      // 
      this.cancelButton.Location = new System.Drawing.Point(464, 345);
      this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(112, 35);
      this.cancelButton.TabIndex = 2;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Enabled = false;
      this.okButton.Location = new System.Drawing.Point(342, 345);
      this.okButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(112, 35);
      this.okButton.TabIndex = 1;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // idTextBox
      // 
      this.idTextBox.Location = new System.Drawing.Point(123, 18);
      this.idTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.idTextBox.Name = "idTextBox";
      this.idTextBox.ReadOnly = true;
      this.idTextBox.Size = new System.Drawing.Size(451, 26);
      this.idTextBox.TabIndex = 3;
      // 
      // nameTextBox
      // 
      this.nameTextBox.Location = new System.Drawing.Point(123, 98);
      this.nameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.nameTextBox.Name = "nameTextBox";
      this.nameTextBox.ReadOnly = true;
      this.nameTextBox.Size = new System.Drawing.Size(451, 26);
      this.nameTextBox.TabIndex = 4;
      // 
      // nameLabel
      // 
      this.nameLabel.AutoSize = true;
      this.nameLabel.Location = new System.Drawing.Point(18, 103);
      this.nameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Size = new System.Drawing.Size(55, 20);
      this.nameLabel.TabIndex = 5;
      this.nameLabel.Text = "Name:";
      // 
      // idLabel
      // 
      this.idLabel.AutoSize = true;
      this.idLabel.Location = new System.Drawing.Point(18, 23);
      this.idLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.idLabel.Name = "idLabel";
      this.idLabel.Size = new System.Drawing.Size(27, 20);
      this.idLabel.TabIndex = 6;
      this.idLabel.Text = "Id:";
      // 
      // typeLabel
      // 
      this.typeLabel.AutoSize = true;
      this.typeLabel.Location = new System.Drawing.Point(18, 63);
      this.typeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.typeLabel.Name = "typeLabel";
      this.typeLabel.Size = new System.Drawing.Size(47, 20);
      this.typeLabel.TabIndex = 11;
      this.typeLabel.Text = "Type:";
      // 
      // typeTextBox
      // 
      this.typeTextBox.Location = new System.Drawing.Point(123, 58);
      this.typeTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.typeTextBox.Name = "typeTextBox";
      this.typeTextBox.ReadOnly = true;
      this.typeTextBox.Size = new System.Drawing.Size(451, 26);
      this.typeTextBox.TabIndex = 10;
      // 
      // RefuseDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(594, 398);
      this.ControlBox = false;
      this.Controls.Add(this.typeLabel);
      this.Controls.Add(this.typeTextBox);
      this.Controls.Add(this.idLabel);
      this.Controls.Add(this.nameLabel);
      this.Controls.Add(this.nameTextBox);
      this.Controls.Add(this.idTextBox);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.reasonTextBox);
      this.Controls.Add(this.reasonLabel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "RefuseDialog";
      this.Text = "Refuse Signature Request";
      this.Load += new System.EventHandler(this.CaNameDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RefuseDialog_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label reasonLabel;
    private System.Windows.Forms.TextBox reasonTextBox;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.TextBox idTextBox;
    private System.Windows.Forms.TextBox nameTextBox;
    private System.Windows.Forms.Label nameLabel;
    private System.Windows.Forms.Label idLabel;
    private System.Windows.Forms.Label typeLabel;
    private System.Windows.Forms.TextBox typeTextBox;
  }
}