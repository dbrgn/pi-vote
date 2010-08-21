/*
 *  <project description>
 * 
 *  Copyright (c) 2008-2009 Stefan Thöni <stefan@savvy.ch> 
 *  <BSD Like license>
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
    /// Email subject for messages pertaining to signing requests.
    /// </summary>
    public string MailRequestSubject
    {
      get { return "Insert subject here."; }
    }

    /// <summary>
    /// Email body for signing request deposited messages.
    /// </summary>
    public string MailRequestDepositedBody
    {
      get { return "Insert subject here."; }
    }

    /// <summary>
    /// Email body for signing request declined messages.
    /// </summary>
    public string MailRequestDeclinedBody
    {
      get { return "Insert subject here."; }
    }

    /// <summary>
    /// Email body for signing request approved messages.
    /// </summary>
    public string MailRequestApprovedBody
    {
      get { return "Insert subject here."; }
    }

    /// <summary>
    /// Email subject for admin new signing request messages.
    /// </summary>
    public string MailAdminNewRequestSubject
    {
      get { return "Insert subject here."; }
    }

    /// <summary>
    /// Email body for admin new signing request messages.
    /// </summary>
    public string MailAdminNewRequestBody
    {
      get { return "Insert subject here."; }
    }

    /// <summary>
    /// Email subject for admin voting status messages.
    /// </summary>
    public string MailAdminVotingStatusSubject
    {
      get { return "Insert subject here."; }
    }

    /// <summary>
    /// Email body for admin voting status messages.
    /// </summary>
    public string MailAdminVotingStatusBody
    {
      get { return "Insert subject here."; }
    }

    /// <summary>
    /// Email subject for admin authority activity messages.
    /// </summary>
    public string MailAdminAuthorityActivitySubject
    {
      get { return "Insert subject here."; }
    }

    /// <summary>
    /// Email body for admin authority activity messages.
    /// </summary>
    public string MailAdminAuthorityActivityBody
    {
      get { return "Insert subject here."; }
    }

    /// <summary>
    /// Email subject for authority action required messages.
    /// </summary>
    public string MailAuthorityActionRequiredSubject
    {
      get { return "Insert subject here."; }
    }

    /// <summary>
    /// Email body for authority action required messages.
    /// </summary>
    public string MailAuthorityActionRequiredBody
    {
      get { return "Insert subject here."; }
    }
  }
}
