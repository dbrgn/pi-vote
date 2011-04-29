namespace Pirate.PiVote.Prover
{
  partial class Master
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
      this.createLoadCertificateButton = new System.Windows.Forms.Button();
      this.createCertificateIdTextBox = new System.Windows.Forms.TextBox();
      this.createProofBox = new System.Windows.Forms.GroupBox();
      this.createSaveProofButon = new System.Windows.Forms.Button();
      this.createProofTextLabel = new System.Windows.Forms.Label();
      this.createProofTextTextBox = new System.Windows.Forms.TextBox();
      this.createCertificateIdLabel = new System.Windows.Forms.Label();
      this.verifyProofBox = new System.Windows.Forms.GroupBox();
      this.verifyLoadProofButton = new System.Windows.Forms.Button();
      this.verifyProofTextLabel = new System.Windows.Forms.Label();
      this.verifyProofTextTextBox = new System.Windows.Forms.TextBox();
      this.verifiyCertificateIdLabel = new System.Windows.Forms.Label();
      this.verifyCertificateIdTextBox = new System.Windows.Forms.TextBox();
      this.verifyCertificateFingerprintTextBox = new System.Windows.Forms.TextBox();
      this.verifyCertificateFingerprintLabel = new System.Windows.Forms.Label();
      this.createProofBox.SuspendLayout();
      this.verifyProofBox.SuspendLayout();
      this.SuspendLayout();
      // 
      // createLoadCertificateButton
      // 
      this.createLoadCertificateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.createLoadCertificateButton.Location = new System.Drawing.Point(564, 19);
      this.createLoadCertificateButton.Name = "createLoadCertificateButton";
      this.createLoadCertificateButton.Size = new System.Drawing.Size(102, 20);
      this.createLoadCertificateButton.TabIndex = 0;
      this.createLoadCertificateButton.Text = "&Load";
      this.createLoadCertificateButton.UseVisualStyleBackColor = true;
      this.createLoadCertificateButton.Click += new System.EventHandler(this.createLoadCertificateButton_Click);
      // 
      // createCertificateIdTextBox
      // 
      this.createCertificateIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.createCertificateIdTextBox.Location = new System.Drawing.Point(121, 19);
      this.createCertificateIdTextBox.Name = "createCertificateIdTextBox";
      this.createCertificateIdTextBox.ReadOnly = true;
      this.createCertificateIdTextBox.Size = new System.Drawing.Size(437, 20);
      this.createCertificateIdTextBox.TabIndex = 1;
      // 
      // createProofBox
      // 
      this.createProofBox.Controls.Add(this.createSaveProofButon);
      this.createProofBox.Controls.Add(this.createProofTextLabel);
      this.createProofBox.Controls.Add(this.createProofTextTextBox);
      this.createProofBox.Controls.Add(this.createCertificateIdLabel);
      this.createProofBox.Controls.Add(this.createLoadCertificateButton);
      this.createProofBox.Controls.Add(this.createCertificateIdTextBox);
      this.createProofBox.Location = new System.Drawing.Point(12, 12);
      this.createProofBox.Name = "createProofBox";
      this.createProofBox.Size = new System.Drawing.Size(672, 163);
      this.createProofBox.TabIndex = 3;
      this.createProofBox.TabStop = false;
      this.createProofBox.Text = "Create Proof";
      // 
      // createSaveProofButon
      // 
      this.createSaveProofButon.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.createSaveProofButon.Enabled = false;
      this.createSaveProofButon.Location = new System.Drawing.Point(518, 131);
      this.createSaveProofButon.Name = "createSaveProofButon";
      this.createSaveProofButon.Size = new System.Drawing.Size(148, 26);
      this.createSaveProofButon.TabIndex = 5;
      this.createSaveProofButon.Text = "&Save Proof";
      this.createSaveProofButon.UseVisualStyleBackColor = true;
      this.createSaveProofButon.Click += new System.EventHandler(this.createSaveProofButon_Click);
      // 
      // createProofTextLabel
      // 
      this.createProofTextLabel.AutoSize = true;
      this.createProofTextLabel.Location = new System.Drawing.Point(6, 48);
      this.createProofTextLabel.Name = "createProofTextLabel";
      this.createProofTextLabel.Size = new System.Drawing.Size(55, 13);
      this.createProofTextLabel.TabIndex = 4;
      this.createProofTextLabel.Text = "Proof text:";
      // 
      // createProofTextTextBox
      // 
      this.createProofTextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.createProofTextTextBox.Location = new System.Drawing.Point(121, 45);
      this.createProofTextTextBox.Multiline = true;
      this.createProofTextTextBox.Name = "createProofTextTextBox";
      this.createProofTextTextBox.Size = new System.Drawing.Size(545, 80);
      this.createProofTextTextBox.TabIndex = 3;
      this.createProofTextTextBox.TextChanged += new System.EventHandler(this.createProofTextTextBox_TextChanged);
      // 
      // createCertificateIdLabel
      // 
      this.createCertificateIdLabel.AutoSize = true;
      this.createCertificateIdLabel.Location = new System.Drawing.Point(6, 22);
      this.createCertificateIdLabel.Name = "createCertificateIdLabel";
      this.createCertificateIdLabel.Size = new System.Drawing.Size(68, 13);
      this.createCertificateIdLabel.TabIndex = 2;
      this.createCertificateIdLabel.Text = "Certificate id:";
      // 
      // verifyProofBox
      // 
      this.verifyProofBox.Controls.Add(this.verifyCertificateFingerprintLabel);
      this.verifyProofBox.Controls.Add(this.verifyCertificateFingerprintTextBox);
      this.verifyProofBox.Controls.Add(this.verifyLoadProofButton);
      this.verifyProofBox.Controls.Add(this.verifyProofTextLabel);
      this.verifyProofBox.Controls.Add(this.verifyProofTextTextBox);
      this.verifyProofBox.Controls.Add(this.verifiyCertificateIdLabel);
      this.verifyProofBox.Controls.Add(this.verifyCertificateIdTextBox);
      this.verifyProofBox.Location = new System.Drawing.Point(12, 181);
      this.verifyProofBox.Name = "verifyProofBox";
      this.verifyProofBox.Size = new System.Drawing.Size(672, 212);
      this.verifyProofBox.TabIndex = 4;
      this.verifyProofBox.TabStop = false;
      this.verifyProofBox.Text = "Verify Proof";
      // 
      // verifyLoadProofButton
      // 
      this.verifyLoadProofButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.verifyLoadProofButton.Location = new System.Drawing.Point(518, 19);
      this.verifyLoadProofButton.Name = "verifyLoadProofButton";
      this.verifyLoadProofButton.Size = new System.Drawing.Size(148, 26);
      this.verifyLoadProofButton.TabIndex = 5;
      this.verifyLoadProofButton.Text = "&Load Proof";
      this.verifyLoadProofButton.UseVisualStyleBackColor = true;
      this.verifyLoadProofButton.Click += new System.EventHandler(this.verifyLoadProofButton_Click);
      // 
      // verifyProofTextLabel
      // 
      this.verifyProofTextLabel.AutoSize = true;
      this.verifyProofTextLabel.Location = new System.Drawing.Point(6, 123);
      this.verifyProofTextLabel.Name = "verifyProofTextLabel";
      this.verifyProofTextLabel.Size = new System.Drawing.Size(55, 13);
      this.verifyProofTextLabel.TabIndex = 4;
      this.verifyProofTextLabel.Text = "Proof text:";
      // 
      // verifyProofTextTextBox
      // 
      this.verifyProofTextTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.verifyProofTextTextBox.Location = new System.Drawing.Point(121, 120);
      this.verifyProofTextTextBox.Multiline = true;
      this.verifyProofTextTextBox.Name = "verifyProofTextTextBox";
      this.verifyProofTextTextBox.ReadOnly = true;
      this.verifyProofTextTextBox.Size = new System.Drawing.Size(545, 80);
      this.verifyProofTextTextBox.TabIndex = 3;
      // 
      // verifiyCertificateIdLabel
      // 
      this.verifiyCertificateIdLabel.AutoSize = true;
      this.verifiyCertificateIdLabel.Location = new System.Drawing.Point(6, 54);
      this.verifiyCertificateIdLabel.Name = "verifiyCertificateIdLabel";
      this.verifiyCertificateIdLabel.Size = new System.Drawing.Size(68, 13);
      this.verifiyCertificateIdLabel.TabIndex = 2;
      this.verifiyCertificateIdLabel.Text = "Certificate id:";
      // 
      // verifyCertificateIdTextBox
      // 
      this.verifyCertificateIdTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.verifyCertificateIdTextBox.Location = new System.Drawing.Point(121, 51);
      this.verifyCertificateIdTextBox.Name = "verifyCertificateIdTextBox";
      this.verifyCertificateIdTextBox.ReadOnly = true;
      this.verifyCertificateIdTextBox.Size = new System.Drawing.Size(545, 20);
      this.verifyCertificateIdTextBox.TabIndex = 1;
      // 
      // verifyCertificateFingerprintTextBox
      // 
      this.verifyCertificateFingerprintTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.verifyCertificateFingerprintTextBox.Location = new System.Drawing.Point(121, 77);
      this.verifyCertificateFingerprintTextBox.Multiline = true;
      this.verifyCertificateFingerprintTextBox.Name = "verifyCertificateFingerprintTextBox";
      this.verifyCertificateFingerprintTextBox.ReadOnly = true;
      this.verifyCertificateFingerprintTextBox.Size = new System.Drawing.Size(545, 37);
      this.verifyCertificateFingerprintTextBox.TabIndex = 6;
      // 
      // verifyCertificateFingerprintLabel
      // 
      this.verifyCertificateFingerprintLabel.AutoSize = true;
      this.verifyCertificateFingerprintLabel.Location = new System.Drawing.Point(6, 80);
      this.verifyCertificateFingerprintLabel.Name = "verifyCertificateFingerprintLabel";
      this.verifyCertificateFingerprintLabel.Size = new System.Drawing.Size(106, 13);
      this.verifyCertificateFingerprintLabel.TabIndex = 7;
      this.verifyCertificateFingerprintLabel.Text = "Certificate fingerprint:";
      // 
      // Master
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(696, 402);
      this.Controls.Add(this.verifyProofBox);
      this.Controls.Add(this.createProofBox);
      this.Name = "Master";
      this.Text = "Certificate Proover";
      this.createProofBox.ResumeLayout(false);
      this.createProofBox.PerformLayout();
      this.verifyProofBox.ResumeLayout(false);
      this.verifyProofBox.PerformLayout();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button createLoadCertificateButton;
    private System.Windows.Forms.TextBox createCertificateIdTextBox;
    private System.Windows.Forms.GroupBox createProofBox;
    private System.Windows.Forms.Button createSaveProofButon;
    private System.Windows.Forms.Label createProofTextLabel;
    private System.Windows.Forms.TextBox createProofTextTextBox;
    private System.Windows.Forms.Label createCertificateIdLabel;
    private System.Windows.Forms.GroupBox verifyProofBox;
    private System.Windows.Forms.Button verifyLoadProofButton;
    private System.Windows.Forms.Label verifyProofTextLabel;
    private System.Windows.Forms.TextBox verifyProofTextTextBox;
    private System.Windows.Forms.Label verifiyCertificateIdLabel;
    private System.Windows.Forms.TextBox verifyCertificateIdTextBox;
    private System.Windows.Forms.Label verifyCertificateFingerprintLabel;
    private System.Windows.Forms.TextBox verifyCertificateFingerprintTextBox;
  }
}

