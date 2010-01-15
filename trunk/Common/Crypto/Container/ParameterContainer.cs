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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Contains all parameters of a voting.
  /// </summary>
  public class ParameterContainer : Serializable
  {
    private const int AuthorityCount = 5;
    private const int Thereshold = 3;
    private const int PrimeBits = 80;
    private const int ProofCount = 10;

    /// <summary>
    /// List of possible options for the voters.
    /// </summary>
    private List<Option> options;

    /// <summary>
    /// Id of this voting.
    /// </summary>
    public int VotingId { get; private set; }

    /// <summary>
    /// Name of this voting.
    /// </summary>
    public string VotingName { get; private set; }

    /// <summary>
    /// Cryptographic parameters.
    /// </summary>
    public Parameters Parameters { get; private set; }

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
    public ParameterContainer()
    {
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
      if (Parameters != null)
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
      if (Parameters != null)
        throw new InvalidOperationException("Already initialized.");

      Parameters = new Parameters(
        Prime.Find(PrimeBits),
        Prime.FindSafe(PrimeBits + 8),
        Thereshold,
        AuthorityCount,
        Options.Count(),
        votesPerVoter,
        ProofCount);
    }

    public ParameterContainer(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingId);
      context.Write(VotingName);
      context.Write(Parameters);
      context.WriteList(Options);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadInt32();
      VotingName = context.ReadString();
      Parameters = context.ReadObject<Parameters>();
      this.options = context.ReadObjectList<Option>();
    }
  }
}
