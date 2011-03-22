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
  /// Server config interface.
  /// </summary>
  public interface IServerConfig
  {
    /// <summary>
    /// Time until an idle client is disconnected in seconds.
    /// </summary>
    double ClientTimeOut { get; }

    /// <summary>
    /// Email address of the admin.
    /// </summary>
    string MailAdminAddress { get; }

    /// <summary>
    /// Email body for admin authority activity messages.
    /// </summary>
    string MailAdminAuthorityActivityBody { get; }

    /// <summary>
    /// Email subject for admin authority activity messages.
    /// </summary>
    string MailAdminAuthorityActivitySubject { get; }

    /// <summary>
    /// Email body for admin new signing request messages.
    /// </summary>
    string MailAdminNewRequestBody { get; }

    /// <summary>
    /// Email subject for admin new signing request messages.
    /// </summary>
    string MailAdminNewRequestSubject { get; }

    /// <summary>
    /// Email body for admin new signing request messages.
    /// </summary>
    string MailAdminVotingStatusBody { get; }

    /// <summary>
    /// Email subject for admin voting status messages.
    /// </summary>
    string MailAdminVotingStatusSubject { get; }

    /// <summary>
    /// Email subject for authority action required messages.
    /// </summary>
    string MailAuthorityActionRequiredBody { get; }

    /// <summary>
    /// Email body for authority action required messages.
    /// </summary>
    string MailAuthorityActionRequiredSubject { get; }

    /// <summary>
    /// Email address of the authorities.
    /// </summary>
    string MailAuthorityAddress { get; }

    /// <summary>
    /// Email body for signing request approved messages.
    /// </summary>
    string MailRequestApprovedBody { get; }

    /// <summary>
    /// Email body for signing request declined messages.
    /// </summary>
    string MailRequestDeclinedBody { get; }

    /// <summary>
    /// Email body for signing request deposited messages.
    /// </summary>
    string MailRequestDepositedBody { get; }

    /// <summary>
    /// Email subject for messages pertaining to signing requests.
    /// </summary>
    string MailRequestSubject { get; }

    /// <summary>
    /// DNS or IP address of the mail server.
    /// </summary>
    string MailServerAddress { get; }

    /// <summary>
    /// Smtp port of the mail server.
    /// </summary>
    int MailServerPort { get; }

    /// <summary>
    /// Connection string for the MySQL database.
    /// </summary>
    string MySqlConnectionString { get; }

    /// <summary>
    /// TCP port on with the server listens.
    /// </summary>
    int Port { get; }

    /// <summary>
    /// Time between SQL keep alive queries in seconds.
    /// </summary>
    double SqlKeepAliveTime { get; }

    /// <summary>
    /// Number of worker threads.
    /// </summary>
    int WorkerCount { get; }

    /// <summary>
    /// Long sleep time for workers in milliseconds.
    /// </summary>
    int WorkerLongWait { get; }

    /// <summary>
    /// Short sleep time for workers in milliseconds.
    /// </summary>
    int WorkerShortWait { get; }
  }
}
