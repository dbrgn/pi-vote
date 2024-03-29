﻿/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// An option for which voters may vote.
  /// </summary>
  [SerializeObject("An option for which voters may vote.")]
  public class Option : Serializable
  {
    /// <summary>
    /// Text of option.
    /// </summary>
    /// <example>
    /// Yes / No / Abstain
    /// Alice Shepard / Bob Miller
    /// </example>
    [SerializeField(0, "Text of option.")]
    public MultiLanguageString Text { get; private set; }

    /// <summary>
    /// Description of the option.
    /// </summary>
    [SerializeField(1, "Description of the option.")]
    public MultiLanguageString Description { get; private set; }

    /// <summary>
    /// Url of the discussion of the option.
    /// </summary>
    [SerializeField(2, "Url of the discussion of the option.")]
    public MultiLanguageString Url { get; private set; }

    /// <summary>
    /// Create a new option.
    /// </summary>
    /// <param name="text">Text of option.</param>
    /// <param name="description">Description of option.</param>
    /// <param name="url">Url of the discussion of the option.</param>
    public Option(MultiLanguageString text, MultiLanguageString description, MultiLanguageString url)
    {
      if (text == null)
        throw new ArgumentNullException("text");
      if (description == null)
        throw new ArgumentNullException("description");

      Text = text;
      Description = description;
      Url = url;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public Option(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Text);
      context.Write(Description);
      context.Write(Url);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      Text = context.ReadMultiLanguageString();
      Description = context.ReadMultiLanguageString();
      Url = context.ReadMultiLanguageString();
    }

    public static Option CreateAbstention()
    { 
      MultiLanguageString text = new MultiLanguageString();
      text.Set(Language.English, LibraryResources.OptionAbstainEnglish);
      text.Set(Language.German, LibraryResources.OptionAbstainGerman);
      text.Set(Language.French, LibraryResources.OptionAbstainFrench);

      return new Option(
        text,
        MultiLanguageString.AllEmpty, 
        MultiLanguageString.AllEmpty);
    }

    public static Option CreateAbstentionSpecial()
    {
      return new Option(
        MultiLanguageString.AllSame(LibraryResources.OptionAbstainSpecial),
        MultiLanguageString.AllEmpty,
        MultiLanguageString.AllEmpty);
    }

    public bool IsAbstentionSpecial
    { 
      get
      {
        return Text.Get(Language.English) == LibraryResources.OptionAbstainSpecial;
      }
    }
  }
}
