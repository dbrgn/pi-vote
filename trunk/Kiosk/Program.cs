using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Pirate.PiVote.Kiosk
{
  public static class Program
  {
    /// <summary>
    /// The main entry point for the application.
    /// </summary>
    [STAThread]
    public static void Main()
    {
      var ks = new KioskServer();
      ks.CertificateStorage = new Crypto.CertificateStorage();
      var ts = new TcpServer(ks);
      ts.Start();
      var c = new Client();
      c.Connect(System.Net.IPAddress.Parse("127.0.0.1"));
      var cs = c.FetchCertificateStroage();

      Console.WriteLine(cs.Certificates.Count());
      Console.ReadLine();

      Application.EnableVisualStyles();
      Application.SetCompatibleTextRenderingDefault(false);
      Application.Run(new Form1());
    }
  }
}
