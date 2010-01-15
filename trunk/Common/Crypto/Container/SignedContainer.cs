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
  public class SignedContainer : Serializable
  {
    public byte[] Data { get; protected set; }
    public byte[] Signature { get; protected set; }
    public byte[] CertificateData { get; protected set; }

    public SignedContainer()
    { }

    public SignedContainer(DeserializeContext context)
      : base(context)
    { }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Data);
      context.Write(Signature);
      context.Write(CertificateData);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Data = context.ReadBytes();
      Signature = context.ReadBytes();
      CertificateData = context.ReadBytes();
    }
  }

  public class SignedContainer<TValue> : SignedContainer
    where TValue : Serializable
  {
    public SignedContainer(TValue value, Certificate certificate)
    {
      Data = value.ToBinary();
      Signature = certificate.Sign(Data);
      CertificateData = certificate.ToBinary();
    }

    public SignedContainer(DeserializeContext context)
      : base(context)
    { }

    public bool Verify()
    {
      return Certificate.Verify(Data, Signature);
    }

    public Certificate Certificate
    {
      get { return Serializable.FromBinary<Certificate>(CertificateData); }
    }

    public TValue Value
    {
      get { return Serializable.FromBinary<TValue>(Data); }
    }
  }
}
