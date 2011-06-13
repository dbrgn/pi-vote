namespace Pirate.PiVote.Gui
{
  partial class GenerateSignCheckDialog
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
      this.qrCodeImage = new System.Windows.Forms.PictureBox();
      this.browseLink = new System.Windows.Forms.LinkLabel();
      this.infoLabel = new System.Windows.Forms.Label();
      this.closeButton = new System.Windows.Forms.Button();
      this.secretLabel = new System.Windows.Forms.Label();
      ((System.ComponentModel.ISupportInitialize)(this.qrCodeImage)).BeginInit();
      this.SuspendLayout();
      // 
      // qrCodeImage
      // 
      this.qrCodeImage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.qrCodeImage.Location = new System.Drawing.Point(12, 114);
      this.qrCodeImage.Name = "qrCodeImage";
      this.qrCodeImage.Size = new System.Drawing.Size(468, 284);
      this.qrCodeImage.TabIndex = 0;
      this.qrCodeImage.TabStop = false;
      // 
      // browseLink
      // 
      this.browseLink.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.browseLink.Location = new System.Drawing.Point(9, 401);
      this.browseLink.Name = "browseLink";
      this.browseLink.Size = new System.Drawing.Size(471, 27);
      this.browseLink.TabIndex = 1;
      this.browseLink.TabStop = true;
      this.browseLink.Text = "browseLink";
      this.browseLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.browseLink_LinkClicked);
      // 
      // infoLabel
      // 
      this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.infoLabel.Location = new System.Drawing.Point(9, 10);
      this.infoLabel.Name = "infoLabel";
      this.infoLabel.Size = new System.Drawing.Size(471, 82);
      this.infoLabel.TabIndex = 2;
      this.infoLabel.Text = "label1";
      // 
      // closeButton
      // 
      this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.closeButton.Location = new System.Drawing.Point(391, 431);
      this.closeButton.Name = "closeButton";
      this.closeButton.Size = new System.Drawing.Size(89, 29);
      this.closeButton.TabIndex = 3;
      this.closeButton.Text = "&OK";
      this.closeButton.UseVisualStyleBackColor = true;
      this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
      // 
      // secretLabel
      // 
      this.secretLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.secretLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.secretLabel.ForeColor = System.Drawing.Color.Red;
      this.secretLabel.Location = new System.Drawing.Point(9, 92);
      this.secretLabel.Name = "secretLabel";
      this.secretLabel.Size = new System.Drawing.Size(471, 19);
      this.secretLabel.TabIndex = 4;
      this.secretLabel.Text = "label1";
      // 
      // GenerateSignCheckDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(492, 473);
      this.ControlBox = false;
      this.Controls.Add(this.secretLabel);
      this.Controls.Add(this.closeButton);
      this.Controls.Add(this.infoLabel);
      this.Controls.Add(this.browseLink);
      this.Controls.Add(this.qrCodeImage);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "GenerateSignCheckDialog";
      this.Text = "GenerateSignCheckDialog";
      this.Load += new System.EventHandler(this.GenerateSignCheckDialog_Load);
      ((System.ComponentModel.ISupportInitialize)(this.qrCodeImage)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.PictureBox qrCodeImage;
    private System.Windows.Forms.LinkLabel browseLink;
    private System.Windows.Forms.Label infoLabel;
    private System.Windows.Forms.Button closeButton;
    private System.Windows.Forms.Label secretLabel;
  }
}