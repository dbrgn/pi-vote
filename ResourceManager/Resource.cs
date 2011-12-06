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
using Pirate.PiVote;

namespace Pirate.PiVote.ResourceManager
{
  public class Resource
  {
    public string Name { get; private set; }

    public MultiLanguageString Text { get; private set; }

    public Resource(string name)
    {
      Name = name;
      Text = new MultiLanguageString();
    }

    public Resource(string name, Language language, string text)
    {
      Name = name;
      Text = new MultiLanguageString();
      Text.Set(language, text);
    }
  }
}
