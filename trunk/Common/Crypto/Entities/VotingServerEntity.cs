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
    /// List of ballots from voters.
    /// </summary>
    private List<Signed<Envelope>> ballots;

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
      CACertificate rootCertificate, 
      CACertificate intermediateCertificate,
      Signed<RevocationList> rootRevocationList,
      Signed<RevocationList> intermediateRevocationList)
    {
      if (parameters == null)
        throw new ArgumentNullException("parameters");

      this.parameters = parameters;
      this.authorities = new Dictionary<int, Certificate>();
      this.shares = new Dictionary<int, Signed<SharePart>>();
      this.responses = new Dictionary<int, Signed<ShareResponse>>();
      this.ballots = new List<Signed<Envelope>>();
      this.partialDeciphers = new Dictionary<int, Signed<PartialDecipherList>>();
      this.certificateStorage = new CertificateStorage();
      this.certificateStorage.AddRoot(rootCertificate);
      this.certificateStorage.Add(intermediateCertificate);
      this.certificateStorage.SetRevocationList(rootRevocationList);
      this.certificateStorage.SetRevocationList(intermediateRevocationList);
      Status = VotingStatus.New;
    }

    /// <summary>
    /// Voting parameters.
    /// </summary>
    public VotingParameters Parameters { get { return this.parameters; } }

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
      if (Status != VotingStatus.Voting)
        throw new InvalidOperationException("Wrong status for operation.");

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
    public void Vote(Signed<Envelope> ballot)
    {
      if (ballot == null)
        throw new ArgumentNullException("ballot");
      if (Status != VotingStatus.Voting)
        throw new InvalidOperationException("Wrong status for operation.");
      if (!ballot.Verify(this.certificateStorage))
        throw new ArgumentException("Bad signature.");
      if (!CanVote(ballot.Certificate))
        throw new ArgumentException("Not allowed to vote.");

      this.ballots.Add(ballot);
    }

    /// <summary>
    /// Is this one allowed to vote?
    /// </summary>
    /// <param name="certificate">Certificate to check.</param>
    /// <returns>Can vote?</returns>
    private bool CanVote(Certificate certificate)
    {
      return certificate is VoterCertificate;
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
    /// Get all ballots for authorities to decrypt sum.
    /// </summary>
    /// <returns>Ballot list for authorities.</returns>
    public AuthorityEnvelopeList GetAllBallots()
    {
      if (Status != VotingStatus.Deciphering)
        throw new InvalidOperationException("Wrong status for operation.");

      VotingMaterial material = new VotingMaterial(
        this.parameters,
        this.responses.Values,
        this.certificateStorage.SignedRevocationLists,
        this.certificateStorage.Certificates);

      return new AuthorityEnvelopeList(Id, this.ballots, material);
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
    /// Get the voting result.
    /// </summary>
    /// <returns>Voting result.</returns>
    public VotingContainer GetVotingResult()
    {
      if (Status != VotingStatus.Finished)
        throw new InvalidOperationException("Wrong status for operation.");

      VotingMaterial material = new VotingMaterial(
        this.parameters,
        this.responses.Values,
        this.certificateStorage.SignedRevocationLists,
        this.certificateStorage.Certificates);

      return new VotingContainer(material, this.ballots, this.partialDeciphers.Values);
    }
  }
}
