/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Gui;
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
    private bool done = false;

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
            Status.CertificateFileName = certificateFiles.Single().FullName;
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
      get { return this.done; }
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
      CheckCertificates();
      SetDefaultLanguage();

      OnUpdateWizard();

      if (!Status.VotingClient.Connected)
      {
        if (TryConnect() &&
            TryGetConfig() &&
            TryGetCertificateStorage())
        {
          UpdateChecker.CheckUpdate(
            Status.RemoteConfig, 
            Resources.UpdateDialogTitle, 
            Resources.UpdateDialogText);
          this.canNext = true;
        }
      }
      else
      {
        Status.SetMessage(Resources.StartReady, MessageType.Success);
        UpdateChecker.CheckUpdate(
          Status.RemoteConfig,
          Resources.UpdateDialogTitle,
          Resources.UpdateDialogText);
        this.canNext = true;
      }

      this.done = true;

      OnUpdateWizard();
    }

    private bool TryGetConfig()
    {
      Status.SetProgress(Resources.StartGettingConfig, 0.3d);
      this.run = true;
      var assembly = Assembly.GetExecutingAssembly();
      var clientName = assembly.GetName().Name;
      var clientVersion = assembly.GetName().Version.ToString();

      Status.VotingClient.GetConfig(clientName, clientVersion, GetConfigComplete);

      while (this.run)
      {
        Status.UpdateProgress();
        Thread.Sleep(10);
      }

      if (this.exception == null)
      {
        UpdateMessages();
        return true;
      }
      else
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
        return false;
      }
    }

    private bool TryGetCertificateStorage()
    {
      Status.SetProgress(Resources.StartGettingCertificates, 0.6d);
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
        return true;
      }
      else if (this.exception is PiException &&
        ((PiException)this.exception).Code == ExceptionCode.ServerCertificateInvalid)
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
        return true;
      }
      else
      {
        Status.SetMessage(this.exception.Message, MessageType.Error);
        return false;
      }
    }

    private bool TryConnect()
    {
      IPEndPoint serverEndPoint = Status.ServerEndPoint;
      IPEndPoint proxyEndPoint = Status.ProxyEndPoint;

      if (serverEndPoint == null)
      {
        Status.SetMessage(Resources.StartDnsError, MessageType.Error);
        this.dnsError = true;
        return false;
      }

      Status.SetProgress(Resources.StartConnecting, 0d);
      this.run = true;
      Status.VotingClient.Connect(serverEndPoint, proxyEndPoint, ConnectComplete);

      while (this.run)
      {
        Status.UpdateProgress();
        Thread.Sleep(10);
      }

      if (this.exception == null)
      {
        return true;
      }
      else
      {
        if (this.exception is SocketException)
        {
          Status.SetMessage(Resources.StartConnectError, MessageType.Error);
        }
        else
        {
          Status.SetMessage(this.exception.Message, MessageType.Error);
        }

        return false;
      }
    }

    private void CheckCertificates()
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
    }

    private void SetDefaultLanguage()
    {
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

      this.versionLabel.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();
    }

    private void GetConfigComplete(IRemoteConfig config, IEnumerable<Group> groups, Exception exception)
    {
      Status.RemoteConfig = config;
      Status.Groups = groups;
      this.exception = exception;
      this.run = false;
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

    private void UpdateMessages()
    {
      if (Status.RemoteConfig != null)
      {
        this.titlelLabel.Text = Status.RemoteConfig.SystemName.Text;
        this.welcomeLabel.Text = Status.RemoteConfig.WelcomeMessage.Text;
        this.urlLink.Text = Status.RemoteConfig.Url;

        if (Status.RemoteConfig.Image.Length > 0)
        {
          MemoryStream bitmapStream = new MemoryStream(Status.RemoteConfig.Image);
          this.tileImage.Image = new Bitmap(bitmapStream);
        }
      }
      else
      {
        this.titlelLabel.Text = string.Empty;
        this.welcomeLabel.Text = string.Empty;
        this.urlLink.Text = string.Empty;
        this.tileImage.Image = null;
      }
    }

    public override void UpdateLanguage()
    {
      base.UpdateLanguage();

      this.votingRadio.Text = Resources.StartVoting;
      this.advancedOptionsRadio.Text = Resources.StartAdvancedOptions;
      this.tallyOnlyRadio.Text = Resources.StartTallyOnly;
      UpdateMessages();

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

    private void SetCulture(CultureInfo culture)
    {
      Resources.Culture = culture;
      GuiResources.Culture = culture;
      LibraryResources.Culture = culture;
    }

    private void englishRadio_CheckedChanged(object sender, EventArgs e)
    {
      SetCulture(CultureInfo.CreateSpecificCulture("en-US"));
      OnChangeLanguage();
    }

    private void germanRadio_CheckedChanged(object sender, EventArgs e)
    {
      SetCulture(CultureInfo.CreateSpecificCulture("de-DE"));
      OnChangeLanguage();
    }

    private void frenchRadio_CheckedChanged(object sender, EventArgs e)
    {
      SetCulture(CultureInfo.CreateSpecificCulture("fr-FR"));
      OnChangeLanguage();
    }

    private void alphaBugLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start(Status.RemoteConfig.Url);
    }

    private void selectPanel_Resize(object sender, EventArgs e)
    {
      this.languagePanel.Top = this.selectPanel.Height / 2 - this.languagePanel.Height / 2;
      this.optionPanel.Top = this.selectPanel.Height / 2 - this.optionPanel.Height / 2;

      int border = 20;
      this.languagePanel.Left = Math.Min(border, this.selectPanel.Width - -this.languagePanel.Width - this.optionPanel.Width - border);
      this.optionPanel.Left = this.languagePanel.Left + this.languagePanel.Width;
    }
  }
}
