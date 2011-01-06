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
using System.Globalization;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote
{
  /// <summary>
  /// Language of the interface and texts.
  /// </summary>
  [SerializeEnum("Language of the interface and texts.")]
  public enum Language
  {
    English = 0,
    German = 1,
    French = 2,
    Italien = 3
  }

  /// <summary>
  /// Extensions concering languages.
  /// </summary>
  public static class LanguageExtensions
  {
    /// <summary>
    /// Converts a language to a culture.
    /// </summary>
    /// <param name="language">Language to convert.</param>
    /// <returns>Culture representing the language.</returns>
    public static CultureInfo ToCulture(this Language language)
    {
      switch (language)
      {
        case Language.English:
          return CultureInfo.CreateSpecificCulture("en-US");
        case Language.German:
          return CultureInfo.CreateSpecificCulture("de-DE");
        case Language.French:
          return CultureInfo.CreateSpecificCulture("fr-FR");
        case Language.Italien:
          return CultureInfo.CreateSpecificCulture("it-IT");
        default:
          throw new ArgumentException("Unknown language");
      }
    }

    /// <summary>
    /// Convert a culture into a language.
    /// </summary>
    /// <param name="culture">Culture to convert.</param>
    /// <returns>Language of the culture.</returns>
    public static Language ToLanguage(this CultureInfo culture)
    {
      if (culture == null)
      {
        return Language.English;
      }
      else
      {
        switch (culture.Name)
        {
          case "en-US":
            return Language.English;
          case "de-DE":
            return Language.German;
          case "fr-FR":
            return Language.French;
          case "it-IT":
            return Language.Italien;
          default:
            throw new ArgumentException("Unknown language");
        }
      }
    }
  }
}
