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
  /// Certificate of a voting server.
  /// </summary>
  public class ServerCertificate : Certificate
  {
    /// <summary>
    /// Full name of the administrator.
    /// </summary>
    private string fullName;

    /// <summary>
    /// Create a new certificate for an voting server.
    /// </summary>
    /// <param name="language">Language preferred by the certificate holder.</param>
    /// <param name="passphrase">Passphrase to encrypt the key with or null for no encryption.</param>
    /// <param name="fullName">Full name of the voting server.</param>
    public ServerCertificate(string fullName)
      : base(Language.English, null)
    {
      if (fullName == null)
        throw new ArgumentNullException("fullName");

      this.fullName = fullName;
    }

    /// <summary>
    /// Creates an object by deserializing from binary data.
    /// </summary>
    /// <param name="context">Context for deserialization.</param>
    public ServerCertificate(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Creates a copy of the certificate.
    /// </summary>
    /// <param name="original">Original certificate to copy.</param>
    /// <param name="onlyPublicPart">Leave the private key out?</param>
    protected ServerCertificate(ServerCertificate original, bool onlyPublicPart)
      : base(original, onlyPublicPart)
    {
      this.fullName = original.fullName;
    }

    /// <summary>
    /// Serializes the object to binary.
    /// </summary>
    /// <param name="context">Context for serializable.</param>
    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(this.fullName);
    }

    /// <summary>
    /// Deserializes binary data to object.
    /// </summary>
    /// <param name="context">Context for deserialization</param>
    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      this.fullName = context.ReadString();
    }

    /// <summary>
    /// Returns only the public key part of the certificate.
    /// </summary>
    /// <remarks>
    /// Used to remove the private key.
    /// </remarks>
    public override Certificate OnlyPublicPart
    {
      get { return new ServerCertificate(this, true); }
    }

    /// <summary>
    /// Adds data to be signed to the stream.
    /// </summary>
    /// <remarks>
    /// Be sure to call the base when overriding.
    /// </remarks>
    /// <param name="writer">Stream writer.</param>
    protected override void AddSignatureContent(BinaryWriter writer)
    {
      base.AddSignatureContent(writer);
      writer.Write(FullName);
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
      get { return Encoding.UTF8.GetBytes("ServerCertificate"); }
    }

    /// <summary>
    /// Full name of the administrator.
    /// </summary>
    public override string FullName
    {
      get { return this.fullName; }
    }

    /// <summary>
    /// Type of the certificate in multilingual text.
    /// </summary>
    public override string TypeText
    {
      get { return LibraryResources.CertificateTypeServer; }
    }
  }
}
