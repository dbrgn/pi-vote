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
  public class VoterCertificate : Certificate
  {
    public VoterCertificate()
    {
    }

    public VoterCertificate(DeserializeContext context)
      : base(context)
    { }

    public VoterCertificate(VoterCertificate original, bool onlyPublicPart)
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
  }
}
