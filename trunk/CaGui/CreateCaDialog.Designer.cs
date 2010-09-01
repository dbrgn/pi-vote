namespace Pirate.PiVote.CaGui
{
  partial class CreateCaDialog
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
      this.rootCaLabel = new System.Windows.Forms.Label();
      this.rootCaCheckBox = new System.Windows.Forms.CheckBox();
      this.passphraseTextBox = new System.Windows.Forms.TextBox();
      this.passphraseLabel = new System.Windows.Forms.Label();
      this.repeatTextBox = new System.Windows.Forms.TextBox();
      this.repeatLabel = new System.Windows.Forms.Label();
      this.caNameTextBox = new System.Windows.Forms.TextBox();
      this.caNameLabel = new System.Windows.Forms.Label();
      this.label1 = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(368, 127);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 5;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.Enabled = false;
      this.okButton.Location = new System.Drawing.Point(287, 127);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 4;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // rootCaLabel
      // 
      this.rootCaLabel.AutoSize = true;
      this.rootCaLabel.Location = new System.Drawing.Point(12, 38);
      this.rootCaLabel.Name = "rootCaLabel";
      this.rootCaLabel.Size = new System.Drawing.Size(50, 14);
      this.rootCaLabel.TabIndex = 4;
      this.rootCaLabel.Text = "Root CA:";
      // 
      // rootCaCheckBox
      // 
      this.rootCaCheckBox.AutoSize = true;
      this.rootCaCheckBox.Location = new System.Drawing.Point(103, 38);
      this.rootCaCheckBox.Name = "rootCaCheckBox";
      this.rootCaCheckBox.Size = new System.Drawing.Size(15, 14);
      this.rootCaCheckBox.TabIndex = 1;
      this.rootCaCheckBox.UseVisualStyleBackColor = true;
      // 
      // passphraseTextBox
      // 
      this.passphraseTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.passphraseTextBox.Location = new System.Drawing.Point(103, 58);
      this.passphraseTextBox.Name = "passphraseTextBox";
      this.passphraseTextBox.PasswordChar = '*';
      this.passphraseTextBox.Size = new System.Drawing.Size(340, 20);
      this.passphraseTextBox.TabIndex = 2;
      this.passphraseTextBox.TextChanged += new System.EventHandler(this.passphraseTextBox_TextChanged);
      // 
      // passphraseLabel
      // 
      this.passphraseLabel.AutoSize = true;
      this.passphraseLabel.Location = new System.Drawing.Point(12, 61);
      this.passphraseLabel.Name = "passphraseLabel";
      this.passphraseLabel.Size = new System.Drawing.Size(68, 14);
      this.passphraseLabel.TabIndex = 6;
      this.passphraseLabel.Text = "Passphrase:";
      // 
      // repeatTextBox
      // 
      this.repeatTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.repeatTextBox.Location = new System.Drawing.Point(103, 84);
      this.repeatTextBox.Name = "repeatTextBox";
      this.repeatTextBox.PasswordChar = '*';
      this.repeatTextBox.Size = new System.Drawing.Size(340, 20);
      this.repeatTextBox.TabIndex = 3;
      this.repeatTextBox.TextChanged += new System.EventHandler(this.repeatTextBox_TextChanged);
      // 
      // repeatLabel
      // 
      this.repeatLabel.AutoSize = true;
      this.repeatLabel.Location = new System.Drawing.Point(12, 87);
      this.repeatLabel.Name = "repeatLabel";
      this.repeatLabel.Size = new System.Drawing.Size(44, 14);
      this.repeatLabel.TabIndex = 8;
      this.repeatLabel.Text = "Repeat:";
      // 
      // caNameTextBox
      // 
      this.caNameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.caNameTextBox.Location = new System.Drawing.Point(103, 12);
      this.caNameTextBox.Name = "caNameTextBox";
      this.caNameTextBox.Size = new System.Drawing.Size(340, 20);
      this.caNameTextBox.TabIndex = 0;
      this.caNameTextBox.TextChanged += new System.EventHandler(this.caNameTextBox_TextChanged);
      // 
      // caNameLabel
      // 
      this.caNameLabel.AutoSize = true;
      this.caNameLabel.Location = new System.Drawing.Point(12, 15);
      this.caNameLabel.Name = "caNameLabel";
      this.caNameLabel.Size = new System.Drawing.Size(54, 14);
      this.caNameLabel.TabIndex = 9;
      this.caNameLabel.Text = "CA Name:";
      // 
      // label1
      // 
      this.label1.AutoSize = true;
      this.label1.Location = new System.Drawing.Point(100, 107);
      this.label1.Name = "label1";
      this.label1.Size = new System.Drawing.Size(262, 14);
      this.label1.TabIndex = 10;
      this.label1.Text = "The passphrase must be at least 12 characters long.";
      // 
      // CreateCaDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(455, 162);
      this.ControlBox = false;
      this.Controls.Add(this.label1);
      this.Controls.Add(this.caNameTextBox);
      this.Controls.Add(this.caNameLabel);
      this.Controls.Add(this.repeatLabel);
      this.Controls.Add(this.repeatTextBox);
      this.Controls.Add(this.passphraseLabel);
      this.Controls.Add(this.passphraseTextBox);
      this.Controls.Add(this.rootCaLabel);
      this.Controls.Add(this.rootCaCheckBox);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.KeyPreview = true;
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(2000, 189);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(400, 189);
      this.Name = "CreateCaDialog";
      this.Text = "Create Certificate Authority";
      this.Load += new System.EventHandler(this.CaNameDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CreateCaDialog_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Label rootCaLabel;
    private System.Windows.Forms.CheckBox rootCaCheckBox;
    private System.Windows.Forms.TextBox passphraseTextBox;
    private System.Windows.Forms.Label passphraseLabel;
    private System.Windows.Forms.TextBox repeatTextBox;
    private System.Windows.Forms.Label repeatLabel;
    private System.Windows.Forms.TextBox caNameTextBox;
    private System.Windows.Forms.Label caNameLabel;
    private System.Windows.Forms.Label label1;
  }
}