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
  public class ShareContainer : Serializable
  {
    public int AuthorityIndex { get; private set; }

    public List<EncryptedContainer<Share>> EncryptedShares { get; private set; }

    public List<VerificationValue> VerificationValues { get; private set; }

    public ShareContainer(int authorityIndex)
    {
      AuthorityIndex = authorityIndex;
      EncryptedShares = new List<EncryptedContainer<Share>>();
      VerificationValues = new List<VerificationValue>();
    }

    public ShareContainer(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(AuthorityIndex);
      context.WriteList(EncryptedShares);
      context.WriteList(VerificationValues);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      AuthorityIndex = context.ReadInt32();
      EncryptedShares = context.ReadObjectList<EncryptedContainer<Share>>();
      VerificationValues = context.ReadObjectList<VerificationValue>();
    }
  }
}
