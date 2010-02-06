﻿
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
  public class AuthorityRpcProxy : VotingRpcProxy
  {
    /// <summary>
    /// Creates a new voting proxy.
    /// </summary>
    /// <param name="binaryProxy">Binary RPC proxy.</param>
    /// <param name="certificate">Certificate of the proxy client entity.</param>
    public AuthorityRpcProxy(IBinaryRpcProxy binaryProxy, AuthorityCertificate certificate)
      : base(binaryProxy, certificate)
    { }

    /// <summary>
    /// Fetches shares from all authorities.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <returns>All shares from all authorities.</returns>
    public AllShareParts FetchAllShares(int votingId)
    {
      var request = new FetchAllSharesAuthorityRequest(Guid.NewGuid(), votingId);
      var response = Execute<FetchAllSharesAuthorityResponse>(request);

      return response.AllShareParts;
    }

    /// <summary>
    /// Pushes partial deciphers from the authority.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="signedPartialDecipherList">Signed list of partial deciphers.</param>
    public void PushPartailDecipher(int votingId, Signed<PartialDecipherList> signedPartialDecipherList)
    {
      var request = new PushPartialDecipherAuthorityRequest(Guid.NewGuid(), votingId, signedPartialDecipherList);
      var response = Execute<PushPartialDecipherAuthorityResponse>(request);
    }

    /// <summary>
    /// Fetches all envelopes.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <returns>List of all envelopes.</returns>
    public AuthorityEnvelopeList FetchEnvelopes(int votingId)
    {
      var request = new FetchEnvelopesAuthorityRequest(Guid.NewGuid(), votingId);
      var response = Execute<FetchEnvelopesAuthorityResponse>(request);

      return response.AuthorityEnvelopeList;
    }

    /// <summary>
    /// Pushes this authority's response to the shares.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="signedShareResponse">Signed response to the shares.</param>
    public void PushShareResponse(int votingId, Signed<ShareResponse> signedShareResponse)
    {
      var request = new PushShareResponseAuthorityRequest(Guid.NewGuid(), votingId, signedShareResponse);
      var response = Execute<PushShareResponseAuthorityResponse>(request);
    }

    /// <summary>
    /// Pushes shares from this authority.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="signedSharePart">Signed shares from this authority.</param>
    public void PushShares(int votingId, Signed<SharePart> signedSharePart)
    {
      var request = new PushSharesAuthorityRequest(Guid.NewGuid(), votingId, signedSharePart);
      var response = Execute<PushSharesAuthorityResponse>(request);
    }
  
    /// <summary>
    /// Fetches the list of authorities for the voting.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <returns>List of authorities.</returns>
    public AuthorityList FetchAuthorityList(int votingId)
    {
      var request = new FetchListAuthorityRequest(Guid.NewGuid(), votingId);
      var response = Execute<FetchListAuthorityResponse>(request);

      return response.AuthorityList;
    }

    /// <summary>
    /// Fetchs the parameters for the voting.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="certificate">Certificate of the authority.</param>
    /// <returns>Index of the authority.</returns>
    public KeyValuePair<int, VotingParameters> FetchParameters(int votingId, AuthorityCertificate certificate)
    {
      var request = new FetchParametersAuthorityRequest(Guid.NewGuid(), votingId, certificate);
      var response = Execute<FetchParametersAuthorityResponse>(request);

      return new KeyValuePair<int, VotingParameters>(response.AuthorityIndex, response.VotingParameters);
    }
  }
}