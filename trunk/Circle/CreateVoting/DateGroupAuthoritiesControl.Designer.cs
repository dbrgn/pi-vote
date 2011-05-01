namespace Pirate.PiVote.Circle.CreateVoting
{
  partial class DateGroupAuthoritiesControl
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
      this.authoritiesLabel = new System.Windows.Forms.Label();
      this.cancelButton = new System.Windows.Forms.Button();
      this.nextButton = new System.Windows.Forms.Button();
      this.authoritiesList = new System.Windows.Forms.ListView();
      this.idColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.nameColumnHeader = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
      this.groupLabel = new System.Windows.Forms.Label();
      this.groupComboBox = new Pirate.PiVote.Gui.GroupComboBox();
      this.votingUntilLabel = new System.Windows.Forms.Label();
      this.votingFromLabel = new System.Windows.Forms.Label();
      this.votingFromPicker = new System.Windows.Forms.DateTimePicker();
      this.votingUntilPicker = new System.Windows.Forms.DateTimePicker();
      this.SuspendLayout();
      // 
      // authoritiesLabel
      // 
      this.authoritiesLabel.AutoSize = true;
      this.authoritiesLabel.Location = new System.Drawing.Point(4, 80);
      this.authoritiesLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.authoritiesLabel.Name = "authoritiesLabel";
      this.authoritiesLabel.Size = new System.Drawing.Size(62, 14);
      this.authoritiesLabel.TabIndex = 56;
      this.authoritiesLabel.Text = "Authorities:";
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.cancelButton.Location = new System.Drawing.Point(554, 429);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(128, 30);
      this.cancelButton.TabIndex = 55;
      this.cancelButton.Text = "&Cancel";
      this.cancelButton.UseVisualStyleBackColor = true;
      this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
      // 
      // nextButton
      // 
      this.nextButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
      this.nextButton.Font = new System.Drawing.Font("Arial", 8.25F);
      this.nextButton.Location = new System.Drawing.Point(688, 429);
      this.nextButton.Name = "nextButton";
      this.nextButton.Size = new System.Drawing.Size(128, 30);
      this.nextButton.TabIndex = 54;
      this.nextButton.Text = "&Next";
      this.nextButton.UseVisualStyleBackColor = true;
      this.nextButton.Click += new System.EventHandler(this.nextButton_Click);
      // 
      // authoritiesList
      // 
      this.authoritiesList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.authoritiesList.CheckBoxes = true;
      this.authoritiesList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.idColumnHeader,
            this.nameColumnHeader});
      this.authoritiesList.FullRowSelect = true;
      this.authoritiesList.HideSelection = false;
      this.authoritiesList.Location = new System.Drawing.Point(104, 76);
      this.authoritiesList.MultiSelect = false;
      this.authoritiesList.Name = "authoritiesList";
      this.authoritiesList.Size = new System.Drawing.Size(712, 347);
      this.authoritiesList.TabIndex = 53;
      this.authoritiesList.UseCompatibleStateImageBehavior = false;
      this.authoritiesList.View = System.Windows.Forms.View.Details;
      this.authoritiesList.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.authoritiesList_ItemChecked);
      // 
      // idColumnHeader
      // 
      this.idColumnHeader.Text = "Id";
      this.idColumnHeader.Width = 220;
      // 
      // nameColumnHeader
      // 
      this.nameColumnHeader.Text = "Name";
      this.nameColumnHeader.Width = 220;
      // 
      // groupLabel
      // 
      this.groupLabel.AutoSize = true;
      this.groupLabel.Location = new System.Drawing.Point(4, 52);
      this.groupLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.groupLabel.Name = "groupLabel";
      this.groupLabel.Size = new System.Drawing.Size(40, 14);
      this.groupLabel.TabIndex = 52;
      this.groupLabel.Text = "Group:";
      // 
      // groupComboBox
      // 
      this.groupComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
      this.groupComboBox.FormattingEnabled = true;
      this.groupComboBox.Location = new System.Drawing.Point(104, 49);
      this.groupComboBox.Margin = new System.Windows.Forms.Padding(2);
      this.groupComboBox.Name = "groupComboBox";
      this.groupComboBox.Size = new System.Drawing.Size(224, 22);
      this.groupComboBox.TabIndex = 49;
      this.groupComboBox.Value = null;
      this.groupComboBox.SelectedIndexChanged += new System.EventHandler(this.groupComboBox_SelectedIndexChanged);
      // 
      // votingUntilLabel
      // 
      this.votingUntilLabel.AutoSize = true;
      this.votingUntilLabel.Location = new System.Drawing.Point(4, 31);
      this.votingUntilLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.votingUntilLabel.Name = "votingUntilLabel";
      this.votingUntilLabel.Size = new System.Drawing.Size(26, 14);
      this.votingUntilLabel.TabIndex = 51;
      this.votingUntilLabel.Text = "until";
      // 
      // votingFromLabel
      // 
      this.votingFromLabel.AutoSize = true;
      this.votingFromLabel.Location = new System.Drawing.Point(4, 7);
      this.votingFromLabel.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
      this.votingFromLabel.Name = "votingFromLabel";
      this.votingFromLabel.Size = new System.Drawing.Size(58, 14);
      this.votingFromLabel.TabIndex = 50;
      this.votingFromLabel.Text = "Open from";
      // 
      // votingFromPicker
      // 
      this.votingFromPicker.Location = new System.Drawing.Point(104, 2);
      this.votingFromPicker.Margin = new System.Windows.Forms.Padding(2);
      this.votingFromPicker.Name = "votingFromPicker";
      this.votingFromPicker.Size = new System.Drawing.Size(224, 20);
      this.votingFromPicker.TabIndex = 47;
      this.votingFromPicker.ValueChanged += new System.EventHandler(this.votingFromPicker_ValueChanged);
      // 
      // votingUntilPicker
      // 
      this.votingUntilPicker.Location = new System.Drawing.Point(104, 26);
      this.votingUntilPicker.Margin = new System.Windows.Forms.Padding(2);
      this.votingUntilPicker.Name = "votingUntilPicker";
      this.votingUntilPicker.Size = new System.Drawing.Size(224, 20);
      this.votingUntilPicker.TabIndex = 48;
      this.votingUntilPicker.ValueChanged += new System.EventHandler(this.votingUntilPicker_ValueChanged);
      // 
      // DateGroupAuthoritiesControl
      // 
      this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
      this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
      this.Controls.Add(this.authoritiesLabel);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.nextButton);
      this.Controls.Add(this.authoritiesList);
      this.Controls.Add(this.groupLabel);
      this.Controls.Add(this.groupComboBox);
      this.Controls.Add(this.votingUntilLabel);
      this.Controls.Add(this.votingFromLabel);
      this.Controls.Add(this.votingFromPicker);
      this.Controls.Add(this.votingUntilPicker);
      this.Name = "DateGroupAuthoritiesControl";
      this.Size = new System.Drawing.Size(819, 462);
      this.ResumeLayout(false);
      this.PerformLayout();

    }

    #endregion

    private System.Windows.Forms.Label groupLabel;
    private Gui.GroupComboBox groupComboBox;
    private System.Windows.Forms.Label votingUntilLabel;
    private System.Windows.Forms.Label votingFromLabel;
    private System.Windows.Forms.DateTimePicker votingFromPicker;
    private System.Windows.Forms.DateTimePicker votingUntilPicker;
    private System.Windows.Forms.ListView authoritiesList;
    private System.Windows.Forms.Button cancelButton;
    private System.Windows.Forms.Button nextButton;
    private System.Windows.Forms.ColumnHeader idColumnHeader;
    private System.Windows.Forms.ColumnHeader nameColumnHeader;
    private System.Windows.Forms.Label authoritiesLabel;
  }
}
