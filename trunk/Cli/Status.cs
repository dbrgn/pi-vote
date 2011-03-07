using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;
using Pirate.PiVote.Crypto;
using Pirate.PiVote.Rpc;

namespace Pirate.PiVote.Cli
{
  public class Status
  {
    public CertificateStorage CertificateStorage { get; private set; }
    
    public VotingClient Client { get; private set; }

    public Certificate Certificate { get; set; }

    public Certificate ServerCertificate { get; set; }

    public string DataPath { get; private set; }

    private const string ClientConfigFileName = "pi-vote-client.cfg";

    public IClientConfig Config { get; private set; }

    public bool Continue { get; set; }

    public Controller Controller { get; private set; }

    public IEnumerable<Group> Groups { get; set; }

    public IRemoteConfig RemoteConfig { get; set; }

    public Status(Controller controller)
    {
      Controller = controller;
      Continue = true;

      string portableDataPath = Path.Combine(Application.StartupPath, Files.PortableDataFolder);

      if (Directory.Exists(portableDataPath))
      {
        DataPath = portableDataPath;
      }
      else
      {
        DataPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Files.DataFolder);

        if (!Directory.Exists(DataPath))
        {
          Directory.CreateDirectory(DataPath);
        }
      }

      Config = new ClientConfig(Path.Combine(Application.StartupPath, ClientConfigFileName));
    }

    public void Disconnect()
    {
      if (Client != null)
      {
        Client.Close();
        Client = null;
      }
    }

    public void Connect()
    {
      if (Client != null && Client.Connected)
      {
        throw new InvalidOperationException("Already connected");
      }

      CertificateStorage = new CertificateStorage();

      if (!CertificateStorage.TryLoadRoot())
      {
        throw new InvalidOperationException("Cannot find root certificate");
      }

      Client = new VotingClient(CertificateStorage);

      var serverIpAddress = ServerEndPoint;

      if (serverIpAddress == null)
      {
        throw new InvalidOperationException("Cannot resolve host");
      }
      else
      {
        Client.Connect(serverIpAddress);
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

    public string GetGroupName(int groupId)
    {
      var groups = Groups.Where(group => group.Id == groupId);

      if (groups.Count() > 0)
      {
        return groups.First().Name.Text;
      }
      else
      {
        return "Unknown Group";
      }
    }
  }
}
