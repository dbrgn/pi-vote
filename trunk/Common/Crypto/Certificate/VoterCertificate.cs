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
    public VoterCertificate()
    {
    }

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

    public override void Serialize(SerializeContext context)
    {
      base.Serialize(context);
    }

    protected override void Deserialize(DeserializeContext context)
    {
      base.Deserialize(context);
    }

    public override Certificate OnlyPublicPart
    {
      get { return new VoterCertificate(this, true); }
    }

    public override bool CanSignCertificates
    {
      get { return false; }
    }

    public override byte[] MagicTypeConstant
    {
      get { return Encoding.UTF8.GetBytes("VoterCertificate"); }
    }
  }
}
