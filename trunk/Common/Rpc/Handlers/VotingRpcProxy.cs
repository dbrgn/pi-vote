
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
  public class VotingRpcProxy
  {
    /// <summary>
    /// Binary RPC proxy.
    /// </summary>
    private IBinaryRpcProxy binaryProxy;

    /// <summary>
    /// Creates a new voting proxy.
    /// </summary>
    /// <param name="binaryProxy"></param>
    public VotingRpcProxy(IBinaryRpcProxy binaryProxy)
    {
      this.binaryProxy = binaryProxy;
    }

    public IEnumerable<int> GetVotingIds()
    {
      var request = new ListVotingIdsRequest(Guid.NewGuid());
      var response = Execute<ListVotingIdsResponse>(request);

      return response.VotingIds;
    }

    public VotingStatus GetVotingStatus(int votingId)
    {
      var request = new VotingStatusRequest(Guid.NewGuid(), votingId);
      var response = Execute<VotingStatusResponse>(request);

      return response.VotingStatus;
    }

    protected TResponse Execute<TResponse>(RpcRequest<VotingRpcServer> request)
      where TResponse : RpcResponse
    {
      var responseData = this.binaryProxy.Execute(request.ToBinary());
      var response = Serializable.FromBinary<TResponse>(responseData);

      if (response.Exception != null)
        throw response.Exception;

      return response;
    }
  }
}
