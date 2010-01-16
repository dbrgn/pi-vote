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

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Result for one option.
  /// </summary>
  /// <remarks>
  /// Used for reporting result to UI.
  /// </remarks>
  public class OptionResult
  {
    /// <summary>
    /// Text of option.
    /// </summary>
    public string Text { get { return this.text; } }

    /// <summary>
    /// Descrption of option.
    /// </summary>
    public string Description { get { return this.description; } }

    /// <summary>
    /// Votes for this option.
    /// </summary>
    public int Result { get { return this.result; } }

    public OptionResult(string text, string description, int result)
    {
      this.text = text;
      this.description = description;
      this.result = result;
    }

    private readonly string text;
    private readonly string description;
    private readonly int result;
  }
}
