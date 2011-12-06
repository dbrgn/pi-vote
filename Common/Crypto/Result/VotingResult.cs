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
using Emil.GMP;

namespace Pirate.PiVote.Crypto
{
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

    /// <summary>
    /// Questions posed in the voting.
    /// </summary>
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

    /// <summary>
    /// Questions posed in the voting.
    /// </summary>
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
    public int ValidBallots { get { return Voters.Where(voter => voter.Valid).Count(); } }

    /// <summary>
    /// Total number of cast ballots.
    /// </summary>
    public int TotalBallots { get { return Voters.Count(); } }

    /// <summary>
    /// Creates a new voting result.
    /// </summary>
    /// <remarks>
    /// Don't forget adding option and envelopes results.
    /// </remarks>
    /// <param name="votingId">Id of the voting procedure.</param>
    /// <param name="votingParameters">Parameters of the voting in question.</param>
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
