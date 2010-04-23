namespace Pirate.PiVote.CaGui
{
  partial class CreateServerDialog
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
      this.nameTextBox = new System.Windows.Forms.TextBox();
      this.nameLabel = new System.Windows.Forms.Label();
      this.validUntilPicker = new System.Windows.Forms.DateTimePicker();
      this.validUntilLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // cancelButton
      // 
      this.cancelButton.Location = new System.Drawing.Point(462, 90);
      this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(112, 35);
      this.cancelButton.TabIndex = 5;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Enabled = false;
      this.okButton.Location = new System.Drawing.Point(342, 90);
      this.okButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(112, 35);
      this.okButton.TabIndex = 4;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // nameTextBox
      // 
      this.nameTextBox.Location = new System.Drawing.Point(140, 18);
      this.nameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.nameTextBox.Name = "nameTextBox";
      this.nameTextBox.Size = new System.Drawing.Size(434, 26);
      this.nameTextBox.TabIndex = 0;
      this.nameTextBox.TextChanged += new System.EventHandler(this.firstNameTextBox_TextChanged);
      // 
      // nameLabel
      // 
      this.nameLabel.AutoSize = true;
      this.nameLabel.Location = new System.Drawing.Point(18, 21);
      this.nameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Size = new System.Drawing.Size(55, 20);
      this.nameLabel.TabIndex = 6;
      this.nameLabel.Text = "Name:";
      // 
      // validUntilPicker
      // 
      this.validUntilPicker.Location = new System.Drawing.Point(140, 54);
      this.validUntilPicker.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.validUntilPicker.Name = "validUntilPicker";
      this.validUntilPicker.Size = new System.Drawing.Size(434, 26);
      this.validUntilPicker.TabIndex = 14;
      // 
      // validUntilLabel
      // 
      this.validUntilLabel.AutoSize = true;
      this.validUntilLabel.Location = new System.Drawing.Point(18, 59);
      this.validUntilLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.validUntilLabel.Name = "validUntilLabel";
      this.validUntilLabel.Size = new System.Drawing.Size(81, 20);
      this.validUntilLabel.TabIndex = 15;
      this.validUntilLabel.Text = "Valid until:";
      // 
      // CreateServerDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(594, 147);
      this.ControlBox = false;
      this.Controls.Add(this.validUntilPicker);
      this.Controls.Add(this.validUntilLabel);
      this.Controls.Add(this.nameLabel);
      this.Controls.Add(this.nameTextBox);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "CreateServerDialog";
      this.Text = "Create Server Certificate";
      this.Load += new System.EventHandler(this.CaNameDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RefuseDialog_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.TextBox nameTextBox;
    private System.Windows.Forms.Label nameLabel;
    private System.Windows.Forms.DateTimePicker validUntilPicker;
    private System.Windows.Forms.Label validUntilLabel;
  }
}