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
  /// Encrypted serializable object.
  /// </summary>
  [SerializeObject("Encrypted serializable object.")]
  public class Encrypted : Serializable
  {
    /// <summary>
    /// Id of the intended receiver.
    /// </summary>
    [SerializeField(0, "Id of the intended receiver.")]
    public Guid ReceiverId { get; protected set; }

    /// <summary>
    /// Encrypted data of serializable object.
    /// </summary>
    [SerializeField(1, "Encrypted data of serializable object.")]
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
      context.Write(ReceiverId);
      context.Write(Data);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      ReceiverId = context.ReadGuid();
      Data = context.ReadBytes();
    }
  }

  /// <summary>
  /// Encrypted serializable object.
  /// </summary>
  [SerializeObject("Encrypted serializable object.")]
  [GenericArgument(0, "TValue", "Type of object to be encrypted.")]
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

      ReceiverId = receiverCertificate.Id;
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
      if (receiverCertificate.Id != ReceiverId)
        throw new ArgumentException("Wrong receiver id.");

      return Serializable.FromBinary<TValue>(receiverCertificate.Decrypt(Data));
    }

    /// <summary>
    /// Creates a trapdoor to this encryption.
    /// </summary>
    /// <param name="receiverCertificate">Certificate of the receiver.</param>
    /// <returns>Created trapdoor.</returns>
    public TrapDoor CreateTrapDoor(Certificate receiverCertificate)
    {
      if (receiverCertificate == null)
        throw new ArgumentNullException("receiverCertificate");
      if (!receiverCertificate.HasPrivateKey)
        throw new ArgumentException("Private key missing.");
      if (receiverCertificate.Id != ReceiverId)
        throw new ArgumentException("Wrong receiver id.");

      return receiverCertificate.CreateTrapDoor(Data);
    }

    /// <summary>
    /// Decrypts the data with a trapdoor.
    /// </summary>
    /// <param name="trapDoor">Trapdoor.</param>
    /// <param name="receiverCertificate">Certificate of the receiver.</param>
    /// <returns>Decrypted data.</returns>
    public TValue DecryptWithTrapDoor(TrapDoor trapDoor, Certificate receiverCertificate)
    {
      if (receiverCertificate == null)
        throw new ArgumentNullException("receiverCertificate");
      if (receiverCertificate.Id != ReceiverId)
        throw new ArgumentException("Wrong receiver id.");
      if (trapDoor.IssuerId != ReceiverId)
        throw new ArgumentException("Wrong trapdoor issuer id.");

      return Serializable.FromBinary<TValue>(receiverCertificate.DeryptWithTrapDoor(trapDoor, Data));
    }
  }
}
