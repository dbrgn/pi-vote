namespace Pirate.PiVote.Client
{
  partial class EncryptPrivateKeyDialog
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
      this.infoLabel = new System.Windows.Forms.Label();
      this.passphraseTextBox = new System.Windows.Forms.TextBox();
      this.passphraseLabel = new System.Windows.Forms.Label();
      this.repeatTextBox = new System.Windows.Forms.TextBox();
      this.repeatLabel = new System.Windows.Forms.Label();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.encryptCheckBox = new System.Windows.Forms.CheckBox();
      this.SuspendLayout();
      // 
      // infoLabel
      // 
      this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.infoLabel.Location = new System.Drawing.Point(12, 9);
      this.infoLabel.Name = "infoLabel";
      this.infoLabel.Size = new System.Drawing.Size(466, 39);
      this.infoLabel.TabIndex = 0;
      this.infoLabel.Text = "You may encrypt the private key of your certificate to proctet it against unautho" +
          "rized use. If you do so you will be prompted to enter your passphrase when your " +
          "private key is used.";
      // 
      // passphraseTextBox
      // 
      this.passphraseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.passphraseTextBox.Location = new System.Drawing.Point(132, 77);
      this.passphraseTextBox.Name = "passphraseTextBox";
      this.passphraseTextBox.PasswordChar = '*';
      this.passphraseTextBox.Size = new System.Drawing.Size(346, 20);
      this.passphraseTextBox.TabIndex = 1;
      this.passphraseTextBox.TextChanged += new System.EventHandler(this.passphraseTextBox_TextChanged);
      // 
      // passphraseLabel
      // 
      this.passphraseLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.passphraseLabel.AutoSize = true;
      this.passphraseLabel.Location = new System.Drawing.Point(12, 80);
      this.passphraseLabel.Name = "passphraseLabel";
      this.passphraseLabel.Size = new System.Drawing.Size(65, 13);
      this.passphraseLabel.TabIndex = 3;
      this.passphraseLabel.Text = "Passphrase:";
      // 
      // repeatTextBox
      // 
      this.repeatTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.repeatTextBox.Location = new System.Drawing.Point(132, 103);
      this.repeatTextBox.Name = "repeatTextBox";
      this.repeatTextBox.PasswordChar = '*';
      this.repeatTextBox.Size = new System.Drawing.Size(346, 20);
      this.repeatTextBox.TabIndex = 2;
      this.repeatTextBox.TextChanged += new System.EventHandler(this.repeatTextBox_TextChanged);
      // 
      // repeatLabel
      // 
      this.repeatLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.repeatLabel.AutoSize = true;
      this.repeatLabel.Location = new System.Drawing.Point(12, 106);
      this.repeatLabel.Name = "repeatLabel";
      this.repeatLabel.Size = new System.Drawing.Size(102, 13);
      this.repeatLabel.TabIndex = 5;
      this.repeatLabel.Text = "Repeat passphrase:";
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.Location = new System.Drawing.Point(286, 129);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(93, 24);
      this.okButton.TabIndex = 3;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(385, 129);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(93, 24);
      this.cancelButton.TabIndex = 4;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // encryptCheckBox
      // 
      this.encryptCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.encryptCheckBox.AutoSize = true;
      this.encryptCheckBox.Location = new System.Drawing.Point(15, 51);
      this.encryptCheckBox.Name = "encryptCheckBox";
      this.encryptCheckBox.Size = new System.Drawing.Size(133, 17);
      this.encryptCheckBox.TabIndex = 0;
      this.encryptCheckBox.Text = "Encrypt my private key";
      this.encryptCheckBox.UseVisualStyleBackColor = true;
      this.encryptCheckBox.CheckedChanged += new System.EventHandler(this.encryptCheckBox_CheckedChanged);
      // 
      // EncryptPrivateKeyDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(490, 165);
      this.ControlBox = false;
      this.Controls.Add(this.encryptCheckBox);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.repeatTextBox);
      this.Controls.Add(this.repeatLabel);
      this.Controls.Add(this.passphraseTextBox);
      this.Controls.Add(this.passphraseLabel);
      this.Controls.Add(this.infoLabel);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "EncryptPrivateKeyDialog";
      this.Text = "Pi-Vote Private Key Protection";
      this.Load += new System.EventHandler(this.EncryptPrivateKeyDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EncryptPrivateKeyDialog_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label infoLabel;
    private System.Windows.Forms.TextBox passphraseTextBox;
    private System.Windows.Forms.Label passphraseLabel;
    private System.Windows.Forms.TextBox repeatTextBox;
    private System.Windows.Forms.Label repeatLabel;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.CheckBox encryptCheckBox;
  }
}