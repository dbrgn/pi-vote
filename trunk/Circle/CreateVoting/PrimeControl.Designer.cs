namespace Pirate.PiVote.Circle.CreateVoting
{
  partial class PrimeControl
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
      this.storedPrimesLabel = new System.Windows.Forms.Label();
      this.readyPrimeLabel = new System.Windows.Forms.Label();
      this.generateButton = new System.Windows.Forms.Button();
      this.takeButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.doneButton = new System.Windows.Forms.Button();
      this.progressBar = new System.Windows.Forms.ProgressBar();
      this.progressLabel = new System.Windows.Forms.Label();
      this.SuspendLayout();
      // 
      // storedPrimesLabel
      // 
      this.storedPrimesLabel.AutoSize = true;
      this.storedPrimesLabel.Location = new System.Drawing.Point(3, 9);
      this.storedPrimesLabel.Name = "storedPrimesLabel";
      this.storedPrimesLabel.Size = new System.Drawing.Size(74, 14);
      this.storedPrimesLabel.TabIndex = 0;
      this.storedPrimesLabel.Text = "Stored Primes";
      // 
      // readyPrimeLabel
      // 
      this.readyPrimeLabel.AutoSize = true;
      this.readyPrimeLabel.Location = new System.Drawing.Point(3, 40);
      this.readyPrimeLabel.Name = "readyPrimeLabel";
      this.readyPrimeLabel.Size = new System.Drawing.Size(67, 14);
      this.readyPrimeLabel.TabIndex = 1;
      this.readyPrimeLabel.Text = "Ready Prime";
      // 
      // generateButton
      // 
      this.generateButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.generateButton.Location = new System.Drawing.Point(387, 3);
      this.generateButton.Name = "generateButton";
      this.generateButton.Size = new System.Drawing.Size(262, 25);
      this.generateButton.TabIndex = 2;
      this.generateButton.Text = "&Generate New";
      this.generateButton.UseVisualStyleBackColor = true;
      this.generateButton.Click += new System.EventHandler(this.generateButton_Click);
      // 
      // takeButton
      // 
      this.takeButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
      this.takeButton.Location = new System.Drawing.Point(387, 34);
      this.takeButton.Name = "takeButton";
      this.takeButton.Size = new System.Drawing.Size(262, 25);
      this.takeButton.TabIndex = 3;
      this.takeButton.Text = "Take &Verify";
      this.takeButton.UseVisualStyleBackColor = true;
      this.takeButton.Click += new System.EventHandler(this.takeButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(387, 290);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(128, 30);
      this.cancelButton.TabIndex = 57;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // doneButton
      // 
      this.doneButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.doneButton.Font = new System.Drawing.Font("Arial", 8.25F);
      this.doneButton.Location = new System.Drawing.Point(521, 290);
      this.doneButton.Name = "doneButton";
      this.doneButton.Size = new System.Drawing.Size(128, 30);
      this.doneButton.TabIndex = 56;
      this.doneButton.Text = "&Done";
      this.doneButton.UseVisualStyleBackColor = true;
      this.doneButton.Click += new System.EventHandler(this.doneButton_Click);
      // 
      // progressBar
      // 
      this.progressBar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.progressBar.Location = new System.Drawing.Point(9, 121);
      this.progressBar.Name = "progressBar";
      this.progressBar.Size = new System.Drawing.Size(640, 17);
      this.progressBar.TabIndex = 58;
      // 
      // progressLabel
      // 
      this.progressLabel.AutoSize = true;
      this.progressLabel.Location = new System.Drawing.Point(3, 103);
      this.progressLabel.Name = "progressLabel";
      this.progressLabel.Size = new System.Drawing.Size(0, 14);
      this.progressLabel.TabIndex = 59;
      // 
      // PrimeControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.progressLabel);
      this.Controls.Add(this.progressBar);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.doneButton);
      this.Controls.Add(this.takeButton);
      this.Controls.Add(this.generateButton);
      this.Controls.Add(this.readyPrimeLabel);
      this.Controls.Add(this.storedPrimesLabel);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.Name = "PrimeControl";
      this.Size = new System.Drawing.Size(652, 323);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label storedPrimesLabel;
    private System.Windows.Forms.Label readyPrimeLabel;
    private System.Windows.Forms.Button generateButton;
    private System.Windows.Forms.Button takeButton;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button doneButton;
    private System.Windows.Forms.ProgressBar progressBar;
    private System.Windows.Forms.Label progressLabel;
  }
}
