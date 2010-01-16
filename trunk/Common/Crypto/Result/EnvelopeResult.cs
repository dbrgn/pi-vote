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
  /// Particping voter result.
  /// </summary>
  /// <remarks>
  /// Does NOT contain any information about the vote
  /// cast by that voter other than validity.
  /// Used for reporting result to UI.
  /// </remarks>
  public class EnvelopeResult
  {
    /// <summary>
    /// Id of the voter.
    /// </summary>
    public int VoterId { get { return this.voterId; } }

    /// <summary>
    /// Full name of the voter.
    /// </summary>
    public string VoterName { get { return this.voterName; } }

    /// <summary>
    /// Was the envelope/ballot from that voter valid?
    /// </summary>
    public bool Valid { get { return this.valid; } }

    /// <summary>
    /// Creates a new envelope result.
    /// </summary>
    /// <param name="voterId">Id of the voter.</param>
    /// <param name="voterName">Full name of the voter.</param>
    /// <param name="valid">Validity of the envelope/ballot.</param>
    public EnvelopeResult(int voterId, string voterName, bool valid)
    {
      this.voterId = voterId;
      this.voterName = voterName;
      this.valid = valid;
    }

    private readonly int voterId;
    private readonly string voterName;
    private readonly bool valid;
  }
}
