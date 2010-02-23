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
    /// Name of the voting procedure.
    /// </summary>
    private readonly string votingName;

    /// <summary>
    /// Results for each option.
    /// </summary>
    private readonly List<OptionResult> options;

    /// <summary>
    /// Participing voters.
    /// </summary>
    private readonly List<EnvelopeResult> voters;

    /// <summary>
    /// Id of the voting procedure.
    /// </summary>
    public Guid VotingId { get { return this.votingId; } }

    /// <summary>
    /// Name of the voting procedure.
    /// </summary>
    public string VotingName { get { return this.votingName; } }

    /// <summary>
    /// Results for each option.
    /// </summary>
    public List<OptionResult> Options { get { return this.options; } }

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
    public int ValidBallots { get { return Options.Sum(option => option.Result); } }

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
    public VotingResult(Guid votingId, string votingName)
    {
      this.votingId = votingId;
      this.votingName = votingName;
      this.options = new List<OptionResult>();
      this.voters = new List<EnvelopeResult>();
    }
  }
}
