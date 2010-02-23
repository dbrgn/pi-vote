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
  /// Request for a signature by a CA.
  /// </summary>
  public class SignatureRequest : Serializable
  {
    /// <summary>
    /// First name of requester.
    /// </summary>
    public string FirstName { get; private set; }

    /// <summary>
    /// Family name of requester.
    /// </summary>
    public string FamilyName { get; private set; }

    /// <summary>
    /// Email address of requester.
    /// </summary>
    public string EmailAddress { get; private set; }

    /// <summary>
    /// Create a new signature request.
    /// </summary>
    /// <param name="firstName">First name of requester.</param>
    /// <param name="familyName">Family name of requester.</param>
    /// <param name="emailAddress">Email address of requester.</param>
    public SignatureRequest(string firstName, string familyName, string emailAddress)
    {
      FirstName = firstName;
      FamilyName = familyName;
      EmailAddress = emailAddress;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public SignatureRequest(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(FirstName);
      context.Write(FamilyName);
      context.Write(EmailAddress);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      FirstName = context.ReadString();
      FamilyName = context.ReadString();
      EmailAddress = context.ReadString();
    }
  }
}
