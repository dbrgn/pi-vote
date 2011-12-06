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

namespace Pirate.PiVote
{
  /// <summary>
  /// A dummy server config for testing.
  /// </summary>
  public class DummyConfig : IServerConfig
  {
    /// <summary>
    /// Connection string for the MySQL database.
    /// </summary>
    public string MySqlConnectionString
    {
      get { return "bad string"; }
    }

    /// <summary>
    /// TCP port on with the server listens.
    /// </summary>
    public int Port
    {
      get { return 4242; }
    }

    /// <summary>
    /// Number of worker threads.
    /// </summary>
    public int WorkerCount
    {
      get { return 2; }
    }

    /// <summary>
    /// Short sleep time for workers in milliseconds.
    /// </summary>
    public int WorkerShortWait
    {
      get { return 1; }
    }

    /// <summary>
    /// Long sleep time for workers in milliseconds.
    /// </summary>
    public int WorkerLongWait
    {
      get { return 100; }
    }

    /// <summary>
    /// Time between SQL keep alive queries in seconds.
    /// </summary>
    public double SqlKeepAliveTime
    {
      get { return 300d; }
    }

    /// <summary>
    /// Time until an idle client is disconnected in seconds.
    /// </summary>
    public double ClientTimeOut
    {
      get { return 120d; }
    }
    /// <summary>
    /// DNS or IP address of the mail server.
    /// </summary>
    public string MailServerAddress
    {
      get { return "smtp.b.c"; }
    }

    /// <summary>
    /// Smtp port of the mail server.
    /// </summary>
    public int MailServerPort
    {
      get { return 25; }
    }
    
    /// <summary>
    /// Email address of the admin.
    /// </summary>
    public string MailAdminAddress
    {
      get { return "a@b.c"; }
    }

    /// <summary>
    /// Email address of the authorities.
    /// </summary>
    public string MailAuthorityAddress
    {
      get { return "a@b.c"; }
    }

    /// <summary>
    /// Get the mail subject and body.
    /// </summary>
    /// <param name="mailType">Type of mail</param>
    /// <param name="logger">Where to log.</param>
    /// <returns>Subject and body</returns>
    public Tuple<string, string> GetMailText(MailType mailType, ILogger logger)
    {
      return new Tuple<string, string>("subject", "body");
    }

    /// <summary>
    /// Validate mail texts.
    /// </summary>
    /// <param name="logger">Where to log.</param>
    public void ValidateMail(ILogger logger)
    {
    }
  }
}
