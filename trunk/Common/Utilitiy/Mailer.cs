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
using System.Net.Mail;

namespace Pirate.PiVote
{
  public static class Mailer
  {
    public const string Sender = "pivote@piratenpartei.ch";

    public static bool TrySend(string recipient, string subject, string body, Logger logger)
    {
      if (logger == null)
        throw new ArgumentNullException("logger");

      logger.Log(LogLevel.Debug, "Sending mail to {0}.", recipient);

      try
      {
        MailMessage message = new MailMessage(Sender, recipient, subject, body);

        SmtpClient client = new SmtpClient("wally.piratenpartei.ch", 25);
        client.Send(message);

        logger.Log(LogLevel.Debug, "Mail to {0} sent.", recipient);

        return true;
      }
      catch (Exception exception)
      {
        logger.Log(LogLevel.Debug, "Sending mail to {0} failed: {1}", recipient, exception.ToString());
        return false;
      }
    }
  }
}
