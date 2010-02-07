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

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Status of the voting procedure.
  /// </summary>
  public enum VotingStatus
  {
    New,
    Sharing,
    Voting,
    Aborted,
    Deciphering,
    Finished,
  }

  /// <summary>
  /// Entity of the voting server.
  /// </summary>
  public class VotingServerEntity
  {
    /// <summary>
    /// Voting parameters.
    /// </summary>
    private VotingParameters parameters;

    /// <summary>
    /// List of authorities.
    /// </summary>
    private Dictionary<int, Certificate> authorities;

    /// <summary>
    /// List of share parts from authorities.
    /// </summary>
    private Dictionary<int, Signed<SharePart>> shares;

    /// <summary>
    /// List of share responses from authorities.
    /// </summary>
    private Dictionary<int, Signed<ShareResponse>> responses;

    /// <summary>
    /// List of singed envelopes from voters.
    /// </summary>
    private List<Signed<Envelope>> signedEnvelopes;

    /// <summary>
    /// List of partial deciphers from authorities.
    /// </summary>
    private Dictionary<int, Signed<PartialDecipherList>> partialDeciphers;

    /// <summary>
    /// Storage of certificates.
    /// </summary>
    private CertificateStorage certificateStorage;

    /// <summary>
    /// Status of the voting procedures.
    /// </summary>
    public VotingStatus Status { get; private set; }

    /// <summary>
    /// Id of the voting procedures.
    /// </summary>
    public int Id
    {
      get { return this.parameters.VotingId; }
    }

    /// <summary>
    /// Create a new voting procedure.
    /// </summary>
    /// <param name="parameters">Voting parameters.</param>
    /// <param name="rootCertificate">Root certificate.</param>
    /// <param name="intermediateCertificate">Intermediate certificate.</param>
    /// /// <param name="rootRevocationList">Certificate revocation list from root.</param>
    /// /// <param name="intermediateRevocationList">Certificate revocation list from intermediate.</param>
    public VotingServerEntity(
      VotingParameters parameters,
      CertificateStorage certificateStorage)
    {
      if (parameters == null)
        throw new ArgumentNullException("parameters");
      if (certificateStorage == null)
        throw new ArgumentNullException("certificateStorage");

      this.parameters = parameters;
      this.authorities = new Dictionary<int, Certificate>();
      this.shares = new Dictionary<int, Signed<SharePart>>();
      this.responses = new Dictionary<int, Signed<ShareResponse>>();
      this.signedEnvelopes = new List<Signed<Envelope>>();
      this.partialDeciphers = new Dictionary<int, Signed<PartialDecipherList>>();
      this.certificateStorage = certificateStorage;
      Status = VotingStatus.New;
    }

    /// <summary>
    /// Voting parameters.
    /// </summary>
    public VotingParameters Parameters { get { return this.parameters; } }

    /// <summary>
    /// Get the index of an authority from certificate.
    /// </summary>
    /// <param name="certificate">Certificate of the authority.</param>
    /// <returns>Index of the authority.</returns>
    public int GetAuthorityIndex(AuthorityCertificate certificate)
    {
      if (certificate == null)
        throw new PiArgumentException(ExceptionCode.ArgumentNull, "Certificate is null.");
      if (!certificate.Valid(this.certificateStorage))
        throw new PiSecurityException(ExceptionCode.InvalidCertificate, "Authority certificate invalid."); 

      foreach (KeyValuePair<int, Certificate> authority in this.authorities)
      {
        if (authority.Value.IsIdentic(certificate))
          return authority.Key;
      }

      throw new PiArgumentException(ExceptionCode.NoAuthorityWithCertificate, "No authority with that certificate."); 
    }

    /// <summary>
    /// Add an authority.
    /// </summary>
    /// <param name="certificate">Authority to be added.</param>
    /// <returns>Index of the authority.</returns>
    public int AddAuthority(Certificate certificate)
    {
      if (certificate == null)
        throw new ArgumentNullException("certificate");
      if (this.authorities.Count >= this.parameters.AuthorityCount)
        throw new InvalidOperationException("Already enough authorities.");

      int authorityIndex = this.authorities.Count + 1;
      this.authorities.Add(authorityIndex, certificate);

      return authorityIndex;
    }

    /// <summary>
    /// List of authorities.
    /// </summary>
    public AuthorityList Authorities
    {
      get
      {
        return new AuthorityList(
          Id, 
          this.authorities.Values,
          this.certificateStorage.Certificates,
          this.certificateStorage.SignedRevocationLists);
      }
    }

    /// <summary>
    /// Deposit a share part from authorities.
    /// </summary>
    /// <param name="shares">Share part.</param>
    public void DepositShares(Signed<SharePart> shares)
    {
      if (shares == null)
        throw new ArgumentNullException("shares");
      if (Status != VotingStatus.New)
        throw new InvalidOperationException("Wrong status for operation.");

      SharePart shareContainer = shares.Value;

      if (!this.authorities.ContainsKey(shareContainer.AuthorityIndex))
        throw new ArgumentException("Bad authority index.");

      if (!shares.Verify(this.certificateStorage))
        throw new ArgumentException("Bad signature.");

      if (!shares.Certificate.IsIdentic(this.authorities[shareContainer.AuthorityIndex]))
        throw new ArgumentException("Not signed by proper authority.");

      if (this.shares.ContainsKey(shareContainer.AuthorityIndex))
        throw new ArgumentException("Authority has already deposited shares.");

      this.shares.Add(shareContainer.AuthorityIndex, shares);

      if (this.shares.Count == this.parameters.AuthorityCount)
      {
        Status = VotingStatus.Sharing;
      }
    }

    /// <summary>
    /// Get all shares from all authorities.
    /// </summary>
    /// <returns>All shares list.</returns>
    public AllShareParts GetAllShares()
    {
      if (Status != VotingStatus.Sharing)
        throw new InvalidOperationException("Wrong status for operation.");

      return new AllShareParts(Id, this.shares.Values);
    }

    /// <summary>
    /// Deposit share responses from authorities.
    /// </summary>
    /// <param name="response">Share response.</param>
    public void DepositShareResponse(Signed<ShareResponse> response)
    {
      if (response == null)
        throw new ArgumentNullException("shares");
      if (Status != VotingStatus.Sharing)
        throw new InvalidOperationException("Wrong status for operation.");

      ShareResponse shareResponse = response.Value;

      if (!this.authorities.ContainsKey(shareResponse.AuthorityIndex))
        throw new ArgumentException("Bad authority index.");

      if (!response.Verify(this.certificateStorage))
        throw new ArgumentException("Bad signature.");

      if (!response.Certificate.IsIdentic(this.authorities[shareResponse.AuthorityIndex]))
        throw new ArgumentException("Not signed by proper authority.");

      if (this.responses.ContainsKey(shareResponse.AuthorityIndex))
        throw new ArgumentException("Authority has already deposited shares.");

      this.responses.Add(shareResponse.AuthorityIndex, response);

      if (this.responses.Count == this.parameters.AuthorityCount)
      {
        if (this.responses.Values.All(r => r.Value.AcceptShares))
        {
          Status = VotingStatus.Voting;
        }
        else
        {
          Status = VotingStatus.Aborted;
        }
      }
    }

    /// <summary>
    /// Get material for a voter.
    /// </summary>
    /// <returns>Material to vote.</returns>
    public VotingMaterial GetVotingMaterial()
    {
      return new VotingMaterial(
        this.parameters, 
        this.responses.Values, 
        this.certificateStorage.SignedRevocationLists, 
        this.certificateStorage.Certificates);
    }

    /// <summary>
    /// Deposit a ballot.
    /// </summary>
    /// <param name="ballot">Ballot in signed envleope.</param>
    public void Vote(Signed<Envelope> signedEnvelope)
    {
      if (signedEnvelope == null)
        throw new ArgumentNullException("ballot");
      if (Status != VotingStatus.Voting)
        throw new PiArgumentException(ExceptionCode.WrongStatusForOperation, "Wrong status for operation.");
      if (!signedEnvelope.Verify(this.certificateStorage))
        throw new PiArgumentException(ExceptionCode.VoteSignatureNotValid, "Vote signature not valid.");
      if (!(signedEnvelope.Certificate is VoterCertificate))
        throw new PiArgumentException(ExceptionCode.NoVoterCertificate, "Not a voter certificate.");
      if (signedEnvelopes.Any(envelope => envelope.Certificate.IsIdentic(signedEnvelope.Certificate)))
        throw new PiArgumentException(ExceptionCode.AlreadyVoted, "Voter has already voted.");

      this.signedEnvelopes.Add(signedEnvelope);
    }

    /// <summary>
    /// End the voting procedure.
    /// </summary>
    public void EndVote()
    {
      if (Status != VotingStatus.Voting)
        throw new InvalidOperationException("Wrong status for operation.");

      Status = VotingStatus.Deciphering;
    }

    /// <summary>
    /// Deposit partial deciphers from an authority.
    /// </summary>
    /// <param name="signedPartialDecipherList">Partial decipher list.</param>
    public void DepositPartialDecipher(Signed<PartialDecipherList> signedPartialDecipherList)
    {
      if (signedPartialDecipherList == null)
        throw new ArgumentNullException("partialDecipherContainer");
      if (Status != VotingStatus.Deciphering)
        throw new InvalidOperationException("Wrong status for operation.");

      PartialDecipherList partialDecipherList = signedPartialDecipherList.Value;

      if (!this.authorities.ContainsKey(partialDecipherList.AuthorityIndex))
        throw new ArgumentException("Bad authority index.");

      if (!signedPartialDecipherList.Verify(this.certificateStorage))
        throw new ArgumentException("Bad signature.");

      if (!signedPartialDecipherList.Certificate.IsIdentic(this.authorities[partialDecipherList.AuthorityIndex]))
        throw new ArgumentException("Not signed by proper authority.");

      if (this.partialDeciphers.ContainsKey(partialDecipherList.AuthorityIndex))
        throw new ArgumentException("Authority has already deposited shares.");

      this.partialDeciphers.Add(partialDecipherList.AuthorityIndex, signedPartialDecipherList);

      if (this.partialDeciphers.Count == this.parameters.AuthorityCount)
      {
        Status = VotingStatus.Finished;
      }
    }

    /// <summary>
    /// Get signed envelope.
    /// </summary>
    /// <param name="envelopeIndex">Index of envelope.</param>
    /// <returns>Signed envelope.</returns>
    public Signed<Envelope> GetEnvelope(int envelopeIndex)
    {
      if (!envelopeIndex.InRange(0, this.signedEnvelopes.Count - 1))
        throw new PiArgumentException(ExceptionCode.ArgumentOutOfRange, "Envelope index out of range.");

      return this.signedEnvelopes[envelopeIndex];
    }

    /// <summary>
    /// Get partial decipher list.
    /// </summary>
    /// <param name="authorityIndex">Index of authority.</param>
    /// <returns>Partial decipher list.</returns>
    public Signed<PartialDecipherList> GetPartialDecipher(int authorityIndex)
    {
      if (!this.partialDeciphers.ContainsKey(authorityIndex))
        throw new PiArgumentException(ExceptionCode.ArgumentOutOfRange, "Authority index out of range.");

      return this.partialDeciphers[authorityIndex];
    }

    /// <summary>
    /// Get number of envelopes.
    /// </summary>
    /// <returns>Envelope count.</returns>
    public int GetEnvelopeCount()
    {
      return this.signedEnvelopes.Count;
    }
  }
}
