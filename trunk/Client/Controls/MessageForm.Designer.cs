namespace Pirate.PiVote.Client
{
  partial class MessageForm
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
      this.infoBox = new System.Windows.Forms.TextBox();
      this.rightButton = new System.Windows.Forms.Button();
      this.middleButton = new System.Windows.Forms.Button();
      this.leftbutton = new System.Windows.Forms.Button();
      this.iconBox = new System.Windows.Forms.PictureBox();
      ((System.ComponentModel.ISupportInitialize)(this.iconBox)).BeginInit();
      this.SuspendLayout();
      // 
      // infoBox
      // 
      this.infoBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.infoBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
      this.infoBox.Location = new System.Drawing.Point(50, 13);
      this.infoBox.Multiline = true;
      this.infoBox.Name = "infoBox";
      this.infoBox.ReadOnly = true;
      this.infoBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.infoBox.Size = new System.Drawing.Size(430, 126);
      this.infoBox.TabIndex = 1;
      // 
      // rightButton
      // 
      this.rightButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.rightButton.Location = new System.Drawing.Point(389, 145);
      this.rightButton.Name = "rightButton";
      this.rightButton.Size = new System.Drawing.Size(91, 28);
      this.rightButton.TabIndex = 2;
      this.rightButton.Text = "button1";
      this.rightButton.UseVisualStyleBackColor = true;
      this.rightButton.Click += new System.EventHandler(this.rightButton_Click);
      // 
      // middleButton
      // 
      this.middleButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.middleButton.Location = new System.Drawing.Point(292, 145);
      this.middleButton.Name = "middleButton";
      this.middleButton.Size = new System.Drawing.Size(91, 28);
      this.middleButton.TabIndex = 3;
      this.middleButton.Text = "button2";
      this.middleButton.UseVisualStyleBackColor = true;
      this.middleButton.Click += new System.EventHandler(this.middleButton_Click);
      // 
      // leftbutton
      // 
      this.leftbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.leftbutton.Location = new System.Drawing.Point(195, 145);
      this.leftbutton.Name = "leftbutton";
      this.leftbutton.Size = new System.Drawing.Size(91, 28);
      this.leftbutton.TabIndex = 4;
      this.leftbutton.Text = "button3";
      this.leftbutton.UseVisualStyleBackColor = true;
      this.leftbutton.Click += new System.EventHandler(this.leftbutton_Click);
      // 
      // iconBox
      // 
      this.iconBox.Location = new System.Drawing.Point(12, 13);
      this.iconBox.Name = "iconBox";
      this.iconBox.Size = new System.Drawing.Size(32, 34);
      this.iconBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
      this.iconBox.TabIndex = 5;
      this.iconBox.TabStop = false;
      // 
      // MessageForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(492, 186);
      this.ControlBox = false;
      this.Controls.Add(this.iconBox);
      this.Controls.Add(this.leftbutton);
      this.Controls.Add(this.middleButton);
      this.Controls.Add(this.rightButton);
      this.Controls.Add(this.infoBox);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.MaximizeBox = false;
      this.MaximumSize = new System.Drawing.Size(800, 429);
      this.MinimizeBox = false;
      this.MinimumSize = new System.Drawing.Size(500, 213);
      this.Name = "MessageForm";
      this.Text = "MessageForm";
      this.Load += new System.EventHandler(this.MessageForm_Load);
      ((System.ComponentModel.ISupportInitialize)(this.iconBox)).EndInit();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox infoBox;
    private System.Windows.Forms.Button rightButton;
    private System.Windows.Forms.Button middleButton;
    private System.Windows.Forms.Button leftbutton;
    private System.Windows.Forms.PictureBox iconBox;

  }
}