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
    public SignatureRequestInfo(string emailAddress)
    {
      EmailAddress = emailAddress;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public SignatureRequestInfo(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(EmailAddress);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      EmailAddress = context.ReadString();
    }
  }
}
