
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
  public class AuthorityRpcProxy : VotingRpcProxy
  {
    public AuthorityRpcProxy(IBinaryRpcProxy binaryProxy)
      : base(binaryProxy)
    { }

    public AllShareParts GetAllShares(int votingId)
    {
      var request = new AuthorityGetAllSharesRequest(Guid.NewGuid(), votingId);
      var response = Execute<AuthorityGetAllSharesResponse>(request);

      return response.AllShareParts;
    }

    public void SetPartailDecipher(int votingId, Signed<PartialDecipherList> signedPartialDecipherList)
    {
      var request = new SetPartialDecipherRequest(Guid.NewGuid(), votingId, signedPartialDecipherList);
      var response = Execute<SetPartialDecipherResponse>(request);
    }

    public AuthorityEnvelopeList GetAllBallots(int votingId)
    {
      var request = new GetAllBallotsRequest(Guid.NewGuid(), votingId);
      var response = Execute<GetAllBallotsResponse>(request);

      return response.AuthorityEnvelopeList;
    }

    public void SetShareResponse(int votingId, Signed<ShareResponse> signedShareResponse)
    {
      var request = new AuthoritySetShareResponseRequest(Guid.NewGuid(), votingId, signedShareResponse);
      var response = Execute<AuthoritySetShareResponseResponse>(request);
    }

    public void SetShares(int votingId, Signed<SharePart> signedSharePart)
    {
      var request = new AuthoritySetSharesRequest(Guid.NewGuid(), votingId, signedSharePart);
      var response = Execute<AuthoritySetSharesResponse>(request);
    }
  
    public AuthorityList GetAuthorityList(int votingId)
    {
      var request = new AuthorityListRequest(Guid.NewGuid(), votingId);
      var response = Execute<AuthorityListResponse>(request);

      return response.AuthorityList;
    }

    public KeyValuePair<int, VotingParameters> GetAuthorityParameters(int votingId, AuthorityCertificate certificate)
    {
      var request = new AuthorityParametersRequest(Guid.NewGuid(), votingId, certificate);
      var response = Execute<AuthorityParametersResponse>(request);

      return new KeyValuePair<int, VotingParameters>(response.AuthorityIndex, response.VotingParameters);
    }
  }
}
