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
  public class VoterEntity
  {
    private BigInt publicKey;

    private ParameterContainer parameters;

    public int VoterId { get; private set; }

    public string VoterName { get; private set; }

    public Certificate Certificate { get; private set; }
    
    public VoterEntity(int voterId, string voterName)
    {
      VoterId = voterId;
      VoterName = voterName;
      Certificate = new Certificate();
    }

    public SignedContainer<BallotContainer> Vote(VotingMaterial votingMaterial, IEnumerable<int> vota)
    {
      this.parameters = votingMaterial.ParameterContainer;
      bool acceptMaterial = true;
      this.publicKey = new BigInt(1);

      foreach (SignedContainer<ShareResponse> signedShareResponse in votingMaterial.PublicKeyParts)
      {
        acceptMaterial &= signedShareResponse.Verify();

        ShareResponse shareResponse = signedShareResponse.Value;
        acceptMaterial &= shareResponse.AcceptShares;
        this.publicKey = (this.publicKey * shareResponse.PublicKeyPart).Mod(this.parameters.Parameters.P);
      }

      if (acceptMaterial)
      {
        Ballot ballot = new Ballot(vota, votingMaterial.ParameterContainer.Parameters, this.publicKey);
        BallotContainer ballotContainer = new BallotContainer(votingMaterial.VotingId, VoterId, VoterName, ballot);

        return new SignedContainer<BallotContainer>(ballotContainer, Certificate);
      }
      else
      {
        return null;
      }
    }

    public IEnumerable<int> Result(VotingContainer votingContainer)
    {
      Vote[] voteSums = new Vote[this.parameters.Parameters.OptionCount];

      foreach (SignedContainer<BallotContainer> signedBallotContainer in votingContainer.Ballots)
      {
        bool acceptVote = true;

        acceptVote &= signedBallotContainer.Verify();

        BallotContainer ballotContainer = signedBallotContainer.Value;
        acceptVote &= ballotContainer.Ballot.Verify(this.publicKey, this.parameters.Parameters);

        if (acceptVote)
        {
          for (int optionIndex = 0; optionIndex < this.parameters.Parameters.OptionCount; optionIndex++)
          {
            voteSums[optionIndex] =
              voteSums[optionIndex] == null ?
              ballotContainer.Ballot.Votes[optionIndex] :
              voteSums[optionIndex] + ballotContainer.Ballot.Votes[optionIndex];
          }
        }
      }

      List<PartialDecipher> partialDeciphers = new List<PartialDecipher>();

      foreach (SignedContainer<PartialDeciphersContainer> signedPartialDeciphersContainer in votingContainer.PartialDeciphers)
      {
        if (signedPartialDeciphersContainer.Verify())
        {
          PartialDeciphersContainer partialDeciphersContainer = signedPartialDeciphersContainer.Value;
          partialDeciphers.AddRange(partialDeciphersContainer.PartialDeciphers);
        }
      }

      List<int> results = new List<int>();

      for (int optionIndex = 0; optionIndex < this.parameters.Parameters.OptionCount; optionIndex++)
      {
        IEnumerable<BigInt> partialDeciphers0 = partialDeciphers
          .Where(partialDecipher => partialDecipher.GroupIndex == 1 && partialDecipher.OptionIndex == optionIndex)
          .Select(partialDecipher => partialDecipher.Value);
        int v0 = partialDeciphers0.Count() == this.parameters.Parameters.Thereshold + 1 ?
          voteSums[optionIndex].Decrypt(partialDeciphers0, parameters.Parameters) :
          -1;

        IEnumerable<BigInt> partialDeciphers1 = partialDeciphers
          .Where(partialDecipher => partialDecipher.GroupIndex == 1 && partialDecipher.OptionIndex == optionIndex)
          .Select(partialDecipher => partialDecipher.Value);
        int v1 = partialDeciphers1.Count() == this.parameters.Parameters.Thereshold + 1 ?
          voteSums[optionIndex].Decrypt(partialDeciphers1, parameters.Parameters) :
          -1;

        IEnumerable<BigInt> partialDeciphers2 = partialDeciphers
          .Where(partialDecipher => partialDecipher.GroupIndex == 1 && partialDecipher.OptionIndex == optionIndex)
          .Select(partialDecipher => partialDecipher.Value);
        int v2 = partialDeciphers2.Count() == this.parameters.Parameters.Thereshold + 1 ?
          voteSums[optionIndex].Decrypt(partialDeciphers2, parameters.Parameters) :
          -1;

        IEnumerable<BigInt> partialDeciphers3 = partialDeciphers
          .Where(partialDecipher => partialDecipher.GroupIndex == 1 && partialDecipher.OptionIndex == optionIndex)
          .Select(partialDecipher => partialDecipher.Value);
        int v3 = partialDeciphers3.Count() == this.parameters.Parameters.Thereshold + 1 ?
          voteSums[optionIndex].Decrypt(partialDeciphers3, parameters.Parameters) :
          -1;

        IEnumerable<BigInt> partialDeciphers4 = partialDeciphers
          .Where(partialDecipher => partialDecipher.GroupIndex == 1 && partialDecipher.OptionIndex == optionIndex)
          .Select(partialDecipher => partialDecipher.Value);
        int v4 = partialDeciphers4.Count() == this.parameters.Parameters.Thereshold + 1 ?
          voteSums[optionIndex].Decrypt(partialDeciphers4, parameters.Parameters) :
          -1;

        results.Add(v0);
      }

      return results;
    }
  }
}
