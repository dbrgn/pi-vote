using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Pirate.PiVote.Utilitiy
{
  public class Syslog : IDisposable
  {
    /// <summary>
    /// syslog severities
    /// </summary>
    /// <remarks>
    /// <para>
    /// The log4net Level maps to a syslog severity using the
    /// <see cref="LocalSyslogAppender.AddMapping"/> method and the <see cref="LevelSeverity"/>
    /// class. The severity is set on <see cref="LevelSeverity.Severity"/>.
    /// </para>
    /// </remarks>
    public enum SyslogSeverity
    {
      /// <summary>
      /// system is unusable
      /// </summary>
      Emergency = 0,

      /// <summary>
      /// action must be taken immediately
      /// </summary>
      Alert = 1,

      /// <summary>
      /// critical conditions
      /// </summary>
      Critical = 2,

      /// <summary>
      /// error conditions
      /// </summary>
      Error = 3,

      /// <summary>
      /// warning conditions
      /// </summary>
      Warning = 4,

      /// <summary>
      /// normal but significant condition
      /// </summary>
      Notice = 5,

      /// <summary>
      /// informational
      /// </summary>
      Informational = 6,

      /// <summary>
      /// debug-level messages
      /// </summary>
      Debug = 7
    };
    
    /// <summary>
    /// syslog facilities
    /// </summary>
    /// <remarks>
    /// <para>
    /// The syslog facility defines which subsystem the logging comes from.
    /// This is set on the <see cref="Facility"/> property.
    /// </para>
    /// </remarks>
    public enum SyslogFacility
    {
      /// <summary>
      /// kernel messages
      /// </summary>
      Kernel = 0,

      /// <summary>
      /// random user-level messages
      /// </summary>
      User = 1,

      /// <summary>
      /// mail system
      /// </summary>
      Mail = 2,

      /// <summary>
      /// system daemons
      /// </summary>
      Daemons = 3,

      /// <summary>
      /// security/authorization messages
      /// </summary>
      Authorization = 4,

      /// <summary>
      /// messages generated internally by syslogd
      /// </summary>
      Syslog = 5,

      /// <summary>
      /// line printer subsystem
      /// </summary>
      Printer = 6,

      /// <summary>
      /// network news subsystem
      /// </summary>
      News = 7,

      /// <summary>
      /// UUCP subsystem
      /// </summary>
      Uucp = 8,

      /// <summary>
      /// clock (cron/at) daemon
      /// </summary>
      Clock = 9,

      /// <summary>
      /// security/authorization  messages (private)
      /// </summary>
      Authorization2 = 10,

      /// <summary>
      /// ftp daemon
      /// </summary>
      Ftp = 11,

      /// <summary>
      /// NTP subsystem
      /// </summary>
      Ntp = 12,

      /// <summary>
      /// log audit
      /// </summary>
      Audit = 13,

      /// <summary>
      /// log alert
      /// </summary>
      Alert = 14,

      /// <summary>
      /// clock daemon
      /// </summary>
      Clock2 = 15,

      /// <summary>
      /// reserved for local use
      /// </summary>
      Local0 = 16,

      /// <summary>
      /// reserved for local use
      /// </summary>
      Local1 = 17,

      /// <summary>
      /// reserved for local use
      /// </summary>
      Local2 = 18,

      /// <summary>
      /// reserved for local use
      /// </summary>
      Local3 = 19,

      /// <summary>
      /// reserved for local use
      /// </summary>
      Local4 = 20,

      /// <summary>
      /// reserved for local use
      /// </summary>
      Local5 = 21,

      /// <summary>
      /// reserved for local use
      /// </summary>
      Local6 = 22,

      /// <summary>
      /// reserved for local use
      /// </summary>
      Local7 = 23
    }
    
    /// <summary>
    /// Marshaled handle to the identity string. We have to hold on to the
    /// string as the <c>openlog</c> and <c>syslog</c> APIs just hold the
    /// pointer to the ident and dereference it for each log message.
    /// </summary>
    private IntPtr m_handleToIdentity = IntPtr.Zero;

    /// <summary>
    /// Open connection to system logger.
    /// </summary>
    [DllImport("libc")]
    private static extern void openlog(IntPtr ident, int option, SyslogFacility facility);

    /// <summary>
    /// Generate a log message.
    /// </summary>
    /// <remarks>
    /// <para>
    /// The libc syslog method takes a format string and a variable argument list similar
    /// to the classic printf function. As this type of vararg list is not supported
    /// by C# we need to specify the arguments explicitly. Here we have specified the
    /// format string with a single message argument. The caller must set the format 
    /// string to <c>"%s"</c>.
    /// </para>
    /// </remarks>
    [DllImport("libc", CharSet = CharSet.Ansi, CallingConvention = CallingConvention.Cdecl)]
    private static extern void syslog(int priority, string format, string message);

    /// <summary>
    /// Close descriptor used to write to system logger.
    /// </summary>
    [DllImport("libc")]
    private static extern void closelog();

    public Syslog()
    {
      // create the native heap ansi string. Note this is a copy of our string
      // so we do not need to hold on to the string itself, holding on to the
      // handle will keep the heap ansi string alive.
      m_handleToIdentity = Marshal.StringToHGlobalAnsi("pivote");

      // open syslog
      openlog(m_handleToIdentity, 1, SyslogFacility.Local7);
    }

    public void Dispose()
    {
      try
      {
        // close syslog
        closelog();
      }
      catch (DllNotFoundException)
      {
        // Ignore dll not found at this point
      }

      if (m_handleToIdentity != IntPtr.Zero)
      {
        // free global ident
        Marshal.FreeHGlobal(m_handleToIdentity);
      }
    }

    /// <summary>
    /// Generate a syslog priority.
    /// </summary>
    /// <param name="facility">The syslog facility.</param>
    /// <param name="severity">The syslog severity.</param>
    /// <returns>A syslog priority.</returns>
    private static int GeneratePriority(SyslogFacility facility, SyslogSeverity severity)
    {
      return ((int)facility * 8) + (int)severity;
    }

    public void Log(SyslogSeverity severity, string message)
    {
      int priority = GeneratePriority(SyslogFacility.Local7, severity);

      // Call the local libc syslog method
      // The second argument is a printf style format string
      syslog(priority, "%s", message);
    }
  }
}
