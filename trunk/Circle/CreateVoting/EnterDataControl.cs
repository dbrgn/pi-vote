using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;

namespace Pirate.PiVote.Circle.CreateVoting
{
  public partial class EnterDataControl : CreateVotingControl
  {
    public EnterDataControl()
    {
      InitializeComponent();
    }

    public override void Prepare()
    {
      this.titleLabel.Text = Resources.CreateVotingTitle;
      this.descriptionLabel.Text = Resources.CreateVotingDescription;
      this.urlLabel.Text = Resources.CreateVotingUrl;
      this.clearButton.Text = Resources.CreateVotingClearButton;
      this.questionLabel.Text = Resources.CreateVotingQuestions;
      this.textColumnHeader.Text = Resources.CreateVotingQuestionText;
      this.descriptionColumnHeader.Text = Resources.CreateVotingQuestionDescription;
      this.nextButton.Text = GuiResources.ButtonNext;
      this.cancelButton.Text = GuiResources.ButtonCancel;

      Status.Data = VotingData.TryLoad(Status.Controller.Status.DataPath);

      if (Status.Data != null)
      {
        this.titleBox.Text = Status.Data.Title;
        this.descriptionBox.Text = Status.Data.Descrption;
        this.urlTextBox.Text = Status.Data.Url;

        foreach (var question in Status.Data.Questions)
        {
          ListViewItem item = new ListViewItem(question.Text.AllLanguages);
          item.SubItems.Add(question.Description.AllLanguages);
          item.Tag = question;
          this.questionListView.Items.Add(item);
        }
      }

      CheckEnable();
    }

    private void addToolStripMenuItem_Click(object sender, EventArgs e)
    {
      Question question = AddQuestionDialog.ShowQuestion(null);

      if (question != null)
      {
        ListViewItem item = new ListViewItem(question.Text.AllLanguages);
        item.SubItems.Add(question.Description.AllLanguages);
        item.Tag = question;
        this.questionListView.Items.Add(item);

        CheckEnable();
      }
    }

    private void removeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.questionListView.SelectedItems.Count > 0)
      {
        this.questionListView.SelectedItems[0].Remove();

        CheckEnable();
      }
    }

    private void editToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.questionListView.SelectedItems.Count > 0)
      {
        ListViewItem item = this.questionListView.SelectedItems[0];
        Question question = (Question)item.Tag;
        question = AddQuestionDialog.ShowQuestion(question);

        if (question != null)
        {
          item.Text = question.Text.AllLanguages;
          item.SubItems[1].Text = question.Description.AllLanguages;
          item.Tag = question;
        }
      }
    }

    private void questionContextMenu_Opening(object sender, CancelEventArgs e)
    {
      this.addToolStripMenuItem.Text = GuiResources.ContextMenuAdd;
      this.removeToolStripMenuItem.Text = GuiResources.ContextMenuRemove;
      this.editToolStripMenuItem.Text = GuiResources.ContextMenuEdit;

      this.removeToolStripMenuItem.Enabled = this.questionListView.SelectedItems.Count > 0;
      this.editToolStripMenuItem.Enabled = this.questionListView.SelectedItems.Count > 0;
    }

    private void CheckEnable()
    {
      bool enable = true;

      enable &= !this.titleBox.IsEmpty;
      enable &= !this.descriptionBox.IsEmpty;
      enable &= this.questionListView.Items.Count >= 1;

      this.nextButton.Enabled = enable;
    }

    private void TitleBox_TextChanged(object sender, System.EventArgs e)
    {
      CheckEnable();
    }

    private void DescriptionBox_TextChanged(object sender, System.EventArgs e)
    {
      CheckEnable();
    }

    private void UrlBox_TextChanged(object sender, System.EventArgs e)
    {
      CheckEnable();
    }

    private void clearButton_Click(object sender, EventArgs e)
    {
      this.titleBox.Clear();
      this.descriptionBox.Clear();
      this.urlTextBox.Clear();
      this.questionListView.Items.Clear();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      OnCloseCreateDialog();
    }

    private void nextButton_Click(object sender, EventArgs e)
    {
      Status.Data = new VotingData(this.titleBox.Text, this.descriptionBox.Text, this.urlTextBox.Text);
      Status.Data.Questions.Clear();
      
      foreach (ListViewItem item in this.questionListView.Items)
      {
        Status.Data.Questions.Add((Question)item.Tag);
      }

      Status.Data.SaveTo(Status.Controller.Status.DataPath);

      var nextControl = new DateGroupAuthoritiesControl();
      nextControl.Status = Status;
      OnShowNextControl(nextControl);
    }
  }
}
