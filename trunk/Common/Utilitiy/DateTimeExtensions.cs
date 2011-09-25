using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Pirate.PiVote.Utilitiy
{
  /// <summary>
  /// Extensions to DateTime.
  /// </summary>
  public static class DateTimeExtensions
  {
    /// <summary>
    /// Pi-Vote time zone: Western Europe Standard Time
    /// </summary>
    private static TimeZoneInfo PiTimeZone
    {
      get
      {
        var timeZoneInfo = TimeZoneInfo.GetSystemTimeZones()
          .Where(timeZone => timeZone.Id == "W. Europe Standard Time")
          .FirstOrDefault();

        if (timeZoneInfo != null)
        {
          return timeZoneInfo;
        }

        timeZoneInfo = TimeZoneInfo.GetSystemTimeZones()
          .Where(timeZone => timeZone.Id == "Europe/Zurich")
          .FirstOrDefault();

        if (timeZoneInfo != null)
        {
          return timeZoneInfo;
        }

        timeZoneInfo = TimeZoneInfo.GetSystemTimeZones()
          .Where(timeZone => timeZone.BaseUtcOffset == new TimeSpan(1, 0, 0))
          .FirstOrDefault();

        if (timeZoneInfo != null)
        {
          return timeZoneInfo;
        }

        return TimeZoneInfo.Local;
      }
    }

    /// <summary>
    /// Converts local time to Pi-Vote time (Western Europe Standard Time).
    /// </summary>
    /// <param name="dateTime">Datetime in local time.</param>
    /// <returns>Datetime in Pi-Vote time.</returns>
    public static DateTime ToPiTime(this DateTime dateTime)
    {
      return TimeZoneInfo.ConvertTime(dateTime, TimeZoneInfo.Local, PiTimeZone);
    }

    /// <summary>
    /// Converts Pi-Vote time (Western Europe Standard Time) to local time.
    /// </summary>
    /// <param name="dateTime">Datetime in Pi-Vote time.</param>
    /// <returns>Datetime in local time.</returns>
    public static DateTime FromPiTime(this DateTime dateTime)
    {
      return TimeZoneInfo.ConvertTime(dateTime, PiTimeZone, TimeZoneInfo.Local);
    }
  }
}
