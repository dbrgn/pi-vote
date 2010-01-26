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
  /// Entity controling an authority.
  /// </summary>
  public class AuthorityEntity
  {
    /// <summary>
    /// Parameters for the voting.
    /// </summary>
    private VotingParameters parameters;

    /// <summary>
    /// Cryptographic authority.
    /// </summary>
    private Authority authority;

    /// <summary>
    /// Certificates of all authorities.
    /// </summary>
    private Dictionary<int, Certificate> authorities;

    /// <summary>
    /// Private certificate of this authority.
    /// </summary>
    private Certificate certificate;

    /// <summary>
    /// Public certificate of this authority.
    /// </summary>
    public Certificate Certificate
    {
      get { return this.certificate.OnlyPublicPart; }
    }

    /// <summary>
    /// Create a new authority entity.
    /// </summary>
    /// <param name="certificate">Certificate of authority.</param>
    public AuthorityEntity(Certificate certificate)
    {
      this.certificate = certificate;
    }

    /// <summary>
    /// Prepares the authority for voting procedure.
    /// </summary>
    /// <param name="index">Index given to this authority.</param>
    /// <param name="parameters">Voting parameters for the procedure.</param>
    public void Prepare(int index, VotingParameters parameters)
    {
      if (index < 1)
        throw new ArgumentException("Index must be at least 1.");
      if (parameters == null)
        throw new ArgumentNullException("parameters");

      this.parameters = parameters;
      this.authority = new Authority(index, this.parameters);
      this.authority.CreatePolynomial();
      this.authorities = new Dictionary<int, Certificate>();
    }

    /// <summary>
    /// Set certificates of all authories in the procedure.
    /// </summary>
    /// <param name="list">List of authorities.</param>
    public void SetAuthorities(AuthorityList list)
    {
      if (list == null)
        throw new ArgumentNullException("list");

      for (int index = 0; index < list.Authorities.Count; index++)
      {
        this.authorities.Add(index + 1, list.Authorities[index]);
      }
    }

    /// <summary>
    /// Gets the share part from this authority.
    /// </summary>
    /// <returns>Signed share part.</returns>
    public Signed<SharePart> GetShares()
    {
      SharePart shareContainer = new SharePart(this.parameters.VotingId, this.authority.Index);

      for (int authorityIndex = 1; authorityIndex <= this.parameters.AuthorityCount; authorityIndex++)
      {
        Share share = this.authority.Share(authorityIndex);
        shareContainer.EncryptedShares.Add(
          new Encrypted<Share>(share, this.authorities[authorityIndex]));
      }

      for (int valueIndex = 0; valueIndex <= this.parameters.Thereshold; valueIndex++)
      {
        shareContainer.VerificationValues.Add(this.authority.VerificationValue(valueIndex));
      }

      return new Signed<SharePart>(shareContainer, this.certificate);
    }

    /// <summary>
    /// Verifies shares from all authorities.
    /// </summary>
    /// <param name="allShareParts">All share parts from all authorities.</param>
    /// <returns>Signed response.</returns>
    public Signed<ShareResponse> VerifyShares(AllShareParts allShareParts)
    {
      if (allShareParts == null)
        throw new ArgumentNullException("allShareParts");

      List<Share> shares = new List<Share>();
      List<List<VerificationValue>> verificationValuesByAuthority = new List<List<VerificationValue>>();
      bool acceptShares = true;

      foreach (Signed<SharePart> signedShareParrt in allShareParts.ShareParts)
      {
        SharePart sharePart = signedShareParrt.Value;

        acceptShares &= signedShareParrt.Verify();
        acceptShares &= signedShareParrt.Certificate.IsIdentic(this.authorities[sharePart.AuthorityIndex]);

        Encrypted<Share> encryptedShare = sharePart.EncryptedShares[this.authority.Index - 1];
        shares.Add(encryptedShare.Decrypt(this.certificate));

        verificationValuesByAuthority.Add(new List<VerificationValue>());

        for (int l = 0; l <= this.parameters.Thereshold; l++)
        {
          verificationValuesByAuthority[verificationValuesByAuthority.Count - 1].Add(sharePart.VerificationValues[l]);
        }
      }

      acceptShares &= this.authority.VerifySharing(shares, verificationValuesByAuthority);

      BigInt publicKeyPart = acceptShares ? this.authority.PublicKeyPart : new BigInt(0);

      ShareResponse response = new ShareResponse(allShareParts.VotingId, this.authority.Index, acceptShares, publicKeyPart);

      return new Signed<ShareResponse>(response, this.certificate);
    }

    /// <summary>
    /// Partially deciphers the sum of votes.
    /// </summary>
    /// <param name="envelopeList">List of envelopes from which to decrypt the sum of votes.</param>
    /// <returns>Partial decipher list for all vote sums.</returns>
    public Signed<PartialDecipherList> PartiallyDecipher(AuthorityEnvelopeList envelopeList)
    {
      if (envelopeList == null)
        throw new ArgumentNullException("envelopeList");

      BigInt publicKey = new BigInt(1);

      foreach (Signed<ShareResponse> signedShareResponse in envelopeList.VotingMaterial.PublicKeyParts)
      {
        ShareResponse shareResponse = signedShareResponse.Value;
        bool acceptResponse = true;
        
        acceptResponse &= signedShareResponse.Verify();
        acceptResponse &= signedShareResponse.Certificate.IsIdentic(this.authorities[shareResponse.AuthorityIndex]);

        if (!acceptResponse)
          return null;

        publicKey = (publicKey * shareResponse.PublicKeyPart).Mod(this.parameters.P);
      }

      Vote[] voteSums = new Vote[this.parameters.OptionCount];

      foreach (Signed<Envelope> signedEnvelope in envelopeList.Envelopes)
      {
        bool acceptVote = true;
        
        acceptVote &= signedEnvelope.Verify();

        Envelope envelope = signedEnvelope.Value;
        acceptVote &= envelope.Ballot.Verify(publicKey, this.parameters);

        if (acceptVote)
        {
          for (int optionIndex = 0; optionIndex < this.parameters.OptionCount; optionIndex ++)
          {
            voteSums[optionIndex] =
              voteSums[optionIndex] == null ?
              envelope.Ballot.Votes[optionIndex] :
              voteSums[optionIndex] + envelope.Ballot.Votes[optionIndex];
          }
        }
      }

      PartialDecipherList partialDecipherList = new PartialDecipherList(envelopeList.VotingId, this.authority.Index);

      for (int optionIndex = 0; optionIndex < this.parameters.OptionCount; optionIndex ++)
      {
        partialDecipherList.PartialDeciphers.AddRange(this.authority.PartialDeciphers(voteSums[optionIndex], optionIndex));
      }

      return new Signed<PartialDecipherList>(partialDecipherList, this.certificate);
    }
  }
}
