/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
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
  [SerializeObject("Signed serializable object.")]
  public class Signed : Serializable
  {
    /// <summary>
    /// Binary data of serializable object.
    /// </summary>
    [SerializeField(0, "Binary data of serializable object.")]
    public byte[] Data { get; protected set; }

    /// <summary>
    /// Signature.
    /// </summary>
    [SerializeField(1, "Signature.")]
    public byte[] Signature { get; protected set; }

    /// <summary>
    /// Binary data of the certificate.
    /// </summary>
    [SerializeField(2, "Binary data of the certificate.")]
    public byte[] CertificateData { get; protected set; }

    /// <summary>
    /// Create a new signed data piece.
    /// </summary>
    public Signed()
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public Signed(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Data);
      context.Write(Signature);
      context.Write(CertificateData);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
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
  [SerializeObject("Signed serializable object.")]
  [GenericArgument(0, "TValue", "Type of bject to be signed.")]
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
      if (!certificate.HasPrivateKey)
        throw new ArgumentException("Private key missing.");

      Data = value.ToBinary();
      Signature = certificate.Sign(Data);
      CertificateData = certificate.OnlyPublicPart.ToBinary();
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public Signed(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Verifies the correctness of the signature.
    /// </summary>
    /// <remarks>
    /// Don't forget to check the identiy of the certificate.
    /// </remarks>
    /// <param name="certificateStorage">Certificate storage to check against.</param>
    /// <returns>Validity of signature.</returns>
    public bool Verify(ICertificateStorage certificateStorage)
    {
      return Certificate.Verify(Data, Signature, certificateStorage);
    }

    /// <summary>
    /// Verifies the correctness of the signature without verifying the certificate itself.
    /// </summary>
    /// <param name="certificateStorage">Certificate storage to check against.</param>
    /// <returns>Validity of signature.</returns>
    public bool VerifySimple()
    {
      return Certificate.VerifySimple(Data, Signature);
    }

    /// <summary>
    /// Verifies the correctnes of the signature.
    /// </summary>
    /// <remarks>
    /// Don't forget to check the identiy of the certificate.
    /// </remarks>
    /// <param name="certificateStorage">Certificate storage to check against.</param>
    /// <param name="date">Date at which the signature must be valid.</param>
    /// <returns>Validity of signature.</returns>
    public bool Verify(ICertificateStorage certificateStorage, DateTime date)
    {
      return Certificate.Verify(Data, Signature, certificateStorage, date);
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
