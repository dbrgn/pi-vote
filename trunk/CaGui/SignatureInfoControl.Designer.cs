namespace Pirate.PiVote.CaGui
{
  partial class SignatureInfoControl
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

    #region Component Designer generated code

    /// <summary> 
    /// Required method for Designer support - do not modify 
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
      this.caLabel = new System.Windows.Forms.Label();
      this.caIdLabel = new System.Windows.Forms.Label();
      this.caNameLabel = new System.Windows.Forms.Label();
      this.caNameTextBox = new System.Windows.Forms.TextBox();
      this.caIdTextBox = new System.Windows.Forms.TextBox();
      this.validFromLabel = new System.Windows.Forms.Label();
      this.validFromTextBox = new System.Windows.Forms.TextBox();
      this.validUntilLabel = new System.Windows.Forms.Label();
      this.validUntilTextBox = new System.Windows.Forms.TextBox();
      this.SuspendLayout();
      // 
      // caLabel
      // 
      this.caLabel.AutoSize = true;
      this.caLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.caLabel.Location = new System.Drawing.Point(0, 0);
      this.caLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.caLabel.Name = "caLabel";
      this.caLabel.Size = new System.Drawing.Size(178, 20);
      this.caLabel.TabIndex = 16;
      this.caLabel.Text = "Certificate Authority";
      // 
      // caIdLabel
      // 
      this.caIdLabel.AutoSize = true;
      this.caIdLabel.Location = new System.Drawing.Point(0, 34);
      this.caIdLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.caIdLabel.Name = "caIdLabel";
      this.caIdLabel.Size = new System.Drawing.Size(27, 20);
      this.caIdLabel.TabIndex = 15;
      this.caIdLabel.Text = "Id:";
      // 
      // caNameLabel
      // 
      this.caNameLabel.AutoSize = true;
      this.caNameLabel.Location = new System.Drawing.Point(-4, 74);
      this.caNameLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.caNameLabel.Name = "caNameLabel";
      this.caNameLabel.Size = new System.Drawing.Size(55, 20);
      this.caNameLabel.TabIndex = 14;
      this.caNameLabel.Text = "Name:";
      // 
      // caNameTextBox
      // 
      this.caNameTextBox.Location = new System.Drawing.Point(112, 69);
      this.caNameTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.caNameTextBox.Name = "caNameTextBox";
      this.caNameTextBox.ReadOnly = true;
      this.caNameTextBox.Size = new System.Drawing.Size(448, 26);
      this.caNameTextBox.TabIndex = 13;
      // 
      // caIdTextBox
      // 
      this.caIdTextBox.Location = new System.Drawing.Point(112, 29);
      this.caIdTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.caIdTextBox.Name = "caIdTextBox";
      this.caIdTextBox.ReadOnly = true;
      this.caIdTextBox.Size = new System.Drawing.Size(448, 26);
      this.caIdTextBox.TabIndex = 12;
      // 
      // validFromLabel
      // 
      this.validFromLabel.AutoSize = true;
      this.validFromLabel.Location = new System.Drawing.Point(-4, 114);
      this.validFromLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.validFromLabel.Name = "validFromLabel";
      this.validFromLabel.Size = new System.Drawing.Size(84, 20);
      this.validFromLabel.TabIndex = 18;
      this.validFromLabel.Text = "Valid from:";
      // 
      // validFromTextBox
      // 
      this.validFromTextBox.Location = new System.Drawing.Point(112, 109);
      this.validFromTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.validFromTextBox.Name = "validFromTextBox";
      this.validFromTextBox.ReadOnly = true;
      this.validFromTextBox.Size = new System.Drawing.Size(448, 26);
      this.validFromTextBox.TabIndex = 17;
      // 
      // validUntilLabel
      // 
      this.validUntilLabel.AutoSize = true;
      this.validUntilLabel.Location = new System.Drawing.Point(-4, 154);
      this.validUntilLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.validUntilLabel.Name = "validUntilLabel";
      this.validUntilLabel.Size = new System.Drawing.Size(81, 20);
      this.validUntilLabel.TabIndex = 20;
      this.validUntilLabel.Text = "Valid until:";
      // 
      // validUntilTextBox
      // 
      this.validUntilTextBox.Location = new System.Drawing.Point(112, 149);
      this.validUntilTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.validUntilTextBox.Name = "validUntilTextBox";
      this.validUntilTextBox.ReadOnly = true;
      this.validUntilTextBox.Size = new System.Drawing.Size(448, 26);
      this.validUntilTextBox.TabIndex = 19;
      // 
      // SignatureInfoControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.validUntilLabel);
      this.Controls.Add(this.validUntilTextBox);
      this.Controls.Add(this.validFromLabel);
      this.Controls.Add(this.validFromTextBox);
      this.Controls.Add(this.caLabel);
      this.Controls.Add(this.caIdLabel);
      this.Controls.Add(this.caNameLabel);
      this.Controls.Add(this.caNameTextBox);
      this.Controls.Add(this.caIdTextBox);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "SignatureInfoControl";
      this.Size = new System.Drawing.Size(562, 180);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label caLabel;
    private System.Windows.Forms.Label caIdLabel;
    private System.Windows.Forms.Label caNameLabel;
    private System.Windows.Forms.TextBox caNameTextBox;
    private System.Windows.Forms.TextBox caIdTextBox;
    private System.Windows.Forms.Label validFromLabel;
    private System.Windows.Forms.TextBox validFromTextBox;
    private System.Windows.Forms.Label validUntilLabel;
    private System.Windows.Forms.TextBox validUntilTextBox;
  }
}
