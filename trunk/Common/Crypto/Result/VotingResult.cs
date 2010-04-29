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
  public class QuestionResult
  {
    /// <summary>
    /// Question of the voting procedure.
    /// </summary>
    private readonly MultiLanguageString question;

    /// <summary>
    /// Results for each option.
    /// </summary>
    private readonly List<OptionResult> options;

    /// <summary>
    /// Question of the voting procedure.
    /// </summary>
    public MultiLanguageString Question { get { return this.question; } }

    /// <summary>
    /// Results for each option.
    /// </summary>
    public List<OptionResult> Options { get { return this.options; } }

    public QuestionResult(QuestionParameters parameters)
    {
      this.question = parameters.Question;
      this.options = new List<OptionResult>();
    }
  }

  /// <summary>
  /// Container for the voting result.
  /// </summary>
  /// <remarks>
  /// Used to report result to UI.
  /// </remarks>
  public class VotingResult
  {
    /// <summary>
    /// Id of the voting procedure.
    /// </summary>
    private readonly Guid votingId;

    /// <summary>
    /// Title of the voting procedure.
    /// </summary>
    private readonly MultiLanguageString title;

    /// <summary>
    /// Description of the voting procedure.
    /// </summary>
    private readonly MultiLanguageString description;

    private readonly List<QuestionResult> questions;

    /// <summary>
    /// Participing voters.
    /// </summary>
    private readonly List<EnvelopeResult> voters;

    /// <summary>
    /// Id of the voting procedure.
    /// </summary>
    public Guid VotingId { get { return this.votingId; } }

    /// <summary>
    /// Title of the voting procedure.
    /// </summary>
    public MultiLanguageString Title { get { return this.title; } }

    /// <summary>
    /// Description of the voting procedure.
    /// </summary>
    public MultiLanguageString Description { get { return this.description; } }

    public List<QuestionResult> Questions { get { return this.questions; } }

    /// <summary>
    /// Participing voters.
    /// </summary>
    public List<EnvelopeResult> Voters { get { return this.voters; } }

    /// <summary>
    /// Number of invalid ballots.
    /// </summary>
    public int InvalidBallots { get { return Voters.Where(voter => !voter.Valid).Count(); } }

    /// <summary>
    /// Number of valid ballots.
    /// </summary>
    public int ValidBallots { get { return Questions.First().Options.Sum(option => option.Result); } }

    /// <summary>
    /// Total number of cast ballots.
    /// </summary>
    public int TotalBallots { get { return ValidBallots + InvalidBallots; } }

    /// <summary>
    /// Creates a new voting result.
    /// </summary>
    /// <remarks>
    /// Don't forget adding option and envelopes results.
    /// </remarks>
    /// <param name="id">Id of the voting procedure.</param>
    /// <param name="name">Name of the voting procedure.</param>
    public VotingResult(Guid votingId, VotingParameters votingParameters)
    {
      this.votingId = votingId;
      this.title = votingParameters.Title;
      this.description = votingParameters.Description;
      this.questions = new List<QuestionResult>();
      this.voters = new List<EnvelopeResult>();
    }
  }
}
