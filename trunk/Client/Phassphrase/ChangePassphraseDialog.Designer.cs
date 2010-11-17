namespace Pirate.PiVote.Client
{
  partial class ChangePassphraseDialog
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
      this.passphraseTextBox = new System.Windows.Forms.TextBox();
      this.passphraseLabel = new System.Windows.Forms.Label();
      this.repeatTextBox = new System.Windows.Forms.TextBox();
      this.repeatLabel = new System.Windows.Forms.Label();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.encryptCheckBox = new System.Windows.Forms.CheckBox();
      this.oldPassphraseTextBox = new System.Windows.Forms.TextBox();
      this.oldPassphraseLabel = new System.Windows.Forms.Label();
      this.messageLabel = new System.Windows.Forms.Label();
      this.certificateIdLabel = new System.Windows.Forms.Label();
      this.certificateTypeLabel = new System.Windows.Forms.Label();
      this.certificateIdTextBox = new System.Windows.Forms.TextBox();
      this.certificateTypeTextBox = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // passphraseTextBox
      // 
      this.passphraseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.passphraseTextBox.Location = new System.Drawing.Point(161, 135);
      this.passphraseTextBox.Name = "passphraseTextBox";
      this.passphraseTextBox.PasswordChar = '*';
      this.passphraseTextBox.Size = new System.Drawing.Size(341, 20);
      this.passphraseTextBox.TabIndex = 1;
      this.passphraseTextBox.TextChanged += new System.EventHandler(this.passphraseTextBox_TextChanged);
      // 
      // passphraseLabel
      // 
      this.passphraseLabel.AutoSize = true;
      this.passphraseLabel.Location = new System.Drawing.Point(12, 138);
      this.passphraseLabel.Name = "passphraseLabel";
      this.passphraseLabel.Size = new System.Drawing.Size(94, 14);
      this.passphraseLabel.TabIndex = 3;
      this.passphraseLabel.Text = "New Passphrase:";
      // 
      // repeatTextBox
      // 
      this.repeatTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.repeatTextBox.Location = new System.Drawing.Point(161, 160);
      this.repeatTextBox.Name = "repeatTextBox";
      this.repeatTextBox.PasswordChar = '*';
      this.repeatTextBox.Size = new System.Drawing.Size(341, 20);
      this.repeatTextBox.TabIndex = 2;
      this.repeatTextBox.TextChanged += new System.EventHandler(this.repeatTextBox_TextChanged);
      // 
      // repeatLabel
      // 
      this.repeatLabel.AutoSize = true;
      this.repeatLabel.Location = new System.Drawing.Point(12, 163);
      this.repeatLabel.Name = "repeatLabel";
      this.repeatLabel.Size = new System.Drawing.Size(105, 14);
      this.repeatLabel.TabIndex = 5;
      this.repeatLabel.Text = "Repeat passphrase:";
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.Location = new System.Drawing.Point(310, 186);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(93, 26);
      this.okButton.TabIndex = 3;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(409, 186);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(93, 26);
      this.cancelButton.TabIndex = 4;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // encryptCheckBox
      // 
      this.encryptCheckBox.AutoSize = true;
      this.encryptCheckBox.Location = new System.Drawing.Point(14, 110);
      this.encryptCheckBox.Name = "encryptCheckBox";
      this.encryptCheckBox.Size = new System.Drawing.Size(136, 18);
      this.encryptCheckBox.TabIndex = 0;
      this.encryptCheckBox.Text = "Encrypt my private key";
      this.encryptCheckBox.UseVisualStyleBackColor = true;
      this.encryptCheckBox.CheckedChanged += new System.EventHandler(this.encryptCheckBox_CheckedChanged);
      // 
      // oldPassphraseTextBox
      // 
      this.oldPassphraseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.oldPassphraseTextBox.Location = new System.Drawing.Point(161, 87);
      this.oldPassphraseTextBox.Name = "oldPassphraseTextBox";
      this.oldPassphraseTextBox.PasswordChar = '*';
      this.oldPassphraseTextBox.Size = new System.Drawing.Size(341, 20);
      this.oldPassphraseTextBox.TabIndex = 6;
      this.oldPassphraseTextBox.TextChanged += new System.EventHandler(this.oldPassphraseTextBox_TextChanged);
      // 
      // oldPassphraseLabel
      // 
      this.oldPassphraseLabel.AutoSize = true;
      this.oldPassphraseLabel.Location = new System.Drawing.Point(11, 90);
      this.oldPassphraseLabel.Name = "oldPassphraseLabel";
      this.oldPassphraseLabel.Size = new System.Drawing.Size(87, 14);
      this.oldPassphraseLabel.TabIndex = 7;
      this.oldPassphraseLabel.Text = "Old Passphrase:";
      // 
      // messageLabel
      // 
      this.messageLabel.Location = new System.Drawing.Point(12, 66);
      this.messageLabel.Name = "messageLabel";
      this.messageLabel.Size = new System.Drawing.Size(490, 18);
      this.messageLabel.TabIndex = 8;
      this.messageLabel.Text = "Message";
      // 
      // certificateIdLabel
      // 
      this.certificateIdLabel.AutoSize = true;
      this.certificateIdLabel.Location = new System.Drawing.Point(12, 15);
      this.certificateIdLabel.Name = "certificateIdLabel";
      this.certificateIdLabel.Size = new System.Drawing.Size(70, 14);
      this.certificateIdLabel.TabIndex = 16;
      this.certificateIdLabel.Text = "Certificate Id:";
      // 
      // certificateTypeLabel
      // 
      this.certificateTypeLabel.AutoSize = true;
      this.certificateTypeLabel.Location = new System.Drawing.Point(11, 41);
      this.certificateTypeLabel.Name = "certificateTypeLabel";
      this.certificateTypeLabel.Size = new System.Drawing.Size(85, 14);
      this.certificateTypeLabel.TabIndex = 15;
      this.certificateTypeLabel.Text = "Certificate Type:";
      // 
      // certificateIdTextBox
      // 
      this.certificateIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.certificateIdTextBox.Location = new System.Drawing.Point(161, 12);
      this.certificateIdTextBox.Name = "certificateIdTextBox";
      this.certificateIdTextBox.ReadOnly = true;
      this.certificateIdTextBox.Size = new System.Drawing.Size(341, 20);
      this.certificateIdTextBox.TabIndex = 13;
      // 
      // certificateTypeTextBox
      // 
      this.certificateTypeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.certificateTypeTextBox.Location = new System.Drawing.Point(161, 38);
      this.certificateTypeTextBox.Name = "certificateTypeTextBox";
      this.certificateTypeTextBox.ReadOnly = true;
      this.certificateTypeTextBox.Size = new System.Drawing.Size(341, 20);
      this.certificateTypeTextBox.TabIndex = 12;
      // 
      // ChangePassphraseDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(512, 223);
      this.ControlBox = false;
      this.Controls.Add(this.certificateIdLabel);
      this.Controls.Add(this.certificateTypeLabel);
      this.Controls.Add(this.certificateIdTextBox);
      this.Controls.Add(this.certificateTypeTextBox);
      this.Controls.Add(this.messageLabel);
      this.Controls.Add(this.oldPassphraseTextBox);
      this.Controls.Add(this.oldPassphraseLabel);
      this.Controls.Add(this.encryptCheckBox);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.repeatTextBox);
      this.Controls.Add(this.repeatLabel);
      this.Controls.Add(this.passphraseTextBox);
      this.Controls.Add(this.passphraseLabel);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(2000, 250);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(500, 250);
      this.Name = "ChangePassphraseDialog";
      this.ShowInTaskbar = false;
      this.Text = "Pi-Vote Private Key Protection";
      this.Load += new System.EventHandler(this.ChangePassphraseDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.EncryptPrivateKeyDialog_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox passphraseTextBox;
    private System.Windows.Forms.Label passphraseLabel;
    private System.Windows.Forms.TextBox repeatTextBox;
    private System.Windows.Forms.Label repeatLabel;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.CheckBox encryptCheckBox;
    private System.Windows.Forms.TextBox oldPassphraseTextBox;
    private System.Windows.Forms.Label oldPassphraseLabel;
    private System.Windows.Forms.Label messageLabel;
    private System.Windows.Forms.Label certificateIdLabel;
    private System.Windows.Forms.Label certificateTypeLabel;
    private System.Windows.Forms.TextBox certificateIdTextBox;
    private System.Windows.Forms.TextBox certificateTypeTextBox;
  }
}