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
  public class Ballot
  {
    public List<Vote> Votes { get; private set; }
    public List<Proof> SumProves { get; private set; }

    public Ballot(IEnumerable<int> vota, Parameters parameters, BigInt publicKey)
    {
      if (vota.Count() != parameters.OptionCount)
        throw new ArgumentException("Bad vota.");
      if (!vota.All(votum => votum == 0 || votum == 1))
        throw new ArgumentException("Bad vota.");
      if (vota.Sum() != parameters.MaxVota)
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
      }

      SumProves = new List<Proof>();

      for (int proofIndex = 0; proofIndex < parameters.ProofCount; proofIndex++)
      {
        SumProves.Add(new Proof(nonceSum * 12, voteSum, publicKey, parameters));
      }
    }

    public bool Verify(BigInt publicKey, Parameters parameters)
    {
      bool verifies = true;
      Vote voteSum = null;

      foreach (Vote vote in Votes)
      {
        verifies &= vote.Verify(publicKey, parameters);
        voteSum = voteSum == null ? vote : voteSum + vote;
      }

      verifies &= SumProves.Count == parameters.ProofCount;
      verifies &= SumProves.All(sumProof => sumProof.Verify(voteSum, publicKey, parameters));

      return verifies;
    }
  }
}
