
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
    /// Deletes a voting procedure.
    /// </summary>
    /// <param name="command">Command to delete the voting.</param>
    /// <returns>Id of the voting procedure.</returns>
    public void DeleteVoting(Signed<DeleteVotingRequest.Command> command)
    {
      var request = new DeleteVotingRequest(Guid.NewGuid(), command);
      var response = Execute<DeleteVotingResponse>(request);
    }

    /// <summary>
    /// Creates a new voting procedure.
    /// </summary>
    /// <param name="votingParameters">Parameters for new voting procedure.</param>
    /// <param name="authorities">List of authorities to oversee new voting procedure.</param>
    /// <returns>Id of the voting procedure.</returns>
    public void CreateVoting(Signed<VotingParameters> votingParameters, IEnumerable<AuthorityCertificate> authorities)
    {
      var request = new CreateVotingRequest(Guid.NewGuid(), votingParameters, authorities);
      var response = Execute<CreateVotingResponse>(request);
    }

    /// <summary>
    /// Ends a voting procedure.
    /// </summary>
    /// <param name="votingId">Id of the voting procedure.</param>
    public void EndVoting(Guid votingId)
    {
      var request = new EndVotingRequest(Guid.NewGuid(), votingId);
      var response = Execute<EndVotingResponse>(request);
    }

    /// <summary>
    /// Fetch a list of pending signature requests.
    /// </summary>
    /// <returns>List of signature ids.</returns>
    public IEnumerable<Guid> FetchSignatureRequestList()
    {
      var request = new FetchSignatureRequestListRequest(Guid.NewGuid());
      var response = Execute<FetchSignatureRequestListResponse>(request);

      return response.SignatureRequestList;
    }

    /// <summary>
    /// Fetch a signature request.
    /// </summary>
    /// <param name="id">Id of the signature request.</param>
    /// <returns>Secure siganture request.</returns>
    public Secure<SignatureRequest> FetchSignatureRequest(Guid id)
    {
      var request = new FetchSignatureRequestRequest(Guid.NewGuid(), id);
      var response = Execute<FetchSignatureRequestResponse>(request);

      return response.SecureSignatureRequest;
    }

    /// <summary>
    /// Pushes a signature response from an CA to the server.
    /// </summary>
    /// <param name="signatureResponse">Signed signature response.</param>
    public void PushSignatureResponse(Signed<SignatureResponse> signatureResponse)
    {
      var request = new PushSignatureResponseRequest(Guid.NewGuid(), signatureResponse);
      var response = Execute<PushSignatureResponseResponse>(request); 
    }

    /// <summary>
    /// Fetches all valid authority certificates from the server.
    /// </summary>
    /// <returns>List of authority certificates.</returns>
    public IEnumerable<AuthorityCertificate> FetchAuthorityCertificates()
    {
      var request = new FetchAuthorityCertificatesRequest(Guid.NewGuid());
      var response = Execute<FetchAuthorityCertificatesResponse>(request);

      return response.AuthorityCertificates;
    }

    /// <summary>
    /// Push certificate storage to the server.
    /// </summary>
    /// <param name="certificateStorage">Certificate storage to add to the server's data.</param>
    public void PushCertificateStorage(CertificateStorage certificateStorage)
    {
      var request = new PushCertificateStorageRequest(Guid.NewGuid(), certificateStorage);
      var response = Execute<PushCertificateStorageResponse>(request);
    }
  }
}
