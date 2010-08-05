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
  /// Certificate for a voter.
  /// </summary>
  /// <remarks>
  /// Does not contain the voter's name to keep his identity confidential.
  /// </remarks>
  public class VoterCertificate : Certificate
  {
    /// <summary>
    /// Creates a new voter certificate.
    /// </summary>
    /// <param name="language">Language preferred by the certificate holder.</param>
    /// <param name="passphrase">Passphrase to encrypt the key with or null for no encryption.</param>
    /// <param name="canton">Canton the holder inhabits.</param>
    public VoterCertificate(Language language, string passphrase, Canton canton)
      : base(language, passphrase)
    {
      AddAttribute(new Int32CertificateAttribute(CertificateAttributeName.Canton, (int)canton));
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public VoterCertificate(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Create a copy of the certificate.
    /// </summary>
    /// <param name="original">Original certificate to copy.</param>
    /// <param name="onlyPublicPart">Leave the private key part out?</param>
    protected VoterCertificate(VoterCertificate original, bool onlyPublicPart)
      : base(original, onlyPublicPart)
    {
    }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
    }

    /// <summary>
    /// Returns only the public key part of the certificate.
    /// </summary>
    /// <remarks>
    /// Used to remove the private key.
    /// </remarks>
    public override Certificate OnlyPublicPart
    {
      get { return new VoterCertificate(this, true); }
    }

    /// <summary>
    /// Is this certificate allowed to sign others.
    /// </summary>
    public override bool CanSignCertificates
    {
      get { return false; }
    }

    /// <summary>
    /// The magic certificate type constant makes sure
    /// a certificate is never mistaken for another type
    /// of certificate.
    /// </summary>
    public override byte[] MagicTypeConstant
    {
      get { return Encoding.UTF8.GetBytes("VoterCertificate"); }
    }

    /// <summary>
    /// Type of the certificate in multilingual text.
    /// </summary>
    public override string TypeText
    {
      get { return LibraryResources.CertificateTypeVoter; }
    }

    /// <summary>
    /// Canton the voter inhabites.
    /// </summary>
    public Canton Canton
    {
      get
      {
        int? value = GetAttributeValueInt32(CertificateAttributeName.Canton);

        if (value == null)
        {
          return Canton.None;
        }
        else
        {
          return (Canton)value;
        }
      }
    }
  }
}
