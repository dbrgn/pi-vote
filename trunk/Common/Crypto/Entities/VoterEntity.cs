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
    /// Full name of this voter.
    /// </summary>
    public string VoterName { get; private set; }

    /// <summary>
    /// Private certificate of this voter.
    /// </summary>
    private Certificate certificate;
    
    public VoterEntity(int voterId, string voterName)
    {
      VoterId = voterId;
      VoterName = voterName;
      this.certificate = new Certificate();
    }

    /// <summary>
    /// Cast a vote and pack it in an envelope.
    /// </summary>
    /// <param name="votingMaterial">Voting material.</param>
    /// <param name="vota">List of vota.</param>
    /// <returns>Signed envelope containing the ballot.</returns>
    public Signed<Envelope> Vote(VotingMaterial votingMaterial, IEnumerable<int> vota)
    {
      this.parameters = votingMaterial.ParameterContainer;
      bool acceptMaterial = true;
      this.publicKey = new BigInt(1);

      foreach (Signed<ShareResponse> signedShareResponse in votingMaterial.PublicKeyParts)
      {
        acceptMaterial &= signedShareResponse.Verify();

        ShareResponse shareResponse = signedShareResponse.Value;
        acceptMaterial &= shareResponse.AcceptShares;
        this.publicKey = (this.publicKey * shareResponse.PublicKeyPart).Mod(this.parameters.P);
      }

      if (acceptMaterial)
      {
        Ballot ballot = new Ballot(vota, votingMaterial.ParameterContainer, this.publicKey);
        Envelope ballotContainer = new Envelope(votingMaterial.VotingId, VoterId, VoterName, ballot);

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
    public IEnumerable<int> Result(VotingContainer votingContainer)
    {
      Vote[] voteSums = new Vote[this.parameters.OptionCount];

      foreach (Signed<Envelope> signedBallotContainer in votingContainer.Emvelopes)
      {
        bool acceptVote = true;

        acceptVote &= signedBallotContainer.Verify();

        Envelope ballotContainer = signedBallotContainer.Value;
        acceptVote &= ballotContainer.Ballot.Verify(this.publicKey, this.parameters);

        if (acceptVote)
        {
          for (int optionIndex = 0; optionIndex < this.parameters.OptionCount; optionIndex++)
          {
            voteSums[optionIndex] =
              voteSums[optionIndex] == null ?
              ballotContainer.Ballot.Votes[optionIndex] :
              voteSums[optionIndex] + ballotContainer.Ballot.Votes[optionIndex];
          }
        }
      }

      List<PartialDecipher> partialDeciphers = new List<PartialDecipher>();

      foreach (Signed<PartialDecipherList> signedPartialDeciphersContainer in votingContainer.PartialDeciphers)
      {
        if (signedPartialDeciphersContainer.Verify())
        {
          PartialDecipherList partialDeciphersContainer = signedPartialDeciphersContainer.Value;
          partialDeciphers.AddRange(partialDeciphersContainer.PartialDeciphers);
        }
      }

      List<int> results = new List<int>();

      for (int optionIndex = 0; optionIndex < this.parameters.OptionCount; optionIndex++)
      {
        List<int> optionResults = new List<int>();

        IEnumerable<BigInt> partialDeciphers0 = partialDeciphers
          .Where(partialDecipher => partialDecipher.GroupIndex == 1 && partialDecipher.OptionIndex == optionIndex)
          .Select(partialDecipher => partialDecipher.Value);
        if (partialDeciphers0.Count() == this.parameters.Thereshold + 1)
          optionResults.Add(voteSums[optionIndex].Decrypt(partialDeciphers0, parameters));

        IEnumerable<BigInt> partialDeciphers1 = partialDeciphers
          .Where(partialDecipher => partialDecipher.GroupIndex == 1 && partialDecipher.OptionIndex == optionIndex)
          .Select(partialDecipher => partialDecipher.Value);
        if (partialDeciphers1.Count() == this.parameters.Thereshold + 1)
          optionResults.Add(voteSums[optionIndex].Decrypt(partialDeciphers1, parameters));

        IEnumerable<BigInt> partialDeciphers2 = partialDeciphers
          .Where(partialDecipher => partialDecipher.GroupIndex == 1 && partialDecipher.OptionIndex == optionIndex)
          .Select(partialDecipher => partialDecipher.Value);
        if (partialDeciphers2.Count() == this.parameters.Thereshold + 1)
          optionResults.Add(voteSums[optionIndex].Decrypt(partialDeciphers2, parameters));

        IEnumerable<BigInt> partialDeciphers3 = partialDeciphers
          .Where(partialDecipher => partialDecipher.GroupIndex == 1 && partialDecipher.OptionIndex == optionIndex)
          .Select(partialDecipher => partialDecipher.Value);
        if (partialDeciphers3.Count() == this.parameters.Thereshold + 1)
          optionResults.Add(voteSums[optionIndex].Decrypt(partialDeciphers3, parameters));

        IEnumerable<BigInt> partialDeciphers4 = partialDeciphers
          .Where(partialDecipher => partialDecipher.GroupIndex == 1 && partialDecipher.OptionIndex == optionIndex)
          .Select(partialDecipher => partialDecipher.Value);
        if (partialDeciphers4.Count() == this.parameters.Thereshold + 1)
          optionResults.Add(voteSums[optionIndex].Decrypt(partialDeciphers4, parameters));

        if (optionResults.Count > 0)
        {
          int firstOptionResult = optionResults[0];

          if (optionResults.All(optionResult => optionResult == firstOptionResult))
          {
            results.Add(firstOptionResult);
          }
          else
          {
            results.Add(-1);
          }
        }
        else
        {
          results.Add(-1);
        }
      }

      return results;
    }
  }
}
