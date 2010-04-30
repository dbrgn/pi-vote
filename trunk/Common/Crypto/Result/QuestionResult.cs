﻿/*
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
  /// Result of a question in a voting.
  /// </summary>
  public class QuestionResult
  {
    /// <summary>
    /// Text of the question.
    /// </summary>
    private readonly MultiLanguageString text;

    /// <summary>
    /// Results for each option.
    /// </summary>
    private readonly List<OptionResult> options;

    /// <summary>
    /// Question of the voting procedure.
    /// </summary>
    public MultiLanguageString Text { get { return this.text; } }

    /// <summary>
    /// Results for each option.
    /// </summary>
    public List<OptionResult> Options { get { return this.options; } }

    /// <summary>
    /// Creates a result from a question.
    /// </summary>
    /// <param name="question">Question in question.</param>
    public QuestionResult(Question question)
    {
      this.text = question.Text;
      this.options = new List<OptionResult>();
    }
  }
}
