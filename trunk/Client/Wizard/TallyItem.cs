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
  public partial class TallyItem : WizardItem
  {
    private bool run;
    private VotingResult result;
    private Exception exception;

    public VotingClient.VotingDescriptor VotingDescriptor { get; set; }

    public TallyItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      return null;
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

    public override void Begin()
    {
      this.run = true;

      Status.VotingClient.ActivateVoter((VoterCertificate)Status.Certificate);
      Status.VotingClient.GetResult(VotingDescriptor.Id, GetResultComplete);

      while (this.run)
      {
        Status.UpdateProgress();
        Application.DoEvents();
        Thread.Sleep(1);
      }

      Status.UpdateProgress();

      if (this.exception == null)
      {
        if (this.result != null)
        {
          ListViewItem title = new ListViewItem(this.result.Title);
          this.resultList.Items.Add(title);

          ListViewItem totalBallots = new ListViewItem(Resources.TotalBallots);
          totalBallots.SubItems.Add(this.result.TotalBallots.ToString());
          this.resultList.Items.Add(totalBallots);

          ListViewItem invalidBallots = new ListViewItem(Resources.InvalidBallots);
          invalidBallots.SubItems.Add(this.result.InvalidBallots.ToString());
          this.resultList.Items.Add(invalidBallots);

          ListViewItem validBallots = new ListViewItem(Resources.ValidBallots);
          validBallots.SubItems.Add(this.result.ValidBallots.ToString());
          this.resultList.Items.Add(validBallots);

          foreach (OptionResult option in this.result.Options)
          {
            ListViewItem optionBallots = new ListViewItem(option.Text);
            optionBallots.SubItems.Add(option.Result.ToString());
            this.resultList.Items.Add(optionBallots);
          }
        }
        else
        {
          Status.SetMessage("No result from server.", MessageType.Error);
        }
      }
      else
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
      }

      OnUpdateWizard();
    }

    private void GetResultComplete(VotingResult result, Exception exception)
    {
      this.exception = exception;
      this.result = result;
      this.run = false;
    }
  }
}
