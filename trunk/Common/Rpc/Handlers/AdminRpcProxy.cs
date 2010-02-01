
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
  public class AdminRpcProxy : VotingRpcProxy
  {
    public AdminRpcProxy(IBinaryRpcProxy binaryProxy)
      : base(binaryProxy)
    { }

    public int CreateVoting(VotingParameters votingParameters, IEnumerable<AuthorityCertificate> authorities)
    {
      var request = new CreateVotingRequest(Guid.NewGuid(), votingParameters, authorities);
      var response = Execute<CreateVotingResponse>(request);

      return response.VotingId;
    }

    public void EndVoting(int votingId)
    {
      var request = new EndVotingRequest(Guid.NewGuid(), votingId);
      var response = Execute<EndVotingResponse>(request);
    }
  }
}
