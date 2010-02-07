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
    /// Id of this voter.
    /// </summary>
    public int VoterId { get; private set; }

    /// <summary>
    /// Private certificate of this voter.
    /// </summary>
    private Certificate certificate;

    /// <summary>
    /// Sum of votes;
    /// </summary>
    private Vote[] voteSums;

    /// <summary>
    /// Result of voting.
    /// </summary>
    private VotingResult result;

    /// <summary>
    /// List of partial deciphers;
    /// </summary>
    private List<PartialDecipher> partialDeciphers;

    public VoterEntity(int voterId, CACertificate rootCertificate, VoterCertificate voterCertificate)
    {
      VoterId = voterId;
      this.certificate = voterCertificate;
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

      if (acceptMaterial)
      {
        Ballot ballot = new Ballot(vota, votingMaterial.Parameters, this.publicKey);
        Envelope ballotContainer = new Envelope(votingMaterial.VotingId, VoterId, ballot);

        return new Signed<Envelope>(ballotContainer, this.certificate);
      }
      else
      {
        return null;
      }
    }

    public void ResetResult()
    {
      this.voteSums = new Vote[this.parameters.OptionCount];
      this.result = new VotingResult(this.parameters.VotingId, this.parameters.VotingName);
      this.partialDeciphers = new List<PartialDecipher>();
    }

    public void AddVoteToResult(Signed<Envelope> signedEnvelope)
    {
      bool acceptVote = true;

      acceptVote &= signedEnvelope.Verify(this.certificateStorage);

      Envelope envelope = signedEnvelope.Value;
      acceptVote &= envelope.Ballot.Verify(this.publicKey, this.parameters);

      if (acceptVote)
      {
        for (int optionIndex = 0; optionIndex < this.parameters.OptionCount; optionIndex++)
        {
          this.voteSums[optionIndex] =
            this.voteSums[optionIndex] == null ?
            envelope.Ballot.Votes[optionIndex] :
            this.voteSums[optionIndex] + envelope.Ballot.Votes[optionIndex];
        }
      }

      this.result.Voters.Add(new EnvelopeResult(envelope.VoterId, acceptVote));
    }

    public void AddPartialDecipher(Signed<PartialDecipherList> signedPartialDecipherList)
    {
      if (signedPartialDecipherList.Verify(this.certificateStorage))
      {
        PartialDecipherList partialDeciphersContainer = signedPartialDecipherList.Value;
        partialDeciphers.AddRange(partialDeciphersContainer.PartialDeciphers);
      }
    }

    public VotingResult Result()
    {
      List<int> results = new List<int>();

      for (int optionIndex = 0; optionIndex < this.parameters.OptionCount; optionIndex++)
      {
        List<int> optionResults = new List<int>();

        for (int groupIndex = 1; groupIndex < this.parameters.AuthorityCount; groupIndex++)
        {
          IEnumerable<BigInt> partialDeciphersByOptionAndGroup = partialDeciphers
            .Where(partialDecipher => partialDecipher.GroupIndex == groupIndex && partialDecipher.OptionIndex == optionIndex)
            .Select(partialDecipher => partialDecipher.Value);
          if (partialDeciphersByOptionAndGroup.Count() == this.parameters.Thereshold + 1)
            optionResults.Add(this.voteSums[optionIndex].Decrypt(partialDeciphersByOptionAndGroup, parameters));
        }

        Option option = this.parameters.Options.ElementAt(optionIndex);

        if (optionResults.Count > 0)
        {
          int firstOptionResult = optionResults[0];

          if (optionResults.All(optionResult => optionResult == firstOptionResult))
          {
            this.result.Options.Add(new OptionResult(option.Text, option.Description, firstOptionResult));
          }
          else
          {
            this.result.Options.Add(new OptionResult(option.Text, option.Description, -1));
          }
        }
        else
        {
          this.result.Options.Add(new OptionResult(option.Text, option.Description, -1));
        }
      }

      return this.result;
    }
  }
}
