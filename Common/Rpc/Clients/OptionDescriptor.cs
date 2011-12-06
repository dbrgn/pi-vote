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
using System.Threading;
using System.Net;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// Voting option descriptor.
  /// </summary>
  public class OptionDescriptor
  {
    private readonly MultiLanguageString text;
    private readonly MultiLanguageString description;
    private readonly MultiLanguageString url;

    /// <summary>
    /// Text of the option.
    /// </summary>
    public MultiLanguageString Text { get { return this.text; } }

    /// <summary>
    /// Description of the option.
    /// </summary>
    public MultiLanguageString Description { get { return this.description; } }

    /// <summary>
    /// Url of the discussion of the option.
    /// </summary>
    public MultiLanguageString Url { get { return this.url; } }

    /// <summary>
    /// Creates a new option decriptor.
    /// </summary>
    /// <param name="option">Option to decscribe.</param>
    public OptionDescriptor(Option option)
    {
      if (option == null)
        throw new ArgumentException("option");

      this.text = option.Text;
      this.description = option.Description;
      this.url = option.Url;
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
