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
  /// Encrypted serializable object.
  /// </summary>
  public class Encrypted : Serializable
  {
    /// <summary>
    /// Encrypted data of serializable object.
    /// </summary>
    public byte[] Data { get; protected set; }

    /// <summary>
    /// Create a new encrypted data piece.
    /// </summary>
    public Encrypted()
    { }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public Encrypted(DeserializeContext context)
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
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Data = context.ReadBytes();
    }
  }

  /// <summary>
  /// Encrypted serializable object.
  /// </summary>
  public class Encrypted<TValue> : Encrypted
    where TValue : Serializable
  {
    /// <summary>
    /// Encrypts an serializable object.
    /// </summary>
    /// <param name="value">Serializable object.</param>
    /// <param name="receiverCertificate">Certificate of the receiver.</param>
    public Encrypted(TValue value, Certificate receiverCertificate)
    {
      if (value == null)
        throw new ArgumentNullException("value");
      if (receiverCertificate == null)
        throw new ArgumentNullException("receiverCertificate");

      Data = receiverCertificate.Encrypt(value.ToBinary());
    }

    public Encrypted(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Decrypt and deserializes data to an object.
    /// </summary>
    /// <param name="receiverCertificate">Certificate of the receiver.</param>
    /// <returns>Data object.</returns>
    public TValue Decrypt(Certificate receiverCertificate)
    {
      if (receiverCertificate == null)
        throw new ArgumentNullException("receiverCertificate");
      if (!receiverCertificate.HasPrivateKey)
        throw new ArgumentException("Private key missing.");

      return Serializable.FromBinary<TValue>(receiverCertificate.Decrypt(Data));
    }
  }
}
