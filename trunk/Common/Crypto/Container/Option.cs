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
    public string Text { get; private set; }

    /// <summary>
    /// Description of the option.
    /// </summary>
    public string Description { get; private set; }

    /// <summary>
    /// Create a new option.
    /// </summary>
    /// <param name="text">Text of option.</param>
    /// <param name="description">Description of option.</param>
    public Option(string text, string description)
    {
      Text = text;
      Description = description;
    }

    public Option(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Text);
      context.Write(Description);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Text = context.ReadString();
      Description = context.ReadString();
    }
  }
}
