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
using System.Security.Cryptography;
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
    /// Total number of envelopes.
    /// </summary>
    public int EnvelopeCount { get; private set; }

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
      BigInt publicKey)
    {
      this.parameters = parameters;
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

      bool acceptVote = true;
      Envelope envelope = signedEnvelope.Value;

      //Certificate is of voter and valid for that canton.
      acceptVote &= signedEnvelope.Certificate is VoterCertificate &&
                    (this.parameters.Canton == Canton.None ||
                    ((VoterCertificate)signedEnvelope.Certificate).Canton == this.parameters.Canton);

      //Signature must be valid.
      acceptVote &= signedEnvelope.Verify(this.certificateStorage, envelope.Date);

      //Voter's vote must not have been counted.
      acceptVote &= !this.countedVoters.Contains(signedEnvelope.Certificate.Id);

      //Date must be in voting period.
      acceptVote &= envelope.Date.Date >= this.parameters.VotingBeginDate;
      acceptVote &= envelope.Date.Date <= this.parameters.VotingEndDate;


      //Ballot must verify (prooves).
      for (int questionIndex = 0; questionIndex < this.parameters.Questions.Count(); questionIndex++)
      {
        Question question = this.parameters.Questions.ElementAt(questionIndex);
        Ballot ballot = envelope.Ballots.ElementAt(questionIndex);

        progress.Down(1d / (double)this.parameters.Questions.Count());
        acceptVote &= ballot.Verify(this.publicKey, this.parameters, question, progress);
        progress.Up();
      }

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

          SHA256Managed sha256 = new SHA256Managed();
          EnvelopeHash = sha256.ComputeHash(EnvelopeHash.Concat(signedEnvelope.ToBinary()));
          EnvelopeCount++;

          if (acceptVote)
          {
            for (int questionIndex = 0; questionIndex < this.parameters.Questions.Count(); questionIndex++)
            {
              Question question = this.parameters.Questions.ElementAt(questionIndex);
              Ballot ballot = envelope.Ballots.ElementAt(questionIndex);

              for (int optionIndex = 0; optionIndex < question.Options.Count(); optionIndex++)
              {
                this.voteSums[questionIndex][optionIndex] =
                  this.voteSums[questionIndex][optionIndex] == null ?
                  ballot.Votes[optionIndex] :
                  this.voteSums[questionIndex][optionIndex] + ballot.Votes[optionIndex];
              }
            }

            this.countedVoters.Add(signedEnvelope.Certificate.Id);
          }

          this.result.Voters.Add(new EnvelopeResult(envelope.VoterId, acceptVote));

          this.nextEnvelopeIndex++;
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
      if (!signedPartialDecipherList.Verify(this.certificateStorage))
        throw new PiSecurityException(ExceptionCode.PartialDecipherBadSignature, "Partial decipher has bad signature.");

      PartialDecipherList partialDeciphersContainer = signedPartialDecipherList.Value;

      if (partialDeciphersContainer.EnvelopeCount != EnvelopeCount)
        throw new PiSecurityException(ExceptionCode.PartialDecipherBadEnvelopeCount, "The number of envelopes does not match the partial decipher.");
      if (!partialDeciphersContainer.EnvelopeHash.Equal(EnvelopeHash))
        throw new PiSecurityException(ExceptionCode.PartialDecipherBadEnvelopeHash, "The hash over all envelopes does not match the partail decipher.");

      partialDeciphers.AddRange(partialDeciphersContainer.PartialDeciphers);
    }

    /// <summary>
    /// Calculates the result of the voting.
    /// </summary>
    public VotingResult Result
    {
      get
      {
        List<int> results = new List<int>();

        for (int questionIndex = 0; questionIndex < parameters.Questions.Count(); questionIndex++)
        {
          Question question = this.parameters.Questions.ElementAt(questionIndex);
          QuestionResult questionResult = new QuestionResult(question);

          for (int optionIndex = 0; optionIndex < question.Options.Count(); optionIndex++)
          {
            List<int> optionResults = new List<int>();

            for (int groupIndex = 1; groupIndex <= this.parameters.AuthorityCount; groupIndex++)
            {
              IEnumerable<BigInt> partialDeciphersByOptionAndGroup = this.partialDeciphers
                .Where(partialDecipher => partialDecipher.GroupIndex == groupIndex && 
                                          partialDecipher.QuestionIndex == questionIndex &&
                                          partialDecipher.OptionIndex == optionIndex)
                .Select(partialDecipher => partialDecipher.Value);
              if (partialDeciphersByOptionAndGroup.Count() == this.parameters.Thereshold + 1)
                optionResults.Add(this.voteSums[questionIndex][optionIndex].Decrypt(partialDeciphersByOptionAndGroup, parameters));
            }

            Option option = question.Options.ElementAt(optionIndex);

            if (optionResults.Count > 0)
            {
              int firstOptionResult = optionResults[0];

              if (optionResults.All(optionResult => optionResult == firstOptionResult))
              {
                questionResult.Options.Add(new OptionResult(option.Text, option.Description, firstOptionResult));
              }
              else
              {
                questionResult.Options.Add(new OptionResult(option.Text, option.Description, -1));
              }
            }
            else
            {
              questionResult.Options.Add(new OptionResult(option.Text, option.Description, -1));
            }
          }

          this.result.Questions.Add(questionResult);
        }

        return this.result;
      }
    }
  }
}
