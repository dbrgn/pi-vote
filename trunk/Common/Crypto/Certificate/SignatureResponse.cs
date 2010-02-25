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
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Response to a signature request.
  /// </summary>
  public class SignatureResponse : Serializable
  {
    /// <summary>
    /// Id of the subject of this signature request response.
    /// </summary>
    public Guid SubjectId { get; private set; }

    /// <summary>
    /// Status of the signature request.
    /// </summary>
    public SignatureResponseStatus Status { get; private set; }

    /// <summary>
    /// Reason the request was declined or empty.
    /// </summary>
    public string Reason { get; private set; }

    /// <summary>
    /// Signature of CA if accepted.
    /// </summary>
    public Signature Signature { get; private set; }

    /// <summary>
    /// Create an accepting response.
    /// </summary>
    /// <param name="signature">Signature of CA.</param>
    public SignatureResponse(Guid subjectId, Signature signature)
    {
      SubjectId = subjectId;
      Status = SignatureResponseStatus.Accepted;
      Signature = signature;
      Reason = string.Empty;
    }

    /// <summary>
    /// Create a declining response.
    /// </summary>
    /// <param name="reason">Reason the request was declined.</param>
    public SignatureResponse(Guid subjectId, string reason)
    {
      SubjectId = subjectId;
      Status = SignatureResponseStatus.Declined;
      Signature = null;
      Reason = reason;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public SignatureResponse(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(SubjectId);
      context.Write((int)Status);
      context.Write(Reason);
      context.Write(Signature);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      SubjectId = context.ReadGuid();
      Status = (SignatureResponseStatus)context.ReadInt32();
      Reason = context.ReadString();
      Signature = context.ReadObject<Signature>();
    }
  }
}
