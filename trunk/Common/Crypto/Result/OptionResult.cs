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
    private readonly MultiLanguageString text;

    /// <summary>
    /// Descrption of option.
    /// </summary>
    private readonly MultiLanguageString description;

    /// <summary>
    /// Votes for this option.
    /// </summary>
    private readonly int result;

    /// <summary>
    /// Text of option.
    /// </summary>
    public MultiLanguageString Text { get { return this.text; } }

    /// <summary>
    /// Descrption of option.
    /// </summary>
    public MultiLanguageString Description { get { return this.description; } }

    /// <summary>
    /// Votes for this option.
    /// </summary>
    public int Result { get { return this.result; } }

    /// <summary>
    /// Creates a new voting option result object.
    /// </summary>
    /// <param name="text">Text of option.</param>
    /// <param name="description">Descrption of option.</param>
    /// <param name="result">Votes for this option.</param>
    public OptionResult(MultiLanguageString text, MultiLanguageString description, int result)
    {
      this.text = text;
      this.description = description;
      this.result = result;
    }
  }
}
