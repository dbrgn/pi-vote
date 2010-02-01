
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
  public class VoterRpcProxy : VotingRpcProxy
  {
    public VoterRpcProxy(IBinaryRpcProxy binaryProxy)
      : base(binaryProxy)
    { }

    public VotingContainer GetVotingResult(int votingId)
    {
      var request = new GetVotingResultRequest(Guid.NewGuid(), votingId);
      var response = Execute<GetVotingResultResponse>(request);

      return response.VotingContainer;
    }

    public void SetVote(int votingId, Signed<Envelope> signedEnvelope)
    {
      var request = new SetVoteRequest(Guid.NewGuid(), votingId, signedEnvelope);
      var response = Execute<SetVoteResponse>(request);
    }

    public VotingMaterial GetVotingMaterial(int votingId)
    {
      var request = new GetVotingMaterialRequest(Guid.NewGuid(), votingId);
      var response = Execute<GetVotingMaterialResponse>(request);

      return response.VotingMaterial;
    }
  }
}
