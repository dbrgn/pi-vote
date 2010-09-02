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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// An option for which voters may vote.
  /// </summary>
  public class Option : Serializable
  {
    /// <summary>
    /// Text of option.
    /// </summary>
    /// <example>
    /// Yes / No / Abstain
    /// Alice Shepard / Bob Miller
    /// </example>
    public MultiLanguageString Text { get; private set; }

    /// <summary>
    /// Description of the option.
    /// </summary>
    public MultiLanguageString Description { get; private set; }

    /// <summary>
    /// Url of the discussion of the option.
    /// </summary>
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
    public Option(DeserializeContext context)
      : base(context)
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
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Text = context.ReadMultiLanguageString();
      Description = context.ReadMultiLanguageString();
      Url = context.ReadMultiLanguageString();
    }
  }
}
