namespace Pirate.PiVote.Client
{
  partial class Message
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
      this.messageLabel = new System.Windows.Forms.Label();
      this.iconBox = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.iconBox)).BeginInit();
      this.SuspendLayout();
      // 
      // messageLabel
      // 
      this.messageLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.messageLabel.Location = new System.Drawing.Point(70, 0);
      this.messageLabel.Name = "messageLabel";
      this.messageLabel.Size = new System.Drawing.Size(432, 79);
      this.messageLabel.TabIndex = 0;
      this.messageLabel.Text = "label1";
      // 
      // iconBox
      // 
      this.iconBox.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.iconBox.Location = new System.Drawing.Point(0, 0);
      this.iconBox.Name = "iconBox";
      this.iconBox.Size = new System.Drawing.Size(64, 69);
      this.iconBox.TabIndex = 1;
      this.iconBox.TabStop = false;
      // 
      // Message
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(144F, 144F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
      this.Controls.Add(this.iconBox);
      this.Controls.Add(this.messageLabel);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "Message";
      this.Size = new System.Drawing.Size(505, 81);
      ((System.ComponentModel.ISupportInitialize)(this.iconBox)).EndInit();
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Label messageLabel;
    private System.Windows.Forms.PictureBox iconBox;
  }
}
