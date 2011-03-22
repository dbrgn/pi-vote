/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
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
    /// Tally of voting.
    /// </summary>
    private Tally tally;

    public VoterEntity(CertificateStorage certificateStorage)
    {
      CertificateStorage = certificateStorage;
    }

    /// <summary>
    /// Cast a vote and pack it in an envelope.
    /// </summary>
    /// <param name="votingMaterial">Voting material.</param>
    /// <param name="voterCertificate">Private certificate of this voter.</param>
    /// <param name="vota">List of vota.</param>
    /// <returns>Signed envelope containing the ballot.</returns>
    public Signed<Envelope> Vote(VotingMaterial votingMaterial, Certificate voterCertificate, IEnumerable<IEnumerable<int>> vota, ProgressHandler progressHandler)
    {
      if (votingMaterial == null)
        throw new ArgumentNullException("votingMaterial");

      bool acceptMaterial = SetVotingMaterial(votingMaterial);

      if (vota == null)
        throw new ArgumentNullException("vota");
      if (vota.Count() != this.parameters.Questions.Count())
        throw new ArgumentException("Bad vota count.");

      if (acceptMaterial)
      {
        List<Tuple<IEnumerable<int>, VotingParameters, Question, BigInt>> inputs = new List<Tuple<IEnumerable<int>, VotingParameters, Question, BigInt>>();

        for (int questionIndex = 0; questionIndex < this.parameters.Questions.Count(); questionIndex++)
        {
          IEnumerable<int> questionVota = vota.ElementAt(questionIndex);
          Question question = this.parameters.Questions.ElementAt(questionIndex);

          if (questionVota == null)
            throw new ArgumentNullException("questionVota");
          if (questionVota.Count() != question.Options.Count())
            throw new ArgumentException("Bad vota count.");
          if (!questionVota.All(votum => votum.InRange(0, 1)))
            throw new ArgumentException("Votum out of range.");

          inputs.Add(new Tuple<IEnumerable<int>, VotingParameters, Question, BigInt>(questionVota, this.parameters, question, this.publicKey));
        }

        List<Ballot> ballots = Parallel
          .Work<Tuple<IEnumerable<int>, VotingParameters, Question, BigInt>, Ballot>(CreateBallot, inputs, progressHandler);

        Envelope ballotContainer = new Envelope(this.parameters.VotingId, voterCertificate.Id, ballots);

        return new Signed<Envelope>(ballotContainer, voterCertificate);
      }
      else
      {
        return null;
      }
    }

    /// <summary>
    /// Create a ballot.
    /// </summary>
    /// <remarks>
    /// Used to multi-thread ballot creation.
    /// </remarks>
    /// <param name="input">Parameters needed to create the ballot.</param>
    /// <param name="progressHandler">Handles the progress done.</param>
    /// <returns>A ballot.</returns>
    private static Ballot CreateBallot(Tuple<IEnumerable<int>, VotingParameters, Question, BigInt> input, ProgressHandler progressHandler)
    {
      return new Ballot(input.First, input.Second, input.Third, input.Fourth, new Progress(progressHandler));
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
          this.publicKey = (this.publicKey * shareResponse.PublicKeyPart).Mod(this.parameters.P);
        }
      }

      return acceptMaterial;
    }

    /// <summary>
    /// Add a vote to the sum of votes.
    /// </summary>
    /// <param name="envelopeIndex">Index of the envelope.</param>
    /// <param name="signedEnvelope">Signed envelope containing the vote.</param>
    /// <param name="progress">Reports progress of the tallying.</param>
    public void TallyAdd(int envelopeIndex, Signed<Envelope> signedEnvelope, Progress progress)
    {
      if (signedEnvelope == null)
        throw new ArgumentNullException("signedEnvelope");
      if (progress == null)
        throw new ArgumentNullException("progress");

      this.tally.Add(envelopeIndex, signedEnvelope, progress);
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
