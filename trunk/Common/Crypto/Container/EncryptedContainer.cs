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
  public class EncryptedContainer : Serializable
  {
    public byte[] Data { get; protected set; }

    public EncryptedContainer()
    { }

    public EncryptedContainer(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Data);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Data = context.ReadBytes();
    }
  }

  public class EncryptedContainer<TValue> : EncryptedContainer
    where TValue : Serializable
  {
    public EncryptedContainer(TValue value, Certificate receiverCertificate)
    {
      Data = receiverCertificate.Encrypt(value.ToBinary());
    }

    public EncryptedContainer(DeserializeContext context)
      : base(context)
    { }

    public TValue Decrypt(Certificate receiverCertificate)
    {
      return Serializable.FromBinary<TValue>(receiverCertificate.Decrypt(Data));
    }
  }
}
