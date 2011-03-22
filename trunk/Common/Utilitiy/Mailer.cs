/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace Pirate.PiVote
{
  /// <summary>
  /// Email sending engine.
  /// </summary>
  public class Mailer
  {
    /// <summary>
    /// Configuration of the server.
    /// </summary>
    private ServerConfig serverConfig;

    /// <summary>
    /// Logs messages to file.
    /// </summary>
    private Logger logger;

    /// <summary>
    /// Creates a new email sending engine.
    /// </summary>
    /// <param name="serverConfig">Configuration of the server.</param>
    /// <param name="logger">Logs messages to file.</param>
    public Mailer(ServerConfig serverConfig, Logger logger)
    {
      if (serverConfig == null)
        throw new ArgumentNullException("serverConfig");
      if (logger == null)
        throw new ArgumentNullException("logger");
      
      this.serverConfig = serverConfig;
      this.logger = logger;
    }

    /// <summary>
    /// Tries to send an email message.
    /// </summary>
    /// <param name="recipient">Intended recipient of the message.</param>
    /// <param name="subject">Subject of the message.</param>
    /// <param name="body">Body of the message.</param>
    /// <returns>Successful sending?</returns>
    public bool TrySend(string recipient, string subject, string body)
    {
      return TrySend(new string[] { recipient }, subject, body);
    }

    /// <summary>
    /// Tries to send an email message.
    /// </summary>
    /// <param name="recipients">Intended recipients of the message.</param>
    /// <param name="subject">Subject of the message.</param>
    /// <param name="body">Body of the message.</param>
    /// <returns>Successful sending?</returns>
    public bool TrySend(IEnumerable<string> recipients, string subject, string body)
    {
      if (recipients == null)
        throw new ArgumentNullException("recipients");
      if (subject == null)
        throw new ArgumentNullException("subject");
      if (body == null)
        throw new ArgumentNullException("body");

      List<string> goodRecipients = new List<string>();
      foreach (string recipient in recipients)
      {
        if (recipient.IsNullOrEmpty())
        {
          this.logger.Log(LogLevel.Warning, "Recpient is null or empty.");
        }
        else if (!Mailer.IsEmailAddressValid(recipient))
        {
          this.logger.Log(LogLevel.Warning, "Recpient {0} is not valid.", recipient);
        }
        else
        {
          goodRecipients.Add(recipient);
        }
      }

      if (goodRecipients.Count > 0)
      {
        this.logger.Log(LogLevel.Debug, "Sending mail to {0}.", string.Join(", ", goodRecipients.ToArray()));

        try
        {
          MailMessage message = new MailMessage();
          message.From = new MailAddress(this.serverConfig.MailAdminAddress);
          message.Subject = subject;
          message.Body = body;
          goodRecipients.Foreach(recipient => message.To.Add(recipient));

          SmtpClient client = new SmtpClient(this.serverConfig.MailServerAddress, this.serverConfig.MailServerPort);
          client.Send(message);

          this.logger.Log(LogLevel.Debug, "Mail to {0} sent.", string.Join(", ", recipients.ToArray()));

          return true;
        }
        catch (Exception exception)
        {
          this.logger.Log(LogLevel.Warning, "Sending mail to {0} failed: {1}", string.Join(", ", recipients.ToArray()), exception.ToString());
          return false;
        }
      }
      else
      {
        this.logger.Log(LogLevel.Warning, "Sending no mail since no acceptable recipient found.");
        return false;
      }
    }

    /// <summary>
    /// Checks weather an email address is valid.
    /// </summary>
    /// <param name="emailAddress">Email address to check.</param>
    /// <returns>Is it valid?</returns>
    public static bool IsEmailAddressValid(string emailAddress)
    {
      string emailPatttern = @"^(([^<>()[\]\\.,;:\s@\""]+"
         + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
         + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
         + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
         + @"[a-zA-Z]{2,}))$";
      Regex emailRegex = new Regex(emailPatttern);

      return emailRegex.IsMatch(emailAddress);
    }
  }
}
