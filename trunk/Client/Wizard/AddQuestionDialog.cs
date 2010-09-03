/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Client
{
  public partial class AddQuestionDialog : Form
  {
    public AddQuestionDialog()
    {
      InitializeComponent();

      Text = Resources.CreateVotingQuestion;
      this.textLabel.Text = Resources.CreateVotingQuestion;
      this.descriptionLabel.Text = Resources.CreateVotingDescription;
      this.urlLabel.Text = Resources.CreateVotingUrl;
      this.optionLabel.Text = Resources.CreateVotingAnswers;
      this.optionNumberLabel.Text = Resources.CreateVotingAnswersPerVoter;
      this.textColumnHeader.Text = Resources.CreateVotingOptionText;
      this.descriptionColumnHeader.Text = Resources.CreateVotingOptionDescription;
      this.okButton.Text = Resources.OkButton;
      this.cancelButton.Text = Resources.CancelButton;
      this.abstentionLabel.Text = Resources.CreateQuestionAbstentionAuto;

      this.textTextBox.TextChanged += new EventHandler(textTextBox_TextChanged);
      this.descriptionTextBox.TextChanged += new EventHandler(descriptionTextBox_TextChanged);

      CenterToScreen();
    }

    private void descriptionTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckValid();
    }

    private void textTextBox_TextChanged(object sender, EventArgs e)
    {
      CheckValid();
    }

    private void addToolStripMenuItem_Click(object sender, EventArgs e)
    {
      AddOptionDialog dialog = new AddOptionDialog();

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        ListViewItem item = new ListViewItem(dialog.OptionText.AllLanguages);
        item.SubItems.Add(dialog.OptionDescription.AllLanguages);
        item.Tag = new Option(dialog.OptionText, dialog.OptionDescription, dialog.OptionUrl);
        this.optionListView.Items.Add(item);

        CheckValid();
      }
    }

    private void removeToolStripMenuItem_Click(object sender, EventArgs e)
    {
      if (this.optionListView.SelectedItems.Count > 0)
      {
        this.optionListView.SelectedItems[0].Remove();

        CheckValid();
      }
    }

    private void optionListContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
    {
      this.addToolStripMenuItem.Text = Resources.ContextMenuAdd;
      this.removeToolStripMenuItem.Text = Resources.ContextMenuRemove;
      this.removeToolStripMenuItem.Enabled = this.optionListView.SelectedIndices.Count > 0;
    }

    public static Question ShowQuestion(Question original)
    {
      AddQuestionDialog dialog = new AddQuestionDialog();

      if (original != null)
      {
        dialog.textTextBox.Text = original.Text;
        dialog.descriptionTextBox.Text = original.Text;
        dialog.optionNumberUpDown.Value = original.MaxVota;

        dialog.optionListView.Items.Clear();
        foreach (Option option in original.Options)
        {
          ListViewItem item = new ListViewItem(option.Text.AllLanguages);
          item.SubItems.Add(option.Description.AllLanguages);
          item.Tag = option;
          dialog.optionListView.Items.Add(item);
        }
      }

      dialog.CheckValid();

      if (dialog.ShowDialog() == DialogResult.OK)
      {
        Question question = new Question(
          dialog.textTextBox.Text,
          dialog.descriptionTextBox.Text,
          dialog.urlTextBox.Text,
          Convert.ToInt32(dialog.optionNumberUpDown.Value));

        foreach (ListViewItem item in dialog.optionListView.Items)
        {
          question.AddOption((Option)item.Tag);
        }

        return question;
      }
      else
      {
        return null;
      }
    }

    private void CheckValid()
    {
      bool valid = true;

      valid &= !this.textTextBox.IsEmpty;
      valid &= this.optionListView.Items.Count >= 2;
      valid &= this.optionListView.Items.Count >= this.optionNumberUpDown.Value;

      this.okButton.Enabled = valid;
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.OK;
      Close();
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      DialogResult = DialogResult.Cancel;
      Close();
    }

    private void optionNumberUpDown_ValueChanged(object sender, EventArgs e)
    {
      CheckValid();
    }
  }
}
