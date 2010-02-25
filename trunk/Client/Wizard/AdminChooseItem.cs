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
  public partial class AdminChooseItem : WizardItem
  {
    public AdminChooseItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      if (this.setSignatureResponsesRadio.Checked)
      {
        return new SetSignatureResponsesItem();
      }
      else if (this.getSignatureRequestsRadio.Checked)
      {
        return new GetSignatureRequestsItem();
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
        return
          this.getSignatureRequestsRadio.Checked |
          this.setSignatureResponsesRadio.Checked |
          this.createVotingRadio.Checked;
      }
    }

    public override void Begin()
    {
      OnUpdateWizard();
    }

    private void getSignatureRequestsRadio_CheckedChanged(object sender, EventArgs e)
    {
      OnUpdateWizard();
    }

    private void setSignatureResponsesRadio_CheckedChanged(object sender, EventArgs e)
    {
      OnUpdateWizard();
    }

    private void createVotingRadio_CheckedChanged(object sender, EventArgs e)
    {
      OnUpdateWizard();
    }
  }
}
