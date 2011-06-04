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
using System.Threading;
using System.Windows.Forms;
using Emil.GMP;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;

namespace Pirate.PiVote.Circle.CreateVoting
{
  public partial class PrimeControl : CreateVotingControl
  {
    private bool run;
    private Thread worker;
    private bool havePrimes = false;
    private int numberCount;
    private int primeCount;

    public PrimeControl()
    {
      InitializeComponent();
    }

    public override void Prepare()
    {
      this.generateButton.Text = Resources.CreateVotingGeneratePrime;
      int primesStored = Prime.CountPregeneratedSafePrimes(Status.Controller.Status.DataPath);
      this.storedPrimesLabel.Text = string.Format(Resources.CreateVotingStored, primesStored);
      this.doneButton.Text = GuiResources.ButtonDone;
      this.cancelButton.Text = GuiResources.ButtonCancel;
      this.doneButton.Enabled = primesStored > 0;
    }

    private void doneButton_Click(object sender, EventArgs e)
    {
      FindForm().Enabled = false;

      this.progressLabel.Text = Resources.CreateVotingVerifyingSafePrime;
      this.progressBar.Style = ProgressBarStyle.Marquee;

      this.run = true;
      this.worker = new Thread(TakePrime);
      this.worker.Start();

      while (this.run)
      {
        this.progressLabel.Text = Resources.CreateVotingVerifyingSafePrime;
        Application.DoEvents();
        Thread.Sleep(1);
      }

      this.progressLabel.Text = string.Empty;
      this.progressBar.Style = ProgressBarStyle.Blocks;

      int primesStored = Prime.CountPregeneratedSafePrimes(Status.Controller.Status.DataPath);
      this.storedPrimesLabel.Text = string.Format(Resources.CreateVotingStored, primesStored);
      this.doneButton.Enabled = primesStored > 0;

      FindForm().Enabled = true;

      if (this.havePrimes)
      {
        var certificate = Status.Controller.GetAdminCertificate();

        if (DecryptPrivateKeyDialog.TryDecryptIfNessecary(certificate, GuiResources.UnlockActionCreateVoting))
        {
          try
          {
            var parameters = new VotingParameters(
              Status.Data.Title,
              Status.Data.Descrption,
              Status.Data.Url,
              Status.FromDate,
              Status.UntilDate,
              Status.VotingGroup.Id);

            foreach (var question in Status.Data.Questions)
            {
              if (question.MaxVota > 1)
              {
                for (int index = 0; index < question.MaxVota; index++)
                {
                  question.AddOption(Option.CreateAbstentionSpecial());
                }
              }
              else
              {
                question.AddOption(Option.CreateAbstention());
              }

              parameters.AddQuestion(question);
            }

            parameters.SetNumbers(Status.Prime, Status.SafePrime);
            var signedParameters = new Signed<VotingParameters>(parameters, certificate);

            Pirate.PiVote.Circle.Status.TextStatusDialog.ShowInfo(Status.Controller, FindForm());
            Status.Controller.CreateVoting(signedParameters, Status.Authorites);
          }
          catch (Exception exception)
          {
            Error.ErrorDialog.ShowError(exception);
          }
          finally
          {
            certificate.Lock();
            Pirate.PiVote.Circle.Status.TextStatusDialog.HideInfo();
            OnCloseCreateDialog();
          }
        }
      }
    }

    private void Feedback(int numberCount, int primeCount)
    {
      this.numberCount = numberCount;
      this.primeCount = primeCount;
    }

    private void generateButton_Click(object sender, EventArgs e)
    {
      FindForm().Enabled = false;

      this.progressLabel.Text = string.Format(Resources.CreateVotingGeneratingSafePrime, 0, 0);
      Application.DoEvents();
      this.progressBar.Style = ProgressBarStyle.Marquee;

      this.run = true;
      this.worker = new Thread(GeneratePrime);
      this.worker.Start();

      while (this.run)
      {
        this.progressLabel.Text = string.Format(Resources.CreateVotingGeneratingSafePrime, this.numberCount, this.primeCount);
        Application.DoEvents();
        Thread.Sleep(1);
      }

      this.progressLabel.Text = string.Empty;
      this.progressBar.Style = ProgressBarStyle.Blocks;

      int primesStored = Prime.CountPregeneratedSafePrimes(Status.Controller.Status.DataPath);
      this.storedPrimesLabel.Text = string.Format(Resources.CreateVotingStored, primesStored);

      FindForm().Enabled = true;
    }

    private void GeneratePrime()
    {
      Prime.GenerateAndStoreSafePrime(Status.Controller.Status.DataPath, Feedback);
      this.run = false;
    }

    private void TakePrime()
    {
      BigInt prime;
      BigInt safePrime;
      this.havePrimes = Prime.TryLoadPregeneratedSafePrime(Status.Controller.Status.DataPath, BaseParameters.PrimeBits, out prime, out safePrime);
      Status.SafePrime = safePrime;
      Status.Prime = prime;
      this.run = false;
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      OnCloseCreateDialog();
    }
  }
}
