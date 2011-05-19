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

      this.votings = new List<VotingDescriptor2>();
      this.controls = new List<VotingControl>();
    }

    public void Set(CircleController controller, IEnumerable<VotingDescriptor2> newVotings)
    {
      var oldQueue = new Queue<VotingDescriptor2>(this.votings);
      var newQueue = new Queue<VotingDescriptor2>(newVotings);
      var oldControlQueue = new Queue<VotingControl>(this.controls);
      var newControlQueue = new Queue<VotingControl>();
      this.votings = new List<VotingDescriptor2>();
      int top = 0;

      this.emptyLabel.Visible = newVotings.Count() == 0;

      while (oldQueue.Count > 0 ||
             newQueue.Count > 0)
      {
        if (oldQueue.Count > 0 &&
            newQueue.Count > 0 &&
            oldQueue.Peek().Id.Equals(newQueue.Peek().Id))
        {
          oldQueue.Dequeue();
          var newVoting = newQueue.Dequeue();
          this.votings.Add(newVoting);

          VotingControl control = oldControlQueue.Dequeue();
          control.Voting = newVoting;
          control.Left = 0;
          control.Top = top;
          control.UpdateDisplay();
          newControlQueue.Enqueue(control);

          top += control.Height + VerticalSpace;
         }
        else if (oldQueue.Count > 0 &&
                 newQueue.Count > 0)
        {
          oldQueue.Dequeue();
          Controls.Remove(oldControlQueue.Dequeue());
        }
        else if (oldQueue.Count > 0)
        {
          oldQueue.Dequeue();
          Controls.Remove(oldControlQueue.Dequeue());
        }
        else if (newQueue.Count > 0)
        {
          var newVoting = newQueue.Dequeue();
          this.votings.Add(newVoting);

          VotingControl control = new VotingControl();
          control.Controller = controller;
          control.Voting = newVoting;
          control.Left = 0;
          control.Top = top;
          control.Width = Width - (VerticalScroll.Visible ? 16 : 0) - HorizontalSpace;
          control.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top;
          control.VotingAction += new VotingActionHandler(Control_VotingAction);
          newControlQueue.Enqueue(control);
          Controls.Add(control);

          top += control.Height + VerticalSpace;
        }
      }

      this.controls = new List<VotingControl>(newControlQueue);

      foreach (var control in this.controls)
      {
        control.Width = Width - (VerticalScroll.Visible ? 16 : 0) - HorizontalSpace;
      }
    }

    protected override void OnResize(EventArgs e)
    {
      base.OnResize(e);

      if (this.controls != null)
      {
        foreach (var control in this.controls)
        {
          control.Width = Width - (VerticalScroll.Visible ? 16 : 0) - HorizontalSpace;
        }
      }
    }

    public void UpdateLanguage()
    {
      this.emptyLabel.Text = Resources.VotingListEmpty;

      foreach (var control in this.controls)
      {
        control.UpdateDisplay();
      }
    }

    private void Control_VotingAction(VotingActionType type, VotingDescriptor2 voting)
    {
      OnVotingAction(type, voting);
    }

    private void OnVotingAction(VotingActionType type, VotingDescriptor2 voting)
    {
      if (VotingAction != null)
      {
        VotingAction(type, voting);
      }
    }
  }
}
