
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
    /// Certificate of the proxy client entity.
    /// </summary>
    private Certificate certificate;

    /// <summary>
    /// Creates a new voting proxy.
    /// </summary>
    /// <param name="binaryProxy">Binary RPC proxy.</param>
    /// <param name="certificate">Certificate of the proxy client entity.</param>
    public VotingRpcProxy(IBinaryRpcProxy binaryProxy, Certificate certificate)
    {
      this.binaryProxy = binaryProxy;
      this.certificate = certificate;
    }

    /// <summary>
    /// Fetches the ids of all voting procedures.
    /// </summary>
    /// <returns>List of ids of voting procedures.</returns>
    public IEnumerable<int> FetchVotingIds()
    {
      var request = new ListVotingIdsRequest(Guid.NewGuid());
      var response = Execute<ListVotingIdsResponse>(request);

      return response.VotingIds;
    }

    /// <summary>
    /// Fetches the status of a voting.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <returns>Status of the voting.</returns>
    public VotingStatus FetchVotingStatus(int votingId)
    {
      var request = new VotingStatusRequest(Guid.NewGuid(), votingId);
      var response = Execute<VotingStatusResponse>(request);

      return response.VotingStatus;
    }

    /// <summary>
    /// Executes a PRC request on a remote server.
    /// </summary>
    /// <typeparam name="TResponse">Type of the response.</typeparam>
    /// <param name="request">RPC request to be executed.</param>
    /// <returns>RPC response from the server.</returns>
    protected TResponse Execute<TResponse>(RpcRequest<VotingRpcServer> request)
      where TResponse : RpcResponse
    {
      var signedResponse = new Signed<RpcRequest<VotingRpcServer>>(request, this.certificate);
      var responseData = this.binaryProxy.Execute(signedResponse.ToBinary());
      var response = Serializable.FromBinary<TResponse>(responseData);

      if (response.Exception != null)
        throw response.Exception;

      return response;
    }

    /// <summary>
    /// Fetches an envelope.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="envelopeIndex">Index of the envelope.</param>
    /// <returns>Signed envelope.</returns>
    public Signed<Envelope> FetchEnvelope(int votingId, int envelopeIndex)
    {
      var request = new FetchEnvelopeRequest(Guid.NewGuid(), votingId, envelopeIndex);
      var response = Execute<FetchEnvelopeResponse>(request);

      return response.Envelope;
    }

    /// <summary>
    /// Fetches a partial decipher list.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="authorityIndex">Index of the authority.</param>
    /// <returns>Singed list of partial deciphers.</returns>
    public Signed<PartialDecipherList> FetchPartialDecipher(int votingId, int authorityIndex)
    {
      var request = new FetchPartialDecipherRequest(Guid.NewGuid(), votingId, authorityIndex);
      var response = Execute<FetchPartialDecipherResponse>(request);

      return response.PartialDecipherList;
    }

    /// <summary>
    /// Fetches the envelope count.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <returns>Number of envelopes.</returns>
    public int FetchEnvelopeCount(int votingId)
    {
      var request = new FetchEnvelopeCountRequest(Guid.NewGuid(), votingId);
      var response = Execute<FetchEnvelopeCountResponse>(request);

      return response.EnvelopeCount;
    }

    /// <summary>
    /// Fetches the voting material.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <returns>Complete voting material.</returns>
    public VotingMaterial FetchVotingMaterial(int votingId)
    {
      var request = new FetchVotingMaterialVoterRequest(Guid.NewGuid(), votingId);
      var response = Execute<FetchVotingMaterialVoterResponse>(request);

      return response.VotingMaterial;
    }
  }
}
