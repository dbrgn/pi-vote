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
using Emil.GMP;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Entity of a voter.
  /// </summary>
  public class VoterEntity
  {
    /// <summary>
    /// Storage of certificates.
    /// </summary>
    private CertificateStorage certificateStorage;

    /// <summary>
    /// Public key of the authorities.
    /// </summary>
    private BigInt publicKey;

    /// <summary>
    /// Voting parameters.
    /// </summary>
    private VotingParameters parameters;

    /// <summary>
    /// Private certificate of this voter.
    /// </summary>
    public VoterCertificate Certificate { get; private set; }

    /// <summary>
    /// Tally of voting.
    /// </summary>
    private Tally tally;

    public VoterEntity(CACertificate rootCertificate, VoterCertificate voterCertificate)
    {
      Certificate = voterCertificate;
      this.certificateStorage = new CertificateStorage();
      this.certificateStorage.AddRoot(rootCertificate);
    }

    /// <summary>
    /// Cast a vote and pack it in an envelope.
    /// </summary>
    /// <param name="votingMaterial">Voting material.</param>
    /// <param name="vota">List of vota.</param>
    /// <returns>Signed envelope containing the ballot.</returns>
    public Signed<Envelope> Vote(VotingMaterial votingMaterial, IEnumerable<int> vota)
    {
      if (votingMaterial == null)
        throw new ArgumentNullException("votingMaterial");
      if (vota == null)
        throw new ArgumentNullException("vota");
      if (vota.Count() != votingMaterial.Parameters.OptionCount)
        throw new ArgumentException("Bad vota count.");
      if (!vota.All(votum => votum.InRange(0, 1)))
        throw new ArgumentException("Votum out of range.");

      bool acceptMaterial = SetVotingMaterial(votingMaterial);

      if (acceptMaterial)
      {
        Ballot ballot = new Ballot(vota, votingMaterial.Parameters, this.publicKey);
        Envelope ballotContainer = new Envelope(votingMaterial.VotingId, Certificate.Id, ballot);

        return new Signed<Envelope>(ballotContainer, Certificate);
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Begin summation of votes.
    /// </summary>
    public void TallyBegin(VotingMaterial votingMaterial)
    {
      if (votingMaterial == null)
        throw new ArgumentNullException("votingMaterial");

      if (!SetVotingMaterial(votingMaterial))
        throw new PiArgumentException(ExceptionCode.BadVotingMaterial, "Bad voting material");

      this.tally = new Tally(this.parameters, this.certificateStorage, this.publicKey);
    }

    private bool SetVotingMaterial(VotingMaterial votingMaterial)
    {
      this.parameters = votingMaterial.Parameters;
      bool acceptMaterial = true;

      this.publicKey = new BigInt(1);

      foreach (Certificate certificate in votingMaterial.Certificates)
      {
        this.certificateStorage.Add(certificate);
      }

      foreach (Signed<RevocationList> signedRevocationList in votingMaterial.RevocationLists)
      {
        this.certificateStorage.SetRevocationList(signedRevocationList);
      }

      foreach (Signed<ShareResponse> signedShareResponse in votingMaterial.PublicKeyParts)
      {
        acceptMaterial &= signedShareResponse.Verify(this.certificateStorage);

        ShareResponse shareResponse = signedShareResponse.Value;
        acceptMaterial &= shareResponse.AcceptShares;
        this.publicKey = (this.publicKey * shareResponse.PublicKeyPart).Mod(this.parameters.P);
      }

      return acceptMaterial;
    }

    /// <summary>
    /// Add a vote to the sum of votes.
    /// </summary>
    /// <param name="signedEnvelope">Signed envelope containing the vote.</param>
    public void TallyAdd(Signed<Envelope> signedEnvelope)
    {
      this.tally.Add(signedEnvelope);
    }

    /// <summary>
    /// Add a partial decipher for deciphering the sum of votes.
    /// </summary>
    /// <param name="signedPartialDecipherList">List of partial deciphers.</param>
    public void TallyAddPartialDecipher(Signed<PartialDecipherList> signedPartialDecipherList)
    {
      this.tally.AddPartialDecipher(signedPartialDecipherList);
    }

    /// <summary>
    /// Result of the voting.
    /// </summary>
    public VotingResult TallyResult
    {
      get
      {
        return this.tally.Result;
      }
    }
  }
}
