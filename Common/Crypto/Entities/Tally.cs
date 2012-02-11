/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
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
    private Vote[][] voteSums;

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
    /// Next envelope index to be added.
    /// </summary>
    private int nextEnvelopeIndex;

    /// <summary>
    /// Queue used to bring envelopes in order.
    /// Key is the envelope index.
    /// Value is a tuple of signed envelope and accept vote boolean.
    /// </summary>
    private Dictionary<int, Tuple<Signed<Envelope>, bool>> envelopeSequencerList;

    /// <summary>
    /// Random number generator.
    /// </summary>
    private RandomNumberGenerator rng;

    /// <summary>
    /// Number of proofs to check.
    /// </summary>
    private int proofCheckCount;

    /// <summary>
    /// Total number of envelopes.
    /// </summary>
    public int EnvelopeCount { get; private set; }

    /// <summary>
    /// Number of valid (accepted) envelopes;
    /// </summary>
    public int ValidEnvelopeCount { get; private set; }

    /// <summary>
    /// Hash over all envelopes tallied.
    /// </summary>
    public byte[] EnvelopeHash { get; private set; }

    /// <summary>
    /// Creates a new summation of votes.
    /// </summary>
    /// <param name="parameters">Voting parameters.</param>
    /// <param name="certificateStorage">Certificate storage to verify against.</param>
    /// <param name="publicKey">Public key with which the votes where encrypted.</param>
    public Tally(
      VotingParameters parameters,
      CertificateStorage certificateStorage, 
      BigInt publicKey,
      int checkProofCount)
    {
      this.rng = RandomNumberGenerator.Create();
      this.parameters = parameters;
      this.proofCheckCount = Math.Min(parameters.ProofCount, checkProofCount);
      this.certificateStorage = certificateStorage;
      this.publicKey = publicKey;

      this.voteSums = new Vote[this.parameters.Questions.Count()][];
      for (int questionIndex = 0; questionIndex < this.parameters.Questions.Count(); questionIndex++)
      {
        Question question = this.parameters.Questions.ElementAt(questionIndex);
        this.voteSums[questionIndex] = new Vote[question.Options.Count()];
      }
      
      this.result = new VotingResult(this.parameters.VotingId, this.parameters);
      this.partialDeciphers = new List<PartialDecipher>();
      this.countedVoters = new List<Guid>();
      this.nextEnvelopeIndex = 0;
      this.envelopeSequencerList = new Dictionary<int, Tuple<Signed<Envelope>, bool>>();

      EnvelopeHash = new byte[] { };
      EnvelopeCount = 0;
      ValidEnvelopeCount = 0;

      CryptoLog.Begin(CryptoLogLevel.Summary, "Begin tallying");
      CryptoLog.Add(CryptoLogLevel.Summary, "Voting id", parameters.VotingId);
      CryptoLog.Add(CryptoLogLevel.Summary, "Voting title", parameters.Title.Text);
      CryptoLog.Add(CryptoLogLevel.Detailed, "ProofCount", parameters.ProofCount);
      CryptoLog.Add(CryptoLogLevel.Detailed, "Thereshold", parameters.Thereshold);
      CryptoLog.Add(CryptoLogLevel.Numeric, "P", parameters.P);
      CryptoLog.Add(CryptoLogLevel.Numeric, "G", parameters.G);
      CryptoLog.Add(CryptoLogLevel.Numeric, "F", parameters.F);
      CryptoLog.Add(CryptoLogLevel.Numeric, "Q", parameters.Q);
      CryptoLog.EndWrite();
    }

    /// <summary>
    /// Adds a vote to the vote sum.
    /// </summary>
    /// <remarks>
    /// Verfies the correctness of the vote first.
    /// </remarks>
    /// <param name="envelopeIndex">Index of the envelope.</param>
    /// <param name="signedEnvelope">Signed envelope from voter.</param>
    /// <param name="progress">Report the progress of the tallying.</param>
    public void Add(int envelopeIndex, Signed<Envelope> signedEnvelope, Progress progress)
    {
      if (signedEnvelope == null)
        throw new ArgumentNullException("signedEnvelope");
      if (voteSums == null)
        throw new InvalidOperationException("Must call TallyBegin first.");

      CryptoLog.Begin(CryptoLogLevel.Detailed, "Tallying envelope");
      CryptoLog.Add(CryptoLogLevel.Detailed, "Envelope index", envelopeIndex);
      CryptoLog.Add(CryptoLogLevel.Detailed, "Certificate id", signedEnvelope.Certificate.Id);
      CryptoLog.Add(CryptoLogLevel.Numeric, "Certificate fingerprint", signedEnvelope.Certificate.Fingerprint);
      CryptoLog.Add(CryptoLogLevel.Detailed, "Certificate type", signedEnvelope.Certificate.TypeText);

      if (signedEnvelope.Certificate is VoterCertificate)
      {
        CryptoLog.Add(CryptoLogLevel.Detailed, "Certificate group id", ((VoterCertificate)signedEnvelope.Certificate).GroupId);
      }

      bool acceptVote = true;
      Envelope envelope = signedEnvelope.Value;

      //Certificate is of voter and valid for that canton.
      acceptVote &= signedEnvelope.Certificate is VoterCertificate &&
                    ((VoterCertificate)signedEnvelope.Certificate).GroupId == this.parameters.GroupId;

      //Signature must be valid.
      acceptVote &= signedEnvelope.Verify(this.certificateStorage, envelope.Date);

      CryptoLog.Add(CryptoLogLevel.Detailed, "Certificate status", signedEnvelope.Certificate.Validate(this.certificateStorage, envelope.Date).Text());
      CryptoLog.Add(CryptoLogLevel.Detailed, "Envelope signature", signedEnvelope.Verify(this.certificateStorage, envelope.Date));

      //Voter's vote must not have been counted.
      acceptVote &= !this.countedVoters.Contains(signedEnvelope.Certificate.Id);

      CryptoLog.Add(CryptoLogLevel.Detailed, "Already voted", this.countedVoters.Contains(signedEnvelope.Certificate.Id));

      //Date must be in voting period.
      acceptVote &= envelope.Date.Date >= this.parameters.VotingBeginDate;
      acceptVote &= envelope.Date.Date <= this.parameters.VotingEndDate;

      CryptoLog.Add(CryptoLogLevel.Detailed, "Envelope date", envelope.Date);

      //Ballot must verify (prooves).
      for (int questionIndex = 0; questionIndex < this.parameters.Questions.Count(); questionIndex++)
      {
        Question question = this.parameters.Questions.ElementAt(questionIndex);
        Ballot ballot = envelope.Ballots.ElementAt(questionIndex);

        progress.Down(1d / (double)this.parameters.Questions.Count());
        acceptVote &= ballot.Verify(this.publicKey, this.parameters, question, this.rng, this.proofCheckCount, progress);

        progress.Up();
      }

      CryptoLog.Add(CryptoLogLevel.Detailed, "Envelope accepted", acceptVote);
      CryptoLog.EndWrite();

      lock (this.envelopeSequencerList)
      {
        this.envelopeSequencerList.Add(envelopeIndex, new Tuple<Signed<Envelope>,bool>(signedEnvelope, acceptVote));
      }

      AddInSequence();
    }

    private void AddInSequence()
    {
      bool nextOne = true;

      while (nextOne)
      {
        Tuple<Signed<Envelope>, bool> envelopeEntry = null;

        lock (this.envelopeSequencerList)
        {
          if (this.envelopeSequencerList.ContainsKey(this.nextEnvelopeIndex))
          {
            envelopeEntry = this.envelopeSequencerList[this.nextEnvelopeIndex];
            this.envelopeSequencerList.Remove(this.nextEnvelopeIndex);
          }
        }

        nextOne = envelopeEntry != null;

        if (envelopeEntry != null)
        {
          Signed<Envelope> signedEnvelope = envelopeEntry.First;
          Envelope envelope = signedEnvelope.Value;
          bool acceptVote = envelopeEntry.Second;

          CryptoLog.Begin(CryptoLogLevel.Detailed, "Adding envelope");
          CryptoLog.Add(CryptoLogLevel.Detailed, "Certificate id", signedEnvelope.Certificate.Id);

          SHA256Managed sha256 = new SHA256Managed();
          EnvelopeHash = sha256.ComputeHash(EnvelopeHash.Concat(signedEnvelope.ToBinary()));
          EnvelopeCount++;

          CryptoLog.Add(CryptoLogLevel.Numeric, "Envelope hash", EnvelopeHash);
          CryptoLog.Add(CryptoLogLevel.Detailed, "Envelope accepted", acceptVote);

          if (acceptVote)
          {
            ValidEnvelopeCount++;

            for (int questionIndex = 0; questionIndex < this.parameters.Questions.Count(); questionIndex++)
            {
              Question question = this.parameters.Questions.ElementAt(questionIndex);
              Ballot ballot = envelope.Ballots.ElementAt(questionIndex);

              for (int optionIndex = 0; optionIndex < question.Options.Count(); optionIndex++)
              {
                CryptoLog.Begin(CryptoLogLevel.Numeric, "Adding vote");
                CryptoLog.Add(CryptoLogLevel.Numeric, "Question", question.Text.Text);
                CryptoLog.Add(CryptoLogLevel.Numeric, "Option", question.Options.ElementAt(optionIndex).Text.Text);

                this.voteSums[questionIndex][optionIndex] =
                  this.voteSums[questionIndex][optionIndex] == null ?
                  ballot.Votes[optionIndex] :
                  this.voteSums[questionIndex][optionIndex] + ballot.Votes[optionIndex];

                CryptoLog.Add(CryptoLogLevel.Numeric, "Vote ciphertext", ballot.Votes[optionIndex].Ciphertext);
                CryptoLog.Add(CryptoLogLevel.Numeric, "Vote halfkey", ballot.Votes[optionIndex].HalfKey);
                CryptoLog.Add(CryptoLogLevel.Numeric, "Vote sum ciphertext", this.voteSums[questionIndex][optionIndex].Ciphertext);
                CryptoLog.Add(CryptoLogLevel.Numeric, "Vote sum halfkey", this.voteSums[questionIndex][optionIndex].HalfKey);
                CryptoLog.End(CryptoLogLevel.Numeric);
              }
            }

            this.countedVoters.Add(signedEnvelope.Certificate.Id);
          }

          this.result.Voters.Add(new EnvelopeResult(envelope.VoterId, acceptVote));

          this.nextEnvelopeIndex++;

          CryptoLog.EndWrite();
        }
      }
    }

    /// <summary>
    /// Sums of votes for all options.
    /// </summary>
    public Vote[][] VoteSums
    {
      get { return this.voteSums; }
    }

    /// <summary>
    /// Add a partial decipher.
    /// </summary>
    /// <param name="signedPartialDecipherList">List of partial deciphers.</param>
    public void AddPartialDecipher(Signed<PartialDecipherList> signedPartialDecipherList)
    {
      PartialDecipherList partialDeciphersList = signedPartialDecipherList.Value;

      CryptoLog.Begin(CryptoLogLevel.Detailed, "Adding partial decipher");
      CryptoLog.Add(CryptoLogLevel.Detailed, "Partial dipher date", partialDeciphersList.Date.Date);
      CryptoLog.Add(CryptoLogLevel.Detailed, "Certificate id", signedPartialDecipherList.Certificate.Id);
      CryptoLog.Add(CryptoLogLevel.Detailed, "Certificate type", signedPartialDecipherList.Certificate.TypeText);
      CryptoLog.Add(CryptoLogLevel.Numeric, "Certificate fingerprint", signedPartialDecipherList.Certificate.Fingerprint);
      CryptoLog.Add(CryptoLogLevel.Detailed, "Certificate full name", signedPartialDecipherList.Certificate.FullName);

      if (!(signedPartialDecipherList.Verify(this.certificateStorage, this.parameters.VotingBeginDate) &&
        partialDeciphersList.Date.Date >= this.parameters.VotingEndDate &&
        partialDeciphersList.Date.Date <= DateTime.Now.Date) &&
        signedPartialDecipherList.Certificate is AuthorityCertificate)
        throw new PiSecurityException(ExceptionCode.PartialDecipherBadSignature, "Partial decipher has bad signature.");

      CryptoLog.Add(CryptoLogLevel.Detailed, "Certificate status", signedPartialDecipherList.Certificate.Validate(this.certificateStorage, this.parameters.VotingBeginDate));
      CryptoLog.Add(CryptoLogLevel.Detailed, "Partial dipher valid", signedPartialDecipherList.Verify(this.certificateStorage, this.parameters.VotingBeginDate));
      CryptoLog.Add(CryptoLogLevel.Numeric, "Envelope count", partialDeciphersList.EnvelopeCount);
      CryptoLog.Add(CryptoLogLevel.Numeric, "Envelope hash", partialDeciphersList.EnvelopeHash);

      if (partialDeciphersList.EnvelopeCount != EnvelopeCount)
      {
        CryptoLog.EndWrite();
        throw new PiSecurityException(ExceptionCode.PartialDecipherBadEnvelopeCount, "The number of envelopes does not match the partial decipher.");
      }

      if (!partialDeciphersList.EnvelopeHash.Equal(EnvelopeHash))
      {
        CryptoLog.EndWrite();
        throw new PiSecurityException(ExceptionCode.PartialDecipherBadEnvelopeHash, "The hash over all envelopes does not match the partail decipher.");
      }

      partialDeciphers.AddRange(partialDeciphersList.PartialDeciphers);

      CryptoLog.EndWrite();
    }

    /// <summary>
    /// Calculates the result of the voting.
    /// </summary>
    public VotingResult Result
    {
      get
      {
        List<int> results = new List<int>();

        CryptoLog.Begin(CryptoLogLevel.Summary, "Calculating voting result");

        for (int questionIndex = 0; questionIndex < parameters.Questions.Count(); questionIndex++)
        {
          Question question = this.parameters.Questions.ElementAt(questionIndex);
          QuestionResult questionResult = new QuestionResult(question);

          CryptoLog.Begin(CryptoLogLevel.Summary, "Calculating question result");
          CryptoLog.Add(CryptoLogLevel.Summary, "Question", question.Text.Text);

          for (int optionIndex = 0; optionIndex < question.Options.Count(); optionIndex++)
          {
            CryptoLog.Begin(CryptoLogLevel.Summary, "Calculating option result");
            CryptoLog.Add(CryptoLogLevel.Summary, "Option", question.Options.ElementAt(optionIndex).Text.Text);

            List<int> optionResults = new List<int>();

            for (int groupIndex = 1; groupIndex <= this.parameters.AuthorityCount; groupIndex++)
            {
              CryptoLog.Begin(CryptoLogLevel.Numeric, string.Format("Authority group {0}", groupIndex));

              IEnumerable<BigInt> partialDeciphersByOptionAndGroup = this.partialDeciphers
                .Where(partialDecipher => partialDecipher.GroupIndex == groupIndex && 
                                          partialDecipher.QuestionIndex == questionIndex &&
                                          partialDecipher.OptionIndex == optionIndex)
                .Select(partialDecipher => partialDecipher.Value);
              if (partialDeciphersByOptionAndGroup.Count() == this.parameters.Thereshold + 1)
              {
                int authorityGroupResult = this.voteSums[questionIndex][optionIndex].Decrypt(partialDeciphersByOptionAndGroup, parameters);
                optionResults.Add(authorityGroupResult);
              }

              CryptoLog.End(CryptoLogLevel.Numeric);
            }

            Option option = question.Options.ElementAt(optionIndex);

            if (optionResults.Count > 0)
            {
              int firstOptionResult = optionResults[0];

              if (optionResults.All(optionResult => optionResult == firstOptionResult))
              {
                CryptoLog.Add(CryptoLogLevel.Summary, "Result", firstOptionResult);
                questionResult.Options.Add(new OptionResult(option.Text, option.Description, firstOptionResult));
              }
              else
              {
                CryptoLog.Add(CryptoLogLevel.Summary, "Result", "Not unanimous");
                questionResult.Options.Add(new OptionResult(option.Text, option.Description, -1));
              }
            }
            else
            {
              CryptoLog.Add(CryptoLogLevel.Summary, "Result", "Not available");
              questionResult.Options.Add(new OptionResult(option.Text, option.Description, -1));
            }

            CryptoLog.End(CryptoLogLevel.Summary);
          }

          CryptoLog.End(CryptoLogLevel.Summary);

          this.result.Questions.Add(questionResult);
        }

        CryptoLog.EndWrite();

        return this.result;
      }
    }
  }
}
