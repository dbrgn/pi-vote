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
  /// Summation of votes for calculating partial deciphers or result.
  /// </summary>
  public class Tally
  {
    /// <summary>
    /// Sums of votes.
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

    /// <summary>
    /// Voting parameters.
    /// </summary>
    private VotingParameters parameters;

    /// <summary>
    /// Certificate storage to verify against.
    /// </summary>
    private CertificateStorage certificateStorage;

    /// <summary>
    /// Public key with which the votes where encrypted.
    /// </summary>
    private BigInt publicKey;

    /// <summary>
    /// List of voters who votes where already counted.
    /// </summary>
    private List<Guid> countedVoters;

    /// <summary>
    /// Creates a new summation of votes.
    /// </summary>
    /// <param name="parameters">Voting parameters.</param>
    /// <param name="certificateStorage">Certificate storage to verify against.</param>
    /// <param name="publicKey">Public key with which the votes where encrypted.</param>
    public Tally(
      VotingParameters parameters,
      CertificateStorage certificateStorage, 
      BigInt publicKey)
    {
      this.parameters = parameters;
      this.certificateStorage = certificateStorage;
      this.publicKey = publicKey;

      this.voteSums = new Vote[this.parameters.OptionCount];
      this.result = new VotingResult(this.parameters.VotingId, this.parameters.VotingName);
      this.partialDeciphers = new List<PartialDecipher>();
      this.countedVoters = new List<Guid>();
    }

    /// <summary>
    /// Adds a vote to the vote sum.
    /// </summary>
    /// <remarks>
    /// Verfies the correctness of the vote first.
    /// </remarks>
    /// <param name="signedEnvelope">Signed envelope from voter.</param>
    public void Add(Signed<Envelope> signedEnvelope)
    {
      if (signedEnvelope == null)
        throw new ArgumentNullException("signedEnvelope");
      if (voteSums == null)
        throw new InvalidOperationException("Must call TallyBegin first.");

      bool acceptVote = true;

      acceptVote &= signedEnvelope.Verify(this.certificateStorage);

      if (this.countedVoters.Contains(signedEnvelope.Certificate.Id))
        acceptVote = false;
      else
        this.countedVoters.Add(signedEnvelope.Certificate.Id);

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

    /// <summary>
    /// Sums of votes for all options.
    /// </summary>
    public Vote[] VoteSums
    {
      get { return this.voteSums; }
    }

    /// <summary>
    /// Add a partial decipher.
    /// </summary>
    /// <param name="signedPartialDecipherList">List of partial deciphers.</param>
    public void AddPartialDecipher(Signed<PartialDecipherList> signedPartialDecipherList)
    {
      if (signedPartialDecipherList.Verify(this.certificateStorage))
      {
        PartialDecipherList partialDeciphersContainer = signedPartialDecipherList.Value;
        partialDeciphers.AddRange(partialDeciphersContainer.PartialDeciphers);
      }
    }

    /// <summary>
    /// Calculates the result of the voting.
    /// </summary>
    public VotingResult Result
    {
      get
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
}
