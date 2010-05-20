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
  /// Config file for the voting server.
  /// </summary>
  public class ServerConfig : Config
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
    /// TCP port on with the server listens
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

    /// <summary>
    /// Email subject for messages pertaining to signing requests.
    /// </summary>
    public string MailRequestSubject
    {
      get { return ReadString("MailRequestSubject", "Insert subject here."); }
    }

    /// <summary>
    /// Email body for signing request deposited messages.
    /// </summary>
    public string MailRequestDepositedBody
    {
      get { return ReadString("MailRequestDepositedBody", "Insert message here."); }
    }

    /// <summary>
    /// Email body for signing request declined messages.
    /// </summary>
    public string MailRequestDeclinedBody
    {
      get { return ReadString("MailRequestDeclinedBody", "Insert message here."); }
    }

    /// <summary>
    /// Email body for signing request approved messages.
    /// </summary>
    public string MailRequestApprovedBody
    {
      get { return ReadString("MailRequestApprovedBody", "Insert message here."); }
    }

    /// <summary>
    /// Email subject for admin new signing request messages.
    /// </summary>
    public string MailAdminNewRequestSubject
    {
      get { return ReadString("MailAdminNewRequestSubject", "Insert subject here."); }
    }

    /// <summary>
    /// Email body for admin new signing request messages.
    /// </summary>
    public string MailAdminNewRequestBody
    {
      get { return ReadString("MailAdminNewRequestBody", "Insert message here."); }
    }

    /// <summary>
    /// Email subject for admin voting status messages.
    /// </summary>
    public string MailAdminVotingStatusSubject
    {
      get { return ReadString("MailAdminVotingStatusSubject", "Insert subject here."); }
    }

    /// <summary>
    /// Email body for admin voting status messages.
    /// </summary>
    public string MailAdminVotingStatusBody
    {
      get { return ReadString("MailAdminVotingStatusBody", "Insert message here."); }
    }

    /// <summary>
    /// Email subject for admin authority activity messages.
    /// </summary>
    public string MailAdminAuthorityActivitySubject
    {
      get { return ReadString("MailAdminAuthorityActivitySubject", "Insert subject here."); }
    }

    /// <summary>
    /// Email body for admin authority activity messages.
    /// </summary>
    public string MailAdminAuthorityActivityBody
    {
      get { return ReadString("MailAdminAuthorityActivityBody", "Insert message here."); }
    }

    /// <summary>
    /// Email subject for authority action required messages.
    /// </summary>
    public string MailAuthorityActionRequiredSubject
    {
      get { return ReadString("MailAuthorityActionRequiredSubject", "Insert subject here."); }
    }

    /// <summary>
    /// Email body for authority action required messages.
    /// </summary>
    public string MailAuthorityActionRequiredBody
    {
      get { return ReadString("MailAuthorityActionRequiredBody", "Insert message here."); }
    }

    protected override void Validate()
    {
      string dummy = null;

      dummy = MailAdminAddress;
      dummy = MailAdminNewRequestBody;
      dummy = MailAdminNewRequestSubject;
      dummy = MailAuthorityAddress;
      dummy = MailRequestApprovedBody;
      dummy = MailRequestDeclinedBody;
      dummy = MailRequestSubject;
      dummy = MailServerAddress;
      dummy = MailAdminAuthorityActivityBody;
      dummy = MailAdminAuthorityActivitySubject;
      dummy = MailAdminVotingStatusBody;
      dummy = MailAdminVotingStatusSubject;
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
  }
}
