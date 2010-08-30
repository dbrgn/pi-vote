namespace Pirate.PiVote.CaGui
{
  partial class CreateAdminDialog
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
      this.firstNameTextBox = new System.Windows.Forms.TextBox();
      this.emailAddressTextBox = new System.Windows.Forms.TextBox();
      this.emailAddressLabel = new System.Windows.Forms.Label();
      this.firstNameLabel = new System.Windows.Forms.Label();
      this.familyNameLabel = new System.Windows.Forms.Label();
      this.familyNameTextBox = new System.Windows.Forms.TextBox();
      this.functionLabel = new System.Windows.Forms.Label();
      this.functionTextBox = new System.Windows.Forms.TextBox();
      this.validUntilPicker = new System.Windows.Forms.DateTimePicker();
      this.validUntilLabel = new System.Windows.Forms.Label();
      this.passphraseLabel = new System.Windows.Forms.Label();
      this.passphraseTextBox = new System.Windows.Forms.TextBox();
      this.repeatLabel = new System.Windows.Forms.Label();
      this.repeatTextBox = new System.Windows.Forms.TextBox();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // cancelButton
      // 
      this.cancelButton.Location = new System.Drawing.Point(332, 214);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 8;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Enabled = false;
      this.okButton.Location = new System.Drawing.Point(251, 214);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 7;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // firstNameTextBox
      // 
      this.firstNameTextBox.Location = new System.Drawing.Point(116, 12);
      this.firstNameTextBox.Name = "firstNameTextBox";
      this.firstNameTextBox.Size = new System.Drawing.Size(291, 20);
      this.firstNameTextBox.TabIndex = 0;
      this.firstNameTextBox.TextChanged += new System.EventHandler(this.firstNameTextBox_TextChanged);
      // 
      // emailAddressTextBox
      // 
      this.emailAddressTextBox.Location = new System.Drawing.Point(116, 90);
      this.emailAddressTextBox.Name = "emailAddressTextBox";
      this.emailAddressTextBox.Size = new System.Drawing.Size(291, 20);
      this.emailAddressTextBox.TabIndex = 3;
      this.emailAddressTextBox.TextChanged += new System.EventHandler(this.emailAddressTextBox_TextChanged);
      // 
      // emailAddressLabel
      // 
      this.emailAddressLabel.AutoSize = true;
      this.emailAddressLabel.Location = new System.Drawing.Point(16, 92);
      this.emailAddressLabel.Name = "emailAddressLabel";
      this.emailAddressLabel.Size = new System.Drawing.Size(77, 14);
      this.emailAddressLabel.TabIndex = 5;
      this.emailAddressLabel.Text = "Email address:";
      // 
      // firstNameLabel
      // 
      this.firstNameLabel.AutoSize = true;
      this.firstNameLabel.Location = new System.Drawing.Point(12, 14);
      this.firstNameLabel.Name = "firstNameLabel";
      this.firstNameLabel.Size = new System.Drawing.Size(60, 14);
      this.firstNameLabel.TabIndex = 6;
      this.firstNameLabel.Text = "First name:";
      // 
      // familyNameLabel
      // 
      this.familyNameLabel.AutoSize = true;
      this.familyNameLabel.Location = new System.Drawing.Point(12, 40);
      this.familyNameLabel.Name = "familyNameLabel";
      this.familyNameLabel.Size = new System.Drawing.Size(69, 14);
      this.familyNameLabel.TabIndex = 11;
      this.familyNameLabel.Text = "Family name:";
      // 
      // familyNameTextBox
      // 
      this.familyNameTextBox.Location = new System.Drawing.Point(116, 38);
      this.familyNameTextBox.Name = "familyNameTextBox";
      this.familyNameTextBox.Size = new System.Drawing.Size(291, 20);
      this.familyNameTextBox.TabIndex = 1;
      this.familyNameTextBox.TextChanged += new System.EventHandler(this.familyNameTextBox_TextChanged);
      // 
      // functionLabel
      // 
      this.functionLabel.AutoSize = true;
      this.functionLabel.Location = new System.Drawing.Point(16, 66);
      this.functionLabel.Name = "functionLabel";
      this.functionLabel.Size = new System.Drawing.Size(51, 14);
      this.functionLabel.TabIndex = 13;
      this.functionLabel.Text = "Function:";
      // 
      // functionTextBox
      // 
      this.functionTextBox.Location = new System.Drawing.Point(116, 64);
      this.functionTextBox.Name = "functionTextBox";
      this.functionTextBox.Size = new System.Drawing.Size(291, 20);
      this.functionTextBox.TabIndex = 2;
      this.functionTextBox.TextChanged += new System.EventHandler(this.functionTextBox_TextChanged);
      // 
      // validUntilPicker
      // 
      this.validUntilPicker.Location = new System.Drawing.Point(116, 185);
      this.validUntilPicker.Name = "validUntilPicker";
      this.validUntilPicker.Size = new System.Drawing.Size(291, 20);
      this.validUntilPicker.TabIndex = 6;
      // 
      // validUntilLabel
      // 
      this.validUntilLabel.AutoSize = true;
      this.validUntilLabel.Location = new System.Drawing.Point(16, 188);
      this.validUntilLabel.Name = "validUntilLabel";
      this.validUntilLabel.Size = new System.Drawing.Size(55, 14);
      this.validUntilLabel.TabIndex = 15;
      this.validUntilLabel.Text = "Valid until:";
      // 
      // passphraseLabel
      // 
      this.passphraseLabel.AutoSize = true;
      this.passphraseLabel.Location = new System.Drawing.Point(16, 118);
      this.passphraseLabel.Name = "passphraseLabel";
      this.passphraseLabel.Size = new System.Drawing.Size(68, 14);
      this.passphraseLabel.TabIndex = 17;
      this.passphraseLabel.Text = "Passphrase:";
      // 
      // passphraseTextBox
      // 
      this.passphraseTextBox.Location = new System.Drawing.Point(116, 116);
      this.passphraseTextBox.Name = "passphraseTextBox";
      this.passphraseTextBox.PasswordChar = '*';
      this.passphraseTextBox.Size = new System.Drawing.Size(291, 20);
      this.passphraseTextBox.TabIndex = 4;
      this.passphraseTextBox.TextChanged += new System.EventHandler(this.passphraseTextBox_TextChanged);
      // 
      // repeatLabel
      // 
      this.repeatLabel.AutoSize = true;
      this.repeatLabel.Location = new System.Drawing.Point(16, 144);
      this.repeatLabel.Name = "repeatLabel";
      this.repeatLabel.Size = new System.Drawing.Size(44, 14);
      this.repeatLabel.TabIndex = 19;
      this.repeatLabel.Text = "Repeat:";
      // 
      // repeatTextBox
      // 
      this.repeatTextBox.Location = new System.Drawing.Point(116, 142);
      this.repeatTextBox.Name = "repeatTextBox";
      this.repeatTextBox.PasswordChar = '*';
      this.repeatTextBox.Size = new System.Drawing.Size(291, 20);
      this.repeatTextBox.TabIndex = 5;
      this.repeatTextBox.TextChanged += new System.EventHandler(this.repeatTextBox_TextChanged);
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(113, 165);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(262, 14);
      this.label1.TabIndex = 20;
      this.label1.Text = "The passphrase must be at least 12 characters long.";
      // 
      // CreateAdminDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(419, 249);
      this.ControlBox = false;
      this.Controls.Add(this.label1);
      this.Controls.Add(this.repeatLabel);
      this.Controls.Add(this.repeatTextBox);
      this.Controls.Add(this.passphraseLabel);
      this.Controls.Add(this.passphraseTextBox);
      this.Controls.Add(this.validUntilPicker);
      this.Controls.Add(this.validUntilLabel);
      this.Controls.Add(this.functionLabel);
      this.Controls.Add(this.functionTextBox);
      this.Controls.Add(this.familyNameLabel);
      this.Controls.Add(this.familyNameTextBox);
      this.Controls.Add(this.firstNameLabel);
      this.Controls.Add(this.emailAddressLabel);
      this.Controls.Add(this.emailAddressTextBox);
      this.Controls.Add(this.firstNameTextBox);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "CreateAdminDialog";
      this.Text = "Create Admin Certificate";
      this.Load += new System.EventHandler(this.CaNameDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RefuseDialog_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.TextBox firstNameTextBox;
    private System.Windows.Forms.TextBox emailAddressTextBox;
    private System.Windows.Forms.Label emailAddressLabel;
    private System.Windows.Forms.Label firstNameLabel;
    private System.Windows.Forms.Label familyNameLabel;
    private System.Windows.Forms.TextBox familyNameTextBox;
    private System.Windows.Forms.Label functionLabel;
    private System.Windows.Forms.TextBox functionTextBox;
    private System.Windows.Forms.DateTimePicker validUntilPicker;
    private System.Windows.Forms.Label validUntilLabel;
    private System.Windows.Forms.Label passphraseLabel;
    private System.Windows.Forms.TextBox passphraseTextBox;
    private System.Windows.Forms.Label repeatLabel;
    private System.Windows.Forms.TextBox repeatTextBox;
    private System.Windows.Forms.Label label1;
  }
}