/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Client
{
  public partial class WizardItem : UserControl
  {
    public event EventHandler UpdateWizard;
    public event EventHandler NextStep;

    public WizardStatus Status { get; set; }

    public WizardItem()
    {
      InitializeComponent();
    }

    protected void OnNextStep()
    {
      if (NextStep != null)
      {
        NextStep(this, new EventArgs());
      }
    }

    protected void OnUpdateWizard()
    {
      if (UpdateWizard != null)
      {
        UpdateWizard(this, new EventArgs());
      }
    }
    
    public virtual WizardItem Next()
    {
      return null;
    }

    public virtual WizardItem Previous()
    {
      return null;
    }

    public virtual WizardItem Cancel()
    {
      return null;
    }

    public virtual bool CanNext
    {
      get { return false; }
    }

    public virtual bool CanPrevious
    {
      get { return false; }
    }

    public virtual bool CanCancel
    {
      get { return false; }
    }

    public virtual void Begin()
    { 
    }
  }
}
