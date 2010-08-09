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
using System.Drawing;
using System.Globalization;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Client
{
  public partial class StartItem : WizardItem
  {
    private bool run = false;
    private Exception exception;
    private bool canNext = false;
    private bool dnsError = false;

    public StartItem()
    {
      InitializeComponent();
    }

    public override WizardItem Next()
    {
      DirectoryInfo directory = new DirectoryInfo(Status.DataPath);
      IEnumerable<FileInfo> certificateFiles = directory.GetFiles("*.pi-cert");

      if (certificateFiles.Count() == 0)
      {
        if (this.advancedOptionsRadio.Checked)
        {
          return new ChooseCertificateItem();
        }
        else if (this.tallyOnlyRadio.Checked)
        {
          return new ListVotingsItem();
        }
        else
        {
          return new SimpleChooseCertificateItem();
        }
      }
      else if (certificateFiles.Count() == 1)
      {
        if (this.advancedOptionsRadio.Checked)
        {
          return new ChooseCertificateItem();
        }
        else if (this.tallyOnlyRadio.Checked)
        {
          return new ListVotingsItem();
        }
        else
        {
          try
          {
            Status.Certificate = Serializable.Load<Certificate>(certificateFiles.Single().FullName);
            CheckCertificateItem item = new CheckCertificateItem();
            item.PreviousItem = this;
            return item;
          }
          catch
          {
            return new ChooseCertificateItem();
          }
        }
      }
      else
      {
        if (this.tallyOnlyRadio.Checked)
        {
          return new ListVotingsItem();
        }
        else
        {
          return new ChooseCertificateItem();
        }
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
      get { return this.canNext; }
    }

    public override bool CanPrevious
    {
      get { return false; }
    }

    private void StartItem_Load(object sender, EventArgs e)
    {
    }

    private void haveCertificateRadio_CheckedChanged(object sender, EventArgs e)
    {
      OnUpdateWizard();
    }

    private void needCertificateRadio_CheckedChanged(object sender, EventArgs e)
    {
      OnUpdateWizard();
    }

    public override void Begin()
    {
      DirectoryInfo directory = new DirectoryInfo(Status.DataPath);
      IEnumerable<FileInfo> certificateFiles = directory.GetFiles("*.pi-cert");

      if (certificateFiles.Count() > 1)
      {
        this.votingRadio.Enabled = false;
        this.advancedOptionsRadio.Checked = true;
      }
      else
      {
        this.votingRadio.Checked = true;
      }
      
      if (!this.englishRadio.Checked &&
          !this.germanRadio.Checked &&
          !this.frenchRadio.Checked)
      {
        if (CultureInfo.CurrentCulture.Name.StartsWith("de"))
        {
          this.germanRadio.Checked = true;
        }
        else if (CultureInfo.CurrentCulture.Name.StartsWith("fr"))
        {
          this.frenchRadio.Checked = true;
        }
        else
        {
          this.englishRadio.Checked = true;
        }
      }

      OnUpdateWizard();

      if (!Status.VotingClient.Connected)
      {
        //IPAddress serverIpAddress = Status.ServerIpAddress;
        IPAddress serverIpAddress = IPAddress.Loopback;

        if (serverIpAddress == null)
        {
          Status.SetMessage(Resources.StartDnsError, MessageType.Error);
          this.dnsError = true;
          return;
        }

        Status.SetProgress(Resources.StartConnecting, 0d);
        this.run = true;
        Status.VotingClient.Connect(serverIpAddress, ConnectComplete);

        while (this.run)
        {
          Status.UpdateProgress();
          Thread.Sleep(10);
        }

        if (this.exception != null)
        {
          if (this.exception is SocketException)
          {
            Status.SetMessage(Resources.StartConnectError, MessageType.Error);
          }
          else
          {
            Status.SetMessage(this.exception.Message, MessageType.Error);
          }
          return;
        }

        Status.SetProgress(Resources.StartGettingCertificates, 0.5d);
        this.run = true;
        Status.VotingClient.GetCertificateStorage(Status.CertificateStorage, GetCertificateStorageComplete);

        while (this.run)
        {
          Status.UpdateProgress();
          Thread.Sleep(10);
        }

        if (this.exception == null)
        {
          Status.SetMessage(Resources.StartReady, MessageType.Success);
          this.canNext = true;
        }
        else
        {
          Status.SetMessage(this.exception.Message, MessageType.Error);
        }
      }
      else
      {
        Status.SetMessage(Resources.StartReady, MessageType.Success);
        this.canNext = true;
      }

      OnUpdateWizard();
    }

    private void GetCertificateStorageComplete(Certificate serverCertificate, Exception exception)
    {
      Status.ServerCertificate = serverCertificate;
      this.exception = exception;
      this.run = false;
    }
    
    private void ConnectComplete(Exception exception)
    {
      this.exception = exception;
      this.run = false;
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.titlelLabel.Text = Resources.StartTitle;
      this.alphaTitleLabel.Text = Resources.StartAlphaTitle + " " + GetType().Assembly.GetName().Version.ToString();
      this.alphaWarningLabel.Text = Resources.StartAlphaWarning;
      this.votingRadio.Text = Resources.StartVoting;
      this.advancedOptionsRadio.Text = Resources.StartAdvancedOptions;
      this.tallyOnlyRadio.Text = Resources.StartTallyOnly;

      if (this.canNext)
      {
        Status.SetMessage(Resources.StartReady, MessageType.Success);
      }
      else if (this.dnsError)
      {
        Status.SetMessage(Resources.StartDnsError, MessageType.Error);
      }
      else
      {
        Status.SetMessage(Resources.StartConnectError, MessageType.Error);
      }
    }

    private void englishRadio_CheckedChanged(object sender, EventArgs e)
    {
      Resources.Culture = CultureInfo.CreateSpecificCulture("en-US");
      LibraryResources.Culture = CultureInfo.CreateSpecificCulture("en-US");
      OnChangeLanguage();
    }

    private void germanRadio_CheckedChanged(object sender, EventArgs e)
    {
      Resources.Culture = CultureInfo.CreateSpecificCulture("de-DE");
      LibraryResources.Culture = CultureInfo.CreateSpecificCulture("de-DE");
      OnChangeLanguage();
    }

    private void frenchRadio_CheckedChanged(object sender, EventArgs e)
    {
      Resources.Culture = CultureInfo.CreateSpecificCulture("fr-FR");
      LibraryResources.Culture = CultureInfo.CreateSpecificCulture("fr-FR");
      OnChangeLanguage();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      base.OnPaint(e);

      e.Graphics.DrawImage(Resources.ballot_200 , 3, 3, 200, 200);
    }

    private void alphaBugLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start("https://dev.piratenpartei.ch/projects/pi-vote");
    }
  }
}
