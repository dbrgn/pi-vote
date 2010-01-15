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
  public class AuthorityEntity
  {
    private ParameterContainer parameters;
    private Authority authority;
    private Dictionary<int, Certificate> authorities;

    public Certificate Certificate { get; private set; }

    public AuthorityEntity()
    {
      Certificate = new Certificate();
    }

    public void Prepare(int index, ParameterContainer parameters)
    {
      this.parameters = parameters;
      this.authority = new Authority(index, this.parameters.Parameters);
      this.authority.CreatePolynomial();
      this.authorities = new Dictionary<int, Certificate>();
    }

    public void SetAuthorities(AuthorityList list)
    {
      for (int index = 0; index < list.Authorities.Count; index++)
      {
        this.authorities.Add(index + 1, list.Authorities[index]);
      }
    }

    public SignedContainer<ShareContainer> GetShares()
    {
      ShareContainer shareContainer = new ShareContainer(this.authority.Index);

      for (int authorityIndex = 1; authorityIndex <= this.parameters.Parameters.AuthorityCount; authorityIndex++)
      {
        Share share = this.authority.Share(authorityIndex);
        shareContainer.EncryptedShares.Add(
          new EncryptedContainer<Share>(share, this.authorities[authorityIndex]));
      }

      for (int valueIndex = 0; valueIndex <= this.parameters.Parameters.Thereshold; valueIndex++)
      {
        shareContainer.VerificationValues.Add(this.authority.VerificationValue(valueIndex));
      }

      return new SignedContainer<ShareContainer>(shareContainer, Certificate);
    }

    public SignedContainer<ShareResponse> VerifyShares(AllSharesContainer allShares)
    {
      List<Share> shares = new List<Share>();
      List<List<VerificationValue>> As = new List<List<VerificationValue>>();
      bool acceptShares = true;

      foreach (SignedContainer<ShareContainer> singedShareContainer in allShares.Shares)
      {
        ShareContainer shareContainer = singedShareContainer.Value;

        acceptShares &= singedShareContainer.Verify();
        acceptShares &= singedShareContainer.Certificate.IsIdentic(this.authorities[shareContainer.AuthorityIndex]);

        EncryptedContainer<Share> encryptedShare = shareContainer.EncryptedShares[this.authority.Index - 1];
        shares.Add(encryptedShare.Decrypt(Certificate));

        As.Add(new List<VerificationValue>());

        for (int l = 0; l <= this.parameters.Parameters.Thereshold; l++)
        {
          As[As.Count - 1].Add(shareContainer.VerificationValues[l]);
        }
      }

      acceptShares &= this.authority.VerifySharing(shares, As);

      BigInt publicKeyPart = acceptShares ? this.authority.PublicKeyPart : new BigInt(0);

      ShareResponse response = new ShareResponse(allShares.VotingId, this.authority.Index, acceptShares, publicKeyPart);

      return new SignedContainer<ShareResponse>(response, Certificate);
    }

    public SignedContainer<PartialDeciphersContainer> PartiallyDecipher(AllBallotsContainer ballotsContainer)
    {
      BigInt publicKey = new BigInt(1);

      foreach (SignedContainer<ShareResponse> signedShareResponse in ballotsContainer.VotingMaterial.PublicKeyParts)
      {
        ShareResponse shareResponse = signedShareResponse.Value;
        bool acceptResponse = true;
        
        acceptResponse &= signedShareResponse.Verify();
        acceptResponse &= signedShareResponse.Certificate.IsIdentic(this.authorities[shareResponse.AuthorityIndex]);

        if (!acceptResponse)
          return null;

        publicKey = (publicKey * shareResponse.PublicKeyPart).Mod(this.parameters.Parameters.P);
      }

      Vote[] voteSums = new Vote[this.parameters.Parameters.OptionCount];

      foreach (SignedContainer<BallotContainer> signedBallotContainer in ballotsContainer.Ballots)
      {
        bool acceptVote = true;
        
        acceptVote &= signedBallotContainer.Verify();

        BallotContainer ballotContainer = signedBallotContainer.Value;
        acceptVote &= ballotContainer.Ballot.Verify(publicKey, this.parameters.Parameters);

        if (acceptVote)
        {
          for (int optionIndex = 0; optionIndex < this.parameters.Parameters.OptionCount; optionIndex ++)
          {
            voteSums[optionIndex] =
              voteSums[optionIndex] == null ?
              ballotContainer.Ballot.Votes[optionIndex] :
              voteSums[optionIndex] + ballotContainer.Ballot.Votes[optionIndex];
          }
        }
      }

      PartialDeciphersContainer partialDeciphersContainer = new PartialDeciphersContainer(ballotsContainer.VotingId, this.authority.Index);

      for (int optionIndex = 0; optionIndex < this.parameters.Parameters.OptionCount; optionIndex ++)
      {
        partialDeciphersContainer.PartialDeciphers.AddRange(this.authority.PartialDeciphers(voteSums[optionIndex], optionIndex));
      }

      return new SignedContainer<PartialDeciphersContainer>(partialDeciphersContainer, Certificate);
    }
  }
}
