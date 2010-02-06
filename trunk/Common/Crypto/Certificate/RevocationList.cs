﻿/*
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
  /// Certificate revocation list.
  /// </summary>
  public class RevocationList : Serializable
  {
    /// <summary>
    /// Id of the issuer.
    /// </summary>
    public Guid IssuerId { get; private set; }

    /// <summary>
    /// List valid from date.
    /// </summary>
    public DateTime ValidFrom { get; private set; }

    /// <summary>
    /// List valid until date.
    /// </summary>
    public DateTime ValidUntil { get; private set; }

    /// <summary>
    /// List of revoked certificates.
    /// </summary>
    public List<Guid> RevokedCertificates { get; private set; }

    /// <summary>
    /// Create a new certificate revocation list.
    /// </summary>
    /// <param name="issuerId">Id of the issuer.</param>
    /// <param name="validFrom">List valid from date.</param>
    /// <param name="validUntil">List valid until date.</param>
    /// <param name="revokedCertificates">List of revoked certificates.</param>
    public RevocationList(Guid issuerId, DateTime validFrom, DateTime validUntil, IEnumerable<Guid> revokedCertificates)
    {
      IssuerId = issuerId;
      ValidFrom = validFrom;
      ValidUntil = validUntil;
      RevokedCertificates = new List<Guid>(revokedCertificates);
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public RevocationList(DeserializeContext context)
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
      context.Write(ValidFrom);
      context.Write(ValidUntil);
      context.Write(RevokedCertificates.Count);
      foreach (Guid certificateId in RevokedCertificates)
      {
        context.Write(certificateId);
      }
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      IssuerId = context.ReadGuid();
      ValidFrom = context.ReadDateTime();
      ValidUntil = context.ReadDateTime();
      RevokedCertificates = new List<Guid>();
      int count = context.ReadInt32();
      for (int index = 0; index < count; index++)
      {
        RevokedCertificates.Add(context.ReadGuid());
      }
    }
  }
}