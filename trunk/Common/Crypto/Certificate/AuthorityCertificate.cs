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
  /// Certificate of a voting authority.
  /// </summary>
  public class AuthorityCertificate : Certificate
  {
    /// <summary>
    /// Full name of the authority.
    /// </summary>
    public string FullName { get; private set; }

    /// <summary>
    /// Create a new certificate for an authority.
    /// </summary>
    /// <param name="fullName">Full name of the authority.</param>
    public AuthorityCertificate(string fullName)
      : base()
    {
      if (fullName == null)
        throw new ArgumentNullException("fullName");

      FullName = fullName;
    }

    public AuthorityCertificate(DeserializeContext context)
      : base(context)
    { }

    /// <summary>
    /// Creates a copy of the certificate.
    /// </summary>
    /// <param name="original">Original certificate to copy.</param>
    /// <param name="onlyPublicPart">Leave the private key out?</param>
    protected AuthorityCertificate(AuthorityCertificate original, bool onlyPublicPart)
      : base(original, onlyPublicPart)
    {
      FullName = original.FullName;
    }

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
      context.Write(FullName);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
      FullName = context.ReadString();
    }

    public override Certificate OnlyPublicPart
    {
      get { return new AuthorityCertificate(this, true); }
    }

    protected override void AddSignatureContent(BinaryWriter writer)
    {
      base.AddSignatureContent(writer);
      writer.Write(FullName);
    }

    public override bool IsRootCertificate
    {
      get { return false; }
    }

    public override bool CanSignCertificates
    {
      get { return false; }
    }

    public override byte[] MagicTypeConstant
    {
      get { return Encoding.UTF8.GetBytes("AuthorityCertificate"); }
    }
  }
}
