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
using System.IO;
using System.Security.Cryptography;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// A trapdoor enabled encryption of data encrypted for some
  /// certificate without the private key. It also allows
  /// verification of said encryption.
  /// </summary>
  public class TrapDoor : Serializable
  {
    /// <summary>
    /// Id of the issuer of this trapdoor.
    /// </summary>
    public Guid IssuerId { get; private set; }

    /// <summary>
    /// Symmetric key allowing decryption.
    /// </summary>
    public byte[] SymmetricKey { get; private set; }

    /// <summary>
    /// Creates a new trapdoor.
    /// </summary>
    /// <param name="issuerId">Id of the issuer of this trapdoor.</param>
    /// <param name="symmetricKey">Symmetric key allowing decryption.</param>
    public TrapDoor(Guid issuerId, byte[] symmetricKey)
    {
      IssuerId = issuerId;
      SymmetricKey = symmetricKey;
    }

    /// <summary>
    /// Creates a new signed serialized object.
    /// </summary>
    /// <param name="value">Serializable object.</param>
    /// <param name="certificate">Certificate of the signer.</param>
    public TrapDoor(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(IssuerId);
      context.Write(SymmetricKey);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      IssuerId = context.ReadGuid();
      SymmetricKey = context.ReadBytes();
    }
  }
}
