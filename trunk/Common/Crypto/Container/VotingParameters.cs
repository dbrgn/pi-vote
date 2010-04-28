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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  public class QuestionParameters : QuestionBaseParameters
  {
    /// <summary>
    /// List of possible options for the voters.
    /// </summary>
    private List<Option> options;

    /// <summary>
    /// Question of this voting.
    /// </summary>
    public MultiLanguageString Question { get; private set; }

    public override int OptionCount
    {
      get { return this.options.Count; }
    }

    /// <summary>
    /// List of possible options for the voters.
    /// </summary>
    public IEnumerable<Option> Options
    {
      get { return this.options; }
    }

    public QuestionParameters(MultiLanguageString question, int maxVota)
      : base(maxVota)
    {
      if (question == null)
        throw new ArgumentNullException("question");

      Question = question;
      this.options = new List<Option>();
    }

    /// <summary>
    /// Add an option to this voting.
    /// </summary>
    /// <remarks>
    /// Only allowed before initialization.
    /// </remarks>
    /// <param name="option">An selectable option.</param>
    public void AddOption(Option option)
    {
      if (option == null)
        throw new ArgumentNullException("option");

      this.options.Add(option);
    }
    
    public QuestionParameters(DeserializeContext context)
      : base(context)
    { 
    }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Question);
      context.WriteList(Options);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Question = context.ReadMultiLanguageString();
      this.options = context.ReadObjectList<Option>();
    }
  }

  /// <summary>
  /// Contains all parameters of a voting.
  /// </summary>
  public class VotingParameters
    : BaseParameters<QuestionParameters, VotingBaseParameters>
  {
    /// <summary>
    /// Id of this voting.
    /// </summary>
    public Guid VotingId { get; private set; }

    /// <summary>
    /// Title of this voting.
    /// </summary>
    public MultiLanguageString Title { get; private set; }

    /// <summary>
    /// Description of this voting.
    /// </summary>
    public MultiLanguageString Description { get; private set; }

    /// <summary>
    /// Date at which voting begins.
    /// </summary>
    public DateTime VotingBeginDate { get; private set; }

    /// <summary>
    /// Date a which voting ends.
    /// </summary>
    public DateTime VotingEndDate { get; private set; }
    
    /// <summary>
    /// Create a new voting.
    /// </summary>
    public VotingParameters(
      CryptoParameters crypto,
      QuestionParameters quest,
      VotingBaseParameters voting,
      MultiLanguageString title, 
      MultiLanguageString description, 
      DateTime votingBeginDate, 
      DateTime votingEndDate)
      : base(crypto, quest, voting)
    {
      if (title == null)
        throw new ArgumentNullException("title");
      if (description == null)
        throw new ArgumentNullException("description");

      VotingId = Guid.NewGuid();
      Title = title;
      Description = description;
      VotingBeginDate = votingBeginDate;
      VotingEndDate = votingEndDate;
    }

    private QuestionParameters GenerateQuestionParameters(MultiLanguageString question, int maxVota)
    {
      return new QuestionParameters(question, maxVota);
    }

    /// <summary>
    /// Initialize this voting.
    /// </summary>
    /// <remarks>
    /// Only allowed once.
    /// </remarks>
    /// <param name="votesPerVoter">How many votes does each voter have?</param>
    ////public void Initialize(int votesPerVoter)
    ////{
    ////  if (Crypto != null)
    ////    throw new InvalidOperationException("Already initialized.");

    ////  BigInt prime = null;
    ////  BigInt safePrime = null;

    ////  DateTime start = DateTime.Now;
    ////  Prime.FindPrimeAndSafePrimeThreaded(PrimeBits, out prime, out safePrime);
    ////  System.Diagnostics.Debug.WriteLine("Found safe prime after " + DateTime.Now.Subtract(start).ToString());
    ////  //Prime.FindPrimeAndSafePrime(PrimeBits, out prime, out safePrime);

    ////  InitilizeCrypto(
    ////    new CryptoParameters(prime, safePrime),
    ////    new QuestionParameters(Options.Count(), votesPerVoter),
    ////    new VotingBaseParameters(StandardThereshold, StandardAuthorityCount, StandardProofCount));
    ////}

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public VotingParameters(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingId);
      context.Write(Title);
      context.Write(Description);
      context.Write(VotingBeginDate);
      context.Write(VotingEndDate);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadGuid();
      Title = context.ReadMultiLanguageString();
      Description = context.ReadMultiLanguageString();
      VotingBeginDate = context.ReadDateTime();
      VotingEndDate = context.ReadDateTime();
    }
  }
}
