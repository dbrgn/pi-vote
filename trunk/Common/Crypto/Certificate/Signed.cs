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
  /// Signed serializable object.
  /// </summary>
  public class Signed : Serializable
  {
    /// <summary>
    /// Binary data of serializable object.
    /// </summary>
    public byte[] Data { get; protected set; }

    /// <summary>
    /// Signature.
    /// </summary>
    public byte[] Signature { get; protected set; }

    /// <summary>
    /// Binary data of the certificate.
    /// </summary>
    public byte[] CertificateData { get; protected set; }

    public Signed()
    { }

    public Signed(DeserializeContext context)
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

  /// <summary>
  /// Binary data of serializable object.
  /// </summary>
  public class Signed<TValue> : Signed
    where TValue : Serializable
  {
    /// <summary>
    /// Creates a new signed serialized object.
    /// </summary>
    /// <param name="value">Serializable object.</param>
    /// <param name="certificate">Certificate of the signer.</param>
    public Signed(TValue value, Certificate certificate)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (certificate == null)
        throw new ArgumentNullException("certificate");

      Data = value.ToBinary();
      Signature = certificate.Sign(Data);
      CertificateData = certificate.ToBinary();
    }

    public Signed(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Verifies the correctnes of the signature.
    /// </summary>
    /// <remarks>
    /// Don't forget to check the identiy of the certificate.
    /// </remarks>
    /// <returns></returns>
    public bool Verify()
    {
      return Certificate.Verify(Data, Signature);
    }

    /// <summary>
    /// Certificate of the signer.
    /// </summary>
    public Certificate Certificate
    {
      get { return Serializable.FromBinary<Certificate>(CertificateData); }
    }

    /// <summary>
    /// The serialize object.
    /// </summary>
    public TValue Value
    {
      get { return Serializable.FromBinary<TValue>(Data); }
    }
  }
}
