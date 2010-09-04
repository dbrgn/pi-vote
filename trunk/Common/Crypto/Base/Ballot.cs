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
    /// Proofs of sum of votes cast.
    /// </summary>
    public List<Proof> SumProves { get; private set; }

    /// <summary>
    /// Creates a new ballot for a voter.
    /// </summary>
    /// <param name="vota">Vota the voter wishes to cast for each option.</param>
    /// <param name="parameters">Cryptographic parameters.</param>
    /// <param name="publicKey">Public key of voting authorities.</param>
    /// <param name="progress">Report progress up.</param>
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

      List<Tuple<int, BigInt, BaseParameters, BigInt>> voteWorkList = new List<Tuple<int, BigInt, BaseParameters, BigInt>>();

      foreach (int votum in vota)
      {
        BigInt nonce = parameters.Random();
        nonceSum += nonce;
        voteWorkList.Add(new Tuple<int, BigInt, BaseParameters, BigInt>(votum, nonce, parameters, publicKey));
      }

      progress.Down(1d / (vota.Count() + 1) * vota.Count());
      List<Vote> voteList = Parallel.Work(CreateVote, voteWorkList, progress.Set); 
      progress.Up();

      foreach (Vote vote in voteList)
      {
        voteSum = voteSum == null ? vote : voteSum + vote;
        Votes.Add(vote);
      }

      progress.Down(1d / (double)(vota.Count() + 1));
      SumProves = new List<Proof>();
      for (int proofIndex = 0; proofIndex < parameters.ProofCount; proofIndex++)
      {
        SumProves.Add(new Proof(nonceSum * 12, voteSum, publicKey, parameters));
        progress.Add(1d / (double)parameters.ProofCount);
      }
      progress.Up();
    }

    /// <summary>
    /// Creates a new ballot for a voter.
    /// </summary>
    /// <param name="vota">Vota the voter wishes to cast for each option.</param>
    /// <param name="parameters">Cryptographic parameters.</param>
    /// <param name="publicKey">Public key of voting authorities.</param>
    /// <param name="progress">Report progress up.</param>
    /// <param name="fakeType">What fake to create?</param>
    public Ballot(IEnumerable<int> vota, BaseParameters parameters, Question questionParameters, BigInt publicKey, FakeType fakeType)
    {
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

      List<Tuple<int, BigInt, BaseParameters, BigInt>> voteWorkList = new List<Tuple<int, BigInt, BaseParameters, BigInt>>();

      foreach (int votum in vota)
      {
        BigInt nonce = parameters.Random();
        nonceSum += nonce;
        voteWorkList.Add(new Tuple<int, BigInt, BaseParameters, BigInt>(votum, nonce, parameters, publicKey));
      }

      List<Vote> voteList = Parallel.Work(CreateVote, voteWorkList, null);

      foreach (Vote vote in voteList)
      {
        voteSum = voteSum == null ? vote : voteSum + vote;
        Votes.Add(vote);
      }

      SumProves = new List<Proof>();
      for (int proofIndex = 0; proofIndex < parameters.ProofCount; proofIndex++)
      {
        SumProves.Add(new Proof(nonceSum * 12, voteSum, publicKey, parameters, fakeType));
      }
    }

    private Vote CreateVote(Tuple<int, BigInt, BaseParameters, BigInt> input, ProgressHandler progressHandler)
    {
      return new Vote(input.First, input.Second, input.Third, input.Fourth, new Progress(progressHandler));
    }

    /// <summary>
    /// Verifies the correctness of the ballot.
    /// </summary>
    /// <remarks>
    /// Verifies all proof for sum and range of votes.
    /// </remarks>
    /// <param name="publicKey">Public key of the authorities.</param>
    /// <param name="parameters">Cryptographic parameters.</param>
    /// <param name="progress">Reports on the progress.</param>
    /// <returns>Result of the verification.</returns>
    public bool Verify(BigInt publicKey, BaseParameters parameters, Question questionParameters, Progress progress)
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
        progress.Add(1d / (double)Votes.Count);
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
