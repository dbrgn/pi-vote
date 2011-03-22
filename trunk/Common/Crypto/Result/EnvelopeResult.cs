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
    private readonly Guid voterId;

    /// <summary>
    /// Was the envelope/ballot from that voter valid?
    /// </summary>
    private readonly bool valid;

    /// <summary>
    /// Id of the voter.
    /// </summary>
    public Guid VoterId { get { return this.voterId; } }

    /// <summary>
    /// Was the envelope/ballot from that voter valid?
    /// </summary>
    public bool Valid { get { return this.valid; } }

    /// <summary>
    /// Creates a new envelope result.
    /// </summary>
    /// <param name="voterId">Id of the voter.</param>
    /// <param name="valid">Validity of the envelope/ballot.</param>
    public EnvelopeResult(Guid voterId, bool valid)
    {
      this.voterId = voterId;
      this.valid = valid;
    }
  }
}
