
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
using Pirate.PiVote.Serialization;
using Pirate.PiVote.Crypto;

namespace Pirate.PiVote.Rpc
{
  /// <summary>
  /// Voting RPC proxy.
  /// </summary>
  public class VoterRpcProxy : VotingRpcProxy
  {
    /// <summary>
    /// Creates a new voting proxy.
    /// </summary>
    /// <param name="binaryProxy">Binary RPC proxy.</param>
    public VoterRpcProxy(IBinaryRpcProxy binaryProxy)
      : base(binaryProxy)
    { }

    /// <summary>
    /// Pushes the enveope containing the vota from this voter.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="signedEnvelope">Signed envelope.</param>
    public void PushEnvelope(Guid votingId, Signed<Envelope> signedEnvelope)
    {
      var request = new PushEnvelopeVoterRequest(Guid.NewGuid(), votingId, signedEnvelope);
      var response = Execute<PushEnvelopeVoterResponse>(request);
    }
  }
}
