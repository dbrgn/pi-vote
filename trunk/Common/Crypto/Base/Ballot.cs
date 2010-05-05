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
  /// Container for all votes from a voter.
  /// </summary>
  public class Ballot : Serializable
  {
    /// <summary>
    /// Votes for each option.
    /// </summary>
    public List<Vote> Votes { get; private set; }

    /// <summary>
    /// Proofes of sum of votes cast.
    /// </summary>
    public List<Proof> SumProves { get; private set; }

    /// <summary>
    /// Creates a new ballot for a voter.
    /// </summary>
    /// <param name="vota">Vota the voter wishes to cast for each option.</param>
    /// <param name="parameters">Cryptographic parameters.</param>
    /// <param name="publicKey">Public key of voting authorities.</param>
    public Ballot(IEnumerable<int> vota, BaseParameters parameters, Question questionParameters, BigInt publicKey, Progress progress)
    {
      if (progress == null)
        throw new ArgumentNullException("progress");
      if (vota == null)
        throw new ArgumentNullException("vota");
      if (parameters == null)
        throw new ArgumentNullException("parameters");
      if (publicKey == null)
        throw new ArgumentNullException("publicKey");
      if (vota.Count() != questionParameters.Options.Count())
        throw new ArgumentException("Bad vota.");
      if (!vota.All(votum => votum.InRange(0, 1)))
        throw new ArgumentException("Bad vota.");
      if (vota.Sum() != questionParameters.MaxVota)
        throw new ArgumentException("Bad vota.");

      Votes = new List<Vote>();
      BigInt nonceSum = new BigInt(0);
      Vote voteSum = null;

      foreach (int votum in vota)
      {
        BigInt nonce = parameters.Random();
        nonceSum += nonce;
        Vote vote = new Vote(votum, nonce, parameters, publicKey);
        voteSum = voteSum == null ? vote : voteSum + vote;
        Votes.Add(vote);

        progress.Add(1d / (double)(vota.Count() + 1));
      }

      SumProves = new List<Proof>();

      for (int proofIndex = 0; proofIndex < parameters.ProofCount; proofIndex++)
      {
        SumProves.Add(new Proof(nonceSum * 12, voteSum, publicKey, parameters));
      }

      progress.Add(1d / (double)(vota.Count() + 1));
    }

    /// <summary>
    /// Verifies the correctness of the ballot.
    /// </summary>
    /// <remarks>
    /// Verifies all proof for sum and range of votes.
    /// </remarks>
    /// <param name="publicKey">Public key of the authorities.</param>
    /// <param name="parameters">Cryptographic parameters.</param>
    /// <returns>Result of the verification.</returns>
    public bool Verify(BigInt publicKey, BaseParameters parameters, Question questionParameters)
    {
      if (publicKey == null)
        throw new ArgumentNullException("publicKey");
      if (parameters == null)
        throw new ArgumentNullException("parameters");

      bool verifies = true;
      Vote voteSum = null;

      foreach (Vote vote in Votes)
      {
        verifies &= vote.Verify(publicKey, parameters);
        voteSum = voteSum == null ? vote : voteSum + vote;
      }

      verifies &= SumProves.Count == parameters.ProofCount;
      verifies &= SumProves.All(sumProof => sumProof.Verify(voteSum, publicKey, parameters, questionParameters));

      return verifies;
    }

    public Ballot(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.WriteList(Votes);
      context.WriteList(SumProves);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Votes = context.ReadObjectList<Vote>();
      SumProves = context.ReadObjectList<Proof>();
    }
  }
}
