namespace Pirate.PiVote.Client
{
  partial class VoteControl
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
      this.containerPanel = new System.Windows.Forms.Panel();
      this.descriptionButton = new System.Windows.Forms.Button();
      this.titleLabel = new System.Windows.Forms.Label();
      this.containerPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // containerPanel
      // 
      this.containerPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.containerPanel.AutoScroll = true;
      this.containerPanel.Controls.Add(this.descriptionButton);
      this.containerPanel.Controls.Add(this.titleLabel);
      this.containerPanel.Font = new System.Drawing.Font("Arial", 8.25F);
      this.containerPanel.Location = new System.Drawing.Point(0, 0);
      this.containerPanel.Margin = new System.Windows.Forms.Padding(5);
      this.containerPanel.Name = "containerPanel";
      this.containerPanel.Size = new System.Drawing.Size(597, 413);
      this.containerPanel.TabIndex = 4;
      // 
      // descriptionButton
      // 
      this.descriptionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.descriptionButton.Location = new System.Drawing.Point(486, 0);
      this.descriptionButton.Name = "descriptionButton";
      this.descriptionButton.Size = new System.Drawing.Size(111, 26);
      this.descriptionButton.TabIndex = 2;
      this.descriptionButton.Text = "&Description";
      this.descriptionButton.UseVisualStyleBackColor = true;
      this.descriptionButton.Click += new System.EventHandler(this.descriptionButton_Click);
      // 
      // titleLabel
      // 
      this.titleLabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.titleLabel.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
      this.titleLabel.Location = new System.Drawing.Point(3, 4);
      this.titleLabel.Name = "titleLabel";
      this.titleLabel.Size = new System.Drawing.Size(477, 16);
      this.titleLabel.TabIndex = 3;
      this.titleLabel.Text = "Title";
      // 
      // VoteControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.containerPanel);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "VoteControl";
      this.Size = new System.Drawing.Size(597, 413);
      this.containerPanel.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.Button descriptionButton;
    private System.Windows.Forms.Label titleLabel;
    private System.Windows.Forms.Panel containerPanel;
  }
}
