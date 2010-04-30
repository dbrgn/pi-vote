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
      this.descriptionButton = new System.Windows.Forms.Button();
      this.titleLabel = new System.Windows.Forms.Label();
      this.questionLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // descriptionButton
      // 
      this.descriptionButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.descriptionButton.Location = new System.Drawing.Point(660, 32);
      this.descriptionButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.descriptionButton.Name = "descriptionButton";
      this.descriptionButton.Size = new System.Drawing.Size(166, 37);
      this.descriptionButton.TabIndex = 2;
      this.descriptionButton.Text = "&Description";
      this.descriptionButton.UseVisualStyleBackColor = true;
      this.descriptionButton.Click += new System.EventHandler(this.descriptionButton_Click);
      // 
      // titleLabel
      // 
      this.titleLabel.AutoSize = true;
      this.titleLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.titleLabel.Location = new System.Drawing.Point(-4, 0);
      this.titleLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.titleLabel.Name = "titleLabel";
      this.titleLabel.Size = new System.Drawing.Size(54, 25);
      this.titleLabel.TabIndex = 3;
      this.titleLabel.Text = "Title";
      // 
      // questionLabel
      // 
      this.questionLabel.AutoSize = true;
      this.questionLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
      this.questionLabel.Location = new System.Drawing.Point(-4, 38);
      this.questionLabel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
      this.questionLabel.Name = "questionLabel";
      this.questionLabel.Size = new System.Drawing.Size(91, 25);
      this.questionLabel.TabIndex = 4;
      this.questionLabel.Text = "Question";
      // 
      // VoteControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.questionLabel);
      this.Controls.Add(this.titleLabel);
      this.Controls.Add(this.descriptionButton);
      this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
      this.Name = "VoteControl";
      this.Size = new System.Drawing.Size(827, 368);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Button descriptionButton;
    private System.Windows.Forms.Label titleLabel;
    private System.Windows.Forms.Label questionLabel;
  }
}
