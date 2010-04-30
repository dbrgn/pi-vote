
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
using System.Threading;
using System.Net;
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// Describes a question in a voting.
  /// </summary>
  public class QuestionDescriptor
  {
    private readonly MultiLanguageString text;
    private readonly MultiLanguageString description;
    private readonly List<OptionDescriptor> options;
    private readonly int maxOptions;

    /// <summary>
    /// Text of the question.
    /// </summary>
    public MultiLanguageString Text { get { return this.text; } }

    /// <summary>
    /// Description of the question.
    /// </summary>
    public MultiLanguageString Description { get { return this.description; } }

    /// <summary>
    /// Options of the question..
    /// </summary>
    public IEnumerable<OptionDescriptor> Options { get { return this.options; } }

    /// <summary>
    /// Maximum number of options one voter may vote for.
    /// </summary>
    public int MaxOptions { get { return this.maxOptions; } }

    /// <summary>
    /// Creates a new question descriptor.
    /// </summary>
    /// <param name="question">Question to describe.</param>
    public QuestionDescriptor(Question question)
    {
      if (question == null)
        throw new ArgumentNullException("question");

      this.text = question.Text;
      this.description = question.Description;
      this.maxOptions = question.MaxVota;

      this.options = new List<OptionDescriptor>();
      question.Options.Foreach(option => this.options.Add(new OptionDescriptor(option)));
    }
  }
}
