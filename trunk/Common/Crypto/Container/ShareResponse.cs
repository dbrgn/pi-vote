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
  public class ShareResponse : Serializable
  {
    public int VotingId { get; private set; }

    public int AuthorityIndex { get; private set; }

    public bool AcceptShares { get; private set; }

    public BigInt PublicKeyPart { get; private set; }

    public ShareResponse(int votingId, int authorityIndex, bool acceptShares, BigInt publicKeyPart)
    {
      VotingId = votingId;
      AuthorityIndex = authorityIndex;
      AcceptShares = acceptShares;
      PublicKeyPart = publicKeyPart;
    }

    public ShareResponse(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(VotingId);
      context.Write(AuthorityIndex);
      context.Write(AcceptShares);
      context.Write(PublicKeyPart);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      VotingId = context.ReadInt32();
      AuthorityIndex = context.ReadInt32();
      AcceptShares = context.ReadBoolean();
      PublicKeyPart = context.ReadBigInt();
    }
  }
}
