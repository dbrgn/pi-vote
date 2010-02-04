
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
  /// Proxy for RPC function calling.
  /// </summary>
  public class AdminRpcProxy : VotingRpcProxy
  {
    public AdminRpcProxy(IBinaryRpcProxy binaryProxy)
      : base(binaryProxy)
    { }

    public int CreateVoting(VotingParameters votingParameters, IEnumerable<AuthorityCertificate> authorities)
    {
      var request = new CreateVotingAdminRequest(Guid.NewGuid(), votingParameters, authorities);
      var response = Execute<CreateVotingAdminResponse>(request);

      return response.VotingId;
    }

    public void EndVoting(int votingId)
    {
      var request = new EndVotingAdminRequest(Guid.NewGuid(), votingId);
      var response = Execute<EndVotingAdminResponse>(request);
    }
  }
}
