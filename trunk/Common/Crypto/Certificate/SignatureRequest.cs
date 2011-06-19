/*
 * Copyright (c) 2009, Pirate Party Switzerland
 * All rights reserved.
 * 
 * Licensed under the New BSD License as seen in License.txt
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Pirate.PiVote.Serialization;

namespace Pirate.PiVote.Crypto
{
  /// <summary>
  /// Request for a signature by a CA.
  /// </summary>
  [SerializeObject("Request for a signature by a CA.")]
  public class SignatureRequest : Serializable
  {
    private const string KeyHashDelimiter = "$$$";

    /// <summary>
    /// First name of requester.
    /// </summary>
    [SerializeField(0, "First name of requester.")]
    public string FirstName { get; private set; }

    /// <summary>
    /// Family name of requester.
    /// </summary>
    [SerializeField(1, "Family name of requester.")]
    public string FamilyName { get; private set; }

    /// <summary>
    /// Email address of requester.
    /// </summary>
    [SerializeField(2, "Email address of requester.")]
    public string EmailAddress { get; private set; }

    /// <summary>
    /// Full name of the requester for display.
    /// </summary>
    public string FullName
    {
      get { return string.Format("{0}, {1}", FamilyName, FirstName); }
    }

    /// <summary>
    /// Is this request valid?
    /// </summary>
    public bool Valid
    {
      get
      {
        return !FirstName.IsNullOrEmpty() &&
               !FamilyName.IsNullOrEmpty() &&
               Mailer.IsEmailAddressValid(EmailAddress);
      }
    }

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
    public SignatureRequest(DeserializeContext context, byte version)
      : base(context, version)
    { }

    public byte[] Key
    {
      get 
      {
        SHA256Managed sha = new SHA256Managed();
        return sha.ComputeHash(ToBinary());
      }
    }

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
    protected override void Deserialize(DeserializeContext context, byte version)
    {
      base.Deserialize(context, version);
      FirstName = context.ReadString();
      FamilyName = context.ReadString();
      EmailAddress = context.ReadString();
    }

    public byte[] Encrypt()
    { 
      return Aes.Encrypt(ToBinary(), Key);
    }

    public static SignatureRequest Decrypt(byte[] encrypted, byte[] key)
    {
      return Serializable.FromBinary<SignatureRequest>(Aes.Decrypt(encrypted, key));
    }
  }
}
