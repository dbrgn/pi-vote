/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

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
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Circle.Result
{
  public partial class ResultDisplayDialog : Form
  {
    private const int Space = 20;

    public ResultDisplayDialog()
    {
      InitializeComponent();

      Text = Resources.ResultDialogTitle;
    }

    private void okButton_Click(object sender, EventArgs e)
    {
      Close();
    }

    public void Set(VotingResult result, IDictionary<Guid, VoteReceiptStatus> voteReceiptStatus)
    {
      this.keyColumn.Width = (this.resultList.Width - Space) / 5 * 4;
      this.valueColumn.Width = (this.resultList.Width - Space) / 5;

      Add(result.Title.Text, true);
      Add(Resources.ResultDialogTotalBallots, result.TotalBallots.ToString());
      Add(Resources.ResultDialogValidBallots, result.ValidBallots.ToString());
      Add(Resources.ResultDialogInvalidBallots, result.InvalidBallots.ToString());
      Add();

      foreach (var question in result.Questions)
      {
        Add(question.Text.Text, true);
        int abstentions = 0;

        foreach (var option in question.Options)
        {
          if (option.Text.Text == Resources.OptionAbstainSpecial)
          {
            abstentions += option.Result;
          }
          else
          {
            Add(option.Text.Text, option.Result.ToString());
          }
        }

        if (abstentions > 0)
        {
          Add(Resources.ResultDialogAbstain, abstentions.ToString());
        }

        Add();
      }

      if (voteReceiptStatus.Count > 0)
      {
        Add(Resources.ResultDialogYourVote, true);

        foreach (var voteReceipt in voteReceiptStatus)
        {
          switch (voteReceipt.Value)
          {
            case VoteReceiptStatus.FoundBad:
              Add(voteReceipt.Key.ToString(), Resources.ResultDialogFoundBad);
              break;
            case VoteReceiptStatus.FoundOk:
              Add(voteReceipt.Key.ToString(), Resources.ResultDialogFoundOk);
              break;
            case VoteReceiptStatus.NotFound:
              Add(voteReceipt.Key.ToString(), Resources.ResultDialogNotFound);
              break;
          }
        }
      }
    }

    private void Add()
    {
      Add(string.Empty);
    }

    private void Add(string text)
    {
      Add(text, string.Empty);
    }

    private void Add(string text, bool bold)
    {
      Add(text, string.Empty, bold);
    }

    private void Add(string text, string value)
    {
      Add(text, value, false);
    }

    private void Add(string text, string value, bool bold)
    {
      var item = this.resultList.Items.Add(text);
      item.SubItems.Add(value);

      if (bold)
      {
        item.Font = new Font(item.Font, FontStyle.Bold);
      }
    }

    public static void ShowResult(VotingResult result, IDictionary<Guid, VoteReceiptStatus> voteReceiptStatus)
    {
      ResultDisplayDialog dialog = new ResultDisplayDialog();
      dialog.Set(result, voteReceiptStatus);
      dialog.ShowDialog();
    }

    private void ResultDisplayDialog_Load(object sender, EventArgs e)
    {
      var screenBounds = Screen.PrimaryScreen.Bounds;

      if (screenBounds.Width < Width)
      {
        Width = screenBounds.Width;
      }

      if (screenBounds.Height < Height)
      {
        Height = screenBounds.Height;
      } 
      
      CenterToScreen();
    }

    private void ResultDisplayDialog_KeyDown(object sender, KeyEventArgs e)
    {
      switch (e.KeyCode)
      {
        case Keys.Enter:
        case Keys.Escape:
          Close();
          break;
      }
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);
      this.keyColumn.Width = (this.resultList.Width - 10) / 5 * 4;
      this.valueColumn.Width = (this.resultList.Width - 10) / 5;
    }
  }
}
