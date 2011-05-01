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
    private bool havePrimes;

    public PrimeControl()
    {
      InitializeComponent();
    }

    public override void Prepare()
    {
      this.generateButton.Text = Resources.CreateVotingGeneratePrime;
      this.takeButton.Text = Resources.CreateVotingTakePrime;
      int primesStored = Prime.CountPregeneratedSafePrimes(Status.Controller.Status.DataPath);
      this.storedPrimesLabel.Text = string.Format(Resources.CreateVotingStored, primesStored);
      this.readyPrimeLabel.Text = Resources.CreateVotingNotReady;
      this.doneButton.Text = GuiResources.ButtonDone;
      this.cancelButton.Text = GuiResources.ButtonCancel;
    }

    private void doneButton_Click(object sender, EventArgs e)
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

    private void generateButton_Click(object sender, EventArgs e)
    {
      FindForm().Enabled = false;

      this.progressLabel.Text = Resources.CreateVotingGeneratingSafePrime;
      this.progressBar.Style = ProgressBarStyle.Marquee;

      this.run = true;
      this.worker = new Thread(GeneratePrime);
      this.worker.Start();

      while (this.run)
      {
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
      Prime.GenerateAndStoreSafePrime(Status.Controller.Status.DataPath);
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

    private void takeButton_Click(object sender, EventArgs e)
    {
      FindForm().Enabled = false;

      this.progressLabel.Text = Resources.CreateVotingVerifyingSafePrime;
      this.progressBar.Style = ProgressBarStyle.Marquee;

      this.run = true;
      this.worker = new Thread(TakePrime);
      this.worker.Start();

      while (this.run)
      {
        Application.DoEvents();
        Thread.Sleep(1);
      }

      this.progressLabel.Text = string.Empty;
      this.progressBar.Style = ProgressBarStyle.Blocks;

      int primesStored = Prime.CountPregeneratedSafePrimes(Status.Controller.Status.DataPath);
      this.storedPrimesLabel.Text = string.Format(Resources.CreateVotingStored, primesStored);

      FindForm().Enabled = true;

      if (this.havePrimes)
      {
        this.readyPrimeLabel.Text = Resources.CreateVotingReady;
        this.doneButton.Enabled = true;
        this.cancelButton.Enabled = true;
        this.generateButton.Enabled = true;
        this.takeButton.Enabled = false;
      }
      else
      {
        this.cancelButton.Enabled = true;
        this.generateButton.Enabled = true;
        this.doneButton.Enabled = false;
        this.takeButton.Enabled = true;
      }
    }

    private void cancelButton_Click(object sender, EventArgs e)
    {
      OnCloseCreateDialog();
    }
  }
}
