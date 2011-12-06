/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text;
using System.IO;
using System.Globalization;
using System.Windows.Forms;

namespace Pirate.PiVote
{
  /// <summary>
  /// Config file for the voting server.
  /// </summary>
  public class ServerConfig : Config, IServerConfig
  {
    public ServerConfig(string fileName)
      : base(fileName)
    { }

    /// <summary>
    /// Connection string for the MySQL database.
    /// </summary>
    public string MySqlConnectionString
    {
      get { return ReadString("MySqlConnectionString", null); }
    }

    /// <summary>
    /// TCP port on with the server listens.
    /// </summary>
    public int Port
    {
      get { return ReadInt32("Port", 4242); }
    }

    /// <summary>
    /// Number of worker threads.
    /// </summary>
    public int WorkerCount
    {
      get { return ReadInt32("WorkerCount", 2); }
    }

    /// <summary>
    /// Short sleep time for workers in milliseconds.
    /// </summary>
    public int WorkerShortWait
    {
      get { return ReadInt32("WorkerShortWait", 1); }
    }

    /// <summary>
    /// Long sleep time for workers in milliseconds.
    /// </summary>
    public int WorkerLongWait
    {
      get { return ReadInt32("WorkerLongWait", 100); }
    }

    /// <summary>
    /// Time between SQL keep alive queries in seconds.
    /// </summary>
    public double SqlKeepAliveTime
    {
      get { return ReadDouble("SqlKeepAliveTime", 300d); }
    }

    /// <summary>
    /// Time until an idle client is disconnected in seconds.
    /// </summary>
    public double ClientTimeOut
    {
      get { return ReadDouble("ClientTimeOut", 120d); }
    }
    /// <summary>
    /// DNS or IP address of the mail server.
    /// </summary>
    public string MailServerAddress
    {
      get { return ReadString("MailServerAddress", "smtp.b.c"); }
    }

    /// <summary>
    /// Smtp port of the mail server.
    /// </summary>
    public int MailServerPort
    {
      get { return ReadInt32("MailServerPort", 25); }
    }
    
    /// <summary>
    /// Email address of the admin.
    /// </summary>
    public string MailAdminAddress
    {
      get { return ReadString("MailAdminAddress", "a@b.c"); }
    }

    /// <summary>
    /// Email address of the authorities.
    /// </summary>
    public string MailAuthorityAddress
    {
      get { return ReadString("MailAuthorityAddress", "a@b.c"); }
    }

    protected override void Validate()
    {
      string dummy = null;

      dummy = MailAdminAddress;
      dummy = MailAuthorityAddress;
      dummy = MailServerAddress;
      dummy = MailServerPort.ToString();

      dummy = Port.ToString();
      dummy = WorkerCount.ToString();
      dummy = WorkerShortWait.ToString();
      dummy = WorkerLongWait.ToString();
      dummy = SqlKeepAliveTime.ToString();
      dummy = ClientTimeOut.ToString();

      Save();

      if (MySqlConnectionString.IsNullOrEmpty())
      {
        Save();
        throw new InvalidDataException("The MySQL connection string in the config file is not valid");
      }
    }

    /// <summary>
    /// Validate mail texts.
    /// </summary>
    /// <param name="logger">Where to log.</param>
    public void ValidateMail(ILogger logger)
    {
      foreach (MailType mailType in Enum.GetValues(typeof(MailType)))
      {
        GetMailText(mailType, logger);
      }
    }

    /// <summary>
    /// Get the mail subject and body.
    /// </summary>
    /// <param name="mailType">Type of mail</param>
    /// <param name="logger">Log errors to</param>
    /// <returns>Subject and body</returns>
    public Tuple<string, string> GetMailText(MailType mailType, ILogger logger)
    {
      string file = Path.Combine(Path.Combine(Application.StartupPath, "messages"), mailType.ToString());

      if (File.Exists(file))
      {
        string text = File.ReadAllText(file, Encoding.UTF8);

        if (text.Contains(Environment.NewLine))
        {
          string subject = text.Substring(0, text.IndexOf(Environment.NewLine));
          string body = text.Substring(text.IndexOf(Environment.NewLine) + Environment.NewLine.Length);
          return new Tuple<string, string>(subject, body);
        }
        else
        {
          return new Tuple<string, string>("Unknown PiVote Message", text);
        }
      }
      else
      {
        logger.Log(LogLevel.Warning, "Mail message file {0} not present.", file);
        return new Tuple<string, string>("Unknown PiVote Message", "This this an unknown message from PiVote. Please contact your administrator.");
      }
    }
  }
}
