
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
  public partial class VotingRpcProxy
  {
    /// <summary>
    /// Fetches shares from all authorities.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <returns>All shares from all authorities.</returns>
    public AllShareParts FetchAllShares(Guid votingId)
    {
      var request = new FetchAllSharesRequest(Guid.NewGuid(), votingId);
      var response = Execute<FetchAllSharesResponse>(request);

      return response.AllShareParts;
    }

    /// <summary>
    /// Pushes partial deciphers from the authority.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="signedPartialDecipherList">Signed list of partial deciphers.</param>
    public void PushPartailDecipher(Guid votingId, Signed<PartialDecipherList> signedPartialDecipherList)
    {
      var request = new PushPartialDecipherRequest(Guid.NewGuid(), votingId, signedPartialDecipherList);
      var response = Execute<PushPartialDecipherResponse>(request);
    }

    /// <summary>
    /// Pushes this authority's response to the shares.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="signedShareResponse">Signed response to the shares.</param>
    public void PushShareResponse(Guid votingId, Signed<ShareResponse> signedShareResponse)
    {
      var request = new PushShareResponseRequest(Guid.NewGuid(), votingId, signedShareResponse);
      var response = Execute<PushShareResponseResponse>(request);
    }

    /// <summary>
    /// Pushes shares from this authority.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="signedSharePart">Signed shares from this authority.</param>
    public void PushShares(Guid votingId, Signed<SharePart> signedSharePart)
    {
      var request = new PushSharesRequest(Guid.NewGuid(), votingId, signedSharePart);
      var response = Execute<PushSharesResponse>(request);
    }
  
    /// <summary>
    /// Fetches the list of authorities for the voting.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <returns>List of authorities.</returns>
    public AuthorityList FetchAuthorityList(Guid votingId)
    {
      var request = new FetchAuthorityListRequest(Guid.NewGuid(), votingId);
      var response = Execute<FetchAuthorityListResponse>(request);

      return response.AuthorityList;
    }

    /// <summary>
    /// Fetchs the parameters for the voting.
    /// </summary>
    /// <param name="votingId">Id of the voting.</param>
    /// <param name="certificate">Certificate of the authority.</param>
    /// <returns>Index of the authority.</returns>
    public KeyValuePair<int, Signed<VotingParameters>> FetchParameters(Guid votingId, AuthorityCertificate certificate)
    {
      var request = new FetchParametersRequest(Guid.NewGuid(), votingId, certificate);
      var response = Execute<FetchParametersResponse>(request);

      return new KeyValuePair<int, Signed<VotingParameters>>(response.AuthorityIndex, response.VotingParameters);
    }
  }
}
