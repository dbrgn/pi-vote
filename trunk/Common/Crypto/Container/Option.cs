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
  public class Option : Serializable
  {
    public string Text { get; private set; }

    public string Description { get; private set; }

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
