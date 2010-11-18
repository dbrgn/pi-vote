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
using System.IO;
using Emil.GMP;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Entity controling an authority.
  /// </summary>
  public class AuthorityEntity
  {
    /// <summary>
    /// Storage of certificates.
    /// </summary>
    private CertificateStorage certificateStorage;

    /// <summary>
    /// Parameters for the voting.
    /// </summary>
    private VotingParameters parameters;

    /// <summary>
    /// Signed version of the voting parameters;
    /// </summary>
    private Signed<VotingParameters> signedParameters;

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
    /// Used to tally the voting.
    /// </summary>
    private Tally tally;

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
    public AuthorityEntity(CertificateStorage certificateStorage, AuthorityCertificate certificate)
    {
      this.certificate = certificate;
      this.certificateStorage = certificateStorage;
    }

    /// <summary>
    /// Creates a new authority entity, loading data from file.
    /// </summary>
    /// <param name="certificateStorage">Certificate storage.</param>
    /// <param name="certificate">Certificate of the authority.</param>
    /// <param name="authorityFileName">File name to load data from.</param>
    public AuthorityEntity(CertificateStorage certificateStorage, AuthorityCertificate certificate, string authorityFileName)
    {
      this.certificate = certificate;
      this.certificateStorage = certificateStorage;
      Load(authorityFileName);
    }

    /// <summary>
    /// Save the data of the authority to file.
    /// </summary>
    /// <param name="fileName">Name of file to save.</param>
    public void Save(string fileName)
    { 
      FileStream fileStream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
      SerializeContext context = new SerializeContext(fileStream);

      context.Write(this.parameters);
      context.Write(this.signedParameters);

      context.Write(this.authorities.Count);
      this.authorities.Foreach(pair => { context.Write(pair.Key); context.Write(pair.Value); });

      this.authority.Serialize(context);

      context.Close();
      fileStream.Close();
    }

    /// <summary>
    /// Load data of authority from file.
    /// </summary>
    /// <param name="fileName">Name of file to load.</param>
    private void Load(string fileName)
    {
      FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
      DeserializeContext context = new DeserializeContext(fileStream);

      this.parameters = context.ReadObject<VotingParameters>();
      this.signedParameters = context.ReadObject<Signed<VotingParameters>>();

      this.authorities = new Dictionary<int,Certificate>();
      int count = context.ReadInt32();
      count.Times(() => this.authorities.Add(context.ReadInt32(), context.ReadObject<Certificate>()));

      this.authority = new Authority(context, this.parameters);

      context.Close();
      fileStream.Close();
    }

    /// <summary>
    /// Prepares the authority for voting procedure.
    /// </summary>
    /// <param name="index">Index given to this authority.</param>
    /// <param name="parameters">Voting parameters for the procedure.</param>
    public void Prepare(int index, Signed<VotingParameters> signedParameters)
    {
      if (index < 1)
        throw new ArgumentException("Index must be at least 1.");
      if (signedParameters == null)
        throw new ArgumentNullException("signedParameters");

      this.signedParameters = signedParameters;
      this.parameters = signedParameters.Value;
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
    /// Creates a proof for bad shares.
    /// </summary>
    /// <param name="allShareParts">All share parts from all authorities.</param>
    /// <returns>Signed proof data.</returns>
    public Signed<BadShareProof> CreateBadShareProof(AllShareParts allShareParts)
    {
      Dictionary<int, TrapDoor> trapDoors = new Dictionary<int, TrapDoor>();
      foreach (Signed<SharePart> signedSharePart in allShareParts.ShareParts)
      {
        SharePart sharePart = signedSharePart.Value;

        Encrypted<Share> encryptedShare = sharePart.EncryptedShares[this.authority.Index - 1];
        trapDoors.Add(sharePart.AuthorityIndex, encryptedShare.CreateTrapDoor(this.certificate));
      }
      
      BadShareProof badShareProof = new BadShareProof(
        this.authority.Index,
        this.certificateStorage,
        this.signedParameters,
        allShareParts,
        trapDoors,
        this.authorities);

      return new Signed<BadShareProof>(badShareProof, this.certificate);
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

        acceptShares &= signedShareParrt.Verify(this.certificateStorage);
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

      ShareResponse response = new ShareResponse(allShareParts.VotingId, this.authority.Index, acceptShares, publicKeyPart, this.signedParameters);

      return new Signed<ShareResponse>(response, this.certificate);
    }

    /// <summary>
    /// Resets the vote sum.
    /// </summary>
    /// <param name="votingMaterial">Voting material.</param>
    public void TallyBegin(VotingMaterial votingMaterial)
    {
      if (votingMaterial == null)
        throw new ArgumentNullException("votingMaterial");

      BigInt publicKey = CalculatePublicKey(votingMaterial);

      this.tally = new Tally(this.parameters, this.certificateStorage, publicKey);
    }

    /// <summary>
    /// Adds a vote to the vote sum.
    /// </summary>
    /// <param name="envelopeIndex">Index of the envelope to be added.</param>
    /// <param name="signedEnvelope">Signed envelope containing the vote.</param>
    /// <param name="progress">Reports progress of the tallying.</param>
    public void TallyAdd(int envelopeIndex, Signed<Envelope> signedEnvelope, Progress progress)
    {
      if (progress == null)
        throw new ArgumentNullException("progress");
      if (signedEnvelope == null)
        throw new ArgumentNullException("signedEnvelope");
      if (this.tally == null)
        throw new InvalidOperationException("Tally not yet begun.");

      this.tally.Add(envelopeIndex, signedEnvelope, progress);
    }

    /// <summary>
    /// Calculates the public key of the authorities.
    /// </summary>
    /// <param name="votingMaterial">Voting material.</param>
    /// <returns>Public key.</returns>
    private BigInt CalculatePublicKey(VotingMaterial votingMaterial)
    {
      if (votingMaterial == null)
        throw new ArgumentNullException("votingMaterial");

      BigInt publicKey = new BigInt(1);

      foreach (Signed<ShareResponse> signedShareResponse in votingMaterial.PublicKeyParts)
      {
        ShareResponse shareResponse = signedShareResponse.Value;

        if (!signedShareResponse.Verify(this.certificateStorage))
          throw new PiSecurityException(ExceptionCode.ShareResponseBadSignature, "Share response has bad signature.");
        if (!signedShareResponse.Certificate.IsIdentic(this.authorities[shareResponse.AuthorityIndex]))
          throw new PiSecurityException(ExceptionCode.ShareResponseWrongAuthority, "Share response is from wrong authority.");
        if (!shareResponse.AcceptShares)
          throw new PiSecurityException(ExceptionCode.ShareResponseNotAccepted, "Share response does not accept.");
        if (!shareResponse.Verify(votingMaterial.Parameters))
          throw new PiSecurityException(ExceptionCode.ShareResponseParametersDontMatch, "Share response does not match voting parameters.");

        publicKey = (publicKey * shareResponse.PublicKeyPart).Mod(this.parameters.P);
      }

      return publicKey;
    }

    /// <summary>
    /// Number of valid envelopes in current tally.
    /// </summary>
    public int TallyValidEnvelopeCount
    {
      get
      {
        if (this.tally == null)
          throw new InvalidOperationException("Tally not yet begun.");

        return this.tally.ValidEnvelopeCount;
      }
    }
    
    /// <summary>
    /// Partially deciphers the sum of votes.
    /// </summary>
    /// <returns>Partial decipher list for all vote sums.</returns>
    public Signed<PartialDecipherList> PartiallyDecipher()
    {
      if (this.tally == null)
        throw new InvalidOperationException("Tally not yet begun.");

      PartialDecipherList partialDecipherList = new PartialDecipherList(this.parameters.VotingId, this.authority.Index, this.tally.EnvelopeCount, this.tally.EnvelopeHash);

#if DEBUG
      if (this.tally.ValidEnvelopeCount >= 1)
#else
      if (this.tally.ValidEnvelopeCount >= 3)
#endif
      {
        for (int questionIndex = 0; questionIndex < this.parameters.Questions.Count(); questionIndex++)
        {
          Question question = this.parameters.Questions.ElementAt(questionIndex);

          for (int optionIndex = 0; optionIndex < question.Options.Count(); optionIndex++)
          {
            partialDecipherList.PartialDeciphers.AddRange(this.authority.PartialDeciphers(this.authority.Index, this.tally.VoteSums[questionIndex][optionIndex], questionIndex, optionIndex));
          }
        }
      }

      return new Signed<PartialDecipherList>(partialDecipherList, this.certificate);
    }
  }
}
