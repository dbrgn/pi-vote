
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
    public AuthorityRpcProxy(IBinaryRpcProxy binaryProxy)
      : base(binaryProxy)
    { }

    public AllShareParts GetAllShares(int votingId)
    {
      var request = new FetchAllSharesAuthorityRequest(Guid.NewGuid(), votingId);
      var response = Execute<FetchAllSharesAuthorityResponse>(request);

      return response.AllShareParts;
    }

    public void SetPartailDecipher(int votingId, Signed<PartialDecipherList> signedPartialDecipherList)
    {
      var request = new PushPartialDecipherAuthorityRequest(Guid.NewGuid(), votingId, signedPartialDecipherList);
      var response = Execute<PushPartialDecipherAuthorityResponse>(request);
    }

    public AuthorityEnvelopeList GetAllBallots(int votingId)
    {
      var request = new FetchEnvelopesAuthorityRequest(Guid.NewGuid(), votingId);
      var response = Execute<FetchEnvelopesAuthorityResponse>(request);

      return response.AuthorityEnvelopeList;
    }

    public void SetShareResponse(int votingId, Signed<ShareResponse> signedShareResponse)
    {
      var request = new PushShareResponseAuthorityRequest(Guid.NewGuid(), votingId, signedShareResponse);
      var response = Execute<PushShareResponseAuthorityResponse>(request);
    }

    public void SetShares(int votingId, Signed<SharePart> signedSharePart)
    {
      var request = new PushSharesAuthorityRequest(Guid.NewGuid(), votingId, signedSharePart);
      var response = Execute<PushSharesAuthorityResponse>(request);
    }
  
    public AuthorityList GetAuthorityList(int votingId)
    {
      var request = new FetchListAuthorityRequest(Guid.NewGuid(), votingId);
      var response = Execute<FetchListAuthorityResponse>(request);

      return response.AuthorityList;
    }

    public KeyValuePair<int, VotingParameters> GetAuthorityParameters(int votingId, AuthorityCertificate certificate)
    {
      var request = new FetchParametersAuthorityRequest(Guid.NewGuid(), votingId, certificate);
      var response = Execute<FetchParametersAuthorityResponse>(request);

      return new KeyValuePair<int, VotingParameters>(response.AuthorityIndex, response.VotingParameters);
    }
  }
}
