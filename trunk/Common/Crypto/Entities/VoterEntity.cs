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
    public CertificateStorage CertificateStorage { get; private set; }

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

    public VoterEntity(CertificateStorage certificateStorage, VoterCertificate voterCertificate)
    {
      Certificate = voterCertificate;
      CertificateStorage = certificateStorage;
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

      bool acceptMaterial = SetVotingMaterial(votingMaterial);

      if (vota == null)
        throw new ArgumentNullException("vota");
      if (vota.Count() != this.parameters.Quest.OptionCount)
        throw new ArgumentException("Bad vota count.");
      if (!vota.All(votum => votum.InRange(0, 1)))
        throw new ArgumentException("Votum out of range.");

      if (acceptMaterial)
      {
        Ballot ballot = new Ballot(vota, this.parameters, this.publicKey);
        Envelope ballotContainer = new Envelope(this.parameters.VotingId, Certificate.Id, ballot);

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

      this.tally = new Tally(this.parameters, CertificateStorage, this.publicKey);
    }

    private bool SetVotingMaterial(VotingMaterial votingMaterial)
    {
      bool acceptMaterial = votingMaterial.Valid(CertificateStorage);

      if (acceptMaterial)
      {
        this.parameters = votingMaterial.Parameters.Value;
        this.publicKey = new BigInt(1);

        foreach (Signed<ShareResponse> signedShareResponse in votingMaterial.PublicKeyParts)
        {
          ShareResponse shareResponse = signedShareResponse.Value;
          this.publicKey = (this.publicKey * shareResponse.PublicKeyPart).Mod(this.parameters.Crypto.P);
        }
      }

      return acceptMaterial;
    }

    /// <summary>
    /// Add a vote to the sum of votes.
    /// </summary>
    /// <param name="envelopeIndex">Index of the envelope.</param>
    /// <param name="signedEnvelope">Signed envelope containing the vote.</param>
    public void TallyAdd(int envelopeIndex, Signed<Envelope> signedEnvelope)
    {
      this.tally.Add(envelopeIndex, signedEnvelope);
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
