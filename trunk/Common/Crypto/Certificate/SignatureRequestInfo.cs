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
using System.IO;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Information that accompanies a request for a signature by a CA.
  /// </summary>
  [SerializeObject("Information that accompanies a request for a signature by a CA.")]
  public class SignatureRequestInfo : Serializable
  {
    /// <summary>
    /// Email address of requester.
    /// </summary>
    [SerializeField(0, "Email address of requester.")]
    public string EmailAddress { get; private set; }

    /// <summary>
    /// Encrypted request data.
    /// </summary>
    [SerializeField(1, "Encrypted request data.", 1)]
    public byte[] EncryptedSignatureRequest { get; private set; }

    public override byte Version
    {
      get { return 1; }
    }

    /// <summary>
    /// Is this request info valid?
    /// </summary>
    /// <remarks>
    /// Email address can be empty. If so you won't get any emails.
    /// </remarks>
    public bool Valid
    {
      get
      {
        return EmailAddress.IsNullOrEmpty() || 
          Mailer.IsEmailAddressValid(EmailAddress);
      }
    }

    /// <summary>
    /// Create a new signature request.
    /// </summary>
    /// <param name="emailAddress">Email address of requester.</param>
    /// <param name="fromDataHash">Symmetrically encrypted signature request.</param>
    public SignatureRequestInfo(string emailAddress, byte[] encryptedSignatureRequest)
    {
      EmailAddress = emailAddress;
      EncryptedSignatureRequest = encryptedSignatureRequest;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public SignatureRequestInfo(DeserializeContext context, byte version)
      : base(context, version)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(EmailAddress);
      context.Write(EncryptedSignatureRequest);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      EmailAddress = context.ReadString();

      if (version >= 1)
      {
        EncryptedSignatureRequest = context.ReadBytes();
      }
      else
      {
        EncryptedSignatureRequest = new byte[0];
      }
    }
  }
}
