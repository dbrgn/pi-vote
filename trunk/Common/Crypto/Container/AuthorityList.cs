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
  public class AuthorityList : Serializable
  {
    public int VotingId { get; private set; }

    public List<Certificate> Authorities { get; private set; }

    public AuthorityList(int votingId, IEnumerable<Certificate> authorities)
    {
      Authorities = new List<Certificate>(authorities);
    }

    public AuthorityList(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingId);
      context.WriteList(Authorities);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadInt32();
      Authorities = context.ReadObjectList<Certificate>();
    }
  }
}
