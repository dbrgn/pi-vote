namespace Pirate.PiVote.CaGui
{
  partial class DecryptCaKeyDialog
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
      this.caNameLabel = new System.Windows.Forms.Label();
      this.caNameTextBox = new System.Windows.Forms.TextBox();
      this.cancelButton = new System.Windows.Forms.Button();
      this.okButton = new System.Windows.Forms.Button();
      this.passphraseTextBox = new System.Windows.Forms.TextBox();
      this.passphraseLabel = new System.Windows.Forms.Label();
      this.caIdTextBox = new System.Windows.Forms.TextBox();
      this.caIdLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // caNameLabel
      // 
      this.caNameLabel.AutoSize = true;
      this.caNameLabel.Location = new System.Drawing.Point(12, 41);
      this.caNameLabel.Name = "caNameLabel";
      this.caNameLabel.Size = new System.Drawing.Size(54, 14);
      this.caNameLabel.TabIndex = 0;
      this.caNameLabel.Text = "CA Name:";
      // 
      // caNameTextBox
      // 
      this.caNameTextBox.Location = new System.Drawing.Point(103, 38);
      this.caNameTextBox.Name = "caNameTextBox";
      this.caNameTextBox.ReadOnly = true;
      this.caNameTextBox.Size = new System.Drawing.Size(340, 20);
      this.caNameTextBox.TabIndex = 0;
      // 
      // cancelButton
      // 
      this.cancelButton.Location = new System.Drawing.Point(368, 90);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 2;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Enabled = false;
      this.okButton.Location = new System.Drawing.Point(287, 90);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 1;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // passphraseTextBox
      // 
      this.passphraseTextBox.Location = new System.Drawing.Point(103, 64);
      this.passphraseTextBox.Name = "passphraseTextBox";
      this.passphraseTextBox.PasswordChar = '*';
      this.passphraseTextBox.Size = new System.Drawing.Size(340, 20);
      this.passphraseTextBox.TabIndex = 0;
      this.passphraseTextBox.TextChanged += new System.EventHandler(this.passphraseTextBox_TextChanged);
      // 
      // passphraseLabel
      // 
      this.passphraseLabel.AutoSize = true;
      this.passphraseLabel.Location = new System.Drawing.Point(12, 67);
      this.passphraseLabel.Name = "passphraseLabel";
      this.passphraseLabel.Size = new System.Drawing.Size(68, 14);
      this.passphraseLabel.TabIndex = 6;
      this.passphraseLabel.Text = "Passphrase:";
      // 
      // caIdTextBox
      // 
      this.caIdTextBox.Location = new System.Drawing.Point(103, 12);
      this.caIdTextBox.Name = "caIdTextBox";
      this.caIdTextBox.ReadOnly = true;
      this.caIdTextBox.Size = new System.Drawing.Size(340, 20);
      this.caIdTextBox.TabIndex = 9;
      // 
      // caIdLabel
      // 
      this.caIdLabel.AutoSize = true;
      this.caIdLabel.Location = new System.Drawing.Point(12, 15);
      this.caIdLabel.Name = "caIdLabel";
      this.caIdLabel.Size = new System.Drawing.Size(35, 14);
      this.caIdLabel.TabIndex = 10;
      this.caIdLabel.Text = "CA Id:";
      // 
      // DecryptCaKeyDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(455, 123);
      this.ControlBox = false;
      this.Controls.Add(this.caIdLabel);
      this.Controls.Add(this.caIdTextBox);
      this.Controls.Add(this.passphraseLabel);
      this.Controls.Add(this.passphraseTextBox);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.caNameTextBox);
      this.Controls.Add(this.caNameLabel);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "DecryptCaKeyDialog";
      this.Text = "Decrypt CA private key";
      this.Load += new System.EventHandler(this.CaNameDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CreateCaDialog_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label caNameLabel;
    private System.Windows.Forms.TextBox caNameTextBox;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.TextBox passphraseTextBox;
    private System.Windows.Forms.Label passphraseLabel;
    private System.Windows.Forms.TextBox caIdTextBox;
    private System.Windows.Forms.Label caIdLabel;
  }
}