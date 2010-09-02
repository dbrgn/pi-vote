namespace Pirate.PiVote.Client
{
  partial class VoteDescriptionForm
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
      this.textTextBox = new System.Windows.Forms.TextBox();
      this.descriptionTextBox = new System.Windows.Forms.TextBox();
      this.closeButton = new System.Windows.Forms.Button();
      this.urlPanel = new System.Windows.Forms.Panel();
      this.urlLink = new System.Windows.Forms.LinkLabel();
      this.urlPanel.SuspendLayout();
      this.SuspendLayout();
      // 
      // textTextBox
      // 
      this.textTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.textTextBox.Location = new System.Drawing.Point(12, 13);
      this.textTextBox.Name = "textTextBox";
      this.textTextBox.ReadOnly = true;
      this.textTextBox.Size = new System.Drawing.Size(505, 20);
      this.textTextBox.TabIndex = 0;
      // 
      // descriptionTextBox
      // 
      this.descriptionTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.descriptionTextBox.Location = new System.Drawing.Point(12, 41);
      this.descriptionTextBox.Multiline = true;
      this.descriptionTextBox.Name = "descriptionTextBox";
      this.descriptionTextBox.ReadOnly = true;
      this.descriptionTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
      this.descriptionTextBox.Size = new System.Drawing.Size(505, 284);
      this.descriptionTextBox.TabIndex = 1;
      // 
      // closeButton
      // 
      this.closeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.closeButton.Location = new System.Drawing.Point(430, 360);
      this.closeButton.Name = "closeButton";
      this.closeButton.Size = new System.Drawing.Size(87, 25);
      this.closeButton.TabIndex = 2;
      this.closeButton.Text = "&Close";
      this.closeButton.UseVisualStyleBackColor = true;
      this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
      // 
      // urlPanel
      // 
      this.urlPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
      this.urlPanel.Controls.Add(this.urlLink);
      this.urlPanel.Location = new System.Drawing.Point(12, 331);
      this.urlPanel.Name = "urlPanel";
      this.urlPanel.Size = new System.Drawing.Size(505, 23);
      this.urlPanel.TabIndex = 3;
      // 
      // urlLink
      // 
      this.urlLink.AutoSize = true;
      this.urlLink.Location = new System.Drawing.Point(2, 3);
      this.urlLink.Name = "urlLink";
      this.urlLink.Size = new System.Drawing.Size(20, 14);
      this.urlLink.TabIndex = 0;
      this.urlLink.TabStop = true;
      this.urlLink.Text = "Url";
      this.urlLink.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.urlLink_LinkClicked);
      // 
      // VoteDescriptionForm
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(529, 397);
      this.ControlBox = false;
      this.Controls.Add(this.urlPanel);
      this.Controls.Add(this.closeButton);
      this.Controls.Add(this.descriptionTextBox);
      this.Controls.Add(this.textTextBox);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "VoteDescriptionForm";
      this.Text = "Option Description";
      this.urlPanel.ResumeLayout(false);
      this.urlPanel.PerformLayout();
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.TextBox textTextBox;
    private System.Windows.Forms.TextBox descriptionTextBox;
    private System.Windows.Forms.Button closeButton;
    private System.Windows.Forms.Panel urlPanel;
    private System.Windows.Forms.LinkLabel urlLink;
  }
}