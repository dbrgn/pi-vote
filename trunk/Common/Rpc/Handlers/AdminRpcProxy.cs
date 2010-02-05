
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
    /// <summary>
    /// Creates a new voting proxy.
    /// </summary>
    /// <param name="binaryProxy">Binary RPC proxy.</param>
    /// <param name="certificate">Certificate of the proxy client entity.</param>
    public AdminRpcProxy(IBinaryRpcProxy binaryProxy, AdminCertificate certificate)
      : base(binaryProxy, certificate)
    { }

    /// <summary>
    /// Creates a new voting procedure.
    /// </summary>
    /// <param name="votingParameters">Parameters for new voting procedure.</param>
    /// <param name="authorities">List of authorities to oversee new voting procedure.</param>
    /// <returns>Id of the voting procedure.</returns>
    public int CreateVoting(VotingParameters votingParameters, IEnumerable<AuthorityCertificate> authorities)
    {
      var request = new CreateVotingAdminRequest(Guid.NewGuid(), votingParameters, authorities);
      var response = Execute<CreateVotingAdminResponse>(request);

      return response.VotingId;
    }

    /// <summary>
    /// Ends a voting procedure.
    /// </summary>
    /// <param name="votingId">Id of the voting procedure.</param>
    public void EndVoting(int votingId)
    {
      var request = new EndVotingAdminRequest(Guid.NewGuid(), votingId);
      var response = Execute<EndVotingAdminResponse>(request);
    }
  }
}
