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
using System.Net;
using System.Net.Sockets;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Client
{
  public class WizardStatus
  {
    private const string ClientConfigFileName = "pi-vote-client.cfg"; 

    public IClientConfig Config { get; private set; }

    public IRemoteConfig RemoteConfig { get; set; }

    public IEnumerable<Group> Groups { get; set; }

    public CertificateStorage CertificateStorage { get; set; }

    public Certificate ServerCertificate { get; set; }

    public Certificate Certificate { get; set; }

    public string CertificateFileName { get; set; }

    public VotingClient VotingClient { get; set; }

    public string DataPath { get { return this.dataPath; } }

    private string dataPath;

    private Message message;

    private Progress progress;

    public string FirstName = null;

    public string FamilyName = null;

    public Certificate CaCertificate
    {
      get
      {
        IEnumerable<Certificate> caCertificates =
          CertificateStorage.Certificates
          .Where(certificate => certificate is CACertificate && 
            certificate.Validate(CertificateStorage) == CertificateValidationResult.Valid);

        IEnumerable<Certificate> caLeaveCertificates = caCertificates.
          Where(certificate => !caCertificates
            .Any(otherCert => otherCert.Signatures
              .Any(signature => signature.SignerId == certificate.Id)));

        return caLeaveCertificates
          .OrderBy(certificate => certificate.CreationDate)
          .LastOrDefault();
      }
    }

    public IPEndPoint ServerEndPoint
    {
      get
      {
        try
        {
#if LOCAL
          IPAddress ipAddress = IPAddress.Loopback;
#else
          IPAddress ipAddress = Dns.GetHostEntry(Config.ServerAddress).AddressList.First();
#endif

          return new IPEndPoint(ipAddress, Config.ServerPort);
        }
        catch (System.Net.Sockets.SocketException)
        {
          return null;
        }
      }
    }

    public IPEndPoint ProxyEndPoint
    {
      get
      {
        try
        {
#if LOCAL
          return null;
#else
          if (!Config.ProxyAddress.IsNullOrEmpty() &&
              Config.ProxyPort > 0)
          {
            IPAddress ipAddress = Dns.GetHostEntry(Config.ProxyAddress).AddressList.First();

            return new IPEndPoint(ipAddress, Config.ProxyPort);
          }
          else
          {
            return null;
          }
#endif
        }
        catch (System.Net.Sockets.SocketException)
        {
          return null;
        }
      }
    }
    
    public WizardStatus(Message message, Progress progress)
    {
      this.message = message;
      this.progress = progress;
      this.progress.Status = this;

      string portableDataPath = Path.Combine(Application.StartupPath, Files.PortableDataFolder);

      if (Directory.Exists(portableDataPath))
      {
        this.dataPath = portableDataPath;
      }
      else
      {
        this.dataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Files.DataFolder);

        if (!Directory.Exists(this.dataPath))
        {
          Directory.CreateDirectory(this.dataPath);
        }
      }

      Config = new ClientConfig(Path.Combine(Application.StartupPath, ClientConfigFileName));
    }

    public void SetMessage(string message, MessageType type)
    {
      this.message.Visible = true;
      this.progress.Visible = false;
      this.message.Set(message, type);
    }

    public void SetNone()
    {
      this.message.Visible = false;
      this.progress.Visible = false;
    }

    public void UpdateProgress()
    {
      this.message.Visible = false;
      this.progress.Visible = true;
      this.progress.UpdateProgress();
      Application.DoEvents();
    }

    public void SetProgress(string message, double progress)
    {
      this.message.Visible = false;
      this.progress.Visible = true;
      this.progress.Set(message, progress);
    }

    public string GetGroupName(int groupId)
    {
      var groups = Groups.Where(group => group.Id == groupId);

      if (groups.Count() > 0)
      {
        return groups.First().Name.Text;
      }
      else
      {
        return Resources.ChooseCertificateGroupNameUnknown;
      }
    }
  }
}
