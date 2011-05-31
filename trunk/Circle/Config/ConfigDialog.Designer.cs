namespace Pirate.PiVote.Circle.Config
{
  partial class ConfigDialog
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
      this.configTabs = new System.Windows.Forms.TabControl();
      this.proofCountTab = new System.Windows.Forms.TabPage();
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.configProofCountControl1 = new Pirate.PiVote.Circle.Config.ConfigProofCountControl();
      this.configTabs.SuspendLayout();
      this.proofCountTab.SuspendLayout();
      this.SuspendLayout();
      // 
      // configTabs
      // 
      this.configTabs.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.configTabs.Controls.Add(this.proofCountTab);
      this.configTabs.Location = new System.Drawing.Point(12, 13);
      this.configTabs.Name = "configTabs";
      this.configTabs.SelectedIndex = 0;
      this.configTabs.Size = new System.Drawing.Size(570, 310);
      this.configTabs.TabIndex = 0;
      // 
      // proofCountTab
      // 
      this.proofCountTab.Controls.Add(this.configProofCountControl1);
      this.proofCountTab.Location = new System.Drawing.Point(4, 23);
      this.proofCountTab.Name = "proofCountTab";
      this.proofCountTab.Padding = new System.Windows.Forms.Padding(3);
      this.proofCountTab.Size = new System.Drawing.Size(562, 283);
      this.proofCountTab.TabIndex = 0;
      this.proofCountTab.Text = "proofCountTab";
      this.proofCountTab.UseVisualStyleBackColor = true;
      // 
      // okButton
      // 
      this.okButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.okButton.Location = new System.Drawing.Point(352, 330);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(112, 32);
      this.okButton.TabIndex = 1;
      this.okButton.Text = "okButton";
      this.okButton.UseVisualStyleBackColor = true;
      this.okButton.Click += new System.EventHandler(this.okButton_Click);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(470, 330);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(112, 32);
      this.cancelButton.TabIndex = 2;
      this.cancelButton.Text = "cancelButton";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // configProofCountControl1
      // 
      this.configProofCountControl1.Dock = System.Windows.Forms.DockStyle.Fill;
      this.configProofCountControl1.InitialProofCount = 1;
      this.configProofCountControl1.Location = new System.Drawing.Point(3, 3);
      this.configProofCountControl1.Name = "configProofCountControl1";
      this.configProofCountControl1.Size = new System.Drawing.Size(556, 277);
      this.configProofCountControl1.TabIndex = 0;
      // 
      // ConfigDialog
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.ClientSize = new System.Drawing.Size(594, 375);
      this.ControlBox = false;
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Controls.Add(this.configTabs);
      this.Font = new System.Drawing.Font("Arial", 8.25F);
      this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ConfigDialog";
      this.Text = "ConfigDialog";
      this.Load += new System.EventHandler(this.ConfigDialog_Load);
      this.configTabs.ResumeLayout(false);
      this.proofCountTab.ResumeLayout(false);
      this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TabControl configTabs;
    private System.Windows.Forms.TabPage proofCountTab;
    private System.Windows.Forms.Button okButton;
    private System.Windows.Forms.Button cancelButton;
    private ConfigProofCountControl configProofCountControl1;
  }
}