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
    public VotingMaterial VotingMaterial { get; set; }
    public VotingDescriptor VotingDescriptor { get; set; }

    public VoteItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      if (this.voteControl.Valid)
      {
        VoteCompleteItem item = new VoteCompleteItem();
        item.Vota = this.voteControl.Vota;
        item.VotingMaterial = VotingMaterial;
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
      get { return this.voteControl.Valid; }
    }

    public override void Begin()
    {
      this.voteControl.Voting = VotingDescriptor;
      this.voteControl.Display(true);
      this.voteControl.ValidChanged += VoteControl_ValidChanged;
      OnUpdateWizard();
    }

    private void VoteControl_ValidChanged(object sender, EventArgs e)
    {
      OnUpdateWizard();
    }
  }
}
