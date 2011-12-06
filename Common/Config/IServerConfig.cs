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
  /// Type of sendable mails with preprepared text files.
  /// </summary>
  public enum MailType
  {
    /// <summary>
    /// Notifies the admin about an authority done her job.
    /// {0} Voting id an title
    /// {1} Id and full name of the authority
    /// {2} Activity string
    /// </summary>
    AdminAuthorityActivity,

    /// <summary>
    /// Report an error to the admin.
    /// {0} Exception string
    /// </summary>
    AdminErrorReport,

    /// <summary>
    /// Notify the admin about a new certificate signing request.
    /// {0} Email address on the request
    /// {1} Certificate id on the request
    /// {2} Certificate type on the request
    /// </summary>
    AdminNewRequest,

    /// <summary>
    /// Notifies the admin that the status of a voting changed.
    /// {0} Id and title of the voting
    /// {1} New status of the voting
    /// </summary>
    AdminStatusChanged,

    /// <summary>
    /// Status report to the admin.
    /// {0} Status report text
    /// </summary>
    AdminStatusReport,

    /// <summary>
    /// Remind the admin to renew the Certificate Revocation List.
    /// {0} Date the last CRL runs out.
    /// </summary>
    AdminCrlGreen,

    /// <summary>
    /// Remind the admin to renew the Certificate Revocation List.
    /// {0} Date the last CRL runs out.
    /// </summary>
    AdminCrlOrange,

    /// <summary>
    /// Remind the admin to renew the Certificate Revocation List.
    /// {0} Date the last CRL runs out.
    /// </summary>
    AdminCrlRed,

    /// <summary>
    /// Warns the autority to create shares.
    /// {0} Id and title of the voting
    /// </summary>
    AuthorityCreateSharesGreen,

    /// <summary>
    /// Warns the autority to create shares.
    /// {0} Id and title of the voting
    /// </summary>
    AuthorityCreateSharesOrange,

    /// <summary>
    /// Warns the autority to create shares.
    /// {0} Id and title of the voting
    /// </summary>
    AuthorityCreateSharesRed,

    /// <summary>
    /// Warns the autority to verify shares.
    /// {0} Id and title of the voting
    /// </summary>
    AuthorityVerifySharesGreen,

    /// <summary>
    /// Warns the autority to verify shares.
    /// {0} Id and title of the voting
    /// </summary>
    AuthorityVerifySharesOrange,

    /// <summary>
    /// Warns the autority to verify shares.
    /// {0} Id and title of the voting
    /// </summary>
    AuthorityVerifySharesRed,

    /// <summary>
    /// Warns the autority to decipher.
    /// {0} Id and title of the voting
    /// </summary>
    AuthorityDecipherGreen,

    /// <summary>
    /// Warns the autority to decipher.
    /// {0} Id and title of the voting
    /// </summary>
    AuthorityDecipherOrange,
    
    /// <summary>
    /// Warns the autority to decipher.
    /// {0} Id and title of the voting
    /// </summary>
    AuthorityDecipherRed,

    /// <summary>
    /// Notifes the voter that his request was approved.
    /// {0} Email address on the request
    /// {1} Certificate id on the request
    /// {2} Certificate type on the request in English
    /// {3} Certificate type on the request in German
    /// {4} Certificate type on the request in French
    /// </summary>
    VoterRequestApproved,

    /// <summary>
    /// Notifies the voter that his request was declined.
    /// {0} Email address on the request
    /// {1} Certificate id on the request
    /// {2} Certificate type on the request in English
    /// {3} Certificate type on the request in German
    /// {4} Certificate type on the request in French
    /// {5} Reason for declining
    /// </summary>
    VoterRequestDeclined,

    /// <summary>
    /// Notifies the voter that his request was stored on the server.
    /// {0} Email address on the request
    /// {1} Certificate id on the request
    /// {2} Certificate type on the request in English
    /// {3} Certificate type on the request in German
    /// {4} Certificate type on the request in French
    /// </summary>
    VoterRequestDeposited,

    /// <summary>
    /// Tells a voter that he can still vote.
    /// {0} Voting title in English
    /// {1} Voting title in German
    /// {2} Voting title in French
    /// {3} Closing date in English
    /// {4} Closing date in German
    /// {5} Closing date in French
    /// </summary>
    VoterRequestCanStill,

    /// <summary>
    /// Tells a voter that he has a last chance to vote.
    /// {0} Voting title in English
    /// {1} Voting title in German
    /// {2} Voting title in French
    /// {3} Closing date in English
    /// {4} Closing date in German
    /// {5} Closing date in French
    /// </summary>
    VoterRequestLastChance
  }

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

    /// <summary>
    /// Get the mail subject and body.
    /// </summary>
    /// <param name="mailType">Type of mail</param>
    /// <param name="logger">Where to log.</param>
    /// <returns>Subject and body</returns>
    Tuple<string, string> GetMailText(MailType mailType, ILogger logger);

    /// <summary>
    /// Validate mail texts.
    /// </summary>
    /// <param name="logger">Where to log.</param>
    void ValidateMail(ILogger logger);
  }
}
