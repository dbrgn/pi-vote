namespace Pirate.PiVote.CaGui
{
  partial class GenerateCrlDialog
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
      this.cancelButton = new System.Windows.Forms.Button();
      this.okButton = new System.Windows.Forms.Button();
      this.idTextBox = new System.Windows.Forms.TextBox();
      this.nameTextBox = new System.Windows.Forms.TextBox();
      this.nameLabel = new System.Windows.Forms.Label();
      this.idLabel = new System.Windows.Forms.Label();
      this.validUntilPicker = new System.Windows.Forms.DateTimePicker();
      this.validUntilLabel = new System.Windows.Forms.Label();
      this.validFromPicker = new System.Windows.Forms.DateTimePicker();
      this.validFromLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // cancelButton
      // 
      this.cancelButton.Location = new System.Drawing.Point(464, 188);
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
      this.okButton.Location = new System.Drawing.Point(342, 188);
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
      this.nameTextBox.Location = new System.Drawing.Point(123, 58);
      this.nameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.nameTextBox.Name = "nameTextBox";
      this.nameTextBox.ReadOnly = true;
      this.nameTextBox.Size = new System.Drawing.Size(451, 26);
      this.nameTextBox.TabIndex = 4;
      // 
      // nameLabel
      // 
      this.nameLabel.AutoSize = true;
      this.nameLabel.Location = new System.Drawing.Point(18, 63);
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
      // validUntilPicker
      // 
      this.validUntilPicker.Location = new System.Drawing.Point(123, 138);
      this.validUntilPicker.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.validUntilPicker.Name = "validUntilPicker";
      this.validUntilPicker.Size = new System.Drawing.Size(451, 26);
      this.validUntilPicker.TabIndex = 1;
      this.validUntilPicker.ValueChanged += new System.EventHandler(this.validUntilPicker_ValueChanged);
      // 
      // validUntilLabel
      // 
      this.validUntilLabel.AutoSize = true;
      this.validUntilLabel.Location = new System.Drawing.Point(18, 143);
      this.validUntilLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.validUntilLabel.Name = "validUntilLabel";
      this.validUntilLabel.Size = new System.Drawing.Size(81, 20);
      this.validUntilLabel.TabIndex = 8;
      this.validUntilLabel.Text = "Valid until:";
      // 
      // validFromPicker
      // 
      this.validFromPicker.Location = new System.Drawing.Point(123, 98);
      this.validFromPicker.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.validFromPicker.Name = "validFromPicker";
      this.validFromPicker.Size = new System.Drawing.Size(451, 26);
      this.validFromPicker.TabIndex = 0;
      this.validFromPicker.ValueChanged += new System.EventHandler(this.validFromPicker_ValueChanged);
      // 
      // validFromLabel
      // 
      this.validFromLabel.AutoSize = true;
      this.validFromLabel.Location = new System.Drawing.Point(18, 103);
      this.validFromLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.validFromLabel.Name = "validFromLabel";
      this.validFromLabel.Size = new System.Drawing.Size(84, 20);
      this.validFromLabel.TabIndex = 10;
      this.validFromLabel.Text = "Valid from:";
      // 
      // GenerateCrlDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(594, 242);
      this.ControlBox = false;
      this.Controls.Add(this.validFromPicker);
      this.Controls.Add(this.validFromLabel);
      this.Controls.Add(this.validUntilPicker);
      this.Controls.Add(this.validUntilLabel);
      this.Controls.Add(this.idLabel);
      this.Controls.Add(this.nameLabel);
      this.Controls.Add(this.nameTextBox);
      this.Controls.Add(this.idTextBox);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "GenerateCrlDialog";
      this.Text = "Generate Certificate Revocation List";
      this.Load += new System.EventHandler(this.CaNameDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RefuseDialog_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.TextBox idTextBox;
    private System.Windows.Forms.TextBox nameTextBox;
    private System.Windows.Forms.Label nameLabel;
    private System.Windows.Forms.Label idLabel;
    private System.Windows.Forms.DateTimePicker validUntilPicker;
    private System.Windows.Forms.Label validUntilLabel;
    private System.Windows.Forms.DateTimePicker validFromPicker;
    private System.Windows.Forms.Label validFromLabel;
  }
}