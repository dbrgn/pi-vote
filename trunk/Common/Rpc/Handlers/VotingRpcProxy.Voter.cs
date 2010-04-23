
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
  public partial class VotingRpcProxy
  {
    /// <summary>
    /// Pushes the enveope containing the vota from this voter.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="signedEnvelope">Signed envelope.</param>
    /// <returns>Vote receipt signed by the server.</returns>
    public Signed<VoteReceipt> PushEnvelope(Guid votingId, Signed<Envelope> signedEnvelope)
    {
      var request = new PushEnvelopeRequest(Guid.NewGuid(), votingId, signedEnvelope);
      var response = Execute<PushEnvelopeResponse>(request);

      return response.VoteReceipt;
    }
  }
}
