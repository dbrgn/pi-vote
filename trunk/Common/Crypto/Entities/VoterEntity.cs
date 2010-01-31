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

    /// <summary>
    /// Tally and verify the result.
    /// </summary>
    /// <param name="votingContainer">Container of voting procedure.</param>
    /// <returns>Results for each option.</returns>
    public VotingResult Result(VotingContainer votingContainer)
    {
      if (votingContainer == null)
        throw new ArgumentNullException("votingContainer");

      VotingResult result = new VotingResult(votingContainer.VotingId, votingContainer.Material.Parameters.VotingName);

      Vote[] voteSums = CalculateBallotSums(votingContainer.Emvelopes, result);

      List<PartialDecipher> partialDeciphers = ListPartialDeciphers(votingContainer.PartialDeciphers);

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
            optionResults.Add(voteSums[optionIndex].Decrypt(partialDeciphersByOptionAndGroup, parameters));
        }

        Option option = votingContainer.Parameters.Options.ElementAt(optionIndex);

        if (optionResults.Count > 0)
        {
          int firstOptionResult = optionResults[0];

          if (optionResults.All(optionResult => optionResult == firstOptionResult))
          {
            result.Options.Add(new OptionResult(option.Text, option.Description, firstOptionResult));
          }
          else
          {
            result.Options.Add(new OptionResult(option.Text, option.Description, -1));
          }
        }
        else
        {
          result.Options.Add(new OptionResult(option.Text, option.Description, -1));
        }
      }

      return result;
    }

    /// <summary>
    /// Lists all valid partail deciphers.
    /// </summary>
    /// <param name="signedPartialDeciphers">List of signed partail deciphers.</param>
    /// <returns>List of partail deciphers.</returns>
    private List<PartialDecipher> ListPartialDeciphers(IEnumerable<Signed<PartialDecipherList>> signedPartialDeciphers)
    {
      if (signedPartialDeciphers == null)
        throw new ArgumentNullException("signedPartialDeciphers");

      List<PartialDecipher> partialDeciphers = new List<PartialDecipher>();

      foreach (Signed<PartialDecipherList> signedPartialDeciphersContainer in signedPartialDeciphers)
      {
        if (signedPartialDeciphersContainer.Verify(this.certificateStorage))
        {
          PartialDecipherList partialDeciphersContainer = signedPartialDeciphersContainer.Value;
          partialDeciphers.AddRange(partialDeciphersContainer.PartialDeciphers);
        }
      }
      return partialDeciphers;
    }

    /// <summary>
    /// Calculates the sum of all ballots for each option.
    /// </summary>
    /// <param name="envelopes">Envelopes from all voters.</param>
    /// <returns>Sums of ballots.</returns>
    private Vote[] CalculateBallotSums(IEnumerable<Signed<Envelope>> envelopes, VotingResult result)
    {
      if (envelopes == null)
        throw new ArgumentNullException("envelopes");
      if (result == null)
        throw new ArgumentNullException("result");

      Vote[] voteSums = new Vote[this.parameters.OptionCount];

      foreach (Signed<Envelope> signedEnvelope in envelopes)
      {
        bool acceptVote = true;

        acceptVote &= signedEnvelope.Verify(this.certificateStorage);

        Envelope envelope = signedEnvelope.Value;
        acceptVote &= envelope.Ballot.Verify(this.publicKey, this.parameters);

        if (acceptVote)
        {
          for (int optionIndex = 0; optionIndex < this.parameters.OptionCount; optionIndex++)
          {
            voteSums[optionIndex] =
              voteSums[optionIndex] == null ?
              envelope.Ballot.Votes[optionIndex] :
              voteSums[optionIndex] + envelope.Ballot.Votes[optionIndex];
          }
        }

        result.Voters.Add(new EnvelopeResult(envelope.VoterId, acceptVote));
      }
      return voteSums;
    }
  }
}
