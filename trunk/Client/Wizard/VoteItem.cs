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

namespace Pirate.PiVote.Client
{
  public partial class VoteItem : WizardItem
  {
    public VotingClient.VotingDescriptor VotingDescriptor { get; set; }

    private List<RadioButton> optionRadioButtons;
    private List<CheckBox> optionCheckBoxes;

    public VoteItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      if (this.optionCheckBoxes != null)
      {
        VoteCompleteItem item = new VoteCompleteItem();
        item.Vota = new List<bool>(this.optionCheckBoxes.Select(box => box.Checked));
        item.VotingDescriptor = VotingDescriptor;
        return item;
      }
      else if (this.optionRadioButtons != null)
      {
        VoteCompleteItem item = new VoteCompleteItem();
        item.Vota = new List<bool>(this.optionRadioButtons.Select(box => box.Checked));
        item.VotingDescriptor = VotingDescriptor;
        return item;
      }
      else
      {
        return null;
      }
    }

    public override WizardItem Previous()
    {
      return null;
    }

    public override WizardItem Cancel()
    {
      return null;
    }

    public override bool CanCancel
    {
      get { return true; }
    }

    public override bool CanNext
    {
      get
      {
        if (this.optionCheckBoxes != null)
        {
          return this.optionCheckBoxes.Where(box => box.Checked).Count() == VotingDescriptor.MaxOptions;
        }
        else if (this.optionRadioButtons != null)
        {
          return this.optionRadioButtons.Where(box => box.Checked).Count() == VotingDescriptor.MaxOptions;
        }
        else
        {
          return false;
        }
      }
    }

    public override void Begin()
    {
      int top = 0;

      Label titleLabel = new Label();
      titleLabel.Font = new Font("Arial", 10, FontStyle.Bold);
      titleLabel.Text = VotingDescriptor.Title;
      titleLabel.AutoSize = false;
      titleLabel.Top = top;
      titleLabel.Left = 0;
      titleLabel.Width = Width;
      Controls.Add(titleLabel);
      top += titleLabel.Height;

      Label descriptionLabel = new Label();
      descriptionLabel.Font = new Font("Arial", 10, FontStyle.Regular);
      descriptionLabel.Text = VotingDescriptor.Description;
      descriptionLabel.AutoSize = false;
      descriptionLabel.Top = top;
      descriptionLabel.Left = 0;
      descriptionLabel.Width = Width;
      descriptionLabel.Height = 50;
      Controls.Add(descriptionLabel);
      top += descriptionLabel.Height;

      Label questionLabel = new Label();
      questionLabel.Font = new Font("Arial", 10, FontStyle.Regular);
      questionLabel.Text = VotingDescriptor.Question;
      questionLabel.AutoSize = false;
      questionLabel.Top = top;
      questionLabel.Left = 0;
      questionLabel.Width = Width;
      Controls.Add(questionLabel);
      top += questionLabel.Height;

      if (VotingDescriptor.MaxOptions > 1)
      {
        this.optionCheckBoxes = new List<CheckBox>();
      }
      else
      {
        this.optionRadioButtons = new List<RadioButton>();
      }

      foreach (VotingClient.OptionDescriptor option in VotingDescriptor.Options)
      {
        top += 10;

        if (VotingDescriptor.MaxOptions > 1)
        {
          CheckBox optionBox = new CheckBox();
          optionBox.Font = new Font("Arial", 10, FontStyle.Regular);
          optionBox.Text = option.Text;
          optionBox.AutoSize = false;
          optionBox.Top = top;
          optionBox.Left = 0;
          optionBox.Width = Width;
          optionBox.CheckedChanged += new EventHandler(optionBox_CheckedChanged);
          Controls.Add(optionBox);
          top += optionBox.Height;
          this.optionCheckBoxes.Add(optionBox);
        }
        else
        {
          RadioButton optionBox = new RadioButton();
          optionBox.Font = new Font("Arial", 10, FontStyle.Regular);
          optionBox.Text = option.Text;
          optionBox.AutoSize = false;
          optionBox.Top = top;
          optionBox.Left = 0;
          optionBox.Width = Width;
          optionBox.CheckedChanged += new EventHandler(optionBox_CheckedChanged);
          Controls.Add(optionBox);
          top += optionBox.Height;
          this.optionRadioButtons.Add(optionBox);
        }

        Label optionDescriptionLabel = new Label();
        optionDescriptionLabel.Font = new Font("Arial", 10, FontStyle.Regular);
        optionDescriptionLabel.Text = option.Description;
        optionDescriptionLabel.AutoSize = false;
        optionDescriptionLabel.Top = top;
        optionDescriptionLabel.Left = 0;
        optionDescriptionLabel.Width = Width;
        optionDescriptionLabel.Height = 50;
        Controls.Add(optionDescriptionLabel);
        top += optionDescriptionLabel.Height;
      }
    }

    private void optionBox_CheckedChanged(object sender, EventArgs e)
    {
      OnUpdateWizard();
    }
  }
}
