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
using System.Reflection;
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

      this.versionLabel.Text = Assembly.GetExecutingAssembly().GetName().Version.ToString();

      OnUpdateWizard();

      if (!Status.VotingClient.Connected)
      {
        IPEndPoint serverEndPoint = Status.ServerEndPoint;

        if (serverEndPoint == null)
        {
          Status.SetMessage(Resources.StartDnsError, MessageType.Error);
          this.dnsError = true;
          return;
        }

        Status.SetProgress(Resources.StartConnecting, 0d);
        this.run = true;
        Status.VotingClient.Connect(serverEndPoint, ConnectComplete);

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

        Status.SetProgress(Resources.StartGettingConfig, 0.3d);
        this.run = true;
        Status.VotingClient.GetConfig(GetConfigComplete);

        while (this.run)
        {
          Status.UpdateProgress();
          Thread.Sleep(10);
        }

        if (this.exception == null)
        {
          UpdateMessages();
          this.canNext = true;

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
            CheckUpdate();
            this.canNext = true;
          }
          else
          {
            Status.SetMessage(this.exception.Message, MessageType.Error);
          }
        }
        else
        {
          Status.SetMessage(this.exception.Message, MessageType.Error);
        }
      }
      else
      {
        Status.SetMessage(Resources.StartReady, MessageType.Success);
        CheckUpdate();
        this.canNext = true;
      }

      OnUpdateWizard();
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

    private void alphaBugLink_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
    {
      System.Diagnostics.Process.Start(Status.RemoteConfig.Url);
    }

    private void CheckUpdate()
    {
      var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version;
      int major = 0;
      int minor = 0;
      int build = 0;
      int revision = 0;

      string[] parts = Status.RemoteConfig.UpdateVersion.Split(new string[] { "." }, StringSplitOptions.RemoveEmptyEntries);
      if (parts.Length == 4)
      {
        int.TryParse(parts[0], out major);
        int.TryParse(parts[1], out minor);
        int.TryParse(parts[2], out build);
        int.TryParse(parts[3], out revision);
      }

      if (IsUpdateVersionNewer(assemblyVersion, major, minor, build, revision))
      {
        string version = string.Format("{0}.{1}.{2}.{3}", major, minor, build, revision);
        string text = string.Format(Resources.UpdateDialogText, assemblyVersion.ToString(), version);

        if (MessageForm.Show(
          text, 
          Resources.UpdateDialogTitle, 
          MessageBoxButtons.YesNo, 
          MessageBoxIcon.Information, 
          DialogResult.Yes)
          == DialogResult.Yes)
        {
          System.Diagnostics.Process.Start(Status.RemoteConfig.UpdateUrl);
          System.Diagnostics.Process.GetCurrentProcess().Kill();
        }
      }
    }

    private static bool IsUpdateVersionNewer(Version assemblyVersion, int major, int minor, int build, int revision)
    {
      if (major > assemblyVersion.Major)
      {
        return true;
      }
      else if (major == assemblyVersion.Major)
      {
        if (minor > assemblyVersion.Minor)
        {
          return true;
        }
        else if (minor == assemblyVersion.Minor)
        {
          if (build > assemblyVersion.Build)
          {
            return true;
          }
          else if (build == assemblyVersion.Build)
          {
            if (revision > assemblyVersion.Revision)
            {
              return true;
            }
          }
        }
      }

      return false;
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
