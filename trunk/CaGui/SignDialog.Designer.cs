namespace Pirate.PiVote.CaGui
{
  partial class SignDialog
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
      this.validUntilLabel = new System.Windows.Forms.Label();
      this.cancelButton = new System.Windows.Forms.Button();
      this.okButton = new System.Windows.Forms.Button();
      this.idTextBox = new System.Windows.Forms.TextBox();
      this.nameTextBox = new System.Windows.Forms.TextBox();
      this.nameLabel = new System.Windows.Forms.Label();
      this.idLabel = new System.Windows.Forms.Label();
      this.validUntilPicker = new System.Windows.Forms.DateTimePicker();
      this.typeLabel = new System.Windows.Forms.Label();
      this.typeTextBox = new System.Windows.Forms.TextBox();
      this.emailAddressLabel = new System.Windows.Forms.Label();
      this.emailAddressTextBox = new System.Windows.Forms.TextBox();
      this.cantonlabel = new System.Windows.Forms.Label();
      this.cantonTextBox = new System.Windows.Forms.TextBox();
      this.fingerprintLabel = new System.Windows.Forms.Label();
      this.fingerprintTextBox = new System.Windows.Forms.TextBox();
      this.acceptSignRadioButton = new System.Windows.Forms.RadioButton();
      this.reasonComboBox = new System.Windows.Forms.ComboBox();
      this.reasonLabel = new System.Windows.Forms.Label();
      this.refuseRadioButton = new System.Windows.Forms.RadioButton();
      this.SuspendLayout();
      // 
      // validUntilLabel
      // 
      this.validUntilLabel.AutoSize = true;
      this.validUntilLabel.Location = new System.Drawing.Point(16, 354);
      this.validUntilLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.validUntilLabel.Name = "validUntilLabel";
      this.validUntilLabel.Size = new System.Drawing.Size(83, 19);
      this.validUntilLabel.TabIndex = 0;
      this.validUntilLabel.Text = "Valid until:";
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(694, 483);
      this.cancelButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(112, 34);
      this.cancelButton.TabIndex = 2;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.Enabled = false;
      this.okButton.Location = new System.Drawing.Point(573, 483);
      this.okButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(112, 34);
      this.okButton.TabIndex = 1;
      this.okButton.Text = "&OK";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // idTextBox
      // 
      this.idTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.idTextBox.Location = new System.Drawing.Point(141, 18);
      this.idTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.idTextBox.Name = "idTextBox";
      this.idTextBox.ReadOnly = true;
      this.idTextBox.Size = new System.Drawing.Size(664, 26);
      this.idTextBox.TabIndex = 3;
      // 
      // nameTextBox
      // 
      this.nameTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.nameTextBox.Location = new System.Drawing.Point(141, 98);
      this.nameTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.nameTextBox.Name = "nameTextBox";
      this.nameTextBox.ReadOnly = true;
      this.nameTextBox.Size = new System.Drawing.Size(664, 26);
      this.nameTextBox.TabIndex = 4;
      // 
      // nameLabel
      // 
      this.nameLabel.AutoSize = true;
      this.nameLabel.Location = new System.Drawing.Point(16, 104);
      this.nameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.nameLabel.Name = "nameLabel";
      this.nameLabel.Size = new System.Drawing.Size(56, 19);
      this.nameLabel.TabIndex = 5;
      this.nameLabel.Text = "Name:";
      // 
      // idLabel
      // 
      this.idLabel.AutoSize = true;
      this.idLabel.Location = new System.Drawing.Point(18, 22);
      this.idLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.idLabel.Name = "idLabel";
      this.idLabel.Size = new System.Drawing.Size(28, 19);
      this.idLabel.TabIndex = 6;
      this.idLabel.Text = "Id:";
      // 
      // validUntilPicker
      // 
      this.validUntilPicker.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.validUntilPicker.Enabled = false;
      this.validUntilPicker.Location = new System.Drawing.Point(141, 346);
      this.validUntilPicker.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.validUntilPicker.Name = "validUntilPicker";
      this.validUntilPicker.Size = new System.Drawing.Size(664, 26);
      this.validUntilPicker.TabIndex = 7;
      this.validUntilPicker.ValueChanged += new System.EventHandler(this.validUntilPicker_ValueChanged);
      // 
      // typeLabel
      // 
      this.typeLabel.AutoSize = true;
      this.typeLabel.Location = new System.Drawing.Point(16, 63);
      this.typeLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.typeLabel.Name = "typeLabel";
      this.typeLabel.Size = new System.Drawing.Size(49, 19);
      this.typeLabel.TabIndex = 9;
      this.typeLabel.Text = "Type:";
      // 
      // typeTextBox
      // 
      this.typeTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.typeTextBox.Location = new System.Drawing.Point(141, 58);
      this.typeTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.typeTextBox.Name = "typeTextBox";
      this.typeTextBox.ReadOnly = true;
      this.typeTextBox.Size = new System.Drawing.Size(664, 26);
      this.typeTextBox.TabIndex = 8;
      // 
      // emailAddressLabel
      // 
      this.emailAddressLabel.AutoSize = true;
      this.emailAddressLabel.Location = new System.Drawing.Point(16, 142);
      this.emailAddressLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.emailAddressLabel.Name = "emailAddressLabel";
      this.emailAddressLabel.Size = new System.Drawing.Size(117, 19);
      this.emailAddressLabel.TabIndex = 11;
      this.emailAddressLabel.Text = "Email address:";
      // 
      // emailAddressTextBox
      // 
      this.emailAddressTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.emailAddressTextBox.Location = new System.Drawing.Point(141, 136);
      this.emailAddressTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.emailAddressTextBox.Name = "emailAddressTextBox";
      this.emailAddressTextBox.ReadOnly = true;
      this.emailAddressTextBox.Size = new System.Drawing.Size(664, 26);
      this.emailAddressTextBox.TabIndex = 10;
      // 
      // cantonlabel
      // 
      this.cantonlabel.AutoSize = true;
      this.cantonlabel.Location = new System.Drawing.Point(16, 182);
      this.cantonlabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.cantonlabel.Name = "cantonlabel";
      this.cantonlabel.Size = new System.Drawing.Size(66, 19);
      this.cantonlabel.TabIndex = 13;
      this.cantonlabel.Text = "Canton:";
      // 
      // cantonTextBox
      // 
      this.cantonTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.cantonTextBox.Location = new System.Drawing.Point(141, 176);
      this.cantonTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.cantonTextBox.Name = "cantonTextBox";
      this.cantonTextBox.ReadOnly = true;
      this.cantonTextBox.Size = new System.Drawing.Size(664, 26);
      this.cantonTextBox.TabIndex = 12;
      // 
      // fingerprintLabel
      // 
      this.fingerprintLabel.AutoSize = true;
      this.fingerprintLabel.Location = new System.Drawing.Point(16, 220);
      this.fingerprintLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.fingerprintLabel.Name = "fingerprintLabel";
      this.fingerprintLabel.Size = new System.Drawing.Size(93, 19);
      this.fingerprintLabel.TabIndex = 15;
      this.fingerprintLabel.Text = "Fingerprint:";
      // 
      // fingerprintTextBox
      // 
      this.fingerprintTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.fingerprintTextBox.Location = new System.Drawing.Point(141, 214);
      this.fingerprintTextBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.fingerprintTextBox.Multiline = true;
      this.fingerprintTextBox.Name = "fingerprintTextBox";
      this.fingerprintTextBox.ReadOnly = true;
      this.fingerprintTextBox.Size = new System.Drawing.Size(664, 72);
      this.fingerprintTextBox.TabIndex = 14;
      // 
      // acceptSignRadioButton
      // 
      this.acceptSignRadioButton.AutoSize = true;
      this.acceptSignRadioButton.Location = new System.Drawing.Point(21, 310);
      this.acceptSignRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.acceptSignRadioButton.Name = "acceptSignRadioButton";
      this.acceptSignRadioButton.Size = new System.Drawing.Size(154, 23);
      this.acceptSignRadioButton.TabIndex = 16;
      this.acceptSignRadioButton.TabStop = true;
      this.acceptSignRadioButton.Text = "Accept and Sign";
      this.acceptSignRadioButton.UseVisualStyleBackColor = true;
      this.acceptSignRadioButton.CheckedChanged += new System.EventHandler(this.acceptSignRadioButton_CheckedChanged);
      // 
      // reasonComboBox
      // 
      this.reasonComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.reasonComboBox.Enabled = false;
      this.reasonComboBox.FormattingEnabled = true;
      this.reasonComboBox.Location = new System.Drawing.Point(141, 435);
      this.reasonComboBox.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.reasonComboBox.Name = "reasonComboBox";
      this.reasonComboBox.Size = new System.Drawing.Size(664, 27);
      this.reasonComboBox.TabIndex = 17;
      this.reasonComboBox.SelectedIndexChanged += new System.EventHandler(this.reasonComboBox_SelectedIndexChanged);
      // 
      // reasonLabel
      // 
      this.reasonLabel.AutoSize = true;
      this.reasonLabel.Location = new System.Drawing.Point(16, 440);
      this.reasonLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.reasonLabel.Name = "reasonLabel";
      this.reasonLabel.Size = new System.Drawing.Size(69, 19);
      this.reasonLabel.TabIndex = 18;
      this.reasonLabel.Text = "Reason:";
      // 
      // refuseRadioButton
      // 
      this.refuseRadioButton.AutoSize = true;
      this.refuseRadioButton.Location = new System.Drawing.Point(18, 399);
      this.refuseRadioButton.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.refuseRadioButton.Name = "refuseRadioButton";
      this.refuseRadioButton.Size = new System.Drawing.Size(84, 23);
      this.refuseRadioButton.TabIndex = 19;
      this.refuseRadioButton.TabStop = true;
      this.refuseRadioButton.Text = "Refuse";
      this.refuseRadioButton.UseVisualStyleBackColor = true;
      this.refuseRadioButton.CheckedChanged += new System.EventHandler(this.refuseRadioButton_CheckedChanged);
      // 
      // SignDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.ClientSize = new System.Drawing.Size(825, 536);
      this.ControlBox = false;
      this.Controls.Add(this.refuseRadioButton);
      this.Controls.Add(this.reasonLabel);
      this.Controls.Add(this.reasonComboBox);
      this.Controls.Add(this.acceptSignRadioButton);
      this.Controls.Add(this.fingerprintLabel);
      this.Controls.Add(this.fingerprintTextBox);
      this.Controls.Add(this.cantonlabel);
      this.Controls.Add(this.cantonTextBox);
      this.Controls.Add(this.emailAddressLabel);
      this.Controls.Add(this.emailAddressTextBox);
      this.Controls.Add(this.typeLabel);
      this.Controls.Add(this.typeTextBox);
      this.Controls.Add(this.validUntilPicker);
      this.Controls.Add(this.idLabel);
      this.Controls.Add(this.nameLabel);
      this.Controls.Add(this.nameTextBox);
      this.Controls.Add(this.idTextBox);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.validUntilLabel);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.KeyPreview = true;
      this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "SignDialog";
      this.Text = "Verify Signature Request";
      this.Load += new System.EventHandler(this.CaNameDialog_Load);
      this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.RefuseDialog_KeyDown);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label validUntilLabel;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.TextBox idTextBox;
    private System.Windows.Forms.TextBox nameTextBox;
    private System.Windows.Forms.Label nameLabel;
    private System.Windows.Forms.Label idLabel;
    private System.Windows.Forms.DateTimePicker validUntilPicker;
    private System.Windows.Forms.Label typeLabel;
    private System.Windows.Forms.TextBox typeTextBox;
    private System.Windows.Forms.Label emailAddressLabel;
    private System.Windows.Forms.TextBox emailAddressTextBox;
    private System.Windows.Forms.Label cantonlabel;
    private System.Windows.Forms.TextBox cantonTextBox;
    private System.Windows.Forms.Label fingerprintLabel;
    private System.Windows.Forms.TextBox fingerprintTextBox;
    private System.Windows.Forms.RadioButton acceptSignRadioButton;
    private System.Windows.Forms.ComboBox reasonComboBox;
    private System.Windows.Forms.Label reasonLabel;
    private System.Windows.Forms.RadioButton refuseRadioButton;
  }
}