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
  public class PartialDeciphersContainer : Serializable
  {
    public int VotingId { get; set; }

    public int AuthorityIndex { get; set; }

    public List<PartialDecipher> PartialDeciphers { get; set; }

    public PartialDeciphersContainer(int votingId, int authorityIndex)
    {
      VotingId = votingId;
      AuthorityIndex = authorityIndex;
      PartialDeciphers = new List<PartialDecipher>();
    }

    public PartialDeciphersContainer(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingId);
      context.Write(AuthorityIndex);
      context.WriteList(PartialDeciphers);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadInt32();
      AuthorityIndex = context.ReadInt32();
      PartialDeciphers = context.ReadObjectList<PartialDecipher>();
    }
  }
}
