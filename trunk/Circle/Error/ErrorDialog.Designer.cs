namespace Pirate.PiVote.Circle.Error
{
  partial class ErrorDialog
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
      this.messageBox = new System.Windows.Forms.TextBox();
      this.infoLabel = new System.Windows.Forms.Label();
      this.iconBox = new System.Windows.Forms.PictureBox();
      this.detailBox = new System.Windows.Forms.TextBox();
      this.closeButton = new System.Windows.Forms.Button();
      ((System.ComponentModel.ISupportInitialize)(this.iconBox)).BeginInit();
      this.SuspendLayout();
      // 
      // messageBox
      // 
      this.messageBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.messageBox.Location = new System.Drawing.Point(12, 63);
      this.messageBox.Multiline = true;
      this.messageBox.Name = "messageBox";
      this.messageBox.ReadOnly = true;
      this.messageBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.messageBox.Size = new System.Drawing.Size(532, 46);
      this.messageBox.TabIndex = 0;
      // 
      // infoLabel
      // 
      this.infoLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.infoLabel.Location = new System.Drawing.Point(50, 9);
      this.infoLabel.Name = "infoLabel";
      this.infoLabel.Size = new System.Drawing.Size(494, 51);
      this.infoLabel.TabIndex = 1;
      this.infoLabel.Text = "Info";
      // 
      // iconBox
      // 
      this.iconBox.Location = new System.Drawing.Point(12, 9);
      this.iconBox.Name = "iconBox";
      this.iconBox.Size = new System.Drawing.Size(32, 34);
      this.iconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.iconBox.TabIndex = 6;
      this.iconBox.TabStop = false;
      // 
      // detailBox
      // 
      this.detailBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.detailBox.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.detailBox.Location = new System.Drawing.Point(12, 115);
      this.detailBox.Multiline = true;
      this.detailBox.Name = "detailBox";
      this.detailBox.ReadOnly = true;
      this.detailBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.detailBox.Size = new System.Drawing.Size(532, 263);
      this.detailBox.TabIndex = 7;
      // 
      // closeButton
      // 
      this.closeButton.Location = new System.Drawing.Point(432, 384);
      this.closeButton.Name = "closeButton";
      this.closeButton.Size = new System.Drawing.Size(112, 28);
      this.closeButton.TabIndex = 8;
      this.closeButton.Text = "&OK";
      this.closeButton.UseVisualStyleBackColor = true;
      this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
      // 
      // ErrorDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(556, 424);
      this.ControlBox = false;
      this.Controls.Add(this.closeButton);
      this.Controls.Add(this.detailBox);
      this.Controls.Add(this.iconBox);
      this.Controls.Add(this.infoLabel);
      this.Controls.Add(this.messageBox);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ErrorDialog";
      this.Text = "ErrorDialog";
      this.Load += new System.EventHandler(this.ErrorDialog_Load);
      ((System.ComponentModel.ISupportInitialize)(this.iconBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox messageBox;
    private System.Windows.Forms.Label infoLabel;
    private System.Windows.Forms.PictureBox iconBox;
    private System.Windows.Forms.TextBox detailBox;
    private System.Windows.Forms.Button closeButton;
  }
}