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
  /// <summary>
  /// Contains all parameters of a voting.
  /// </summary>
  public class VotingParameters : Parameters
  {
    private const int StandardAuthorityCount = 5;
    private const int StandardThereshold = 3;
    private const int StandardProofCount = 40;
    private const int PrimeBits = 4096;

    /// <summary>
    /// List of possible options for the voters.
    /// </summary>
    private List<Option> options;

    /// <summary>
    /// Id of this voting.
    /// </summary>
    public Guid VotingId { get; private set; }

    /// <summary>
    /// Name of this voting.
    /// </summary>
    public string VotingName { get; private set; }

    /// <summary>
    /// Date at which voting begins.
    /// </summary>
    public DateTime VotingBeginDate { get; private set; }

    /// <summary>
    /// Date a which voting ends.
    /// </summary>
    public DateTime VotingEndDate { get; private set; }
    
    /// <summary>
    /// List of possible options for the voters.
    /// </summary>
    public IEnumerable<Option> Options
    {
      get { return this.options; }
    }

    /// <summary>
    /// Create a new voting.
    /// </summary>
    public VotingParameters(string votingName, DateTime votingBeginDate, DateTime votingEndDate)
    {
      if (votingName == null)
        throw new ArgumentNullException("votingName");

      VotingId = Guid.NewGuid();
      VotingName = votingName;
      VotingBeginDate = votingBeginDate;
      VotingEndDate = votingEndDate;
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
      if (P != null)
        throw new InvalidOperationException("Already initialized.");

      this.options.Add(option);
    }

    /// <summary>
    /// Initialize this voting.
    /// </summary>
    /// <remarks>
    /// Only allowed once.
    /// </remarks>
    /// <param name="votesPerVoter">How many votes does each voter have?</param>
    public void Initialize(int votesPerVoter)
    {
      if (P != null)
        throw new InvalidOperationException("Already initialized.");

      BigInt prime = null;
      BigInt safePrime = null;

      DateTime start = DateTime.Now;
      Prime.FindPrimeAndSafePrimeThreaded(PrimeBits, out prime, out safePrime);
      Console.WriteLine(DateTime.Now.Subtract(start).ToString());
      //Prime.FindPrimeAndSafePrime(PrimeBits, out prime, out safePrime);

      InitilizeCrypto(
        prime,
        safePrime,
        StandardThereshold,
        StandardAuthorityCount,
        Options.Count(),
        votesPerVoter,
        StandardProofCount);
    }

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
      context.Write(VotingName);
      context.WriteList(Options);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadGuid();
      VotingName = context.ReadString();
      this.options = context.ReadObjectList<Option>();
    }
  }
}
