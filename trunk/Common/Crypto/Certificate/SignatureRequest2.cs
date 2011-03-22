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
  /// Request for a signature by a CA signed by another certificate.
  /// </summary>
  [SerializeObject("Request for a signature by a CA signed by another certificate.")]
  public class SignatureRequest2 : SignatureRequest
  {
    /// <summary>
    /// Signature from the signing certificate.
    /// </summary>
    [SerializeField(0, "Signature from the signing certificate.")]
    public byte[] Signature { get; private set; }

    /// <summary>
    /// Signing certificate.
    /// </summary>
    [SerializeField(1, "Signing certificate.")]
    public Certificate SigningCertificate { get; private set; }

    /// <summary>
    /// Create a new signature request.
    /// </summary>
    /// <param name="firstName">First name of requester.</param>
    /// <param name="familyName">Family name of requester.</param>
    /// <param name="emailAddress">Email address of requester.</param>
    /// <param name="SigningCertificate">Signing certificate.</param>
    public SignatureRequest2(string firstName, string familyName, string emailAddress, Certificate signingCertificate)
      : base(firstName, familyName, emailAddress)
    {
      SigningCertificate = signingCertificate.OnlyPublicPart;
      Signature = signingCertificate.Sign(GetSignedData());
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public SignatureRequest2(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(Signature);
      context.Write(SigningCertificate);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      Signature = context.ReadBytes();
      SigningCertificate = context.ReadObject<Certificate>();
    }

    private byte[] GetSignedData()
    { 
      MemoryStream memoryStream = new MemoryStream();
      SerializeContext context = new SerializeContext(memoryStream);
      context.Write(FirstName);
      context.Write(FamilyName);
      context.Write(EmailAddress);
      context.Close();
      memoryStream.Close();

      return memoryStream.ToArray();
    }

    public bool IsSignatureValid()
    {
      return SigningCertificate.VerifySimple(GetSignedData(), Signature);
    }
  }
}
