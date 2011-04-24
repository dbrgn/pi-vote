/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Circle
{
  public partial class VotingListControl : UserControl
  {
    private const int VerticalSpace = 4;
    private const int HorizontalSpace = 4;

    private List<VotingDescriptor2> votings;
    
    private List<VotingControl> controls;

    [Browsable(true)]
    public event VotingActionHandler VotingAction;
    
    public VotingListControl()
    {
      InitializeComponent();
    }

    public void Set(CircleController controller, IEnumerable<VotingDescriptor2> votings)
    {
      this.votings = new List<VotingDescriptor2>(votings);
      this.controls = new List<VotingControl>();
      Controls.Clear();

      int top = 0;

      foreach (var voting in this.votings)
      {
        VotingControl control = new VotingControl();
        control.Controller = controller;
        control.Voting = voting;
        control.Left = 0;
        control.Top = top;
        control.Width = ClientSize.Width - HorizontalSpace;
        control.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
        control.VotingAction += new VotingActionHandler(Control_VotingAction);
        this.controls.Add(control);
        Controls.Add(control);

        top += control.Height + VerticalSpace;
      }

      foreach (var control in this.controls)
      {
        control.Width = ClientSize.Width - HorizontalSpace;
      }
    }

    private void Control_VotingAction(VotingDescriptor2 voting)
    {
      OnVotingAction(voting);
    }

    private void OnVotingAction(VotingDescriptor2 voting)
    {
      if (VotingAction != null)
      {
        VotingAction(voting);
      }
    }
  }
}
