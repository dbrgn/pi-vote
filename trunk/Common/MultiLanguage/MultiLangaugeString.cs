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
using System.Globalization;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote
{
  /// <summary>
  /// A text in multiple languages.
  /// </summary>
  public class MultiLanguageString
  {
    /// <summary>
    /// Dictionary of string by language.
    /// </summary>
    private Dictionary<Language, string> content;

    /// <summary>
    /// Creates a new multi language string.
    /// </summary>
    public MultiLanguageString()
    {
      this.content = new Dictionary<Language, string>();
    }

    /// <summary>
    /// Creates a new multi language string.
    /// </summary>
    public MultiLanguageString(string englishString)
    {
      this.content = new Dictionary<Language, string>();
      this.content.Add(Language.English, englishString);
    }

    /// <summary>
    /// Gets a string in a specific language.
    /// </summary>
    /// <param name="language">Language of the string to retrieve.</param>
    /// <returns>String in that language.</returns>
    public string Get(Language language)
    {
      if (this.content.ContainsKey(language))
      {
        return this.content[language];
      }
      else if (this.content.ContainsKey(Language.English))
      {
        return this.content[Language.English];
      }
      else
      {
        return string.Empty;
      }
    }

    /// <summary>
    /// Gets a string in a specific language.
    /// </summary>
    /// <param name="language">Language of the string to retrieve.</param>
    /// <returns>String in that language.</returns>
    public string GetOrEmpty(Language language)
    {
      if (this.content.ContainsKey(language))
      {
        return this.content[language];
      }
      else
      {
        return string.Empty;
      }
    }

    /// <summary>
    /// Has it a string for this language.
    /// </summary>
    /// <param name="language">Language in question.</param>
    /// <returns>Is there a string in this language.</returns>
    public bool Has(Language language)
    {
      return this.content.ContainsKey(language);
    }

    /// <summary>
    /// Sets a string value for a language.
    /// </summary>
    /// <param name="language">Language of the string.</param>
    /// <param name="value">String to place.</param>
    public void Set(Language language, string value)
    {
      if (this.content.ContainsKey(language))
      {
        this.content[language] = value;
      }
      else if (!value.IsNullOrEmpty()) 
      {
        this.content.Add(language, value);
      }
    }

    /// <summary>
    /// String in the current language.
    /// </summary>
    public string Text
    {
      get { return Get(LibraryResources.Culture.ToLanguage()); }
    }

    public string AllLanguages
    {
      get
      {
        return string.Join(" / ", this.content.Values.ToArray());
      }
    }

    /// <summary>
    /// Serialize the string.
    /// </summary>
    /// <param name="context">Context of the serialization.</param>
    public void Serialize(SerializeContext context)
    {
      context.Write(this.content.Count);

      foreach (KeyValuePair<Language, string> item in this.content)
      {
        context.Write((int)item.Key);
        context.Write(item.Value);
      }
    }

    /// <summary>
    /// Deserialize a string.
    /// </summary>
    /// <param name="context">Context of the deserialization.</param>
    /// <returns>Deserialized multi language string.</returns>
    public static MultiLanguageString Deserialize(DeserializeContext context)
    {
      int count = context.ReadInt32();
      MultiLanguageString value = new MultiLanguageString();

      for (int index = 0; index < count; index++)
      {
        value.content.Add((Language)context.ReadInt32(), context.ReadString());
      }

      return value;
    }
  }
}
